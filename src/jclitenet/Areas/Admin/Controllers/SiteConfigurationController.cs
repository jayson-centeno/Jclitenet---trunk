using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Implementations.Security;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Model;
using jclitenet.Areas.Admin.Models;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class SiteConfigurationController : Controller
    {
        //
        // GET: /Admin/SiteConfiguration/

        public ActionResult Index()
        {
            var configs = ServiceFactory.GetInstance<ISiteConfigurationService>().GetAll();

            var models = configs.Select(c => new SiteConfigurationModel()
            {
                Id = c.ID,
                Name = c.Name,
                ConfigValue = c.ConfigValue.ToHtmlDecode(),
                IsDeleted = c.IsDeleted.Value,
                IsHtml = c.IsHtml.Value
            });

            return View(models);
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<SiteConfigurationModel> editedConfigs)
        {
            var service = ServiceFactory.GetInstance<ISiteConfigurationService>();
            if (ModelState.IsValid && editedConfigs != null)
            {
                var configs = service.GetAll();

                editedConfigs = editedConfigs.Where(c => c.IsEdited);

                var lstId = editedConfigs.Select(c => c.Id)
                                         .ToArray();

                if (lstId.Count() == 0)
                {
                    return RedirectToAction("Index");
                }

                var dbModels = configs.Where(c => lstId.Any(c1 => c1 == c.ID))
                                      .AsParallel()
                                      .ToList();

                foreach (var model in editedConfigs)
                {
                    var trueModel = dbModels.First(c => c.ID == model.Id);
                    trueModel.Name = model.Name;
                    trueModel.ConfigValue = model.ConfigValue.ToHtmlEncode();
                    trueModel.IsDeleted = model.IsDeleted;
                    model.IsHtml = model.IsHtml;
                    service.SaveChanges();
                }
            }

            service.CacheConfigs();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SiteConfigurationModel model)
        {
            if (ModelState.IsValid)
            {
                var service = ServiceFactory.GetInstance<ISiteConfigurationService>();

                service.Add(new SiteConfiguration()
                {
                    ConfigValue = model.ConfigValue.ToHtmlEncode(),
                    IsDeleted = model.IsDeleted,
                    IsHtml = model.IsHtml,
                    Name = model.Name
                });

                service.CacheConfigs();
            }

            return RedirectToAction("Index");
        }

    }
}
