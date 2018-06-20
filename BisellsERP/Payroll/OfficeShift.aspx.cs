using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Payroll
{
    public partial class OfficeShift : System.Web.UI.Page
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

                Entities.Payroll.OfficeShift off = new Entities.Payroll.OfficeShift();
                off.ID = Convert.ToInt32(hdId.Value);
                off.Name = txtName.Text.Trim();
                off.MondayInTime = Convert.ToDateTime(txtMonClockIn.Text);
                off.MondayOutTime = Convert.ToDateTime(txtMonClockOut.Text);
                off.TuesdayInTime = Convert.ToDateTime(txtTueClockIn.Text);
                off.TuesdayOutTime = Convert.ToDateTime(txtTueClockOut.Text);
                off.WednesdayInTime = Convert.ToDateTime(txtWedClockIn.Text);
                off.WednesdayOutTime = Convert.ToDateTime(txtWedClockOut.Text);
                off.ThursdayInTime = Convert.ToDateTime(txtThuClockIn.Text);
                off.ThursdayOutTime = Convert.ToDateTime(txtThuClockOut.Text);
                off.FridayInTime = Convert.ToDateTime(txtFriClockIn.Text);
                off.FridayOutTime = Convert.ToDateTime(txtFriClockOut.Text);
                off.SaturdayInTime = Convert.ToDateTime(txtSatClockIn.Text);
                off.SaturdayOuttime = Convert.ToDateTime(txtSatClockOut.Text);
                off.SundayInTime = Convert.ToDateTime(txtSunClockIn.Text);
                off.SundayOutTime = Convert.ToDateTime(txtSunClockOut.Text);
                off.CompanyId = CPublic.GetCompanyID();
                off.CreatedBy = CPublic.GetuserID();
                off.ModifiedBy = CPublic.GetuserID();

                Entities.OutputMessage result = null;
                if (off.ID == 0)
                {
                    result = off.Save();
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
                    result = off.Update();
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
            txtFriClockIn.Text = string.Empty;
            txtFriClockOut.Text = string.Empty;
            txtMonClockIn.Text = string.Empty;
            txtMonClockOut.Text = string.Empty;
            txtSatClockIn.Text = string.Empty;
            txtSatClockOut.Text = string.Empty;
            txtSunClockIn.Text = string.Empty;
            txtSunClockOut.Text = string.Empty;
            txtThuClockIn.Text = string.Empty;
            txtThuClockOut.Text = string.Empty;
            txtTueClockIn.Text = string.Empty;
            txtTueClockOut.Text = string.Empty;
            txtWedClockIn.Text = string.Empty;
            txtWedClockOut.Text = string.Empty;
            hdId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"ion-checkmark-round\"></i>Add');", true);
        }

    }
}