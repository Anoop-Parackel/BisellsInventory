using System;
using System.Linq;
using System.Net;
using Entities.Payroll;
using System.Net.Http;
using System.Web.Http;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class HourlyTemplateController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, HourlyTemplate.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, HourlyTemplate.GetDetails(id, CompanyId));
        }

        //public void Post([FromBody]string value)
        //{
        //}
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            HourlyTemplate ht = new HourlyTemplate();
            ht.ID = id;
            ht.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, ht.Delete());
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]HourlyTemplate Hourly)
        {
            if (Hourly.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Hourly.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Hourly.Update());
            }
        }
    }
}