using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase
{
    public partial class ConfirmQuote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            
            ddlLocation.LoadLocations(Publics.CPublic.GetCompanyID());
            ddlSupplier.LoadSupplier(Publics.CPublic.GetCompanyID());
        }
    }
}