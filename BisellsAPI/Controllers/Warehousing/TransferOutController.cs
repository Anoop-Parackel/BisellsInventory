using BisellsAPI.Filters;
using Entities.WareHousing;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class TransferOutController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(TransferOut to)
        {
            if (to.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, to.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, to.Save());
            }
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferOut.GetDetails(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferOut.GetDetails(LocationId, from, to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferOut.GetDetails(Id,LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            TransferOut tr = new TransferOut();
            tr.ID = Id;
            tr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, tr.Delete());
        }
        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
    }
}