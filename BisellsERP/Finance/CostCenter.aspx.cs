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
    public partial class CostCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.CostCenter CostCenter = new Entities.Finance.CostCenter();
                CostCenter.ID = Convert.ToInt32(hdItemId.Value);
                CostCenter.Name = txtCostCenterName.Text;
                CostCenter.Status = Convert.ToInt32(ddlDisabled.SelectedValue);
                CostCenter.CreatedBy = CPublic.GetuserID();
                CostCenter.ModifiedBy = CPublic.GetuserID();
                OutputMessage Result = null;
                if (CostCenter.ID == 0)
                {
                    Result = CostCenter.Save();
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
                    Result = CostCenter.Update();
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
                txtCostCenterName.Text = "";
                ddlDisabled.SelectedValue = "0";
                hdItemId.Value = "0";
            }
            catch (Exception)
            {
            }
        }
    }
}