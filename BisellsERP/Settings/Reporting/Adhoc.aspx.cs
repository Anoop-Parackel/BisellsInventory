﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
namespace BisellsERP.Settings.Reporting
{
    public partial class Adhoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlViews.LoadViews();
            ddlDetailViews.LoadReports();
        }
    }
}