using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion properties
        #region Functions
        /// <summary>
        ///  Retrieve Id and Name for Dropdownlist Population
        /// </summary>
        /// <param name="CompanyId">company id of the particular category</param>
        /// <returns>Dropdown list of Category names</returns>
        public static DataTable GetCategory(int CompanyId)
        {
             using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT  [Category_Id],[Name] FROM [dbo].[TBL_CATEGORY_MST] where Status<>0 and Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text,query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Category | GetCategory(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each category
        /// </summary>
        /// <returns>Output message of Success when details Saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if(!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Category, Security.PermissionTypes.Create))
                {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Category | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Category name must not be empty", false,Type.RequiredFields, "Category | Save", System.Net.HttpStatusCode.InternalServerError);
            
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_CATEGORY_MST](Name,[Order],[Status],Created_By,Created_Date,Company_Id) values(@Name,@Order,@Status,@Created_By,GETUTCDATE(),@Company_Id);select @@identity";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        //bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (identity >= 1)
                        {
                            return new OutputMessage("Category saved successfully ", true, Type.NoError, "Category | Save", System.Net.HttpStatusCode.OK,identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Category could not be saved", false, Type.Others, "Category | Save", System.Net.HttpStatusCode.InternalServerError);

                        }

                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Category could not be saved", false, Type.Others, "Category | Save", System.Net.HttpStatusCode.InternalServerError,ex);

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
        /// Update details of each category
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if(!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Category, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Category | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID==0)
            {
                return new OutputMessage("Id Must Not Be Empty", false, Type.Others, "Category | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if(string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name Must Not Be Empty", false, Type.Others, "Category | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_CATEGORY_MST] set Name=@Name,[Order]=@Order,[Status]=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Category_Id=@id"; 
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);

                        bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Category updated successfully", true, Type.NoError, "Category | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Category could not be updated", false, Type.Others, "Category | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Category could not be updated", false, Type.Others, "Category | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                        
                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual category from category master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>Return success alert when details deleted successfully otherwise returns an error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Category, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Category | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                  else if (this.ID!=0)
                    {
                        db.Open();
                        string query = @"delete from TBL_CATEGORY_MST where Category_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Category Deleted Successfully", true, Type.NoError, "Category | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Category coul not be deleted", false, Type.Others, "Category | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "Category | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("You cannot delete this category because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "category | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "category | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.Category could not be deleted", false, Type.Others, "category | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    
                        db.Close();
                 
                }

            }
        }
        /// <summary>
        /// Retrieve all categories from category master
        /// </summary>
        /// <param name="CompanyId">Company id of that category list</param>
        /// <returns>list of categories</returns>
        public static List<Category> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select ca.Category_Id,ca.Name,ca.[Order],isnull(ca.[Status],0)[Status],isnull(ca.Created_By,0)[Created_By],
                               ca.Created_Date,isnull(ca.Modified_By,0)[Modified_By],ca.Modified_Date,
                               isnull(ca.Company_Id,0)[Company_Id],c.Name[Company] from TBL_CATEGORY_MST ca 
                               left join TBL_COMPANY_MST c on c.Company_Id = ca.Company_Id where c.Company_Id =@Company_Id order by Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
               List<Category> result = new List<Category>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Category category = new Category();
                        category.ID = item["Category_Id"] != DBNull.Value ? Convert.ToInt32(item["Category_Id"]) : 0;
                        category.Name = Convert.ToString(item["Name"]);
                        category.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        category.Order = item["Order"] != DBNull.Value ? Convert.ToInt32(item["Order"]) : 0;
                        result.Add(category);
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
                Application.Helper.LogException(ex, "Category | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single category from list of categories
        /// </summary>
        /// <param name="id">Id of that particular category</param>
        /// <param name="CompanyId">company id of the particular category</param>
        /// <returns>single category details</returns>
        public static Category GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 ca.Category_Id,isnull(ca.Name,0)[Name],isnull(ca.[Order],0)[Order],isnull(ca.[Status],0)[Status],
                               isnull(ca.Created_By,0)[Created_By],isnull(ca.Created_Date,0)[Created_Date],isnull(ca.Modified_By,0)[Modified_By],
                               isnull(ca.Modified_Date,0)[Modified_Date],isnull(ca.Company_Id,0)[Company_Id],isnull(c.Name,0)[Company] 
                               from TBL_CATEGORY_MST ca 
                               left join TBL_COMPANY_MST c on c.Company_Id = ca.Company_Id where c.Company_Id =@Company_Id and Category_Id=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                if (dt != null)
                {
                    
                        Category category = new Category();
                        DataRow item = dt.Rows[0];
                        category.ID = item["Category_Id"] != DBNull.Value ? Convert.ToInt32(item["Category_Id"]) : 0;
                        category.Name = Convert.ToString(item["Name"]);
                        category.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        category.Order = item["Order"] != DBNull.Value ? Convert.ToInt32(item["Order"]) : 0;
                    
                    return category;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Category | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        #endregion Functions
    }
}
