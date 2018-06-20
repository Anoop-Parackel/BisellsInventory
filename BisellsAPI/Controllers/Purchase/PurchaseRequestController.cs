using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using Entities;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class PurchaseRequestController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(PurchaseRequestRegister pr)
        {
            if (pr.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, pr.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, pr.Save());
            }
        }
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage SearchItem(string keyword,int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetails(keyword, LocationId));
        }

        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(Id,LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id,[FromBody] int modifiedBy)
        {
            PurchaseRequestRegister pr = new PurchaseRequestRegister();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(LocationId, SupplierId, from, to));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetFilterDetails([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(LocationId, SupplierId, from, to));
        }
    }

}