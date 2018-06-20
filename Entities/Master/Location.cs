using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Location
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Contact { get; set; }
        public string ContactPerson { get; set; }
        public string RegId1 { get; set; }
        public string RegId2 { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of location for populating dropdown list of location
        /// </summary>
        /// <param name="ComapnyId">Company id of that particular group</param>
        /// <returns>dropdown list of location</returns>
        public static DataTable GetLocation(int ComapnyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Location_Id],[Name] FROM [dbo].[TBL_LOCATION_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", ComapnyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Location  | GetLocation(int ComapnyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each location
        ///  for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Location, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Location | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Location name must not be empty", false, Type.RequiredFields, "Location | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if(string.IsNullOrWhiteSpace(this.ContactPerson))
            {
                return new OutputMessage("Contact Person must not be empty", false, Type.RequiredFields, "Location|Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.RegId1))
            {
                return new OutputMessage("Registration Id must not be empty", false, Type.RequiredFields, "Location|Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_LOCATION_MST](Name,Address1,Address2,Contact,Contact_Person,Reg_Id1,Reg_Id2,Status,Created_By,Created_Date,Company_Id) values(@Name,@Address1,@Address2,@Contact,@Contact_Person,@Reg_Id1,@Reg_Id2,@Status,@Created_By,GETUTCDATE(),@Company_Id)";
                        db.CreateParameters(10);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Contact", this.Contact);
                        db.AddParameters(4, "@Contact_Person", this.ContactPerson);
                        db.AddParameters(5, "@Reg_Id1", this.RegId1);
                        db.AddParameters(6, "@Reg_Id2", this.RegId2);
                        db.AddParameters(7, "@Status", this.Status);
                        db.AddParameters(8, "@Created_By", this.CreatedBy);
                        db.AddParameters(9, "@Company_Id", this.CompanyId);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Location Saved successfully", true, Entities.Type.NoError, "Location | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Location could not be saved", false, Entities.Type.Others, "Location | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Location could not be saved", false, Entities.Type.Others, "Location | Save", System.Net.HttpStatusCode.InternalServerError,ex);

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
        /// Update details of each location
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Location, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Location | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID==0)
            {
                return new OutputMessage("ID must not be empty", false, Entities.Type.Others, "Location | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if(string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Entities.Type.RequiredFields, "Location | Update", System.Net.HttpStatusCode.InternalServerError);
              
            }
            else if(string.IsNullOrWhiteSpace(this.ContactPerson))
            {
                return new OutputMessage("Contact Person must not be empty", false, Entities.Type.RequiredFields, "Location|Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if(string.IsNullOrWhiteSpace(this.RegId1))
            {
                return new OutputMessage("Registration Id must not be empty", false, Entities.Type.RequiredFields, "Location|Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_LOCATION_MST] set Name=@Name,Address1=@Address1,Address2=@Address2,
                         Contact=@Contact,Contact_Person=@Contact_Person,Reg_Id1=@Reg_Id1,Reg_Id2=@Reg_Id2,
                         Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Location_Id=@id"; 
                        db.CreateParameters(10);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address1", this.Address1);
                        db.AddParameters(2, "@Address2", this.Address2);
                        db.AddParameters(3, "@Contact", this.Contact);
                        db.AddParameters(4, "@Contact_Person", this.ContactPerson);
                        db.AddParameters(5, "@Reg_Id1", this.RegId1);
                        db.AddParameters(6, "@Reg_Id2", this.RegId2);
                        db.AddParameters(7, "@Status", this.Status);
                        db.AddParameters(8, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(9, "@id", this.ID);
                        if( Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Location  updated successfully ", true, Entities.Type.NoError, "Location | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Location could not be updated", false, Entities.Type.Others, "Location | Update", System.Net.HttpStatusCode.InternalServerError);

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

                                return new OutputMessage("You cannot update this location because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Location | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                            else
                            {
                                db.RollBackTransaction();

                                return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Location | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                            }
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Location could not be updated", false, Type.Others, "Location | Update", System.Net.HttpStatusCode.InternalServerError, ex);
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
        /// Retrieve a list of location from location master under a particular company id
        /// </summary>
        /// <param name="CompanyId">Company id of locations list</param>
        /// <returns>list of locations</returns>
        public static List<Location> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select l.Location_Id,l.Name,l.Address1,l.Address2,l.Contact,
                               l.Contact_Person,l.Reg_Id1,l.Reg_Id2,isnull(l.[Status],0)[Status],
                               isnull(l.Created_By,0)[Created_By],l.Created_Date,isnull(l.Modified_By,0)[Modified_By],
                               l.Modified_Date,isnull(l.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_LOCATION_MST l 
                               left join TBL_COMPANY_MST c on c.Company_Id = l.Company_Id  
                               where c.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Location> result = new List<Location>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Location location = new Location();
                        location.ID = item["location_Id"] != DBNull.Value ? Convert.ToInt32(item["Location_Id"]) : 0;
                        location.Name = Convert.ToString(item["Name"]);
                        location.Address1 = Convert.ToString(item["address1"]);
                        location.Address2 = Convert.ToString(item["address2"]);
                        location.Contact = Convert.ToString(item["contact"]);
                        location.ContactPerson = Convert.ToString(item["contact_person"]);
                        location.RegId1 = Convert.ToString(item["reg_id1"]);
                        location.RegId2 = Convert.ToString(item["reg_id2"]);
                        location.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                       result.Add(location);
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
                Application.Helper.LogException(ex, "Location  | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single location from location master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>details of single location</returns>
        public static Location GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 l.Location_Id,l.Name,l.Address1,l.Address2,
                               l.Contact,l.Contact_Person,l.Reg_Id1,l.Reg_Id2,
                               isnull(l.[Status],0)[Status],isnull(l.Created_By,0)[Created_By],l.Created_Date,isnull(l.Modified_By,0)[Modified_By], 
                               l.Modified_Date,isnull(l.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_LOCATION_MST l left join TBL_COMPANY_MST c on c.Company_Id = l.Company_Id 
                               where l.Company_Id=@Company_Id and l.Location_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    
                        Location location = new Location();
                        DataRow item= dt.Rows[0];
                        location.ID = item["location_Id"] != DBNull.Value ? Convert.ToInt32(item["Location_Id"]) : 0;
                        location.Name = Convert.ToString(item["Name"]);
                        location.Address1 = Convert.ToString(item["address1"]);
                        location.Address2 = Convert.ToString(item["address2"]);
                        location.Contact = Convert.ToString(item["contact"]);
                        location.ContactPerson = Convert.ToString(item["contact_person"]);
                        location.RegId1 = Convert.ToString(item["reg_id1"]);
                        location.RegId2 = Convert.ToString(item["reg_id2"]);
                        location.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                       return location;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Location  | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        ///  Delete individual location from location master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>

        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Location, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Location | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"select isnull(Is_System_Defined,0)[Is_System_Defined] from TBL_LOCATION_MST where Location_Id=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        bool IsSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                        if (IsSystemDefined)
                        {
                            return new OutputMessage("You cannot delete this location because it is system defined", false, Entities.Type.Others, "Location | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                        else
                        {
                       query = @"begin try delete from TBL_LOCATION_MST where Location_Id=@ID;select ERROR_NUMBER() end try begin catch select ERROR_NUMBER() ErrorId end catch";
                        object ErrorType = db.ExecuteScalar(CommandType.Text, query);
                        if (ErrorType == DBNull.Value)
                        {
                            OutputMessage msg = new OutputMessage();
                            msg.Operation = "Master|Location|Delete";
                            msg.Success = true;
                            msg.Message = "Location Deleted Successfully";
                            msg.ErrorType = Type.NoError;
                            return msg;
                        }
                        else
                        {
                            switch (Convert.ToInt32(ErrorType))
                            {
                                case 547:
                                    OutputMessage msg = new OutputMessage();
                                    msg.Operation = "Master|Location|Delete";
                                    msg.Success = false;
                                    msg.Message = "You cannot delete this location because it is already used in other transactions";
                                    msg.ErrorType = Type.ForeignKeyViolation;
                                    return msg;
                                default:
                                    OutputMessage msg2 = new OutputMessage();
                                    msg2.Operation = "Master|Location|Delete";
                                    msg2.Success = false;
                                    msg2.Message = "Something Went Wrong";
                                    msg2.ErrorType = Type.Others;
                                    return msg2;
                            }
                        }
                    }
                    }
                    else
                    {
                        throw new InvalidOperationException("ID must not be zero for deletion");
                    }
                }
                catch (Exception ex)
                {
                    OutputMessage msg = new OutputMessage();
                    msg.Operation = "Master|Location|Delete";
                    msg.Success = false;
                    msg.Message = "Something Went Wrong";
                    msg.ErrorType = Type.Others;
                    Application.Helper.LogException(ex, "Location | Delete()");
                    return msg;
                }
                finally
                {
                   
                        db.Close();
                   
                }

            }
        }
 }
}
