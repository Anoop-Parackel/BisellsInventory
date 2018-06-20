using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Tools.Indent
{
    public partial class Build : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSupplier.LoadSupplier(Publics.CPublic.GetCompanyID());
                ddlLocation.LoadLocations(Publics.CPublic.GetCompanyID());
            }
        }
    }
}