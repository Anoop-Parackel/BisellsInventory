
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using Entities.Security;
using System.Data;
using System.Dynamic;

namespace BisellsERP.Application
{
    public partial class PermissionsOld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                
                ddlGroup.LoadUserGroups();
                ddlUser.LoadUsers();
                ddlGroup.Visible = true;
                ddlUser.Visible = false;
                rdGroup.Checked = true;
                GetModuleHierarchy();
            }

        }



        protected void rdUser_CheckedChanged(object sender, EventArgs e)
        {
            ResetCheckBoxes();
            ddlGroup.Visible = false;
            ddlUser.Visible = true;
        }

        protected void rdGroup_CheckedChanged(object sender, EventArgs e)
        {
            ResetCheckBoxes();
            ddlGroup.Visible = true;
            ddlUser.Visible = false;
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlGroup.SelectedIndex==0)
            {
                ResetCheckBoxes();
            }
        }



        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.SelectedIndex == 0)
            {
                ResetCheckBoxes();
            }
        }

        private void GetModuleHierarchy()
        {
            DataSet ds = Modules.GetModules();
            ds.Relations.Add("children", ds.Tables[0].Columns["Module_Id"], ds.Tables[0].Columns["Parent_Id"]);
            foreach (DataRow level1 in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(level1["Parent_Id"].ToString()) || level1["Parent_Id"].ToString() == "0")
                {
                    TreeNode parentTreeNode = new TreeNode();
                    parentTreeNode.Text = "<i class='fa fa-user'></i> "+ level1["Module_Name"].ToString();
                    parentTreeNode.Value = level1["Module_Id"].ToString();

                    GetChildRows(level1, parentTreeNode);
                    moduleTree.Nodes.Add(parentTreeNode);
                }
            }
            moduleTree.CollapseAll();
        }
        private void GetChildRows(DataRow dataRow, TreeNode treeNode)
        {
            DataRow[] childRows = dataRow.GetChildRows("children");
            foreach (DataRow childRow in childRows)
            {
                TreeNode childTreeNode = new TreeNode();
                childTreeNode.Text = childRow["Module_Name"].ToString();
                childTreeNode.Value = childRow["Module_Id"].ToString();
                treeNode.ChildNodes.Add(childTreeNode);
                if (childRow.GetChildRows("children").Length > 0)
                {
                    GetChildRows(childRow, childTreeNode);
                }
            }
        }
        private void ResetCheckBoxes()
        {
            chkAll.Checked = false;
            chkCreate.Checked = false;
            chkDelete.Checked = false;
            chkUpdate.Checked = false;
            chkView.Checked = false;
        }

        protected void moduleTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode treeNode = ((TreeView)sender).SelectedNode;
            if (treeNode.Parent != null)
            {
                //EnableCheckBoxs();
                lblModule.Text = treeNode.Parent.Text + " <i class='fa fa-angle-double-right'></i> " + treeNode.Text;
            }
            else
            {
                lblModule.Text = treeNode.Text;
                //DisableCheckBoxs();
            }

            int moduleId = Convert.ToInt32(treeNode.Value);
            int groupId = Convert.ToInt32(ddlGroup.SelectedValue);
            int userId= Convert.ToInt32(ddlUser.SelectedValue);
            ResetCheckBoxes();
            if (rdGroup.Checked)
            {
                if(ddlGroup.SelectedIndex!=0)
                {
                    dynamic Permission = Entities.Security.Permissions.GetGroupPermission(groupId, moduleId);

                    if (Convert.ToBoolean(Permission.View))
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Create))
                    {
                        chkCreate.Checked = true;
                    }
                    else
                    {
                        chkCreate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Update))
                    {
                        chkUpdate.Checked = true;
                    }
                    else
                    {
                        chkUpdate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Delete))
                    {
                        chkDelete.Checked = true;
                    }
                    else
                    {
                        chkDelete.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.All))
                    {
                        chkAll.Checked = true;
                    }
                    else
                    {
                        chkAll.Checked = false;
                    }
                }
            }
            else
            {
                if(ddlUser.SelectedIndex!=0)
                {
                    dynamic Permission = Entities.Security.Permissions.GetUserPermission(userId, moduleId);

                    if (Convert.ToBoolean(Permission.View))
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Create))
                    {
                        chkCreate.Checked = true;
                    }
                    else
                    {
                        chkCreate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Update))
                    {
                        chkUpdate.Checked = true;
                    }
                    else
                    {
                        chkUpdate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Delete))
                    {
                        chkDelete.Checked = true;
                    }
                    else
                    {
                        chkDelete.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.All))
                    {
                        chkAll.Checked = true;
                    }
                    else
                    {
                        chkAll.Checked = false;
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
                int moduleId = Convert.ToInt32(moduleTree.SelectedNode.Value);
                int groupId = Convert.ToInt32(ddlGroup.SelectedValue);
                int userId = Convert.ToInt32(ddlUser.SelectedValue);
                #region Grabing Permission
                dynamic Permission = new ExpandoObject();
                if (chkView.Checked)
                {
                    Permission.View = true;
                }
                else
                {
                    Permission.View = false;
                }
                if (chkCreate.Checked)
                {
                    Permission.Create = true;
                }
                else
                {
                    Permission.Create = false;
                }
                if (chkUpdate.Checked)
                {
                    Permission.Update = true;
                }
                else
                {
                    Permission.Update = false;
                }
                if (chkDelete.Checked)
                {
                    Permission.Delete = true;
                }
                else
                {
                    Permission.Delete = false;
                }
                if (chkAll.Checked)
                {
                    Permission.All = true;
                }
                else
                {
                    Permission.All = false;
                }
                #endregion

                if (rdGroup.Checked && ddlGroup.SelectedIndex!=0)
                {
                    Entities.Security.Permissions.SetGroupPermission(Permission, moduleId, groupId, Publics.CPublic.GetuserID());

                }
                else
                {
                Entities.Security.Permissions.SetUserPermission(Permission, moduleId, userId, Publics.CPublic.GetuserID());
                }
                //Retrieving saved permission
            ResetCheckBoxes();
            if (rdGroup.Checked)
            {
                if (ddlGroup.SelectedIndex != 0)
                {
                    Permission = Entities.Security.Permissions.GetGroupPermission(groupId, moduleId);

                    if (Convert.ToBoolean(Permission.View))
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Create))
                    {
                        chkCreate.Checked = true;
                    }
                    else
                    {
                        chkCreate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Update))
                    {
                        chkUpdate.Checked = true;
                    }
                    else
                    {
                        chkUpdate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Delete))
                    {
                        chkDelete.Checked = true;
                    }
                    else
                    {
                        chkDelete.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.All))
                    {
                        chkAll.Checked = true;
                    }
                    else
                    {
                        chkAll.Checked = false;
                    }
                }
            }
            else
            {
                if (ddlUser.SelectedIndex != 0)
                {
                    Permission = Entities.Security.Permissions.GetUserPermission(userId, moduleId);

                    if (Convert.ToBoolean(Permission.View))
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Create))
                    {
                        chkCreate.Checked = true;
                    }
                    else
                    {
                        chkCreate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Update))
                    {
                        chkUpdate.Checked = true;
                    }
                    else
                    {
                        chkUpdate.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.Delete))
                    {
                        chkDelete.Checked = true;
                    }
                    else
                    {
                        chkDelete.Checked = false;
                    }
                    if (Convert.ToBoolean(Permission.All))
                    {
                        chkAll.Checked = true;
                    }
                    else
                    {
                        chkAll.Checked = false;
                    }
                }
            }

        }
        void DisableCheckBoxs() {

            chkAll.Enabled = false;
            chkCreate.Enabled = false;
            chkUpdate.Enabled = false;
            chkView.Enabled = false;
            chkDelete.Enabled = false;
        }
        void EnableCheckBoxs()
        {

            chkAll.Enabled = true;
            chkCreate.Enabled = true;
            chkUpdate.Enabled = true;
            chkView.Enabled = true;
            chkDelete.Enabled = true;
        }
    }
}