using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using Entities;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class PurchaseIndentController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(PurchaseIndentRegister pi)
        {
            if (pi.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, pi.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, pi.Save());
            }
        }
        [HttpGet]
        public HttpResponseMessage SearchItem(string keyword, int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Item.GetDetails(keyword, LocationId));
        }

        [Obsolete]

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseIndentRegister.GetDetails(Id));
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            PurchaseIndentRegister pi = new PurchaseIndentRegister();
            pi.ID = Id;
            pi.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pi.Delete());
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, PurchaseIndentRegister.GetDetails(from, to));
        }

        [HttpPost]
        public HttpResponseMessage GetSupplierMail([FromBody]int companyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Supplier.GetSupplierMail(companyId));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SendSupplierMail([FromBody]PurchaseIndentRegister indent, [FromUri] int indentId, [FromUri] string url)
        {
            
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(()=> indent.SendMail(indentId,url)));
        }

    }

}