using BisellsERP.Helper;
using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class Ledger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Initilization at Pageload
                    ddlTransHead.LoadAccountHeads(CPublic.GetCompanyID());
                    Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                    DateTime newDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);//From date(First day of the current month)
                    txtFromDate.Text = newDate.ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.UtcNow.ToString("dd-MMM-yyyy");
                    ddlCostCenter.LoadCost(CPublic.GetCompanyID());
                    ddlTransHead.SelectedValue = "6";
                    if (ddlTransHead.SelectedValue != "0")
                    {
                        lblLedgerHead.InnerText = ddlTransHead.SelectedItem.Text;
                    }
                    else
                    {
                        lblLedgerHead.InnerText = "Cash";
                    }
                    lblDate.InnerText = "From " + txtFromDate.Text + " to " + txtToDate.Text;
                    literalGrid.Text = fin.BindGrid(hiddenChildId.Value, ddlChildHead.SelectedValue, ddlTransHead.SelectedValue, txtFromDate.Text, txtToDate.Text, ddlCostCenter.SelectedValue);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        protected void cmdPrint_Click(object sender, EventArgs e)
        {

        }

        protected void cmdBack_Click(object sender, EventArgs e)
        {

        }

        protected void cmdConsoleLedger_Click(object sender, EventArgs e)
        {

        }
    }
}