using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Application;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class UserGroupController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserGroup.GetDetails());
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserGroup.GetDetails(id));
        }
        // GetGroupwiseUsers api for GET User details
        [HttpPost]
        public HttpResponseMessage GetUsers([FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, UserGroup.GetUsers(id));
        }

        [HttpPost]
        public HttpResponseMessage SaveRoles(UserGroup ug)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ug.SaveUserGroup());
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            UserGroup group = new UserGroup();
            group.ID = id;
            return Request.CreateResponse(HttpStatusCode.OK, group.Delete());
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]UserGroup Group)
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