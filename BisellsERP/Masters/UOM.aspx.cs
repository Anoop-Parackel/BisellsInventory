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
    public partial class UOM : System.Web.UI.Page
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
                Entities.UOM uom = new Entities.UOM();
                uom.ID = Convert.ToInt32(hdUomId.Value);
                uom.Name = txtName.Text.Trim();
                uom.ShortName = txtShortName.Text.Trim();
                uom.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                uom.CompanyId = CPublic.GetCompanyID();
                uom.CreatedBy = CPublic.GetuserID();
                uom.ModifiedBy = CPublic.GetuserID();

                OutputMessage result = null;
                if (uom.ID == 0)
                {
                    result = uom.Save();
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
                    result = uom.Update();
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
            txtShortName.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdUomId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}