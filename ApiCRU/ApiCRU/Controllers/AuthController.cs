using System;
using System.Net;
using WebCRU.DTO;
using System.Web.Http;
using System.Net.Http;
using System.Web;
using WebCRU.Auth;

namespace WebCRU.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth/{uporabnikID}")]
        public HttpResponseMessage Auth(string UporabnikID)
        {

            DAuth auth = new DAuth()
            {
                SessionAuthToken = Guid.NewGuid().ToString(),
                IsUserValidInAD = AuthWorker.ValidateUser(UporabnikID)
            };

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, auth);


            return response;
        }
    }
}
