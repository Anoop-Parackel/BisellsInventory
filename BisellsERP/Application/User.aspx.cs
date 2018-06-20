using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Application
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlEmpName.LoadEmployee(CPublic.GetCompanyID());
                ddlLoc.LoadLocations(CPublic.GetCompanyID());
            }

        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Application.User user = new Entities.Application.User();
                user.ID = Convert.ToInt32(hdUserId.Value);
                user.UserName = txtUName.Text.Trim();
                user.Password = txtPassword.Text.Trim();
                user.FullName = txtFullName.Text.Trim();                
                user.EmployeeId = ddlEmpName.SelectedValue;
                user.ExpiryPeriod = Convert.ToInt32(ddlDays.SelectedValue);
                user.ForcePasswordChange = chkExpiry.Checked;
                user.CreatedBy = CPublic.GetuserID();
                user.Disable = chkDisable.Checked;
                user.LocationId = Convert.ToInt32(ddlLoc.SelectedValue);
                user.ModifiedBy = CPublic.GetuserID();
                user.CompanyId= CPublic.GetCompanyID();
                OutputMessage result = null;
               
            
            if (user.ID == 0)
                {
                    result = user.Save();
                    if (txtConfirmPass.Text != txtPassword.Text)
                    {
                        result = user.Authenticate();
                        if (result.Success)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "');$('#add-item-portlet').addClass('in');", true);
                        }

                        else
                        {
                            result = user.Authenticate();
                            ClientScript.RegisterStartupScript(GetType(), "message", "successAlert('" + result.Message + "');", true);
                        }
                    }
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "');$('#add-item-portlet').addClass('in');", true);
                    }
                }
                else
                {
                    result = user.Update();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "');$('#add-item-portlet').addClass('in');", true);
                    }
                }
            }
            catch (FormatException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid Order');$('#add-item-portlet').addClass('in');", true);
            }



        }
        void Reset()
        {
            txtUName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtFullName.Text = string.Empty;
            ddlLoc.SelectedIndex = 0;
            ddlEmpName.SelectedIndex = 0;
            txtConfirmPass.Text = string.Empty;
            chkDisable.Text = string.Empty;
            chkExpiry.Text = string.Empty;
            hdUserId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }


    }
}