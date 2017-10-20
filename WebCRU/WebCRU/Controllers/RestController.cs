using WebCRU.Models;
using si.hit.WebCRU.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCRU.DAO;
using WebCRU.DTO;

namespace si.hit.WebCRU.Controllers
{
    public class RestController : ApiController
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


        /*Drugi način branja baze*/
        [Route("api/aplikacije2/{searchString}")]
        public HttpResponseMessage Get(string searchString, int pageIndex, int pageSizeSelected, string sortKey, string asc)
        {
            DResponse dto = new DResponse();
            DAOService service = new DAOService();
            dto = service.GetDResponse(searchString, pageIndex, pageSizeSelected, sortKey, asc);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            return response;
        }


    }
}
