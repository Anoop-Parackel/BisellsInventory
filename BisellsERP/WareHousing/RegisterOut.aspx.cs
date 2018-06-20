using BisellsERP.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.WareHousing
{
    public partial class RegisterOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            lblOrderNo.Text = Entities.Register.Register.GetOrderNo(Publics.CPublic.GetCompanyID(), Publics.CPublic.GetFinYear(), "ROT");
            ddlCostCenter.LoadCost(Publics.CPublic.GetCompanyID());
            ddlJob.LoadJobs(Publics.CPublic.GetCompanyID());
            ddlCustomer.LoadCustomer(Publics.CPublic.GetCompanyID());
            ddlEmployee.LoadEmployee(Publics.CPublic.GetCompanyID());
        }
    }
}