using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Anonymous
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = Request.QueryString["user"] != null ? Request.QueryString["user"].ToString() : string.Empty;
            if (userName != string.Empty)
            {
                txtUserName.Text = userName;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Entities.Application.User usr = new Entities.Application.User();
            usr.UserName = txtUserName.Text;
            usr.Password = txtPassword.Text;
            Entities.OutputMessage result = null;
            result = usr.Authenticate();
            if (result.Success)
            {
                Context.Session["UserDetails"] = result.Object;
                dynamic UserDetails = (dynamic)result.Object;
                HttpCookie bsl1 = new HttpCookie("bsl_1");
                bsl1.Value = UserDetails.CompanyId;
                bsl1.Expires = DateTime.Now.AddDays(7);
                HttpCookie bsl2 = new HttpCookie("bsl_2");
                bsl2.Value = UserDetails.LocationId;
                bsl2.Expires = DateTime.Now.AddDays(7);
                HttpCookie bsl3 = new HttpCookie("bsl_3");
                bsl3.Value = UserDetails.UserId;
                bsl3.Expires = DateTime.Now.AddDays(7);
                HttpCookie bsl4 = new HttpCookie("bsl_4");
                bsl4.Value = UserDetails.FinYear;
                bsl4.Expires = DateTime.Now.AddDays(7);
                Context.Response.Cookies.Add(bsl1);
                Context.Response.Cookies.Add(bsl2);
                Context.Response.Cookies.Add(bsl3);
                Context.Response.Cookies.Add(bsl4);
                FormsAuthentication.RedirectFromLoginPage(usr.UserName, chkRememberMe.Checked);

            }
            else
            {
                error.Visible = true;
            }

        }
    }
}