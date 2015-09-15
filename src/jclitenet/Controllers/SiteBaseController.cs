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
using System.Threading.Tasks;

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
            ViewBag.Archives = Archives;
            ViewBag.CurrentLogin = ServiceFactory.GetInstance<IAuthenticationService>().CurrentUser;
        }

        private IEnumerable<SideMenuModelItem> RecentPostItems
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

        private IEnumerable<SideMenuModelItem> LatestAlbums
        {
            get
            {
                var albumsTask = Task.Run(() => ServiceFactory.GetInstance<IAlbumService>().GetAllAlbumAsync());
                return albumsTask.Result.Select(a =>
                                        new SideMenuModelItem()
                                        {
                                            Posted = a.DateCreated.Value,
                                            Title = a.Name,
                                            ID = a.ID,
                                            Href = HttpServerTool.ToUrlAction("Album", "PhotoGallery", new { a.ID, name = a.Name.ToSlug() })
                                        });
            }
        }

        private IEnumerable<SideMenuModelItem> LatestTutorials
        {
            get
            {
                var task = Task.Run(() => ServiceFactory.GetInstance<ITutorialService>().GetAllTutorialWithCategoryWithCommentsAsync(5));
                return task.Result.Select(
                            a =>
                                new SideMenuModelItem
                                {
                                    Posted = a.DateCreated.Value,
                                    Title = a.Name,
                                    ID = a.ID,
                                    Href = HttpServerTool.ToUrlAction("View", "Tutorial", new { cat = a.TutorialCategory.ID, id = a.ID, name = a.Name.ToSlug() })
                                })
                            .OrderBy(x => Guid.NewGuid());
            }
        }

        private IEnumerable<SideMenuModelItem> LatestComments
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

        private IEnumerable<SideMenuModelItem> Archives 
        {
            get
            {
                var x = ServiceFactory.GetInstance<ITutorialService>()
                                     .GetAllTutorial()
                                     .GroupBy(g => g.DateCreated.Value.Date)
                                     .Select(g => new { title = g.Key.Date.ToString("MMMM yyyy"), 
                                                        year = g.Key.Date.ToString("yyyy"), 
                                                        month = g.Key.Date.ToString("MM") })
                                     .Distinct()
                                     .Select(
                                        a =>
                                            new SideMenuModelItem
                                            {
                                                Title = a.title, 
                                                Href = HttpServerTool.ToUrlAction("Index", "Archive", new { year = a.year, month = a.month })
                                            }).Distinct();


                return x;
            }
        }
    }
}
