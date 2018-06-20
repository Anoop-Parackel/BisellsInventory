using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class RegisterOutController : ApiController
    {
         [HttpPost]
        public HttpResponseMessage Save(RegisterOut ro)
        {
            if (ro.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ro.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ro.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterOut.GetDetails(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterOut.GetDetails(LocationId, from, to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterOut.GetDetails(Id,LocationId));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            RegisterOut ro = new RegisterOut();
            ro.ID = Id;
            ro.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, ro.Delete());
        }
    }
}