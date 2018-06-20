using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Payroll;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class OfficeShiftController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, OfficeShift.GetDetails(CompanyId));
        }

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, OfficeShift.GetDetails(id, CompanyId));
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
            OfficeShift off = new OfficeShift();
            off.ID = id;
            off.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, off.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]OfficeShift Shift)
        {
            if (Shift.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Shift.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Shift.Update());
            }
        }
    }
}