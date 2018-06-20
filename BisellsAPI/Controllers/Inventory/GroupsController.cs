using BisellsAPI.Filters;
using Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class GroupsController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Group.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Group.GetDetails(id, CompanyId));

        }

        //public void Post([FromBody]string value)
        //{
        //}
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody] int modifiedBy)
        {
            Group group = new Group();
            group.ID = id;
            group.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, group.Delete());
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]Group Group)
        {
            if (Group.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Group.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Group.Update());
            }
        }
    }
}