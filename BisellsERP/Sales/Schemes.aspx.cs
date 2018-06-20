using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Register;
using Entities;
using BisellsERP.Publics;
using System.Data;

namespace BisellsERP.Sales
{
    public partial class Schemes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdItems.DataSource = Scheme.GetItems();
                grdItems.DataBind();
                grdCustomers.DataSource = Scheme.GetCustomers();
                grdCustomers.DataBind();

                lstProductType.DataSource = ProductType.GetProductType(CPublic.GetCompanyID());
                lstProductType.DataTextField = "Name";
                lstProductType.DataValueField = "Type_Id";
                lstProductType.DataBind();

                lstGroup.DataSource = Group.GetGroup(CPublic.GetCompanyID());
                lstGroup.DataTextField = "Name";
                lstGroup.DataValueField = "Group_ID";
                lstGroup.DataBind();

                lstCategory.DataSource = Category.GetCategory(CPublic.GetCompanyID());
                lstCategory.DataTextField = "Name";
                lstCategory.DataValueField = "Category_Id";
                lstCategory.DataBind();

                lstBrand.DataSource = Brand.GetBrand(CPublic.GetCompanyID());
                lstBrand.DataTextField = "Name";
                lstBrand.DataValueField = "Brand_Id";
                lstBrand.DataBind();


               DataTable dt= Scheme.GetCreditMax();
                if (dt.Rows.Count>0)
                {
                    hdCreditDayMax.Value = dt.Rows[0]["credit days"].ToString();
                    hdCreditCashMax.Value = dt.Rows[0]["credit_amount"].ToString();
                }
           

            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Scheme scheme = new Scheme

                {

                    SchemeName = !string.IsNullOrWhiteSpace(txtSchemeName.Text.Trim()) ? Convert.ToString(txtSchemeName.Text.Trim()) : "",
                    StartDate = !string.IsNullOrWhiteSpace(txtFromDate.Text.Trim()) ? Convert.ToDateTime(txtFromDate.Text.Trim()) : DateTime.MinValue,
                    EndDate = !string.IsNullOrWhiteSpace(txtToDate.Text.Trim()) ? Convert.ToDateTime(txtToDate.Text.Trim()) : DateTime.MinValue,
                    AmountOrPercentage = !string.IsNullOrWhiteSpace(txtAmountOrPercentage.Text) ? Convert.ToDecimal(txtAmountOrPercentage.Text) : 0,
                    Quantity = !string.IsNullOrWhiteSpace(txtQuantity.Text) ?Convert.ToDecimal(txtQuantity.Text):0,
                    Mode = !string.IsNullOrWhiteSpace(ddlMode.SelectedValue) ? Convert.ToInt32(ddlMode.SelectedValue):0,
                    Status = !string.IsNullOrWhiteSpace(ddlStatus.SelectedValue) ? Convert.ToInt32(ddlStatus.SelectedValue):0,
                    SchemeType = !string.IsNullOrWhiteSpace(ddlSchemeType.SelectedValue) ? Convert.ToInt32(ddlSchemeType.SelectedValue):0,
                    IsPercentageBased = !string.IsNullOrWhiteSpace(Convert.ToString(chkIsPercent.Checked)) ? Convert.ToBoolean(chkIsPercent.Checked):false

                };
                scheme.LocationId = CPublic.GetLocationID();
                scheme.SchemeId = Convert.ToInt32(hdSchemeId.Value);
                List<Item> items = new List<Item>();

                foreach (ListItem item in lbProducts.Items)
                {

                    items.Add(new Item() { ItemID = Convert.ToInt32(item.Value) });

                }
                scheme.Items = items;
                List<Customer> customers = new List<Customer>();

                foreach (ListItem item in lbCustomer.Items)
                {

                    customers.Add(new Customer() { ID = Convert.ToInt32(item.Value) });

                }
                scheme.Customers = customers;
                OutputMessage result = null;

