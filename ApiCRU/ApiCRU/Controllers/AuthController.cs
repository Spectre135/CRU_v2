using System;
using System.Net;
using ApiCRU.DTO;
using System.Web.Http;
using System.Net.Http;
using System.Web;
using WebCRU.Auth;

namespace WebCRU.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth/createsession/{uporabnikID}")]
        public HttpResponseMessage Auth(string UporabnikID)
        {

            DAuth auth = AuthWorker.CreateSession(UporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, auth);


            return response;
        }
    }
}
