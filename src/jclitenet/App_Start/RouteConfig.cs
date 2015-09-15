using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace jclitenet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "PorfolioWizard", // Route name
              "portfolio-wizard", // URL with parameters
                new { controller = "Home", action = "MyPortfolio", type = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "Development", // Route name
              "Tutorial/{type}", // URL with parameters
                new { controller = "Tutorial", action = "Index", type = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "Category", // Route name
              "Tutorial/Category/{id}/{name}", // URL with parameters
                new { controller = "Tutorial", action = "Category", id = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "TutorialView", // Route name
              "Tutorial/View/{cat}/{id}/{name}", // URL with parameters
                new { controller = "Tutorial", action = "View", cat = UrlParameter.Optional, id = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "PhotoGallery", // Route name
              "PhotoGallery/Album/{id}/{name}", // URL with parameters
                new { controller = "PhotoGallery", action = "Album", id = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "Games", // Route name
              "Games/Display/{id}/{name}", // URL with parameters
                new { controller = "Games", action = "Display", id = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
              "Archive", // Route name
              "Archive/{year}/{month}", // URL with parameters
                new { controller = "Archive", action = "Index", year = UrlParameter.Optional, month = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "jclitenet.Controllers" }
            );
        }
    }
}