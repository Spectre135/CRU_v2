﻿using WebCRU.Models;
using si.hit.WebCRU.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCRU.DAO;
using WebCRU.DTO;
using System;

namespace si.hit.WebCRU.Controllers
{
    public class RestController : ApiController
    {
        [HttpGet]
        [Route("api/aplikacije/{uporabnikID}")]
        public HttpResponseMessage GetAplikacije(string uporabnikID)
        {
            DAOService service = new DAOService();
            List<Aplikacija> dto = service.GetAplikacije(uporabnikID);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);

            
            return response;
        }

        [HttpGet]
        [Route("api/pravice/")]
        public HttpResponseMessage GetPravice(int aplikacijaKLJ, int vlogaKLJ)
        {
            DAOService service = new DAOService();
            List<DVlogePravice> dto = service.GetPravice(aplikacijaKLJ, vlogaKLJ);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }

        [HttpPost]
        [Route("api/aplikacije/save/")]
        public HttpResponseMessage Save([FromBody]Aplikacija aplikacija)
        {
            DAOService service = new DAOService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                service.SaveAplikacija(aplikacija);

            }catch(Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex);
            }


            return response;
        }

        [HttpPost]
        [Route("api/aplikacije/delete/")]
        public HttpResponseMessage Delete([FromBody]Aplikacija aplikacija)
        {
            DAOService service = new DAOService();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                service.DeleteAplikacija(aplikacija);

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


        [Route("api/sifranti/{id}")]
        public HttpResponseMessage GetSifranti(string id)
        {
            DAOService service = new DAOService();
            List<DSifranti> dto = service.GetDSifranti(id);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dto);


            return response;
        }


    }
}