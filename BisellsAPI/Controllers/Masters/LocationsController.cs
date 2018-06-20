using BisellsAPI.Filters;
using Entities;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class LocationsController : ApiController
    {
        // GET api/<controller>
          [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Location.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id,[FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Location.GetDetails(id, CompanyId));
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
        public HttpResponseMessage Delete([FromUri]int id,[FromBody] int modifiedBy)
        {
            Location location = new Location();
            location.ID = id;
            location.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, location.Delete());
        }
    }
}
