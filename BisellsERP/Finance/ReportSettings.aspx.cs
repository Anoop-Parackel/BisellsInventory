using BisellsERP.Helper;
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
    public partial class ReportSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlAccountGroup.LoadAccountGroups(CPublic.GetCompanyID());
            }
        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.ReportSettings Reports = new Entities.Finance.ReportSettings();
                Reports.ID = Convert.ToInt32(hdItemId.Value);
                Reports.ReportID = Convert.ToInt32(ddlReportType.SelectedValue);
                Reports.AccountGroupID = Convert.ToInt32(ddlAccountGroup.SelectedValue);
                Reports.Order = Convert.ToInt32(txtAccountGroupOrder.Text);
                Reports.Postion = Convert.ToInt32(ddlSide.SelectedValue);
                Reports.isMinus = 1;
                Reports.CreatedBy = CPublic.GetuserID();
                Reports.ModifiedBy = CPublic.GetuserID();
                OutputMessage Result = null;
                if (Reports.ID == 0)
                {
                    Result = Reports.Save();
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
                    Result = Reports.Update();
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Something Went Wrong')", true);
            }
        }
        private void Reset()
        {
            try
            {
                txtAccountGroupOrder.Text = "";
                ddlAccountGroup.SelectedValue = "0";
                ddlReportType.SelectedValue = "0";
                hdItemId.Value = "0";
            }
            catch (Exception)
            {

            }
        }
    }
}