using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Entities.Application;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Dynamic;
using System.IO;

namespace Entities
{
    public class Customer
    {
        #region Properties
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
        public decimal CreditAmount { get; set; }
        public decimal LockAmount { get; set; }
        public int CreditPeriod { get; set; }
        public int LockPeriod { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string currency { get; set; }
        public string Time { get; set; }
        public List<Job> Jobs { get; set; }
        public string Date { get; set; }
        public string Reference { get; set; }
       //Status for Lead
       //1-New Lead
       //2-Pending
       //3-Follow Up
       //4-Process
       //5-Convert to customer
       //6-Declined
        public int PrimaryStatus { get; set; }
        public int AssignId { get; set; }
        public string Assign { get; set; }
        public int Source { get; set; }
        public string Activity { get; set; }
        public string Url { get; set; }
        public string EntryDate { get; set; }
        public decimal TotalReceivable { get; set; }
        public string Salutation { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int CustomerAddressID { get; set; }
        public bool IsPrimary { get; set; }
        public string Contact_Name { get; set; }
        public int NoOfOrders { get; set; }
        public int NoOfBills { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileImageB64 { get; set; }
        #endregion Properties

        /// <summary>
        /// Retrieve Id and Name of Customers for populating dropdown list of customer
        /// </summary>
        /// <param name="CompanyId">company id of customer list</param>
        /// <returns>drop down list of Customer names</returns>
        public static DataTable GetCustomer(int CompanyId)

        {
            using (DBManager db = new DBManager())
            {

                try
                {
                    db.Open();
                    string query = @"SELECT [Customer_Id],[Name] FROM [dbo].[TBL_CUSTOMER_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Customer |  GetCustomer(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// Save details of each customers
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Customer, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Customer | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Customer name must not be empty", false, Type.RequiredFields, "Customer | Save", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {

                return new OutputMessage("Enter a valid email", false, Type.Others, "Customer | Save", System.Net.HttpStatusCode.InternalServerError);
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
                                string filePath = "Resources\\Customers\\ProfileImages";
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
                                this.ProfileImagePath = "/Resources/Customers/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        db.Open();
                        string query = @"insert into [dbo].[TBL_CUSTOMER_MST](Name,Address1,Address2,Country_Id,State_Id,Phone1,Phone2,Email,Taxno1,Taxno2,Currency_Id,
                                       Status,Created_By,Created_Date,Company_Id,Credit_Amount,Credit_Period,Lock_Amount,Lock_Period,Salutation,ZipCode,City,Contact_Name,Profile_Image_Path) 
                                       values(@Name,@Address1,@Address2,@Country_Id,@State_Id,@Phone1,@Phone2,@Email,@Taxno1,@Taxno2,@Currency_Id
                                       ,@Status,@Created_By,GETUTCDATE(),@Company_Id,@Credit_Amount,@Credit_Period,@Lock_Amount,@Lock_Period,@Salutation,@ZipCode,@City,@Contact_Name,@Profile_Image_Path);select @@identity";
                        db.CreateParameters(23);
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
                        db.AddParameters(14, "@Credit_Amount", this.CreditAmount);
                        db.AddParameters(15, "@Credit_Period", this.CreditPeriod);
                        db.AddParameters(16, "@Lock_Period", this.LockPeriod);
                        db.AddParameters(17, "@Lock_Amount", this.LockAmount);
                        db.AddParameters(18, "@Salutation", this.Salutation);
                        db.AddParameters(19, "@ZipCode", this.ZipCode);
                        db.AddParameters(20, "@City", this.City);
                        db.AddParameters(21, "@Contact_Name", this.Contact_Name);
                        db.AddParameters(22, "@Profile_Image_Path", this.ProfileImagePath);
                        db.BeginTransaction();
                        int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        query = @"INSERT INTO [dbo].[TBL_CUSTOMER_ADDRESS]
                                 ([Customer_ID],[Salutation],[Name],[Address1],[Address2],[City],[State_ID],[Country_ID],[ZipCode],[Phone1],[Phone2],[Email],[Is_Primary],[Created_By],[Created_Date])
                                VALUES
                                 (@Customer_ID,@Salutation,@Name,@Address1,@Address2,@City,@State_ID,@Country_ID,@ZipCode,@Phone1,@Phone2,@Email,1,@Created_By,GETUTCDATE());";
                        DataTable dt = db.ExecuteQuery(CommandType.Text, "select Customer_Id from TBL_CUSTOMER_MST where Customer_Id=" + identity);
                        db.CleanupParameters();
                        db.CreateParameters(13);
                        db.AddParameters(0, "@Customer_ID", identity);
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
                        return new OutputMessage("Customer saved successfully", true, Type.NoError, "Customer | Save", System.Net.HttpStatusCode.OK, new { Id = dt.Rows[0]["Customer_Id"] });

                    }
                    catch (Exception ex)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Customer could not be saved", false, Type.Others, "Customer | Save", System.Net.HttpStatusCode.InternalServerError, ex);

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
        /// Update details of each customer
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Customer, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Customer | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("ID must not be empty", false, Type.Others, "Customer | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.Others, "Customer | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {

                return new OutputMessage("Enter a valid Email", false, Type.Others, "Customer | Update", System.Net.HttpStatusCode.InternalServerError);
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
                                string filePath = "Resources\\Customers\\ProfileImages";
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
                                this.ProfileImagePath = "/Resources/Customers/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        db.Open();
                        string query = @"Update [dbo].[TBL_CUSTOMER_MST] set Name=@Name,Address1=@Address1,Address2=@Address2,
                                      Country_Id=@Country_Id,State_Id=@State_Id,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,Taxno1=@Taxno1,Taxno2=@Taxno2,
                       Currency_Id=@Currency_Id,Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Credit_Amount=@Credit_Amount,
                        Credit_Period=@Credit_Period,Lock_Period=@Lock_Period,Lock_Amount=@Lock_Amount,Salutation=@Salutation,ZipCode=@ZipCode,City=@City,Contact_Name=@Contact_Name,Profile_Image_Path=@Profile_Image_Path where Customer_Id=@id";
                        db.CreateParameters(23);
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
                        db.AddParameters(13, "@Credit_Amount", this.CreditAmount);
                        db.AddParameters(14, "@Credit_Period", this.CreditPeriod);
                        db.AddParameters(15, "@Lock_Period", this.LockPeriod);
                        db.AddParameters(16, "@Lock_Amount", this.LockAmount);
                        db.AddParameters(17, "@id", this.ID);
                        db.AddParameters(18, "@Salutation", this.Salutation);
                        db.AddParameters(19, "@ZipCode", this.ZipCode);
                        db.AddParameters(20, "@City", this.City);
                        db.AddParameters(21, "@Contact_Name", this.Contact_Name);
                        db.AddParameters(22, "@Profile_Image_Path", this.ProfileImagePath);
                        db.BeginTransaction();
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        query = @"update TBL_CUSTOMER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Customer_ID=@Customer_ID and Is_Primary=1";
                        db.CleanupParameters();
                        db.CreateParameters(13);
                        db.AddParameters(0, "@Customer_ID", this.ID);
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
                        return new OutputMessage("Details updated Successfully", true, Type.Others, "Customer | Update", System.Net.HttpStatusCode.OK);
                    }
                    catch (Exception ex)
                    {
                        db.RollBackTransaction();
                        Helper.LogException(ex, "Customer | Update()");
                        return new OutputMessage("Something went wrong. Customer could not be updated", false, Type.Others, "Customer | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }

        /// <summary>
        ///  Delete individual customer from customer master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Customer, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Customer | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_CUSTOMER_MST where Customer_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Customer deleted successfully", true, Type.Others, "Customer | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Customer could not be deleted", false, Type.Others, "Customer | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be zero for deleting", false, Type.Others, "Customer | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this customer because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "customer | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "customer | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Customer could not be deleted", false, Type.Others, "customer | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieve all customers from customer master
        /// </summary>
        /// <param name="CompanyId">company id customer list</param>
        /// <returns>list of customer details</returns>
        public static List<Customer> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select c.Customer_Id,c.Name,c.Address1,c.Address2,isnull(c.Country_Id,0)[Country_Id],isnull(c.State_Id,0)[State_Id],
                               c.Phone1,c.Phone2,c.Email,c.Taxno1,c.Taxno2,isnull(c.Currency_Id,0)[Currency_Id],
                               isnull( c.[Status],0)[Status],isnull(c.Created_By,0)[Created_By],c.Created_Date,isnull(c.Modified_By,0)[Modified_By],c.Modified_Date,
                               c.Company_Id,isnull(c.Credit_Amount,0)[Credit_Amount],isnull(c.Lock_Amount,0)[Lock_Amount],isnull(c.Credit_Period,0)[Credit_Period],
                               isnull(c.Lock_Period,0)[Lock_Period],co.Name[Country],s.Name[State],cu.Code[Currency],
                               f.Name[Company],c.Salutation,c.Zipcode,c.City from TBL_CUSTOMER_MST c
                               left join TBL_COUNTRY_MST co on c.Country_Id = co.Country_Id
                               left join TBL_STATE_MST s on s.State_Id = c.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id = c.Currency_Id
                               left join TBL_COMPANY_MST f on f.Company_Id = c.Company_Id where c.Company_Id=@Company_Id  order by Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Customer> result = new List<Customer>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Customer customer = new Customer();
                        customer.ID = (item["Customer_Id"] != DBNull.Value) ? Convert.ToInt32(item["Customer_Id"]) : 0;
                        customer.Name = Convert.ToString(item["Name"]);
                        customer.Address1 = Convert.ToString(item["Address1"]);
                        customer.Address2 = Convert.ToString(item["Address2"]);
                        customer.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                        customer.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                        customer.Phone1 = Convert.ToString(item["Phone1"]);
                        customer.Phone2 = Convert.ToString(item["Phone2"]);
                        customer.Email = Convert.ToString(item["Email"]);
                        customer.Taxno1 = Convert.ToString(item["Taxno1"]);
                        customer.Taxno2 = Convert.ToString(item["Taxno2"]);
                        customer.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                        customer.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        customer.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        customer.Country = Convert.ToString(item["Country"]);
                        customer.State = Convert.ToString(item["State"]);
                        customer.currency = Convert.ToString(item["currency"]);
                        customer.CreditAmount = item["Credit_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Credit_Amount"]) : 0;
                        customer.CreditPeriod = item["Credit_Period"] != DBNull.Value ? Convert.ToInt32(item["Credit_Period"]) : 0;
                        customer.LockAmount = item["Lock_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Lock_Amount"]) : 0;
                        customer.LockPeriod = item["Lock_Period"] != DBNull.Value ? Convert.ToInt32(item["Lock_Period"]) : 0;
                        customer.City = Convert.ToString(item["City"]);
                        customer.Salutation = Convert.ToString(item["Salutation"]);
                        customer.ZipCode = Convert.ToString(item["Zipcode"]);
                        result.Add(customer);
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
                Application.Helper.LogException(ex, "Customer |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve all customers name from master for customer list page
        /// </summary>
        /// <param name="CompanyId">company id customer list</param>
        /// <returns>list of customer details</returns>
        public static List<Customer> GetCustomerDetails(int CompanyId, int? Customer_id)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select c.Customer_Id,c.Name,f.Name [company],c.Email,c.Phone1,(select isnull(sum([pending amount]),0) from Report_PendingCustomerPayments where Customer_Id=c.Customer_Id) [Total_Receivable] from TBL_CUSTOMER_MST c
                     left join TBL_COMPANY_MST f on f.Company_Id = c.Company_Id where c.Company_Id=@Company_Id  {#customerfilter#}
                     order by c.Created_Date desc";

                if (Customer_id != null && Customer_id > 0)
                {
                    query = query.Replace("{#customerfilter#}", " and c.Customer_Id=@customer_id ");
                }
                else
                {

                    query = query.Replace("{#customerfilter#}", string.Empty);
                }
                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@customer_id", Customer_id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Customer> result = new List<Customer>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Customer customer = new Customer();
                        customer.ID = (item["Customer_Id"] != DBNull.Value) ? Convert.ToInt32(item["Customer_Id"]) : 0;
                        customer.Name = Convert.ToString(item["Name"]);
                        customer.Company = Convert.ToString(item["company"]);
                        customer.Email = Convert.ToString(item["Email"]);
                        customer.Phone1 = Convert.ToString(item["Phone1"]);
                        customer.TotalReceivable = Convert.ToDecimal(item["Total_Receivable"]);
                        result.Add(customer);
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
                Application.Helper.LogException(ex, "Customer |  GetCustomerDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single customer from list of customers
        /// </summary>
        /// <param name="Id">id of the particular customer which u want to retrieve</param>
        /// <param name="CompanyId">company id of the particular customer</param>
        /// <returns> single customer details</returns>
        public static Customer GetDetails(int CompanyId, int Id)
        {
            DBManager db = new DBManager();
            try
            {

                db.Open();
                string query = @"select top 1 c.Customer_Id,c.Name,c.Address1,c.Address2,isnull(c.Country_Id,0)[Country_Id],isnull(c.State_Id,0)[State_Id],
                               c.Phone1,c.Phone2,c.Email,c.Taxno1,c.Taxno2,isnull( c.Currency_Id,0)[Currency_Id],
                               isnull( c.[Status],0)[Status],isnull(c.Created_By,0)[Created_By], c.Created_Date,isnull(c.Modified_By,0)[Modified_By],c.Modified_Date,
                               isnull(c.Company_Id,0)[Company_Id],isnull(c.Credit_Amount,0)[Credit_Amount],isnull(c.Lock_Amount,0)[Lock_Amount],isnull(c.Credit_Period,0)[Credit_Period],
                               isnull(c.Lock_Period,0)[Lock_Period],co.Name[Country],s.Name[State],cu.Code[Currency],
                               f.Name[Company],c.Salutation,c.Zipcode,c.City,c.Contact_Name,c.Profile_Image_Path from TBL_CUSTOMER_MST c
                               left join TBL_COUNTRY_MST co on c.Country_Id = co.Country_Id
                               left join TBL_STATE_MST s on s.State_Id = c.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id = c.Currency_Id
                               left join TBL_COMPANY_MST f on f.Company_Id = c.Company_Id where c.Company_Id=@Company_Id and Customer_Id=@id;
                               select * from tbl_job_mst where Customer_Id=@id;";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                Customer customer = new Customer();

                if (ds.Tables[0] != null)
                {
                    DataRow item = ds.Tables[0].Rows[0];
                    customer.ID = (item["Customer_Id"] != DBNull.Value) ? Convert.ToInt32(item["Customer_Id"]) : 0;
                    customer.Name = Convert.ToString(item["Name"]);
                    customer.Address1 = Convert.ToString(item["Address1"]);
                    customer.Address2 = Convert.ToString(item["Address2"]);
                    customer.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                    customer.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                    customer.Phone1 = Convert.ToString(item["Phone1"]);
                    customer.Phone2 = Convert.ToString(item["Phone2"]);
                    customer.Email = Convert.ToString(item["Email"]);
                    customer.Taxno1 = Convert.ToString(item["Taxno1"]);
                    customer.Taxno2 = Convert.ToString(item["Taxno2"]);
                    customer.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                    customer.Status = Convert.ToInt32(item["Status"]);
                    customer.CreatedBy = Convert.ToInt32(item["Created_By"]);
                    customer.Country = Convert.ToString(item["Country"]);
                    customer.State = Convert.ToString(item["State"]);
                    customer.currency = Convert.ToString(item["currency"]);
                    customer.CreditAmount = item["Credit_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Credit_Amount"]) : 0;
                    customer.CreditPeriod = item["Credit_Period"] != DBNull.Value ? Convert.ToInt32(item["Credit_Period"]) : 0;
                    customer.LockAmount = item["Lock_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Lock_Amount"]) : 0;
                    customer.LockPeriod = item["Lock_Period"] != DBNull.Value ? Convert.ToInt32(item["Lock_Period"]) : 0;
                    customer.City = Convert.ToString(item["City"]);
                    customer.Salutation = Convert.ToString(item["Salutation"]);
                    customer.ZipCode = Convert.ToString(item["Zipcode"]);
                    customer.Contact_Name = Convert.ToString(item["Contact_Name"]);
                    customer.ProfileImagePath = Convert.ToString(item["profile_image_path"]);
                    customer.Jobs = new List<Job>();
                    if (ds.Tables[1] != null)
                    {
                        foreach (DataRow job in ds.Tables[1].Rows)
                        {
                            Job j = new Job();
                            j.ID = Convert.ToInt32(job["Job_Id"]);
                            j.JobName = Convert.ToString(job["Job_Name"]);
                            customer.Jobs.Add(j);
                        }

                    }


                }

                return customer;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Customer | GetDetails(int CompanyId, int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// Retrievecustomer wise activity
        /// </summary>
        /// <param name="Id">id of the particular customer which u want to retrieve</param>
        /// <returns> single customer details</returns>
        public static List<Customer> GetCustomerActivity(int Id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                #region query
                string query = @";with cte as (
                                 select top 5 sr.[Created_Date][Date],sr.Request_Date [entry_date],CONVERT(varchar(15),cast(sr.[Created_Date] as time),100)[Time],Request_No[Reference],Customer_Name[party],
                                 'SR'[Code],'#c45850'[CodeColor],'New Sales Request added-<strong>'+Customer_Name+'</strong>.'[Activity],us.Full_Name[User] 
                                 ,'/Sales/Request?MODE=edit&UID='+convert(varchar(20),sr.Sr_Id)[Url] from TBL_SALES_REQUEST_REGISTER sr left join TBL_USER_MST us on sr.created_by=us. [User_Id] where sr.Customer_Id=@Customer_Id order by sr.Created_Date desc
                                 ),                                                     
                                 cte4 as (
                                 select top 5 se.Created_Date[Date],se.Sales_Date [entry_date],CONVERT(varchar(15),cast(se.Created_Date as time),100)[Time],se.Sales_Bill_No[Reference],
                                 l.Name[Name],'SE'[Code],'#800000'[CodeColor],'Invoice added -<strong>' +c.Name+'</strong>'[Activity],
                                 u.Full_Name[User],'/Sales/Entry?MODE=edit&UID='+convert(varchar(20),se.Se_Id)[Url] from TBL_SALES_ENTRY_REGISTER se
                                  left join TBL_LOCATION_MST l on l.Location_Id=se.Location_Id
                                  left join TBL_USER_MST u on u.[User_Id]=se.Created_By
                                  left join TBL_CUSTOMER_MST c on c.Customer_Id=se.Customer_Id  where se.Customer_Id=@Customer_Id
                                 order by se.Created_Date desc
                                  ),
                                  cte5 as (
                                  select top 5 sq.Created_Date[Date],sq.Entry_Date,CONVERT(varchar(15),cast(sq.Created_Date as time),100)[Time],sq.Quote_No[Reference],
                                  l.Name[Name],'SQ'[Code],'#808000'[CodeColor],'New sales order has been drafted against <strong>'+c.Name+'</strong>'[Activity],
                                  u.Full_Name[User],'/Sales/Quote?MODE=edit&UID='+convert(varchar(20),sq.Sq_Id)[Url] from TBL_SALES_QUOTE_REGISTER sq 
                                  left join TBL_LOCATION_MST l on l.Location_Id=sq.Location_Id
                                  left join TBL_USER_MST u on u.[User_Id]=sq.Created_By
                                  left join TBL_CUSTOMER_MST c on c.Customer_Id=sq.Customer_Id  where sq.Customer_Id=@Customer_Id
                                   order by sq.Created_Date desc
                                  ),
                                 cte6 as(
                                 select * from cte 
                                 union all     
                                 select * from cte4
                                 union all
                                 select * from cte5
                                 )
                                 select * from cte6 order by [date] desc ";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Customer_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                query = @"select count(*) bills from TBL_SALES_ENTRY_REGISTER where customer_id=@customer_id
                          select count(*) orders from TBL_SALES_QUOTE_REGISTER where customer_id=@customer_id";
                db.CleanupParameters();
                db.CreateParameters(1);
                db.AddParameters(0, "@customer_id", Id);
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(CommandType.Text, query);
                List<Customer> result = new List<Customer>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Customer customer = new Customer();
                        customer.Name = Convert.ToString(item["party"]);
                        customer.Reference = Convert.ToString(item["reference"]);
                        customer.Activity = Convert.ToString(item["Activity"]);
                        customer.Url = Convert.ToString(item["url"]);
                        customer.Date = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["date"])).ToString("dd MMM yyyy");
                        customer.EntryDate = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["entry_date"])).ToString("dd MMM yyyy");
                        customer.Time = Application.Localisation.GetLocalDate(Convert.ToDateTime(item["date"])).ToString("hh:mm tt");
                        customer.NoOfBills = Convert.ToInt32(ds.Tables[0].Rows[0]["bills"]);
                        customer.NoOfOrders = Convert.ToInt32(ds.Tables[1].Rows[0]["orders"]);
                        result.Add(customer);
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
                Application.Helper.LogException(ex, "Customer | GetDetails(int CompanyId, int Id)");
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
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static List<Customer> GetCustomerAddress(int CustomerID)
        {
            DBManager db = new DBManager();
            try
            {
                List<Customer> listcust = new List<Customer>();
                string query = @"select Customer_address_ID,Customer_id,Salutation,cust.Name,Address1,Address2,City,cust.Phone1,cust.Phone2,cust.Email,st.Name State,Co.Name Country,cust.State_Id,cust.Country_Id,cust.Zipcode,cust.Is_Primary from tbl_customer_address cust
                                 left join TBL_STATE_MST st on st.State_Id=cust.State_Id
                                 left join TBL_COUNTRY_MST co on co.Country_Id=cust.Country_Id where customer_id=@customer";

                DataTable dt = new DataTable();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@customer", CustomerID);
                dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Customer cust = new Customer();
                        cust.ID = dt.Rows[i]["Customer_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Customer_id"]) : 0;
                        cust.Salutation = Convert.ToString(dt.Rows[i]["Salutation"]);
                        cust.Name = Convert.ToString(dt.Rows[i]["Name"]);
                        cust.Address1 = Convert.ToString(dt.Rows[i]["Address1"]);
                        cust.Address2 = Convert.ToString(dt.Rows[i]["Address2"]);
                        cust.City = Convert.ToString(dt.Rows[i]["City"]);
                        cust.Phone1 = Convert.ToString(dt.Rows[i]["Phone1"]);
                        cust.Phone2 = Convert.ToString(dt.Rows[i]["Phone2"]);
                        cust.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        cust.State = Convert.ToString(dt.Rows[i]["State"]);
                        cust.Country = Convert.ToString(dt.Rows[i]["Country"]);
                        cust.ZipCode = Convert.ToString(dt.Rows[i]["ZipCode"]);
                        cust.StateId = dt.Rows[i]["State_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["State_Id"]) : 0;
                        cust.CountryId = dt.Rows[i]["Country_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Country_Id"]) : 0;
                        cust.CustomerAddressID = dt.Rows[i]["Customer_address_ID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["Customer_address_ID"]) : 0;
                        cust.IsPrimary = dt.Rows[i]["Is_Primary"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[i]["Is_Primary"]) : false;
                        listcust.Add(cust);
                    }
                }
                return listcust;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Customer |  GetCustomerAddress(int CustomerID)");
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
        public OutputMessage SaveCustomerAddress()
        {
            DBManager db = new DBManager();
            try
            {
                if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
                {
                    return new OutputMessage("Enter a valid email", false, Type.Others, "Customer | AddressSave", System.Net.HttpStatusCode.InternalServerError);
                }
                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    string query = @"INSERT INTO [dbo].[TBL_CUSTOMER_ADDRESS]
                                 ([Customer_ID],[Salutation],[Name],[Address1],[Address2],[City],[State_ID],[Country_ID],[ZipCode],[Phone1],[Phone2],[Email],[Created_By],[Created_Date])
                                VALUES
                                 (@Customer_ID,@Salutation,@Name,@Address1,@Address2,@City,@State_ID,@Country_ID,@ZipCode,@Phone1,@Phone2,@Email,@Created_By,GETUTCDATE());select @@Identity";
                    db.CreateParameters(13);
                    db.AddParameters(0, "@Customer_ID", this.ID);
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
                    int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    if (identity > 0)
                    {
                        return new OutputMessage("Address added successfully", true, Type.NoError, "Customer | AddressSave", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("Failed to save", false, Type.Others, "Customer | AddressSave", System.Net.HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    return new OutputMessage("Contact Name Cannot be empty", false, Type.Others, "Customer | AddressSave", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Customer |  SaveCustomerAddress()");
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
        public static Customer GetCustomerAddressEdit(int CustomerAddressID, int CustomerID)
        {
            Entities.Customer Cust = new Customer();
            DBManager db = new DBManager();
            string query = "";
            query = @"select Customer_Address_ID,Customer_ID,Salutation,Name,Address1,Address2,City,Country_ID,State_ID,ZipCode,Phone1,Phone2,Email,is_primary from TBL_CUSTOMER_ADDRESS where Customer_Address_ID=" + CustomerAddressID;
            DataTable dt = new DataTable();
            db.Open();
            try
            {
                dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    Cust.ID = dt.Rows[0]["Customer_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Customer_id"]) : 0;
                    Cust.Salutation = Convert.ToString(dt.Rows[0]["Salutation"]);
                    Cust.Name = Convert.ToString(dt.Rows[0]["Name"]);
                    Cust.Address1 = Convert.ToString(dt.Rows[0]["Address1"]);
                    Cust.Address2 = Convert.ToString(dt.Rows[0]["Address2"]);
                    Cust.City = Convert.ToString(dt.Rows[0]["City"]);
                    Cust.Phone1 = Convert.ToString(dt.Rows[0]["Phone1"]);
                    Cust.Phone2 = Convert.ToString(dt.Rows[0]["Phone2"]);
                    Cust.Email = Convert.ToString(dt.Rows[0]["Email"]);
                    Cust.ZipCode = Convert.ToString(dt.Rows[0]["ZipCode"]);
                    Cust.StateId = dt.Rows[0]["State_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["State_Id"]) : 0;
                    Cust.CountryId = dt.Rows[0]["Country_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Country_Id"]) : 0;
                    Cust.CustomerAddressID = dt.Rows[0]["Customer_address_ID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Customer_address_ID"]) : 0;
                    Cust.IsPrimary = dt.Rows[0]["is_primary"] != DBNull.Value ? Convert.ToBoolean(dt.Rows[0]["is_primary"]) : false;
                }
                return Cust;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Customer | GetCustomerAddressEdit(int CustomerAddressID,int CustomerID)");
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
        public OutputMessage UpdateCustomerAddress()
        {
            DBManager db = new DBManager();
            try
            {
                if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
                {
                    return new OutputMessage("Enter a valid email", false, Type.Others, "Customer | UpdateCustomerAddress", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (!String.IsNullOrWhiteSpace(this.Name))
                {
                    string query = "";
                    if (this.IsPrimary)
                    {
                        query = @"update TBL_CUSTOMER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Customer_Address_ID=@Customer_Address_ID
                            Update [dbo].[TBL_CUSTOMER_MST] set Address1=@Address1,Address2=@Address2,
                                 Country_Id=@Country_Id,State_Id=@State_Id,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,
                                 Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Salutation=@Salutation,ZipCode=@ZipCode,City=@City,Contact_Name=@Name where Customer_Id=@Customer_Id";
                    }
                    else
                    {
                        query = @"update TBL_CUSTOMER_ADDRESS set Salutation=@Salutation,Name=@Name,Address1=@Address1,Address2=@Address2,
                                 City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                 Email=@Email,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Customer_Address_ID=@Customer_Address_ID";
                    }
                    db.CreateParameters(14);
                    db.AddParameters(0, "@Customer_Address_ID", this.CustomerAddressID);
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
                    db.AddParameters(13, "@Customer_ID", this.ID);
                    db.Open();
                    db.ExecuteScalar(CommandType.Text, query);
                    return new OutputMessage("Address Updated successfully", true, Type.NoError, "Customer | UpdateCustomerAddress", System.Net.HttpStatusCode.OK);
                } 
                else
                {
                    return new OutputMessage("Please check the details", false, Type.Others, "Customer | UpdateCustomerAddress", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Customer |  UpdateCustomerAddress()");
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
            Customer cust = new Customer();
            cust = Entities.Customer.GetCustomerAddressEdit(this.CustomerAddressID, this.ID);
            DBManager db = new DBManager();
            try
            {
                string query = @"update TBL_CUSTOMER_ADDRESS set Is_Primary=0 where Customer_ID=@CustomerID
                                  update TBL_CUSTOMER_ADDRESS set Is_Primary=1 where Customer_Address_ID=@CustomerAddressID
                                  update TBL_CUSTOMER_MST set Salutation=@Salutation,Contact_Name=@Name,Address1=@Address1,Address2=@Address2,
                                  City=@City,Country_ID=@Country_ID,State_ID=@State_ID,ZipCode=@ZipCode,Phone1=@Phone1,Phone2=@Phone2,
                                  Email=@Email,Modified_By=@Modified_By where Customer_Id=@CustomerID";
                db.CreateParameters(14);
                db.AddParameters(0, "@CustomerID", this.ID);
                db.AddParameters(1, "@CustomerAddressID", this.CustomerAddressID);
                db.AddParameters(2, "@Salutation", cust.Salutation);
                db.AddParameters(3, "@Name", cust.Name);
                db.AddParameters(4, "@Address1", cust.Address1);
                db.AddParameters(5, "@Address2", cust.Address2);
                db.AddParameters(6, "@City", cust.City);
                db.AddParameters(7, "@Country_ID", cust.CountryId);
                db.AddParameters(8, "@State_ID", cust.StateId);
                db.AddParameters(9, "@ZipCode", cust.ZipCode);
                db.AddParameters(10, "@Phone1", cust.Phone1);
                db.AddParameters(11, "@Phone2", cust.Phone2);
                db.AddParameters(12, "@Email", cust.Email);
                db.AddParameters(13, "@Modified_By", this.ModifiedBy);
                db.Open();
                db.ExecuteQuery(CommandType.Text, query);
                return new OutputMessage("Address is set to primary address", true, Type.NoError, "Customer | SetAsPrimary", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex, "Customer | SetAsPrimary()");
                return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Customer | SetAsPrimary", System.Net.HttpStatusCode.InternalServerError);
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
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Customer, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Customer | DeleteAddress", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_CUSTOMER_ADDRESS where Customer_Address_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                        return new OutputMessage("Address deleted successfully", true, Type.Others, "Customer | DeleteAddress", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("Id must not be zero for deleting", false, Type.Others, "Customer | DeleteAddress", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {

                    db.RollBackTransaction();
                    Helper.LogException(ex, "Customer | DeleteAddress()");
                    return new OutputMessage("Something went wrong. Address could not be deleted", false, Type.Others, "customer | DeleteAddress", System.Net.HttpStatusCode.InternalServerError, ex);


                }
                finally
                {

                    db.Close();

                }

            }
        }
    }
}
