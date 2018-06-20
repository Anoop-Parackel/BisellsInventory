using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.VAT.F
{
    public partial class CreditNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationId = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    string invoicetype = "";
                    try
                    {
                        invoicetype = Request.QueryString["invoicetype"].ToString();
                    }
                    catch (Exception)
                    {

                    }
                    Entities.Register.CreditNote sr = new Entities.Register.CreditNote();
                    sr = Entities.Register.CreditNote.GetDetails(id, locationId);
                    dynamic setting = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(setting.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationId);
                    lblCompRegNo.Text = c.RegId1;
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    //lblCustName.Text = sr.Customer;
                    lblCustomer.Text = sr.Customer;
                    lblCustomerAddress1.Text = sr.BillingAddress[0].Address1;
                    lblCustomerAddress2.Text = sr.BillingAddress[0].Address2;
                    lblCustPh.Text = sr.BillingAddress[0].Phone1;
                    lblCustEmail.Text = sr.BillingAddress[0].Email;
                    lblCustTRN.Text = sr.CustomerTaxNo;
                    lblComp.Text = sr.Company;
                    lblTerms.Text = sr.TermsandConditon;
                    lblJob.Text = sr.JobName;
                    lblCompAddr1.Text = c.Address1;
                    lblCompAddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompEmail.Text = c.Email;
                    lblCustPhone.Text = sr.BillingAddress[0].Phone1;
                    lblCompanyEmail.Text = c.Email;
                    lblLocName.Text = sr.Location;
                    lblLocAddr1.Text = sr.LocationAddress1;
                    lblLocAddr2.Text = sr.LocationAddress2;
                    lblWords.Text = NumberToWords(Convert.ToDouble(sr.NetAmount));
                    //lblCustName.Text = sr.Customer;
                    //lblDiscount.Text = Convert.ToString(sr.Discount);
                    //lblFreight.Text = Convert.ToString(sr.FreightAmount);
                    lblLocPhone.Text = sr.LocationPhone;
                    lblDate.Text = sr.EntryDateString;
                    lblInvoiceNo.Text = sr.BillNo;
                    lblTax.Text = Convert.ToString(sr.TaxAmount);
                    lblGross.Text = Convert.ToString(sr.Gross);
                    lblroundOff.Text = Convert.ToString(sr.RoundOff);
                    //lblNet.Text = Convert.ToString(sr.NetAmount);
                    lblAmountinWords.Text = Convert.ToString(sr.NetAmount);
                    lblComp.Text = sr.Company;
                    //lblDuedate.Text = sr.EntryDateString;
                    lblCompanyPhone.Text = c.OfficeNo;
                    //lblAddress.Text = sr.CustomerAddress;
                    //lblCustName.Text = sr.Customer;
                    lblCustPhone.Text = sr.BillingAddress[0].Phone1;
                    //lblTfn.Text = sr.CustomerTaxNo;
                    lblProjectName.Text = sr.JobName;
                    //lblLPONumber.Text = sr.LPO;
                    lblDeduction.Text = Convert.ToString(sr.Discount);
                    //if (sr.PaymentStatus == 0)
                    //{
                    //    if (invoicetype == "delivery")
                    //    {
                    //        lblMainHead.Text = "DELIVERY ORDER";
                    //    }
                    //    else
                    //    {
                    //        lblMainHead.Text = "INVOICE";
                    //    }

                    //}
                    //else if (sr.PaymentStatus == 1)
                    //{
                    //    if (invoicetype == "delivery")
                    //    {
                    //        lblMainHead.Text = "DELIVERY ORDER";
                    //    }
                    //    else
                    //    {
                    //        lblMainHead.Text = "PROFORMA INVOICE";
                    //    }
                    //}
                    //else if (sr.PaymentStatus == 2)
                    //{
                    //    if (invoicetype == "delivery")
                    //    {
                    //        lblMainHead.Text = "DELIVERY ORDER";
                    //    }
                    //    else
                    //    {
                    //        lblMainHead.Text = "PROFORMA INVOICE";
                    //    }
                    //}
                    //lblTrn.Text = sr.CustomerTaxNo;
                    lblMainHead.Text = "Credit Note";
                    tAndC.Text = sr.TermsandConditon; //Entities.Application.Settings.GetSetting(148);

                    if (invoicetype == "delivery")
                    {
                        thRate.Visible = false;
                        thTotal.Visible = false;

                        for (int i = 0; i < sr.Products.Count; i++)
                        {
                            TableRow r = new TableRow();
                            TableCell t1 = new TableCell();
                            t1.Text = (i + 1).ToString();
                            r.Cells.Add(t1);
                            //TableCell t2 = new TableCell();
                            //t2.Text = sr.Products[i].ItemCode;
                            //r.Cells.Add(t2);
                            if (setting.EnableDescription)//Enabled Description
                            {
                                TableCell t3 = new TableCell();
                                t3.Width = 500;
                                string itemName = "<b>";
                                itemName += sr.Products[i].Name;
                                itemName += "</b><br/>";
                                itemName += sr.Products[i].Description;
                                t3.Text = itemName;
                                r.Cells.Add(t3);
                            }
                            else
                            {
                                TableCell t3 = new TableCell();
                                t3.Text = sr.Products[i].Name;
                                r.Cells.Add(t3);
                            }

                            TableCell t4 = new TableCell();
                            t4.Text = sr.Products[i].Quantity.ToString();
                            t4.Style.Add("text-align", "right");
                            r.Cells.Add(t4);

                            TableCell t5 = new TableCell();
                            t5.Text = sr.Products[i].Unit.ToString();
                            t5.Style.Add("text-align", "right");
                            r.Cells.Add(t5);
                            //TableCell t5 = new TableCell();
                            //t5.Text = sr.Products[i].MRP.ToString();
                            //r.Cells.Add(t5);
                            //TableCell t6 = new TableCell();
                            //t6.Text = sr.Products[i].SellingPrice.ToString();
                            //r.Cells.Add(t6);
                            //TableCell t7 = new TableCell();
                            //t7.Text = sr.Products[i].TaxPercentage.ToString();
                            //r.Cells.Add(t7);
                            //TableCell t8 = new TableCell();
                            //t8.Text = sr.Products[i].Gross.ToString();
                            //r.Cells.Add(t8);
                            //TableCell t9 = new TableCell();
                            //t9.Text = sr.Products[i].TaxAmount.ToString();
                            //r.Cells.Add(t9);
                            //TableCell t10 = new TableCell();
                            //t10.Text = sr.Products[i].NetAmount.ToString();
                            //r.Cells.Add(t10);
                            listTable.Rows.Add(r);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < sr.Products.Count; i++)
                        {
                            TableRow r = new TableRow();
                            TableCell t1 = new TableCell();
                            t1.Text = (i + 1).ToString();
                            r.Cells.Add(t1);
                            //TableCell t2 = new TableCell();
                            //t2.Text = sr.Products[i].ItemCode;
                            //r.Cells.Add(t2);
                            if (setting.EnableDescription)//Enabled Description
                            {
                                TableCell t3 = new TableCell();
                                t3.Width = 500;
                                string itemName = "<b>";
                                itemName += sr.Products[i].Name;
                                itemName += "</b><br/>";
                                itemName += sr.Products[i].Description;
                                t3.Text = itemName;
                                r.Cells.Add(t3);
                            }
                            else
                            {
                                TableCell t3 = new TableCell();
                                t3.Text = sr.Products[i].Name;
                                r.Cells.Add(t3);
                            }

                            TableCell t4 = new TableCell();
                            t4.Text = sr.Products[i].Quantity.ToString();
                            t4.Style.Add("text-align", "right");
                            r.Cells.Add(t4);

                            TableCell t5 = new TableCell();
                            t5.Text = sr.Products[i].Unit.ToString();
                            t5.Style.Add("text-align", "right");
                            r.Cells.Add(t5);
                            //TableCell t5 = new TableCell();
                            //t5.Text = sr.Products[i].MRP.ToString();
                            //r.Cells.Add(t5);
                            TableCell t6 = new TableCell();
                            t6.Text = sr.Products[i].SellingPrice.ToString();
                            t6.Style.Add("text-align", "right");
                            r.Cells.Add(t6);
                            //TableCell t7 = new TableCell();
                            //t7.Text = sr.Products[i].TaxPercentage.ToString();
                            //r.Cells.Add(t7);
                            //TableCell t8 = new TableCell();
                            //t8.Text = sr.Products[i].Gross.ToString();
                            //r.Cells.Add(t8);
                            //TableCell t9 = new TableCell();
                            //t9.Text = sr.Products[i].TaxAmount.ToString();
                            //r.Cells.Add(t9);
                            TableCell t10 = new TableCell();
                            t10.Text = sr.Products[i].Gross.ToString();
                            t10.Style.Add("text-align", "right");
                            r.Cells.Add(t10);
                            listTable.Rows.Add(r);
                        }

                        TableFooterRow tf = new TableFooterRow();
                        TableFooterRow TaxAmountcell = new TableFooterRow();
                        TableFooterRow SubTotal = new TableFooterRow();
                        tf.CssClass = "inv-footer beige";
                        TaxAmountcell.CssClass = "inv-footer beige";
                        SubTotal.CssClass = "inv-footer ";

                        TableCell tfc1 = new TableCell();
                        tfc1.Text = "<b>VAT AMOUNT</b>";

                        tfc1.ColumnSpan = 5;
                        tfc1.HorizontalAlign = HorizontalAlign.Right;
                        TaxAmountcell.Cells.Add(tfc1);
                        TableCell tfc2 = new TableCell();
                        tfc2.Text = "<b>" + Convert.ToString(sr.TaxAmount) + "</b>";
                        tfc2.HorizontalAlign = HorizontalAlign.Right;
                        TaxAmountcell.Cells.Add(tfc2);
                        listTable.Rows.Add(TaxAmountcell);

                        TableCell tfc3 = new TableCell();
                        tfc3.Text = "<b>SUB TOTAL</b>";
                        tfc3.ColumnSpan = 5;
                        tfc3.HorizontalAlign = HorizontalAlign.Right;
                        SubTotal.Cells.Add(tfc3);
                        TableCell tfc4 = new TableCell();
                        tfc4.Text = "<b>" + Convert.ToString(sr.Gross) + "</b>";
                        tfc4.HorizontalAlign = HorizontalAlign.Right;
                        SubTotal.Cells.Add(tfc4);
                        listTable.Rows.Add(SubTotal);





                        TableCell tfc5 = new TableCell();
                        tfc5.Text = "<b>GRAND TOTAL ( " + setting.CurrencySymbol + " )</b>";
                        tfc5.ColumnSpan = 5;
                        tfc5.HorizontalAlign = HorizontalAlign.Right;
                        tf.Cells.Add(tfc5);
                        TableCell tfc6 = new TableCell();
                        tfc6.Text = "<b>" + Convert.ToString(sr.NetAmount) + "</b>";
                        tfc6.HorizontalAlign = HorizontalAlign.Right;
                        tf.Cells.Add(tfc6);
                        listTable.Rows.Add(tf);
                    }
                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "Quote | Page_Load(object sender, EventArgs e)");
                Response.Write("<script> alert('" + ex.Message + "') </script>");
            }
        }



        public static string NumberToWords(double doubleNumber)
        {
            long beforeFloatingPoint = (long)Math.Floor(doubleNumber);
            var beforeFloatingPointWord = $"{NumberToWords(beforeFloatingPoint)} dirham";
            string[] nums = Convert.ToString(doubleNumber).Split('.');
            string small = "00";
            if (nums.Length == 2)
            {
                small = nums[1];
                if (nums[1].Length == 0)
                {
                    small += "00";
                }
                else if (nums[1].Length == 1)
                {
                    small += "0";
                }
            }

            long afterFloatingPoint = (long)Convert.ToInt32(small);
            var afterFloatingPointWord =
                $"{SmallNumberToWord(afterFloatingPoint, "")} fils";
            if (afterFloatingPoint > 0)
            {
                return $"{beforeFloatingPointWord} and {afterFloatingPointWord}";

            }
            return $"{beforeFloatingPointWord}";
        }

        private static string NumberToWords(long number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + "billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }

        private static string SmallNumberToWord(long number, string words)
        {
            if (number <= 0) return words;
            if (words != "")
                words += " ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }


    }
}




