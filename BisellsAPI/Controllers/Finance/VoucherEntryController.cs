using Entities.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BisellsAPI.Filters;

namespace BisellsAPI.Controllers.Finance
{
    public class VoucherEntryController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherEntry.GetDetails());
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

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetHeads([FromUri]int CompanyId, [FromUri]int Credit, [FromBody]int voucher)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherEntry.GetAccountHeadsVoucher(voucher, Credit, CompanyId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetHeads([FromUri]int CompanyId, [FromBody]int voucher)
        {
            return Request.CreateResponse(HttpStatusCode.OK, AccountHeadMaster.GetAccountHeadsVoucher(voucher,CompanyId));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GeCostCenter([FromBody]int CompanyId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, CostCenter.LoadCostCenter());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GeVoucherNumber([FromBody]int CompanyId,[FromUri] int Voucher)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherEntry.GetVoucherNumber(Voucher));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetVoucherData([FromBody]FinancialTransactions fin)
        {
            return Request.CreateResponse(HttpStatusCode.OK, fin.GetTransactions());
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetVoucherDataforEdit([FromBody]int GroupID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherEntry.GetDataset(GroupID));
        }

        [HttpPost]
        [DeflateCompression]
        public HttpResponseMessage GetVoucherDataforPrint([FromUri]int GroupID,[FromBody]int CompanyID)
        {
            return Request.CreateResponse(HttpStatusCode.OK, VoucherEntry.GetDataForPrint(GroupID,CompanyID));
        }

        [HttpPost]
        public HttpResponseMessage Save(VoucherEntry voucher)
        {
            if (voucher.groupID==0)
            {
              return  Request.CreateResponse(HttpStatusCode.OK, voucher.Save(voucher.VoucherType,voucher.AccountHead,voucher.AccountChild,voucher.Amount,voucher.CostCenter,voucher.Jobs,voucher.EntryDesc));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, voucher.Update(voucher.VoucherType, voucher.AccountHead, voucher.AccountChild, voucher.Amount, voucher.CostCenter));
            }
        }


        [HttpDelete]
        public HttpResponseMessage Delete([FromUri]int id, [FromBody]int modifiedBy)
        {
            VoucherEntry Report = new VoucherEntry();
            Report.ID = id;
            Report.ModifiedBy = modifiedBy;
            return Request.CreateResponse(HttpStatusCode.OK, Report.Delete());

        }
    }
}