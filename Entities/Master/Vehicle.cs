using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entities.Master
{
    public class Vehicle
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of vehicles for populating dropdownlist of vehicles
        /// </summary>
        /// <param name="CompanyId">company id of that particular item</param>
        /// <returns>dropdown list of vehicles</returns>
        public static DataTable GetVehicle(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Vehicle_Id],[Name] FROM [dbo].[TBL_VEHICLE_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Vehicle | GetVehicle(int CompanyId)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each vehicles
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Vehicle, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Vehicle | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Vehicle Name must not be Empty", false, Entities.Type.RequiredFields, "Vehicle | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into TBL_VEHICLE_MST (Name,Number,Company_Id,Type,Owner,Status,Created_By,Created_Date)
                                       values(@Name,@Number,@Company_Id,@Type_Id,@Owner,@Status,@Created_By,GETUTCDATE());select @@identity";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Number", this.Number);
                        db.AddParameters(2, "@Company_Id", this.CompanyId);
                        db.AddParameters(3, "@Type_Id", this.Type);
                        db.AddParameters(4, "@Owner", this.Owner);
                        db.AddParameters(5, "@Status", this.Status);
                        db.AddParameters(6, "@Created_By", this.CreatedBy);
                        int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        //bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (identity >= 1)
                        {
                            return new OutputMessage("Vehicle saved successfully", true, Entities.Type.NoError, "Vehicle | Save", System.Net.HttpStatusCode.OK, identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Vehicle could not be saved", false, Entities.Type.Others, "Vehicle | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Vehicle could not be saved", false, Entities.Type.Others, "Vehicle | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Update details of each vehicles
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Vehicle, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Vehicle | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Entities.Type.RequiredFields, "Vehicle | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Vehicle Name must not be empty", false, Entities.Type.RequiredFields, "Vehicle | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = "update TBL_VEHICLE_MST set Name=@Name,Number=@Number,Type=@Type_Id,Owner=@Owner,Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Vehicle_Id=@id";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Number", this.Number);
                        db.AddParameters(2, "@Type_Id", this.Type);
                        db.AddParameters(3, "@Owner", this.Owner);
                        db.AddParameters(4, "@Status", this.Status);
                        db.AddParameters(5, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(6, "@id", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Vehicle updated successfully ", true, Entities.Type.NoError, "Vehicle | Update", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Vehicle could not be updated", false, Entities.Type.Others, "Vehicle | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Vehicle could not be updated", false, Entities.Type.Others, "Vehicle | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Delete individual vehicles from vehicle master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Vehicle, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = "delete from TBL_VEHICLE_MST where Vehicle_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Vehicle Deleted Successfully", true, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Vehicle could not be deleted", false, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id is empty. Vehicle could not be deleted", false, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this Vehicle because it is referenced in other transactions", false, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Vehicle could not be deleted", false, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Vehicle could not be deleted", false, Entities.Type.Others, "Vehicle | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Retrieves all the vehicle from the vehicle Master
        /// </summary>
        /// <returns>List of vehicles</returns>
        public static List<Vehicle> GetDetails(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    string query = @"Select v.Vehicle_Id,v.Name,v.Number,isnull(v.Company_Id,0)[Company_Id],v.[Type],v.[Owner],isnull(v.[Status],0)[Status],
                                   v.Created_By, v.Created_Date, v.Modified_By, v.Modified_Date,
                                   C.Name[Company] 
                                   from TBL_VEHICLE_MST v
                                   left join TBL_COMPANY_MST C on C.Company_Id = v.Company_Id 
                                   where c.Company_Id=@Company_Id order by Created_Date desc";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<Vehicle> result = new List<Vehicle>();
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            Vehicle veh = new Vehicle();
                            veh.ID = (item["Vehicle_Id"] != DBNull.Value) ? Convert.ToInt32(item["Vehicle_Id"]) : 0;
                            veh.Name = Convert.ToString(item["Name"]);
                            veh.Number = Convert.ToString(item["Number"]);
                            veh.Type = Convert.ToString(item["Type"]);
                            veh.Owner = Convert.ToString(item["Owner"]);
                            veh.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                            veh.Type = Convert.ToString(item["Type"]);
                            result.Add(veh);

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
                    Application.Helper.LogException(ex, "Vehicle | GetDetails(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Retrieve a single vehicle from the vehicle Master
        /// </summary>
        /// <returns>vehicle list</returns>
        public static Vehicle GetDetails(int id, int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    string query = @"Select top 1 v.Vehicle_Id,v.Name,v.Number,v.Company_Id,v.[Type],v.[Owner],v.[Status],
                                                                     v.Created_By, v.Created_Date, v.Modified_By, v.Modified_Date, 
                                                                     C.Name[Company] from TBL_VEHICLE_MST v 
                                                                     left join TBL_COMPANY_MST C on C.Company_Id = v.Company_Id where c.Company_Id=@Company_Id and Vehicle_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    db.AddParameters(1, "@id", id);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    DataRow item = dt.Rows[0];
                    Vehicle vehicle = new Vehicle();
                    vehicle.ID = (item["Vehicle_Id"] != DBNull.Value) ? Convert.ToInt32(item["Vehicle_Id"]) : 0;
                    vehicle.Name = Convert.ToString(item["Name"]);
                    vehicle.Number = Convert.ToString(item["Number"]);
                    vehicle.Type = Convert.ToString(item["Type"]);
                    vehicle.Owner = Convert.ToString(item["Owner"]);
                    vehicle.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                    vehicle.Type = Convert.ToString(item["Type"]);
                    return vehicle;
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Vehicle |  GetDetails(int id, int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }


    }
}



