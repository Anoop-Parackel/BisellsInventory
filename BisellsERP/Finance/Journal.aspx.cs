using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using System.Data;

namespace BisellsERP.Finance
{
    public partial class Journal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Gets the voucher type from url and loads it into hiddenfield
                if (Request.QueryString["VOUCHER"] != null)
                {
                    hdnVoucherType.Value = Request.QueryString["VOUCHER"].ToString();
                    if (Request.QueryString["ID"]!=null)
                    {
                        hdnGroupID.Value = Request.QueryString["ID"].ToString();
                    }
                }
                //If Only ID is Given in the URL
                else if (Request.QueryString["ID"] != null)
                {
                    hdnGroupID.Value = Request.QueryString["ID"].ToString();
                    DataSet VoucherData = new DataSet();
                    VoucherData=Entities.Finance.VoucherEntry.GetDataset(Convert.ToInt32(hdnGroupID.Value));
                    hdnVoucherType.Value=VoucherData.Tables[0].Rows[0]["Fve_VoucherType"].ToString();
                }
                else
                {
                    //If null stores is at JNL voucher type //Default
                    hdnVoucherType.Value = "8";
                }
                DataTable dt = new DataTable();
                Entities.Finance.AccountHeadMaster account = new Entities.Finance.AccountHeadMaster();
                //Datatable to load credit head
                dt = account.GetAccountHeadsVoucher(Convert.ToInt32(hdnVoucherType.Value), 1, CPublic.GetCompanyID());
                ddlCreditHead.Items.Add(new ListItem("--select--", "0"));
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //Creates a new list item
                        ListItem items = new ListItem(item["name"].ToString(), item["parent"].ToString() + "|" + item["ID"].ToString());
                        ddlCreditHead.Items.Add(items);
                    }
                }
                //Datatable to load the debit heads 
                dt = account.GetAccountHeadsVoucher(Convert.ToInt32(hdnVoucherType.Value), 0, CPublic.GetCompanyID());
                ddlDebitDummyHead.Items.Add(new ListItem("--select--", "0"));
                ddlDebithead1.Items.Add(new ListItem("--select--", "0"));
                ddlDebithead2.Items.Add(new ListItem("--select--", "0"));
                ddlDebithead3.Items.Add(new ListItem("--select--", "0"));
                ddlDebithead4.Items.Add(new ListItem("--select--", "0"));
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        //Creates a new list item and appends it to the debit dropdownlist and clone dropdownlist
                        ListItem items = new ListItem(item["name"].ToString(), item["parent"].ToString() + "|" + item["ID"].ToString());
                        ddlDebitDummyHead.Items.Add(items);
                        ddlDebithead1.Items.Add(items);
                        ddlDebithead2.Items.Add(items);
                        ddlDebithead3.Items.Add(items);
                        ddlDebithead4.Items.Add(items);
                    }
                }
                Entities.Finance.VoucherEntry voucher = new Entities.Finance.VoucherEntry();
                //Datatable Loads the Job and appends to all JOb drop downlist
                dt = voucher.GetJobs(CPublic.GetCompanyID());
                ddlJobs1.Items.Add(new ListItem("--select--", "0"));
                ddlJobs2.Items.Add(new ListItem("--select--", "0"));
                ddlJobs3.Items.Add(new ListItem("--select--", "0"));
                ddlJobs4.Items.Add(new ListItem("--select--", "0"));
                ddlDebitDummyJob.Items.Add(new ListItem("--select--", "0"));
                ddlCreditJob.Items.Add(new ListItem("--select--", "0"));
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem items = new ListItem(item["Job_Name"].ToString(), item["Job_Id"].ToString());
                        ddlJobs1.Items.Add(items);
                        ddlJobs2.Items.Add(items);
                        ddlJobs3.Items.Add(items);
                        ddlJobs4.Items.Add(items);
                        ddlDebitDummyJob.Items.Add(items);
                        ddlCreditJob.Items.Add(items);
                    }
                }

                //Datatable to load cost center and appends to the cost center dropdownlist
                dt = voucher.LoadCostCenter();
                ddlDebitDummyCost.Items.Add(new ListItem("--select--", "0"));
                ddlDebitCost1.Items.Add(new ListItem("--select--", "0"));
                ddlDebitCost2.Items.Add(new ListItem("--select--", "0"));
                ddlDebitCost3.Items.Add(new ListItem("--select--", "0"));
                ddlDebitCost4.Items.Add(new ListItem("--select--", "0"));
                ddlCreditCost.Items.Add(new ListItem("--select--", "0"));
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem items = new ListItem(item["name"].ToString(), item["id"].ToString());
                        ddlDebitDummyCost.Items.Add(items);
                        ddlDebitCost1.Items.Add(items);
                        ddlDebitCost2.Items.Add(items);
                        ddlDebitCost3.Items.Add(items);
                        ddlDebitCost4.Items.Add(items);
                        ddlCreditCost.Items.Add(items);
                    }
                }
                DataSet ds = new DataSet();
                //Gets the Url Parameter and changes title and voucher number according to url parameter
                ds = voucher.GetVoucherNo(Convert.ToInt32(hdnVoucherType.Value));
                VoucherNumber.InnerText = ds.Tables[1].Rows[0]["Fvt_TypeName"].ToString() + ":" + ds.Tables[0].Rows[0]["Number"].ToString();
                lblTitle.InnerText = ds.Tables[1].Rows[0]["Fvt_TypeName"].ToString();
                hdnVoucherTypeName.Value = ds.Tables[1].Rows[0]["Fvt_TypeName"].ToString();
                //Loads the voucher types in the changetype dropdown list
                ddlChangeType.LoadVoucherTypes(CPublic.GetCompanyID());
                if (hdnGroupID.Value!="0")
                {
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}