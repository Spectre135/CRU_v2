using WebCRU.Models;
using si.hit.WebCRU.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace si.hit.WebCRU.Controllers
{
    public class AplikacijeController : ApiController
    {
        [Route("api/aplikacije/{uporabnikID}")]
        public HttpResponseMessage Get(string uporabnikID)
        {
            DAOService service = new DAOService();
            List<Aplikacija> dto = service.GetAplikacije(uporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            
            return response;
        }

        [Route("api/aplikacije/aplikacija")]
        public HttpResponseMessage Post(Aplikacija aplikacija)
        {
            DAOService service = new DAOService();

            service.AddAplikacija(aplikacija);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);


            return response;
        }

        [Route("api/uporabniki")]
        public HttpResponseMessage Get()
        {
            DAOService service = new DAOService();
            List<Uporabniki> dto = service.GetUporabniki();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }


    }
}
