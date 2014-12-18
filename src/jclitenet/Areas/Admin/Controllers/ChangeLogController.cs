using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreFramework4.Implementations.Security;
using jclitenet.Models;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4;
using CoreFramework4.Model;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class ChangeLogController : Controller
    {
        public ActionResult Index()
        {
            var all = ServiceFactory.GetInstance<IChangeLogRepository>()
                          .GetAll()
                          .Select(c => new ChangeLogModel() { 
                            ID = c.ID,
                            DateCreated = c.DateCreated,
                            Description = c.Description
                          });

            return View(all);
        }

        public ActionResult Register(ChangeLogModel model)  
        {
            if (ModelState.IsValid)
            {
                ServiceFactory.GetInstance<IChangeLogRepository>()
                              .Register(new ChangeLog()
                              {
                                  Description = model.Description,
                                  DateCreated = model.DateCreated
                              });

                return RedirectToAction("index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var changeLog = ServiceFactory.GetInstance<IChangeLogRepository>()
                                          .Single(c => c.ID == id);

            return View(new ChangeLogModel() { 
                            ID = changeLog.ID,
                            DateCreated = changeLog.DateCreated,
                            Description = changeLog.Description
                        });
        }

        public ActionResult Delete(int id)
        {
            var repo = ServiceFactory.GetInstance<IChangeLogRepository>();

            repo.Delete(repo.Single(c => c.ID == id));
            repo.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(ChangeLogModel model)
        {
            if (ModelState.IsValid)
            {
                var changeLog = ServiceFactory.GetInstance<IChangeLogRepository>()
                                              .Single(c => c.ID == model.ID);
                changeLog.Description = model.Description;
                changeLog.DateCreated = model.DateCreated;
                ServiceFactory.GetInstance<IChangeLogRepository>()
                              .SaveChanges();

                return RedirectToAction("index");
            }

            return View();
        }
    }
}
