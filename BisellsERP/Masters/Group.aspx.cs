using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Entities;
using System.Web.UI.WebControls;
using BisellsERP.Publics;

namespace BisellsERP.Masters
{
    public partial class Group : System.Web.UI.Page
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
                Entities.Group group = new Entities.Group();
            group.ID = Convert.ToInt32(hdGroupId.Value);
            group.Name = txtName.Text.Trim();
            group.Order = Convert.ToInt32( txtOrder.Text.Trim());
            group.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            group.CompanyId = CPublic.GetCompanyID();
                group.CreatedBy=CPublic.GetuserID();
                group.ModifiedBy =CPublic.GetuserID();
          
                OutputMessage result = null;
                if (group.ID == 0)
                {
                    result = group.Save();
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
                    result = group.Update();
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
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('" + ex.Message+" ')", true);


            }

        }
        void Reset()
        {
            txtName.Text = string.Empty;
            txtOrder.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdGroupId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}