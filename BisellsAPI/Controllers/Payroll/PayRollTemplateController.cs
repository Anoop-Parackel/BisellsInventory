using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Payroll;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class PayRollTemplateController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PayRollTemplate.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PayRollTemplate.GetDetails(id, CompanyId));
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
            PayRollTemplate p = new PayRollTemplate();
            p.ID = id;
            p.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, p.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]PayRollTemplate Payroll)
        {
            if (Payroll.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Payroll.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Payroll.Update());
            }
        }
    }
}