using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using Entities.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entities
{
    public class Supplier
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Taxno1 { get; set; }
        public string Taxno2 { get; set; }
        public int CurrencyId { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Date { get; set; }
        public string Reference { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }
        public string EntryDate { get; set; }
        public string Time { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public decimal TotalPayables { get; set; }
        public string Salutation { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Contact_Name { get; set; }
        public int SupplierAddressID { get; set; }
        public bool IsPrimary { get; set; }
        public int NoOfOrders { get; set; }
        public int NoOfBills { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileImageB64 { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name for Dropdownlist Population
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns>Dropdown list of supplier names</returns>
        public static DataTable GetSupplier(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Supplier_Id],[Name] FROM [dbo].[TBL_SUPPLIER_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "supplier | GetSupplier(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each supplier
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Suppliers, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Suppliers | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Supplier name must not be empty", false, Type.RequiredFields, "Supplier | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {

                return new OutputMessage("Enter a valid Email", false, Type.Others, "Supplier | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        try
                        {
                            if (this.ProfileImageB64 != null && !string.IsNullOrWhiteSpace(this.ProfileImageB64))
                            {
                                byte[] bytes = Convert.FromBase64String(this.ProfileImageB64);
                                string guid = Guid.NewGuid().ToString();
                                string basePath = System.Web.Configuration.WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                                string filePath = "Resources\\Suppliers\\ProfileImages";
                                string fullPath = Path.Combine(basePath, filePath);
                                string file = guid + ".jpeg";
                                if (!Directory.Exists(fullPath))
                                {
                                    Directory.CreateDirectory(fullPath);
                                }
                                using (FileStream fs = new FileStream(Path.Combine(fullPath, file), FileMode.Create))
                                {
                                    fs.Write(bytes, 0, bytes.Length);
                                    fs.Flush();
                                    fs.Dispose();
                                }
                                this.ProfileImagePath = "/Resources/Suppliers/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        db.Open();
                        string query = @"insert into [dbo].[TBL_SUPPLIER_MST](Name,Address1,Address2,Country_Id,State_Id,Phone1,Phone2,Email,Taxno1,Taxno2,Currency_Id,Status,Created_By,Created_Date,Company_Id,Salutation,ZipCode,City,Contact_Name,Profile_Image_Path)
                        values(@Name,@Address1,@Address2,@Country_Id,@State_Id,@Phone1,@Phone2,@Email,@Taxno1,@Taxno2,@Currency_Id,@Status,@Created_By,GETUTCDATE(),@Company_Id,@Salutation,@ZipCode,@City,@Contact_Name,@Profile_Image_Path);select @@identity";
                        db.CreateParameters(19);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Country_Id", this.CountryId);
                        db.AddParameters(4, "@State_Id", this.StateId);
                        db.AddParameters(5, "@Phone1", this.Phone1);
                        db.AddParameters(6, "@Phone2", this.Phone2);
                        db.AddParameters(7, "@Email", this.Email);
                        db.AddParameters(8, "@Taxno1", this.Taxno1);
                        db.AddParameters(9, "@Taxno2", this.Taxno2);
                        db.AddParameters(10, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(11, "@Status", this.Status);
                        db.AddParameters(12, "@Created_By", this.CreatedBy);
                        db.AddParameters(13, "@Company_Id", this.CompanyId);
                        db.AddParameters(14, "@Salutation", this.Salutation);
                        db.AddParameters(15, "@ZipCode", this.ZipCode);
                        db.AddParameters(16, "@City", this.City);
                        db.AddParameters(17, "@Contact_Name", this.Contact_Name);
                        db.AddParameters(18, "@Profile_Image_Path", this.ProfileImagePath);
                        db.BeginTransaction();
                        int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        if (identity >= 1)
                        {
                            query = @"INSERT INTO [dbo].[TBL_SUPPLIER_ADDRESS]
                                ([Supplier_ID],[Salutation],[Name],[Address1],[Address2],[City],[State_ID],[Country_ID],[ZipCode],[Phone1],[Phone2],[Email],[Is_Primary],[Created_By],[Created_Date])
                                 VALUES
                                (@Supplier_ID,@Salutation,@Name,@Address1,@Address2,@City,@State_ID,@Country_ID,@ZipCode,@Phone1,@Phone2,@Email,1,@Created_By,GETUTCDATE())";
                            db.CleanupParameters();
                            db.CreateParameters(13);
                            db.AddParameters(0, "@Supplier_ID", identity);
                            db.AddParameters(1, "@Salutation", this.Salutation);
                            db.AddParameters(2, "@Name", this.Contact_Name);
                            db.AddParameters(3, "@Address1", this.Address1);
                            db.AddParameters(4, "@Address2", this.Address2);
                            db.AddParameters(5, "@City", this.City);
                            db.AddParameters(6, "@State_ID", this.StateId);
                            db.AddParameters(7, "@Country_ID", this.CountryId);
                            db.AddParameters(8, "@ZipCode", this.ZipCode);
                            db.AddParameters(9, "@Phone1", this.Phone1);
                            db.AddParameters(10, "@Phone2", this.Phone2);
                            db.AddParameters(11, "@Email", this.Email);
                            db.AddParameters(12, "@Created_By", this.CreatedBy);
                            db.ExecuteQuery(CommandType.Text, query);
                            db.CommitTransaction();
                            return new OutputMessage("Supplier Saved successfully", true, Type.NoError, "Supplier | Save", System.Net.HttpStatusCode.OK, identity);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Supplier could not be saved", false, Type.Others, "Supplier | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        db.RollBackTransaction();
                        Helper.LogException(ex, "Supplier | Save()");
                        return new OutputMessage("Something went wrong. Supplier could not be saved", false, Type.Others, "Supplier | Save", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Update details of each supplier
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Suppliers, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Suppliers | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "Supplier | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.RequiredFields, "Supplier | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {
                return new OutputMessage("Enter a valid Email", false, Type.Others, "Supplier | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        try
                        {
                            if (this.ProfileImageB64 != null && !string.IsNullOrWhiteSpace(this.ProfileImageB64))
                            {
                                byte[] bytes = Convert.FromBase64String(this.ProfileImageB64);
                                string guid = Guid.NewGuid().ToString();
                                string basePath = System.Web.Configuration.WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                                string filePath = "Resources\\Suppliers\\ProfileImages";
                                string fullPath = Path.Combine(basePath, filePath);
                                string file = guid + ".jpeg";
                                if (!Directory.Exists(fullPath))
                                {
                                    Directory.CreateDirectory(fullPath);
                                }
                                using (FileStream fs = new FileStream(Path.Combine(fullPath, file), FileMode.Create))
                                {
                                    fs.Write(bytes, 0, bytes.Length);
                                    fs.Flush();
                                    fs.Dispose();
                                }
                                this.ProfileImagePath = "/Resources/Suppliers/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        db.Open();
                        string query = @"Update [dbo].[TBL_SUPPLIER_MST] set Name=@Name,Address1=@Address1,Address2=@Address2,Country_Id=@Country_Id,
                            State_Id=@State_Id,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,Taxno1=@Taxno1,
                             Taxno2=@Taxno2,Currency_Id=@Currency_Id,Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE()
                            ,Salutation=@Salutation,ZipCode=@ZipCode,City=@City,Contact_Name=@Contact_Name,Profile_Image_Path=@Profile_Image_Path where Supplier_Id=@id";
                        db.CreateParameters(19);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Country_Id", this.CountryId);
                        db.AddParameters(4, "@State_Id", this.StateId);
                        db.AddParameters(5, "@Phone1", this.Phone1);
                        db.AddParameters(6, "@Phone2", this.Phone2);
                        db.AddParameters(7, "@Email", this.Email);
                        db.AddParameters(8, "@Taxno1", this.Taxno1);
                        db.AddParameters(9, "@Taxno2", this.Taxno2);
                        db.AddParameters(10, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(11, "@Status", this.Status);
                        db.AddParameters(12, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(13, "@id", this.ID);
                        db.AddParameters(14, "@Salutation", this.Salutation);
                        db.AddParameters(15, "@ZipCode", this.ZipCode);
                        db.AddParameters(16, "@City", this.City);
                        db.AddParameters(17, "@Contact_Name", this.Contact_Name);
                        db.AddParameters(18, "@Profile_Image_Path", this.ProfileImagePath);
                        db.BeginTransaction();
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                        query = @"update TBL_SUPPLIER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Supplier_ID=@Supplier_ID and Is_Primary=1";
                        db.CleanupParameters();
                        db.CreateParameters(13);
                        db.AddParameters(0, "@Supplier_ID", this.ID);
                        db.AddParameters(1, "@Salutation", this.Salutation);
                        db.AddParameters(2, "@Name", this.Contact_Name);
                        db.AddParameters(3, "@Address1", this.Address1);
                        db.AddParameters(4, "@Address2", this.Address2);
                        db.AddParameters(5, "@City", this.City);
                        db.AddParameters(6, "@State_ID", this.StateId);
                        db.AddParameters(7, "@Country_ID", this.CountryId);
                        db.AddParameters(8, "@ZipCode", this.ZipCode);
                        db.AddParameters(9, "@Phone1", this.Phone1);
                        db.AddParameters(10, "@Phone2", this.Phone2);
                        db.AddParameters(11, "@Email", this.Email);
                        db.AddParameters(12, "@Modified_By", this.ModifiedBy);
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                        db.CommitTransaction();
                        return new OutputMessage("Supplier Updated successfully", true, Type.NoError, "Supplier | Update", System.Net.HttpStatusCode.OK);

                    }
                    catch (Exception ex)
                    {
                        db.RollBackTransaction();
                        Helper.LogException(ex, "Supplier | Update()");
                        return new OutputMessage("Something went wrong. Supplier could not be updated", false, Type.Others, "Supplier | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual supplier from supplier master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Suppliers, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Suppliers | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_SUPPLIER_MST where Supplier_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Supplier deleted successfully", true, Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Supplier could not be deleted", false, Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {

                        return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this Supplier because it is referenced in other transactions", false, Entities.Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Supplier could not be deleted", false, Entities.Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Supplier could not be deleted", false, Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {

                    db.Close();

                }

            }
        }
        /// <summary>
        /// Retrieves all the supplier from the supplier Master
        /// </summary>
        /// <returns>List of supplier</returns>
        public static List<Supplier> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select S.Supplier_Id,s.Name,s.Address1,s.Address2,isnull(s.Country_Id,0)[Country_Id],isnull(s.State_Id,0)[State_Id],
                               s.Phone1,s.Phone2,s.Email,s.Taxno1,s.Taxno2,isnull(s.Currency_Id,0)[Currency_Id],isnull(s.[Status],0)[Status],
                               isnull(s.Created_By,0)[Created_By],s.Created_Date,isnull(s.Modified_By,0)[Modified_By],s.Modified_Date,isnull(s.Company_Id,0)[Company_Id],
                               co.Name[Country],st.Name[State],cu.Code[Currency],cm.Name[Company],s.Salutation,s.ZipCode,s.City  from TBL_SUPPLIER_MST S
                               left join TBL_COUNTRY_MST co on S.Country_Id=co.Country_Id
                               left join TBL_STATE_MST st on st.State_Id=S.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id=S.Currency_Id
                               left join TBL_COMPANY_MST cm on cm.Company_Id=s.Company_Id where cm.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Supplier> result = new List<Supplier>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Supplier supplier = new Supplier();
                        supplier.ID = (item["Supplier_Id"] != DBNull.Value) ? Convert.ToInt32(item["Supplier_Id"]) : 0;
                        supplier.Name = Convert.ToString(item["Name"]);
                        supplier.Address1 = Convert.ToString(item["Address1"]);
                        supplier.Address2 = Convert.ToString(item["Address2"]);
                        supplier.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                        supplier.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                        supplier.Phone1 = Convert.ToString(item["Phone1"]);
                        supplier.Phone2 = Convert.ToString(item["Phone2"]);
                        supplier.Email = Convert.ToString(item["Email"]);
                        supplier.Taxno1 = Convert.ToString(item["Taxno1"]);
                        supplier.Taxno2 = Convert.ToString(item["Taxno2"]);
                        supplier.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                        supplier.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                        supplier.CreatedBy = (item["Created_By"] != DBNull.Value) ? Convert.ToInt32(item["Created_By"]) : 0;
                        supplier.Currency = Convert.ToString(item["Currency"]);
                        supplier.Country = Convert.ToString(item["Country"]);
                        supplier.State = Convert.ToString(item["State"]);
                        supplier.City = Convert.ToString(item["City"]);
                        supplier.ZipCode = Convert.ToString(item["ZipCode"]);
                        supplier.Salutation = Convert.ToString(item["Salutation"]);
                        result.Add(supplier);
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
                Application.Helper.LogException(ex, "supplier |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single supplier from the supplier Master
        /// </summary>
        /// <returns>supplier list</returns>
        public static Supplier GetDetails(int Id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 S.Supplier_Id,s.Name,s.Address1,s.Address2,isnull(s.Country_Id,0)[Country_Id],isnull(s.State_Id,0)[State_Id],
                               s.Phone1, s.Phone2, s.Email, s.Taxno1, s.Taxno2,isnull(s.Currency_Id,0)[Currency_Id],isnull(s.[Status],0)[Status],
                               isnull(s.Created_By,0)[Created_By],s.Created_Date,isnull( s.Modified_By,0)[Modified_By],s.Modified_Date,isnull(s.Company_Id,0)[Company_Id],
                               co.Name[Country], st.Name[State], cu.Code[Currency], cm.Name[Company],s.Salutation,s.ZipCode,s.City,s.Contact_Name,s.Profile_Image_Path from TBL_SUPPLIER_MST S
                               left join TBL_COUNTRY_MST co on S.Country_Id = co.Country_Id
                               left join TBL_STATE_MST st on st.State_Id = S.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id = S.Currency_Id
                               left join TBL_COMPANY_MST cm on cm.Company_Id = s.Company_Id where cm.Company_Id=@Company_Id and Supplier_Id=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataRow item = dt.Rows[0];
                Supplier supplier = new Supplier();
                supplier.ID = (item["Supplier_Id"] != DBNull.Value) ? Convert.ToInt32(item["Supplier_Id"]) : 0;
                supplier.Name = Convert.ToString(item["Name"]);
                supplier.Address1 = Convert.ToString(item["Address1"]);
                supplier.Address2 = Convert.ToString(item["Address2"]);
                supplier.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                supplier.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                supplier.Phone2 = Convert.ToString(item["Phone2"]);
                supplier.Phone1 = Convert.ToString(item["Phone1"]);
                supplier.Email = Convert.ToString(item["Email"]);
                supplier.Taxno1 = Convert.ToString(item["Taxno1"]);
                supplier.Taxno2 = Convert.ToString(item["Taxno2"]);
                supplier.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                supplier.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                supplier.Currency = Convert.ToString(item["Currency"]);
                supplier.Country = Convert.ToString(item["Country"]);
                supplier.State = Convert.ToString(item["State"]);
                supplier.City = Convert.ToString(item["City"]);
                supplier.ZipCode = Convert.ToString(item["ZipCode"]);
                supplier.Salutation = Convert.ToString(item["Salutation"]);
                supplier.Contact_Name= Convert.ToString(item["Contact_Name"]);
                supplier.ProfileImagePath = Convert.ToString(item["profile_image_path"]);
                return supplier;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "supplier |  GetDetails(int Id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// used to return mail id of suppliers in purchase indent
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> GetSupplierMail(int CompanyId)
        {
            try
            {
                DBManager db = new DBManager();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@company", CompanyId);
                string query = @"select supplier_id,Name,email from TBL_SUPPLIER_MST where company_id=@company";
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Supplier> suppliers = new List<Supplier>();
                if (dt != null)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        Supplier supplier = new Supplier();
                        supplier.ID = Convert.ToInt32(row["supplier_id"]);
                        supplier.Name = Convert.ToString(row["Name"]);
                        supplier.Email = Convert.ToString(row["email"]);
                        suppliers.Add(supplier);
                    }
                    return suppliers;
                }
                return null;

            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public static List<Supplier> GetSupplierDetails(int CompanyId, int? SupplierId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select s.Supplier_Id,s.Name[Supplier],c.Name[Company],s.Email,s.Phone1,
	                           (select isnull(sum([pending amount]),0) from REPORT_PENDINGSUPPLIERPAYMENTS 
                               where Supplier_Id=s.Supplier_Id)[Total_Payable] from TBL_SUPPLIER_MST s
	                           left join TBL_COMPANY_MST c on c.Company_Id=s.Company_Id
	                           where s.Company_Id=@Company_Id  {#supplierfilter#} order by s.Created_Date desc";

                if (SupplierId != null && SupplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and s.Supplier_Id=@supplier_Id ");
                }
                else
                {

                    query = query.Replace("{#supplierfilter#}", string.Empty);
                }
                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@supplier_Id", SupplierId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataSet ds = new DataSet(); 
                List<Supplier> result = new List<Supplier>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Supplier sup = new Supplier();
                        sup.ID = (item["Supplier_Id"] != DBNull.Value) ? Convert.ToInt32(item["Supplier_Id"]) : 0;
                        sup.Name = Convert.ToString(item["Supplier"]);
                        sup.Company = Convert.ToString(item["Company"]);
                        sup.Email = Convert.ToString(item["Email"]);
                        sup.Phone1 = Convert.ToString(item["Phone1"]);
                        sup.TotalPayables = Convert.ToDecimal(item["Total_Payable"]);
                        result.Add(sup);
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
                Application.Helper.LogException(ex, "Supplier |  GetSupplierDetails(int CompanyId, int? SupplierId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<Supplier> GetSupplierActivity(int Id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                #region query
                string query = @";with cte as (
                               select top 5 pe.Created_Date[Date],CONVERT(varchar(15),cast(pe.Created_Date as time),100)[Time],pe.entry_Date,pe.Entry_No[Reference],s.name[Supplier_Name],
                                 l.Name[Name],'PE'[Code],'#F08080'[CodeColor],'New items purchased from  <strong>' +s.Name+'</strong>' [Activity],
                                 u.Full_Name[User],'/Purchase/Entry?MODE=edit&UID='+convert(varchar(20),pe.Pe_Id)[Url]
                                  from TBL_PURCHASE_ENTRY_REGISTER pe left join TBL_LOCATION_MST l on l.Location_Id=pe.Location_Id
                                  left join TBL_SUPPLIER_MST s on s.Supplier_Id=pe.Supplier_Id
                                  left join TBL_USER_MST u on u.User_Id=pe.Created_By where pe.Supplier_Id=@Supplier_Id order by pe.Created_Date desc
                                  ),
                                 cte1 as (
                                 select top 5 pq.Created_Date[Date],CONVERT(varchar(15),cast(pq.Created_Date as time),100)[Time],pq.Entry_Date,pq.Quote_No[Reference],s.name[Supplier_Name],
                                 l.Name[Name],'PQ'[Code],'#808080'[CodeColor],'New purchase order has been drafted to <strong>'+s.Name+'</Strong>'[Activity],
                                 u.Full_Name[User],'/Purchase/Quote?MODE=edit&UID='+convert(varchar(20),pq.Pq_Id)[Url] from TBL_PURCHASE_QUOTE_REGISTER pq
                                 left join TBL_LOCATION_MST l on l.Location_Id=pq.Location_Id
                                 left join TBL_SUPPLIER_MST s on s.Supplier_Id=pq.Supplier_Id
                                 left join TBL_USER_MST u on u.[User_Id]=pq.Created_By where pq.Supplier_Id=@Supplier_Id order by pq.Created_Date desc
                                 ),
                                cte2 as(
                                select * from cte 
                                union all     
                                select * from cte1
                                
                                )
                                select * from cte2 order by [date] desc ";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Supplier_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                query = @"select count(*) bills from TBL_PURCHASE_ENTRY_REGISTER where Supplier_Id=@supplier_Id
                          select count(*) orders from TBL_PURCHASE_QUOTE_REGISTER where Supplier_Id=@supplier_Id";
                db.CleanupParameters();
                db.CreateParameters(1);
                db.AddParameters(0, "@supplier_Id", Id);
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(CommandType.Text, query);
                List<Supplier> result = new List<Supplier>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Supplier sup = new Supplier();
                        sup.Name = Convert.ToString(item["Supplier_Name"]);
                        sup.Reference = Convert.ToString(item["Reference"]);
                        sup.Activity = Convert.ToString(item["Activity"]);
                        sup.Url = Convert.ToString(item["url"]);
                        sup.Date = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["date"])).ToString("dd MMM yyyy");
                        sup.EntryDate = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["entry_date"])).ToString("dd MMM yyyy");
                        sup.Time = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["date"])).ToString("hh:mm tt");
                        sup.NoOfBills = Convert.ToInt32(ds.Tables[0].Rows[0]["bills"]);
                        sup.NoOfOrders = Convert.ToInt32(ds.Tables[1].Rows[0]["orders"]);
                        result.Add(sup);
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
                Application.Helper.LogException(ex, "Supplier | GetSupplierActivity(int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// Gets All the customer contact address of purticular ID
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public static List<Supplier> GetSupplierAddress(int SupplierID)
        {
            DBManager db = new DBManager();
            try
            {
                List<Supplier> listSupplier = new List<Supplier>();
                string query = @"select Supplier_Address_ID,Supplier_ID,Salutation,sup.Name,Address1,Address2,City,sup.Phone1,sup.Phone2,sup.Email,st.Name State,Co.Name Country,sup.State_Id,sup.Country_Id,sup.Zipcode,sup.Is_Primary from TBL_SUPPLIER_ADDRESS sup
                                 left join TBL_STATE_MST st on st.State_Id=sup.State_Id
                                 left join TBL_COUNTRY_MST co on co.Country_Id=sup.Country_Id where Supplier_ID=@Supplier_ID";

                DataTable dt = new DataTable();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@Supplier_ID", SupplierID);
                dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Supplier sup = new Supplier();
                        sup.ID = dt.Rows[i]["Supplier_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Supplier_id"]) : 0;
                        sup.Salutation = Convert.ToString(dt.Rows[i]["Salutation"]);
                        sup.Name = Convert.ToString(dt.Rows[i]["Name"]);
                        sup.Address1 = Convert.ToString(dt.Rows[i]["Address1"]);
                        sup.Address2 = Convert.ToString(dt.Rows[i]["Address2"]);
                        sup.City = Convert.ToString(dt.Rows[i]["City"]);
                        sup.Phone1 = Convert.ToString(dt.Rows[i]["Phone1"]);
                        sup.Phone2 = Convert.ToString(dt.Rows[i]["Phone2"]);
                        sup.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        sup.State = Convert.ToString(dt.Rows[i]["State"]);
                        sup.Country = Convert.ToString(dt.Rows[i]["Country"]);
                        sup.ZipCode = Convert.ToString(dt.Rows[i]["ZipCode"]);
                        sup.StateId = dt.Rows[i]["State_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["State_Id"]) : 0;
                        sup.CountryId = dt.Rows[i]["Country_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Country_Id"]) : 0;
                        sup.SupplierAddressID = dt.Rows[i]["Supplier_address_ID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Supplier_address_ID"]) : 0;
                        sup.IsPrimary = dt.Rows[i]["Is_Primary"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[i]["Is_Primary"]) : false;
                        listSupplier.Add(sup);
                    }
                }
                return listSupplier;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Supplier |  GetSupplierAddress(int SupplierID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Saves the Customer address
        /// </summary>
        /// <returns></returns>
        public OutputMessage SaveSupplierAddress()
        {
            DBManager db = new DBManager();
            try
            {
                if (String.IsNullOrWhiteSpace(this.Name))
                {
                    return new OutputMessage("Contact Name cannot be empty", false, Type.RequiredFields, "Supplier | SaveSupplierAddress", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (!String.IsNullOrWhiteSpace(this.Email)&&!Helper.IsValidEmail(this.Email))
                {
                    return new OutputMessage("Enter a valid Email", false, Type.RequiredFields, "Supplier | SaveSupplierAddress", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    string query = @"INSERT INTO [dbo].[TBL_SUPPLIER_ADDRESS]
                                ([Supplier_ID],[Salutation],[Name],[Address1],[Address2],[City],[State_ID],[Country_ID],[ZipCode],[Phone1],[Phone2],[Email],[Created_By],[Created_Date])
                                 VALUES
                                (@Supplier_ID,@Salutation,@Name,@Address1,@Address2,@City,@State_ID,@Country_ID,@ZipCode,@Phone1,@Phone2,@Email,@Created_By,GETUTCDATE())";
                    db.CreateParameters(13);
                    db.AddParameters(0, "@Supplier_ID", this.ID);
                    db.AddParameters(1, "@Salutation", this.Salutation);
                    db.AddParameters(2, "@Name", this.Name);
                    db.AddParameters(3, "@Address1", this.Address1);
                    db.AddParameters(4, "@Address2", this.Address2);
                    db.AddParameters(5, "@City", this.City);
                    db.AddParameters(6, "@State_ID", this.StateId);
                    db.AddParameters(7, "@Country_ID", this.CountryId);
                    db.AddParameters(8, "@ZipCode", this.ZipCode);
                    db.AddParameters(9, "@Phone1", this.Phone1);
                    db.AddParameters(10, "@Phone2", this.Phone2);
                    db.AddParameters(11, "@Email", this.Email);
                    db.AddParameters(12, "@Created_By", this.CreatedBy);
                    db.Open();
                    db.ExecuteQuery(CommandType.Text, query);
                    return new OutputMessage("Saved Successfully", true, Type.NoError, "Supplier | SaveAddress", System.Net.HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Supplier |  SaveSupplierAddress()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Updates customer address if it is primary address also upadates master table
        /// </summary>
        /// <returns></returns>
        public OutputMessage UpdateSupplierAddress()
        {
            DBManager db = new DBManager();
            try
            {
                if (String.IsNullOrWhiteSpace(this.Name))
                {
                    return new OutputMessage("Name cannot be empty", false, Type.RequiredFields, "Supplier | UpdateSupplierAddress", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (!String.IsNullOrWhiteSpace(this.Email)&&!Helper.IsValidEmail(this.Email))
                {
                    return new OutputMessage("Enter a valid Email", false, Type.RequiredFields, "Supplier | UpdateSupplierAddress", System.Net.HttpStatusCode.InternalServerError);
                }
                string query = "";
                if (this.IsPrimary)
                {
                    query = @"update TBL_SUPPLIER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Supplier_Address_ID=@Supplier_Address_ID
                            Update [dbo].[TBL_SUPPLIER_MST] set Address1=@Address1,Address2=@Address2,
                                 Country_Id=@Country_Id,State_Id=@State_Id,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,
                                 Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Salutation=@Salutation,ZipCode=@ZipCode,City=@City,Contact_Name=@Name where Supplier_Id=@Supplier_Id";
                }
                else
                {
                    query = @"update TBL_SUPPLIER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Supplier_Address_ID=@Supplier_Address_ID";
                }
                db.CreateParameters(14);
                db.AddParameters(0, "@Supplier_Address_ID", this.SupplierAddressID);
                db.AddParameters(1, "@Salutation", this.Salutation);
                db.AddParameters(2, "@Name", this.Name);
                db.AddParameters(3, "@Address1", this.Address1);
                db.AddParameters(4, "@Address2", this.Address2);
                db.AddParameters(5, "@City", this.City);
                db.AddParameters(6, "@State_ID", this.StateId);
                db.AddParameters(7, "@Country_ID", this.CountryId);
                db.AddParameters(8, "@ZipCode", this.ZipCode);
                db.AddParameters(9, "@Phone1", this.Phone1);
                db.AddParameters(10, "@Phone2", this.Phone2);
                db.AddParameters(11, "@Email", this.Email);
                db.AddParameters(12, "@Modified_By", this.ModifiedBy);
                db.AddParameters(13, "@Supplier_Id", this.ID);
                db.Open();
                db.ExecuteScalar(CommandType.Text, query);
                return new OutputMessage("Address Updated Successfully", true, Type.NoError, "Supplier | UpdateSupplierAddress", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Supplier |  UpdateSupplierAddress()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// For editing the customer address
        /// </summary>
        /// <param name="CustomerAddressID"></param>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Supplier GetSupplierAddressEdit(int SupplierAddressID, int SupplierID)
        {
            Entities.Supplier Sup = new Supplier();
            DBManager db = new DBManager();
            string query = "";
            query = @"select Supplier_Address_ID,Supplier_ID,Salutation,Name,Address1,Address2,City,Country_ID,State_ID,ZipCode,Phone1,Phone2,Email,is_primary from TBL_SUPPLIER_ADDRESS where Supplier_Address_ID=" + SupplierAddressID;
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    Sup.ID = dt.Rows[0]["Supplier_ID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Supplier_ID"]) : 0;
                    Sup.Salutation = Convert.ToString(dt.Rows[0]["Salutation"]);
                    Sup.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    Sup.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                    Sup.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                    Sup.City = Convert.ToString(dt.Rows[0]["City"]);
                    Sup.Phone1 = Convert.ToString(dt.Rows[0]["Phone1"]);
                    Sup.Phone2 = Convert.ToString(dt.Rows[0]["Phone2"]);
                    Sup.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    Sup.ZipCode = Convert.ToString(dt.Rows[0]["ZipCode"]);
                    Sup.StateId = dt.Rows[0]["State_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["State_Id"]) : 0;
                    Sup.CountryId = dt.Rows[0]["Country_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Country_Id"]) : 0;
                    Sup.SupplierAddressID = dt.Rows[0]["Supplier_Address_ID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Supplier_Address_ID"]) : 0;
                    Sup.IsPrimary = dt.Rows[0]["is_primary"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["is_primary"]) : false;
                }
                return Sup;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Supplier | GetSupplierAddressEdit(int SupplierAddressID,int SupplierID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Function used to set an address as primary address
        /// First updates isprimary to 0 of all address with customer ID.then updates only the selected customer addressID to 1 
        /// Then updates the address of the master table with new primary address
        /// </summary>
        /// <returns></returns>
        public OutputMessage SetAsPrimary()
        {
            Supplier sup = new Supplier();
            sup = Entities.Supplier.GetSupplierAddressEdit(this.SupplierAddressID, this.ID);
            DBManager db = new DBManager();
            try
            {
                string query = @"update TBL_SUPPLIER_ADDRESS set Is_Primary=0 where Supplier_ID=@SupplierID
                                  update TBL_SUPPLIER_ADDRESS set Is_Primary=1 where Supplier_Address_ID=@SupplierAddressID
                                  update TBL_SUPPLIER_MST set Salutation=@Salutation,Contact_Name=@Name,Address1=@Address1,Address2=@Address2,
                                  City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                  Email=@Email,Modified_By=@Modified_By where Supplier_Id=@SupplierID";
                db.CreateParameters(14);
                db.AddParameters(0, "@SupplierID", this.ID);
                db.AddParameters(1, "@SupplierAddressID", this.SupplierAddressID);
                db.AddParameters(2, "@Salutation", sup.Salutation);
                db.AddParameters(3, "@Name", sup.Name);
                db.AddParameters(4, "@Address1", sup.Address1);
                db.AddParameters(5, "@Address2", sup.Address2);
                db.AddParameters(6, "@City", sup.City);
                db.AddParameters(7, "@Country_ID", sup.CountryId);
                db.AddParameters(8, "@State_ID", sup.StateId);
                db.AddParameters(9, "@ZipCode", sup.ZipCode);
                db.AddParameters(10, "@Phone1", sup.Phone1);
                db.AddParameters(11, "@Phone2", sup.Phone2);
                db.AddParameters(12, "@Email", sup.Email);
                db.AddParameters(13, "@Modified_By", this.ModifiedBy);
                db.Open();
                db.ExecuteQuery(CommandType.Text, query);
                return new OutputMessage("Address is set to primary address", true, Type.NoError, "Supplier | SetAsPrimary", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex, "Supplier | SetAsPrimary()");
                return new OutputMessage("Something Went wrong.", false, Type.Others, "Supplier | SetAsPrimary", System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Function to delete the address from the address table
        /// </summary>
        /// <returns></returns>
        public OutputMessage DeleteAddress()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Suppliers, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Supplier | DeleteAddress", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_SUPPLIER_ADDRESS where Supplier_Address_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                        return new OutputMessage("Deleted Successfully", true, Type.Others, "Supplier | Delete", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("Id must not be zero for deleting", false, Type.Others, "Supplier | DeleteAddress", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {

                    db.RollBackTransaction();
                    Helper.LogException(ex, "Supplier | DeleteAddress()");
                    return new OutputMessage("Something went wrong. customer could not be deleted", false, Type.Others, "Supplier | DeleteAddress", System.Net.HttpStatusCode.InternalServerError, ex);


                }
                finally
                {

                    db.Close();

                }

            }
        }
    }
}
