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
    public class GRNController : ApiController
    {

        //public void Post([FromBody]string value)
        //{
        //}
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            GRNEntryRegister pr = new GRNEntryRegister();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }

        [HttpPost]
        public HttpResponseMessage Save(GRNEntryRegister GRN)
        {
            if (GRN.GRNID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, GRN.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, GRN.Save());
            }
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri] DateTime? FromDate, [FromUri]DateTime? ToDate, [FromUri] int? SupplierId)
        {
            List<GRNEntryRegister> reg = GRNEntryRegister.GetDetailsForConfirm(LocationId).ToList();
            if (FromDate != null && ToDate != null)
            {
                reg = reg.Where(x => x.EntryDate >= FromDate && x.EntryDate <= ToDate).ToList();
            }
            if (SupplierId != null && SupplierId != 0)
            {
                reg = reg.Where(x => x.SupplierId == SupplierId).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, reg);
        }

        [HttpPost]
        public HttpResponseMessage GetSupplierWiseDetailsConfirmed(dynamic Params)
        {
            int SupplierId = Convert.ToInt32(Params.SupplierId);
            int LocationId = Convert.ToInt32(Params.LocationId);
            List<GRNEntryRegister> list = GRNEntryRegister.GetDetails(LocationId).Where(x => x.SupplierId == SupplierId).Where(x => x.Status == false).ToList();
            list.ForEach(x => x.Products.RemoveAll(y => y.Status != 0));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id, [FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, GRNEntryRegister.GetDetails(Id, LocationId));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int grnId, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => GRNEntryRegister.SendMail(grnId, toAddress, userId, url)));
        }

    }
}