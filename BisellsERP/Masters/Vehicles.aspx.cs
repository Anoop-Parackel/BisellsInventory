using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Masters
{
    public partial class Vehicles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Master.Vehicle vehicle = new Entities.Master.Vehicle();
                vehicle.ID = Convert.ToInt32(hdVehicleId.Value);
                vehicle.Name = txtName.Text.Trim();
                vehicle.Number = txtNumber.Text.Trim();
                vehicle.Type = txtType.Text.Trim();
                vehicle.Owner = txtOwner.Text.Trim();
                vehicle.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                vehicle.CompanyId = CPublic.GetCompanyID();
                vehicle.CreatedBy = CPublic.GetuserID();
                vehicle.ModifiedBy = CPublic.GetuserID();
                OutputMessage result = null;
                if (vehicle.ID == 0)
                {
                    result = vehicle.Save();
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
                    result = vehicle.Update();
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + ex.Message+"')", true);
            }



        }
        void Reset()
        {
            txtName.Text = string.Empty;
            txtNumber.Text = string.Empty;
            txtType.Text = string.Empty;
            txtOwner.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdVehicleId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}