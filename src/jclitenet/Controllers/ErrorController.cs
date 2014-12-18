using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jclitenet.Models;

namespace jclitenet.Controllers
{
    public class ErrorController : SiteBaseController
    {
        public ActionResult Index(Exception exception)
        {
            // log the error here
            return View(exception);
        }

        public ActionResult Http401()
        {
            return View("401");
        }

        public ActionResult Http404()
        {
            return View("404");
        }

        public ActionResult Http403()
        {
            return View("403");
        }
    }
}
