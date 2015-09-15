using CoreFramework4;
using CoreFramework4.Implementations.Services;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace jclitenet.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly ISiteConfigurationService _siteconfigurationService;
        private readonly IMailService _emailService;
        private readonly IBlogRepository _blogRepository;
        private readonly IChangeLogRepository _changeLogRepository;

        public HomeController(ISiteConfigurationService siteconfigurationService, 
                              IMailService emailService,
                              IBlogRepository blogRepository,
                              IChangeLogRepository changeLogRepository) 
        {
            _siteconfigurationService = siteconfigurationService;
            _emailService = emailService;
            _blogRepository = blogRepository;
            _changeLogRepository = changeLogRepository;
        }
        
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
            if(ModelState.IsValid) _emailService.SendContactMe(contactModel.Email, contactModel.Message);
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
            var blogs = new BlogModel();
            //blogRepository.GetAll()
            //                          .Include(b=> b.CreatedBy)
            //                          .Include(b => b.Comments);
            return View(blogs);
        }

        public async Task<ActionResult> SiteLog()
        {
            var lst = await _changeLogRepository.GetAllAsyncList();
            var vm = lst.Select(m => new ChangeLogModel()
                        {
                            DateCreated = m.DateCreated,
                            Description = m.Description
                        }).OrderByDescending(c => c.DateCreated);

            return View(vm);
        }

        public ActionResult MyPortfolio()
        {
            return View();
        }
    }
}
