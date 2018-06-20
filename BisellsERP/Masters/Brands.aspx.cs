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
    public partial class Brands : System.Web.UI.Page
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
                Entities.Brand brand = new Entities.Brand();
                brand.ID = Convert.ToInt32(hdBrandId.Value);
                brand.Name = txtName.Text.Trim();
                brand.Order = Convert.ToInt32(txtOrder.Text.Trim());
                brand.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                brand.CompanyId = CPublic.GetCompanyID();
                brand.CreatedBy = CPublic.GetuserID();
                brand.ModifiedBy = CPublic.GetuserID();

                OutputMessage result = null;
                if (brand.ID == 0)
                {
                    result = brand.Save();
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
                    
                    result = brand.Update();
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid Order')", true);
            }
               
            

        }
        void Reset()
        {
            txtName.Text = string.Empty;
            txtOrder.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdBrandId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);

        }
    }
}