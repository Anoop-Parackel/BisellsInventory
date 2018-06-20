
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Application
{
    public partial class Usergroup : System.Web.UI.Page
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
                Entities.Application.UserGroup group = new Entities.Application.UserGroup();
                group.ID = Convert.ToInt32(hdUserId.Value);
                group.Name = txtGrpName.Text.Trim();
                group.Description = txtDesc.Text.Trim();
                

                OutputMessage result = null;
                if (group.ID == 0)
                {
                    result = group.Save();
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
            catch (FormatException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid Order')", true);
            }



        }
        void Reset()
        {
            txtGrpName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            hdUserId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}