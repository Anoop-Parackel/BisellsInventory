using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Payroll
{
    public partial class HourlyTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {

                Entities.Payroll.HourlyTemplate hour = new Entities.Payroll.HourlyTemplate();
                hour.ID = Convert.ToInt32(hdHourId.Value);
                hour.Title = txtTitle.Text.Trim();
                hour.Rate =Convert.ToDecimal(txtRate.Text.Trim());
                hour.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                hour.CompanyId = CPublic.GetCompanyID();
                hour.CreatedBy = CPublic.GetuserID();
                hour.ModifiedBy = CPublic.GetuserID();

                Entities.OutputMessage result = null;
                if (hour.ID == 0)
                {
                    result = hour.Save();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "')", true);
                    }
                }
                else
                {
                    result = hour.Update();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + ex.Message + "')", true);


            }

        }
        void Reset()
        {
            txtTitle.Text = string.Empty;
            txtRate.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdHourId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}