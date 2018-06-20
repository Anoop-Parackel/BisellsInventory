using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Purchase.Print.VAT.D
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
                    pq = Entities.Register.PurchaseQuoteRegister.GetDetails(id, locationid);
                    dynamic settings = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(settings.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationid);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblSupName.Text = pq.Supplier;
                    //lblComp.Text = pq.Company;
                    //lblCompAddr1.Text = c.Address1;
                    //lblCompAddr2.Text = c.Address2;
                    //lblCompPh.Text = c.MobileNo1;
                    //lblCompEmail.Text = c.Email;
                    lblCompGst.Text = c.RegId1;
                    lblSupPhone.Text = pq.BillingAddress[0].Phone1;
                    //lblLocName.Text = pq.Location;
                    //lblLocAddr1.Text = pq.LocationAddress1;
                    //lblLocAddr2.Text = pq.LocationAddress2;
                    //lblLocPhone.Text = pq.LocationPhone;
                    lblDate.Text = pq.EntryDateString;
                    lblInvoiceNo.Text = pq.QuoteNumber;
                    lblTax.Text = Convert.ToString(pq.TaxAmount);
                    lblGross.Text = Convert.ToString(pq.Gross);
                    lblroundOff.Text = Convert.ToString(pq.RoundOff);
                    lblNet.Text = Convert.ToString(pq.NetAmount);
                    tAndC.Text = pq.TermsandConditon;
                    //lblCompName.Text = pq.Company;
                    lblProjectName.Text = pq.JobName;
                    lblPaymentTerms.Text = pq.Payment_Terms;
                    lblAmountinWords.Text = NumberToWords(Convert.ToDouble(pq.NetAmount));
                    lblSupAddress.Text = pq.SupplierAddress1;
                    lblSupAddress2.Text = pq.SupplierAddress2;
                    lblSupEmail.Text = pq.SupplierEmail;
                    lblTRN.Text = pq.SupplierTaxNo;
                    for (int i = 0; i < pq.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        //TableCell t2 = new TableCell();
                        //t2.Text = pq.Products[i].ItemCode;
                        //r.Cells.Add(t2);
                        TableCell t3 = new TableCell();
                        t3.Text = pq.Products[i].Name;
                        r.Cells.Add(t3);
                        TableCell t4 = new TableCell();
                        t4.Text = pq.Products[i].Quantity.ToString();
                        r.Cells.Add(t4);
                        //TableCell t5 = new TableCell();
                        //t5.Text = pq.Products[i].MRP.ToString();
                        //r.Cells.Add(t5);
                        TableCell t6 = new TableCell();
                        t6.Text = pq.Products[i].CostPrice.ToString();
                        r.Cells.Add(t6);
                        TableCell t7 = new TableCell();
                        t7.Text = pq.Products[i].Gross.ToString();
                        r.Cells.Add(t7);
                        //TableCell t8 = new TableCell();
                        //t8.Text = pq.Products[i].TaxPercentage.ToString();
                        //r.Cells.Add(t8);
                        TableCell t9 = new TableCell();
                        t9.Text = pq.Products[i].TaxAmount.ToString();
                        r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = pq.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
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