using CoreFramework4;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Models;
using System.Linq;
using System.Web.Mvc;

namespace jclitenet.Controllers
{
    public class HomeController : SiteBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inspiration()
        {
            return View();
        }

        public ActionResult About()
        {
            var service = ServiceFactory.GetInstance<ISiteConfigurationService>();
            string aboutValue = service.GetConfigValue<string>(SiteConfigName.ABOUT);

            return View();
        }

        public ActionResult SiteInfo()
        {
            return View();
        }

        public ActionResult Contact()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels contactModel)
        {
            if(ModelState.IsValid)
                ServiceFactory.GetInstance<IMailService>()
                              .SendContactMe(contactModel.Email, contactModel.Message);

            return RedirectToAction("Contact");
        }

        public ActionResult Tutorials()
        {
            return View();
        }

        public ActionResult PhotoGallery()
        { 
           return View();
        }

        public ActionResult Blogs()
        {
            var blogRepository = ServiceFactory.GetInstance<IBlogRepository>();
            BlogModel blogs = new BlogModel();
            //blogRepository.GetAll()
            //                          .Include(b=> b.CreatedBy)
            //                          .Include(b => b.Comments);
            return View(blogs);
        }

        public ActionResult SiteLog()
        {
            return View(ServiceFactory.GetInstance<IChangeLogRepository>()
                                                       .GetAll()
                                                       .Select(m => new ChangeLogModel() {
                                                           DateCreated = m.DateCreated,
                                                           Description = m.Description
                                                       })
                                                       .OrderByDescending(c => c.DateCreated)
                                                       .AsParallel()
                      );
        }

        public ActionResult MyPortfolio()
        {
            return View();
        }
    }
}
