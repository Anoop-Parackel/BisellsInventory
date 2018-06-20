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
    public partial class Locations : System.Web.UI.Page
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
                Entities.Location location = new Entities.Location();
            location.ID = Convert.ToInt32(hdLocationId.Value);
            location.Name = txtName.Text.Trim();
            location.Address1 = txtAddress1.Text.Trim();
            location.Address2 = txtAddress2.Text.Trim();
            location.Contact = txtContact.Text.Trim();
            location.ContactPerson = txtContactPerson.Text.Trim();
            location.RegId1 = txtRegId1.Text.Trim();
            location.RegId2 = txtRegId2.Text.Trim();
            location.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            location.CompanyId = CPublic.GetCompanyID();
                location.CreatedBy = CPublic.GetuserID();
                location.ModifiedBy = CPublic.GetuserID();
                OutputMessage result = null;
                if (location.ID == 0)
                {
                    result = location.Save();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('"+result.Message+"');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + result.Message + "')", true);
                    }
                }
                else
                {
                    result = location.Update();
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
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtRegId1.Text = string.Empty;
            txtRegId2.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdLocationId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}