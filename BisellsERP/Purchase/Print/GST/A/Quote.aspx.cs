using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Print.GST.A
{
    public partial class Quote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             

            try
            {
               int id = Convert.ToInt32(Request.QueryString["id"]);
                    int locationid = Convert.ToInt32(Request.QueryString["location"]);
                    if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                    {
                    Entities.Register.PurchaseQuoteRegister pq = new Entities.Register.PurchaseQuoteRegister();
                    pq = Entities.Register.PurchaseQuoteRegister.GetDetails(id,locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblSupName.Text = pq.Supplier;
                    lblCity.Text = c.City;
                    lblCompState.Text = c.State;
                    lblCompCountry.Text = c.Country;
                    lblSupState.Text = pq.BillingAddress[0].State;
                    lblSupCountry.Text = pq.BillingAddress[0].Country;
                    lblComRegId.Text = c.RegId1;
                    lblLocRegId.Text = pq.LocationRegId;
                    lblSupTaxNo.Text = pq.SupplierTaxNo;
                    lblSupAddr1.Text = pq.BillingAddress[0].Address1;
                    lblSupAddr2.Text = pq.BillingAddress[0].Address2;
                    lblSupPhone.Text = pq.BillingAddress[0].Phone1;
                    lblComp.Text = pq.Company;
                    lblCompaddr2.Text = c.Address2;
                    lblCompAddr1.Text = c.Address1;
                    lblCompPh.Text = c.MobileNo1;
                    lblLocName.Text = pq.Location;
                    lblLocAddr1.Text = pq.LocationAddress1;
                    lblLocAddr2.Text = pq.LocationAddress2;
                    lblLocPhone.Text = pq.LocationPhone;
                    lblDate.Text = pq.EntryDateString;
                    lblInvoiceNo.Text = pq.QuoteNumber;
                    lblTotal.Text = Convert.ToString(pq.Gross);
                    lblTax.Text = Convert.ToString(pq.TaxAmount);
                    lblroundOff.Text = Convert.ToString(pq.RoundOff);
                    lblNet.Text = Convert.ToString(pq.NetAmount);
                    tAndC.Text = Entities.Application.Settings.GetSetting(118);
                    for (int i = 0; i < pq.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = pq.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = pq.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = pq.Products[i].MRP.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = pq.Products[i].CostPrice.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = pq.Products[i].Quantity.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = pq.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = pq.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        if(c.StateId==pq.BillingAddress[0].StateID)
                        {
                            TableCell t9 = new TableCell();
                            t9.Text = (Convert.ToDecimal(pq.Products[i].TaxAmount)/2).ToString();
                            r.Cells.Add(t9);
                            TableCell t10 = new TableCell();
                            t10.Text = (Convert.ToDecimal(pq.Products[i].TaxAmount) / 2).ToString();
                            r.Cells.Add(t10);
                            TableCell t11 = new TableCell();
                            t11.Text = "0.00";
                            r.Cells.Add(t11);
                        }
                        else
                        {
                            TableCell t9 = new TableCell();
                            t9.Text = "0.00";
                            r.Cells.Add(t9);
                            TableCell t10 = new TableCell();
                            t10.Text = "0.00";
                            r.Cells.Add(t10);
                            TableCell t11 = new TableCell();
                            t11.Text = pq.Products[i].TaxAmount.ToString();
                            r.Cells.Add(t11);
                        }
                        TableCell t12 = new TableCell();
                        t12.Text = pq.Products[i].NetAmount.ToString();
                        r.Cells.Add(t12);
                        listTable.Rows.Add(r);
                    }

                }
              }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "quote | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }

        }
    }
}