using BisellsAPI.Filters;
using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BisellsAPI.Controllers
{
    public class SalesRequestController : ApiController
    {
       [HttpPost]
        public HttpResponseMessage Save([FromBody]SalesRequestRegister sr)
        {
            if (sr.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, sr.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, sr.Save());
            }
        }

        [HttpGet]
        [DeflateCompression]
        public HttpResponseMessage SearchItem(string keyword,int LocationId)
            {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetails(keyword, LocationId));
            }
        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesRequestRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id,[FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesRequestRegister.GetDetails(Id,LocationId));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id,[FromBody] int userId)
        {
            SalesRequestRegister sr = new SalesRequestRegister();
            sr.ID = Id;
            sr.ModifiedBy = userId;
            return Request.CreateResponse(HttpStatusCode.OK, sr.Delete());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? CustomerId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesRequestRegister.GetDetails(LocationId, CustomerId, from, to));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int requestId, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => SalesRequestRegister.SendMail(requestId, toAddress, userId, url)));
        }
    }
}