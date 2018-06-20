using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using Entities.Master;

namespace BisellsERP.Settings
{
    public partial class Preferences : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string querystring=Request.QueryString["SectionType"];
            if (querystring=="invoice")
            {
                hdSection.Value = "invoice";
            }
            else if (querystring == "organizationProfile")
            {
                hdSection.Value = "organizationProfile";
            }
            else if (querystring == "generalsetting")
            {
                hdSection.Value = "generalsetting";
            }
            else if (querystring == "finantialsetting")
            {
                hdSection.Value = "finantialsetting";
            }
            else if (querystring == "emailsetting")
            {
                hdSection.Value = "emailsetting";
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ddlCountryList.LoadCountry(Publics.CPublic.GetCompanyID());
            ddlCustomer.LoadCustomer(Publics.CPublic.GetCompanyID());
        }
    }
}