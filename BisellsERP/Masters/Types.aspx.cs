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
    public partial class Types : System.Web.UI.Page
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
                ProductType proType = new ProductType();
            proType.ID = Convert.ToInt32(hdItemId.Value);
            proType.Name = txtTypeName.Text.Trim();
            proType.Order = Convert.ToInt32(txtTypeOrder.Text.Trim());
            proType.Status = Convert.ToInt32(ddlTypeStatus.SelectedValue);
            proType.companyId = CPublic.GetCompanyID();
                proType.CreatedBy = CPublic.GetuserID();
                proType.Modifiedby = CPublic.GetuserID();

           
                OutputMessage result = null;
                if(proType.ID==0)
                {
                    result = proType.Save();
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
                    result = proType.Update();
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

            txtTypeName.Text = string.Empty;
            txtTypeOrder.Text = string.Empty;
            ddlTypeStatus.SelectedIndex = 0;
            hdItemId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);

        }
    }
}