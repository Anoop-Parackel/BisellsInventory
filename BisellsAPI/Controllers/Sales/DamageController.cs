using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Entities.WareHousing;
using System.Web.Http;
using Newtonsoft.Json;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class DamageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save( Damage d)
        {
            if (d.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, d.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, d.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Damage.GetDetails(LocationId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Damage.GetDetails(LocationId,from,to));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Damage.GetDetails(Id,LocationId));
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            Damage d = new Damage();
            d.ID = Id;
            d.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, d.Delete());
        }

        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
    }
}