using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoreFramework4.Implementations.Security;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;
using CoreFramework4.Model;
using System;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class TutorialController : Controller
    {
        public ActionResult Index(int type)
        {
            var tutorialRepository = ServiceFactory.GetInstance<ITutorialRepository>();
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

        public ActionResult Category(int catType, int id)
        {
            var tutorialRepository = ServiceFactory.GetInstance<ITutorialRepository>();
            var currentCategory = tutorialRepository.GetAllTutorialCategory.First(c => c.ID == id);
            var listOfTutorials = tutorialRepository.GetAllTutorialByCategoryID(id);

            var modelList = new TutorialModelList()
            {
                CategoryName = currentCategory.Name,
                CategoryId = currentCategory.ID,
                TutorialModels = new List<TutorialModel>()
            };

            if (listOfTutorials.Count() != 0)
            {
                var tutorialModel = listOfTutorials.Select(t => new TutorialModel()
                {
                    ID = t.ID,
                    Name = t.Name,
                    HtmlContent = t.HtmlContent,
                    DateCreated = t.DateCreated
                });

                modelList.TutorialModels = tutorialModel;
            }

            return View(modelList);
        }

        public ActionResult Edit(int id)
        {
            var repository = ServiceFactory.GetInstance<ITutorialRepository>();
            var tutorial = repository.GetByIDWithTutorialCategory(id);

            var model = new TutorialModel()
            {
                ID = tutorial.ID,
                Name = tutorial.Name,
                HtmlContent = tutorial.HtmlContent.ToHtmlDecode(),
                DateCreated = tutorial.DateCreated
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TutorialModel turialModel)
        {
            var repository = ServiceFactory.GetInstance<ITutorialRepository>();
            var tutorial = repository.GetByIDWithTutorialCategory(turialModel.ID);

            if (ModelState.IsValid)
            {
                tutorial.Name = turialModel.Name;
                tutorial.HtmlContent = turialModel.HtmlContent.ToHtmlEncode();
                repository.SaveChanges();

                return RedirectToAction("Category", "Tutorial", new { catType = 1, id = tutorial.TutorialCategory.ID, });
            }
            else
            {
                turialModel = new TutorialModel()
                {
                    ID = tutorial.ID,
                    Name = tutorial.Name,
                    HtmlContent = tutorial.HtmlContent.ToHtmlDecode(),
                    DateCreated = tutorial.DateCreated
                };
            }

            return View(turialModel);
        }

        [HttpPost]
        public ActionResult Create(TutorialModel turialModel)
        {
            if (ModelState.IsValid)
            {
                var repository = ServiceFactory.GetInstance<ITutorialRepository>();
                var currentCategory = repository.GetAllTutorialCategory.First(c => c.ID == turialModel.Category.CategoryId);

                var tut = new Tutorial
                {
                    Name = turialModel.Name,
                    HtmlContent = turialModel.HtmlContent.ToHtmlEncode(),
                    IsDeleted = false,
                    DateCreated = DateTime.Now,
                    TutorialCategory = currentCategory
                };

                repository.Add(tut);
                repository.SaveChanges();

                return RedirectToAction("Category", "Tutorial", new { catType = 1, id = currentCategory.ID, });
            }

            return View();
        }

        public ActionResult Create(int id)
        {
            var model = new TutorialModel();
            model.Category.CategoryId = id;

            return View(model);
        }

        public ActionResult Delete(int id, int catType, int catId)
        {
            var tutorialRepository = ServiceFactory.GetInstance<ITutorialRepository>();
            var tutorial = tutorialRepository.GetByIDWithTutorialCategory(id);
            if (tutorial != null)
            {
                tutorialRepository.Delete(tutorial);
                tutorialRepository.SaveChanges();
                return RedirectToAction("Category", "Tutorial", new { catType = catType, id = catId });
            }

            return View();
        }
    }
}
