using System;
using System.Web;

namespace CoreFramework4
{
    public class CookieTool
    {
        public static void Remove(string key)
        {
            HttpContext.Current.Request.Cookies.Remove(key);
        }

        public static bool IsExist(string key)
        {
            return HttpContext.Current.Request.Cookies.Get(key) != null;
        }

        public static string GetValue(string key)
        {
            var httpCookie = HttpContext.Current.Request.Cookies.Get(key);
            return httpCookie != null ? httpCookie.Value : string.Empty;
        }

        public static HttpCookie Get(string key)
        {
            return HttpContext.Current.Request.Cookies.Get(key);
        }

        public static void Add(HttpCookie cookie)
        {
            if (IsExist(cookie.Name))
            {
                Remove(cookie.Name);
            }
            HttpContext.Current.Request.Cookies.Add(cookie);
        }

        public static void Add(string name, string value, DateTime expiration)
        {
            Add(new HttpCookie(name) { Value = value, Expires = expiration });
        }
    }
}
