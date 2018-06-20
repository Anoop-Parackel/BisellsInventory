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
    public class AccountHeadMasterController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.GetDetails());
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.GetDetails(id));
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public HttpResponseMessage Get1([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.getNature(id));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetHeads([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.GetAccountHeads(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage GetGroups([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.GetAccountGroup(CompanyId));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        [HttpPost]
        public HttpResponseMessage GetTree([FromBody] int Company)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)AccountHeadMaster.GetHeadTree(Company));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        public HttpResponseMessage Save(AccountHeadMaster head)
        {
            if (head.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, head.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, head.Update());
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            AccountHeadMaster Account = new AccountHeadMaster();
            Account.ID = id;
            Account.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Account.Delete());

        }
    }
}