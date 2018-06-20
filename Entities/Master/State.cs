using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
 public class State
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CurrencyId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of State for populating dropdownlist of state 
        /// </summary>
        /// <param name="CompanyId">company id of that particular state</param>
        /// <returns>dropdown list of state</returns>
        public static DataTable GetState(int CompanyId,int CountryId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [State_ID],[Name] FROM [dbo].[TBL_STATE_MST] where Country_Id=@Country_Id and  Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(1, "@Country_Id", CountryId);
                    return db.ExecuteQuery(CommandType.Text, query);
                  
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "state |  GetState(int CompanyId,int CountryId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each State
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new InvalidOperationException("State name must not be empty");
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_STATE_MST](Name,Code,Currency_Id,Created_By,Created_Date,Company_Id) values(@Name,@Code,@Currency_Id,@Created_By,GETUTCDATE(),@Company_Id)";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Code", this.Code);
                        db.AddParameters(2, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "state |  Save()");
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
        /// Update details of each state
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
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
                        string query = @"Update [dbo].[TBL_STATE_MST] set Name=@Name,Code=@Code,Currency_Id=@Currency_Id,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where State_Id=@id"; 
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Code", this.Code);
                        db.AddParameters(2, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "state | Update()");
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
        ///  Delete individual state from state master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error alert</returns>
        public bool Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_STATE_MST where State_Id=@ID";
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
                    Application.Helper.LogException(ex, "state | Delete()");
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

        public static dynamic GetStates( int CountryId, int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    List<dynamic> result = new List<dynamic>();
                    string query = @"SELECT [State_ID],[Name] FROM [dbo].[TBL_STATE_MST] where Country_Id=@Country_Id and  Status<>0";
                    db.CreateParameters(1);
                   db.AddParameters(0, "@Country_Id", CountryId);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new { State=item["name"].ToString(),StateId=Convert.ToInt32(item["state_id"])});
                    }
                    return result;

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "state | GetStates( int CountryId, int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
      
    }
}
