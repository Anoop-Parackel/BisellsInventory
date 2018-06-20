using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.VAT.B
{
    public partial class Entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationId = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {

                    Entities.Register.SalesEntryRegister sr = new Entities.Register.SalesEntryRegister();
                    sr = Entities.Register.SalesEntryRegister.GetDetails(id, locationId);
                    dynamic setting = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(setting.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationId);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblCustName.Text = sr.Customer;
                    lblComp.Text = sr.Company;
                    lblCompAddr1.Text = c.Address1;
                    lblCompAddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompEmail.Text = sr.CompanyEmail;
                    lblCustPhone.Text = sr.BillingAddress[0].Phone1;
                    lblLocName.Text = sr.Location;
                    lblLocAddr1.Text = sr.LocationAddress1;
                    lblLocAddr2.Text = sr.LocationAddress2;
                    lblLocPhone.Text = sr.LocationPhone;
                    lblDate.Text = sr.EntryDateString;
                    lblInvoiceNo.Text = sr.SalesBillNo;
                    lblDiscount.Text = Convert.ToString(sr.Discount);
                    lblFreight.Text = Convert.ToString(sr.FreightAmount);
                    lblTax.Text = Convert.ToString(sr.TaxAmount);
                    lblGross.Text = Convert.ToString(sr.Gross);
                    lblroundOff.Text = Convert.ToString(sr.RoundOff);
                    lblNet.Text = Convert.ToString(sr.NetAmount);
                    tandc.Text = Entities.Application.Settings.GetSetting(116);
                    for (int i = 0; i < sr.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        if (setting.EnableDescription)//Enabled Description
                        {
                            TableCell t2 = new TableCell();
                            t2.Width = 500;
                            string itemName = "<b>";
                            itemName += sr.Products[i].Name;
                            itemName += "</b><br/>";
                            itemName += sr.Products[i].Description;
                            t2.Text = itemName;
                            r.Cells.Add(t2);
                        }
                        else
                        {
                            TableCell t2 = new TableCell();
                            t2.Text = sr.Products[i].Name;
                            r.Cells.Add(t2);
                        }
                        TableCell t3 = new TableCell();
                        t3.Text = sr.Products[i].ItemCode;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = sr.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = sr.Products[i].MRP.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = sr.Products[i].SellingPrice.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = sr.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = sr.Products[i].Gross.ToString();
                        r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = sr.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = sr.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
                        listTable.Rows.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "Entry | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
        }
    }
}