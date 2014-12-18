using System;
using System.Web;
using System.Web.Mvc;

namespace CoreFramework4
{
    public class HttpServerTool
    {
        public const string HTTP_HOST = "HTTP_HOST";
        public const string QUERY_STRING = "QUERY_STRING";

        public static string GetServerHttpHost()
        {
            return HttpContext.Current.Request.ServerVariables[HTTP_HOST];
        }

        public static string GetServerQueryString()
        {
            return HttpContext.Current.Request.ServerVariables[QUERY_STRING];
        }

        public static string GetIpAddress(HttpRequestBase request)
        {
            string ip;
            try
            {
                ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    if (ip.IndexOf(",") > 0)
                    {
                        string[] ipRange = ip.Split(',');
                        int le = ipRange.Length - 1;
                        ip = ipRange[le];
                    }
                }
                else
                {
                    ip = request.UserHostAddress;
                }
            }
            catch { ip = null; }

            return ip;
        }

        public static string ToUrlAction(string action, string controller, object route)
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext)
                            .Action(action, controller, route);
        }

    }
}
