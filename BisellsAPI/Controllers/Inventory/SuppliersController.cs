using BisellsAPI.Filters;
using Entities;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class SuppliersController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(Supplier s)
        {
            if (s.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, s.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, s.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetDetails(id, CompanyId));
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
        public HttpResponseMessage Delete([FromUri]int id,[FromBody]int modifiedBy)
        {
            Supplier supplier = new Supplier();
            supplier.ID = id;
            supplier.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, supplier.Delete());
            
        }

        [HttpPost]
        public HttpResponseMessage GetSupplierList([FromBody]int CompanyId, [FromUri]int? supplierId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetSupplierDetails(CompanyId, supplierId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSupplierActivity([FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetSupplierActivity(id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSupplierAddress([FromUri]int SupplierID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetSupplierAddress(SupplierID));
        }

        /// <summary>
        /// Address Save Controller
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveAddress(Supplier sup)
        {
            if (sup.SupplierAddressID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, sup.UpdateSupplierAddress());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, sup.SaveSupplierAddress());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSupplierAddressEdit([FromUri]int SupplierID, [FromBody]int SupplierAddressID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetSupplierAddressEdit(SupplierAddressID, SupplierID));
        }

        [HttpPost]
        public HttpResponseMessage SetPrimaryAddress(Supplier Sup)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Sup.SetAsPrimary());
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAddress([FromUri]int id, [FromBody]int modifiedBy)
        {
            Supplier Supplier = new Supplier();
            Supplier.ID = id;
            Supplier.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.DeleteAddress());
        }
    }
}