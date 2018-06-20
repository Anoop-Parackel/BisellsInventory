using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int ReportUID;
            int KeyId;
            if (!string.IsNullOrWhiteSpace(this.Context.Request.QueryString["ReportUID"]) && int.TryParse(this.Context.Request.QueryString["ReportUID"], out ReportUID))
            {
                if (!string.IsNullOrWhiteSpace(this.Context.Request.QueryString["KeyValue"]) && int.TryParse(this.Context.Request.QueryString["KeyValue"], out KeyId))
                {
                    Literal1.Text = new Entities.Reporting.ReportingTool(ReportUID, KeyId).GenerateReport();
                }
                else
                {
                    Literal1.Text = new Entities.Reporting.ReportingTool(ReportUID).GenerateReport();
                }
            }
        }
    }
}