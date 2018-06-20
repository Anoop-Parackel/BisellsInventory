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
    public class AccountOpeningBalanceController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get1()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountOpeningBalance.GetDetails());
        }
        [HttpPost]
        public HttpResponseMessage Get2([FromUri] int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountOpeningBalance.GetDetails(id));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountOpeningBalance.GetDetails(id, CompanyId));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            AccountOpeningBalance Account = new AccountOpeningBalance();
            Account.ID = id;
            Account.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Account.Delete());
        }
    }
}