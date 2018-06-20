using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Masters
{
    public partial class Customer : System.Web.UI.Page
    {
         protected void page_LoadComplete(object sender, EventArgs e)
        {
                ddlCustomerCountry.LoadCountry(CPublic.GetCompanyID());
                ddlCustomerCurrency.LoadCurrency(CPublic.GetCompanyID());
         }
      }
}