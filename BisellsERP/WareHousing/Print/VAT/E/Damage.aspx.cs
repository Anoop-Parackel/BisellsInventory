using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.WareHousing.Print.VAT.E
{
    public partial class Damage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {

                    Entities.WareHousing.Damage d = new Entities.WareHousing.Damage();
                    d = Entities.WareHousing.Damage.GetDetails(id, locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblCompAddr1.Text = c.Address1;
                    lblComp.Text = c.Name;
                    lblCompEmail.Text = c.Email;
                    lblCompPh.Text = c.Address1;
                    lblLocName.Text = d.Location;
                    lblLocAddr1.Text = d.LocationAddress1;
                    lblLocAddr2.Text = d.LocationAddress2;
                    lblLocPhone.Text = d.LocationPhone;
                    lblDate.Text = d.EntryDateString;
                    lblInvoiceNo.Text = d.DamageNo;
                    lblGross.Text = Convert.ToString(d.Gross);
                    lblTax.Text = Convert.ToString(d.TaxAmount);
                    lblroundOff.Text = Convert.ToString(d.RoundOff);
                    lblNet.Text = Convert.ToString(d.NetAmount);
                    tAndC.Text = Entities.Application.Settings.GetSetting(147);
                    for (int i = 0; i < d.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = d.Products[i].Name;
                        r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = d.Products[i].ItemCode;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = d.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        TableCell t5 = new TableCell();
                        t5.Text = d.Products[i].MRP.ToString();
                        r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = d.Products[i].CostPrice.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = d.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = d.Products[i].TaxPercentage.ToString();
                        r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = d.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = d.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t10);
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