using ApiCRU.AuthService;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebCRU.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth/createsession/{uporabnikID}")]
        public HttpResponseMessage Auth(string UporabnikID)
        {

            AuthServiceClient proxy = new AuthServiceClient();

            DAuth auth = proxy.GetSession(UporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, auth);

            return response;
        }
    }
}
