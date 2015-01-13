using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Models;
using jclitenet.Helper;

namespace jclitenet.Controllers
{
    public class PartialController : Controller
    {
        private readonly ITutorialRepository _tutorialRepository;

        public PartialController(ITutorialRepository tutorialRepository) {
            _tutorialRepository = tutorialRepository;
        }

        public PartialViewResult PartialTuTorialCategories()
        {
            var items = _tutorialRepository.GetAllTutorialCategory.Select(Hydrators.HydrateSideMenuModel);
            return PartialView("_SubmenuPartial", items);
        }

        public PartialViewResult PartialTutorials(int? page = 1)
        {
            int pageSize = SiteConfigTool.GetValue<int>("home-tutorial-page-size");

            if (pageSize == 0) pageSize = 5;

            int skipPage = pageSize * (page.Value == 1 ? 0 : page.Value - 1);
            int takepage = pageSize;

            var lst = _tutorialRepository.GetAllTutorialWithCategoryWithComments
                                        .Select(a => new SideMenuModelItem()
                                        {
                                            Posted = a.DateCreated.Value,
                                            Title = a.Name,
                                            ID = a.ID,
                                            Href = Url.Action("View", "Tutorial", new { cat = a.TutorialCategory.ID, id = a.ID, name = a.Name.ToSlug() }),
                                            Content = a.HtmlContent.ToHtmlDecode(),
                                            CommentCount = a.Comments.Count()
                                        })
                                        .Skip(skipPage).Take(takepage)
                                        .OrderByDescending(t => t.CommentCount);

            return PartialView("_PagingTutorialsPartial", lst);
        }
        
    }
}
