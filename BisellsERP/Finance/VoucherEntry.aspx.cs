using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class VoucherEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    txtDate.Text = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                    if (string.Equals(Request.QueryString["type"], "Normal", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Request.QueryString["id"] == "0")
                        {
                            ddlVoucherType.LoadVoucherTypesForEntry(CPublic.GetCompanyID());
                            ddlfrommain.LoadAccountHeadsVoucher(CPublic.GetCompanyID());
                            ddltomain.LoadAccountHeadsVoucher(CPublic.GetCompanyID());
                        }
                        else
                        {
                            ddlVoucherType.LoadVoucherTypesForEntry(CPublic.GetCompanyID());
                            ddlfrommain.LoadAccountHeadsVoucher(CPublic.GetCompanyID());
                            ddltomain.LoadAccountHeadsVoucher(CPublic.GetCompanyID());
                            loadVoucherDetails();
                        }
                        Session["type"] = Request.QueryString["type"];
                    }
                    else if (string.Equals(Request.QueryString["type"], "Expense", StringComparison.OrdinalIgnoreCase) && Request.QueryString["id"] == "0")
                    {
                        lblType.Text = "Expense";
                        loadVoucher();
                        Session["type"] = Request.QueryString["type"];
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Bad URL')", true);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Write("<script>alert('ssss')</script>");
                //AddDatatoDataset(0);
                //AddDatatoDataset(1);
                OutputMessage Result = null;
                if (hdItemId.Value == "0")
                {
                    Result = SaveVoucherEntry();
                    if (Result.Success)
                    {
                        Reset();
                        ViewState["DSTemp"] = null;
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }
                else
                {
                    
                    Result = UpdateVoucher();//UpdateVoucherNew();
                    if (Result.Success)
                    {
                        Reset();
                        ViewState["DSTemp"] = null;
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Something Went Wrong')", true);
            }
        }

        protected void ddlfrommain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherEntry Voucher = new Entities.Finance.VoucherEntry();
                decimal OpeningAmount = 0;
                string[] words1 = ddlfrommain.SelectedValue.Split('|');
                string headID, childID;
                if (Session["type"].ToString() == "Normal")
                {
                    if (words1[0].ToString() == "0")
                    {
                        headID = words1[1].ToString();
                        OpeningAmount = Voucher.CalculateOpeningBalance(Convert.ToInt32(headID), 0);
                    }
                    else
                    {
                        headID = words1[0].ToString();
                        childID = words1[1].ToString();
                        OpeningAmount = Voucher.CalculateOpeningBalance(Convert.ToInt32(headID), Convert.ToInt32(childID));
                    }
                }
                if (Session["type"].ToString() == "Expense")
                {
                    OpeningAmount = Voucher.CalculateOpeningBalance(Convert.ToInt32(ddlfrommain.SelectedValue), 0);
                }
                lblOpeningBalance.Text = OpeningAmount.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void loadVoucher()
        {
            try
            {
                Entities.Finance.ExpenseEntry.getVoucherTypes(ddlVoucherType);
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddltomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherEntry Voucher = new Entities.Finance.VoucherEntry();
                decimal OpeningAmount = 0;
                string[] words1 = ddltomain.SelectedValue.Split('|');
                string headID, childID;
                if (words1[0].ToString() == "0")
                {
                    headID = words1[1].ToString();
                    OpeningAmount = Voucher.CalculateOpeningBalance(Convert.ToInt32(headID), 0);
                }
                else
                {
                    headID = words1[0].ToString();
                    childID = words1[1].ToString();
                    OpeningAmount = Voucher.CalculateOpeningBalance(Convert.ToInt32(headID), Convert.ToInt32(childID));
                }
                lblToBalance.Text = OpeningAmount.ToString();
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Used For display the credit and debit in the table
        /// </summary>
        /// <param name="flag">0 for credit and 1 for debit</param>
        private void AddDatatoDataset(int flag = 0)
        {
            try
            {
                decimal Amount = 0;
                if (hdnAmount.Value != "0" && flag == 1)
                {
                    Amount = Convert.ToDecimal(hdnAmount.Value) + Convert.ToDecimal(txtamount.Text); //Amount Addition for debit
                }
                else if (txtamount.Text != "")
                {
                    Amount = Convert.ToDecimal(txtamount.Text);
                }
                else
                {
                    Amount = 0;
                }
                DataSet dsTemp = new DataSet();
                Entities.Finance.VoucherEntry voucher = new Entities.Finance.VoucherEntry();
                if (ViewState["DSTemp"] != null)
                    dsTemp = (DataSet)ViewState["DSTemp"];
                else
                {
                    dsTemp = voucher.CreateDataset();
                    ViewState["DSTemp"] = dsTemp;
                }
                DataRow dr;
                try
                {
                    dr = dsTemp.Tables[0].NewRow();
                    if (flag == 0)
                    {
                        string head = "";
                        //string child = "";
                        string[] words = ddltomain.SelectedValue.Split('|'); // for Voucher entry only
                        if (words[0].ToString() == "0")
                        {
                            head = words[1].ToString();
                        }
                        else
                        {
                            head = words[0].ToString();
                        }
                        dr["ParticularsID"] = head;//ddltomain.SelectedValue;
                        if (words[0].ToString() != "0")
                        {
                            dr["Particulars"] = ddlfrommain.SelectedItem.Text + "[" + ddltomain.SelectedItem.Text + "]";
                            dr["CostHead"] = words[1].ToString();
                        }
                        //if (ddltosub.SelectedIndex >= 0)
                        //{
                        //    dr["Particulars"] = ddltomain.SelectedItem.Text + "(" + ddltosub.SelectedItem.Text + ")";
                        //    dr["CostHead"] = ddltosub.SelectedValue;
                        //}
                        else
                        {
                            dr["Particulars"] = ddltomain.SelectedItem.Text + "[" + ddlfrommain.SelectedItem.Text + "]";
                            dr["CostHead"] = "0";
                        }
                        dr["CreditAmt"] = txtamount.Text;
                        dr["DebitAmt"] = 0;
                        dr["CreditOrDebit"] = "1";
                    }
                    else
                    {
                        try
                        {
                            dsTemp.Tables[0].Rows[0].Delete(); // deleting the top debit row if exists
                        }
                        catch (Exception)
                        {

                        }
                        string headID = "";
                        //string child = "";
                        string[] words1 = ddlfrommain.SelectedValue.Split('|');
                        if (Session["type"].ToString() == "Normal")
                        {
                            if (words1[0].ToString() == "0")
                            {
                                headID = words1[1].ToString();
                            }
                            else
                            {
                                headID = words1[0].ToString();
                            }
                        }
                        else
                        {
                            headID = ddlfrommain.SelectedValue;
                        }

                        dr["ParticularsID"] = headID;
                        //dr["ParticularsID"] = ddlfrommain.SelectedValue;
                        try
                        {
                            if (words1[0].ToString() != "0")
                            {
                                dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                                dr["CostHead"] = words1[1].ToString();
                            }
                            //if (ddlfromsub.SelectedIndex >= 0)
                            //{
                            //    dr["Particulars"] = ddlfrommain.SelectedItem.Text + "(" + ddlfromsub.SelectedItem.Text + ")";
                            //    dr["CostHead"] = ddlfromsub.SelectedValue;
                            //}
                            else
                            {
                                dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                                dr["CostHead"] = "0";
                            }
                        }
                        catch (Exception)
                        {
                            dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                            dr["CostHead"] = "0";
                        }
                        //dr["CostHead"] = "0";
                        //dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                        dr["DebitAmt"] = Amount;
                        dr["CreditAmt"] = 0;
                        dr["CreditOrDebit"] = "0";
                    }


                    dr["CostCenter"] = "1" + "`" + txtamount.Text;

                    dr["Amount"] = Amount;
                    dsTemp.Tables[0].Rows.Add(dr);
                    dsTemp.AcceptChanges();
                    DataTable dtviewfilter = new DataTable();
                    if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        dtviewfilter = dsTemp.Tables[0];
                        DataView dv = new DataView(dtviewfilter);
                        dv.Sort = "CreditOrDebit ASC";
                        dtviewfilter = dv.ToTable();
                        dsTemp.Tables.Clear();
                        dsTemp.Tables.Add(dtviewfilter);
                    }

                    ViewState["DSTemp"] = dsTemp;
                    string lit = "";
                    //Adding Cntents to table
                    for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                    {
                        lit += "<tr><td>" + dsTemp.Tables[0].Rows[i]["Particulars"].ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(dsTemp.Tables[0].Rows[i]["DebitAmt"])).ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(dsTemp.Tables[0].Rows[i]["CreditAmt"])).ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(dsTemp.Tables[0].Rows[i]["Amount"])).ToString() + "</td></tr>";
                    }
                    tableContent.Text = lit;
                    Amount = Math.Round(Convert.ToDecimal(dsTemp.Tables[0].Rows[0]["Amount"]));
                    hdnAmount.Value = Amount.ToString();
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        private OutputMessage SaveVoucherEntry()
        {
            try
            {
                OutputMessage result = null;
                Entities.Finance.VoucherEntry Voucher = new Entities.Finance.VoucherEntry();
                Voucher.CreatedBy = CPublic.GetuserID();
                DataSet dsTemp = new DataSet();
                StringBuilder StrVType = new StringBuilder();
                StringBuilder StrAccID = new StringBuilder();
                StringBuilder StrChdID = new StringBuilder();
                StringBuilder StrAmount = new StringBuilder();
                StringBuilder StrCostCtr = new StringBuilder();
                if (ViewState["DSTemp"] != null)
                    dsTemp = (DataSet)ViewState["DSTemp"];
                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                    {
                        row = dsTemp.Tables[0].Rows[i];
                        if (Convert.ToString(row["CreditOrDebit"]) != "")
                        {
                            if (StrVType.ToString() == "")
                                StrVType.Append(Convert.ToString(row["CreditOrDebit"]));
                            else
                                StrVType.Append("|" + Convert.ToString(row["CreditOrDebit"]));
                        }
                        if (Convert.ToString(row["ParticularsID"]) != "")
                        {
                            if (StrAccID.ToString() == "")
                                StrAccID.Append(Convert.ToString(row["ParticularsID"]));
                            else
                                StrAccID.Append("|" + Convert.ToString(row["ParticularsID"]));
                        }
                        if (Convert.ToString(row["CostHead"]) != "")
                        {
                            if (StrChdID.ToString() == "")
                                StrChdID.Append(Convert.ToString(row["CostHead"]));
                            else
                                StrChdID.Append("|" + Convert.ToString(row["CostHead"]));
                        }
                        if (Convert.ToString(row["Amount"]) != "")
                        {
                            if (StrAmount.ToString() == "")
                                StrAmount.Append(Convert.ToString(row["Amount"]));
                            else
                                StrAmount.Append("|" + Convert.ToString(row["Amount"]));
                        }
                        if (Convert.ToString(row["CostCenter"]) != "")
                        {
                            if (StrCostCtr.ToString() == "")
                                StrCostCtr.Append(Convert.ToString(row["CostCenter"]));
                            else
                                StrCostCtr.Append("|" + Convert.ToString(row["CostCenter"]));
                        }
                    }
                }
                //string Vtype = "", StrAccID="", StrChdID="", StrAmount="", StrCostCtr="";
                //if (ViewState["DSTemp"] != null)
                //{
                //    dsTemp = (DataSet)ViewState["DSTemp"];
                //}
                //if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                //{
                //    DataRow row;
                //    for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                //    {
                //        Voucher.Frm_TransID = 0;
                //        Voucher.ToTransID = 0;
                //        Voucher.FrmTransChildID = 0;
                //        Voucher.ToTransChildID = 0;
                //        row = dsTemp.Tables[0].Rows[i];
                //        //if (Convert.ToString(row["CreditOrDebit"]) != "")
                //        //{
                //        //    //Voucher.VoucherType = Convert.ToString(row["CreditOrDebit"]);
                //        //    Vtype += Convert.ToString(row["CreditOrDebit"]) + "|";
                //        //}
                //        //if (Convert.ToString(row["ParticularsID"]) != "")
                //        //{

                //        //    StrAccID += Convert.ToInt32(row["ParticularsID"]) + "|";
                //        //    //if (Voucher.VoucherType=="1")
                //        //    //{

                //        //    //    //Voucher.Frm_TransID = Convert.ToInt32(row["ParticularsID"]);
                //        //    //}
                //        //    //else
                //        //    //{
                //        //    //    StrAccID += Convert.ToInt32(row["ParticularsID"]) + "|";
                //        //    //    Voucher.ToTransID = Convert.ToInt32(row["ParticularsID"]);
                //        //    //}
                //        //}
                //        if (Convert.ToString(row["CostHead"]) != ""||Convert.ToString(row["CostHead"])!="0")    //row["CostHead"]
                //        {
                //            if (Voucher.VoucherType == "0")
                //            {
                //                if (Voucher.Frm_TransID!=0)
                //                {
                //                     Voucher.FrmTransChildID = Convert.ToInt32(row["CostHead"]);
                //                }
                //                else
                //                {
                //                    //Voucher.FrmTransChildID == 0 ? null : Convert.ToInt32(row["CostHead"]);
                //                }
                //            }
                //            else
                //            {
                //                Voucher.ToTransChildID = Convert.ToInt32(row["CostHead"]);
                //            }
                //        }
                //        if (Convert.ToString(row["Amount"]) != "")
                //        {
                //            Voucher.AmountNew = Convert.ToDecimal(row["Amount"]);
                //        }
                //        if (Convert.ToString(row["CostCenter"]) != "")
                //        {
                //            Voucher.CostCenter = Convert.ToString(row["CostCenter"]);
                //        }
                Voucher.Date = Convert.ToDateTime(txtDate.Text); //DateTime.Now.ToString();
                Voucher.username = Convert.ToString(Session["UserName"]);
                Voucher.Description = txtnarration.Text;
                Voucher.IsCheque = 0;
                Voucher.ChequeNo = "";
                Voucher.ChequeDate = null;
                Voucher.IsVoucher = 1;
                Voucher.VoucherTypeID = Convert.ToInt32(ddlVoucherType.SelectedValue);
                if (txtNumber.ReadOnly == true)
                {
                    if (txtNumber.Text.Trim() == "")
                        Voucher.VoucherNo = 0;
                    else
                        Voucher.VoucherNo = Convert.ToInt32(txtNumber.Text);
                }
                //        Voucher.groupID = Convert.ToInt32(hdnGroupID.Value);
                //    }
                result = Voucher.Save(Convert.ToString(StrVType), Convert.ToString(StrAccID), Convert.ToString(StrChdID), Convert.ToString(StrAmount), Convert.ToString(StrCostCtr),"","");
                //    hdnGroupID.Value = Convert.ToString(result.Object);
                //}
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private void Reset()
        {
            try
            {

                txtamount.Text = "";
                tableContent.Text = "";
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtnarration.Text = "";
                ddlfrommain.Enabled = true;
                ddlVoucherType.SelectedValue = "0";
                ddlfrommain.SelectedValue = "";
                ddltomain.SelectedValue = "";
                ddlfromsub.Items.Clear();
                ddltosub.Items.Clear();
                //Response.Redirect("/finance/voucherentry?type=Normal&id=0");
            }
            catch (Exception ex)
            {

            }
        }

        protected void hiddenButton_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherEntry voucher = new Entities.Finance.VoucherEntry();
                voucher.getDataForEdit(Convert.ToInt32(hdItemId.Value), txtDate, txtnarration, ddlVoucherType, txtamount, ddlfrommain, ddltomain, ddltosub, ddlfromsub);
            }
            catch (Exception)
            {

            }
        }
        private OutputMessage UpdateVoucher()
        {
            try
            {
                OutputMessage result = null;
                Entities.Finance.VoucherEntry Voucher = new Entities.Finance.VoucherEntry();
                Voucher.CreatedBy = CPublic.GetuserID();
                DataSet dsTemp = new DataSet();
                if (ViewState["DSTemp"] != null)
                {
                    dsTemp = (DataSet)ViewState["DSTemp"];
                }
                if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                {
                    DataRow row;
                    Voucher.DeleteVoucher(Convert.ToInt32(hdItemId.Value));
                    for (int i = 0; i < dsTemp.Tables[0].Rows.Count; i++)
                    {
                        Voucher.Frm_TransID = 0;
                        Voucher.ToTransID = 0;
                        Voucher.FrmTransChildID = 0;
                        Voucher.ToTransChildID = 0;
                        row = dsTemp.Tables[0].Rows[i];
                        if (Convert.ToString(row["CreditOrDebit"]) != "")
                        {
                            Voucher.VoucherType = Convert.ToString(row["CreditOrDebit"]);
                        }
                        if (Convert.ToString(row["ParticularsID"]) != "")
                        {
                            if (Voucher.VoucherType == "0")
                            {
                                Voucher.Frm_TransID = Convert.ToInt32(row["ParticularsID"]);
                            }
                            else
                            {
                                Voucher.ToTransID = Convert.ToInt32(row["ParticularsID"]);
                            }
                        }
                        if (Convert.ToString(row["CostHead"]) != "")    //row["CostHead"]
                        {
                            if (Voucher.VoucherType == "0")
                            {
                                if (Voucher.Frm_TransID != 0)
                                {
                                    Voucher.FrmTransChildID = Convert.ToInt32(row["CostHead"]);
                                }
                                else
                                {
                                    //Voucher.FrmTransChildID == 0 ? null : Convert.ToInt32(row["CostHead"]);
                                }
                            }
                            else
                            {
                                Voucher.ToTransChildID = Convert.ToInt32(row["CostHead"]);
                            }
                        }
                        if (Convert.ToString(row["Amount"]) != "")
                        {
                            Voucher.AmountNew = Convert.ToDecimal(row["Amount"]);
                        }
                        if (Convert.ToString(row["CostCenter"]) != "")
                        {
                            Voucher.CostCenter = Convert.ToString(row["CostCenter"]);
                        }
                        Voucher.Date = Convert.ToDateTime(txtDate.Text); //DateTime.Now.ToString();
                        Voucher.username = Convert.ToString(Session["UserName"]);
                        Voucher.Description = txtnarration.Text;
                        Voucher.IsCheque = 0;
                        Voucher.ChequeNo = "";
                        Voucher.ChequeDate = null;
                        Voucher.IsVoucher = 1;
                        Voucher.VoucherTypeID = Convert.ToInt32(ddlVoucherType.SelectedValue);
                        if (txtNumber.ReadOnly == true)
                        {
                            if (txtNumber.Text.Trim() == "")
                                Voucher.VoucherNo = 0;
                            else
                                Voucher.VoucherNo = Convert.ToInt32(txtNumber.Text);
                        }
                        Voucher.groupID = Convert.ToInt32(hdItemId.Value);
                        result = Voucher.Save();
                        //hdnGroupID.Value = Convert.ToString(result.Object);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["type"].ToString() == "Expense")
                {
                    Entities.Finance.ExpenseEntry.getAccountGroup(ddlfrommain, Convert.ToInt32(ddlVoucherType.SelectedValue));
                    ddltomain.LoadAccountHeadsVoucher(CPublic.GetCompanyID());
                }
            }
            catch (Exception)
            {
            }
        }
        private void loadVoucherDetails()
        {
            Entities.Finance.VoucherEntry Voucher = new Entities.Finance.VoucherEntry();
            hdItemId.Value = Request.QueryString["id"].ToString();
            DataSet ds = new DataSet();
            ds = Voucher.GetDatasetForEdit(Convert.ToInt32(hdItemId.Value));
            ViewState["DSTemp"] = ds;
            string lit = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lit += "<tr><td>" + ds.Tables[0].Rows[i]["Particulars"].ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["DebitAmt"])).ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["CreditAmt"])).ToString() + "</td><td>" + Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"])).ToString() + "</td></tr>";
            }
            hdnAmount.Value = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"]).ToString();
            tableContent.Text = lit;
            ddlfrommain.Enabled = false;
            Voucher.loadVoucherDetails(Convert.ToInt32(Request.QueryString["id"]), ddlfrommain, ddltomain, ddlVoucherType, txtnarration, txtDate, txtamount);
        }

        protected void AddNewHead_Click(object sender, EventArgs e)
        {

            ddlfrommain.Enabled = false;
            if (ddlfrommain.SelectedValue==ddltomain.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "errorAlert('Select Different Accounts')", true);
            }
            else if (ddlfrommain.SelectedValue=="0"||ddltomain.SelectedValue=="0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "errorAlert('Select Account')", true);
            }
            else
            {
                if (txtamount.Text != "")
                {
                    AddDatatoDataset(1);
                    AddDatatoDataset(0);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "errorAlert('No Amount Entered')", true);
                }
            }
            txtamount.Text = "";
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ViewState["DSTemp"] = null;
            string type = Request.QueryString["type"].ToString();
            Response.Redirect("/finance/voucherentry?type=" + type + "&id=0");
        }

        //private OutputMessage UpdateVoucherNew()
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = (DataSet)ViewState["DSTemp"];
        //        String ObjectID = "", ChildAccountID = "", DrAmount = "", CrAmount = "";
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (ObjectID == "")
        //                ObjectID = ds.Tables[0].Rows[i]["ParticularsID"].ToString();//((Label)grdVoucher.Rows[i].FindControl("ObjectID")).Text;
        //            else
        //                ObjectID = ObjectID + "|" + ds.Tables[0].Rows[i]["ParticularsID"].ToString();//((Label)grdVoucher.Rows[i].FindControl("ObjectID")).Text;

        //               if (ChildAccountID == "")
        //                   ChildAccountID = ds.Tables[0].Rows[i]["CostHead"].ToString();//"0";
        //            else
        //                   ChildAccountID = ChildAccountID + "|" + ds.Tables[0].Rows[i]["CostHead"].ToString();
        //            if (DrAmount == "")
        //                DrAmount = ds.Tables[0].Rows[i]["DebitAmt"].ToString();//((TextBox)grdVoucher.Rows[i].FindControl("txtDr")).Text;
        //            else
        //                DrAmount = DrAmount + "|" + ds.Tables[0].Rows[i]["DebitAmt"].ToString();//((TextBox)grdVoucher.Rows[i].FindControl("txtDr")).Text;

        //            if (CrAmount == "")
        //                CrAmount = ds.Tables[0].Rows[i]["creditAmt"].ToString(); // ((TextBox)grdVoucher.Rows[i].FindControl("txtCr")).Text;
        //            else
        //               CrAmount = CrAmount + "|" + ds.Tables[0].Rows[i]["creditAmt"].ToString();//((TextBox)grdVoucher.Rows[i].FindControl("txtCr")).Text;
        //        }
        //        if (!String.IsNullOrEmpty(ObjectID))
        //        {
        //            Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
        //            OutputMessage Result = null;
        //            voucher.ModifiedBy = CPublic.GetuserID();
        //            voucher.ID = Convert.ToInt32(hdItemId.Value);
        //            Result = voucher.UpdateVoucher(ObjectID, ChildAccountID, DrAmount, CrAmount);
        //            return Result;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}