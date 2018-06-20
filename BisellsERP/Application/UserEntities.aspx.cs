using BisellsERP.Helper;
using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Application
{
    public partial class UserEntities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlEmpolyee.LoadEmployee(CPublic.GetCompanyID());
                ddlUserLocation.LoadLocations(CPublic.GetCompanyID());
                ddlUser.LoadUsers(CPublic.GetCompanyID());
                ddlGroup.LoadUserGroups();

            }

        }
    }
}