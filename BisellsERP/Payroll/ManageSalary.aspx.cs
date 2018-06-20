using BisellsERP.Helper;
using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Payroll
{
    public partial class ManageSalary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {

            ddlEmp.LoadEmployee(CPublic.GetCompanyID());
        }
    }
}