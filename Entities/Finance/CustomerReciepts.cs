using Core.DBManager;
using Entities.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Finance
{
    public class CustomerReciepts
    {
        #region Properties
        public int CustomerID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CompanyID { get; set; }
        public int SupplierID { get; set; }
        public DateTime PayDate { get; set; }
        public int PayNumber { get; set; }
        public decimal PayAmount { get; set; }
        public string PayInvoiceNumber { get; set; }
        public int CreatedBy { get; set; }
        public int VoucherGroupID { get; set; }
        /// <summary>
        /// 0 -customer Reciepts 1->Payment Return 3->Sales Payment Opening
        /// </summary>
        public int BillType { get; set; }
        public int IsSetOff { get; set; } // 0 by default
        public int SupplierFahID { get; set; } //For SalesPayment Opening
        public string Narration { get; set; }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static dynamic GetSalesData(int CustomerId)
        {
            DBManager db = new DBManager();
            try
            {
                DataSet ds = new DataSet();
                #region query
                string sql = @"SELECT * FROM [VW_CUSTOMER_RECEIPTS] WHERE CUSTOMER_id=" + CustomerId + ";select * from  [dbo].[VW_UNLINKED_CUSTOMER_VOUCHERS] where Customer_Id=" + CustomerId;
                #endregion
                db.Open();
                ds = db.ExecuteDataSet(CommandType.Text, sql);

                if (ds != null)
                {

                    object Sales = new List<object>();
                    object Vouchers = new List<object>();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        ((List<object>)Sales).Add(new { SalesId = Convert.ToInt32(item["se_id"]), SalesDate = Convert.ToDateTime(item["sales_date"]).ToString("dd/MMM/yyyy"), BillNo = Convert.ToString(item["sales_bill_no"]), NetAmount = Convert.ToDecimal(item["netamount"]), BalanceAmount = Convert.ToDecimal(item["balamount"]) });
                    }
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        ((List<object>)Vouchers).Add(new { Type = Convert.ToString(item["Fvt_TypeName"]), Id = Convert.ToInt32(item["Fve_Number"]), Date = Convert.ToDateTime(item["Fve_Date"]).ToString("dd/MMM/yyyy"), Amount = Convert.ToDecimal(item["Fve_Amount"]) });
                    }

                    return new { CustomerID = Convert.ToInt32(ds.Tables[0].Rows[0]["customer_id"]), AvailableAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["avaamount"]), Vouchers, Sales, PendingAmount = ds.Tables[0].AsEnumerable().Sum(x => x.Field<decimal>("BalAmount")) };
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "CustomerReciepts | GetSalesData(int CustomerId)");
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
                string Accounts = "";
                string AmountString = "";
                string Child = "";
                string VoucherType = "0|1";
                VoucherEntry voucher = new VoucherEntry();
                voucher.VoucherTypeID = 4;
                voucher.VoucherNo = 0;
                voucher.Description = "C R";
                voucher.AmountNew = Convert.ToDecimal(Settlement.PayingAmount);
                voucher.Frm_TransID = 2;
                voucher.FrmTransChildID = Convert.ToInt32(Settlement.PartyId);
                string payby = Convert.ToString(Settlement.PayBy);
                if (payby== "Cheque"|| payby=="Bank")
                {
                    voucher.IsCheque = 1;
                    voucher.ChequeDate = ((string)(Settlement.ChequeDate)).IsValidDate() ? Convert.ToDateTime(Settlement.ChequeDate) : null;
                    voucher.ChequeNo = Settlement.ChequeNumber == "" ? null : Settlement.ChequeNumber;
                    voucher.ToTransID = Settlement.PayBank;//Parent Group for bank heads
                    voucher.ToTransChildID = 0;//Selected Bank Head id
                }
                else if (payby== "Cash")
                {
                    voucher.IsCheque = 0;
                    voucher.ChequeDate =  null;
                    voucher.ChequeNo =  null;
                    voucher.ToTransID = 6;
                    voucher.ToTransChildID = 0;
                }
                
                voucher.IsVoucher = 1;
                string costcenter = "1`" + voucher.AmountNew+ "|1`" + voucher.AmountNew;
                voucher.CostCenter = costcenter;
                voucher.Date = Convert.ToDateTime(Settlement.Date);
                voucher.CreatedBy = Convert.ToInt32(Settlement.UserId);
                voucher.Drawon = Convert.ToString(Settlement.DrawOn);
                OutputMessage result = null;
                Accounts = voucher.Frm_TransID + "|" + voucher.ToTransID;
                Child = voucher.FrmTransChildID + "|" + voucher.ToTransChildID;
                AmountString = voucher.AmountNew + "|" + voucher.AmountNew;
                voucher.username = "";
                result = voucher.Save(VoucherType, Accounts, Child, AmountString, voucher.CostCenter,"","");
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
                            int se_id = Convert.ToInt32(Settlement.Bills[i].BillNo);
                            int partyId = Convert.ToInt32(Settlement.PartyId);
                            string sql = @"INSERT INTO [dbo].[TBL_FIN_CUSTOMER_RECEIPTS]([Customer_Id],[Se_Id],[Entry_Date],[Amount],[Fve_GroupID],[Created_By],[Created_Date]) VALUES(@customer_Id,@Se_Id,@Entry_Date,@Amount,@Fve_GroupID,1,Getdate())";
                            db.CreateParameters(5);
                            db.AddParameters(0, "@customer_Id", partyId);
                            db.AddParameters(1, "@Se_Id", se_id);
                            db.AddParameters(2, "@Entry_Date", DateTime.Now);
                            db.AddParameters(3, "@Fve_GroupID", Id);
                            db.AddParameters(4, "@Amount", Amount);
                            db.ExecuteNonQuery(CommandType.Text, sql);
                        }
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Selected bills are settled", true, Type.NoError, "CustomerReciepts | Settle", System.Net.HttpStatusCode.OK);

                }
                else
                {
                    return new OutputMessage("Failed to Make Payment", false, Type.Others, "CustomerReciepts | Settle", System.Net.HttpStatusCode.OK,result.Message);
                }
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong", false, Type.NoError, "CustomerReciepts | Settle", System.Net.HttpStatusCode.InternalServerError,ex);
            }
        }


    }
}
