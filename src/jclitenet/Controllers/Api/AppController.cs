using CoreFramework4.Implementations.Security;
using CoreFramework4.Infrastructure.Services;
using jclitenet.Domain.Objects.Poco;
using jclitenet.Models.Spa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace jclitenet.Controllers.Api
{

    public class AppController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IResumeService _resumeService;

        public AppController(IAuthenticationService authenticationService,
            IResumeService resumeService)
        {
            _authenticationService = authenticationService;
            _resumeService = resumeService;
        }

        [HttpGet]
        public IEnumerable<Tutorial> GetAllTutorials()
        {
            return Enumerable.Repeat<Tutorial>(new Tutorial()
            {
                Description = "test",
                Id = 5
            }, 5);
        }

        [HttpGet]
        public bool Authenticate(string email, string password)
        {
            return _authenticationService.LogIn(email, password, false);
        }

        [HttpGet]
        [SiteAuthorization]
        public async Task<ResumeDetails> GetResumeDetails()
        {
            if (_authenticationService.CurrentUser == null) return null;

            return await _resumeService.GetResumeDetails(_authenticationService.CurrentUser.Id);
        }

        [SiteAuthorization]
        [HttpPost]
        public void UpdateResumeIntroduction([FromBody]SimpleValue value)
        {
            if (_authenticationService.CurrentUser == null) return;

            _resumeService.UpdateIntroduction(_authenticationService.CurrentUser.Id, value.Data);
        }

    }
}