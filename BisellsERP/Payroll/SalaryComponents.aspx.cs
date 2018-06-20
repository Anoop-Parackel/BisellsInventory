using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Master;
namespace BisellsERP.Payroll
{
    public partial class SalaryComponents : System.Web.UI.Page
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

                Entities.Payroll.SalaryComponent sal = new Entities.Payroll.SalaryComponent();
                sal.ID = Convert.ToInt32(hdSalId.Value);
                sal.Name = txtName.Text.Trim();
                sal.ComponentType = Convert.ToString(ddlCompType.SelectedValue);
                sal.CalculationType = Convert.ToString(ddlCalcType.SelectedValue);
                sal.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                sal.CompanyId = CPublic.GetCompanyID();
                sal.CreatedBy = CPublic.GetuserID();
                sal.ModifiedBy = CPublic.GetuserID();

                Entities.OutputMessage result = null;
                if (sal.ID == 0)
                {
                    result = sal.Save();
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
                    result = sal.Update();
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
            txtName.Text = string.Empty;
            ddlCompType.SelectedIndex = 0;
            ddlCalcType.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            hdSalId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}