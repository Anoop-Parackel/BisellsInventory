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
    public partial class ExpenseEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadVoucher();
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                AddDatatoDataset(0);
                AddDatatoDataset(1);
                OutputMessage Result = null;
                if (hdItemId.Value=="0")
                {
                    Result = SaveExpenseEntry();
                    if (Result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }
                else
                {
                    Result = UpdateExpense();
                    if (Result.Success)
                    {
                        Reset();
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter A valid Expense')", true);
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
        private void AddDatatoDataset(int flag = 0)
        {
            try
            {
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
                        dr["ParticularsID"] = ddltomain.SelectedValue;
                        if (ddltosub.SelectedIndex >= 0)
                        {
                            dr["Particulars"] = ddltomain.SelectedItem.Text + "(" + ddltosub.SelectedItem.Text + ")";
                            dr["CostHead"] = ddltosub.SelectedValue;
                        }
                        else
                        {
                            dr["Particulars"] = ddltomain.SelectedItem.Text;
                            dr["CostHead"] = "0";
                        }
                        dr["DebitAmt"] = txtamount.Text;
                        dr["CreditAmt"] = "";
                        dr["CreditOrDebit"] = "1";
                    }
                    else
                    {
                        dr["ParticularsID"] = ddlfrommain.SelectedValue;
                        if (ddlfromsub.SelectedIndex >= 0)
                        {
                            dr["Particulars"] = ddlfrommain.SelectedItem.Text + "(" + ddlfromsub.SelectedItem.Text + ")";
                            dr["CostHead"] = ddlfromsub.SelectedValue;
                        }
                        else
                        {
                            dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                            dr["CostHead"] = "0";
                        }
                        dr["Particulars"] = ddlfrommain.SelectedItem.Text;
                        dr["CreditAmt"] = txtamount.Text;
                        dr["DebitAmt"] = "";
                        dr["CreditOrDebit"] = "0";
                    }
                    dr["CostCenter"] = "1" + "`" + txtamount.Text;
                    dr["Amount"] = txtamount.Text;
                    dsTemp.Tables[0].Rows.Add(dr);
                    dsTemp.AcceptChanges();
                    DataTable dtviewfilter = new DataTable();
                    if (dsTemp != null && dsTemp.Tables[0].Rows.Count > 0)
                    {
                        dtviewfilter = dsTemp.Tables[0];

                        DataView dv = new DataView(dtviewfilter);
                        dv.Sort = "CreditOrDebit DESC";
                        dtviewfilter = dv.ToTable();
                        dsTemp.Tables.Clear();
                        dsTemp.Tables.Add(dtviewfilter);
                    }
                    ViewState["DSTemp"] = dsTemp;
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception)
            {
                
            }
        }
        private OutputMessage SaveExpenseEntry()
        {
            try
            {
                Entities.Finance.VoucherEntry Expense = new Entities.Finance.VoucherEntry();
                DataSet dsTemp = new DataSet();
                StringBuilder StrVType = new StringBuilder();
                StringBuilder StrAccID = new StringBuilder();
                StringBuilder StrChdID = new StringBuilder();
                StringBuilder StrAmount = new StringBuilder();
                StringBuilder StrCostCtr = new StringBuilder();
                Expense.CreatedBy = CPublic.GetuserID();
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
                Expense.Date = DateTime.Now;
                Expense.username = Convert.ToString(Session["UserName"]);
                Expense.Description = txtnarration.Text;

                Expense.IsCheque = 0;
                Expense.ChequeNo = "";
                Expense.ChequeDate = DateTime.MinValue;
                if (txtNumber.ReadOnly == true)
                {
                    if (txtNumber.Text.Trim() == "")
                        Expense.VoucherNo = 0;
                    else
                        Expense.VoucherNo = Convert.ToInt32(txtNumber.Text);
                }
                Expense.VoucherTypeID = Convert.ToInt32(ddlVoucherType.SelectedValue);
                Expense.VoucherType = StrVType.ToString();
                Expense.AccountHead = StrAccID.ToString();
                Expense.AccountChild = StrChdID.ToString();
                Expense.Amount = StrAmount.ToString();
                Expense.CostCenter = StrCostCtr.ToString();
                DataSet dsVoucherEntryDet = new DataSet();
                OutputMessage Result = null;
                Result = Expense.Save(StrVType.ToString(), StrAccID.ToString(), StrChdID.ToString(), StrAmount.ToString(), StrCostCtr.ToString(),"","");
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void Reset()
        {
            try
            {
                ddlVoucherType.SelectedValue = "0";
                ddlfrommain.SelectedValue = "0";
                ddltomain.SelectedValue = "0";
                txtamount.Text = "";
                txtDate.Text = "";
                txtnarration.Text = "";
                ddltosub.Items.Clear();
                ddlfromsub.Items.Clear();
            }
            catch (Exception)
            {
               
            }
        }
        protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.ExpenseEntry.getAccountGroup(ddlfrommain, Convert.ToInt32(ddlVoucherType.SelectedValue));
                Entities.Finance.ExpenseEntry.LoadToHead(ddltomain);
            }
            catch (Exception)
            {
            }
        }
        protected void hiddenButton_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.ExpenseEntry Expense = new Entities.Finance.ExpenseEntry();
                Expense.getDataForEdit(Convert.ToInt32(hdItemId.Value), txtDate, txtnarration, ddlVoucherType, txtamount, ddlfrommain, ddltomain, ddltosub, ddlfromsub);
            }
            catch (Exception)
            {
                
            }
        }
        private OutputMessage UpdateExpense()
        {
            try
            {
                Entities.Finance.VoucherEntry Expense = new Entities.Finance.VoucherEntry();
                DataSet dsTemp = new DataSet();
                StringBuilder StrVType = new StringBuilder();
                StringBuilder StrAccID = new StringBuilder();
                StringBuilder StrChdID = new StringBuilder();
                StringBuilder StrAmount = new StringBuilder();
                StringBuilder StrCostCtr = new StringBuilder();
                Expense.CreatedBy = CPublic.GetuserID();

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


                        if (Convert.ToString(row["CostHead"]) != "")    //row["CostHead"]
                        {
                            if (StrChdID.ToString() == "")
                                StrChdID.Append(Convert.ToString(row["CostHead"]));  //row["CostHead"]
                            else
                                StrChdID.Append("|" + Convert.ToString(row["CostHead"]));  //row["CostHead"]
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

                Expense.Date = DateTime.Now;
                Expense.username = Convert.ToString(Session["UserName"]);
                Expense.Description = txtnarration.Text;

                Expense.IsCheque = 0;
                Expense.ChequeNo = "";
                Expense.ChequeDate = DateTime.MinValue;
                if (txtNumber.ReadOnly == true)
                {
                    if (txtNumber.Text.Trim() == "")
                        Expense.VoucherNo = 0;
                    else
                        Expense.VoucherNo = Convert.ToInt32(txtNumber.Text);
                }


                Expense.VoucherTypeID = Convert.ToInt32(ddlVoucherType.SelectedValue);
                Expense.VoucherType = StrVType.ToString();
                Expense.AccountHead = StrAccID.ToString();
                Expense.AccountChild = StrChdID.ToString();
                Expense.Amount = StrAmount.ToString();
                Expense.CostCenter = StrCostCtr.ToString();
                Expense.ModifiedBy = CPublic.GetuserID();
                Expense.ID = Convert.ToInt32(hdItemId.Value);
                DataSet dsVoucherEntryDet = new DataSet();
                OutputMessage result = null;
                result = Expense.Update(StrVType.ToString(), StrAccID.ToString(), StrChdID.ToString(), StrAmount.ToString(), StrCostCtr.ToString());
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}