using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Application
{
    public class User
    {


        public int ID { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        private string _Password;
        public string Password
        {
            set
            {
                _Password = value;
            }
            get
            {
                return _Password;
            }
        }
        public string FullName { get; set; }
        public bool Disable { get; set; }
        public DateTime LastLogin { get; set; }
        public int ExpiryPeriod { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmployeeId { get; set; }
        public int SpecialPrivilege { get; set; }
        public string LastLoggedIp { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileImageB64 { get; set; }
        public bool ForcePasswordChange { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public User(int UserId)
        {
            this.ID = UserId;
        }
        public User()
        {

        }
        /// <summary>
        /// load dropdown for Roles
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static DataTable GetUserForRole(int companyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    db.CreateParameters(1);
                    db.AddParameters(0, "@companyId", companyId);
                    return db.ExecuteDataSet(CommandType.Text, "SELECT  [User_Id],[User_Name] FROM [dbo].[TBL_USER_MST] where Company_Id=@companyId and Disable=0").Tables[0];

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "User |  GetUserForRole(int companyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static DataTable GetUser()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {

                    return db.ExecuteDataSet(CommandType.Text, "SELECT  [User_Id],[User_Name] FROM [dbo].[TBL_USER_MST]").Tables[0];

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "User |  GetUser()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public OutputMessage Save()
        {
            DBManager db = new DBManager();
            string query1 = @"select count(*) from TBL_USER_MST where User_Name=@username";
            db.CreateParameters(1);
            db.AddParameters(0, "@username", this.UserName);
            db.Open();
            bool exist = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query1)) > 0 ? true : false;
            if (string.IsNullOrWhiteSpace(this.UserName))
            {
                return new OutputMessage("User name must not be empty", false, Type.RequiredFields, "User | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (exist)
            {
                return new OutputMessage("User name already exist", false, Type.NoError, "User|Save", System.Net.HttpStatusCode.OK);
            }
            else if (this.Password.Length <= 8 && this.Password.Length >= 16)
            {
                return new OutputMessage("Password should be in between 8 and 16 characters length", false, Type.NoError, "User|Save", System.Net.HttpStatusCode.OK);
            }

            else
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
                            string filePath = "Resources\\User\\ProfileImages";
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
                            this.ProfileImagePath = "/Resources/User/ProfileImages/" + file;
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    dynamic Encrypt = Security.Cryptor.Hash(_Password, null);
                    string Hash = Encrypt.Hash;
                    string Salt = Encrypt.Salt;

                    string query = @"insert into [dbo].[TBL_USER_MST]([User_Name],[hash],[Salt],[Full_Name],[Disable],
                                            [Expiry_Period],[Created_Date],
                                            [Employee_Id],[Special_Privilege],[Last_Logged_Ip],
                                            [Created_By],Force_Password_Change,Location_Id,Company_Id,Profile_Image_Path) values(@User_Name,@hash,@salt,@Full_Name,
                                            @Disable,@Expiry_Period,GETUTCDATE(),@Employee_Id,@Special_Privilege
                                            ,@Last_Logged_Ip,@Created_By,@Force_Password_Change,@Location_Id,@Company_Id,@Profile_Image_Path)";
                    db.CleanupParameters();
                    db.CreateParameters(14);
                    db.AddParameters(0, "@User_Name", this.UserName);
                    db.AddParameters(1, "@hash", Hash);
                    db.AddParameters(2, "@salt", Salt);
                    db.AddParameters(3, "@Full_Name", this.FullName);
                    db.AddParameters(4, "@Disable", this.Disable);
                    db.AddParameters(5, "@Expiry_Period", this.ExpiryPeriod);
                    db.AddParameters(6, "@Employee_Id", this.EmployeeId);
                    db.AddParameters(7, "@Special_Privilege", this.SpecialPrivilege);
                    db.AddParameters(8, "@Last_Logged_Ip", this.LastLoggedIp);
                    db.AddParameters(9, "@Created_By", this.CreatedBy);
                    db.AddParameters(10, "@Force_Password_Change", this.ForcePasswordChange);
                    db.AddParameters(11, "@Location_Id", this.LocationId);
                    db.AddParameters(12, "@Company_Id", this.CompanyId);
                    db.AddParameters(13, "@Profile_Image_Path", this.ProfileImagePath);
                    bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    if (status)
                    {
                        return new OutputMessage("User saved successfully", true, Type.NoError, "User | Save", System.Net.HttpStatusCode.OK);

                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. User could not be saved", false, Type.Others, "User | Save", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. User could not be saved", false, Type.Others, "User | Save", System.Net.HttpStatusCode.InternalServerError, ex);

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

        public OutputMessage Update()
        {
            DBManager db = new DBManager();
            db.Open();
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.UserName))
            {
                return new OutputMessage("User name must not be empty", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
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
                            string filePath = "Resources\\User\\ProfileImages";
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
                            this.ProfileImagePath = "/Resources/User/ProfileImages/" + file;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    db.Open();
                    string query = @"Update [dbo].[TBL_USER_MST] set Full_Name=@Full_Name,Disable=@Disable,Expiry_Period=@Expiry_Period,Employee_Id=@Employee_Id,Special_Privilege=@Special_Privilege,Last_Logged_Ip=@Last_Logged_Ip,Modified_By=@Modified_By,Location_Id=@Location_Id,Modified_Date=GETUTCDATE(),Profile_Image_Path=@Profile_Image_Path where [User_Id]=@id";
                    db.CreateParameters(10);
                    db.AddParameters(0, "@Full_Name", this.FullName);
                    db.AddParameters(1, "@Disable", this.Disable);
                    db.AddParameters(2, "@Expiry_Period", this.ExpiryPeriod);
                    db.AddParameters(3, "@Employee_Id", this.EmployeeId);
                    db.AddParameters(4, "@Special_Privilege", this.SpecialPrivilege);
                    db.AddParameters(5, "@Last_Logged_Ip", this.LastLoggedIp);
                    db.AddParameters(6, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(7, "@id", this.ID);
                    db.AddParameters(8, "@Location_Id", this.LocationId);
                    db.AddParameters(9, "@Profile_Image_Path", this.ProfileImagePath);
                    bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    if (status)
                    {
                        return new OutputMessage("User successfully updated", true, Type.NoError, "User | Update", System.Net.HttpStatusCode.OK);

                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. User could not be updated", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. User could not be updated", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {
                    db.Close();

                }


            }
        }

        public OutputMessage UpdatePassword()
        {
            DBManager db = new DBManager();
            db.Open();
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.Password.Length <= 8 && this.Password.Length >= 16)
            {
                return new OutputMessage("Password should be in between 8 and 16 characters length", false, Type.NoError, "User|Save", System.Net.HttpStatusCode.OK);
            }
            else
            {
                try
                {
                    db.Open();
                    dynamic Encrypt = Security.Cryptor.Hash(_Password, null);
                    string Hash = Encrypt.Hash;
                    string Salt = Encrypt.Salt;
                    string query = @"Update [dbo].[TBL_USER_MST] set hash=@hash,[salt]=@salt,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where [User_Id]=@id";
                    db.CreateParameters(4);
                    db.AddParameters(0, "@hash", Hash);
                    db.AddParameters(1, "@salt", Salt);
                    db.AddParameters(2, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(3, "@id", this.ID);
                    bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                    if (status)
                    {
                        return new OutputMessage("User successfully updated", true, Type.NoError, "User | Update", System.Net.HttpStatusCode.OK);

                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. User could not be updated", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. User could not be updated", false, Type.Others, "User | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {
                    db.Close();

                }


            }
        }

        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.ID != 0)
                    {
                        //Checking whether the user is defined by system
                        string query = "select ISNULL(is_system_defined,0) from TBL_USER_MST where [User_Id]=@Id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@Id", this.ID);
                        db.Open();
                        bool isSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                        if (isSystemDefined)
                        {
                            return new OutputMessage("You cannot delete this user since it is defined by the system", false, Type.Others, "User | Delete", System.Net.HttpStatusCode.InternalServerError);
                        }

                        query = @"delete from [TBL_USER_MST] where [User_Id]=@Id";

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("User deleted successfully", true, Type.NoError, "User | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. User could not be deleted", false, Type.Others, "User | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "User | Delete", System.Net.HttpStatusCode.InternalServerError);

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
                            return new OutputMessage("You have no permission to delete this user", false, Entities.Type.ForeignKeyViolation, "User | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "User | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. User could not be deleted", false, Type.Others, " User | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }

                finally
                {

                    db.Close();

                }

            }
        }


        /// <summary>
        /// Retrieves all the Users from the User Master
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<User> GetDetails()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select u.*, em.First_Name[Employee_name],l.Name[Location] from[TBL_USER_MST] u
                                                                   left join[dbo].[TBL_EMPLOYEE_MST] em on u.Employee_Id = em.Employee_Id
                                                                   left join TBL_LOCATION_MST l on l.Location_Id=u.Location_Id
                                                                   order by u.Created_Date desc").Tables[0];
                List<User> result = new List<User>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        User user = new User();
                        user.ID = item["User_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Id"]) : 0;
                        user.UserName = Convert.ToString(item["User_Name"]);
                        user.FullName = Convert.ToString(item["full_Name"]);
                        user.ExpiryPeriod = item["Expiry_Period"] != DBNull.Value ? Convert.ToInt32(item["Expiry_Period"]) : 0;
                        user.EmployeeId = Convert.ToString(item["Employee_name"]);
                        user.SpecialPrivilege = item["Special_Privilege"] != DBNull.Value ? Convert.ToInt32(item["Special_Privilege"]) : 0;
                        user.LastLoggedIp = Convert.ToString(item["Last_Logged_Ip"]);
                        user.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        user.LocationId = Convert.ToInt32(item["Location_Id"]);
                        user.Location = Convert.ToString(item["Location"]);
                        result.Add(user);
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
                Application.Helper.LogException(ex, "User | GetDetails()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieves a single User from the User Master
        /// </summary>
        /// <returns>User</returns>
        public static User GetDetails(int id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 * from TBL_USER_MST where User_Id=@id";
                db.CreateParameters(1);
                db.AddParameters(0, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<User> result = new List<User>();
                if (dt != null)
                {

                    User user = new User();
                    DataRow item = dt.Rows[0];
                    user.ID = item["User_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Id"]) : 0;
                    user.UserName = Convert.ToString(item["User_Name"]);
                    user.LocationId = Convert.ToInt32(item["Location_Id"]);
                    user.FullName = Convert.ToString(item["full_Name"]);
                    //user.Disable = Convert.ToBoolean(item["Disable"]);
                    user.ExpiryPeriod = item["Expiry_Period"] != DBNull.Value ? Convert.ToInt32(item["Expiry_Period"]) : 0;
                    user.EmployeeId = Convert.ToString(item["Employee_Id"]);
                    user.SpecialPrivilege = item["Special_Privilege"] != DBNull.Value ? Convert.ToInt32(item["Special_Privilege"]) : 0;
                    user.LastLoggedIp = Convert.ToString(item["Last_Logged_Ip"]);
                    user.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                    user.ProfileImagePath = Convert.ToString(item["profile_image_path"]);
                    return user;
                }


                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "User | GetDetails(int id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public OutputMessage Authenticate()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select count(*) from [TBL_USER_MST] where [User_Name]=@username";
                db.CreateParameters(1);
                db.AddParameters(0, "@username", this.UserName);
                db.Open();
                if (Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query)) == 1)
                {
                    db.CleanupParameters();
                    query = @"SELECT top 1 u.[User_Id],u.Location_Id,isnull(l.Name,'Office')[Location],u.Company_Id,e.Employee_Id, 
                              u.[Hash],u.Salt,u.Full_Name,u.profile_image_path,d.Designation ,dbo.UDF_GetFinYear()[FinYear]
                              FROM [dbo].[TBL_USER_MST] u                        
                              left join TBL_LOCATION_MST l on u.location_id=l.location_id   
                              left join [dbo].[TBL_EMPLOYEE_MST] e  on u.Employee_Id=e.Employee_Id  
                              left join TBL_DESIGNATION_MST d on e.Designation_Id=d.Designation_id 
                              where [User_Name] =@user_name";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@user_name", UserName);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    string Hash = dt.Rows[0]["Hash"].ToString();
                    string Salt = dt.Rows[0]["Salt"].ToString();
                    byte[] SaltBytes = Convert.FromBase64String(Salt);
                    dynamic Encrypt = Security.Cryptor.Hash(this.Password, SaltBytes);
                    if (Hash.Equals(Encrypt.Hash))
                    {
                        dynamic Details = new ExpandoObject();
                        Details.UserName = dt.Rows[0]["Full_Name"].ToString();
                        Details.Designation = dt.Rows[0]["Designation"].ToString();
                        Details.Photo = dt.Rows[0]["profile_image_path"] != DBNull.Value ? dt.Rows[0]["profile_image_path"] : string.Empty;
                        Details.UserId = dt.Rows[0]["User_id"].ToString();
                        Details.LocationId = dt.Rows[0]["Location_Id"].ToString();
                        Details.Location = dt.Rows[0]["Location"].ToString();
                        Details.CompanyId = dt.Rows[0]["Company_Id"].ToString();
                        Details.FinYear = dt.Rows[0]["FinYear"].ToString();
                        return new OutputMessage("Access Granted", true, Type.NoError, "User | Authenticate", System.Net.HttpStatusCode.OK, Details);
                    }
                    else
                    {
                        return new OutputMessage("Passwords Do not Match", false, Type.UnAuthenticated, "User | Authenticate", System.Net.HttpStatusCode.Unauthorized);
                    }

                }
                else
                {

                    return new OutputMessage("User not found", false, Type.NotFound, "User | Authenticate", System.Net.HttpStatusCode.Unauthorized);
                }

            }
            catch (Exception ex)
            {
                return new OutputMessage("Something went wrong", false, Type.Others, "User | Authenticate", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }
        }

        public static object GetUserDetails(int UserId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"SELECT top 1 u.[User_Id],u.Location_Id,isnull(l.Name,'Office')[Location],u.Company_Id,e.Employee_Id, 
                              u.[Hash],u.Salt,u.Full_Name,e.Photo_Path,u.profile_image_path,d.Designation ,dbo.UDF_GetFinYear()[FinYear],e.Mobile,e.Email
                              FROM [dbo].[TBL_USER_MST] u                        
                              left join TBL_LOCATION_MST l on u.location_id=l.location_id   
                              left join [dbo].[TBL_EMPLOYEE_MST] e  on u.Employee_Id=e.Employee_Id  
                              left join TBL_DESIGNATION_MST d on e.Designation_Id=d.Designation_id                             
                    where u.[User_Id] =@user_id";
                db.CreateParameters(1);
                db.AddParameters(0, "@user_id", UserId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                dynamic Details = new ExpandoObject();
                Details.UserName = dt.Rows[0]["Full_Name"].ToString();
                Details.Designation = dt.Rows[0]["Designation"].ToString();
                Details.Photo = dt.Rows[0]["Photo_Path"] != DBNull.Value ? dt.Rows[0]["Photo_Path"].ToString() : string.Empty;
                Details.UserId = dt.Rows[0]["User_id"].ToString();
                Details.LocationId = dt.Rows[0]["Location_Id"].ToString();
                Details.CompanyId = dt.Rows[0]["Company_Id"].ToString();
                Details.FinYear = dt.Rows[0]["FinYear"].ToString();
                Details.Location = dt.Rows[0]["Location"].ToString();
                Details.Mobile = dt.Rows[0]["Mobile"].ToString();
                Details.Email = dt.Rows[0]["Email"].ToString();
                Details.ProfileImagePath = Convert.ToString(dt.Rows[0]["profile_image_path"]);
                return Details;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "User | GetUserDetails(int UserId)");
                db.Close();
                return null;
            }
        }

        public string BuildMenuMarkup()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@userid", this.ID);
                return db.ExecuteScalar(CommandType.StoredProcedure, "[USP_GET_USER_MENU]").ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "User | BuildMenuMarkup()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// return a list of users in specific group [role]
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns></returns>
        public static List<User> GetUsersInGroup(int GroupId, int CompanyId)
        {

            DBManager db = new DBManager();
            try
            {
                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@groupId", GroupId);
                db.AddParameters(1, "@Company_Id", CompanyId);

                DataTable dt = db.ExecuteQuery(CommandType.Text, @"select usr.User_Id, usr.Full_Name, usr.User_Name from TBL_USER_MST usr
left join TBL_USER_GROUP_RELATION ugr on ugr.User_Id = usr.User_Id where
          usr.Disable <> 1 and ugr.User_Group_Id = @groupId and usr.Company_Id=@Company_Id");
                List<User> result = new List<User>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        User user = new User();
                        user.ID = item["User_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Id"]) : 0;
                        user.FullName = Convert.ToString(item["Full_Name"]);
                        user.UserName = Convert.ToString(item["User_Name"]);

                        result.Add(user);
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
                Application.Helper.LogException(ex, "User | GetUsersInGroup(int GroupId,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// return a list of user [role]
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<User> GetUsers(int UserId, int CompanyId)
        {

            DBManager db = new DBManager();
            try
            {
                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@UserId", UserId);
                db.AddParameters(1, "@Company_Id", CompanyId);

                DataTable dt = db.ExecuteQuery(CommandType.Text, @"select usr.User_Id,usr.User_Name,usr.Full_Name from TBL_USER_MST usr
                                             where usr.Disable<>1 and usr.user_id=@UserId and usr.Company_Id=@Company_Id");
                List<User> result = new List<User>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        User user = new User();
                        user.ID = item["User_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Id"]) : 0;
                        user.FullName = Convert.ToString(item["Full_Name"]);
                        user.UserName = Convert.ToString(item["User_Name"]);

                        result.Add(user);
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
                Application.Helper.LogException(ex, "User | GetUsers(int UserId, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
