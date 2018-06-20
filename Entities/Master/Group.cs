using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Group
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Modifieddate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of Group for populating dropdown list of groups
        /// </summary>
        /// <param name="CompanyId">Company id of that particular group</param>
        /// <returns>dropdown list of group </returns>
        public static DataTable GetGroup(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Group_ID],[Name] FROM [dbo].[TBL_GROUP_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text,query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Group | GetGroup(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of groups
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Group, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Group | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Group name must not be empty", false, Type.RequiredFields, "Group | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_GROUP_MST](Name,[Order],[Status],Created_By,Created_Date,Company_Id ) values(@Name,@Order,@Status,@Created_By,GETUTCDATE(),@Company_Id);select @@identity";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        if (identity >= 1)
                        {
                            return new OutputMessage("Group saved successfully ", true, Type.NoError, "Group | Save", System.Net.HttpStatusCode.OK,identity);

                        }
                        else
                        {
                            return new OutputMessage("Someting went wrong. Group could not be saved ", false, Type.Others, "Group | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Someting went wrong. Group could not be saved ", false, Type.Others, "Group | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        
                      db.Close();
                        
                    }

                }
            }
        }
        /// <summary>
        /// Update details of Groups
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message </returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Group, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Group | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID==0)
            {
                return new OutputMessage("ID must not be empty", false, Type.Others, "Group | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if(string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.RequiredFields, "Group | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_GROUP_MST] set Name=@Name,[Order]=@Order,[Status]=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Group_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Order", this.Order);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Details updated successfully", true, Type.NoError, "Group | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong .Group could not be updated", false, Type.Others, "Group | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Group could not be updated", false, Type.Others, "Group | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                      db.Close();
                      
                    }

                }
            }
        }
        /// <summary>
        /// Delete individual group from group master.
        /// For delete an entry first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Group, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Group | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID!=0)
                    {
                        db.Open();
                        string query = @"delete from TBL_GROUP_MST where Group_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Group deleted successfully", true, Type.NoError, "Group | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Group could not be deleted", false, Type.Others, "Group | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "Group | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this group because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "group | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "group | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Group could not be deleted", false, Type.Others, "group | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                    
                        db.Close();
                    
                }

            }
        }

        /// <summary>
        ///  Retrieve a list of group from group master under a particular company id
        /// </summary>
        /// <param name="CompanyId">Company id of group list</param>
        /// <returns>list of groups</returns>
        public static List<Group> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select g.Group_ID,g.Name,g.[Order],isnull(g.[Status],0)[Status],isnull(g.Created_By,0)[Created_By],
                               g.Created_Date,isnull(g.Modified_By,0)[Modified_By],g.Modified_Date,
                               isnull(g.Company_Id,0)[Company_Id],c.Name[Company] from TBL_GROUP_MST g
                               left join TBL_COMPANY_MST c on c.Company_Id = g.Company_Id where c.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Group> result = new List<Group>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Group group = new Group();
                        group.ID = item["group_Id"] != DBNull.Value ? Convert.ToInt32(item["group_Id"]) : 0;
                        group.Name = Convert.ToString(item["Name"]);
                        group.Order= item["order"] != DBNull.Value ? Convert.ToInt32(item["order"]) : 0;
                        group.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
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
                Application.Helper.LogException(ex, "Group | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single group from group master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>details of a single group </returns>
        public static Group GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 g.Group_ID,g.Name,g.[Order],isnull(g.[Status],0)[Status],isnull(g.Created_By,0)[Created_By],
                               g.Created_Date,isnull(g.Modified_By,0)[Modified_By],g.Modified_Date,
                               isnull(g.Company_Id,0)[Company_Id],c.Name[Company] from TBL_GROUP_MST g
                               left join TBL_COMPANY_MST c on c.Company_Id = g.Company_Id where c.Company_Id=@Company_Id and Group_ID=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                if (dt != null)
                {
                    
                        Group group = new Group();
                        DataRow item = dt.Rows[0];
                        group.ID = item["group_Id"] != DBNull.Value ? Convert.ToInt32(item["group_Id"]) : 0;
                        group.Name = Convert.ToString(item["Name"]);
                        group.Order = item["order"] != DBNull.Value ? Convert.ToInt32(item["order"]) : 0;
                        group.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        return group;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Group | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
