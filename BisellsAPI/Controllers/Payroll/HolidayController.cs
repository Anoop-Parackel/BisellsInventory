using BisellsAPI.Filters;
using Entities.Master;
using Entities.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class HolidayController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Holiday.GetDetails(CompanyId));
        }

        //[HttpPost]
        //public HttpResponseMessage Save([FromBody]Holiday h)
        //{
        //  return Request.CreateResponse(HttpStatusCode.OK, h.Save());
        //}

        [HttpPost]
        public HttpResponseMessage Save(Holiday h)
        {
            if (h.Id > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, h.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, h.Save());
            }
        }
    }
}