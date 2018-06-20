using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales
{
    public partial class Return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                ddlCustomer.LoadCustomer(Publics.CPublic.GetCompanyID());
                ddlCostCenter.LoadCost(Publics.CPublic.GetCompanyID());
                hdTandC.Value = Entities.Application.Settings.GetSetting(117);
                ddlCustomerFilter.LoadCustomer(Publics.CPublic.GetCompanyID());
                ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
                lblOrderNo.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "SRT");
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