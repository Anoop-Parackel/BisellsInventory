using BisellsAPI.Filters;
using Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class VehiclesController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody] int CompanyId)
        {
            return Request.CreateResponse(Vehicle.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Vehicle.GetDetails(id, CompanyId));
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
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.ID = id;
            vehicle.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, vehicle.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Vehicle Vehicle)
        {
            if (Vehicle.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Vehicle.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Vehicle.Update());
            }
        }
    }
}