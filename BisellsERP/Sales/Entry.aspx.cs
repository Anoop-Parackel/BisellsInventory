using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;

namespace BisellsERP.Sales
{
    public partial class Entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int companyId = Publics.CPublic.GetCompanyID();
            ddlCustomer.LoadCustomer(companyId);
            ddlCustomerFilter.LoadCustomer(companyId);
            ddlVehicle.LoadVehicles(companyId);
            ddlFreightTax.LoadTaxes(companyId);
            ddlSalesPerson.LoadEmployee(companyId);
            ddlDespatch.LoadDespatch(companyId);
            hdTandC.Value = Entities.Application.Settings.GetSetting(116);
            ddlCostCenter.LoadCost(companyId);
            ddlCountry.LoadCountry(companyId);
            lblOrderNumber.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "SEN");
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