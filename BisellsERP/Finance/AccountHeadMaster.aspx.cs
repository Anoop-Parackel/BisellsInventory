using BisellsERP.Publics;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Helper;
using System.Data;

namespace BisellsERP.Finance
{
    public partial class AccountHeadMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlReverseHead.LoadAccountHeads(CPublic.GetCompanyID());
                ddlParentGroup.LoadAccountGroups(CPublic.GetCompanyID());
                FillTree();
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Entities.Finance.AccountHeadMaster Account = new Entities.Finance.AccountHeadMaster();
                Account.ID = Convert.ToInt32(hdItemId.Value);
                Account.Name = txtAccountHeadName.Text;
                Account.status = Convert.ToInt32(ddlStatus.SelectedValue);
                Account.Description = txtDescription.Text;
                Account.IsDebit = Convert.ToInt32(ddlIsDebit.SelectedValue);
                Account.ParentId = 0;
                Account.CreatedBy = CPublic.GetuserID();
                Account.Category = Convert.ToInt32(ddlCategory.SelectedValue);
                Account.DataSQL = txtDataSQL.Text;
                Account.SQLID = txtDataID.Text;
                Account.SQLName = txtDataName.Text;
                Account.SQLTable = txtRefenerenceTable.Text;
                Account.TransactionSQL = txtTransactionSQL.Text;
                Account.AmountSQL = txtAmountSQL.Text;
                Account.Phone = txtPhoneNumber.Text;
                Account.Email = txtEmail.Text;
                if (txtOpeningBalance.Text=="")
                {
                    Account.OpeningBalance =0;
                }
                else
                {
                    Account.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text);
                }
                if (txtOpeningDate.Text=="")
                {
                    Account.OpeningDate = DateTime.UtcNow;
                }
                else
                {
                    Account.OpeningDate = Convert.ToDateTime(txtOpeningDate.Text);
                }
                Account.CompanyID = CPublic.GetCompanyID();
                Account.ModifiedBy = CPublic.GetuserID();
                Account.AccountNature = Convert.ToInt32(ddlAccountNature.SelectedValue);
                Account.AccountType = Convert.ToInt32(ddlAccountType.SelectedValue);
                Account.Address = txtAddress.Text;
                Account.ContactPerson = txtContactPerson.Text;
                Account.AccountGroupId = Convert.ToInt32(ddlParentGroup.SelectedValue);
                Account.ReverseHeadId = Convert.ToInt32(ddlReverseHead.SelectedValue);
                OutputMessage Result = null;
                if (Account.ID == 0)
                {
                    Result = Account.Save();
                    if (Result.Success)
                    {
                        Reset();
                        ddlReverseHead.LoadAccountHeads(CPublic.GetCompanyID());
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');window.setTimeout(function(){window.location.href = '/Finance/AccountHeadMaster';}, 3000);", true);
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
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + Result.Message + "');window.setTimeout(function(){window.location.href = '/Finance/AccountHeadMaster';}, 3000);", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + Result.Message + "')", true);
                    }
                }

            }
            catch (Exception ex)
            {
              
            }
        }
        private void Reset()
        {
            try
            {
                txtAccountHeadName.Text = "";
                txtAddress.Text = "";
                txtContactPerson.Text = "";
                txtDescription.Text = "";
                txtEmail.Text = "";
                txtOpeningBalance.Text = "";
                txtOpeningDate.Text = "";
                txtPhoneNumber.Text = "";
                ddlAccountNature.SelectedValue = "0";
                ddlAccountType.SelectedValue = "0";
                ddlCategory.SelectedValue = "0";
                ddlIsDebit.SelectedValue = "0";
                ddlParentGroup.SelectedValue = "0";
                ddlStatus.SelectedValue = "0";
                ddlReverseHead.SelectedValue = "0";
                txtAmountSQL.Text = "";
                txtDataID.Text = "";
                txtDataSQL.Text = "";
                txtRefenerenceTable.Text = "";
                txtTransactionSQL.Text = "";
                txtDataName.Text = "";
                hdItemId.Value = "0";
                lblID.Text = "";
                btnSave.InnerHtml = "Save";
            }
            catch (Exception ex)
            {
                
            }
        }
        private void FillTree()
        {
            try
            {
                Entities.Finance.AccountHeadMaster account = new Entities.Finance.AccountHeadMaster();
                TreeNode tree = new TreeNode();
                tree = account.GetTree(CPublic.GetCompanyID());
                moduleTree.Nodes.Add(tree);
                moduleTree.CollapseAll();
            }
            catch (Exception)
            {

            }
        }

        protected void moduleTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (moduleTree.SelectedNode.ChildNodes.Count>0)
                {
                    Reset();
                }
                else
                {
                    Entities.Finance.AccountHeadMaster Account = new Entities.Finance.AccountHeadMaster();
                    try
                    {
                        dynamic Accounttree = Entities.Finance.AccountHeadMaster.GetGroupData(Convert.ToInt32(moduleTree.SelectedValue));
                        txtAccountHeadName.Text = Accounttree.Name;
                        txtAddress.Text = Accounttree.Address;
                        txtAmountSQL.Text = Accounttree.AmountSQL;
                        txtContactPerson.Text = Accounttree.ContactPerson;
                        txtDataID.Text = Accounttree.SQLID;
                        txtDataName.Text = Accounttree.SQLName;
                        txtDescription.Text = Accounttree.Description;
                        txtEmail.Text = Accounttree.Email;
                        hdItemId.Value = Convert.ToString(Accounttree.ID);
                        txtPhoneNumber.Text = Accounttree.Phone;
                        txtRefenerenceTable.Text = Accounttree.SQLTable;
                        txtTransactionSQL.Text = Accounttree.TransactionSQL;
                        txtOpeningDate.Text = Convert.ToString(Accounttree.OpeningDate);
                        txtOpeningBalance.Text = Convert.ToString(Accounttree.OpeningBalance);
                        ddlAccountNature.SelectedValue = Convert.ToString(Accounttree.AccountNature);
                        ddlAccountType.SelectedValue = Convert.ToString(Accounttree.AccountType);
                        ddlCategory.SelectedValue = Convert.ToString(Accounttree.Category);
                        ddlIsDebit.SelectedValue = Convert.ToString(Accounttree.IsDebit);
                        ddlParentGroup.SelectedValue = Convert.ToString(Accounttree.AccountGroupId);
                        ddlReverseHead.SelectedValue = Convert.ToString(Accounttree.ReverseHeadId);
                        ddlStatus.SelectedValue = Convert.ToString(Accounttree.status);
                        txtDataSQL.Text = Convert.ToString(Accounttree.DataSQL);
                        lblID.Text = "["+Convert.ToString(Accounttree.ID)+"]";
                        btnSave.InnerHtml = "Update";

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
