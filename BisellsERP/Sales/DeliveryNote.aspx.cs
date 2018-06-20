using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;

namespace BisellsERP.Sales
{
    public partial class DeliveryNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ddlCustomer.LoadCustomer(Publics.CPublic.GetCompanyID());
            ddlCostCenter.LoadCost(Publics.CPublic.GetCompanyID());
            hdTandC.Value = Entities.Application.Settings.GetSetting(151);
            ddlCustomerFilter.LoadCustomer(Publics.CPublic.GetCompanyID());
            lblOrderNumber.Text= Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "DLN");
            if (Entities.Application.Settings.IsAutoRoundOff())
            {
                txtRoundOff.Enabled = false;
            }
            else
            {
                txtRoundOff.Enabled = true;
            }
            ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
        }
    }
}