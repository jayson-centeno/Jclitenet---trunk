using System.Net;
using System.Net.Mail;
using System.Text;
using CoreFramework4.Infrastructure.Services;
namespace CoreFramework4.Implementations.Services
{
    public class MailService : IMailService
    {
        public void SendNewComment(string commentorName, string comment, string topicTitle)
        {
            bool sendEmail = CoreFramework4.SiteConfigTool.GetValue<bool>("EmailEnabled");
            if (!sendEmail) return;

            var bodyText = new StringBuilder();
            bodyText.Append("Commentor name: " + commentorName + " <br />");
            bodyText.Append("Comment: " + comment);

            SendDefault(bodyText.ToString(), topicTitle);
        }

        public void SendDefault(string body, string subject)
        {
            Send(body, subject, "jayson.centeno@jclitenet.com", "www.jclitenet.com", "jaysword1@yahoo.com", "jclitenet");
        }

        public void Send(string body, string subject, string fromAddress, string fromDisplayName,
            string toAddress, string toDisplayName)
        {
            var fromMailAddress = new MailAddress(fromAddress, fromDisplayName);
            var cc = new MailAddress(fromAddress, fromDisplayName);
            var toMailAddress = new MailAddress(toAddress, toDisplayName);

            string mailSubject = subject;
            string mailBody = body;

            //var smtp = new SmtpClient
            //{
            //    Host = "mail.jclitenet.com",
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Timeout = 100
            //};

            var smtp = new SmtpClient();
            //smtp.Credentials = new NetworkCredential("jayson.centeno@jclitenet.com", "darkjay1");
            var message = new MailMessage(fromMailAddress, toMailAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            message.CC.Add(cc);

            try
            {
                smtp.Send(message);
            }
            catch (System.Net.Mail.SmtpException)
            {
                
            }

        }

        public void SendContactMe(string email, string message)
        {
            Send(message, "Contact Me: " + email, email, email, "jaysword1@yahoo.com", "jclitenet");
        }
    }
}
