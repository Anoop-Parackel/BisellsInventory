using BisellsERP.Helper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BisellsERP.Publics;

namespace BisellsERP.Masters
{
    public partial class Items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlUOM.LoadUnits(CPublic.GetCompanyID());
                ddlBrand.LoadBrands(CPublic.GetCompanyID());
                ddlCategory.LoadCategories(CPublic.GetCompanyID());
                ddlTax.LoadTaxes(CPublic.GetCompanyID());
                ddlGroup.LoadGroups(CPublic.GetCompanyID());
                ddlType.LoadTypes(CPublic.GetCompanyID());
            }
        }

        protected void btnSaveConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                Product product = new Product();
                product.ItemID = Convert.ToInt32(hdItemId.Value);
                product.Name = txtItemName.Text.Trim();
                product.UnitID = Convert.ToInt32(ddlUOM.SelectedValue);
                product.TaxId = Convert.ToInt32(ddlTax.SelectedValue);
                product.Description = txtDescription.Text.Trim();
                product.ItemCode = txtItemCode.Text.Trim();
                product.OEMCode = txtOEM.Text.Trim();
                product.HSCode = txtHSCode.Text.Trim();
                product.Barcode = txtBarcode.Text.Trim();
                product.TypeID = Convert.ToInt32(ddlType.SelectedValue);
                product.MRP = Convert.ToDecimal(txtMrp.Text);
                product.CostPrice = Convert.ToDecimal(txtCost.Text);
                product.SellingPrice = Convert.ToDecimal(txtSell.Text);
                //product.Remarks = txtRemarks.Text.Trim();
                product.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                product.GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
                product.BrandID = Convert.ToInt32(ddlBrand.SelectedValue);
                product.CompanyId = CPublic.GetCompanyID();
                product.CreatedBy = CPublic.GetuserID();
                product.ModifiedBy = CPublic.GetuserID();
                product.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                OutputMessage result = null;
                if (product.ItemID == 0)
                {
                    result = product.Save();
                   
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                    }
                }
                else
                {
                    result = product.Update();
                    if (result.Success)
                    {
                        Reset();
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "successAlert('" + result.Message + "')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + result.Message + "')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "message", "errorAlert('" + ex.Message + "')", true);


            }

        }
        void Reset()
        {
            txtDescription.Text = string.Empty;
            txtHSCode.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtOEM.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            txtBarcode.Text = string.Empty;
            txtSell.Text = string.Empty;
            txtMrp.Text = string.Empty;
            txtCost.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlBrand.SelectedIndex = 0;
            ddlCategory.SelectedIndex = 0;
            ddlGroup.SelectedIndex = 0;
            ddlTax.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            ddlUOM.SelectedIndex = 0;
            hdItemId.Value = "0";
            ClientScript.RegisterStartupScript(this.GetType(), "btnreset", "$('#btnSave').html('<i class=\"md-add-circle-outline\"></i>&nbsp;Add');", true);
        }
    }
}