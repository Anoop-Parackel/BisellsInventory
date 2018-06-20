using BisellsAPI.Filters;
using Entities.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class SalaryComponentsController : ApiController
    {
        // GET api/<controller>
     [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalaryComponent.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id,[FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalaryComponent.GetDetails(id, CompanyId));
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
        public HttpResponseMessage Delete([FromUri]int id,[FromBody]int modifiedBy)
        {
            SalaryComponent salary = new SalaryComponent();
            salary.ID = id;
            salary.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, salary.Delete());
           
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]SalaryComponent sal)
        {
            if (sal.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, sal.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, sal.Update());
            }
        }
    }
}