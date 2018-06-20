using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.Security;
using Entities;

namespace BisellsERP
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)

            {
                hdApiUrl.Value = WebConfigurationManager.AppSettings["APIURL"].ToString();
                dynamic UserDetails;
                try
                {
                    if (this.Context.Session["UserDetails"] == null)
                    {
                        int UserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["bsl_3"].Value);
                        this.Context.Session["UserDetails"] = (object)Entities.Application.User.GetUserDetails(UserId);
                        UserDetails = (dynamic)this.Context.Session["UserDetails"];
                    }
                    else
                    {
                        UserDetails = (dynamic)Context.Session["UserDetails"];
                    }


                    //ltrlSideMenuMst.Text = new Entities.Application.User(Convert.ToInt32(UserDetails.UserId)).BuildMenuMarkup();
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(UserDetails.Photo)))
                    {

                    UserProfileImageMSt.ImageUrl = UserDetails.Photo;
                    }
                    LocationMst.InnerText = (string)UserDetails.Location;
                    txtUserNameMst.InnerHtml = (string)UserDetails.UserName;
                    txtDesignationMst.InnerHtml = (string)UserDetails.Designation;
                }
                catch (NullReferenceException ex)
                {
                    Response.Redirect("/Anonymous/Login");
                }
                catch (Exception ex)
                {
                    Response.Redirect("/Anonymous/Login");
                }
            }
            //Authorize User to view the current page according to permission set on DB
            try
            {

                if (!Entities.Security.Permissions.AuthorizePage(Publics.CPublic.GetuserID(), this.Context.Request.Url.AbsolutePath.Replace(".aspx", string.Empty)))
                {
                    Response.Redirect("/Error/NotAuthorized.html");

                }

            }
            catch (Exception ex)
            {
                Response.Redirect("/Error/NotAuthorized.html?" + ex.Message);

            }
            //Get Application settings
            hdSettings.Value = Entities.Application.Settings.GetFeaturedSettingsSerialized();

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Logout();

        }
        void Logout()
        {
            try
            {
                HttpCookie AuthCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                AuthCookie.Expires = DateTime.Now.AddYears(-1);
                HttpCookie ASPNET_SessionId = new HttpCookie("ASP.NET_SessionId", "");
                ASPNET_SessionId.Expires = DateTime.Now.AddYears(-1);
                HttpCookie bsl_1 = new HttpCookie("bsl_1", "");
                bsl_1.Expires = DateTime.Now.AddYears(-1);
                HttpCookie bsl_2 = new HttpCookie("bsl_2", "");
                bsl_2.Expires = DateTime.Now.AddYears(-1);
                HttpCookie bsl_3 = new HttpCookie("bsl_3", "");
                bsl_3.Expires = DateTime.Now.AddYears(-1);
                HttpCookie bsl_4 = new HttpCookie("bsl_4", "");
                bsl_4.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(AuthCookie);
                Response.Cookies.Add(ASPNET_SessionId);
                Response.Cookies.Add(bsl_1);
                Response.Cookies.Add(bsl_2);
                Response.Cookies.Add(bsl_3);
                Response.Cookies.Add(bsl_4);
                if (this.Context.Session["UserDetails"] != null)
                {
                    this.Context.Session["UserDetails"] = null;
                    this.Context.Session.Clear();
                    this.Context.Session.RemoveAll();
                    Session.Abandon();
                }
                else
                {

                }
                FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

        }

        protected void btnLogout2_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}