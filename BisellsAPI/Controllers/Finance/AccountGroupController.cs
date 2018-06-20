using BisellsAPI.Filters;
using Entities.Finance;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class AccountGroupController : ApiController
    {
         [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountGroup.GetDetails());
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountGroup.GetDetails(id));
        }

        [HttpPost]
        public HttpResponseMessage GetGroups([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountGroup.GetAccountGroups(CompanyId));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTree([FromBody] int Company)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object)AccountGroup.GetTree(Company));
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody] AccountGroup group)
        {
            if (group.Id>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, group.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, group.Save());
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            AccountGroup Account = new AccountGroup();
            Account.Id = id;
            Account.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Account.Delete());

        }
    }
}