using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Master;
using Entities.Register;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class SalesEntryController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(SalesEntryRegister se)
        {
            if (se.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, se.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, se.Save());
            }
        }
        [HttpPost]
        public HttpResponseMessage updateDespatch(SalesEntryRegister sr)
        {
            return Request.CreateResponse(HttpStatusCode.OK, sr.UpdateDespatch());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSalesQuotes([FromBody]dynamic Params)
        {
            int LocationId = Convert.ToInt32(Params.LocationId);
            int CustomerId = Convert.ToInt32(Params.CustomerId);
            List<SalesQuoteRegister> list = SalesQuoteRegister.GetDetails(LocationId).Where(x => x.CustomerId == CustomerId).Where(x => x.ApprovedStatus == 1).ToList().Where(x => x.Status == 0).ToList();
            list.ForEach(x => x.Products.RemoveAll(y => y.Status != 0));
            return Request.CreateResponse(HttpStatusCode.OK, list);

        }

        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId,[FromUri] DateTime? FromDate,[FromUri]DateTime? ToDate)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetails(LocationId).Where(x => x.SalesDate >= FromDate && x.SalesDate <= ToDate));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetails(Id, LocationId));
        }
        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? CustomerId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesEntryRegister.GetDetails(LocationId, CustomerId, from, to));
        }
        [HttpPost]
        public HttpResponseMessage GetDespatch([FromBody]int CompanyId)
        {

            return Request.CreateResponse(HttpStatusCode.OK,(object) Despatch.GetDespatchList(CompanyId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            SalesEntryRegister pr = new SalesEntryRegister();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url,[FromUri] int salesId,[FromUri]string toAddress,[FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(()=> SalesEntryRegister.SendMail(salesId, toAddress, userId, url)));
        }
        [HttpGet]
        public HttpResponseMessage GetReceipts([FromUri]int salesId)
        {

            return Request.CreateResponse(HttpStatusCode.OK, (object)SalesEntryRegister.GetReceipts(salesId));
        }
    }
}