using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Application;
using System.IO;
using System.Web;

namespace Entities.Master
{
    public class Company
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo1 { get; set; }
        public string PinCode { get; set; }
        public string MobileNo2 { get; set; }
        public string Email { get; set; }
        public string RegId1 { get; set; }
        public string RegId2 { get; set; }
        public string RegId3 { get; set; }
        public int CurrencyId { get; set; }
        public string TypeOfFirm { get; set; }
        public string LogoBase64 { get; set; }
        public DateTime OpeningDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Currency { get; set; }
        public string PhotoBase64 { get; set; }
        #endregion properties

        #region Functions
        /// <summary>
        /// Retrieve Id and Name of Company for dropdown list population
        /// </summary>
        /// <param name="ID">id of that particular company</param>
        /// <returns>dropdown list of Company names</returns>
        public static DataTable GetCompany(int ID)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "SELECT [Company_Id],[Name] FROM [dbo].[TBL_COMPANY_MST] where Status<>0").Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Company | GetCompany(int ID)");
                    return null;
                }
                finally
                {
                    db.Close();
                }

            }
        }
        /// <summary>
        /// Save details of each Company
        /// </summary>
        /// <returns>Output message of Success when details Saved successfully otherwise return error message</returns>
        public bool Save()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                throw new InvalidOperationException("Company name must not be empty");
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_COMPANY_MST](Name,Address1,Address2,Country_Id,State_Id,City,Office_No,
                        Mobile_No1,Pin_Code,Mobile_No2,Email,Reg_Id1,Reg_Id2,Reg_Id3,Currency_Id,Type_Of_Firm,Opening_Date,Created_By,Created_Date ) 
                        values(@Name,@Address1,@Address2,@Country_Id,@State_Id,@City,@Office_No,@Mobile_No1,@Pin_Code,@Mobile_No2,@Email,@Reg_Id1,@Reg_Id2,@Reg_Id3,@Currency_Id,  
                        @Type_Of_Firm,@Opening_Date,@Created_By,GETUTCDATE())";
                        db.CreateParameters(18);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Country_Id", this.CountryId);
                        db.AddParameters(4, "@State_Id", this.StateId);
                        db.AddParameters(5, "@City", this.City);
                        db.AddParameters(6, "@Office_No", this.OfficeNo);
                        db.AddParameters(7, "@Mobile_No1", this.MobileNo1);
                        db.AddParameters(8, "@Pin_Code", this.PinCode);
                        db.AddParameters(9, "@Mobile_No2", this.MobileNo2);
                        db.AddParameters(10, "@Email", this.Email);
                        db.AddParameters(11, "@Reg_Id1", this.RegId1);
                        db.AddParameters(12, "@Reg_Id2", this.RegId2);
                        db.AddParameters(13, "@Reg_Id3", this.RegId3);
                        db.AddParameters(14, "@Currency_Id", this.CurrencyId);
                        db.AddParameters(15, "@Type_Of_Firm", this.TypeOfFirm);
                        db.AddParameters(16, "@Opening_Date", this.OpeningDate);
                        db.AddParameters(17, "@Created_By", this.CreatedBy);
                        return Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        Application.Helper.LogException(ex, "Company | Save()");
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
        /// Update details of each Company
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.company, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Company | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Currently There is no Company Added", false, Type.Others, "Company | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Company Name must not be empty", false, Type.Others, "Company | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (!this.Email.IsValidEmail())
            {
                return new OutputMessage("Invalid Email Id", false, Type.Others, "Company | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.CountryId == 0)
            {
                return new OutputMessage("Select a Country", false, Type.Others, "Company | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        byte[] image = Convert.FromBase64String(this.PhotoBase64);
                        string filepath = HttpContext.Current.Server.MapPath("~/Logo_image/logo" + this.ID + ".jpg");
                        File.WriteAllBytes(filepath, image);
                        db.Open();
                        string query = @"Update [dbo].[TBL_COMPANY_MST] set Name=@Name,Address1=@Address1,Address2=@Address2,Country_Id=@Country_Id,
                        State_Id=@State_Id,City=@City,Office_No=@Office_No, Mobile_No1= @Mobile_No1,Pin_Code=@Pin_Code,Mobile_No2=@Mobile_No2
                       ,Email=@Email,Reg_Id1=@Reg_Id1,Reg_Id2=@Reg_Id2,Reg_Id3=@Reg_Id3,
                        Type_Of_Firm=@Type_Of_Firm,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),logo=@Logo where Company_Id=@id";
                        db.CreateParameters(18);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Country_Id", this.CountryId);
                        db.AddParameters(4, "@State_Id", this.StateId);
                        db.AddParameters(5, "@City", this.City);
                        db.AddParameters(6, "@Office_No", this.OfficeNo);
                        db.AddParameters(7, "@Mobile_No1", this.MobileNo1);
                        db.AddParameters(8, "@Pin_Code", this.PinCode);
                        db.AddParameters(9, "@Mobile_No2", this.MobileNo2);
                        db.AddParameters(10, "@Email", this.Email);
                        db.AddParameters(11, "@Reg_Id1", this.RegId1);
                        db.AddParameters(12, "@Reg_Id2", this.RegId2);
                        db.AddParameters(13, "@Reg_Id3", this.RegId3);
                        db.AddParameters(14, "@Type_Of_Firm", this.TypeOfFirm);
                        db.AddParameters(15, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(16, "@Logo", (byte[])Convert.FromBase64String(this.PhotoBase64));
                        db.AddParameters(17, "@id", this.ID);
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);

                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Company | Update", System.Net.HttpStatusCode.OK);


                    }
                    catch (Exception ex)
                    {
                        dynamic Exception = ex;
                        if (ex.GetType().GetProperty("Number") != null)
                        {
                            if (Exception.Number == 547)
                            {
                                db.RollBackTransaction();

                                return new OutputMessage("You cannot update this company because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Company | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                            else
                            {
                                db.RollBackTransaction();

                                return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Company | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong.Company could not be updated", false, Type.Others, "Company | Update", System.Net.HttpStatusCode.InternalServerError, ex);
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
        ///  Delete individual company from company master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>Return success alert when details deleted successfull otherwise returns an error alert</returns>
        public bool Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_COMPANY_MST where Company_Id=@ID";
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
                    Application.Helper.LogException(ex, "Company | Delete()");
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
        /// <summary>
        /// Retrieve all the companies from company master
        /// </summary>
        /// <returns>list of companies</returns>
        public static List<Company> GetDetails()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select c.Company_Id,isnull(c.Name,0)[Name],isnull(c.Address1,0)[Address1],isnull(c.Address2,0)[Address2],c.Country_Id,c.State_Id,
                                                                   isnull(c.City,0)[City],isnull(c.Office_No,0)[Office_No],isnull(c.Mobile_No1,0)[Mobile_No1],isnull(c.Pin_Code,0)[Pin_Code],
                                                                    isnull(c.Mobile_No2,0)[Mobile_No2],isnull(c.Email,0)[Email],isnull(c.Reg_Id1,0)[Reg_Id1],isnull(c.Reg_Id2,0)[Reg_Id2],
                                                                    isnull(c.Reg_Id3,0)[Reg_Id3],c.Currency_Id,isnull(c.Type_Of_Firm,0)[Type_Of_Firm],c.Opening_Date,c.Created_By,c.Created_Date,
                                                                    isnull(c.Modified_By,0)[Modified_By],isnull(c.Modified_Date,0)[Modified_Date],isnull(co.Name,0)[Country],c.logo,
                                                                    isnull(s.Name,0)[State],isnull(cu.Code,0)[Currency] from TBL_COMPANY_MST c
                                                                    left join TBL_COUNTRY_MST co on co.Country_Id=c.Country_Id
                                                                    left join TBL_STATE_MST s on s.State_Id=c.State_Id
                                                                    left join TBL_CURRENCY_MST cu on cu.Currency_Id=c.Currency_Id").Tables[0];
                List<Company> result = new List<Company>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Company comp = new Company();
                        comp.ID = (item["Company_Id"] != DBNull.Value) ? Convert.ToInt32(item["Company_Id"]) : 0;
                        comp.Name = Convert.ToString(item["Name"]);
                        comp.Address1 = Convert.ToString(item["Address1"]);
                        comp.Address2 = Convert.ToString(item["Address2"]);
                        comp.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                        comp.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                        comp.City = Convert.ToString(item["City"]);
                        comp.OfficeNo = Convert.ToString(item["Office_No"]);
                        comp.MobileNo1 = Convert.ToString(item["Mobile_No1"]);
                        comp.PinCode = Convert.ToString(item["Pin_Code"]);
                        comp.MobileNo2 = Convert.ToString(item["Mobile_No2"]);
                        comp.Email = Convert.ToString(item["Email"]);
                        comp.RegId1 = Convert.ToString(item["Reg_Id1"]);
                        comp.RegId2 = Convert.ToString(item["Reg_Id2"]);
                        comp.RegId3 = Convert.ToString(item["Reg_Id3"]);
                        comp.LogoBase64 = Convert.ToBase64String(item["logo"] as byte[]);
                        comp.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                        comp.TypeOfFirm = Convert.ToString(item["Type_Of_Firm"]);
                        comp.OpeningDate = Convert.ToDateTime(item["Opening_Date"]);
                        comp.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                        comp.Country = Convert.ToString(item["Country"]);
                        comp.State = Convert.ToString(item["State"]);
                        comp.Currency = Convert.ToString(item["Currency"]);
                        comp.CreatedBy = Convert.ToInt32(item["Created_By"]);
                        result.Add(comp);
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
                Application.Helper.LogException(ex, "Company |  GetDetails()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// retrieve a single company from company master
        /// </summary>
        /// <param name="Id">Id of that particular company which u want to retrieve</param>
        /// <returns> single company details</returns>
        public static Company GetDetails(int Id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select c.Company_Id,isnull(c.Name,0)[Name],isnull(c.Address1,0)[Address1],isnull(c.Address2,0)[Address2],c.Country_Id,c.State_Id,
                                                                   isnull(c.City,0)[City],isnull(c.Office_No,0)[Office_No],isnull(c.Mobile_No1,0)[Mobile_No1],isnull(c.Pin_Code,0)[Pin_Code],
                                                                    isnull(c.Mobile_No2,0)[Mobile_No2],isnull(c.Email,0)[Email],isnull(c.Reg_Id1,0)[Reg_Id1],isnull(c.Reg_Id2,0)[Reg_Id2],
                                                                    isnull(c.Reg_Id3,0)[Reg_Id3],c.Currency_Id,isnull(c.Type_Of_Firm,0)[Type_Of_Firm],c.Opening_Date,c.Created_By,c.Created_Date,
                                                                    isnull(c.Modified_By,0)[Modified_By],isnull(c.Modified_Date,0)[Modified_Date],isnull(co.Name,0)[Country],c.logo,
                                                                    isnull(s.Name,0)[State],isnull(cu.Code,0)[Currency] from TBL_COMPANY_MST c
                                                                    left join TBL_COUNTRY_MST co on co.Country_Id=c.Country_Id
                                                                    left join TBL_STATE_MST s on s.State_Id=c.State_Id
                                                                    left join TBL_CURRENCY_MST cu on cu.Currency_Id=c.Currency_Id where c.Company_Id=@Company_Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataRow item = dt.Rows[0];
                Company comp = new Company();
                comp.ID = (item["Company_Id"] != DBNull.Value) ? Convert.ToInt32(item["Company_Id"]) : 0;
                comp.Name = Convert.ToString(item["Name"]);
                comp.Address1 = Convert.ToString(item["Address1"]);
                comp.Address2 = Convert.ToString(item["Address2"]);
                comp.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                comp.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                comp.City = Convert.ToString(item["City"]);
                comp.OfficeNo = Convert.ToString(item["Office_No"]);
                comp.MobileNo1 = Convert.ToString(item["Mobile_No1"]);
                comp.PinCode = Convert.ToString(item["Pin_Code"]);
                comp.MobileNo2 = Convert.ToString(item["Mobile_No2"]);
                comp.Email = Convert.ToString(item["Email"]);
                comp.RegId1 = Convert.ToString(item["Reg_Id1"]);
                comp.RegId2 = Convert.ToString(item["Reg_Id2"]);
                comp.RegId3 = Convert.ToString(item["Reg_Id3"]);
                comp.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                comp.TypeOfFirm = Convert.ToString(item["Type_Of_Firm"]);
                comp.Country = Convert.ToString(item["Country"]);
                comp.State = Convert.ToString(item["State"]);
                comp.LogoBase64 = Convert.ToBase64String(item["logo"] as byte[]);
                comp.PhotoBase64 = Convert.ToBase64String((byte[])item["logo"]);
                return comp;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Company |  GetDetails(int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Retrieve a single entry by locationid
        /// </summary>
        /// <param name="LocationId">Filter details according to specified locationId</param>
        /// <returns>returns details of a single entry</returns>
        public static Company GetDetailsByLocation(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select c.Company_Id,isnull(c.Name,0)[Name],isnull(c.Address1,0)[Address1],isnull(c.Address2,0)[Address2],c.Country_Id,c.State_Id,
                                                                   isnull(c.City,0)[City],isnull(c.Office_No,0)[Office_No],isnull(c.Mobile_No1,0)[Mobile_No1],isnull(c.Pin_Code,0)[Pin_Code],
                                                                    isnull(c.Mobile_No2,0)[Mobile_No2],isnull(c.Email,0)[Email],isnull(c.Reg_Id1,0)[Reg_Id1],isnull(c.Reg_Id2,0)[Reg_Id2],
                                                                    isnull(c.Reg_Id3,0)[Reg_Id3],c.Currency_Id,isnull(c.Type_Of_Firm,0)[Type_Of_Firm],c.Opening_Date,c.Created_By,c.Created_Date,
                                                                    isnull(c.Modified_By,0)[Modified_By],isnull(c.Modified_Date,0)[Modified_Date],isnull(co.Name,0)[Country],c.logo,
                                                                    isnull(s.Name,0)[State],isnull(cu.Code,0)[Currency] from TBL_COMPANY_MST c
                                                                    left join TBL_COUNTRY_MST co on co.Country_Id=c.Country_Id
                                                                    left join TBL_STATE_MST s on s.State_Id=c.State_Id
                                                                    left join TBL_CURRENCY_MST cu on cu.Currency_Id=c.Currency_Id where c.Company_Id=(select Company_Id from TBL_LOCATION_MST where location_id=@location_id)";
                db.CreateParameters(1);
                db.AddParameters(0, "@location_id", LocationId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataRow item = dt.Rows[0];
                Company comp = new Company();
                comp.ID = (item["Company_Id"] != DBNull.Value) ? Convert.ToInt32(item["Company_Id"]) : 0;
                comp.Name = Convert.ToString(item["Name"]);
                comp.Address1 = Convert.ToString(item["Address1"]);
                comp.Address2 = Convert.ToString(item["Address2"]);
                comp.CountryId = (item["Country_Id"] != DBNull.Value) ? Convert.ToInt32(item["Country_Id"]) : 0;
                comp.StateId = (item["State_Id"] != DBNull.Value) ? Convert.ToInt32(item["State_Id"]) : 0;
                comp.City = Convert.ToString(item["City"]);
                comp.OfficeNo = Convert.ToString(item["Office_No"]);
                comp.MobileNo1 = Convert.ToString(item["Mobile_No1"]);
                comp.PinCode = Convert.ToString(item["Pin_Code"]);
                comp.MobileNo2 = Convert.ToString(item["Mobile_No2"]);
                comp.Email = Convert.ToString(item["Email"]);
                comp.RegId1 = Convert.ToString(item["Reg_Id1"]);
                comp.RegId2 = Convert.ToString(item["Reg_Id2"]);
                comp.RegId3 = Convert.ToString(item["Reg_Id3"]);
                comp.CurrencyId = (item["Currency_Id"] != DBNull.Value) ? Convert.ToInt32(item["Currency_Id"]) : 0;
                comp.TypeOfFirm = Convert.ToString(item["Type_Of_Firm"]);
                comp.Country = Convert.ToString(item["Country"]);
                comp.State = Convert.ToString(item["State"]);
                comp.LogoBase64 = Convert.ToBase64String(item["logo"] as byte[]);
                return comp;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Company |  GetDetailsByLocation(int LocationId)");
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
