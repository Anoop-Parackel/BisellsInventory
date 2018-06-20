using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.GST.A
{
    public partial class CeditNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["Id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.Register.SalesReturnRegister sq = new Entities.Register.SalesReturnRegister();
                    sq = Entities.Register.SalesReturnRegister.GetDetails(id, locationid);
                    dynamic setting = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(setting.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    //lblCustName.Text = sq.CustomerName;
                    lblCompRegId.Text = sq.CompanyRegId;
                    lblLocRegId.Text = sq.LocationRegId;
                    lblCity.Text = c.City;
                    lblCompState.Text = c.State;
                    lblCompCountry.Text = c.Country;
                    lblCustState.Text = sq.CustomerState;
                    lblCustCountry.Text = sq.CustomerCountry;
                    lblCustTaxNo.Text = sq.CustomerTaxNo;
                    lblCustAddr1.Text = sq.CustomerAddress;
                    lblCustAddr2.Text = sq.CustomerAddress2;
                    lblComp.Text = sq.Company;
                    lblCompAddr1.Text = c.Address1;
                    lblCompAddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompRegId.Text = c.RegId1;
                    lblCustPhone.Text = sq.ContactNo;
                    lblLocName.Text = sq.Location;
                    lblLocAddr1.Text = sq.LocationAddress1;
                    lblLocAddr2.Text = sq.LocationAddress2;
                    lblLocPhone.Text = sq.LocationPhone;
                    lblDate.Text = sq.EntryDateString;
                    //lblInvoiceNo.Text = sq.CreditNoteNumber;
                    lblTotal.Text = Convert.ToString(sq.Gross);
                    lblTax.Text = Convert.ToString(sq.TaxAmount);
                    lblNet.Text = Convert.ToString(sq.NetAmount);
                    lblroundOff.Text = Convert.ToString(sq.RoundOff);
                    tAndC.Text = Entities.Application.Settings.GetSetting(115);
                    for (int i = 0; i < sq.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = sq.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = sq.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = sq.Products[i].MRP.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = sq.Products[i].SellingPrice.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = sq.Products[i].Quantity.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = sq.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = sq.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        if (c.StateId == sq.CustStateId)
                        {
                            TableCell t9 = new TableCell();
                            t9.Text = (Convert.ToDecimal(sq.Products[i].TaxAmount) / 2).ToString();
                            r.Cells.Add(t9);
                            TableCell t10 = new TableCell();
                            t10.Text = (Convert.ToDecimal(sq.Products[i].TaxAmount) / 2).ToString();
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
                            t11.Text = sq.Products[i].TaxAmount.ToString();
                            r.Cells.Add(t11);
                        }
                        TableCell t12 = new TableCell();
                        t12.Text = sq.Products[i].NetAmount.ToString();
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
