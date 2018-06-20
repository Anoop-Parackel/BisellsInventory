using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Application;


namespace Entities.Finance
{
    public class PurchasePayments
    {
        #region Properties
        public int ID { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static dynamic GetPurchaseData(int SuplierId)
        {
            DBManager db = new DBManager();
            try
            {
                DataSet ds = new DataSet();
                #region query
                string sql = @"SELECT * FROM [VW_SUPPLIER_PAYMENTS] WHERE Supplier_Id=" + SuplierId + ";select * from  [dbo].[VW_UNLINKED_SUPPLIER_VOUCHERS] where Supplier_Id=" + SuplierId;
                #endregion
                db.Open();
                ds = db.ExecuteDataSet(CommandType.Text, sql);

                if (ds != null)
                {

                    object Purchase = new List<object>();
                    object Vouchers = new List<object>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        ((List<object>)Purchase).Add(new { PurchaseId = Convert.ToInt32(item["pe_id"]), PurchaseDate = Convert.ToDateTime(item["Created_Date"]).ToString("dd/MMM/yyyy"), InvoicNo = Convert.ToString(item["Invoice_No"]), NetAmount = Convert.ToDecimal(item["netamount"]), BalanceAmount = Convert.ToDecimal(item["balamount"]) });
                    }
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        ((List<object>)Vouchers).Add(new { Type = Convert.ToString(item["Fvt_TypeName"]), Id = Convert.ToInt32(item["Fve_Number"]), Date = Convert.ToDateTime(item["Fve_Date"]).ToString("dd/MMM/yyyy"), Amount = Convert.ToDecimal(item["Fve_Amount"]) });
                    }

                    return new { SupplierId = Convert.ToInt32(ds.Tables[0].Rows[0]["Supplier_Id"]), AvailableAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["avaamount"]), Vouchers, Purchase, PendingAmount = ds.Tables[0].AsEnumerable().Sum(x => x.Field<decimal>("BalAmount")) };
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "purchasepayment | GetPurchaseData(int SuplierId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        public static OutputMessage SettleBills(dynamic Settlement)
        {
            DBManager db = new DBManager();
            try
            {
                VoucherEntry voucher = new VoucherEntry();
                string Accounts = "";
                string AmountString = "";
                string Child = "";
                string VoucherType = "0|1";
                voucher.VoucherTypeID = 3;
                voucher.VoucherNo = 0;
                voucher.Description = "C P";
                voucher.AmountNew = Convert.ToDecimal(Settlement.PayingAmount);
                voucher.ToTransID = 8;
                voucher.ToTransChildID = Convert.ToInt32(Settlement.PartyId);
                string payby = Convert.ToString(Settlement.PayBy);
                if (payby == "Cheque"||payby=="Bank")
                {
                    voucher.IsCheque = 1;
                    voucher.ChequeDate = ((string)(Settlement.ChequeDate)).IsValidDate() ? Convert.ToDateTime(Settlement.ChequeDate) : null;
                    voucher.ChequeNo = Settlement.ChequeNumber == "" ? null : Settlement.ChequeNumber;
                    voucher.Frm_TransID = Settlement.PayBank;
                    voucher.FrmTransChildID = 0;
                }
                else if (payby == "Cash")
                {
                    voucher.IsCheque = 0;
                    voucher.ChequeDate = null;
                    voucher.ChequeNo =  null;
                    voucher.Frm_TransID = 6;
                    voucher.FrmTransChildID = 0;
                }
                
                voucher.IsVoucher = 1;
                string costcenter = "1`" + voucher.AmountNew+"|1`"+ voucher.AmountNew;
                voucher.CostCenter = costcenter;
                voucher.Date = Convert.ToDateTime(Settlement.Date);
                voucher.CreatedBy = Convert.ToInt32(Settlement.UserId);
                voucher.Drawon=  Convert.ToString(Settlement.DrawOn);
                OutputMessage result = null;
                Accounts = voucher.Frm_TransID + "|" + voucher.ToTransID;
                Child = voucher.FrmTransChildID + "|" + voucher.ToTransChildID;
                AmountString = voucher.AmountNew + "|" + voucher.AmountNew;
                voucher.username = "";
                result = voucher.Save(VoucherType,Accounts,Child,AmountString,voucher.CostCenter,"","");
                if (result.Success)
                {
                    int GroupID = Convert.ToInt32(result.Object);
                    db.Open();
                    db.BeginTransaction();
                    for (int i = 0; i < Settlement.Bills.Count; i++)
                    {
                        for (int j = 0; j < Settlement.Bills[i].Vouchers.Count; j++)
                        {
                            int Id = Convert.ToInt32(Settlement.Bills[i].Vouchers[j].Id) != 0 ? Convert.ToInt32(Settlement.Bills[i].Vouchers[j].Id) : GroupID;
                            decimal Amount = Convert.ToDecimal(Settlement.Bills[i].Vouchers[j].Amount);
                            int pe_id = Convert.ToInt32(Settlement.Bills[i].BillNo);
                            int partyId = Convert.ToInt32(Settlement.PartyId);
                            string sql = @"INSERT INTO [dbo].[TBL_FIN_SUPPLIER_PAYMENTS]([Supplier_Id],[Pe_Id],[Entry_Date],[Amount],[Fve_GroupID],[Created_By],[Created_Date]) VALUES(@Supplier_Id,@Pe_Id,@Entry_Date,@Amount,@Fve_GroupID,1,Getdate())";
                            db.CreateParameters(5);
                            db.AddParameters(0, "@Supplier_Id", partyId);
                            db.AddParameters(1, "@Pe_Id", pe_id);
                            db.AddParameters(2, "@Entry_Date", DateTime.Now);
                            db.AddParameters(3, "@Fve_GroupID", Id);
                            db.AddParameters(4, "@Amount", Amount);
                            db.ExecuteNonQuery(CommandType.Text, sql);
                        }
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Selected bills are settled", true, Type.NoError, "PurchasePayments | Settle", System.Net.HttpStatusCode.OK);
                }
                else
                {
                    return new OutputMessage("Failed To Make Payment", false, Type.Others, "PurchasePayments | Settle", System.Net.HttpStatusCode.OK,result.Message);
                }
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong", false, Type.NoError, "PurchasePayments | Settle", System.Net.HttpStatusCode.InternalServerError,ex);
            }
        }
    }
}
