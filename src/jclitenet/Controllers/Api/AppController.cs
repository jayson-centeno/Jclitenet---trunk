using CoreFramework4.Infrastructure.Services;
using jclitenet.Models.Spa.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jclitenet.Controllers.Api
{
    public class AppController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AppController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
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
    }
}