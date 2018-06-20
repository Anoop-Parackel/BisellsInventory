using Entities;
using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class VoucherTypeController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        [HttpPost]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherType.GetVoucherType());
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherType.GetDetails(id, CompanyId));
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            VoucherType Voucher = new VoucherType();
            Voucher.ID = id;
            Voucher.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Voucher.Delete());

        }
        [HttpPost]
        public HttpResponseMessage Save([FromBody]VoucherType voucher)
        {
            if (voucher.ID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, voucher.Save());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, voucher.Update());
            }
        }
    }
}