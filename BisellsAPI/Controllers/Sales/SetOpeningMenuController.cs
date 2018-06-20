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
    public class SetOpeningMenuController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        public HttpResponseMessage Save(OpeningStock s)
        {
            if (s.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, s.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, s.Save());
            }
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, OpeningStock.GetDetails(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? SupplierId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, OpeningStock.GetDetails(LocationId,SupplierId,from,to));
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, OpeningStock.GetDetails(Id,LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            OpeningStock s = new OpeningStock();
            s.ID = Id;
            s.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, s.Delete());
        }
    }
}