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
    public partial class Tax : System.Web.UI.Page
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
                Entities.Tax tax = new Entities.Tax();
            tax.ID = Convert.ToInt32(hdTaxId.Value);
            tax.Name = txtTaxName.Text.Trim();
            tax.Percentage = Convert.ToDecimal(txtTaxPercentage.Text);
            tax.Type = txtTaxType.Text.Trim();
            tax.Status = Convert.ToInt32(ddlTaxStatus.SelectedValue);
            tax.CompanyId = CPublic.GetCompanyID();
                tax.CreatedBy = CPublic.GetuserID();
                tax.ModifiedBy = CPublic.GetuserID();
                OutputMessage result = null;
                if(tax.ID==0)
                {
                    result = tax.Save();
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
                    result = tax.Update();
                   if(result.Success)
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + ex.Message+" ')", true);


            }

        }
        void Reset()
        {
           
            txtTaxName.Text = string.Empty;
            txtTaxPercentage.Text = string.Empty;
            txtTaxType.Text = string.Empty;
            ddlTaxStatus.SelectedIndex = 0;
            hdTaxId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);

        }
    }
}