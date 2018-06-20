using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class FinalAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                string GetDate = "";
                GetDate = fin.GetFinancialYear();
                txtFromDate.Text = GetDate;
                txtToDate.Text = DateTime.UtcNow.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnShowPL_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                int reportID = 0;
                hiddenCompanyId.Value = CPublic.GetCompanyID().ToString();
                if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Enter Valid Date');", true);
                }
                else
                {
                    grdLiteral.Text = fin.GetProfitAndLoss(hiddenReportId, reportID, hiddenCompanyId, txtFromDate, txtToDate, ddlCostCenter);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Something Went Wrong. " + ex.Message + "');", true);
            }
        }

        protected void btnBalanceSheet_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                int reportID = 1;
                hiddenCompanyId.Value = CPublic.GetCompanyID().ToString();
                if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Enter Valid Date');", true);
                }
                else
                {
                    grdLiteral.Text = fin.getbalanceSheet(hiddenCompanyId, hiddenReportId, txtFromDate, txtToDate, ddlCostCenter, reportID);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Something Went Wrong. " + ex.Message + "');", true);
            }
        }
    }
}