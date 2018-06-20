using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
 public  class Country
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int CurrencyId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of Country for populating dropdown list
        /// </summary>
        /// <param name="CompanyId">Company id of the country list</param>
        /// <returns>dropdown list of country</returns>
        public static DataTable GetCountry(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
             
                try
                {
                    db.Open();
                    string query = @"SELECT [Country_Id],[Name] FROM [dbo].[TBL_COUNTRY_MST] where Status<>0";
                    return db.ExecuteQuery(CommandType.Text,query);
                 }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Country |  GetCountry(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each country
        /// </summary>
        /// <returns>Output message of Success when details Saved successfully otherwise return error message</returns>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new InvalidOperationException("Country name must not be empty");
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_COUNTRY_MST](Name,Currency_Id,Created_By,Created_Date,Company_Id) values(@Name,@Currency_Id,@Created_By,GETUTCDATE(),@Company_Id)";
                         db.CreateParameters(4);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(2, "@Created_By", this.CreatedBy);
                        db.AddParameters(3, "@Company_Id", this.CompanyId);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "Country |  Save()");
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
        /// Update details of each country
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public bool Update()
        {
            if (this.ID==0)
            {
                throw new InvalidOperationException("ID must not be empty");
            }
            else if(string.IsNullOrWhiteSpace(this.Name))
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
                        string query = @"Update [dbo].[TBL_COUNTRY_MST] set Name=@Name,Currency_Id=@Currency_Id,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Country_Id=@id"; 
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(2, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(3, "@id", this.ID);
               return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "Country |  Update()");
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
        ///  Delete individual country from country master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                  if(this.ID!=0)
                    {
                        db.Open();
                        string query = @"delete from TBL_COUNTRY_MST where Country_Id=@ID";
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
                    Application.Helper.LogException(ex, "Country |  Delete()");
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
