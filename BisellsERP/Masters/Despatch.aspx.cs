using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Publics;

namespace BisellsERP.Masters
{
    public partial class Despatch : System.Web.UI.Page
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
                Entities.Master.Despatch despatch = new Entities.Master.Despatch();
                despatch.ID = Convert.ToInt32(hdDespatchId.Value);
                despatch.Name = txtName.Text.Trim();
                despatch.Address = txtAddress.Text.Trim();
                despatch.PhoneNo = txtPhone.Text.Trim();
                despatch.MobileNo = txtMobile.Text.Trim();
                despatch.ContactPerson = txtContactPerson.Text.Trim();
                despatch.ContactPersonPhone = txtContactPersonPhone.Text.Trim();
                despatch.Narration = txtNarration.Text.Trim();
                despatch.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                despatch.CompanyId = CPublic.GetCompanyID();
                despatch.CreatedBy = CPublic.GetuserID();
                despatch.ModifiedBy = CPublic.GetuserID();
                OutputMessage result = null;
                if (despatch.ID == 0)
                {
                    result = despatch.Save();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "')", true);
                    }
                }
                else
                {
                    
                    result = despatch.Update();
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
            catch (FormatException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid despatch')", true);
            }
               
            

        }
        void Reset()
        {
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtContactPersonPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtNarration.Text = string.Empty;
            txtPhone.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdDespatchId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}