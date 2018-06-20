using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class DailyStatement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void grdCash_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                if (e.Row.Cells[1].Text == "Closing Balance")
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.Font.Bold = true;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }

                if (e.Row.Cells[1].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                    e.Row.Font.Bold = true;

                }


            }
            catch (Exception ex)
            {

            }
        }
        protected void grdDay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {


                if (e.Row.Cells[1].Text == "Closing Balance")
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    e.Row.Font.Bold = true;
                }

                if (e.Row.Cells[1].Text == "Total")
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    e.Row.Font.Bold = true;

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                if (Convert.ToDateTime(txtFromDate.Text)>Convert.ToDateTime(txtToDate.Text))
                {
                    string strSaveScript = "<script language='javascript' type='text/javascript'>errorAlert('From date is greater than to date')</script>";
                    ClientScript.RegisterStartupScript(typeof(string), "SaveSCript", strSaveScript);
                }
                else
                {
                    fin.FromDate = Convert.ToDateTime(txtFromDate.Text);
                    fin.ToDate = Convert.ToDateTime(txtToDate.Text);
                    grdCash.DataSource = fin.ShowDailyStatement();
                    grdCash.DataBind();
                    grdCash.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string strSaveScript = "<script language='javascript' type='text/javascript'>errorAlert('Something Went wrong')</script>";
                ClientScript.RegisterStartupScript(typeof(string), "SaveSCript", strSaveScript);
            }
        }
    }
}