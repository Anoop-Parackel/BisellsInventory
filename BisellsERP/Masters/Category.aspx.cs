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
    public partial class Category : System.Web.UI.Page
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
                Entities.Category Cat = new Entities.Category();
                Cat.ID = Convert.ToInt32(hdItemId.Value);
                Cat.Name = txtCategoryName.Text.Trim();
                Cat.Order = Convert.ToInt32(txtCategoryOrder.Text);
                Cat.Status = Convert.ToInt32(ddlCategoryStatus.SelectedValue);
                Cat.CompanyId = CPublic.GetCompanyID();
                Cat.CreatedBy = CPublic.GetuserID();
                Cat.ModifiedBy = CPublic.GetuserID();


                OutputMessage result = null;

                if (Cat.ID == 0)
                {
                    result = Cat.Save();
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
                    result = Cat.Update();
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
            catch(FormatException)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid Order')", true);
            }

        }
        void Reset()
        {
            txtCategoryName.Text = string.Empty;
            txtCategoryOrder.Text = string.Empty;
            ddlCategoryStatus.SelectedIndex = 0;
            hdItemId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);

        }
    }
}