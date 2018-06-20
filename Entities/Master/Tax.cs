using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Tax
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public string Type { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of tax for populating dropdown list of tax
        /// </summary>
        /// <param name="CompanyId">company id of that particular item</param>
        /// <returns>dropdown list of tax names</returns>
        public static DataTable GetTax(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Tax_Id],[Percentage] FROM [dbo].[TBL_TAX_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "tax | GetTax(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each tax
        ///for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message </returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Tax, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Tax | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
             
                return new OutputMessage("Tax must not be empty", false, Entities.Type.RequiredFields, "Tax | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_TAX_MST](Name,Percentage,Type,Status,Created_By,Created_Date,Company_Id) values(@Name,@Percentage,@Type,@Status,@Created_By,GETUTCDATE(),@Company_Id);select @@identity";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Percentage", this.Percentage);
                        db.AddParameters(2, "@Type", this.Type);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(4, "@Created_By", this.CreatedBy);
                        db.AddParameters(5, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        if ( identity >= 1)
                        {
                            return new OutputMessage("Tax saved successfully", true, Entities.Type.NoError, "Tax | Save", System.Net.HttpStatusCode.OK,identity);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Tax could not be Saved", false, Entities.Type.Others, "Tax | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Tax could not be Saved", false, Entities.Type.Others, "Tax | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                       
                            db.Close();
                        
                    }

                }
            }
        }
        /// <summary>
        /// Update details of each tax
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Tax, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Tax | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID==0)
            {
               
                return new OutputMessage("ID must not be empty", false, Entities.Type.Others, "Tax | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if(string.IsNullOrWhiteSpace(this.Name))
            {
             
                return new OutputMessage("Name must not be empty", false, Entities.Type.RequiredFields, "Tax | Update", System.Net.HttpStatusCode.InternalServerError);


            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_TAX_MST] set Name=@Name,Percentage=@Percentage,Type=@Type,Status=@Status,Modified_By=@Modified_By,Modified_Date= GETUTCDATE() where Tax_Id=@id";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Percentage", this.Percentage);
                        db.AddParameters(2, "@Type", this.Type);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(4, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(5, "@id", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Details updated successfully", true, Entities.Type.NoError, "Tax | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Tax could not be updated", false, Entities.Type.Others, "Tax | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Tax could not be updated", false, Entities.Type.Others, "Tax | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                     db.Close();
                        
                    }

                }
            }
        }
        /// <summary>
        /// Delete individual tax from tax master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Tax, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        string query = "select ISNULL(is_system_defined,0) from TBL_TAX_MST where [Tax_Id]=@Id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@Id", this.ID);
                        db.Open();
                        bool isSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                        if (isSystemDefined)
                        {
                            return new OutputMessage("You cannot delete this tax because it is system defined", false, Entities.Type.Others, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {

                        query = @"delete from TBL_TAX_MST where Tax_Id=@Id";
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Tax deleted Successfully", true, Entities.Type.NoError, "Tax | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Tax could not be Deleted", false, Entities.Type.Others, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be zero for deletion", false, Entities.Type.Others, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    { 
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this Tax because it is referenced in other transactions", false, Entities.Type.Others, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Tax could not be Deleted", false, Entities.Type.Others, "Tax | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.Tax could not be Deleted", false, Entities.Type.Others, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                   
                        db.Close();
                    
                }

            }
        }

        /// <summary>
        /// Retrieves all the Taxes from the Tax Master
        /// </summary>
        /// <returns>List of Taxes</returns>
        public static List<Tax> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select t.Tax_Id,t.Name,isnull(t.Percentage,0)[Percentage],t.[Type],isnull(t.[Status],0)[Status],isnull(t.Created_By,0)[Created_By],
                               t.Created_Date,isnull(t.Modified_By,0)[Modified_By],t.Modified_Date,isnull(t.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_TAX_MST t 
                               left join TBL_COMPANY_MST c on c.Company_Id = t.Company_Id
                               where c.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Tax> result = new List<Tax>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Tax tax = new Tax();
                        tax.ID = item["tax_Id"] != DBNull.Value ? Convert.ToInt32(item["tax_Id"]) : 0;
                        tax.Name = Convert.ToString(item["Name"]);
                        tax.Percentage = item["percentage"] != null ? Convert.ToDecimal(item["percentage"]) : 0;
                        tax.Type = Convert.ToString(item["type"]);
                        tax.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        result.Add(tax);
                    }
                    return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "tax | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieves a single  Tax object from the Tax Master
        /// </summary>
        /// <returns>Tax list</returns>
        public static Tax GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 t.Tax_Id,t.Name,isnull(t.Percentage,0)[Percentage],t.[Type],isnull(t.[Status],0)[Status],isnull(t.Created_By,0)[Created_By],
                                t.Created_Date,isnull(t.Modified_By,0)[Modified_By],t.Modified_Date,isnull(t.Company_Id,0)[Company_Id], c.Name[Company]
                                from TBL_TAX_MST t 
                                left join TBL_COMPANY_MST c on c.Company_Id = t.Company_Id 
                                where c.Company_Id =@Company_Id and Tax_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Tax> result = new List<Tax>();
                if (dt != null)
                {
                   
                        Tax tax = new Tax();
                        DataRow item = dt.Rows[0];
                        tax.ID = item["tax_Id"] != DBNull.Value ? Convert.ToInt32(item["tax_Id"]) : 0;
                        tax.Name = Convert.ToString(item["Name"]);
                        tax.Percentage = item["percentage"] != null ? Convert.ToDecimal(item["percentage"]) : 0;
                        tax.Type = Convert.ToString(item["type"]);
                        tax.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;                        
                    
                    return tax;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "tax | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
