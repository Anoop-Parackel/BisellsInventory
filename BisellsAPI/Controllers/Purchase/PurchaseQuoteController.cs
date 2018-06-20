using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using System.Collections.Generic;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class PurchaseQuoteController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Save(PurchaseQuoteRegister Pq)
        {

            if (Pq.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Pq.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Pq.Save());
            }
        }

        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, PurchaseQuoteRegister.GetDetails(LocationId));
        //}

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseQuoteRegister.GetDetails(Id,LocationId));
        }
        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri] DateTime? FromDate, [FromUri]DateTime? ToDate,[FromUri] int? SupplierId)
        {
            List<PurchaseQuoteRegister> reg = PurchaseQuoteRegister.GetDetailsForConfirm(LocationId).ToList();
            if (FromDate!=null&&ToDate!=null)
            {
                reg = reg.Where(x => x.EntryDate >= FromDate && x.EntryDate <= ToDate).ToList();
            }
            if(SupplierId!=null&&SupplierId!=0)
            {
                reg = reg.Where(x => x.SupplierId == SupplierId).ToList();
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, reg);
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id,[FromBody] int modifiedBy)
        {
            PurchaseQuoteRegister pq = new PurchaseQuoteRegister();
            pq.ID = Id;
            pq.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pq.Delete());
        }

        [HttpPost]
        public HttpResponseMessage Confirm([FromUri] int Id,[FromBody] int user_ID)
        {
            return Request.CreateResponse(HttpStatusCode.OK,new PurchaseQuoteRegister(Id, user_ID).Confirm());
        }

        //[HttpPost]
        //public async Task<HttpResponseMessage> SendMail([FromUri]int Id,[FromBody]string Url,int CreatedBy)
        //{
        //    PurchaseQuoteRegister pq = new PurchaseQuoteRegister();
        //    return Request.CreateResponse(HttpStatusCode.OK, await pq.SendPoAsync(Id, Url, CreatedBy)); 
        //}
        
        [HttpPost]
        public HttpResponseMessage ToggleConfirm([FromUri] int Id, [FromBody] int user_ID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new PurchaseQuoteRegister(Id, user_ID).ToggleConfirm());
        }

        [HttpPost]
        public HttpResponseMessage GetSupplierWiseDetails(dynamic Params)
        {
            int SupplierId = Convert.ToInt32(Params.SupplierId);
            int LocationId = Convert.ToInt32(Params.LocationId);
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseQuoteRegister.GetDetails(LocationId).Where(x => x.SupplierId == SupplierId));
        }

        [HttpPost]
        public HttpResponseMessage GetSupplierWiseDetailsConfirmed(dynamic Params)
        {
            int SupplierId = Convert.ToInt32(Params.SupplierId);
            int LocationId = Convert.ToInt32(Params.LocationId);
            List<PurchaseQuoteRegister> list = PurchaseQuoteRegister.GetDetails(LocationId).Where(x => x.SupplierId == SupplierId).Where(x => x.IsApproved).Where(x=>x.Status==0).ToList();
            list.ForEach(x=>x.Products.RemoveAll(y=>y.Status!=0));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int purchaseid, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => PurchaseQuoteRegister.SendMail(purchaseid, toAddress, userId, url)));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseQuoteRegister.GetDetails(LocationId, SupplierId, from, to));
        }

    }
}