using CoreFramework4;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace jclitenet.Controllers
{
    public class ArchiveController : SiteBaseController
    {
        private readonly ITutorialService _tutorialService;

        public ArchiveController(ITutorialService tutorialService)
        {
            _tutorialService = tutorialService;
        }

        public ActionResult Index(string year, string month)
        {
            var tutorials = _tutorialService.GetAllTutorialWithCategory();

            int? trueYear = year.TryParseInt();
            int? trueMonth = month.TryParseInt();

            if (!trueYear.HasValue) trueYear = 2005;
            if (!trueMonth.HasValue) trueMonth = 1;

            var filteredTutorials = tutorials
                                        .Where(
                                            t => t.DateCreated.Value.Date >= new DateTime(trueYear.Value, trueMonth.Value, 1) &&
                                                 t.DateCreated.Value.Date <= new DateTime(trueYear.Value, trueMonth.Value, 30))
                                        .Select(
                                            m => new TutorialModel {
                                                ID = m.ID,
                                                Category = new CategoryModel { CategoryId = m.TutorialCategory.ID },
                                                Name = m.Name,
                                                DateCreated = m.DateCreated
                                        }).ToList();

            return View(filteredTutorials);
        }
    }
}