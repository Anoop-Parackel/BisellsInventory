using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Reporting;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class AdHocController : ApiController
    {

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage FilterReport([FromBody]List<ReportFilter> Filters,int Id,int? KeyValue)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new ReportingTool(Id, Filters, KeyValue).ApplyFilters());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetFields([FromBody] int ViewObjectId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ReportingTool.GetFields(ViewObjectId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetDetails()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ReportingTool.GetDetails());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetDetails([FromUri]int id)
        {
            ReportingTool rt = new ReportingTool();
            return Request.CreateResponse(HttpStatusCode.OK, rt.GetDetails(id));
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody] ReportingTool report,[FromUri] int id)
        {
            if (id==0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, report.Save());
            }
            else
            {

                return Request.CreateResponse(HttpStatusCode.OK, report.Update());

            }
        }

        [HttpDelete ]
        public HttpResponseMessage Delete([FromUri]int id)
        {
            ReportingTool rt = new ReportingTool();
            return Request.CreateResponse(HttpStatusCode.OK, rt.Delete(id));
        }
    }
}