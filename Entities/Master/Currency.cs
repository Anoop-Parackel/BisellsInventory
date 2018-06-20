using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
 public class Currency
    {
        #region properties
        public int ID { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties

        /// <summary>
        /// Retrieve Id and Name of Currency for populating dropdown list of Currency
        /// </summary>
        /// <param name="CompanyId">company id of currency list</param>
        /// <returns>dropdown list of currency names</returns>
        public static DataTable GetCurrency(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Currency_ID],[Code] FROM [dbo].[TBL_CURRENCY_MST] where Status<>0";
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Currency |  GetCurrency(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each currency
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                throw new InvalidOperationException("Currency must not be empty");
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_CURRENCY_MST](Code,Symbol,Deleted,Created_By,Created_Date,Company_Id) values(@Code,@Symbol,@Deleted,@Created_By,GETUTCDATE(),@Company_Id)";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Code", this.Code);
                        db.AddParameters(1, "@Symbol", this.Symbol);
                        db.AddParameters(2, "@Deleted", this.Deleted);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "Currency |  Save()");
                        return false;
                    }
                    finally
                    {
                        if (db.Connection.State == System.Data.ConnectionState.Open)
                        {
                            db.Close();
                        }
                    }

                }
            }
        }
        /// <summary>
        /// Update details of each currency
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public bool Update()
        {
            if (this.ID==0)
            {
                throw new InvalidOperationException("ID must not be empty");
            }
            else if(string.IsNullOrWhiteSpace(this.Code))
            {
                throw new InvalidOperationException("Name must not be empty");
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_CURRENCY_MST] set Code=@Code,Symbol=@Symbol,Deleted=@Deleted,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Currency_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Code", this.Code);
                        db.AddParameters(1, "@Symbol", this.Symbol);
                        db.AddParameters(2, "@Deleted", this.Deleted);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "Currency |  Update()");
                        return false;
                    }
                    finally
                    {
                       db.Close();
                        
                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual currency from currency master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_CURRENCY_MST where Currency_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    else
                    {
                        throw new InvalidOperationException("ID must not be zero for deletion");
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Currency |  Delete()");
                    return false;
                }
                finally
                {
                    if (db.Connection.State == System.Data.ConnectionState.Open)
                    {
                        db.Close();
                    }
                }

            }
        }
  }
}
