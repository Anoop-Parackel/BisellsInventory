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
    public partial class AccountOpeningBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Entities.Finance.AccountOpeningBalance.LoadAccountHeadsForOpening(ddlAccountHead);
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.AccountOpeningBalance OpeningBalance = new Entities.Finance.AccountOpeningBalance();
                OpeningBalance.AccountHeadID = Convert.ToInt32(ddlAccountHead.SelectedValue);
                OpeningBalance.ChildheadID = Convert.ToInt32(ddlSubAccountHead.SelectedValue);
                OpeningBalance.Balance = Convert.ToDecimal(txtOpeningBalance.Text);
                OpeningBalance.OpeningDate = Convert.ToDateTime(txtOpeningDate.Text);
                OpeningBalance.CreatedBy = CPublic.GetuserID();
                OpeningBalance.SQLtable = hdSqlTable.Value;
                OpeningBalance.ModifiedBy = CPublic.GetuserID();
                OpeningBalance.isDebit = Convert.ToInt32(ddlISDebit.SelectedValue);
                OpeningBalance.ID = Convert.ToInt32(hdItemId.Value);
                try
                {
                    OutputMessage result = new OutputMessage();
                    if (OpeningBalance.ID == 0)
                    {
                        result = OpeningBalance.Save();
                        if (result.Success)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                            reset();

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                        }
                    }
                    else
                    {
                        result = OpeningBalance.Update();
                        if (result.Success)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                            reset();
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                        }
                    }

                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {
                
            }
        }

        protected void ddlAccountHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.AccountOpeningBalance account = new Entities.Finance.AccountOpeningBalance();
                hdSqlTable.Value = account.LoadAccountChildHeads(ddlSubAccountHead, Convert.ToInt32(ddlAccountHead.SelectedValue));
            }
            catch (Exception)
            {
                
            }
        }
        private void reset()
        {
            try
            {
                ddlAccountHead.SelectedValue = "0";
                ddlSubAccountHead.SelectedValue = "0";
                txtOpeningBalance.Text = "";
                txtOpeningDate.Text = "";
                hdItemId.Value = "0";
                hdSqlTable.Value = "";
            }
            catch (Exception)
            {

            }
        }
    }
}