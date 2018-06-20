using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Finance;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.Finance
{
    public class SettleBillsController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public  HttpResponseMessage GetSalesData([FromUri]int CustomerId)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) CustomerReciepts.GetSalesData(CustomerId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetPurchaseData([FromUri]int SuplierId)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) PurchasePayments.GetPurchaseData(SuplierId));
        }
        [HttpPost]
        public HttpResponseMessage SettleBillsCustomer([FromBody]dynamic Settlement)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object) CustomerReciepts.SettleBills(Settlement));
        }
        [HttpPost]
        public HttpResponseMessage SettleBillsSupplier([FromBody]dynamic Settlement)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) PurchasePayments.SettleBills(Settlement));
        }
    }
}
