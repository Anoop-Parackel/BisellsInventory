using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
namespace Entities.Register
{
    /// <summary>
    /// Abstract class to implement common properties and functionalities to Register Classes
    /// Imherit this in all Registers
    /// </summary>
    public abstract class Register
    {
        public int ID { get; set; }
        public decimal Gross { get; set; }
        public decimal Discount { get; set; }
        public decimal RoundOff { get; set; }
        public decimal OtherCharges { get; set; }
        public decimal TaxAmount { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDateString { get; set; }
        public int TotalItems { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }


        /// <summary>  
        /// This Property has a default calculation. 
        /// However you can implement your own calculation by overriding this
        /// </summary>
        public virtual decimal NetAmount
        {
            get
            {
                return Gross + TaxAmount + RoundOff + OtherCharges - Discount;
            }
            set { }
        }
        
        public string Narration { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Function to update the last order number for registers
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="FinancialYear"></param>
        /// <param name="ReferenceTable">short code of register</param>
        /// <param name="Db">Datatbase object used to save the register entry(usually object in a transaction)</param>
        /// <returns></returns>
        public static bool UpdateOrderNumber(int CompanyID,string FinancialYear,string ReferenceTable,DBManager Db)
        {
            string query = @"update TBL_AUTO_ID set last_Bill_No  = last_Bill_No + 1 where company = "+CompanyID+" and Financial_Year='"+FinancialYear+"' and Ref_Table_Name='"+ReferenceTable+"'";
            return Db.ExecuteNonQuery(System.Data.CommandType.Text,query)>0?true:false;
        }

        public static string GetOrderNo(int CompanyId,string FinancialYear,string RegisterCode)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select [dbo].UDF_Generate_Sales_Bill(" + CompanyId + ",'" + FinancialYear + "','" + RegisterCode + "')[New_Order]";
                db.Open();
                return Convert.ToString(db.ExecuteScalar(System.Data.CommandType.Text, query));
            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Register | GetOrderNo(int CompanyId,string FinancialYear,string RegisterCode)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
