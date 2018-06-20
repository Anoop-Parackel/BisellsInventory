using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Security;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.Security
{
    public class PermissionsController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetModules()
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) Modules.GetModulesObject());
        }
        [HttpPost]
        public HttpResponseMessage GetUserPermission([FromUri]int UserId)
        {
            return Request.CreateResponse(HttpStatusCode.OK,Permissions.GetUserPermission(UserId));
        }
        [HttpPost]
        public HttpResponseMessage GetGroupPermission([FromUri]int GroupId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Permissions.GetGroupPermission(GroupId));
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]object permission,[FromUri] bool isUser)
        {
            if(isUser)
                return Request.CreateResponse(HttpStatusCode.OK, Permissions.SetUserPermission(permission));
            else
                return Request.CreateResponse(HttpStatusCode.OK, Permissions.SetGroupPermission(permission));
        }
    }
}
