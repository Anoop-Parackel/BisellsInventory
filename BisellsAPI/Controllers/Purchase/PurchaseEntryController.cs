using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class PurchaseEntryController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(PurchaseEntryRegister pe)
        {
            if (pe.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, pe.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, pe.Save());
            }
        }

        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetails(Id,LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSupplierWiseDetails(dynamic Params)
        {
            int SupplierId = Convert.ToInt32(Params.SupplierId);
            int LocationId = Convert.ToInt32(Params.LocationId);
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetails(LocationId).Where(x => x.SupplierId == SupplierId));
        }
       
       [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id,[FromBody] int modifiedBy)
        {
            PurchaseEntryRegister pr = new PurchaseEntryRegister();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int purchaseid, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => PurchaseEntryRegister.SendMail(purchaseid, toAddress, userId, url)));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseEntryRegister.GetDetails(LocationId, SupplierId, from, to));
        }
        [HttpGet]
        public HttpResponseMessage GetPayments([FromUri]int PurchaseId)
        {

            return Request.CreateResponse(HttpStatusCode.OK, (object)PurchaseEntryRegister.GetPayments(PurchaseId));
        }
    }
}