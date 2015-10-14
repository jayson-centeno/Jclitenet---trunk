using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace jclitenet
{
    using Controllers.Api;
    using System.Web.Mvc;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Authentication",
                routeTemplate: "{action}/{email}/{password}",
                defaults: new
                {
                    controller = GetFriendlyControllerName(typeof(AppController)),
                    action = "Authenticate"
                }
            );

            config.Routes.MapHttpRoute(
                name: "App Api",
                routeTemplate: "app/{action}/{id}",
                defaults: new { controller = GetFriendlyControllerName(typeof(AppController)), id = RouteParameter.Optional }
            );

        }

        private static string GetFriendlyControllerName(Type controller)
        {
            return controller.Name.ToLowerInvariant().Replace("controller", "");
        }
    }
}
