using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Print.VAT.D
{
    public partial class Indent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (Request.QueryString["id"] != null)
                {

                    Entities.Register.PurchaseIndentRegister pi = new Entities.Register.PurchaseIndentRegister();
                    pi = Entities.Register.PurchaseIndentRegister.GetDetails(id);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + pi.CompanyLogo;
                    lblComp.Text = pi.Company;
                    lblCompEmail.Text = pi.CompanyEmail;
                    lblCompAddr1.Text = pi.CompanyAddress1;
                    lblCompAddr2.Text = pi.CompanyAddress2;
                    lblCompPh.Text = pi.CompanyPhone;
                    lblCompGst.Text = pi.CompanyRegId;
                    lblDate.Text = pi.EntryDateString;
                    lblInvoiceNo.Text = pi.IndentNo;
                    lblTax.Text = Convert.ToString(pi.TaxAmount);
                    lblGross.Text = Convert.ToString(pi.Gross);
                    lblroundOff.Text = Convert.ToString(pi.RoundOff);
                    lblNet.Text = Convert.ToString(pi.NetAmount);
                    tAndC.Text = pi.TermsandConditon;//Entities.Application.Settings.GetSetting(144);
                    for (int i = 0; i < pi.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = pi.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = pi.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = pi.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = pi.Products[i].MRP.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = pi.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = pi.Products[i].CostPrice.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = pi.Products[i].Gross.ToString();
                        r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = pi.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = pi.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
                        listTable.Rows.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "Indent | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
        }
    }
}