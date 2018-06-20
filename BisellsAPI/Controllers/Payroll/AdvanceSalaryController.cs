using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Payroll;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class AdvanceSalaryController : ApiController
    {
        
        [HttpPost]
        public HttpResponseMessage Save([FromBody]AdvanceSalary AdvSal)
        {
            if (AdvSal.Id == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, AdvSal.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, AdvSal.Update());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {

            return Request.CreateResponse(HttpStatusCode.OK, AdvanceSalary.GetDetails(CompanyId));
        }
        
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AdvanceSalary.GetDetails(id, CompanyId));
        }
       
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            AdvanceSalary AdvSal = new AdvanceSalary();
            AdvSal.Id = id;
            AdvSal.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, AdvSal.Delete());

        }
    }
}