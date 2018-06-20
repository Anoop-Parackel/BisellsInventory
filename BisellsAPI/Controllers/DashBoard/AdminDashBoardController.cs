using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.DashBoard;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.DashBoard
{
    public class AdminDashBoardController : ApiController
    {
        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage SalesVsPurchase(DateTime? From,DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) Admin.SalesVsPurchase(From,To));
        }

        [HttpGet]
        public HttpResponseMessage CashVsCreditVsExpense([FromUri] DateTime? From, [FromUri]DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) Admin.CashVsCreditVsExpenses(From, To));
        }
        [HttpGet]
        public HttpResponseMessage PurchaseSupplierwiseData(DateTime? From, DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) Admin.PurchaseSupplierwiseData(From, To));
        }
        [HttpGet]
        public HttpResponseMessage InitialiseAllCharts(DateTime? From, DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK,(object) Admin.InitialiseAllCharts(null, null));
        }
        [HttpGet]
        public HttpResponseMessage ActivityLog()
        {
            return Request.CreateResponse(HttpStatusCode.OK, (object) Admin.PopulateActivityLog());
        }
    }
}