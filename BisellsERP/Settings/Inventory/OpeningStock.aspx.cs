using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Settings.Inventory
{
    public partial class OpeningStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            lblOrderNo.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "PEN");
            ddlSupplier.LoadSupplier(Publics.CPublic.GetCompanyID());
            ddlSupplierFilter.LoadSupplier(Publics.CPublic.GetCompanyID());

        }
    }
}