using System.Linq;
using System.Web.Mvc;
using CoreFramework4.Implementations.Repository;
using CoreFramework4.Model;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;

namespace jclitenet.Controllers
{
    public class BlogController : SiteBaseController
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult DisplayBlog(int id)
        {
            var blog = _blogRepository.First(b => b.Id == id);
            return View(blog);
        }

        public ActionResult Edit(int id)    
        {
            var blog = _blogRepository.First(b => b.Id == id);
            return View(blog);
        }

        [HttpPost]
        public ActionResult Edit(Blog blog)
        {
            Blog eblog = null;
            if (ModelState.IsValid)
            {
                eblog = _blogRepository.First(b => b.Id == blog.Id);
                eblog.Name = blog.Name;
                eblog.Content = blog.Content;
                _blogRepository.SaveChanges();
            }

            return View(eblog);
        }

        public ActionResult Index()
        {
            var blogs = _blogRepository.GetAll()
                                      .ToList()
                                      .Select(b => new BlogModel()
                                      {
                                            ID = b.Id,
                                            Name = b.Name,
                                            Content = b.Content
                                      });
            return View(blogs);
        }
    }
}