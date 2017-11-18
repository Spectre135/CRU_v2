using ApiCRU.AuthService;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebCRU.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth/createsession/")]
        public HttpResponseMessage Auth(string uporabnikID)
        {

            AuthServiceClient authService = new AuthServiceClient();

            DAuth dAuth = authService.GetSession(uporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dAuth);

            return response;
        }


        [HttpGet]
        [Route("api/auth/roles/")]
        public HttpResponseMessage Auth(string uporabnikID,string apl)
        {

            AuthServiceClient authService = new AuthServiceClient();

            DAplRoles roles = authService.GetApplicationRoles(uporabnikID, apl);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, roles);

            return response;
        }
    }
}
