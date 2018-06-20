using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.VAT.C
{
    public partial class Entry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationid = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.Register.SalesEntryRegister sr = new Entities.Register.SalesEntryRegister();
                    sr = Entities.Register.SalesEntryRegister.GetDetails(id, locationid);
                    lblDate.Text = sr.EntryDateString;
                    lblInvno.Text = sr.SalesBillNo;
                    lblCustomerName.Text = sr.Customer;
                    lblAddress.Text = sr.BillingAddress[0].Address1;
                    lblAddress1.Text = sr.BillingAddress[0].Address2;
                    lblCountry.Text = sr.BillingAddress[0].Country;
                    lblPhoneNo1.Text = sr.BillingAddress[0].Phone1;
                    lblCustomer.Text = sr.Customer;
                    lblAddress2.Text = sr.BillingAddress[0].Address2;
                    lblAddress3.Text = sr.BillingAddress[0].Address2;
                    lblCountry1.Text = sr.BillingAddress[0].Country;
                    lblPhoneno.Text = sr.BillingAddress[0].Phone1;
                    lblNetCfr.Text = sr.NetAmount.ToString();
                    lblTaxAmount.Text = sr.TaxAmount.ToString();
                    lblSubTotal.Text = Convert.ToDecimal(sr.Gross).ToString();
                    for (int i = 0; i <sr.Products.Count; i++)
                    {
                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        cell1.Text = sr.Products[i].ItemCode;
                        row.Cells.Add(cell1);

                        TableCell cell2 = new TableCell();
                        cell2.Text = sr.Products[i].Name;
                        row.Cells.Add(cell2);

                        TableCell cell3 = new TableCell();
                        cell3.Text = sr.Products[i].Quantity.ToString();
                        cell3.Style.Add("text-align","right");
                        row.Cells.Add(cell3);

                        TableCell cell4 = new TableCell();
                        cell4.Text = sr.Products[i].SellingPrice.ToString();
                        cell4.Style.Add("text-align", "right");
                        row.Cells.Add(cell4);

                        TableCell cell5 = new TableCell();
                        cell5.Text = sr.Products[i].TaxAmount.ToString();
                        cell5.Style.Add("text-align", "right");
                        row.Cells.Add(cell5);

                        TableCell cell6 = new TableCell();
                        cell6.Text = sr.Products[i].NetAmount.ToString();
                        cell6.Style.Add("text-align", "right");
                        row.Cells.Add(cell6);

                        productTable.Rows.Add(row);

                    }
                   
                  //;  for (int i = 0; i < sr.Products.Count; i++)
                  //  {
                  //      TableRow r = new TableRow();
                  //      TableCell t1 = new TableCell();
                  //      t1.Text = (i + 1).ToString();
                  //      r.Cells.Add(t1)
                        //if (settings.EnableDescription)//Enabled Description
                        //{
                        //    TableCell t2 = new TableCell();
                        //    t2.Width = 500;
                        //    string itemName = "<b>";
                        //    itemName += sr.Products[i].Name;
                        //    itemName += "</b><br/>";
                        //    itemName += sr.Products[i].Description;
                        //    t2.Text = itemName;

                        //    r.Cells.Add(t2);
                        //}

                        //else
                        //{
                        //    TableCell t2 = new TableCell();
                        //    t2.Text = sr.Products[i].Name;
                        //    r.Cells.Add(t2);
                        //}
                        //TableCell t3 = new TableCell();
                        //t3.Text = Convert.ToDecimal(sr.Products[i].Quantity).ToString();
                        //r.Cells.Add(t3);
                        //TableCell t4 = new TableCell();
                        //t4.Text = Convert.ToDecimal(sr.Products[i].SellingPrice).ToString();
                        //r.Cells.Add(t4);
                        //TableCell t5 = new TableCell();
                        //t5.Text = Convert.ToDecimal(sr.Products[i].TaxAmount).ToString();
                        //r.Cells.Add(t5);
                        //TableCell t6 = new TableCell();
                        //t6.Text = Convert.ToDecimal(sr.Products[i].NetAmount).ToString();
                        //r.Cells.Add(t6);
                        //    TableCell t7 = new TableCell();
                        //    t7.Text = sr.Products[i].Gross.ToString();
                        //    r.Cells.Add(t7);
                        //    TableCell t8 = new TableCell();
                        //    t8.Text = sr.Products[i].TaxPercentage.ToString();
                        //    r.Cells.Add(t8);
                        //    if (sr.CustStateId == c.StateId)
                        //    {
                        //        TableCell t9 = new TableCell();
                        //        t9.Text = (Convert.ToDecimal(sr.Products[i].TaxAmount) / 2).ToString();
                        //        r.Cells.Add(t9);
                        //        TableCell t10 = new TableCell();
                        //        t10.Text = (Convert.ToDecimal(sr.Products[i].TaxAmount) / 2).ToString();
                        //        r.Cells.Add(t10);
                        //        TableCell t11 = new TableCell();
                        //        t11.Text = "0.00";
                        //        r.Cells.Add(t11);
                        //    }
                        //    else
                        //    {
                        //        TableCell t9 = new TableCell();
                        //        t9.Text = "0.00";
                        //        r.Cells.Add(t9);
                        //        TableCell t10 = new TableCell();
                        //        t10.Text = "0.00";
                        //        r.Cells.Add(t10);
                        //        TableCell t11 = new TableCell();
                        //        t11.Text = sr.Products[i].TaxAmount.ToString();
                        //        r.Cells.Add(t11);
                        //    }
                        //    TableCell t12 = new TableCell();
                        //    t12.Text = sr.Products[i].NetAmount.ToString();
                        //    r.Cells.Add(t12);
                        //    listTable.Rows.Add(r);
                        //}
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