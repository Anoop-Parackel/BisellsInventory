using BisellsERP.Helper;
using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Finance
{
    public partial class GroupedLedger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                Entities.Finance.FinancialTransactions fin = new Entities.Finance.FinancialTransactions();
                dt = fin.GetAccountGroups(CPublic.GetCompanyID());
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem items = new ListItem(item["name"].ToString(), item["id"].ToString());
                        ddlAccountGroups.Items.Add(items);
                    }
                }
                dt = fin.GetAccountHeads(Convert.ToInt32(ddlAccountGroups.SelectedValue));
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ListItem items = new ListItem(item["Fah_Name"].ToString(), item["Fah_ID"].ToString());
                        ddlTransHead.Items.Add(items);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}