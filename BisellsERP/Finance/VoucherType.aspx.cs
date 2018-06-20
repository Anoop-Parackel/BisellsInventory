using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class VoucherType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.VoucherType Voucher = new Entities.Finance.VoucherType();
                Voucher.ID = Convert.ToInt32(hdItemId.Value);
                Voucher.IsDebit = Convert.ToInt32(ddlIsDebit.SelectedValue);
                Voucher.Name = txtVoucherTypeName.Text;
                Voucher.CreatedBy = CPublic.GetuserID();
                Voucher.Disable = Convert.ToInt32(ddlDisable.SelectedValue);
                Voucher.Numbering = Convert.ToInt32(ddlNumbering.SelectedValue);
                Voucher.NumberStartFrom = Convert.ToInt32(txtNumberingStarts.Text);
                Voucher.RestartNumber = Convert.ToInt32(ddlRestartsOn.SelectedValue);
                Voucher.DisplayInJournal = Convert.ToInt32(ddlDisplayInJournal.SelectedValue);
                Voucher.ModifiedBy = CPublic.GetuserID();
                OutputMessage Result = null;
                if (Voucher.ID == 0)
                {
                    Result = Voucher.Save();
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
                    Result = Voucher.Update();
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
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Something Went Wrong')", true);
            }
        }
        void Reset()
        {
            try
            {
                txtVoucherTypeName.Text = "";
                ddlRestartsOn.SelectedValue = "0";
                txtNumberingStarts.Text = "";
                ddlNumbering.SelectedValue = "0";
                ddlDisable.SelectedValue = "1";
                ddlDisplayInJournal.SelectedValue = "1";
                ddlIsDebit.SelectedValue = "1";
                hdItemId.Value = "0";
            }
            catch (Exception)
            {

            }

        }
    }
}