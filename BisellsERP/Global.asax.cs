using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BisellsERP
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            BisellsERP.Application.Configuration.RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            HttpCookie bsl4 = new HttpCookie("bsl_4");
            bsl4.Value = Entities.Application.Helper.FindFinancialYear();
            bsl4.Expires = DateTime.Now.AddDays(7);
            Context.Response.Cookies.Add(bsl4);
        }


    }
}