using System;
using System.Net;
using WebCRU.DTO;
using System.Web.Http;
using System.Net.Http;
using System.Web;

namespace WebCRU.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth/{uporabnikID}")]
        public HttpResponseMessage Auth(string uporabnikID)
        {

            DAuth auth = new DAuth()
            {
                SessionAuthToken = Guid.NewGuid().ToString()
            };

            HttpContext.Current.Session["AuthSessionToken"]= auth.SessionAuthToken;

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, auth);


            return response;
        }
    }
}
