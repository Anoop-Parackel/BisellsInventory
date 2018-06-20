using BisellsERP.Helper;
using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Publics;
namespace BisellsERP.Payroll
{
    public partial class Designation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlDept.LoadDepartments(CPublic.GetCompanyID());

            }

        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {

                Entities.Payroll.Designation desig = new Entities.Payroll.Designation();
            desig.ID = Convert.ToInt32(hdDesigId.Value);
            desig.Name = txtDesignation.Text.Trim();
            desig.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            desig.DepartmentId = Convert.ToInt32(ddlDept.SelectedValue);
            desig.CompanyId = CPublic.GetCompanyID();
                desig.CreatedBy = CPublic.GetuserID();
                desig.ModifiedBy = CPublic.GetuserID();
                OutputMessage result = null;
                if (desig.ID == 0)
                {
                    result = desig.Save();
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
                    result = desig.Update();
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
            txtDesignation.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            hdDesigId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
}