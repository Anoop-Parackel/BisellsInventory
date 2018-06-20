using BisellsERP.Publics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.VAT.E
{
    public partial class Quote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationId = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.Register.SalesQuoteRegister sq = new Entities.Register.SalesQuoteRegister();
                    sq = Entities.Register.SalesQuoteRegister.GetDetails(id, locationId);
                    dynamic setting = Entities.Application.Settings.GetFeaturedSettings();
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationId);
                    int User = Convert.ToInt32(Request.QueryString["lid"]);
                    dynamic obj;
                    if (Request.QueryString["lid"] != null)
                    {
                        obj = Entities.Application.User.GetUserDetails(User);
                    }
                    else
                    {
                        obj = Entities.Application.User.GetUserDetails(CPublic.GetuserID());
                    }
                    //dynamic obj = Entities.Application.User.GetUserDetails(CPublic.GetuserID());
                    lblUserName.Text = obj.UserName;
                    lblDesignation.Text = obj.Designation;
                    lblCompanyContact.Text = obj.Mobile;
                    lblCompanyEmail.Text = obj.Email;
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    imgLogo1.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    imgLogo2.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    imgLogo3.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblCustCompany.Text = sq.Customer_Name;
                    lblCompany.Text = sq.Company;
                    lblCustemail.Text = sq.BillingAddress[0].Email;
                    lblCustState.Text = sq.BillingAddress[0].State + "," + sq.BillingAddress[0].Country;
                    //lblCompanyContact.Text = c.MobileNo1;
                    //lblCompanyEmail.Text = sq.CompanyEmail;
                    lblDate.Text = sq.EntryDateString;
                    lblReference.Text = sq.QuoteNumber;
                    tAndC.Text = sq.TermsandConditon;
                    foreach (var billing in sq.BillingAddress)
                    {
                        lblCustName.Text = billing.ContactName;
                    }
                    for (int i = 0; i < sq.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        //TableCell t1 = new TableCell();
                        //t1.Text = (i + 1).ToString();
                        //r.Cells.Add(t1);
                        TableCell t2 = new TableCell();
                        t2.Text = sq.Products[i].ItemCode;
                        r.Cells.Add(t2);
                        //TableCell t3 = new TableCell();
                        //t3.Text = sq.Products[i].Name;
                        //r.Cells.Add(t3);
                        if (setting.EnableDescription)//Enabled Description
                        {
                            TableCell t3 = new TableCell();
                            t3.Width = 500;
                            string itemName = "<b>";
                            itemName += sq.Products[i].Name;
                            itemName += "</b><br/>";
                            itemName += sq.Products[i].Description;
                            t3.Text = itemName;
                            r.Cells.Add(t3);
                        }
                        else
                        {
                            TableCell t3 = new TableCell();
                            t3.Text = sq.Products[i].Name;
                            r.Cells.Add(t3);
                        }
                        TableCell t4 = new TableCell();
                        t4.Text = sq.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        //TableCell t5 = new TableCell();
                        //t5.Text = sq.Products[i].MRP.ToString();
                        //r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = sq.Products[i].SellingPrice.ToString();
                        r.Cells.Add(t6);
                        //TableCell t7 = new TableCell();
                        //t7.Text = sq.Products[i].TaxPercentage.ToString();
                        //r.Cells.Add(t7);
                        //TableCell t8 = new TableCell();
                        //t8.Text = sq.Products[i].Gross.ToString();
                        //r.Cells.Add(t8);
                        //TableCell t9 = new TableCell();
                        //t9.Text = sq.Products[i].TaxAmount.ToString();
                        //r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = sq.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
                        listTable.Rows.Add(r);
                    }
                    TableFooterRow tf = new TableFooterRow();
                    //TableCell tfc1 = new TableCell();
                    //tf.Cells.Add(tfc1);
                    //TableCell tfc2 = new TableCell();
                    //tf.Cells.Add(tfc2);
                    //TableCell tfc3 = new TableCell();
                    //tf.Cells.Add(tfc3);
                    //TableCell tfc4 = new TableCell();
                    //tf.Cells.Add(tfc4);
                    TableCell tfc5 = new TableCell();
                    tfc5.Text = "<b>Total in " + setting.CurrencySymbol+"</b>";
                    tfc5.ColumnSpan = 4;
                    tfc5.HorizontalAlign = HorizontalAlign.Right;
                    tf.Cells.Add(tfc5);
                    TableCell tfc6 = new TableCell();
                    tfc6.Text ="<b>" +Convert.ToString(sq.Gross)+"</b>";
                    tfc6.HorizontalAlign = HorizontalAlign.Right;
                    tf.Cells.Add(tfc6);
                    listTable.Rows.Add(tf);
                    TableFooterRow TaxAmountcell = new TableFooterRow();
                    TableCell tfc1 = new TableCell();
                    tfc1.Text = "<b>VAT AMOUNT</b>";
                    tfc1.ColumnSpan = 4;
                    tfc1.HorizontalAlign = HorizontalAlign.Right;
                    TaxAmountcell.Cells.Add(tfc1);
                    TableCell tfc2 = new TableCell();
                    tfc2.Text = "<b>"+Convert.ToString(sq.TaxAmount)+"</b>";
                    tfc2.HorizontalAlign = HorizontalAlign.Right;
                    TaxAmountcell.Cells.Add(tfc2);
                    listTable.Rows.Add(TaxAmountcell);
                    TableFooterRow TotalCell = new TableFooterRow();
                    TableCell tfc3 = new TableCell();
                    tfc3.Text = "<b>Grand Total in " + setting.CurrencySymbol+"</b>";
                    tfc3.ColumnSpan = 4;
                    tfc3.HorizontalAlign = HorizontalAlign.Right;
                    TotalCell.Cells.Add(tfc3);
                    TableCell tfc4 = new TableCell();
                    tfc4.Text = "<b>"+Convert.ToString(sq.NetAmount)+"</b>";
                    tfc4.HorizontalAlign = HorizontalAlign.Right;
                    TotalCell.Cells.Add(tfc4);
                    listTable.Rows.Add(TotalCell);

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