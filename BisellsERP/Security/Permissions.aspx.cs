
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using Entities.Security;
using System.Data;
using System.Dynamic;

namespace BisellsERP.security
{
    public partial class Permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlGroup.LoadUserGroups();
            ddlUser.LoadUsers();
        }

    }
        
}