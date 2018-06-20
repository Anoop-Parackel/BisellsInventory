﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;

namespace BisellsERP.Purchase
{
    public partial class Entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            lblOrderNo.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "PEN");
            ddlSupplier.LoadSupplier(Publics.CPublic.GetCompanyID());
            hdTandC.Value = Entities.Application.Settings.GetSetting(119);
            ddlSupplierFilter.LoadSupplier(Publics.CPublic.GetCompanyID());
            ddlCostCenter.LoadCost(Publics.CPublic.GetCompanyID());
            ddlJob.LoadJobs(Publics.CPublic.GetCompanyID());
            ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
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