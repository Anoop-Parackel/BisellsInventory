using Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Entities.Master;
using System.Web.Http;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class CustomersController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Save(Customer c)
        {
            if (c.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetDetails(CompanyId));
        }

        /// <summary>
        /// used in customer list
        /// </summary>
        /// <param name="CompanyId">id of the particular company</param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomerList([FromBody]int CompanyId, [FromUri]int? Customer_id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetCustomerDetails(CompanyId, Customer_id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomerInvoice([FromBody]int LocationId, [FromUri] int customer_id, [FromUri] DateTime? FromDate, [FromUri]DateTime? ToDate)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Register.SalesEntryRegister.GetDetails(LocationId, customer_id, FromDate, ToDate));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetDetails(CompanyId, id));
        }
        /// <summary>
        /// Get activities of customer for customer list
        /// </summary>
        /// <param name="id">id of the customer for filter customer details</param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomerActivity([FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetCustomerActivity(id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetStates([FromBody]int CompanyId, [FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)State.GetStates(id, CompanyId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCountry([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Country.GetCountry(CompanyId));
        }

        public void Post([FromBody]string value)
        {
        }

        //public void Put(int id, [FromBody]string value)
        //{
        //}
        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, customer.Delete());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomerAddress([FromUri]int CustomerID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetCustomerAddress(CustomerID));
        }

        /// <summary>
        /// Address Save Controller
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SaveAddress(Customer c)
        {
            if (c.CustomerAddressID>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.UpdateCustomerAddress());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.SaveCustomerAddress());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCustomerAddressEdit([FromUri]int CustomerID,[FromBody]int CustomerAddressID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Customer.GetCustomerAddressEdit(CustomerAddressID, CustomerID));
        }


        [HttpPost]
        public HttpResponseMessage SetPrimaryAddress(Customer c)
        {
          return Request.CreateResponse(HttpStatusCode.OK, c.SetAsPrimary());
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAddress([FromUri]int id, [FromBody]int modifiedBy)
        {
            Customer customer = new Customer();
            customer.ID = id;
            customer.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, customer.DeleteAddress());
        }
    }
}