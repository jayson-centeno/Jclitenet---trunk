using System.Web.Mvc;
using System.Web.Security;
using CoreFramework4.Implementations.Security;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
