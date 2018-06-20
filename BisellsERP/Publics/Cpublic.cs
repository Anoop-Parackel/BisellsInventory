using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using System.Web;

namespace BisellsERP.Publics
{
    public class CPublic
    {
        
        /// <summary>
        /// Gets the Location ID from current Session
        /// </summary>
        /// <returns>Location ID</returns>
        public static int GetLocationID()
        {
            dynamic UserDetails;
            if (System.Web.HttpContext.Current.Session["UserDetails"] == null)
            {
                int UserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["bsl_3"].Value);
                System.Web.HttpContext.Current.Session["UserDetails"] = (object)Entities.Application.User.GetUserDetails(UserId);
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            else
            {
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            return Convert.ToInt32(UserDetails.LocationId);
        }
        public static int GetCompanyID()
        {
            dynamic UserDetails;
            if (System.Web.HttpContext.Current.Session["UserDetails"] == null)
            {
                int UserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["bsl_3"].Value);
                System.Web.HttpContext.Current.Session["UserDetails"] = (object)Entities.Application.User.GetUserDetails(UserId);
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            else
            {
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            return Convert.ToInt32(UserDetails.CompanyId);
        }
        public static int GetuserID()
        {
            dynamic UserDetails;
            if (System.Web.HttpContext.Current.Session["UserDetails"] == null)
            {
                int UserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["bsl_3"].Value);
                System.Web.HttpContext.Current.Session["UserDetails"] = (object)Entities.Application.User.GetUserDetails(UserId);
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            else
            {
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            return Convert.ToInt32(UserDetails.UserId);
        }
        public static string GetFinYear()
        {
            dynamic UserDetails;
            if (System.Web.HttpContext.Current.Session["UserDetails"] == null)
            {
                int UserId = Convert.ToInt32(HttpContext.Current.Request.Cookies["bsl_3"].Value);
                System.Web.HttpContext.Current.Session["UserDetails"] = (object)Entities.Application.User.GetUserDetails(UserId);
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            else
            {
                UserDetails = (dynamic)System.Web.HttpContext.Current.Session["UserDetails"];
            }
            return Convert.ToString(UserDetails.FinYear);
        }

    }
}