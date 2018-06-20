using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using BisellsERP.Publics;

namespace BisellsERP.Masters
{
    public partial class Suppliers : System.Web.UI.Page
    {
         protected void Page_LoadComplete(object sender, EventArgs e)
        {
           ddlCountry.LoadCountry(CPublic.GetCompanyID());
           ddlCurrency.LoadCurrency(CPublic.GetCompanyID());
        }
     }
}