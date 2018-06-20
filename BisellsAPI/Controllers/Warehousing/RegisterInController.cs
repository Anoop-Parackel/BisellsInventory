using BisellsAPI.Filters;
using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class RegisterInController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        public HttpResponseMessage Save(RegisterIn ri)
        {
            if (ri.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ri.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, ri.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterIn.GetDetails(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterIn.GetDetails(LocationId,from,to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, RegisterIn.GetDetails(Id,LocationId));
        }

        [HttpPost]
        public HttpResponseMessage GetUserwise([FromBody]int LocationId,[FromUri] string id,[FromUri] int type)
        {
            List<RegisterOut> RegisterOuts= RegisterIn.GetDetailsUserWise(LocationId,id,type).Where(x=>x.Status==0).ToList();
            RegisterOuts.ForEach(x => x.Products.RemoveAll(y => y.Status != 0));
            return Request.CreateResponse(HttpStatusCode.OK, RegisterOuts);
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            RegisterIn ri = new RegisterIn();
            ri.ID = Id;
            ri.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, ri.Delete());
        }
     }
}