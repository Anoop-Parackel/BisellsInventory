using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using Newtonsoft.Json;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class QuoteBuilderController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]dynamic Params)
        {
            int LocationId = (int)Params.LocationId;
            DateTime StartDate = Params.StartDate!=null && !string.IsNullOrWhiteSpace((string)Params.StartDate)?(DateTime)Params.StartDate:DateTime.MinValue;
            DateTime EndDate = Params.EndDate!=null && !string.IsNullOrWhiteSpace((string)Params.EndDate) ? (DateTime)Params.EndDate:DateTime.MinValue;
            if(StartDate.Year>1900 && EndDate.Year > 1900)
            {
                return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(LocationId).Where(x => x.RequestStatus == 0 && x.EntryDate >= StartDate && x.EntryDate <= EndDate));
                
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, PurchaseRequestRegister.GetDetails(LocationId).Where(x => x.RequestStatus == 0));
            }
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId,[FromUri]int RequestId)
        {

            PurchaseRequestRegister pr = PurchaseRequestRegister.GetDetails(LocationId).First(x => x.ID == RequestId);
            IEnumerable < Item> prod =  pr.Products.Where(x => x.Status == 0).ToList();
            pr.Products.Clear();
            pr.Products =(List<Item>) prod;
            return Request.CreateResponse(HttpStatusCode.OK, pr);
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody] string Qoute)
        {
            dynamic Qoutes = JsonConvert.DeserializeObject(Qoute);
            return Request.CreateResponse(HttpStatusCode.OK,(object) PurchaseQuoteRegister.BulkSave(Qoutes));
        }

        [HttpPost]
        public HttpResponseMessage SaveFromIndent([FromBody] string Qoute)
        {
            dynamic Qoutes = JsonConvert.DeserializeObject(Qoute);
            return Request.CreateResponse(HttpStatusCode.OK, (object)PurchaseQuoteRegister.BulkSaveFromIndent(Qoutes));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetFromIndent([FromBody]dynamic Params)
        {
            int LocationId = (int)Params.LocationId;
            DateTime StartDate = Params.StartDate != null && !string.IsNullOrWhiteSpace((string)Params.StartDate) ? (DateTime)Params.StartDate : DateTime.MinValue;
            DateTime EndDate = Params.EndDate != null && !string.IsNullOrWhiteSpace((string)Params.EndDate) ? (DateTime)Params.EndDate : DateTime.MinValue;
            if (StartDate.Year > 1900 && EndDate.Year > 1900)
            {
                return Request.CreateResponse(HttpStatusCode.OK, PurchaseIndentRegister.GetDetailsIndent(LocationId).Where(x => x.RequestStatus == 0 && x.EntryDate >= StartDate && x.EntryDate <= EndDate));

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, PurchaseIndentRegister.GetDetailsIndent(LocationId).Where(x => x.RequestStatus == 0));
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetIndent([FromBody]int LocationId, [FromUri]int RequestId)
        {
            PurchaseIndentRegister pr = PurchaseIndentRegister.GetDetailsIndent(LocationId).First(x => x.ID == RequestId);
            IEnumerable<Item> prod = pr.Products.Where(x => x.Status == 0).ToList();
            pr.Products.Clear();
            pr.Products = (List<Item>)prod;
            return Request.CreateResponse(HttpStatusCode.OK, pr);
        }

    }
}