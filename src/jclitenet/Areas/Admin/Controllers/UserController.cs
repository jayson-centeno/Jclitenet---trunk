using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jclitenet.Controllers;
using System.Web.Security;
using CoreFramework4.Implementations.Security;
using jclitenet.Models;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Model;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class UserController : SiteBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUsers()
        {
            return View();
        }
    }
}