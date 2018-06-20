using Entities;
using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class SchemeController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Scheme.GetDetails(LocationId));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK,Scheme.GetDetails(id,LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Scheme sc = new Scheme();
            sc.SchemeId = id;
            sc.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, sc.Delete());

        }

    }
}