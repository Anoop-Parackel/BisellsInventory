using BisellsAPI.Filters;
using Entities;
using Entities.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class EmployeeController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Save(Employee e)
        {
            if (e.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Employee.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id,[FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Employee.GetDetails(id, CompanyId));
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
            Employee employee = new Employee();
            employee.ID = id;
            employee.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, employee.Delete());
         }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetDetailsForSalary([FromBody]int CompanyId,[FromUri] int? EmployeeId)
        {
            dynamic result = Employee.GetDetailsForManageSalary(CompanyId);
            List<Employee> emp =(List<Employee>) (result.Employees);
            if (  EmployeeId != 0)
            {
                emp = emp.Where(x => x.ID == EmployeeId).ToList();
            }
            result.Employees = emp;
            return Request.CreateResponse(HttpStatusCode.OK, (object)result);
        }

        [HttpPost]
        public HttpResponseMessage GetSalary([FromUri] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object)Employee.GetDetailsForManageSalary( CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage SaveSalary(List<Employee> emps)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Employee.ManageSalary(emps));
        }
    }
}