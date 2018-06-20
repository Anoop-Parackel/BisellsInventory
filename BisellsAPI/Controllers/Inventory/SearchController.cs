using BisellsAPI.Filters;
using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class SearchController : ApiController
    {
        //Search by Item Name or Code
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage Items(string keyword,int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetails(keyword, CompanyId));
           
        }
        /// <summary>
        /// used in register out and transfer out
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage ItemsFromPurchaseWithStock(string keyword, int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetailsFromPurchaseWithStock(keyword, LocationId));
        }
        //[HttpGet]
        //public string ItemsFromPurchaseSupplierWise(string keyword,int SupplierId,int LocationId,int ReturnType)
        //{
        //    return JsonConvert.SerializeObject(Item.GetDetailsFromPurchaseSupplierWise(keyword,SupplierId,LocationId, ReturnType));
        //}
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage ItemsFromSalesCustomerWise(string keyword, int CustomerId, int companyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetailsFromSalesCustomerWise(keyword, CustomerId, companyId));
        }
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage ItemsFromPurchaseWithScheme(string keyword, int CustomerId,int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetailsFromPurchaseWithScheme(keyword, CustomerId, LocationId));
        }


    }
}