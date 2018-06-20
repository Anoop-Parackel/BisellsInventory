using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Print.VAT.B
{
    public partial class Entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"]!=null)
                {

                    Entities.Register.PurchaseEntryRegister pe = new Entities.Register.PurchaseEntryRegister();
                    pe = Entities.Register.PurchaseEntryRegister.GetDetails(id,locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblSupName.Text = pe.Supplier;
                    lblComp.Text = pe.Company;
                    lblCompAddr1.Text = c.Address1;
                    lblCompAddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompEmail.Text = pe.CompanyEmail;
                    lblCompGst.Text = c.RegId1;
                    lblSupPhone.Text = pe.BillingAddress[0].Phone1;
                    lblLocName.Text = pe.Location;
                    lblLocAddr1.Text = pe.LocationAddress1;
                    lblLocAddr2.Text = pe.LocationAddress2;
                    lblLocPhone.Text = pe.LocationPhone;
                    lblDate.Text = pe.EntryDateString;
                    lblInvoiceNo.Text = pe.EntryNo;
                    lblDiscount.Text = Convert.ToString(pe.Discount);
                    lblTax.Text = Convert.ToString(pe.TaxAmount);
                    lblGross.Text = Convert.ToString(pe.Gross);
                    lblroundOff.Text = Convert.ToString(pe.RoundOff);
                    lblNet.Text = Convert.ToString(pe.NetAmount);
                    tAndC.Text = Entities.Application.Settings.GetSetting(119);
                    for (int i = 0; i < pe.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = pe.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = pe.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = pe.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = pe.Products[i].MRP.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = pe.Products[i].CostPrice.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = pe.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = pe.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = pe.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = pe.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
                        listTable.Rows.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "entry | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }

        }
    }
}