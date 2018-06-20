using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;

namespace BisellsERP.Finance
{
    public partial class FinancialTransactions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                    //fin.LoadAccountHeadsTypes(ddlAccountTypes);
                    ddlAccountTypes.LoadAccountHeads(CPublic.GetCompanyID());
                    //ddlVoucherTypes.LoadVoucherTypesFinancial(CPublic.GetCompanyID());
                    ddlVoucherTypes.LoadVoucherTypes(CPublic.GetCompanyID());
                    txtFromDate.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.UtcNow.AddHours(5.5));
                    txtTo.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.UtcNow.AddHours(5.5).AddYears(1));
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}