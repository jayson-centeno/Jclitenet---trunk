using System.Linq;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;

namespace jclitenet.Areas.Admin.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            var games = ServiceFactory.GetInstance<IGameRepository>()
                                      .GetAll().Select(g => new GamesModel()
                                      {
                                          ID = g.ID,
                                          Name = g.Name,
                                      });

            return View(games);
        }

        public ActionResult Edit(int? id)
        {
            var game = ServiceFactory.GetInstance<IGameRepository>()
                                      .First(g => g.ID == id);

            return View(new GamesModel() { ID = game.ID, Name = game.Name, HtmlContent = game.HtmlContent.ToHtmlDecode() });
        }

        [HttpPost]
        public ActionResult Edit(GamesModel gameModel)
        {
            var repository = ServiceFactory.GetInstance<IGameRepository>();
            if(ModelState.IsValid)
            {
                var game = repository.First(g => g.ID == gameModel.ID);
                game.Name = gameModel.Name;
                game.HtmlContent = gameModel.HtmlContent.ToHtmlEncode();
                ServiceFactory.GetInstance<IGameRepository>().SaveChanges();

                return RedirectToAction("Edit", "Game", new { id = game.ID });
            }

            return View();
        }

    }
}
