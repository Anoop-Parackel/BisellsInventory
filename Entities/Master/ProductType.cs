using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductType
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int Modifiedby { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int companyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of product type for populating dropdown list
        /// </summary>
        /// <param name="CompanyId">company id of that particular product type</param>
        /// <returns>dropdown list of product type</returns>
        public static DataTable GetProductType(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Type_Id],[Name] FROM [dbo].[TBL_TYPE_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text,query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "producttype | GetProductType(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each product type
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Type, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Type | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                
                return new OutputMessage("Type name must not be empty", false, Entities.Type.RequiredFields, "Type | Save", System.Net.HttpStatusCode.InternalServerError);


            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_TYPE_MST](Name,[Order],[Status],Created_By,Created_Date,Company_Id)values(@Name,@Order,@Status,@Created_By,GETUTCDATE(),@Company_Id);select @@identity";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.companyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        if( identity>=1)
                        {
                            return new OutputMessage("Product Type saved successfully", true, Entities.Type.NoError, "Type | Save", System.Net.HttpStatusCode.OK,identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Product Type could not be saved ", false, Entities.Type.Others, "Type | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Product Type could not be saved", false, Entities.Type.Others, "Type | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                       
                            db.Close();
                      
                    }

                }
            }
        }
        /// <summary>
        /// Update details of each product type
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.Modifiedby, Security.BusinessModules.Type, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Type | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID==0)
            {
               
                return new OutputMessage("ID must not be empty", false, Entities.Type.Others, "Type | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if(string.IsNullOrWhiteSpace(this.Name))
            {
          
                return new OutputMessage("Name must not be empty", false, Entities.Type.RequiredFields, "Type | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_TYPE_MST] set Name=@Name,[Order]=@Order,[Status]=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Type_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.Modifiedby);
                        db.AddParameters(4, "@id", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Product Type updated successfully", true, Entities.Type.NoError, "Type | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Product Type could not be updated", false, Entities.Type.Others, "Type | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                            return new OutputMessage("Something went wrong. Product Type could not be updated", false, Entities.Type.Others, "Type | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                      db.Close();
                       
                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual product type from product type master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.Modifiedby, Security.BusinessModules.Type, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Type | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID!=0)
                    {
                        db.Open();
                        string query = @"delete from TBL_TYPE_MST where Type_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Product Type deleted successfully", true, Entities.Type.Others, "Type | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Product Type could not be deleted", false, Entities.Type.Others, "Type | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be zero for deletion", false, Entities.Type.Others, "Type | Delete", System.Net.HttpStatusCode.InternalServerError);

                      
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

                            return new OutputMessage("You cannot delete this product type because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "producttype | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "producttype | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Product Type could not be deleted", false, Type.Others, "producttype | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                   
                        db.Close();
                   
                }

            }
        }

        /// <summary>
        /// Retrieve all product types from product type master
        /// </summary>
        /// <param name="CompanyId">company id of product type</param>
        /// <returns>list of product types</returns>
        public static List<ProductType> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select ty.[Type_Id],ty.Name,ty.[Order],isnull(ty.[Status],0)[Status],
                               isnull(ty.Created_By,0)[Created_By],ty.Created_Date,isnull(ty.Modified_By,0)[Modified_By],
                               ty.Modified_Date,isnull(ty.Company_Id,0)[Company_Id],c.Name[Company] 
                               from TBL_TYPE_MST ty
                               left join TBL_COMPANY_MST c on c.Company_Id = ty.Company_Id 
                               where c.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                List<ProductType> result = new List<ProductType>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProductType producttype = new ProductType();
                        producttype.ID = item["type_Id"] != DBNull.Value ? Convert.ToInt32(item["type_Id"]) : 0;
                        producttype.Name = Convert.ToString(item["Name"]);
                        producttype.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        producttype.Order= item["order"] != DBNull.Value ? Convert.ToInt32(item["order"]) : 0;
                        result.Add(producttype);
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
                Application.Helper.LogException(ex, "producttype | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single product type from product type master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">company id of that particular item</param>
        /// <returns>details of single product type</returns>
        public static ProductType GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 ty.[Type_Id],ty.Name,ty.[Order],isnull(ty.[Status],0)[Status],
                               isnull(ty.Created_By,0)[Created_By],ty.Created_Date,isnull(ty.Modified_By,0)[Modified_By],
                               ty.Modified_Date,isnull(ty.Company_Id,0)[Company_Id],c.Name[Company] 
                               from TBL_TYPE_MST ty
                               left join TBL_COMPANY_MST c on c.Company_Id = ty.Company_Id 
                               where c.Company_Id=@Company_Id  and [Type_Id]=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                        ProductType producttype = new ProductType();
                        DataRow item = dt.Rows[0];
                        producttype.ID = item["type_Id"] != DBNull.Value ? Convert.ToInt32(item["type_Id"]) : 0;
                        producttype.Name = Convert.ToString(item["Name"]);
                        producttype.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        producttype.Order = item["order"] != DBNull.Value ? Convert.ToInt32(item["order"]) : 0;
                        return producttype;
                }
                else
                {
                  
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "producttype | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
