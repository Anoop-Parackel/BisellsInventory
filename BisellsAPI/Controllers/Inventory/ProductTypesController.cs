using BisellsAPI.Filters;
using Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class ProductTypesController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ProductType.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ProductType.GetDetails(id, CompanyId));
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
            ProductType producttype = new ProductType();
            producttype.ID = id;
            producttype.Modifiedby = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, producttype.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]ProductType ProductType)
        {
            if (ProductType.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ProductType.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ProductType.Update());
            }
        }
    }
}