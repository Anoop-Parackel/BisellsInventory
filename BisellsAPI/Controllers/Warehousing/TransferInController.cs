using BisellsAPI.Filters;
using Entities.WareHousing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class TransferInController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(Entities.WareHousing.TransferIn ti)
        {
            if (ti.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ti.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ti.Save());
            }
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferIn.GetDetails(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferIn.GetDetails(LocationId, from, to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, TransferIn.GetDetails(Id,LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTransferOut([FromBody] int LocationId)
        {
            List<TransferOut> TransferOuts = TransferIn.GetDetailsFromTransferOut(LocationId).Where(x=>x.Status==0).ToList();
            TransferOuts.ForEach(x=>x.Products.RemoveAll(y=>y.Status!=0));
            return Request.CreateResponse(HttpStatusCode.OK, TransferOuts);
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            TransferIn tr = new TransferIn();
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