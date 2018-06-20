using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Brand
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties

        #region Functions
        /// <summary>
        /// Retrieve Id and Name for Dropdownlist Population
        /// </summary>
        /// <param name="CompanyId">Company id of that particular brand</param>
        /// <returns>Dropdown list of Brand names</returns>
        public static DataTable GetBrand(int CompanyId)
        {

            using (DBManager db = new DBManager())
            {

                try
                {
                    db.Open();
                    string query = "SELECT [Brand_ID],[Name] FROM [dbo].[TBL_BRAND_MST] where Status<>0 and Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Brand | GetBrand(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each brands
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Brand, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Brand | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Brand Name must not be empty", false, Type.RequiredFields, "Brand | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_BRAND_MST](Name,[Order],[Status],Created_By,Created_Date,Company_Id) values(@Name,@Order,@Status,@Created_By,GETUTCDATE(),@Company_Id);select @@identity";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        if (identity >= 1)
                        {
                            return new OutputMessage("Brand Saved successfully", true, Type.NoError, "Brand | Save", System.Net.HttpStatusCode.OK, identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Brand could not be saved", false, Type.Others, "Brand | Save", System.Net.HttpStatusCode.InternalServerError);
                        }

                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Brand could not be saved", false, Type.Others, "Brand | Save", System.Net.HttpStatusCode.InternalServerError,ex);
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
        /// Update details of each brands
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Brand, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Brand | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.RequiredFields, "Brand | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Brand Name must not be empty", false, Type.RequiredFields, "Brand | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_BRAND_MST] set Name=@Name,[Order]=@Order,[Status]=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Brand_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Brand updated successfully", true, Type.NoError, "Brand | Update", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Brand could not be updated", false, Type.Others, "Brand | Update", System.Net.HttpStatusCode.InternalServerError);
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

                                return new OutputMessage("You cannot update this brand because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Brand | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                            else
                            {
                                db.RollBackTransaction();

                                return new OutputMessage("Something went wrong.Brand could not be updated", false, Type.RequiredFields, "Brand | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong.Brand could not be updated", false, Type.Others, "Brand | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                       
                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual brand from brand master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Brand, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_BRAND_MST where Brand_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Brand Deleted Successfully", true, Type.Others, "Brand | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Please Select a Brand.Brand coul not be deleted", false, Type.Others, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Something went wrong.Brand could not be deleted", false, Type.Others, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError);
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

                            return new OutputMessage("You cannot delete this brand because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.could not Delete brand", false, Type.Others, "Brand | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieve a list of brands from brand master under a particular company id
        /// </summary>
        /// <param name="CompanyId">Company id of brands list</param>
        /// <returns>list of brands</returns>
        public static List<Brand> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select b.Brand_ID,b.Name,b.[Order],isnull(b.[Status],0)[Status],isnull(b.Created_By,0)[Created_By],
                               b.Created_Date,isnull(b.Modified_By,0)[Modified_By],b.Modified_Date,
                               isnull(b.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_BRAND_MST b 
                               left join TBL_COMPANY_MST c on c.Company_Id = b.Company_Id  
                               where c.Company_Id =@Company_Id order by Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Brand> result = new List<Brand>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Brand brand = new Brand();
                        brand.ID = item["Brand_Id"] != DBNull.Value ? Convert.ToInt32(item["Brand_Id"]) : 0;
                        brand.Name = Convert.ToString(item["Name"]);
                        brand.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        brand.Order = item["Order"] != DBNull.Value ? Convert.ToInt32(item["Order"]) : 0;
                        result.Add(brand);
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
                Application.Helper.LogException(ex, "Brand | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single brand from brand master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>Details of a single brand</returns>
        public static Brand GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 b.Brand_ID,b.Name,b.[Order],isnull(b.[Status],0)[Status],
                               isnull(b.Created_By,0)[Created_By],b.Created_Date,isnull(b.Modified_By,0)[Modified_By],
                               b.Modified_Date,isnull(b.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_BRAND_MST b left join TBL_COMPANY_MST c on c.Company_Id = b.Company_Id 
                               where c.Company_Id =@Company_Id and Brand_ID=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    Brand brand = new Brand();
                    DataRow item = dt.Rows[0];
                    brand.ID = item["Brand_ID"] != DBNull.Value ? Convert.ToInt32(item["Brand_ID"]) : 0;
                    brand.Name = Convert.ToString(item["Name"]);
                    brand.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                    brand.Order = item["Order"] != DBNull.Value ? Convert.ToInt32(item["Order"]) : 0;
                    return brand;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Brand | GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        #endregion Function

    }
}
