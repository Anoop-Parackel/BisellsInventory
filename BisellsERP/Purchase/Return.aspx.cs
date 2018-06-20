using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase
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
                ddlSupplier.LoadSupplier(Publics.CPublic.GetCompanyID());
                ddlCostCenter.LoadCost(Publics.CPublic.GetCompanyID());
                ddlJob.LoadJobs(Publics.CPublic.GetCompanyID());
                ddlSupplierFilter.LoadSupplier(Publics.CPublic.GetCompanyID());
                ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
                hdTandC.Value = Entities.Application.Settings.GetSetting(120);
                lblOrderNumber.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "PRT");
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