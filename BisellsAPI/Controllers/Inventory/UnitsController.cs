using BisellsAPI.Filters;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class UnitsController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UOM.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UOM.GetDetails(id, CompanyId));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            UOM uom = new UOM();
            uom.ID = id;
            uom.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, uom.Delete());
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]UOM UOM)
        {
            if (UOM.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, UOM.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, UOM.Update());
            }
        }
    }
}