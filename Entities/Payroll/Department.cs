using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
    public class Department
    {
        #region properties
        public int ID { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of Department for populating dropdown list of Departments
        /// </summary>
        /// <param name="CompanyId">company id of the department list</param>
        /// <returns>dropdown list of department names</returns>
        public static DataTable GetDepartment(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Department_Id],[Department] FROM [dbo].[TBL_DEPARTMENT_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text,query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Department | GetDepartment(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }

            }
        }
        /// <summary>
        /// Save details of each department
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Department, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Department | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Department name must not be empty", false, Type.RequiredFields, "Department | Save", System.Net.HttpStatusCode.InternalServerError);


            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_DEPARTMENT_MST](Company_Id,Department,Status,Created_By,Created_Date ) 
                        values(@Company_Id,@Department,@Status,@Created_By,GETUTCDATE())";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Company_Id", this.CompanyId);
                        db.AddParameters(1, "@Department", this.Name);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Created_By", this.CreatedBy);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Department Saved successfully", true, Type.NoError, "Department | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Department could not be saved", false, Type.Others, "Department | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Department could not be saved", false, Type.Others, "Department | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Update details of each department
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Department, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Department | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "Department | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.RequiredFields, "Department | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_DEPARTMENT_MST] set Department=@Department,
                       Status=@Status, Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Department_Id=@id";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Department", this.Name);
                        db.AddParameters(1, "@Status", this.Status);
                        db.AddParameters(2, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(3, "@id", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Department Updated Successfully", true, Type.NoError, "Department | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Department could not be updated", false, Type.Others, "Department | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Department could not be updated", false, Type.Others, "Department | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }


                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual department from department master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success a</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Department, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Department | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_DEPARTMENT_MST where Department_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Department deleted successfully", true, Type.NoError, "Department | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Department could not be deleted", false, Type.Others, "Department | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be empty for deletion", false, Type.Others, "Department | Update", System.Net.HttpStatusCode.InternalServerError);
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

                            return new OutputMessage("You cannot delete this department because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Department | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Department | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Department could not be deleted", false, Type.Others, "Department | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    
                        db.Close();
                    
                }

            }
        }
        /// <summary>
        /// Retrieve all Departments from department master
        /// </summary>
        /// <param name="CompanyId">company id of the department list</param>
        /// <returns>list of departments</returns>
        public static List<Department> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select d.Department_Id,isnull(d.Company_Id,0)[Company_Id],d.Department,isnull(d.[Status],0)[Status],
                               isnull(d.Created_By,0)[Created_By],d.Created_Date,isnull(d.Modified_By,0)[Modified_By],
                               d.Modified_Date,c.Name[Company] from [TBL_DEPARTMENT_MST] d
                               left join TBL_COMPANY_MST c on d.Company_Id = c.Company_Id where c.Company_Id=@Company_Id order by Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Department> result = new List<Department>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Department dept = new Department();
                        dept.ID = (item["Department_Id"] != DBNull.Value) ? Convert.ToInt32(item["Department_Id"]) : 0;
                        dept.Name = Convert.ToString(item["Department"]);
                        dept.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                        dept.CreatedBy =item["Created_By"]!=DBNull.Value? Convert.ToInt32(item["Created_By"]):0;
                        result.Add(dept);
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
                Application.Helper.LogException(ex, "Department | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single department from list of departments
        /// </summary>
        /// <param name="Id">id of the particular department which u want to retrieve</param>
        /// <param name="CompanyId">company id of the particular department</param>
        /// <returns>single department details</returns>
        public static Department GetDetails(int Id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 d.Department_Id,isnull(d.Company_Id,0)[Company_Id],d.Department,isnull(d.[Status],0)[Status],
                               isnull(d.Created_By,0)[Created_By],d.Created_Date,isnull(d.Modified_By,0)[Modified_By],
                               d.Modified_Date,c.Name[Company]
                               from[TBL_DEPARTMENT_MST] d
                               left join TBL_COMPANY_MST c on d.Company_Id = c.Company_Id where c.Company_Id=@Company_Id and Department_Id=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                DataRow item = dt.Rows[0];
                Department dept = new Department();
                dept.ID = (item["Department_Id"] != DBNull.Value) ? Convert.ToInt32(item["Department_Id"]) : 0;
                dept.Name = Convert.ToString(item["Department"]);
                dept.Status = (item["Status"] != DBNull.Value) ? Convert.ToInt32(item["Status"]) : 0;
                dept.CreatedBy = (item["Created_By"] != DBNull.Value) ? Convert.ToInt32(item["Created_By"]) : 0;
                return dept;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Department | GetDetails(int Id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
    }
}




