using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Configuration;

namespace CoreFramework4
{
    public static class ErrorHandler
    {
        public static void RegisterErrorHandler(HttpApplication httpApplication)
        {
            httpApplication.Error += httpApplication_Error;
        }

        private static void httpApplication_Error(object sender, EventArgs e)
        {
            var currentException = HttpContext.Current.Server.GetLastError();
            HandleError(currentException);
        }

        public static void HandleError(Exception exception)
        {
            try
            {
                //Find innermost exception and check for 404-error
                var ex = exception;
                while (true)
                {
                    #region Ignore 404
                    if ((ex is HttpException) && (ex as HttpException).GetHttpCode() == 404) return;
                    #endregion

                    #region Ignore MaxRequestExceededException
                    const int TimedOutExceptionCode = -2147467259;
                    if ((ex is HttpException) && (ex as HttpException).ErrorCode == TimedOutExceptionCode)
                    {
                        // hack: no alternative method of identifing if the error is max request exceeded as it is treated as a timeout exception
                        if (exception.StackTrace.Contains("GetEntireRawContent")) return;
                    }
                    #endregion

                    //Find innermost exception
                    if (ex.InnerException == null) break;
                    ex = ex.InnerException;
                }

                var innermostException = ex;

                //Report
                var errorReportXml = GetExceptionsXml(exception);
                string reportHtml = GetErrorReportHtml(errorReportXml);

                #region Send email
                //Message
                var message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["Error_Email_Reporting_From"]);
                message.To.Add(ConfigurationManager.AppSettings["Error_Email_Reporting_To"]);
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Error_Email_Reporting_Cc"])) message.CC.Add(ConfigurationManager.AppSettings["Error_Email_Reporting_Cc"]);
                message.IsBodyHtml = true;
                message.Body = reportHtml;

                //Check for invalid message subject (when error message contains invalid characters)
                try
                {
                    string subject = "Error report: " + System.Text.RegularExpressions.Regex.Replace(innermostException.Message, @"[^ -~]", "");
                    message.Subject = subject;
                }
                catch
                {
                    message.Subject = "Error report";
                }

                //Attachment
                Stream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(errorReportXml.ToString()));
                var attachment = new Attachment(stream, "error.xml", "text/xml");
                message.Attachments.Add(attachment);

                //Send
                var client = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["Error_Email_Reporting_SMTPHost"],
                    Port = ConfigurationManager.AppSettings["Error_Email_Reporting_Port"].TryParseInt().Value,
                    Timeout = ConfigurationManager.AppSettings["Error_Email_Reporting_TimeOut"].TryParseInt().Value,
                    UseDefaultCredentials = true,
                    EnableSsl = ConfigurationManager.AppSettings["Error_Email_Reporting_EnabledSsl"].TryParseBool().Value
                };

                if (ConfigurationManager.AppSettings["Error_Email_Reporting_UserName"] != null) 
                    client.Credentials = new System.Net.NetworkCredential(
                        ConfigurationManager.AppSettings["Error_Email_Reporting_UserName"], 
                        ConfigurationManager.AppSettings["Error_Email_Reporting_Password"]);

                client.Send(message);

                #endregion

            }
            catch (Exception ex)
            {
                //ignore exception to avoid infinite loop
            }
        }

        private static XElement GetExceptionsXml(Exception exception)
        {
            var xml = new XElement("ErrorReport");
            var session = new XElement("Session");
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    foreach (string key in HttpContext.Current.Session.Keys)
                    {
                        string value = HttpContext.Current.Session[key].ToString();
                        if (value.Length > 500) value = value.Substring(0, 500);
                        session.Add(new XElement("Item", new XAttribute("Key", key), value));
                    }
                }
            }
            catch { }

            //Request info
            if (HttpContext.Current.Request != null)
            {
                xml.Add(
                        new XElement("Request",
                            new XElement("DateTime", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString()),
                            new XElement("URL", HttpContext.Current.Request.Url.AbsoluteUri),
                            new XElement("IPAddress", HttpContext.Current.Request.UserHostAddress),
                            new XElement("Server", HttpContext.Current.Server.MachineName),
                            new XElement("PhysicalApplicationPath", HttpContext.Current.Request.PhysicalApplicationPath),
                            new XElement("Browser", HttpContext.Current.Request.Browser.Browser + "(" + HttpContext.Current.Request.Browser.MajorVersion + ")"),
                            session
                        )
                );
            }

            //Get exceptions
            var exceptions = new List<Exception>();
            var ex = exception;
            while (true)
            {
                exceptions.Add(ex);
                if (ex.InnerException == null) break;
                ex = ex.InnerException;
            }

            //Iterate the exceptions
            exceptions.Reverse(); //start with innermost exception first
            exceptions.ForEach(exItem =>
            {
                var exceptionXml = new XElement("Exception"
                    , new XElement("Message", exItem.Message)
                    , new XElement("Method", exItem.TargetSite.Name)
                    , new XElement("Class", exItem.TargetSite.ReflectedType.FullName)
                    , new XElement("Line", RetrieveLineNumber(exItem))
                    , new XElement("DLL", exItem.TargetSite.ReflectedType.Module.Name)
                    , new XElement("Source", exItem.Source)
                    , new XElement("StackTrace", exItem.StackTrace)
                );

                if (ex is HttpException)
                {
                    exceptionXml.Add(new XElement("HttpException"
                        , new XElement("ErrorCode", ((HttpException)ex).ErrorCode.ToString())
                        , new XElement("HttpCode", (ex as HttpException).GetHttpCode().ToString())
                        , new XElement("HtmlErrorMessage", (ex as HttpException).GetHtmlErrorMessage())));
                }

                xml.Add(exceptionXml);
            });

            return xml;
        }

        private static int RetrieveLineNumber(Exception ex)
        {
            try
            {
                if (ex.StackTrace.IndexOf("line") == -1) return -1;
                return int.Parse(ex.StackTrace.Substring(ex.StackTrace.IndexOf("line"), 15).Split(" ".ToCharArray())[1]);
            }
            catch
            {
                return -1;
            }
        }

        private static string GetErrorReportHtml(XElement xmlExceptionInfo)
        {

            string xsltTempate = GetXsltTemplate();
            string retval = string.Empty;

            var xsl = new XslCompiledTransform();
            xsl.Load(new XmlTextReader(new StringReader(xsltTempate)));

            var sw = new StringWriter();
            var xslarg = new XsltArgumentList();

            try
            {
                xsl.Transform(xmlExceptionInfo.CreateReader(), xslarg, sw);
                retval = sw.ToString();
            }
            catch { }
            finally
            {
                sw.Close();
                sw.Dispose();
            }

            return retval;

        }

        //read the embedded file
        private static string GetXsltTemplate()
        {
            StreamReader reader = null;
            string retval = string.Empty;

            //get the embedded file
            Assembly Asm = Assembly.GetExecutingAssembly();
            Stream strm = Asm.GetManifestResourceStream(Asm.GetName().Name + ".ErrorReporting.EmailReport.xslt");

            // Read the content of the embedded file.
            reader = new StreamReader(strm);
            retval = reader.ReadToEnd();
            reader.Dispose();
            reader.Close();

            return retval;
        }
    }
}