using Entities.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Publics;
using BisellsERP.Helper;

namespace BisellsERP.Purchase
{
    public partial class request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
       
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ddlCostCenter.LoadCost(CPublic.GetCompanyID());
            ddlJob.LoadJobs(CPublic.GetCompanyID());
            lblOrderNo.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "PRQ");
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