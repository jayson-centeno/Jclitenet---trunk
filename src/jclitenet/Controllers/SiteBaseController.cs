using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using CoreFramework4.Infrastructure.Application;
using CoreFramework4.Implementations.Security;
using jclitenet.Models;
using CoreFramework4.Infrastructure.Services;
using CoreFramework4.Infrastructure.Repository;
using CoreFramework4;

namespace jclitenet.Controllers
{
    public class SiteBaseController : Controller, ISiteBaseController
    {
        public SiteBaseController()
        {
            ViewBag.RecentPostItems = RecentPostItems;
            ViewBag.LatestAlbums = LatestAlbums;
            ViewBag.LatestTutorials = LatestTutorials;
            ViewBag.LatestComments = LatestComments;
            ViewBag.CurrentLogin = ServiceFactory.GetInstance<IAuthenticationService>().CurrentUser;
        }

        public IEnumerable<SideMenuModelItem> RecentPostItems
        {
            get
            {
                var items = new List<SideMenuModelItem>();

                return items;

                items.Add(new SideMenuModelItem()
                {
                    Posted = DateTime.Now,
                    Title = "test"
                });

                items.Add(new SideMenuModelItem()
                {
                    Posted = DateTime.Now,
                    Title = "test2"
                });

                return items;
            }
        }

        public IEnumerable<SideMenuModelItem> LatestAlbums
        {
            get
            {
                return ServiceFactory.GetInstance<IAlbumService>()
                                     .GetAllAlbum()
                                     .Select(
                                        a => 
                                            new SideMenuModelItem() 
                                            {
                                                Posted = a.DateCreated.Value,
                                                Title = a.Name,
                                                ID = a.ID,
                                                Href = HttpServerTool.ToUrlAction("Album", "PhotoGallery", new {a.ID, name = a.Name.ToSlug() })
                                            });
            }
        }

        public IEnumerable<SideMenuModelItem> LatestTutorials
        {
            get
            {
                return ServiceFactory.GetInstance<ITutorialService>()
                                     .GetAllTutorialWithCategoryWithComments
                                         .Select(
                                            a => 
                                                new SideMenuModelItem() {
                                                    Posted = a.DateCreated.Value,
                                                    Title = a.Name,
                                                    ID = a.ID,
                                                    Href = HttpServerTool.ToUrlAction("View", "Tutorial", new { cat = a.TutorialCategory.ID, id = a.ID, name = a.Name.ToSlug() })
                                                })
                                         .OrderBy(x => Guid.NewGuid())
                                         .Take(5);
            }
        }

        public IEnumerable<SideMenuModelItem> LatestComments
        {
            get
            {
                var commentService = ServiceFactory.GetInstance<ICommentService>();
                var lst = (from comment in commentService.GetCommentsWithParent() 
                           where comment.IsApproved
                           let tempHref = (comment.Tutorial_ID.HasValue)?
                                            HttpServerTool.ToUrlAction("View", "Tutorial", 
                                                new
                                                {
                                                    cat = comment.Tutorial.TutorialCategory.ID, 
                                                    id = comment.Tutorial.ID, 
                                                    name = comment.Tutorial.Name.ToSlug()
                                                }) : String.Empty
                           let website = (comment.IsValidWebSite.HasValue && 
                                          comment.IsValidWebSite.Value) 
                                          ? comment.Website : String.Empty
                           select new SideMenuModelItem
                           {
                                        Posted = comment.DateCreated.Value,
                                        Title = comment.Tutorial.Name,
                                        Name = comment.AnonymousName,
                                        ID = comment.ID,
                                        Href = tempHref,
                                        Website = website
                          })
                          .OrderByDescending(x => x.Posted)
                          .Take(5);

                return lst;
            }
        }
    }
}
