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
    public class ExpenseEntryController : ApiController
    {
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ExpenseEntry.GetDetails());
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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
            ExpenseEntry Report = new ExpenseEntry();
            Report.ID = id;
            Report.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Report.Delete());

        }
    }
}