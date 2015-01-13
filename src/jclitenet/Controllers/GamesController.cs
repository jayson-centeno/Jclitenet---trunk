using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;
using System.Linq;

namespace jclitenet.Controllers
{
    public class GamesController : SiteBaseController
    {
        private readonly IGameRepository _gameRepository;
        public GamesController(IGameRepository gameRepository) 
        {
            _gameRepository = gameRepository;
        }

        public ActionResult Index()
        {
            var games = _gameRepository
                            .GetAll()
                            .Select(
                                g => new GamesModel()
                                    {
                                        ID = g.ID,
                                        Name = g.Name
                                });
            return View(games);
        }

        public ActionResult Display(int id)
        {
            var game = _gameRepository.GetQuery()
                                     .Include("Comments")
                                     .First(g => g.ID == id);

            var Comments = game.Comments
                            .Where(c => c.IsApproved)
                            .Select(c => new CommentModel() { 
                                CommentText = c.CommentText,
                                AnonymousName = c.AnonymousName,
                                DateCreated = c.DateCreated,
                                Email = c.Email,
                                Website = c.Website
                            });

            var gameModel = new GamesModel() { ID = game.ID, Name = game.Name, HtmlContent = game.HtmlContent.ToHtmlDecode() };
            gameModel.Comments = Comments;

            return View(gameModel);
        }
    }
}
