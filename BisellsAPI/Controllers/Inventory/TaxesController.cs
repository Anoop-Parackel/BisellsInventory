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
    public class TaxesController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Tax.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Tax.GetDetails(id, CompanyId));
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Tax tax)
        {
            if (tax.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, tax.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, tax.Update());
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Tax tax = new Tax();
            tax.ID = id;
            tax.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, tax.Delete());

        }
    }
}