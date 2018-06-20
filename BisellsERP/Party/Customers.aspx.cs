using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Party
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int companyId = Publics.CPublic.GetCompanyID();
            ddlCountry.LoadCountry(companyId);
            foreach (ListItem item in ddlCountry.Items)
            {
                ddlSiteCountry.Items.Add(item);
                ddlJobCountry.Items.Add(item);
            }

        }
    }
}