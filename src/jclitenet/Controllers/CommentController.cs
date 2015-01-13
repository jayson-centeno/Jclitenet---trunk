using System;
using System.Linq;
using System.Web.Mvc;
using CoreFramework4;
using CoreFramework4.Infrastructure.Repository;
using jclitenet.Models;
using CoreFramework4.Model;
using CoreFramework4.Infrastructure.Services;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace jclitenet.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMailService _emailService;

        public CommentController(ICommentRepository commentRepository, IMailService emailService) 
        {
            _commentRepository = commentRepository;
            _emailService = emailService;
        }

        public ActionResult Save(CommentModel model)
        {
            string ip = HttpServerTool.GetIpAddress(this.Request);
            string ipToBlocked = SiteConfigTool.GetValue<string>("blockedip");

            if (!ipToBlocked.IsNullOrEmptyTrimmed() && 
                    ipToBlocked.Split(new char[] { ',' }).Any(_ => _ == ip)) 
                        return Content(String.Empty);

            var recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (recaptchaHelper.Response.IsNullOrEmptyTrimmed())
                return new JsonResult
                {
                    Data = new { Error = true, ErrorMessage = "Invalid recaptcha value" }
                };

            var recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
                return new JsonResult
                {
                    Data = new { Error = true, ErrorMessage = "Invalid recaptcha value" }
                };

            if (ModelState.IsValid)
            {
                var comment = new Comment()
                {
                    AnonymousName = model.AnonymousName,
                    CommentText = model.CommentText,
                    Email = model.Email,
                    Website = model.Website,
                    DateCreated = DateTime.Now,
                    IsDeleted = false,
                    IpAddress = HttpServerTool.GetIpAddress(this.Request)
                };

                if (model.Tutorial_ID.HasValue)
                    comment.Tutorial_ID = model.Tutorial_ID.Value;
                else if (model.Game_ID.HasValue)
                    comment.Game_ID = model.Game_ID.Value;

                _commentRepository.Add(comment);
                _commentRepository.SaveChanges();
                _emailService.SendNewComment(comment.AnonymousName, comment.CommentText, model.Title);
            }

            return Content(String.Empty);
        }
    }
}