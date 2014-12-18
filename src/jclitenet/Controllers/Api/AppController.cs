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
        [HttpGet]
        public IEnumerable<Tutorial> GetAllTutorials()
        {
            return Enumerable.Repeat<Tutorial>(new Tutorial()
            {
                Description = "test",
                Id = 5
            }, 5);
        }

        //public HttpResponseMessage Get(int id)
        //{
        //    return Request.CreateResponse<Tutorial>(HttpStatusCode.OK, new Tutorial()
        //    {
        //        Description = "test",
        //        Id = 1
        //    });
        //}

        //// PUT api/customers/5
        //public HttpResponseMessage Put(int id, [FromBody]Tutorial tutorial)
        //{
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //// PUT api/customers/5
        //public HttpResponseMessage Post([FromBody]Tutorial tutorial)
        //{
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        //public HttpResponseMessage Delete(int id)
        //{
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}
    }
}