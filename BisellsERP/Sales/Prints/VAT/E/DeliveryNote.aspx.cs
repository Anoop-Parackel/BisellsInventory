using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BisellsERP.Sales.Prints.VAT.E
{
    public partial class DeliveryNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int locationId = Convert.ToInt32(Request.QueryString["location"]);
                if (Request.QueryString["id"] != null && Request.QueryString["location"] != null)
                {
                    Entities.Register.SalesDeliveryNote sq = new Entities.Register.SalesDeliveryNote();
                    sq = Entities.Register.SalesDeliveryNote.GetDetails(id, locationId);
                    dynamic setting = Entities.Application.Settings.GetFeaturedSettings();
                    lblCurrency.Text = Convert.ToString(setting.CurrencySymbol);
                    Entities.Master.Company c = new Entities.Master.Company();
                    c = Entities.Master.Company.GetDetailsByLocation(locationId);
                    imgLogo.ImageUrl = "data:image/jpeg;base64, " + c.LogoBase64;
                    lblCustName.Text = sq.CustomerName;
                    lblComp.Text = sq.Company;
                    lblCompAddr1.Text = c.Address1;
                    lblCompAddr2.Text = c.Address2;
                    lblCompPh.Text = c.MobileNo1;
                    lblCompGst.Text = c.RegId1;
                    lblCompEmail.Text = sq.CompanyEmail;
                    lblCustPhone.Text = sq.BillingAddress[0].Phone1;
                    lblLocName.Text = sq.Location;
                    lblLocAddr1.Text = sq.LocationAddress1;
                    lblLocAddr2.Text = sq.LocationAddress2;
                    lblLocPhone.Text = sq.LocationPhone;
                    lblDate.Text = sq.EntryDateString;
                    lblCompName.Text = sq.Company;
                    lblInvoiceNo.Text = sq.DeliveryNoteNumber;
                    lblTax.Text = Convert.ToString(sq.TaxAmount);
                    lblGross.Text = Convert.ToString(sq.Gross);
                    lblroundOff.Text = Convert.ToString(sq.RoundOff);
                    lblNet.Text = Convert.ToString(sq.NetAmount);
                    lblAmountinWords.Text = NumberToWords(Convert.ToDouble(sq.NetAmount));
                    tAndC.Text = sq.TermsandConditon;
                    lblProjectName.Text = sq.JobName;
                    for (int i = 0; i < sq.Products.Count; i++)
                    {
                        TableRow r = new TableRow();
                        TableCell t1 = new TableCell();
                        t1.Text = (i + 1).ToString();
                        r.Cells.Add(t1);
                        //TableCell t2 = new TableCell();
                        //t2.Text = sq.Products[i].ItemCode;
                        //r.Cells.Add(t2);
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
                        t6.Text = sq.Products[i].CostPrice.ToString();
                        r.Cells.Add(t6);
                        //TableCell t7 = new TableCell();
                        //t7.Text = sq.Products[i].TaxPercentage.ToString();
                        //r.Cells.Add(t7);
                        TableCell t8 = new TableCell();
                        t8.Text = sq.Products[i].Gross.ToString();
                        r.Cells.Add(t8);
                        //TableCell t9 = new TableCell();
                        //t9.Text = sq.Products[i].TaxAmount.ToString();
                        //r.Cells.Add(t9);
                        TableCell t10 = new TableCell();
                        t10.Text = sq.Products[i].NetAmount.ToString();
                        r.Cells.Add(t10);
                        listTable.Rows.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "DeliveryNote | Page_Load(object sender, EventArgs e)");
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
