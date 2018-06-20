using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Publics;
using BisellsERP.Helper;

namespace BisellsERP.Sales
{
    public partial class Request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCust.LoadCustomer(CPublic.GetCompanyID());
                ddlLoc.LoadLocations(CPublic.GetCompanyID());
                hdTandC.Value = Entities.Application.Settings.GetSetting(127);
                ddlCostCenter.LoadCost(CPublic.GetCompanyID());
                ddlCustomerFilter.LoadCustomer(CPublic.GetCompanyID());
                ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
                lblOrderNumber.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "SRQ");
                if (Entities.Application.Settings.IsAutoRoundOff())
                {
                    txtRoundOff.Enabled = false;
                }
                else
                {
                    txtRoundOff.Enabled = true;
                }
            }
        }
    }
}