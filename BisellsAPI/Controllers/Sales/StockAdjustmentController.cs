using BisellsAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class StockAdjustmentController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetStock([FromUri]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Register.StockAdjustment.StockAdjustments(LocationId));
        }

        [HttpPost]
        public HttpResponseMessage Save(List<Entities.Register.StockAdjustment> stock)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Register.StockAdjustment.Save(stock));
        }
    }
}