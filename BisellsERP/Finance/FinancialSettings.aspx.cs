using BisellsERP.Helper;
using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class FinancialSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ddlAccountGroupReport.LoadAccountGroups(CPublic.GetCompanyID());
                ddlParentGroup.LoadAccountGroups(CPublic.GetCompanyID());
                Entities.Finance.AccountGroup account = new Entities.Finance.AccountGroup();
                //moduleTree.Nodes.Clear();
                //account.BindTree(moduleTree, CPublic.GetCompanyID());
            }
        }

        protected void moduleTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            dynamic Accounttree = Entities.Finance.AccountGroup.GetGroupData(Convert.ToInt32(moduleTree.SelectedValue));
            txtAccountGroupName.Text = Accounttree.Name;
            txtAccountGroupDescription.Text = Accounttree.Description;
            hdnGroupID.Value = Convert.ToString(Accounttree.ID);
            ddlAccountNatureGroup.SelectedValue = Convert.ToString(Accounttree.AccountType);
            ddlIsAffectGP.SelectedValue = Convert.ToString(Accounttree.isAffectGP);
            ddlGroupStatus.SelectedValue = Convert.ToString(Accounttree.IsDisable);
            ddlParentGroup.SelectedValue = Convert.ToString(Accounttree.ParentId);
        }
    }
}