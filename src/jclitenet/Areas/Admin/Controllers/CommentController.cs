using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Implementations.Security;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Model;
using jclitenet.Areas.Admin.Models;
using jclitenet.Models;

namespace jclitenet.Areas.Admin.Controllers
{
    [SiteAuthorization]
    public class CommentController : Controller
    {
        public ActionResult Index(int? SelectedTutorial)
        {
            var commentService = ServiceFactory.GetInstance<ICommentService>();
            var tutorialService = ServiceFactory.GetInstance<ITutorialRepository>();

            var comments = new List<AdminCommentModel>();
            if (SelectedTutorial.HasValue)
            {
                comments = commentService.GetAll()
                                         .Select(c => new AdminCommentModel()
                                         {
                                             Id = c.ID,
                                             AnonymousName = c.AnonymousName,
                                             CommentText = c.CommentText,
                                             DateCreated = c.DateCreated,
                                             Email = c.Email,
                                             Website = c.Website,
                                             IsApproved = c.IsApproved,
                                             IpAddress = c.IpAddress,
                                             Tutorial_ID = c.Tutorial_ID,
                                             IsValidWebSite = c.IsValidWebSite.HasValue? c.IsValidWebSite.Value : false
                                         }).
                                         Where(c => c.Tutorial_ID == SelectedTutorial)
                                         .ToList();
            }

            var tutorials = tutorialService.GetAll()
                                           .Select(t => new AdminTutorialModel()
                                           {
                                               ID = t.ID,
                                               Name = t.Name
                                           }).ToList();

            var selectModel = new AdminTutorialModel() { ID = 0, Name = "Select" };
            tutorials.Insert(0, selectModel);

            var adminCommentModelHeader = new AdminCommentModelHeader()
            {
                Comments = comments,
                Tutorials = tutorials,
                SelectedTutorial = selectModel.Name
            };

            return View(adminCommentModelHeader);
        }

        [HttpPost]
        public ActionResult Index(IEnumerable<AdminCommentModel> comments)
        {
            var commentService = ServiceFactory.GetInstance<ICommentService>();
            if (ModelState.IsValid && comments != null)
            {
                var lstId = comments.Select(c => c.Id).ToArray();
                var lstComments = commentService.GetAll()
                                                .Where(c => lstId.Any(c1 => c1 == c.ID))
                                                .AsParallel()
                                                .ToList();
                foreach (var item in comments)
                {
                    if (item.IsDelete)
                    {
                        commentService.DeleteById(item.Id);
                    }
                    else
                    {
                        var comment = lstComments.First(c => c.ID == item.Id);
                        comment.IsApproved = item.IsApproved;
                        comment.IsValidWebSite = item.IsValidWebSite;
                    }
                }

                commentService.SaveChanges();
            }

            int? id = (comments.Count() > 0) ? comments.ToArray()[0].Tutorial_ID : null;

            return RedirectToAction("Index", new { SelectedTutorial = id });
        }
    }
}