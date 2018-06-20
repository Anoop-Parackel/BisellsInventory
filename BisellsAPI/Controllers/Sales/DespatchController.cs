using BisellsAPI.Filters;
using Entities.Master;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class DespatchController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Despatch.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Despatch.GetDetails(id, CompanyId));
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Despatch despatch = new Despatch();
            despatch.ID = id;
            despatch.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, despatch.Delete());
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]Despatch Despatch)
        {
            if (Despatch.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Despatch.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Despatch.Update());
            }
        }
    }
}