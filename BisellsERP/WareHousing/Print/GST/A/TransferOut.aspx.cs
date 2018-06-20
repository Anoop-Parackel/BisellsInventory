using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.WareHousing.Print.GST.A
{
    public partial class TransferOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.WareHousing.TransferOut to = new Entities.WareHousing.TransferOut();
                    to = Entities.WareHousing.TransferOut.GetDetails(id, locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblCity.Text = c.City;
                    lblCompCountry.Text = c.Country;
                    lblCompState.Text = c.State;
                    lblCompRegId.Text = c.RegId1;
                    lblLocRegId.Text = to.LocationRegId;
                    lblComp.Text = c.Name;
                    lblCompaddr2.Text = c.Address2;
                    lblCompAddr1.Text = c.Address1;
                    lblCompPh.Text = c.Address1;
                    lblLocName.Text = to.Location;
                    lblLocAddr1.Text = to.LocationAddress1;
                    lblLocAddr2.Text = to.LocationAddress2;
                    lblLocPhone.Text = to.LocationPhone;
                    lblDate.Text = to.EntryDateString;
                    lblInvoiceNo.Text = to.EntryNo;
                    lblTotal.Text = Convert.ToString(to.Gross);
                    lblTax.Text = Convert.ToString(to.TaxAmount);
                    lblroundOff.Text = Convert.ToString(to.RoundOff);
                    lblNet.Text = Convert.ToString(to.NetAmount);
                    tAndC.Text = Entities.Application.Settings.GetSetting(145);
                    for (int i = 0; i < to.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = to.Products[i].Name;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = to.Products[i].ItemCode;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = to.Products[i].MRP.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = to.Products[i].CostPrice.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = to.Products[i].Quantity.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = to.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = to.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = (Convert.ToDecimal(to.Products[i].TaxAmount) / 2).ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = (Convert.ToDecimal(to.Products[i].TaxAmount) / 2).ToString();
                        r.Cells.Add(t10);
                        TableCell t11 = new TableCell();
                        t11.Text = "0.00";
                        r.Cells.Add(t11);
                        TableCell t12 = new TableCell();
                        t12.Text = to.Products[i].NetAmount.ToString();
                        r.Cells.Add(t12);
                        listTable.Rows.Add(r);
                    }

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
        }
    }
}