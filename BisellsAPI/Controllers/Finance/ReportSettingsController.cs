using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class ReportSettingsController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Get([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ReportSettings.GetData(CompanyId));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ReportSettings.GetDetails(id));
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
            ReportSettings Report = new ReportSettings();
            Report.ID = id;
            Report.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Report.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody] ReportSettings Report)
        {
            if (Report.ID>0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Report.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Report.Save());
            }

        }
    }
}