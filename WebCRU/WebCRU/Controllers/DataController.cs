using si.hit.WebCRU.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace si.hit.WebCRU.Controllers
{
    public class DataController : ApiController
    {
        [Route("api/aplikacije/{uporabnikID}")]
        public HttpResponseMessage Get(string uporabnikID)
        {
            DataService service = new DataService();
            List<Aplikacija> dto = new List<Aplikacija>();
            dto = service.GetAplikacije(uporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            return response;
        }
    }
}
