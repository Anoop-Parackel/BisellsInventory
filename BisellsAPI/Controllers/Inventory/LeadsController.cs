using BisellsAPI.Filters;
using Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BisellsAPI.Controllers.Inventory
{
    public class LeadsController : ApiController
    {
        /// <summary>
        /// save leads
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public HttpResponseMessage SaveLeads(Lead l)
        {
            if (l.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, l.UpdateLeads());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, l.SaveLeads());
            }
        }
        /// <summary>
        /// Delete leads
        /// </summary>
        /// <param name="id">Id of that particular lead</param>
        /// <param name="modifiedBy">user id</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteLeads([FromUri]int id, [FromBody]int modifiedBy)
        {
            Lead lead = new Lead();
            lead.ID = id;
            lead.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, lead.DeleteLeads());
        }
        /// <summary>
        /// retrieving a single lead
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetLeads([FromUri]int id, [FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Lead.GetDetailsForLeads(CompanyId, id));
        }
        /// <summary>
        /// retrieving a list of leads
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetLeads([FromBody]int? CompanyId,[FromUri]int? Status,[FromUri]int? EmployeeId,[FromUri]DateTime? From,[FromUri]DateTime? To)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Lead.GetDetailsForLeads(CompanyId, Status, EmployeeId,From,To));
        }
        /// <summary>
        /// change primary status
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateStatus(Lead l)
        {
            return Request.CreateResponse(HttpStatusCode.OK, l.UpdateStatus());
        }
        [HttpPost]
        public HttpResponseMessage ConvertToCustomer([FromUri]int LeadId,[FromBody] Entities.Customer customer)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Lead().ConvertToCustomer(LeadId,customer));
        }

        /// <summary>
        /// for populating assign dropdown in leads
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetEmployee([FromUri]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Payroll.Employee.GetEmployee(CompanyId));
        }
    }
}