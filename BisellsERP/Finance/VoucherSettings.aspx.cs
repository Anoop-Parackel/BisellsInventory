using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class VoucherSettings : System.Web.UI.Page
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
                    ddlVoucherType.LoadVoucherTypes(CPublic.GetCompanyID());
                    bindgrid();
                }
                catch (Exception)
                {
                    
                }
            }
        }
        private void bindgrid()
        {
            try
            {
                Entities.Finance.VoucherSettings Voucher = new Entities.Finance.VoucherSettings();
                DataSet ds = new DataSet();
                ds = Voucher.GetVoucherSetings(Convert.ToInt32(CPublic.GetCompanyID()), Convert.ToInt32(ddlVoucherType.SelectedValue), Convert.ToInt32(ddlgroupBy.SelectedValue));
                gvAccounts.DataSource = ds;
                gvAccounts.DataBind();
            }
            catch (Exception)
            {
                
            }
        }

        protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindgrid();
            }
            catch (Exception)
            {
                
            }
        }

        protected void ddlgroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bindgrid();
            }
            catch (Exception)
            {
                
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherSettings Voucher = new Entities.Finance.VoucherSettings();
                string ByGroup = "", TypeId = "", HeadID = "", AllowCr = "", AllowDr = "", CrPartial = "", DrPartial = "";
                string chkSelectDrIndex = "", chkSelectCrIndex = "", partialDr = "", partialCr = "";
                TypeId = ddlVoucherType.SelectedValue;
                ByGroup = ddlgroupBy.SelectedValue;
                foreach (GridViewRow dgi in this.gvAccounts.Rows)
                {
                    if (HeadID == "")
                        HeadID = Convert.ToString(((Label)dgi.FindControl("lblId")).Text);
                    else
                        HeadID = HeadID + "|" + Convert.ToString(((Label)dgi.FindControl("lblId")).Text);

                    if (((HtmlInputCheckBox)dgi.FindControl("chkSelectDr")).Checked)
                        chkSelectDrIndex = "1";
                    //chkSelectDrIndex = chkSelectDrIndex + "," + ((HtmlInputCheckBox)dgi.FindControl("chkSelectDr")).Value;
                    else
                        chkSelectDrIndex = "0";

                    if (((HtmlInputCheckBox)dgi.FindControl("chkSelectCr")).Checked)
                        chkSelectCrIndex = "1";
                    //chkSelectCrIndex = chkSelectCrIndex + "," + ((HtmlInputCheckBox)dgi.FindControl("chkSelectCr")).Value;
                    else
                        chkSelectCrIndex = "0";

                    if (AllowCr == "")
                        AllowCr = chkSelectCrIndex;
                    else
                        AllowCr = AllowCr + "|" + chkSelectCrIndex;

                    if (AllowDr == "")
                        AllowDr = chkSelectDrIndex;
                    else
                        AllowDr = AllowDr + "|" + chkSelectDrIndex;

                    if (dgi.Cells[2].BackColor == System.Drawing.Color.DimGray && ((HtmlInputCheckBox)dgi.FindControl("chkSelectDr")).Checked == true)
                        partialDr = "1";
                    else
                        partialDr = "0";

                    if (dgi.Cells[3].BackColor == System.Drawing.Color.DimGray && ((HtmlInputCheckBox)dgi.FindControl("chkSelectCr")).Checked == true)
                        partialCr = "1";
                    else
                        partialCr = "0";

                    if (DrPartial == "")
                        DrPartial = partialDr;
                    else
                        DrPartial = DrPartial + "|" + partialDr;

                    if (CrPartial == "")
                        CrPartial = partialCr;
                    else
                        CrPartial = CrPartial + "|" + partialCr;
                }
                OutputMessage Result = null;
                Voucher.CreatedBy = CPublic.GetuserID();
                if (Voucher.ID == 0)
                {
                    Result = Voucher.Save(ByGroup, TypeId, HeadID, AllowDr, AllowCr, CrPartial, DrPartial);
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
                    Response.Write("<script>alert('')</script>");
                }
            }
            catch (Exception)
            {
                
            }

        }
        public void Reset()
        {
            try
            {
                ddlgroupBy.SelectedValue = "0";
            }
            catch (Exception)
            {
                
            }
        }
        protected void gvAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int TotalHead = 0, DrHead = 0, CrHead = 0;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string isDebit = "", isCredit = "";
                    isDebit = ((Label)e.Row.FindControl("lblDr")).Text;
                    isCredit = ((Label)e.Row.FindControl("lblCr")).Text;
                    if (isDebit == "True" || isDebit == "1")
                    {
                        HtmlInputCheckBox cbDr = (HtmlInputCheckBox)(e.Row.FindControl("chkSelectDr"));
                        cbDr.Checked = true;
                    }
                    if (isCredit == "True" || isCredit == "1")
                    {
                        HtmlInputCheckBox cbCr = (HtmlInputCheckBox)(e.Row.FindControl("chkSelectCr"));
                        cbCr.Checked = true;
                    }
                    if (((Label)e.Row.FindControl("lblTotalHead")).Text != "")
                        TotalHead = Convert.ToInt32(((Label)e.Row.FindControl("lblTotalHead")).Text);
                    if (((Label)e.Row.FindControl("lblDrHead")).Text != "")
                        DrHead = Convert.ToInt32(((Label)e.Row.FindControl("lblDrHead")).Text);
                    if (((Label)e.Row.FindControl("lblCrHead")).Text != "")
                        CrHead = Convert.ToInt32(((Label)e.Row.FindControl("lblCrHead")).Text);

                    if (TotalHead > DrHead && DrHead > 0)
                    {
                        e.Row.Cells[2].BackColor = System.Drawing.Color.DimGray;
                    }
                    if (TotalHead > CrHead && CrHead > 0)
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.DimGray;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}