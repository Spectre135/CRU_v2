using ApiCRU.DTO;
using ApiCRU.Models;
using ApiCRU.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace si.hit.WebCRU.Controllers
{
    public class RestController : ApiController
    {
        [HttpGet]
        [Route("api/aplikacije/{uporabnikID}")]
        public HttpResponseMessage GetAplikacije(string uporabnikID)
        {
            CRUDService service = new CRUDService();
            List<Aplikacija> dto = service.GetAplikacije(uporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            
            return response;
        }

        [HttpPost]
        [Route("api/aplikacije/save/")]
        public HttpResponseMessage Save([FromBody]Aplikacija aplikacija)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Save(aplikacija, db.Aplikacijas, aplikacija.AplikacijaKLJ);

            }
            catch(Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }


            return response;
        }

        [HttpPost]
        [Route("api/aplikacije/delete/")]
        public HttpResponseMessage Delete([FromBody]Aplikacija aplikacija)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Delete(aplikacija, db.Aplikacijas);

            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }


            return response;
        }

        [HttpGet]
        [Route("api/pravice/")]
        public HttpResponseMessage GetPravice(int aplikacijaKLJ, int vlogaKLJ)
        {
            CRUDService service = new CRUDService();
            List<DVlogePravice> dto = service.GetPravice(aplikacijaKLJ, vlogaKLJ);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }

        [HttpPost]
        [Route("api/pravice/save/")]
        public HttpResponseMessage Save([FromBody]DVlogePravice dVlogePravice)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Save(CRUDService.ParsePravice(dVlogePravice), db.Pravices, dVlogePravice.PravicaKLJ);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }


            return response;
        }

        [HttpPost]
        [Route("api/pravice/delete/")]
        public HttpResponseMessage Delete([FromBody]DVlogePravice dVlogePravice)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Delete(CRUDService.ParsePravice(dVlogePravice), db.Pravices);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }


            return response;
        }

        [Route("api/uporabniki")]
        public HttpResponseMessage Get()
        {
            CRUDService service = new CRUDService();
            List<Uporabniki> dto = service.GetUporabniki();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }

        [HttpPost]
        [Route("api/uporabniki/save/")]
        public HttpResponseMessage Save([FromBody]Uporabniki uporabniki)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Save(uporabniki, db.Uporabnikis, uporabniki.UporabnikKLJ);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }


            return response;
        }

        [HttpPost]
        [Route("api/uporabniki/delete/")]
        public HttpResponseMessage Delete([FromBody]Uporabniki uporabniki)
        {
            CRUDService service = new CRUDService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            Entities db = new Entities();

            try
            {
                service.Delete(uporabniki, db.Uporabnikis);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }


            return response;
        }

        /*Drugi način branja baze*/
        [Route("api/aplikacije2/{searchString}")]
        public HttpResponseMessage Get(string searchString, int pageIndex, int pageSizeSelected, string sortKey, string asc)
        {
            DResponse dto = new DResponse();
            CRUDService service = new CRUDService();
            dto = service.GetDResponse(searchString, pageIndex, pageSizeSelected, sortKey, asc);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            return response;
        }

        [Route("api/sifranti/{id}")]
        public HttpResponseMessage GetSifranti(string id)
        {
            CRUDService service = new CRUDService();
            List<DSifranti> dto = service.GetDSifranti(id);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }


    }
}
