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
    public class AttendanceController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]int CompanyId,[FromUri]DateTime? From, [FromUri]DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Attendance.GetAttendanceDetails(CompanyId,From,To));
        }
        [HttpPost]
        public HttpResponseMessage Save(List<Entities.Payroll.Attendance> attendance)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.Attendance.Save(attendance));
        }
    }
}