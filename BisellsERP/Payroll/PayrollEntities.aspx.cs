using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Payroll
{
    public partial class Payrolls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["mode"]!=null&&Request.QueryString["Section"]!=null)
                {
                    hdnID.Value = Request.QueryString["id"].ToUpper();
                    hdnMode.Value = Request.QueryString["mode"].ToUpper();
                    hdnSectionName.Value = Request.QueryString["Section"].ToUpper();
                }
            }
            else if (Request.QueryString["Section"] != null)
            {
                hdnID.Value = "0";
                hdnMode.Value = "New";
                hdnSectionName.Value = Request.QueryString["Section"].ToUpper();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Invalid Parameters')");
            }

        }
    }
}