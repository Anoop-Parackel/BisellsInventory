using BisellsAPI.Filters;
using Entities.Application;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.User.GetDetails());
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.User.GetDetails(id));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id)
        {
            User user = new User();
            user.ID = id;
            return Request.CreateResponse(HttpStatusCode.OK, user.Delete());
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]User User)
        {
            if (User.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, User.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, User.Update());
            }
        }

        [HttpPost]
        public HttpResponseMessage Updatepassword([FromBody]User User)
        {
            return Request.CreateResponse(HttpStatusCode.OK, User.UpdatePassword());
        }

        [HttpPost]
        public HttpResponseMessage GetUserInGroup([FromUri] int GroupId, [FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.User.GetUsersInGroup(GroupId, CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage GetUser([FromUri] int UserId, [FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Application.User.GetUsers(UserId, CompanyId));
        }
    }
}