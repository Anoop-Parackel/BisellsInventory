using BisellsAPI.Filters;
using Entities.Payroll;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class DesignationsController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Designation.GetDetails(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id,[FromBody] int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Designation.GetDetails(id, CompanyId));
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
            Designation des = new Designation();
            des.ID = id;
            des.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, des.Delete());
            
        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]Designation desig)
        {
            if (desig.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, desig.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, desig.Update());
            }
        }
    }
}