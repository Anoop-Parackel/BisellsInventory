using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;

namespace BisellsERP.Party
{
    public partial class Leads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ddlAssign.LoadEmployee(Publics.CPublic.GetCompanyID());
            ddlCountry.LoadCountry(Publics.CPublic.GetCompanyID());
            ddlEmployee.LoadEmployee(Publics.CPublic.GetCompanyID());
        }
    }
}