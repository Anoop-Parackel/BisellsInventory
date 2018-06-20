using BisellsERP.Helper;
using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Master;
using BisellsERP.Publics;

namespace BisellsERP.Payroll
{
    public partial class Departments : System.Web.UI.Page
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
                Entities.Payroll.Department dept = new Entities.Payroll.Department();
               dept.ID = Convert.ToInt32(hdDeptId.Value);
               dept.Name = txtDept.Text.Trim();
               dept.Status = Convert.ToInt32(ddlStatus.SelectedValue);
               dept.CompanyId = CPublic.GetCompanyID();
                dept.CreatedBy = CPublic.GetuserID();
                dept.ModifiedBy = CPublic.GetuserID();
            
                OutputMessage result = null;
                if (dept.ID == 0)
                {
                    result = dept.Save();
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
                    result = dept.Update();
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
            txtDept.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            hdDeptId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }
    }
    }
