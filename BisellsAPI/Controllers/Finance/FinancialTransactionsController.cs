using BisellsAPI.Filters;
using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class FinancialTransactionsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Getdetails([FromUri]int customer_id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetDetails(customer_id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetdetailsForCustomerPayment([FromUri]int customer_id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetDetailsForCustomerPayment(customer_id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetdetailsSupplierwise([FromUri]int SupplierId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetDetailsSupplierwise(SupplierId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetAccountGroup([FromUri]string Nature,[FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetAccountGroupsByNature(Nature,CompanyID));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetAccountHeadsByGroup([FromUri]int Group, [FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetAccountHeadsByGroup(Group, CompanyID));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetAccountTransactions([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.GridViewData());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetAccountHeadsChild([FromUri]int HeadID, [FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Finance.FinancialTransactions.GetAccountHeadsChilds(HeadID, CompanyID));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetReport([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.GetGroupLedgerData());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetLedgerReport([FromUri]string ChildHead, [FromUri]string MainHead, [FromUri]string FromDate, [FromUri]string ToDate, [FromUri]string CostCenter)
        {
            Entities.Finance.FinancialTransactions fin = new FinancialTransactions();
            return Request.CreateResponse(HttpStatusCode.OK, fin.BindGrid(null,ChildHead,MainHead,FromDate,ToDate,CostCenter));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetChequeDetails([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.GetChequeTransactions());
        }

        [HttpPost]
        public HttpResponseMessage ClearCheque([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.ClearCheque());
        }

        [HttpPost]
        public HttpResponseMessage BounceCheque([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.BounceCheque());
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            FinancialTransactions fin = new FinancialTransactions();
            fin.ID = id;
            fin.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, fin.Delete());

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}