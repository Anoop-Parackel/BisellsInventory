using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
    public class Designation
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }
        public string Department { get; set; }
        public int CompanyId { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of Designation for populating dropdown list of designation
        /// </summary>
        /// <param name="CompanyId">company id of the designation list</param>
        /// <returns>dropdown list of designation</returns>


        public static DataTable GetDesignation(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Designation_Id],[Designation] FROM [dbo].[TBL_DESIGNATION_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text,query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Designation | GetDesignation(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }

            }
        }
        /// <summary>
        /// Save details of each designations
        /// to save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Designation, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Designation | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
              
                 return new OutputMessage("Designation name must not be empty", false, Type.Others, "Designation | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_DESIGNATION_MST](Designation,Department_id,Company_Id,Status,Created_By,Created_Date ) 
                        values(@Designation,@Department_Id,@Company_Id,@Status,@Created_By,GETUTCDATE())";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Designation", this.Name);
                        db.AddParameters(1, "@Department_Id", this.DepartmentId);
                        db.AddParameters(2, "@Company_Id", this.CompanyId);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(4, "@Created_By", this.CreatedBy);
                        bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Designation saved Successfully", true, Type.NoError, "Designation | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Designation could not be saved", false, Type.Others, "Designation | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Designation could not be saved", false, Type.Others, "Designation | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Update details of each designations
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Designation, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Designation | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                
                return new OutputMessage("ID must not be empty", false, Type.Others, "Designation | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Designations must not be empty", false, Type.RequiredFields, "Designation | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_DESIGNATION_MST] set Designation=@Designation,Department_id=@Department_Id,
                      Status=@Status, Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Designation_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Designation", this.Name);
                        db.AddParameters(1, "@Department_Id", this.DepartmentId);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);
                        bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Designation Updated Successfully", true, Type.NoError, "Designation | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Designation could not be updated", false, Type.Others, "Designation | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Designation could not be updated", false, Type.Others, "Designation | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual designations from designation master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {

                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Designation, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_DESIGNATION_MST where Designation_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status= Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Designation deleted successfully", true, Type.NoError, "Designation | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Designation could not be deleted", false, Type.Others, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be empty", false, Type.Others, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this designation because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Designation could not be deleted", false, Type.Others, "Designation | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }


                }
                finally
                {
                    
                        db.Close();
                    
                }

            }
        }
        /// <summary>
        /// Retrieve all designations from designation master
        /// </summary>
        /// <param name="CompanyId">company id of the designation list</param>
        /// <returns>list of designations</returns>
        public static List<Designation> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select d.Designation_id,d.Designation,isnull(d.Company_ID,0)[Company_ID],isnull(d.[Status],0)[Status],
                               isnull(d.Created_By,0)[Created_By],d.Created_Date,isnull(d.Modified_By,0)[Modified_By],
                               d.Modified_Date,isnull(d.Department_Id,0)[Department_Id],
                               c.Name[Company],de.Department[Department]
                               from TBL_DESIGNATION_MST d
                               left join TBL_COMPANY_MST c on d.Company_ID = c.Company_Id
                               left join TBL_DEPARTMENT_MST de on de.Department_Id = d.Department_Id where c.Company_ID=@Company_Id order by Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                List<Designation> result = new List<Designation>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Designation desig = new Designation();
                        desig.ID = (item["Designation_Id"] != DBNull.Value) ? Convert.ToInt32(item["Designation_Id"]) : 0;
                        desig.Name = Convert.ToString(item["Designation"]);
                        desig.DepartmentId = item["Department_Id"] != DBNull.Value? Convert.ToInt32(item["Department_Id"]):0;
                        desig.CompanyId = (item["Company_Id"] != DBNull.Value) ? Convert.ToInt32(item["Company_Id"]) : 0;
                        desig.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                        desig.Department = Convert.ToString(item["Department"]);
                        desig.CreatedBy =item["Created_By"]!=DBNull.Value? Convert.ToInt32(item["Created_By"]):0;
                        result.Add(desig);
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
                Application.Helper.LogException(ex, "Designation | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single designation from list of designations
        /// </summary>
        /// <param name="Id">id of the particular designation which u want  to retrieve</param>
        /// <param name="CompanyId">company id of the particular designation</param>
        /// <returns>details of a single designations</returns>
        public static Designation GetDetails(int Id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 d.Designation_id,d.Designation,isnull(d.Company_ID,0)[Company_ID],isnull(d.[Status],0)[Status],
                                isnull(d.Created_By,0)[Created_By],d.Created_Date,isnull(d.Modified_By,0)[Modified_By],
                                d.Modified_Date,isnull(d.Department_Id,0)[Department_Id],
                                c.Name[Company],de.Department[Department] 
                                from TBL_DESIGNATION_MST d
                                left join TBL_COMPANY_MST c on d.Company_ID = c.Company_Id
                                left join TBL_DEPARTMENT_MST de on de.Department_Id = d.Department_Id where c.Company_ID =@Company_Id and Designation_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
               
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataRow item = dt.Rows[0];
                Designation desig = new Designation();
                desig.ID = (item["Designation_Id"] != DBNull.Value) ? Convert.ToInt32(item["Designation_Id"]) : 0;
                desig.Name = Convert.ToString(item["Designation"]);
                desig.DepartmentId = item["Department_Id"]!=DBNull.Value? Convert.ToInt32(item["Department_Id"]):0;
                desig.CompanyId = (item["Company_Id"] != DBNull.Value) ? Convert.ToInt32(item["Company_Id"]) : 0;
                desig.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                desig.CreatedBy = (item["Created_By"] != DBNull.Value) ? Convert.ToInt32(item["Created_By"]) : 0;
                desig.Company = Convert.ToString(item["Company"]);
                desig.Department = Convert.ToString(item["Department"]);
                return desig;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Designation | GetDetails(int Id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

    }
}
