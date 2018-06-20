using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Masters
{
    public partial class Inventories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"]!=null)
            {
                if (Request.QueryString["Mode"]!=null&&Request.QueryString["Section"]!=null)
                {
                    hdnID.Value = Request.QueryString["ID"].ToString() ;
                    hdnMode.Value = Request.QueryString["Mode"].ToUpper();
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