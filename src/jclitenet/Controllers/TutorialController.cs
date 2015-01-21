using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace jclitenet.Controllers
{
    public class TutorialController : SiteBaseController
    {
        private readonly ITutorialRepository _tutorialRepository;

        public TutorialController(ITutorialRepository tutorialRepository)
        {
            _tutorialRepository = tutorialRepository;
        }

        public ActionResult Index(int type)
        {
            var tutorialRepository = _tutorialRepository;
            var items = tutorialRepository.GetAllTutorialCategory
                                          .Where(c => c.TutorialType.ID == type)
                                          .Select(a => new CategoryModel()
            {
                CategoryId = a.ID,
                Title = a.Name,
                TypeId = a.TutorialType.ID
            });

            return View(items);
        }

        public ActionResult View(int cat, int id)
        {
            var tutorialRepository = _tutorialRepository;
            var viewedTutorial = tutorialRepository.GetQuery()
                                                   .Include("Comments")
                                                   .Include("TutorialCategory")
                                                   .First(r => r.ID == id);

            var model = new TutorialModel()
            {
                ID = viewedTutorial.ID,
                Name = viewedTutorial.Name,
                HtmlContent = viewedTutorial.HtmlContent.ToHtmlDecode(),
                DateCreated = viewedTutorial.DateCreated,
                Category = new CategoryModel() { CategoryId = viewedTutorial.TutorialCategory.ID },
                Comments = viewedTutorial.Comments
                            .Where(c => c.IsApproved)
                            .Select(c => new CommentModel() { 
                                Id = c.ID,
                                CommentText = c.CommentText,
                                AnonymousName = c.AnonymousName,
                                DateCreated = c.DateCreated,
                                Email = c.Email,
                                Website = c.Website,
                                IsValidWebSite = c.IsValidWebSite.HasValue? c.IsValidWebSite.Value : false
                            })
            };

            return View(model);
        }

        public ActionResult Category(int id)
        {
            var tutorialRepository = _tutorialRepository;
            var currentCategory = tutorialRepository.GetTutorialCategoryByID(id);
            var listOfTutorials = tutorialRepository.GetAllTutorialWithCommentsByCategoryID(id);

            var modelList = new TutorialModelList()
            {
                CategoryName = currentCategory.Name,
                TutorialModels = new List<TutorialModel>(),
                CategoryId = currentCategory.ID
            };

            if (listOfTutorials.Any())
            {
                var tutorialModel = listOfTutorials.Select(t => new TutorialModel()
                {
                    ID = t.ID,
                    Name = t.Name,
                    HtmlContent = t.HtmlContent,
                    DateCreated = t.DateCreated,
                    CommentCount = t.Comments.Count(),
                    Category = new CategoryModel() { CategoryId = id }
                });

                modelList.TutorialModels = tutorialModel;
            }

            return View(modelList);
        }
    }
}