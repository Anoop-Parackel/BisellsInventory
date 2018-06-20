using BisellsAPI.Filters;
using Entities.Payroll;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class DepartmentsController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Department.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id,[FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Department.GetDetails(id, CompanyId));
        }

        //public void Post([FromBody]string value)
        //{
        //}
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id,[FromBody]int modifiedBy)
        {
            Department department = new Department();
            department.ID = id;
            department.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, department.Delete());
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody]Department dept)
        {
            if (dept.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, dept.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, dept.Update());
            }
        }
    }
}