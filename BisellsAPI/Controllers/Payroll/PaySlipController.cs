using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class PaySlipController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage GetDetailsForPaySlip([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.PayRollTemplate.GetDetailsForPaySlip(CompanyId));
        }
        [HttpPost]
        public HttpResponseMessage GetDetailsForPayment([FromUri]int CompanyId,[FromUri]DateTime From,[FromUri]DateTime To)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.PayRollTemplate.GetDetailsForPayment(CompanyId,From,To));
        }
        [HttpPost]
        public HttpResponseMessage GetDetailsForPaySlip([FromUri]int CompanyId, [FromUri]DateTime From, [FromUri]DateTime To)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.PayRollTemplate.GetDetailsFromPayslip(CompanyId, From, To));
        }
        [HttpPost]
        public HttpResponseMessage Save(List<Entities.Payroll.PayRollTemplate> pay)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.PayRollTemplate.SavePaySlip(pay));
        }

    }
}