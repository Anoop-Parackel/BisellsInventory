using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class VoucherEdit : System.Web.UI.Page
    {
        #region "Member Declaration"

        private double DrSum = 0;
        private double CrSum = 0;

        public double GetDrSum
        {
            get { return DrSum; }

        }
        public double GetCrSum
        {
            get { return CrSum; }

        }



        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["vchid"] != null && Request.QueryString["groupid"] != null)
                    {
                        hdnGroupID.Value = Request.QueryString["groupid"];
                        hdnVoucherID.Value = Request.QueryString["vchid"];
                        hdnIsVoucher.Value = Request.QueryString["FvVouchrtype"];
                        ddlVoucherType.LoadVoucherTypes(CPublic.GetCompanyID());
                        loaddata(Convert.ToInt32(hdnGroupID.Value));
                        txtChequeDate.Enabled = false;
                        txtChequeNumber.Enabled = false;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void loaddata(int group)
        {
            try
            {
                lblVoucherNumber.Text = hdnGroupID.Value;
                Entities.Finance.VoucherEdit voucher1 = new Entities.Finance.VoucherEdit();
                Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
                DataTable dt = new DataTable();
                dt = voucher1.getdata(Convert.ToInt32(hdnGroupID.Value), ddlVoucherType, txtNarration, txtVoucherDate);
                DrSum = Convert.ToDouble(dt.Rows[0]["SumDrAmount"].ToString());
                CrSum = Convert.ToDouble(dt.Rows[0]["SumCrAmount"].ToString());
                grdVoucher.DataSource = dt;
                grdVoucher.DataBind();
            }
            catch (Exception)
            {
                
            }
        }
        protected void grdVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    int pageCountToAdd = (grdVoucher.PageIndex * grdVoucher.PageSize) + 1;
                    e.Row.Cells[1].Text = Convert.ToString(e.Row.RowIndex + pageCountToAdd);
                    if (grdVoucher.EditIndex == -1)
                    {
                        ((Label)e.Row.FindControl("lblCr")).Visible = false;
                        ((Label)e.Row.FindControl("lblDr")).Visible = false;
                        if (((Label)e.Row.FindControl("PreFixID")).Text == "Dr")
                        {
                            ((TextBox)e.Row.FindControl("txtCr")).Visible = false;
                            //  ((LinkButton)e.Row.FindControl("cmdCr")).Visible = false;
                        }
                        else
                        {
                            ((TextBox)e.Row.FindControl("txtDr")).Visible = false;
                            // ((LinkButton)e.Row.FindControl("cmdDr")).Visible = false;
                        }

                        if (((Label)e.Row.FindControl("HasChild")).Text == "0")
                        {
                            ((DropDownList)e.Row.FindControl("ddlChildHead")).Visible = false;
                            // ((LinkButton)e.Row.FindControl("cmdDr")).Visible = false;
                            // ((LinkButton)e.Row.FindControl("cmdCr")).Visible = false;
                        }
                        else
                        {
                            if (((Label)e.Row.FindControl("HasTrans")).Text == "1")
                            {
                                //((TextBox)e.Row.FindControl("txtCr")).Visible = false;
                                //((TextBox)e.Row.FindControl("txtDr")).Visible = false;
                                ((DropDownList)e.Row.FindControl("ddlChildHead")).Enabled = false;
                                //  ((LinkButton)e.Row.FindControl("cmdDr")).Visible = false;
                                //  ((LinkButton)e.Row.FindControl("cmdCr")).Visible = false;

                                //if (((Label)e.Row.FindControl("AccountID")).Text == "4" ||
                                //   ((Label)e.Row.FindControl("AccountID")).Text == "6" ||
                                //   ((Label)e.Row.FindControl("AccountID")).Text == "11" ||
                                //   ((Label)e.Row.FindControl("HasGeneral")).Text == "1"
                                //   )
                                //{
                                //    ((DropDownList)e.Row.FindControl("ddlChildHead")).Enabled = false;

                                //}
                                //else
                                //    ((DropDownList)e.Row.FindControl("ddlChildHead")).Enabled = true;



                            }
                            else
                            {
                                // ((LinkButton)e.Row.FindControl("cmdDr")).Visible = false;
                                // ((LinkButton)e.Row.FindControl("cmdCr")).Visible = false;

                            }
                        }



                    }
                    else
                    {

                        // ((Button)e.Row.FindControl("cmdUpdate")).Visible = false;
                        ((TextBox)e.Row.FindControl("txtCr")).Visible = false;
                        ((TextBox)e.Row.FindControl("txtDr")).Visible = false;
                        ((DropDownList)e.Row.FindControl("ddlChildHead")).Visible = false;
                        //((LinkButton)e.Row.FindControl("cmdDr")).Visible = false;
                        //((LinkButton)e.Row.FindControl("cmdCr")).Visible = false;

                        if (((Label)e.Row.FindControl("HasChild")).Text == "1")
                            if (((Label)e.Row.FindControl("HasTrans")).Text == "1")
                                ((DropDownList)e.Row.FindControl("ddlAccHead")).Enabled = false;

                        //{
                        //    if (((Label)e.Row.FindControl("AccountID")).Text == "4" 
                        //        || ((Label)e.Row.FindControl("AccountID")).Text == "6"
                        //        ||((Label)e.Row.FindControl("AccountID")).Text == "11" 
                        //        ||((Label)e.Row.FindControl("HasGeneral")).Text == "1"
                        //        )
                        //        if (e.Row.FindControl("ddlAccHead")!=null)
                        //    ((DropDownList)e.Row.FindControl("ddlAccHead")).Enabled = false;


                        //}


                    }


                }

                if (e.Row.RowType == DataControlRowType.Footer)
                    if (grdVoucher.EditIndex >= 0)

                        ((Button)e.Row.FindControl("cmdUpdate")).Visible = false;
            }
            catch (Exception)
            {
               
            }
        }
        protected void grdVoucher_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdVoucher.EditIndex = e.NewEditIndex;
                loaddata(Convert.ToInt32(hdnGroupID.Value));
                fillAccountHeads();
            }
            catch (Exception)
            {
               
            }
        }
        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateVoucher();
                grdVoucher.EditIndex = -1;
                loaddata(Convert.ToInt32(hdnGroupID.Value));

            }
            catch (Exception)
            {
                
            }

        }
        private void UpdateVoucher()
        {
            try
            {
                String ObjectID = "", ChildAccountID = "", DrAmount = "", CrAmount = "";
                for (int i = 0; i < grdVoucher.Rows.Count; i++)
                {
                    if (ObjectID == "")
                        ObjectID = ((Label)grdVoucher.Rows[i].FindControl("ObjectID")).Text;
                    else
                        ObjectID = ObjectID + "|" + ((Label)grdVoucher.Rows[i].FindControl("ObjectID")).Text;

                    if (((DropDownList)grdVoucher.Rows[i].FindControl("ddlChildHead")).Visible == true)
                    {
                        if (ChildAccountID == "")
                            ChildAccountID = ((DropDownList)grdVoucher.Rows[i].FindControl("ddlChildHead")).SelectedValue;
                        else
                            ChildAccountID = ChildAccountID + "|" + ((DropDownList)grdVoucher.Rows[i].FindControl("ddlChildHead")).SelectedValue;
                    }
                    else
                    {
                        if (ChildAccountID == "")
                            ChildAccountID = "0";
                        else
                            ChildAccountID = ChildAccountID + "|" + "0";
                    }

                    if (DrAmount == "")
                        DrAmount = ((TextBox)grdVoucher.Rows[i].FindControl("txtDr")).Text;
                    else
                        DrAmount = DrAmount + "|" + ((TextBox)grdVoucher.Rows[i].FindControl("txtDr")).Text;

                    if (CrAmount == "")
                        CrAmount = ((TextBox)grdVoucher.Rows[i].FindControl("txtCr")).Text;
                    else
                        CrAmount = CrAmount + "|" + ((TextBox)grdVoucher.Rows[i].FindControl("txtCr")).Text;


                }


                if (!String.IsNullOrEmpty(ObjectID))
                {
                    Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
                    OutputMessage Result = null;
                    voucher.ModifiedBy = CPublic.GetuserID();
                    voucher.ID = Convert.ToInt32(hdnGroupID.Value);
                    Result = voucher.UpdateVoucher(ObjectID, ChildAccountID, DrAmount, CrAmount);

                }

            }
            catch (Exception)
            {
                
            }
        }
        protected void grdVoucher_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdVoucher.EditIndex = -1;
                loaddata(Convert.ToInt32(hdnGroupID.Value));
            }
            catch (Exception)
            {

            }
        }
        protected void grdVoucher_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                UpdateHeads();
                grdVoucher.EditIndex = -1;
                loaddata(Convert.ToInt32(hdnGroupID.Value));
            }
            catch (Exception)
            {
                
            }
        }
        private void UpdateHeads()
        {
            try
            {
                Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
                if (hdnIsVoucher.Value == "1")
                {
                    int ObjectID = 0, AccountID = 0;
                    ObjectID = Convert.ToInt32(((Label)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("ObjectID")).Text);
                    AccountID = Convert.ToInt32(((DropDownList)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("ddlAccHead")).SelectedValue);

                    if (ObjectID > 0 && AccountID > 0)
                    {
                        voucher.updatehead(ObjectID, AccountID);
                    }

                }
            }
            catch (Exception)
            {
                
            }
        }
        private void fillAccountHeads()
        {
            try
            {
                ((DropDownList)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("ddlAccHead")).LoadAccountHeads(CPublic.GetCompanyID());
                ((DropDownList)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("ddlAccHead")).SelectedValue = ((Label)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("AccountID")).Text;
                ((DropDownList)grdVoucher.Rows[grdVoucher.EditIndex].FindControl("ddlAccHead")).DataBind();
            }
            catch (Exception)
            {
                
            }

        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
                OutputMessage Result = null;
                Result = voucher.RemoveVoucher(Convert.ToInt32(hdnGroupID.Value));
                if (Result.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                }
            }
            catch (Exception)
            {
                
            }
        }
        protected void cmdUpdateVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateVoucherEntry();
            }
            catch (Exception)
            {
                
            }
        }
        private void UpdateVoucherEntry()
        {
            //if (hdnIsVoucher.Value == "1")
            //{
            try
            {
                Entities.Finance.VoucherEdit voucher = new Entities.Finance.VoucherEdit();
                int GroupID = 0, VoucherID = 0, IsCheque = 0;
                DateTime VoucherDate = DateTime.UtcNow;
                DateTime? ChqDate = null;

                string ChqNumber = "", Narration = "", UserID = "";

                if (string.IsNullOrEmpty(txtNarration.Text))
                {

                }

                if (rblIsCheque.SelectedValue == "1" && (string.IsNullOrEmpty(txtChequeNumber.Text)))
                {

                }




                try
                {
                    GroupID = Convert.ToInt32(hdnGroupID.Value);
                    VoucherID = Convert.ToInt32(ddlVoucherType.SelectedValue);
                    VoucherDate = Convert.ToDateTime(txtVoucherDate.Text);
                    IsCheque = Convert.ToInt32(rblIsCheque.SelectedValue);
                    //CostCenterID = Convert.ToInt32(ddlCostCenterID.SelectedValue);
                    ChqNumber = txtChequeNumber.Text;

                    if (IsCheque == 1)
                        ChqDate = Convert.ToDateTime(txtChequeDate.Text);
                    else
                    {
                        ChqDate = null;
                        ChqNumber = "";
                    }

                    Narration = txtNarration.Text;
                    UserID = CPublic.GetuserID().ToString();

                }
                catch
                {

                }
                OutputMessage Result = null;
                Result = voucher.UpdateVoucherEntry(GroupID, VoucherID, VoucherDate, IsCheque, ChqNumber, ChqDate, Narration, UserID);
                if (Result.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                }
                //}
            }
            catch (Exception)
            {
                
            }

        }
        protected void rblIsCheque_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (rblIsCheque.SelectedValue == "1")
                {

                    txtChequeDate.Enabled = true;
                    txtChequeNumber.Enabled = true;
                }
                else
                {
                    txtChequeDate.Enabled = false;
                    txtChequeNumber.Enabled = false;
                }
            }
            catch (Exception)
            {
                
            }
        }

    }
}