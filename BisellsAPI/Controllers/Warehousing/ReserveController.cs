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
    public class ReserveController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetReservedProducts([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Reserve.GetReservedProducts(LocationId));
        }
        [HttpPost]
        public HttpResponseMessage UpdateReserve([FromUri]int InstanceId,[FromUri]int Id, [FromUri] int LocationId, [FromUri] int ModifiedBy,[FromBody] decimal Quantity)
        {
           
            return Request.CreateResponse(HttpStatusCode.OK, Reserve.UpdateReserve(InstanceId,Id, LocationId, ModifiedBy, Quantity));
        }
        [HttpPost]
        public HttpResponseMessage ReserveQty([FromUri]int InstanceId, [FromUri] int ItemId, [FromUri] int locationId,[FromUri] int CreatedBy, [FromBody]decimal Quantity)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Reserve.ReserveQty(InstanceId, ItemId, locationId, Quantity, CreatedBy));
        }
        [HttpPost]
        public HttpResponseMessage DeleteReserve([FromUri]int Id, [FromUriAttribute] int InstanceId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Reserve.ReserveDelete(Id, InstanceId));
        }
    }
}