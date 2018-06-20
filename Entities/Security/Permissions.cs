using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entities.Security
{
    public class Permissions
    {

        public static bool AuthorizeUser(int UserId,BusinessModules Module,PermissionTypes PermissionLevel)
        {
            DBManager db = new DBManager();
            try
            {                              
                db.Open();
                string query = @"[dbo].[USP_GET_USER_PERMISSIONS] "+UserId+" , "+(int)Module;

                DataRow row= db.ExecuteDataSet(System.Data.CommandType.Text, query).Tables[0].Rows[0];
                if(Convert.ToBoolean(row["all"]))
                {
                    return true;
                }
                else
                {
                    switch (PermissionLevel)
                    {
                        case PermissionTypes.Retrieve:
                            if(Convert.ToBoolean(row["view"]))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case PermissionTypes.Create:
                            if (Convert.ToBoolean(row["create"]))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case PermissionTypes.Update:
                            if (Convert.ToBoolean(row["update"]))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case PermissionTypes.Delete:
                            if (Convert.ToBoolean(row["delete"]))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        default:
                            return false;
                    }
                }

                

            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Permissions | AuthorizeUser(int UserId,BusinessModules Module,PermissionTypes PermissionLevel)");
                 return false;
            }
            finally
            {
                db.Close();
            }
        }

        public static bool AuthorizePage(int UserId,string Url)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select top 1 Module_Id,Url from TBL_MODULES_MST where url=@url";
                db.CreateParameters(1);
                db.AddParameters(0, "@url", Url);
                db.Open();
                int ModuleId =(int) db.ExecuteScalar(CommandType.Text,query);
                BusinessModules Module = (BusinessModules)ModuleId;
                return AuthorizeUser(UserId,Module,PermissionTypes.Retrieve);
            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Permissions | AuthorizePage(int UserId,string Url)");
                return false;
            }
        }

        public static dynamic GetUserPermission(int UserId,int ModuleId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select case when (b.Permission  &1)=1 then 1 else 0 end [View] ,
                             case when (b.Permission  &2)=2 then 1 else 0 end [Create] ,
                             case when (b.Permission  &4)=4 then 1 else 0 end [Update] ,
                             case when (b.Permission  &8)=8 then 1 else 0 end [Delete] ,
                             case when (b.Permission  &16)=16 then 1 else 0 end [All] 
                             from TBL_USER_MST a
                             INNER JOIN TBL_USER_PERMISSIONS b on a.[User_Id] = b.[User_Id]
                             where a.[User_Id] =@user_id and b.Module_Id=@module_id";
                db.CreateParameters(2);
                db.AddParameters(0, "@user_id", UserId);
                db.AddParameters(1, "@module_id", ModuleId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                dynamic Permission = new ExpandoObject();
                Permission.View = Convert.ToBoolean(dt.Rows[0]["View"]);
                Permission.Create = Convert.ToBoolean(dt.Rows[0]["Create"]);
                Permission.Update = Convert.ToBoolean(dt.Rows[0]["Update"]);
                Permission.Delete = Convert.ToBoolean(dt.Rows[0]["Delete"]);
                Permission.All = Convert.ToBoolean(dt.Rows[0]["All"]);
                return Permission;
            }

            catch (Exception ex)
            {
                dynamic Permission = new ExpandoObject();
                Permission.View = false;
                Permission.Create = false;
                Permission.Update = false;
                Permission.Delete = false;
                Permission.All = false;
                Application.Helper.LogException(ex, "Permissions | GetUserPermission(int UserId,int ModuleId)");
                return Permission;
            }
        }

        public static dynamic GetGroupPermission(int GroupId, int ModuleId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select case when (b.Permission  &1)=1 then 1 else 0 end [View] ,
                              case when (b.Permission  &2)=2 then 1 else 0 end [Create] ,
                              case when (b.Permission  &4)=4 then 1 else 0 end [Update] ,
                              case when (b.Permission  &8)=8 then 1 else 0 end [Delete] ,
                              case when (b.Permission  &16)=16 then 1 else 0 end [All] 
                              from TBL_USER_GROUP_MST a
                              INNER JOIN TBL_GROUP_PERMISSIONS b on a.User_Group_Id = b.Group_Id
                              where a.User_Group_Id =@group_id and b.Module_Id=@module_id";
                db.CreateParameters(2);
                db.AddParameters(0, "@group_id", GroupId);
                db.AddParameters(1, "@module_id", ModuleId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                dynamic Permission = new ExpandoObject();
                Permission.View = Convert.ToBoolean(dt.Rows[0]["View"]);
                Permission.Create = Convert.ToBoolean(dt.Rows[0]["Create"]);
                Permission.Update = Convert.ToBoolean(dt.Rows[0]["Update"]);
                Permission.Delete = Convert.ToBoolean(dt.Rows[0]["Delete"]);
                Permission.All = Convert.ToBoolean(dt.Rows[0]["All"]);
                return Permission;
            }
            catch(Exception ex)
            {
                dynamic Permission = new ExpandoObject();
                Permission.View = false;
                Permission.Create = false;
                Permission.Update = false;
                Permission.Delete = false;
                Permission.All = false;
                Application.Helper.LogException(ex, "Permissions | GetGroupPermission(int GroupId, int ModuleId)");
                return Permission;                
            }
        }

        public static OutputMessage SetGroupPermission(dynamic Permission,int ModuleId,int GroupId,int CreatedBy)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"  if exists (SELECT 1 FROM TBL_GROUP_PERMISSIONS where Group_Id=@groupId and Module_Id=@moduleId)
                                     begin
                                    	UPDATE TBL_GROUP_PERMISSIONS SET Permission=0 where Group_Id=@groupId and Module_Id=@moduleId;
                                    	SELECT TOP 1 Group_Permission_Id[IDENTITY] FROM TBL_GROUP_PERMISSIONS where Group_Id=@groupId   and             Module_Id=@moduleId;
                                     end
                                     ELSE
                                     BEGIN
                                     insert into TBL_GROUP_PERMISSIONS (Module_Id,Group_Id,Permission,Created_By,Created_Date)values
                                     (@moduleId,@groupId,0,@userId,GETUTCDATE());select @@identity[IDENTITY];
                                     END";
                db.CreateParameters(3);
                db.AddParameters(0, "@groupId", GroupId);
                db.AddParameters(1, "@moduleId", ModuleId);
                db.AddParameters(2, "@userId", CreatedBy);
                db.Open();
                db.BeginTransaction();
                int identity=Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));

                if(identity>0)
                {
                    if (Permission.View)
                    {
                        query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|1 where Group_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if (Permission.Create)
                    {
                        query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|3 where Group_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if (Permission.Update)
                    {
                        query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|5 where Group_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if(Permission.Delete)
                    {
                        query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|9 where Group_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if(Permission.All)
                    {
                        query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|31 where Group_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                }
                db.CommitTransaction();
                return new OutputMessage("Permission Set Succesfully", true, Type.NoError, "Security|Group|Permission", System.Net.HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something Went Wrong", true, Type.Others, "Security|Group|Permission", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }

        public static OutputMessage SetUserPermission(dynamic Permission, int ModuleId, int UserId, int CreatedBy)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"  if exists (SELECT 1 FROM TBL_USER_PERMISSIONS where [User_Id]=@userId and Module_Id=@moduleId)
                                  begin
                                	UPDATE TBL_USER_PERMISSIONS SET Permission=0 where [User_Id]=@userId and Module_Id=@moduleId;
                                	SELECT TOP 1 [User_Permission_Id] FROM TBL_USER_PERMISSIONS where [User_Id]=@userId and Module_Id=@moduleId;
                                  end
                                  ELSE
                                  BEGIN
                                  insert into TBL_USER_PERMISSIONS (Module_Id,[User_Id],Permission,Created_By,Created_Date)values
                                  (@moduleId,@userId,0,@createdBy,GETUTCDATE());select @@identity[IDENTITY];
                                  END";
                db.CreateParameters(3);
                db.AddParameters(0, "@userId", UserId);
                db.AddParameters(1, "@moduleId", ModuleId);
                db.AddParameters(2, "@createdBy", CreatedBy);
                db.Open();
                db.BeginTransaction();
                int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));

                if (identity > 0)
                {
                    if (Permission.View)
                    {
                        query = @"update TBL_USER_PERMISSIONS set Permission=Permission|1 where User_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if (Permission.Create)
                    {
                        query = @"update TBL_USER_PERMISSIONS set Permission=Permission|3 where User_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if (Permission.Update)
                    {
                        query = @"update TBL_USER_PERMISSIONS set Permission=Permission|5 where User_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    if (Permission.Delete)
                    {
                        query = @"update TBL_USER_PERMISSIONS set Permission=Permission|9 where User_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);                                                                  
                    }
                    if (Permission.All)
                    {
                        query = @"update TBL_USER_PERMISSIONS set Permission=Permission|31 where User_Permission_Id =@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", identity);
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                }
                db.CommitTransaction();
                return new OutputMessage("Permission Set Succesfully", true, Type.NoError, "Security|Group|Permission", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something Went Wrong", true, Type.Others, "Security|Group|Permission", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }

        public static object GetUserPermission(int UserId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select b.module_id,
                                 case when (b.Permission  &1)=1 then 1 else 0 end [View] ,
                                 case when (b.Permission  &2)=2 then 1 else 0 end [Create] ,
                                 case when (b.Permission  &4)=4 then 1 else 0 end [Update] ,
                                 case when (b.Permission  &8)=8 then 1 else 0 end [Delete] ,
                                 case when (b.Permission  &16)=16 then 1 else 0 end [All] 
                                 from TBL_USER_MST a 
                                 INNER JOIN TBL_USER_PERMISSIONS b on a.[User_Id] = b.[User_Id]
                                 where a.[User_Id] =@userId";
                db.CreateParameters(1);
                db.AddParameters(0, "@userId", UserId);
                db.Open();
                return db.ExecuteQuery(CommandType.Text, query);
            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Permissions | GetUserPermission(int UserId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public static object GetGroupPermission(int GroupId)
        {
            DBManager db = new DBManager();
            try
            {
                
                db.CreateParameters(1);
                db.AddParameters(0, "@GroupId", GroupId);
                db.Open();
                 return db.ExecuteQuery(CommandType.Text, @"select b.Module_Id, case when (b.Permission  &1)=1 then 1 else 0 end [View] ,case when (b.Permission  &2)=2 then 1 else 0 end [Create] ,case when (b.Permission  &4)=4 then 1 else 0 end [Update] ,case when (b.Permission  &8)=8 then 1 else 0 end [Delete] ,case when (b.Permission  &16)=16 then 1 else 0 end [All] from TBL_USER_GROUP_MST a INNER JOIN TBL_GROUP_PERMISSIONS b on a.User_Group_Id = b.Group_Id where a.User_Group_Id =@GroupId");
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Permissions | GetGroupPermission(int GroupId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static OutputMessage SetGroupPermission(dynamic Permission)
        {
            DBManager db = new DBManager();
            try
            {
               int GroupId = Convert.ToInt32(Permission.PartyId);
               int CreatedBy = Convert.ToInt32(Permission.UserId);
                db.Open();
                db.BeginTransaction();
                for (int i = 0; i < Permission.Permissions.Count; i++)
                {
                    string query = @"  if exists (SELECT 1 FROM TBL_GROUP_PERMISSIONS where Group_Id=@groupId and Module_Id=@moduleId)
                                     begin
                                    	UPDATE TBL_GROUP_PERMISSIONS SET Permission=0 where Group_Id=@groupId and Module_Id=@moduleId;
                                    	SELECT TOP 1 Group_Permission_Id[IDENTITY] FROM TBL_GROUP_PERMISSIONS where Group_Id=@groupId and Module_Id=@moduleId;
                                     end
                                     ELSE
                                     BEGIN
                                     insert into TBL_GROUP_PERMISSIONS (Module_Id,Group_Id,Permission,Created_By,Created_Date)values
                                     (@moduleId,@groupId,0,@userId,GETUTCDATE());select @@identity[IDENTITY];
                                     END";
                    db.CleanupParameters();
                    db.CreateParameters(3);
                    db.AddParameters(0, "@groupId", GroupId);
                    db.AddParameters(1, "@moduleId", Convert.ToInt32(Permission.Permissions[i].ModuleId));
                    db.AddParameters(2, "@userId", CreatedBy);
                    int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    if (identity > 0)
                    {
                        if ((bool)Permission.Permissions[i].View)
                        {
                            query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|1 where Group_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Create)
                        {
                            query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|3 where Group_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Update)
                        {
                            query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|5 where Group_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Delete)
                        {
                            query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|9 where Group_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].All)
                        {
                            query = @"update TBL_GROUP_PERMISSIONS set Permission=Permission|31 where Group_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                    }
                }
                db.CommitTransaction();
                return new OutputMessage("Permissions Set Succesfully", true, Type.NoError, "Security|Group|Permission", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something Went Wrong", true, Type.Others, "Security|Group|Permission", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }

        public static OutputMessage SetUserPermission(dynamic Permission)
        {
            DBManager db = new DBManager();
            try
            {
                int UserId = Convert.ToInt32(Permission.PartyId);
                int CreatedBy = Convert.ToInt32(Permission.UserId);
                db.Open();
                db.BeginTransaction();
                for (int i = 0; i < Permission.Permissions.Count; i++)
                {
                    string query = @"if exists (SELECT 1 FROM TBL_USER_PERMISSIONS where [User_Id]=@userId and Module_Id=@moduleId)
                                  begin
                                	UPDATE TBL_USER_PERMISSIONS SET Permission=0 where [User_Id]=@userId and Module_Id=@moduleId;
                                	SELECT TOP 1 [User_Permission_Id] FROM TBL_USER_PERMISSIONS where [User_Id]=@userId and Module_Id=@moduleId;
                                  end
                                  ELSE
                                  BEGIN
                                  insert into TBL_USER_PERMISSIONS (Module_Id,[User_Id],Permission,Created_By,Created_Date)values
                                  (@moduleId,@userId,0,@createdBy,GETUTCDATE());select @@identity[IDENTITY];
                                  END";
                    db.CleanupParameters();
                    db.CreateParameters(3);
                    db.AddParameters(0, "@userId", UserId);
                    db.AddParameters(1, "@moduleId", Convert.ToInt32(Permission.Permissions[i].ModuleId));
                    db.AddParameters(2, "@createdBy", CreatedBy);
                    int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    if (identity > 0)
                    {
                        if ((bool)Permission.Permissions[i].View)
                        {
                            query = @"update TBL_USER_PERMISSIONS set Permission=Permission|1 where User_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Create)
                        {
                            query = @"update TBL_USER_PERMISSIONS set Permission=Permission|3 where User_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Update)
                        {
                            query = @"update TBL_USER_PERMISSIONS set Permission=Permission|5 where User_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].Delete)
                        {
                            query = @"update TBL_USER_PERMISSIONS set Permission=Permission|9 where User_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                        if ((bool)Permission.Permissions[i].All)                                                                                        
                        {
                            query = @"update TBL_USER_PERMISSIONS set Permission=Permission|31 where User_Permission_Id =@id";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@id", identity);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                    }
                }
                db.CommitTransaction();
                return new OutputMessage("Permissions Set Succesfully", true, Type.NoError, "Security|Group|Permission", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something Went Wrong", true, Type.Others, "Security|Group|Permission", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
    }



}
