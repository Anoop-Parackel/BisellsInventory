using BisellsAPI.Filters;
using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class CostCenterController : ApiController
    {
       [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, CostCenter.GetDetails());
        }
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, CostCenter.GetDetails(id));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            CostCenter Cost = new CostCenter();
            Cost.ID = id;
            Cost.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Cost.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody] CostCenter c)
        {
            if (c.ID>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, c.Save());
            }
        }
    }
}