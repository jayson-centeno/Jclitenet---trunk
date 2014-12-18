using System.Web.Mvc;

namespace jclitenet.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              "AdminTutorial", // Route name
              "Admin/Tutorial/{type}", // URL with parameters
                new { controller = "Tutorial", action = "Index", type = UrlParameter.Optional },
                new[] { "jclitenet.Areas.Admin.Controllers" }
            );

            context.MapRoute(
              "AdminTutorialCategory", // Route name
              "Admin/Tutorial/Category/{catType}/{id}", // URL with parameters
                new { controller = "Tutorial", action = "Category", catType = UrlParameter.Optional, id = UrlParameter.Optional },
                new[] { "jclitenet.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
