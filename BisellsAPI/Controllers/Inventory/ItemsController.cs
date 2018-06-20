using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class ItemsController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            var result = Product.GetDetails(CompanyId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET api/<controller>/5
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Product.GetDetails(id, CompanyId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetDetailsForService([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Product.GetDetailsForService(CompanyId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetDetailsForService([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Product.GetDetailsForService(id, CompanyId));
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
            Product product = new Product();
            product.ItemID = id;
            product.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, product.Delete());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetAsscociateData([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)Product.getAssociateData(CompanyId));
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Product Product)
        {
            if (Product.ItemID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Product.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Product.Update());
            }
        }
    }
}