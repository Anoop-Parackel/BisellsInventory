using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Print.GST.A
{
    public partial class Return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.Register.PurchaseReturnRegister pr = new Entities.Register.PurchaseReturnRegister();
                    pr = Entities.Register.PurchaseReturnRegister.GetDetails(id,locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblComp.Text = pr.Company;
                    lblCity.Text = c.City;
                    lblCompCountry.Text = c.Country;
                    lblCompState.Text = c.State;
                    lblSupCountry.Text = pr.BillingAddress[0].Country;
                    lblSupState.Text = pr.BillingAddress[0].State;
                    lblCompAddr1.Text = c.Address1;
                    lblCompaddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompRegId.Text = c.RegId1;
                    lblSupName.Text = pr.Supplier;
                    lblSupAddr1.Text = pr.BillingAddress[0].Address1;
                    lblSupAddr2.Text = pr.BillingAddress[0].Address2;
                    lblSupPhone.Text = pr.BillingAddress[0].Phone1;
                    lblSupTaxNo.Text = pr.SupplierTaxNo;
                    lblLocName.Text = pr.Location;
                    lblLocAddr1.Text = pr.LocationAddress1;
                    lblLocAddr2.Text = pr.LocationAddress2;
                    lblLocPhone.Text = pr.LocationPhone;
                    lblLocRegId.Text = pr.LocationRegId;
                    lblInvoiceNo.Text = pr.BillNo;
                    lblTax.Text =Convert.ToString(pr.TaxAmount);
                    lblTotal.Text = Convert.ToString(pr.Gross);
                    lblroundOff.Text = Convert.ToString(pr.RoundOff);
                    lblNet.Text = Convert.ToString(pr.NetAmount);
                    tAndC.Text = Entities.Application.Settings.GetSetting(120);
                    for (int i = 0; i < pr.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = pr.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = pr.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = pr.Products[i].MRP.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = pr.Products[i].CostPrice.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = pr.Products[i].Quantity.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = pr.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = pr.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        if(c.StateId==pr.BillingAddress[0].StateID)
                        {
                            TableCell t9 = new TableCell();
                            t9.Text =(Convert.ToDecimal(pr.Products[i].TaxAmount)/2).ToString();
                            r.Cells.Add(t9);
                            TableCell t10 = new TableCell();
                            t10.Text = (Convert.ToDecimal(pr.Products[i].TaxAmount) / 2).ToString();
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
                            t11.Text = pr.Products[i].TaxAmount.ToString();
                            r.Cells.Add(t11);
                        }
                        TableCell t12 = new TableCell();
                        t12.Text = pr.Products[i].NetAmount.ToString();
                        r.Cells.Add(t12);
                        listTable.Rows.Add(r);
                    }

                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "return | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
        
    }
    }
}