using BisellsAPI.Filters;
using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Finance
{
    public class VoucherSettingsController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        public HttpResponseMessage Get([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherType.GetVoucherTypes(id));
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetTable([FromBody] int CompanyID, [FromUri] int VoucherType,[FromUri]int isGroup)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherSettings.GetVoucherSetingsTable(CompanyID, VoucherType, isGroup));
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        [HttpPost]
        public HttpResponseMessage GetVoucherTypes([FromBody] int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherType.GetVoucherType());
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }


        [HttpPost]
        public HttpResponseMessage Save([FromBody] VoucherSettings Settings)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Settings.Save());
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}