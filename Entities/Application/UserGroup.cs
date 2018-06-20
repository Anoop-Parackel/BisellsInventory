using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Application
{
   public class UserGroup 
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }        
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<User> AssisgnedUsers { get; set; }
        public List<User> UnAssisgnedUsers { get; set; }
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Constructor for accepting group id and user id(from sessoin)
        /// </summary>
        /// <param group id="id"></param>
        /// <param  created by="createdby"></param>
        public UserGroup(int id,int createdby)
        {
            this.ID = id;
            this.CreatedBy = createdby;
        }
        public UserGroup()
        {

        }
        /// <summary>
        /// Retrieves all Groups from  User Group Master for loading in dropedownlist
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUserGroup()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "SELECT  [User_Group_Id],[Group_Name] FROM [dbo].[TBL_USER_GROUP_MST]").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "UserGroup | GetUserGroup()");
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
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage(" Group name must not be empty", false, Type.RequiredFields, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"INSERT INTO [dbo].[TBL_USER_GROUP_MST]([Group_Name],[Created_Date],[Created_By],[Description])
                                                     VALUES(@Group_Name,getutcdate(),@Created_By,@Description)";
                        db.CreateParameters(3);
                        db.AddParameters(0, "@Group_Name", this.Name);                      
                        db.AddParameters(1, "@Created_By", this.CreatedBy);
                        db.AddParameters(2, "@Description", this.Description);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("User group saved successfully", true, Type.NoError, "UserGroup | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. User group could not be saved", false, Type.Others, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError);

                        }

                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. User group could not be saved", false, Type.Others, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError,ex);

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
        /// Get all Group wise Users list
        /// </summary>
        /// <param Group_id="id"></param>
        /// <returns>object of Users</returns>
        public static UserGroup GetUsers(int id)
            
        {
            UserGroup grp = new UserGroup();
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"select * from TBL_USER_MST where User_Id not in (select User_Id from TBL_USER_GROUP_RELATION where User_Group_Id=@id)
                                                            select distinct u.User_Id,u.User_Name from TBL_USER_MST u left outer join [TBL_USER_GROUP_RELATION] r on u.[User_Id] = r.[User_Id]
                                                            where r.User_Group_Id =@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", id);
                    DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                    DataTable UnAssignedDt = ds.Tables[0];
                    DataTable AssignedDt = ds.Tables[1];
                    grp.ID = id;
                    List<User> AssignedUsers = new List<User>();
                    List<User> UnAssignedUsers = new List<User>();
                    foreach (DataRow item in AssignedDt.Rows)
                    {
                        User user = new User();
                        user.ID = Convert.ToInt32(item["User_Id"]);
                        user.UserName = Convert.ToString(item["User_Name"]);
                        AssignedUsers.Add(user);
                    }
                    foreach (DataRow item in UnAssignedDt.Rows)
                    {
                        User user = new User();
                        user.ID = Convert.ToInt32(item["User_Id"]);
                        user.UserName = Convert.ToString(item["User_Name"]);
                        UnAssignedUsers.Add(user);
                    }

                    grp.UnAssisgnedUsers = UnAssignedUsers;
                    grp.AssisgnedUsers = AssignedUsers;
                    return grp;
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "UserGroup | GetUsers(int id)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public OutputMessage Update()
        {
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "UserGroup | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Group name must not be empty", false, Type.Others, "UserGroup | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"UPDATE [dbo].[TBL_USER_GROUP_MST]
                                         SET [Group_Name]=@Group_Name,[Modified_Date]=getutcdate(),[Modified_By]=@Modified_By,[Description]=@Description
                                           WHERE [User_Group_Id]=@id";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Group_Name", this.Name);
                   
                        db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(2, "@Description", this.Description);
                        db.AddParameters(3, "@id", this.ID);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" User group updated successfully", true, Type.NoError, "UserGroup | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. User group could not be updated", false, Type.Others, "UserGroup | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. User group could not be updated", false, Type.Others, "UserGroup | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

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
                        string query = "select ISNULL(is_system_defined,0) from TBL_USER_GROUP_MST where [User_Group_Id]=@Id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@Id", this.ID);
                        db.Open();
                        bool isSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                        if (isSystemDefined)
                        {
                            return new OutputMessage("You cannot delete this group because it is system defined", false, Type.Others, "UserGroup | Delete", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                         query = @"delete from [TBL_USER_GROUP_MST] where [User_Group_Id]=@Id";
                         bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("User group deleted successfully", true, Type.NoError, "UserGroup | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. User group could not be deleted", false, Type.Others, "UserGroup | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "UserGroup | Delete", System.Net.HttpStatusCode.InternalServerError);

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
                            return new OutputMessage("You have no permission to delete this user group", false, Entities.Type.ForeignKeyViolation, "UserGroup | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "UserGroup | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. User group could not be deleted", false, Type.Others, " UserGroup | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }

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
        /// Retrieves all the Users Group from the User Group Master
        /// </summary>
        /// <returns>List of UserGroup</returns>
        public static List<UserGroup> GetDetails()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, "select * from [TBL_USER_GROUP_MST]").Tables[0];
                List<UserGroup> result = new List<UserGroup>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        UserGroup group = new UserGroup();
                        group.ID = item["User_Group_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Group_Id"]) : 0;
                        group.Description = Convert.ToString(item["description"]);
                        group.Name = Convert.ToString(item["Group_Name"]);
                        group.CreatedDate = Convert.ToDateTime(item["Created_Date"]);
                        group.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        result.Add(group);
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
                Application.Helper.LogException(ex, "UserGroup | GetDetails()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieves a single User Group from the Group Master
        /// </summary>
        /// <returns>Group</returns>
        public static UserGroup GetDetails(int id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 * from [TBL_USER_GROUP_MST] where User_Group_Id=@id";
                db.CreateParameters(1);
                db.AddParameters(0, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<UserGroup> result = new List<UserGroup>();
                if (dt != null)
                {

                    UserGroup group = new UserGroup();
                    DataRow item = dt.Rows[0];
                    group.ID = item["User_Group_Id"] != DBNull.Value ? Convert.ToInt32(item["User_Group_Id"]) : 0;
                    group.Name = Convert.ToString(item["Group_Name"]);
                    group.Description = Convert.ToString(item["description"]);
                    group.CreatedDate = Convert.ToDateTime(item["Created_Date"]);
                    group.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                    return group;


                }


                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "UserGroup | GetDetails(int id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Save Unassigned user to a specified group
        /// </summary>
        public OutputMessage SaveUserGroup()
        {
            if (this.ID == 0)
            {
                return new OutputMessage("Please select a group to save", false, Type.Others, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
              
                DBManager db = new DBManager();
                try
                {
                    db.Open();
                    db.BeginTransaction();
                    string query = @"delete from TBL_USER_GROUP_RELATION where User_Group_Id=" + this.ID;
                    db.ExecuteNonQuery(CommandType.Text, query);
                    foreach (User _user in AssisgnedUsers)
                    {
                        //check for user is disabled or not
                        db.CreateParameters(1);
                        db.AddParameters(0, "@user_id", _user.ID);
                        query = @"select count(*) from tbl_user_mst where User_Id=@user_id and Disable<>1";
                        int DisabledUsercount = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        if (DisabledUsercount<1)
                        {
                            return new OutputMessage("Current User is in Disabled Status.", false, Type.Others, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                        query = @"insert into [TBL_USER_GROUP_RELATION] ([User_Id],[User_Group_Id],[Created_Date],[Created_By]) 
                                                        values(@user_id,@group_id,GETUTCDATE(),@created_by)";
                        db.CleanupParameters();
                        db.CreateParameters(3);
                        db.AddParameters(0, "@user_id", _user.ID);
                        db.AddParameters(1, "@group_id", this.ID);
                        db.AddParameters(2, "@created_by", this.CreatedBy);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Roles Updated Successfully", true, Type.NoError, "UserGroup | Save", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {

                    db.RollBackTransaction();
                    return new OutputMessage("Somthing went wrong. Could not set Roles", false, Type.Others, "UserGroup | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }

        }
        /// <summary>
        /// check user is disabled and Expiry period
        /// </summary>
        /// <returns></returns>
 }
}
