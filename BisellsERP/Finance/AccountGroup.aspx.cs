using BisellsERP.Helper;
using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class AccountGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlParentGroup.LoadAccountGroups(CPublic.GetCompanyID());
                Entities.Finance.AccountGroup account = new Entities.Finance.AccountGroup();
                moduleTree.Nodes.Clear();
                account.BindTree(moduleTree, CPublic.GetCompanyID());
            }
        }
        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.AccountGroup Account = new Entities.Finance.AccountGroup();
                Account.Id = Convert.ToInt32(hdItemId.Value);
                Account.Name = txtAccountGroupName.Text;
                Account.Disable = Convert.ToInt32(ddlStatus.SelectedValue);
                Account.Description = txtDescription.Text;
                Account.Depth = 1;
                Account.ParentId = Convert.ToInt32(ddlParentGroup.SelectedValue);
                Account.Company = CPublic.GetCompanyID();
                Account.AccountType = Convert.ToInt32(ddlAccountNature.SelectedValue);
                Account.CreatedBy = CPublic.GetuserID();
                Account.IsAffectGP = Convert.ToInt32(ddlIsAffectGrossProfit.SelectedValue);
                Account.ModifiedBy = CPublic.GetuserID();
                OutputMessage Result = null;
                if (Account.Id == 0)
                {
                    Result = Account.Save();
                    if (Result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');window.setTimeout(function(){window.location.href = '/Finance/AccountGroup';}, 3000); ", true);
                       
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }
                else
                {
                    Result = Account.Update();
                    if (Result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');window.setTimeout(function(){window.location.href = '/Finance/AccountGroup';}, 3000);", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }

            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "$('#add-item-portlet').addClass('in');errorAlert('Enter a Valid Group')", true);
            }
        }
        private void Reset()
        {
            try
            {
                txtAccountGroupName.Text = "";
                txtDescription.Text = "";
                ddlAccountNature.SelectedValue = "0";
                ddlParentGroup.SelectedValue = "0";
                ddlIsAffectGrossProfit.SelectedValue = "0";
                ddlStatus.SelectedValue = "0";
                hdItemId.Value = "0";
                btnSave.InnerHtml = "Save";
            }
            catch (Exception)
            {

            }
        }
        protected void moduleTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            //Response.Write("<script>alert('"+moduleTree.SelectedValue+"')</script>");
            Entities.Finance.AccountGroup Account = new Entities.Finance.AccountGroup();
            try
            {
                dynamic Accounttree = Entities.Finance.AccountGroup.GetGroupData(Convert.ToInt32(moduleTree.SelectedValue));
                txtAccountGroupName.Text = Accounttree.Name;
                txtDescription.Text = Accounttree.Description;
                hdItemId.Value = Convert.ToString(Accounttree.ID);
                ddlAccountNature.SelectedValue = Convert.ToString(Accounttree.AccountType);
                ddlIsAffectGrossProfit.SelectedValue = Convert.ToString(Accounttree.isAffectGP);
                ddlStatus.SelectedValue = Convert.ToString(Accounttree.IsDisable);
                //ddlIsDebit.SelectedValue = Convert.ToString(Accounttree.IsDebit);
                ddlParentGroup.SelectedValue = Convert.ToString(Accounttree.ParentId);
                //ddlReverseHead.SelectedValue = Convert.ToString(Accounttree.ReverseHeadId);
                //ddlStatus.SelectedValue = Convert.ToString(Accounttree.status);
                //txtDataSQL.Text = Convert.ToString(Accounttree.DataSQL);
                btnSave.InnerHtml="Update";

            }
            catch (Exception)
            {

            }

        }
    }
}