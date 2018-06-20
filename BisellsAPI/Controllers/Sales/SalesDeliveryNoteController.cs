﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Entities.Register;
using System.Threading.Tasks;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers
{
    public class SalesDeliveryNoteController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Save(Entities.Register.SalesDeliveryNote Sq)
        {
            if (Sq.ID > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, Sq.Update());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Sq.Save());
            }
        }
        [Obsolete]
        //[HttpPost]
        //public HttpResponseMessage Get([FromBody]int LocationId)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, SalesQuoteRegister.GetDetails(LocationId));
        //}
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri]int? CustomerId, [FromUri]DateTime? from, [FromUri]DateTime? to)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesDeliveryNote.GetDetails(LocationId, CustomerId, from, to));
        }

        [HttpPost]
        public HttpResponseMessage Get([FromUri] int Id, [FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SalesDeliveryNote.GetDetails(Id, LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage Get([FromBody]int LocationId, [FromUri] DateTime? FromDate, [FromUri]DateTime? ToDate, [FromUri] int? CustomerId)
        {
            List<SalesDeliveryNote> reg = SalesDeliveryNote.GetDetailsForConfirm(LocationId).ToList();
            if (FromDate != null && ToDate != null)
            {
                reg = reg.Where(x => x.EntryDate >= FromDate && x.EntryDate <= ToDate).ToList();
            }
            if (CustomerId != null && CustomerId != 0)
            {
                reg = reg.Where(x => x.CustomerId == CustomerId).ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, reg);
        }
        [HttpPost]
        public HttpResponseMessage GetCompanyDetails([FromBody]int LocationId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, Entities.Master.Company.GetDetailsByLocation(LocationId));
        }
        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetSalesRequest([FromBody]dynamic Params)
        {
            int LocationId = Convert.ToInt32(Params.LocationId);
            int CustomerId = Convert.ToInt32(Params.CustomerId);
            List<SalesDeliveryNote> list = SalesDeliveryNote.GetDetails(LocationId).Where(x => x.CustomerId == CustomerId).Where(x => x.Status == 0).ToList();
            list.ForEach(x => x.Products.RemoveAll(y => y.Status != 0));
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        public HttpResponseMessage Confirm([FromUri] int Id, [FromBody] int user_ID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new SalesDeliveryNote(Id, user_ID).Confirm());
        }
        [HttpPost]
        public HttpResponseMessage ToggleConfirm([FromUri] int Id, [FromBody] int user_ID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new SalesDeliveryNote(Id, user_ID).ToggleConfirm());
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int Id, [FromBody] int modifiedBy)
        {
            SalesDeliveryNote pr = new SalesDeliveryNote();
            pr.ID = Id;
            pr.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, pr.Delete());
        }
        [HttpPost]
        public async Task<HttpResponseMessage> SendMail([FromBody] string url, [FromUri] int salesId, [FromUri]string toAddress, [FromUri]int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await Task.Run(() => SalesDeliveryNote.SendMail(salesId, toAddress, userId, url)));
        }
    }
}
