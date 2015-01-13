using CoreFramework4;
using CoreFramework4.Implementations.Security;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Migration.Migration;
using CoreFramework4.Utility;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Html;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace jclitenet
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();

            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            routeData.Values["exception"] = exception;

            Response.StatusCode = 500;
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 401:
                        routeData.Values["action"] = "Http401";
                        break;
                    case 403:
                        routeData.Values["action"] = "Http403";
                        break;
                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                }

                if (Response.StatusCode == 500)
                {
                    var authExp = httpException as AuthorizationException;
                    if (authExp != null)
                    {
                        if (authExp.ErrorCode == 401)
                        {
                            routeData.Values["action"] = "Http401";
                        }
                    }
                }

            }

            // Avoid IIS7 getting in the middle
            Response.TrySkipIisCustomErrors = true;

            IController errorsController = new jclitenet.Controllers.ErrorController();
            var wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authenticationService = ServiceFactory.GetInstance<IAuthenticationService>();
            authenticationService.AutoLogin();
        }
    }
}