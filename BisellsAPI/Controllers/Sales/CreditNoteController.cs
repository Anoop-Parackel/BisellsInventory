using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.Sales
{
    public class CreditNoteController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(CreditNote Sr)
        {
            if (Sr.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Sr.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Sr.Save());
            }
        }
        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesReturnRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? CustomerId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, CreditNote.GetDetails(LocationId, CustomerId, from, to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id, [FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, CreditNote.GetDetails(Id, LocationId));
        }




        /// <summary>
        /// return details of customer wise items  
        /// </summary>
        /// <param name="LocationId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[DeflateCompression]
        //public HttpResponseMessage GetSalesEntry([FromBody]int LocationId, [FromUri] int customerId, [FromUri] int ItemId, [FromUri] int InstanceId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetailsItemWise(LocationId, customerId, ItemId, InstanceId).Where(x => x.Status == 0));
        //}
        /// <summary>
        /// return sales entry details  
        /// </summary>
        /// <param name="LocationId"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        //[HttpPost]
        //public HttpResponseMessage GetSalesEntryDetails([FromBody]int LocationId, [FromUri] int ID)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetailsFromEntry(LocationId, ID));
        //}
        //[HttpPost]
        //[DeflateCompression]
        //public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int salesId, [FromUri]string toAddress, [FromUri]int userId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => SalesReturnRegister.SendMail(salesId, toAddress, userId, url)));
        //}
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            SalesReturnRegister Sr = new SalesReturnRegister();
            Sr.ID = Id;
            Sr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Sr.Delete());
        }

        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int salesId, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => CreditNote.SendMail(salesId, toAddress, userId, url)));
        }
    }
}
