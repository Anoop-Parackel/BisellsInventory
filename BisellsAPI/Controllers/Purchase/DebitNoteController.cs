using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.Purchase
{
    public class DebitnoteController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(DebitNote pr)
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

        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, PurchaseReturnRegister.GetDetails(LocationId));
        //}

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id, [FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, DebitNote.GetDetails(Id, LocationId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetPurchaseEntry([FromBody]int LocationId, [FromUri] int SupplierId, [FromUri] int ItemId, [FromUri] int instanceId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetailsItemWise(LocationId, SupplierId, ItemId, instanceId));

        }
        /// <summary>
        /// return sales return register of damaged items for purchase return
        /// </summary>
        /// <param name="LocationId"></param>
        /// <param name="SupplierId"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetSalesReturn([FromBody]int LocationId, [FromUri] int SupplierId, [FromUri] int ItemId, [FromUri] int InstanceId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesReturnRegister.GetDetailsDamageType(LocationId, SupplierId, ItemId, InstanceId));
        }

        [HttpPost]
        public HttpResponseMessage GetSalesReturnDetails([FromBody]int LocationId, [FromUri] int ID, [FromUri] int type)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesReturnRegister.GetDetailsDamageType(LocationId, ID, type));
        }
        [HttpPost]
        public HttpResponseMessage GetPurchaseEntryDetails([FromBody]int LocationId, [FromUri] int ID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetailsFromEntry(LocationId, ID));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetFilterDetails([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, DebitNote.GetDetails(LocationId, SupplierId, from, to));
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int purchaseid, [FromUri]string toAddress, [FromUri]int userId)
        {
           return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => DebitNote.SendMail(purchaseid, toAddress, userId, url)));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            DebitNote pr = new DebitNote();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }
    }
}
