using Entities.Application;
using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Application
{
    public partial class Role : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    ddlGroup.LoadUserGroups();
                }

        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
              
            }
            catch (FormatException ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('Enter a Valid Role')", true);
            }



        }
        void Reset()
        {
           
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbAsUser.Items.Clear();
            lbUnAsUser.Items.Clear();
            if (ddlGroup.SelectedIndex != 0)
            {
                Entities.Application.UserGroup Group = Entities.Application.UserGroup.GetUsers(Convert.ToInt32(ddlGroup.SelectedValue));
                foreach (Entities.Application.User item in Group.AssisgnedUsers)
                {
                    lbAsUser.Items.Add(new ListItem(item.UserName, item.ID.ToString()));
                }
                foreach (Entities.Application.User item in Group.UnAssisgnedUsers)
                {
                    lbUnAsUser.Items.Add(new ListItem(item.UserName, item.ID.ToString()));
                }

            }
          
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
          if (lbUnAsUser.SelectedItem !=null)
            {
                lbAsUser.Items.Add(lbUnAsUser.SelectedItem);
                lbUnAsUser.Items.Remove(lbUnAsUser.SelectedItem);
            }
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            if (lbAsUser.SelectedItem != null)
            {
                lbUnAsUser.Items.Add(lbAsUser.SelectedItem);
                lbAsUser.Items.Remove(lbAsUser.SelectedItem);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                OutputMessage result = null;
               
                List<Entities.Application.User> users = new List<Entities.Application.User>();
                foreach (ListItem item in lbAsUser.Items)
                {
                    Entities.Application.User user = new Entities.Application.User();
                    user.ID = Convert.ToInt32(item.Value);
                    users.Add(user);
                }
                int Group = Convert.ToInt32(ddlGroup.SelectedValue);
               //result= new UserGroup(Group, CPublic.GetuserID()).SaveUserGroup(users);
                if (result.Success)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "');", true);
                }

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + ex.Message + "');", true);
            }
        }
    }
}