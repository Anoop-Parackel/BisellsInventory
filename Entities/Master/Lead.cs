using Core.DBManager;
using Entities.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
    public class Lead
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
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }
        public string State { get; set; }
        //Status for Lead
        //1-New Lead
        //2-Pending
        //3-Follow Up
        //4-Process
        //5-Declined
        //6-Converted to customer
        public int Status { get; set; }
        public int AssignId { get; set; }
        public string Assign { get; set; }
        //Lead source 
        //1-Daddy street
        //2-Facebook
        //3-Tele Sales
        //4-Twitter
        //5-Management
        public int Source { get; set; }
        public string Salutation { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string ContactName { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileImageB64 { get; set; }
        #endregion Properties

        #region Functions
        /// <summary>
        /// Function for saving leads
        /// </summary>
        /// <returns>Return success alert when details inserted successfully otherwise returns an error alert</returns>
        public OutputMessage SaveLeads()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Leads, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Lead name must not be empty", false, Type.RequiredFields, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.AssignId == 0)
            {
                return new OutputMessage("Assign Id must not be empty", false, Type.RequiredFields, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {

                return new OutputMessage("Enter a valid Email", false, Type.Others, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);
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
                                string filePath = "Resources\\Leads\\ProfileImages";
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
                                this.ProfileImagePath = "/Resources/Leads/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        string query = @"insert into TBL_LEADS(Name,Address1,Address2,Country_Id,State_Id,Phone1,Phone2,Email,TaxNo1,
                                       TaxNo2,Primary_Status,Assign,[Source],Company_Id,Created_By,Created_Date,Salutation,  
                                       City,Contact_Name,Zip_Code,Details,Profile_Image_Path)
                                       values(@Name,@Address1,@Address2,@Country_Id,@State_Id,@Phone1,@Phone2,@Email,@TaxNo1,
                                       @TaxNo2,@Primary_Status, @Assign,@Source,@Company_Id,@Created_By,GETUTCDATE(),@Salutation,  
                                       @City,@Contact_Name,@Zip_Code,@Details,@Profile_Image_Path)";
                        db.CreateParameters(21);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Country_Id", this.CountryId);
                        db.AddParameters(4, "@State_Id", this.StateId);
                        db.AddParameters(5, "@Phone1", this.Phone1);
                        db.AddParameters(6, "@Phone2", this.Phone2);
                        db.AddParameters(7, "@Email", this.Email);
                        db.AddParameters(8, "@TaxNo1", this.Taxno1);
                        db.AddParameters(9, "@TaxNo2", this.Taxno2);
                        db.AddParameters(10, "@Primary_Status", this.Status);
                        db.AddParameters(11, "@Assign", this.AssignId);
                        db.AddParameters(12, "@Source", this.Source);
                        db.AddParameters(13, "@Company_Id", this.CompanyId);
                        db.AddParameters(14, "@Created_By", this.CreatedBy);
                        db.AddParameters(15, "@Salutation", this.Salutation);
                        db.AddParameters(16, "@City", this.City);
                        db.AddParameters(17, "@Contact_Name", this.ContactName);
                        db.AddParameters(18, "@Zip_Code", this.ZipCode);
                        db.AddParameters(19, "@Details", this.Details);
                        db.AddParameters(20, "@Profile_Image_Path", this.ProfileImagePath);
                        db.Open();
                        int noOfRowsAffected = db.ExecuteNonQuery(CommandType.Text, query);
                        if (noOfRowsAffected >= 1)
                        {
                            return new OutputMessage("Leads saved successfully", true, Type.NoError, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Couldnot save leads", false, Type.Others, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Couldnot save leads", false, Type.Others, "Lead | SaveLeads", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Function for updating leads
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return an error message</returns>
        public OutputMessage UpdateLeads()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Leads, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("ID must not be empty", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.AssignId == 0)
            {
                return new OutputMessage("Assign Id must not be empty", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!string.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
            {

                return new OutputMessage("Enter a valid Email", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);
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
                                string filePath = "Resources\\Leads\\ProfileImages";
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
                                this.ProfileImagePath = "/Resources/Leads/ProfileImages/" + file;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        db.CreateParameters(21);
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
                        db.AddParameters(10, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(11, "@id", this.ID);
                        db.AddParameters(12, "@Primary_Status", this.Status);
                        db.AddParameters(13, "@Assign", this.AssignId);
                        db.AddParameters(14, "@Source", this.Source);
                        db.AddParameters(15, "@Salutation", this.Salutation);
                        db.AddParameters(16, "@City", this.City);
                        db.AddParameters(17, "@Contact_Name", this.ContactName);
                        db.AddParameters(18, "@Zip_Code", this.ZipCode);
                        db.AddParameters(19, "@Details", this.Details);
                        db.AddParameters(20, "@Profile_Image_Path", this.ProfileImagePath);
                        string query = @"select Primary_Status from tbl_leads where Lead_Id=@id";
                        db.Open();
                        int status = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        if (status == 6)
                        {
                            return new OutputMessage("This lead already converted to customer.So you cannot update your lead Status", false, Type.NoError, "Lead | UpdateStatus", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                         query = @"Update [dbo].[TBL_LEADS] set Name=@Name,Address1=@Address1,Address2=@Address2,
                                       Country_Id=@Country_Id,State_Id=@State_Id,Phone1=@Phone1,Phone2=@Phone2,Email=@Email,
                                       Taxno1=@Taxno1,Taxno2=@Taxno2,Modified_By=@Modified_By,
                                       Modified_Date=GETUTCDATE(),Primary_Status=@Primary_Status,Assign=@Assign,[Source]=@Source,
                                       Salutation=@Salutation,City=@City,Contact_Name=@Contact_Name,Zip_Code=@Zip_Code,Details=@Details,Profile_Image_Path=@Profile_Image_Path where Lead_Id=@id";
                       

                        bool result = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (result)
                        {
                            return new OutputMessage("Details updated Successfully", true, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Leads could not be updated", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError);

                        }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Leads could not be updated", false, Type.Others, "Lead | UpdateLeads", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Function for deleting leads
        /// Id must not be zero for deleting an entry
        /// </summary>
        /// <returns>return successalert when details deleted successfully otherwise return an error alert</returns>
        public OutputMessage DeleteLeads()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Leads, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_LEADS where Lead_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Lead Deleted Successfully", true, Type.Others, "Lead | DeleteLeads", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Lead could not be Deleted", false, Type.Others, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be zero for deleting", false, Type.Others, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError);

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
                            return new OutputMessage("You cannot delete this customer because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Lead could not be deleted", false, Type.Others, "Lead | DeleteLeads", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Function for retrieving a list of leads
        /// </summary>
        /// <param name="CompanyId">Id of the company</param>
        /// <returns>list of leads</returns>
        public static List<Lead> GetDetailsForLeads(int? CompanyId,int? Status, int? EmployeeId,DateTime? From,DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select le.Lead_Id,le.Name,le.Address1,le.Address2,isnull(le.Country_Id,0)[Country_Id],
                               isnull(le.State_Id,0)[State_Id],le.Phone1,le.Phone2,le.Email,le.Taxno1,le.Taxno2,
                               isnull(le.Currency_Id,0)[Currency_Id],le.Primary_Status,le.Assign,le.[Source],  
                               isnull(le.Created_By,0)[Created_By],le.Created_Date,isnull(le.Modified_By,0)[Modified_By],
                               le.Modified_Date, le.Company_Id,co.Name[Country],le.Salutation,le.City,le.Contact_Name,le.Zip_Code,
                               s.Name[State],cu.Code[Currency],f.Name[Company],em.First_Name[Employee],le.Details
                               from TBL_LEADS le
                               left join TBL_COUNTRY_MST co on le.Country_Id = co.Country_Id
							   left join TBL_EMPLOYEE_MST em on em.employee_Id=le.Assign
                               left join TBL_STATE_MST s on s.State_Id = le.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id = le.Currency_Id
                               left join TBL_COMPANY_MST f on f.Company_Id = le.Company_Id 
                               where le.Company_Id=@Company_Id {#employeefilter#} {#statusfilter#} {#daterangefilter#} order by le.Created_Date desc";
                if (Status > 0 && Status != null)
                {
                    query = query.Replace("{#statusfilter#}", " and le.Primary_Status=@Primary_Status");
                }
                else
                {
                    query = query.Replace("{#statusfilter#}", string.Empty);
                }
                if (EmployeeId > 0 && EmployeeId != null)
                {
                    query = query.Replace("{#employeefilter#}", "and le.Assign=@Assign ");
                }
                else
                {

                    query = query.Replace("{#employeefilter#}", string.Empty);
                }
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and convert(date,le.Created_Date)>=@fromdate and convert(date,le.Created_Date)<= @todate ");
                }
                else
                {
                    To = DateTime.UtcNow;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and le.Created_Date>=@fromdate and le.Created_Date<=@todate ");
                }
                db.CreateParameters(5);
                db.AddParameters(0, "@Assign", EmployeeId);
                db.AddParameters(1, "@Primary_Status", Status);
                db.AddParameters(2, "@Company_Id", CompanyId);
                db.AddParameters(3, "@fromdate", From.Value);
                db.AddParameters(4, "@todate", To.Value);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Lead> result = new List<Lead>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Lead lead = new Lead();
                        lead.ID = (item["Lead_Id"] != DBNull.Value) ? Convert.ToInt32(item["Lead_Id"]) : 0;
                        lead.Name = Convert.ToString(item["Name"]);
                        lead.Address1 = Convert.ToString(item["Address1"]);
                        lead.Address2 = Convert.ToString(item["Address2"]);
                        lead.Details = Convert.ToString(item["Details"]);
                        lead.City = Convert.ToString(item["City"]);
                        lead.Salutation = Convert.ToString(item["Salutation"]);
                        lead.ContactName = Convert.ToString(item["Contact_Name"]);
                        lead.ZipCode = Convert.ToString(item["Zip_Code"]);
                        lead.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                        lead.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                        lead.Phone1 = Convert.ToString(item["Phone1"]);
                        lead.Phone2 = Convert.ToString(item["Phone2"]);
                        lead.Email = Convert.ToString(item["Email"]);
                        lead.Taxno1 = Convert.ToString(item["Taxno1"]);
                        lead.Taxno2 = Convert.ToString(item["Taxno2"]);
                        lead.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        lead.Country = Convert.ToString(item["Country"]);
                        lead.State = Convert.ToString(item["State"]);
                        lead.Status = item["Primary_Status"] != DBNull.Value ? Convert.ToInt32(item["Primary_Status"]) : 0;
                        lead.AssignId = item["Assign"] != DBNull.Value ? Convert.ToInt32(item["Assign"]) : 0;
                        lead.Assign = Convert.ToString(item["Employee"]);
                        lead.Source = item["Source"] != DBNull.Value ? Convert.ToInt32(item["Source"]) : 0;
                        result.Add(lead);
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
                Application.Helper.LogException(ex, "Lead |  GetDetailsForLeads(int? CompanyId,int? Status, int? EmployeeId,DateTime? From,DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for retrieving a single lead from the list
        /// </summary>
        /// <param name="CompanyId">Id of the company</param>
        /// <param name="Id">Id of that particular lead you want to retrieve</param>
        /// <returns>single lead</returns>
        public static Lead GetDetailsForLeads(int CompanyId, int Id)
        {
            DBManager db = new DBManager();
            try
            {

                db.Open();
                string query = @"select top 1 le.Lead_Id,le.Name,le.Address1,le.Address2,isnull(le.Country_Id,0)[Country_Id],
                               isnull(le.State_Id,0)[State_Id],le.Phone1,le.Phone2,le.Email,le.Taxno1,le.Taxno2,
                               isnull( le.Currency_Id,0)[Currency_Id],le.Primary_Status,le.Assign,le.[Source],
                               isnull(le.Created_By,0)[Created_By], le.Created_Date,isnull(le.Modified_By,0)[Modified_By],
                               le.Modified_Date,isnull(le.Company_Id,0)[Company_Id],co.Name[Country],s.Name[State],cu.Code[Currency],
                               f.Name[Company],le.Salutation,le.City,le.Contact_Name,le.Zip_Code,le.Details,le.Profile_Image_Path
							   from TBL_LEADS le
                               left join TBL_COUNTRY_MST co on le.Country_Id = co.Country_Id
                               left join TBL_STATE_MST s on s.State_Id = le.State_Id
                               left join TBL_CURRENCY_MST cu on cu.Currency_Id = le.Currency_Id
                               left join TBL_COMPANY_MST f on f.Company_Id = le.Company_Id 
                               where le.Company_Id=@Company_Id and le.Lead_Id=@id;";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                Lead lead = new Lead();
                if (ds.Tables[0] != null)
                {
                    DataRow item = ds.Tables[0].Rows[0];
                    lead.ID = (item["Lead_Id"] != DBNull.Value) ? Convert.ToInt32(item["Lead_Id"]) : 0;
                    lead.Name = Convert.ToString(item["Name"]);
                    lead.Address1 = Convert.ToString(item["Address1"]);
                    lead.Address2 = Convert.ToString(item["Address2"]);
                    lead.Details = Convert.ToString(item["Details"]);
                    lead.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                    lead.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                    lead.Phone1 = Convert.ToString(item["Phone1"]);
                    lead.Phone2 = Convert.ToString(item["Phone2"]);
                    lead.Email = Convert.ToString(item["Email"]);
                    lead.Taxno1 = Convert.ToString(item["Taxno1"]);
                    lead.Taxno2 = Convert.ToString(item["Taxno2"]);
                    lead.City = Convert.ToString(item["City"]);
                    lead.Salutation = Convert.ToString(item["Salutation"]);
                    lead.ContactName = Convert.ToString(item["Contact_Name"]);
                    lead.ZipCode = Convert.ToString(item["Zip_Code"]);
                    lead.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                    lead.Country = Convert.ToString(item["Country"]);
                    lead.State = Convert.ToString(item["State"]);
                    lead.Status = item["Primary_Status"] != DBNull.Value ? Convert.ToInt32(item["Primary_Status"]) : 0;
                    lead.AssignId = item["Assign"] != DBNull.Value ? Convert.ToInt32(item["Assign"]) : 0;
                    lead.Source = item["Source"] != DBNull.Value ? Convert.ToInt32(item["Source"]) : 0;
                    lead.ProfileImagePath = Convert.ToString(item["profile_image_path"]);
                }
                return lead;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Lead |GetDetailsForLeads(int CompanyId, int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for changing primary status of lead
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return an error alert</returns>
        public OutputMessage UpdateStatus()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Leads, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Lead | UpdateStatus", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                if (this.Status == 0)
                {
                    return new OutputMessage("Select a status to update", false, Type.Others, "Lead | UpdateStatus", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.CreateParameters(3);
                    db.AddParameters(0, "@Primary_Status", this.Status);
                    db.AddParameters(2, "@Id", this.ID);
                    db.Open();
                    string query = @"select Primary_Status from TBL_LEADS where Lead_Id=@Id";
                    int status = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    if (status == 6)
                    {
                        return new OutputMessage("This lead already converted to customer", false, Type.NoError, "Lead | UpdateStatus", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        query = @"update TBL_LEADS set Primary_Status=@Primary_Status where Lead_Id=@Id";
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                    }
                }
                return new OutputMessage("Status updated successfully", true, Type.NoError, "Lead | UpdateStatus", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Something went wrong. Status could not be updated", false, Type.Others, "Lead | UpdateStatus", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for converting a lead to customer
        /// </summary>
        /// <param name="LeadId">Id of that particular lead</param>
        /// <param name="customer">object of customer details</param>
        /// <returns></returns>
        public OutputMessage ConvertToCustomer(int LeadId, Customer customer)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(customer.CreatedBy, Security.BusinessModules.Leads, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Customer | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        var result = customer.Save();
                        int Id = ((dynamic)result.Object).Id;
                        if (result.Success)
                        {
                            string query = @"update tbl_leads set Primary_Status=6 where Lead_id=@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", LeadId);
                            db.Open();
                            db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                            return new OutputMessage("Lead successfully converted to customer", true, Type.NoError, "Lead | ConvertToCustomer", System.Net.HttpStatusCode.OK, new { Id = Id });
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Could not Convert to Customer", true, Type.NoError, "Lead | ConvertToCustomer", System.Net.HttpStatusCode.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Status could not be updated", false, Type.Others, "Lead | ConvertToCustomer", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        #endregion Functions
    }
}
