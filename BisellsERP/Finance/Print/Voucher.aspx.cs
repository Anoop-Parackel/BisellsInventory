using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance.Print
{
    public partial class Voucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hdApiUrl.Value = WebConfigurationManager.AppSettings["APIURL"].ToString();
            Entities.Master.Company c = new Entities.Master.Company();
            c = Entities.Master.Company.GetDetailsByLocation(CPublic.GetLocationID());
            imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;

        }
    }
}