                if (scheme.SchemeId == 0)
                {
                    result = scheme.Save();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                    }
                }
                else
                {
                    result = scheme.Update();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('Something Went Wrong. Could Not save Scheme');", true);

            }
        }

        void Reset()
        {
            txtAmountOrPercentage.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtSchemeName.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlMode.SelectedIndex = 0;
            ddlSchemeType.SelectedIndex = 0;
            lbCustomer.Items.Clear();
            lbProducts.Items.Clear();
            ddlStatus.SelectedIndex = 0;
            hdSchemeId.Value = "0";
            foreach (GridViewRow item in grdCustomers.Rows)
            {
                ((CheckBox)item.Cells[0].FindControl("chkCustomers")).Checked = false;
            }
            foreach (GridViewRow item in grdItems.Rows)
            {
                ((CheckBox)item.Cells[0].FindControl("chkItems")).Checked = false;
            }
        }

        protected void grdItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grdItems.Rows.Count > 1)
            {
                grdItems.UseAccessibleHeader = true;
                grdItems.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void grdCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (grdCustomers.Rows.Count > 1)
            {
                grdCustomers.UseAccessibleHeader = true;
                grdCustomers.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }

        protected void btnRightProduct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updatePanel, updatePanel.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            for (int i = 0; i < grdItems.Rows.Count; i++)
            {
                bool exists = false;
                for (int j = 0; j < lbProducts.Items.Count; j++)
                {
                    if (grdItems.Rows[i].Cells[1].Text == lbProducts.Items[j].Value)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists && ((CheckBox)grdItems.Rows[i].Cells[0].FindControl("chkItems")).Checked == true)
                {
                    lbProducts.Items.Add(new ListItem(grdItems.Rows[i].Cells[2].Text, grdItems.Rows[i].Cells[1].Text));
  

                }


((CheckBox)grdItems.Rows[i].Cells[0].FindControl("chkItems")).Checked = false;


            }
             
            //Used for trigerring customers into list
            btnCustomerRight_Click(sender, e);

        }

        protected void btnCustomerRight_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updatePanel2, updatePanel2.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            for (int i = 0; i < grdCustomers.Rows.Count; i++)
            {
                bool exists = false;
                for (int j = 0; j < lbCustomer.Items.Count; j++)
                {
                    if (grdCustomers.Rows[i].Cells[1].Text == lbCustomer.Items[j].Value)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists && ((CheckBox)grdCustomers.Rows[i].Cells[0].FindControl("chkCustomers")).Checked == true)
                {
                    lbCustomer.Items.Add(new ListItem(grdCustomers.Rows[i].Cells[2].Text, grdCustomers.Rows[i].Cells[1].Text));
                }



((CheckBox)grdCustomers.Rows[i].Cells[0].FindControl("chkCustomers")).Checked = false;
            }

        }

        protected void btnleftProduct_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updatePanel, updatePanel.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            if (lbProducts.SelectedItem != null)
            {
                lbProducts.Items.Remove(lbProducts.SelectedItem);




                }
            }


      

        protected void btnCustomerleft_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updatePanel2, updatePanel2.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            if (lbCustomer.SelectedItem != null)
            {

                lbCustomer.Items.Remove(lbCustomer.SelectedItem);
            }

        }

        protected void btnCustomerFilter_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updatePanel2, updatePanel2.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            if (!string.IsNullOrWhiteSpace(crLimitMax.Text) && !string.IsNullOrWhiteSpace(crLimitMin.Text) && !string.IsNullOrWhiteSpace(crDaysMax.Text) && !string.IsNullOrWhiteSpace(crDaysMin.Text))
            {
                int CreditLimitMin = Convert.ToInt32(crLimitMax.Text);
                int CreditLimitMax = Convert.ToInt32(crLimitMin.Text);
                int CreditDayMin = Convert.ToInt32(crDaysMax.Text);
                int CreditDayMax = Convert.ToInt32(crDaysMin.Text);
                string keyword = txtSearchCustomer.Text;
               DataTable dt  = Scheme.GetFilteredCustomers(CreditLimitMin, CreditLimitMax, CreditDayMin, CreditDayMax,keyword);
                if (dt.Rows.Count>0)
                {
                    grdCustomers.Visible = true;
                    lblNoitm.Visible = false;
                    grdCustomers.DataSource = dt;
                    grdCustomers.DataBind();

                }
                else
                {
                    grdCustomers.Visible = false;
                    lblNoitm.Visible = true;
                }
            }

            //hdCreditCashSliderMin.Value = crLimitMin.Text;
            //hdCreditCashSliderMax.Value = crLimitMax.Text;
            //hdCreditDaySliderMin.Value = crDaysMin.Text;
            //hdCreditDaySliderMax.Value = crDaysMax.Text;
        }

        protected void btnProductFilter_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(updatePanel2, updatePanel2.GetType(), "listBoxManipulate", "listBoxManipulate()", true);
            List<int> brandlist= new List<int>();
            for (int i = 0; i < lstBrand.Items.Count; i++)
            {
                if (lstBrand.Items[i].Selected)
                {
                    brandlist.Add(Convert.ToInt32(lstBrand.Items[i].Value));
                }
            }
            List<int> Categorylist = new List<int>();
            for (int i = 0; i<lstCategory.Items.Count; i++)
            {
                if (lstCategory.Items[i].Selected)
                {
                    Categorylist.Add(Convert.ToInt32(lstCategory.Items[i].Value));
                }
            }
            List<int> Grouplist = new List<int>();
            for (int i = 0; i<lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Selected)
                {
                    Grouplist.Add(Convert.ToInt32(lstGroup.Items[i].Value));
                }
            }
            List<int> ProductTypelist = new List<int>();
            for (int i = 0; i<lstProductType.Items.Count; i++)
            {
                if (lstProductType.Items[i].Selected)
                {
                    ProductTypelist.Add(Convert.ToInt32(lstProductType.Items[i].Value));

                }
            }
            string key = txtSearchItems.Text;
            DataTable dt= Scheme.GetItems(brandlist, Categorylist, Grouplist, ProductTypelist,key);
            if (dt.Rows.Count>0)
            {
                grdItems.Visible = true;
                lblListItem.Visible = false;
                grdItems.DataSource = dt;
                grdItems.DataBind();
            }
            else
            {
                grdItems.Visible = false;
                lblListItem.Visible = true;
            }
            


        }

     
    }
}
