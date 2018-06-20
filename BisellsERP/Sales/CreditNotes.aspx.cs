using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales
{
    public partial class CreditNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int companyId = Publics.CPublic.GetCompanyID();
          
            ddlCustomer.LoadCustomer(companyId);
        }
    }
}