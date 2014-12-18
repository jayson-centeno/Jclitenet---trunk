using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace CoreFramework4.Implementations.Security
{
    public class SiteAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthorizationException();
            }

            if ((httpContext.User.Identity as LogonInfo).IsGuest)
            {
                throw new AuthorizationException();
            }

            return true;
        }
    }
}
