using Core.DBManager;
using System;
using Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
    public class SalaryComponent
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string ComponentType { get; set; }
        public string CalculationType { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of salary components for populating dropdown list of salarycomponent
        /// </summary>
        /// <param name="CompanyId">company id of the particular salary component</param>
        /// <returns> dropdown list of salarycomponent</returns>
        public static DataTable GetSalaryComponent(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Component_Id],[Component_Name] FROM [dbo].[TBL_SALARY_COMPONENTS_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "salarycomponent |  GetSalaryComponent(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each salarycomponents
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalaryComponents, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "SalaryComponent | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Salary component must not be Empty", false, Type.RequiredFields, "SalaryComponent | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_SALARY_COMPONENTS_MST](Component_Name,Component_Type,Calculation_Type,Status,Created_By,Created_Date,Company_Id)
                        values(@Component_Name,@Component_Type,@Calculation_Type,@Status,@Created_By,GETUTCDATE(),@Company_Id)";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Component_Name", this.Name);
                        db.AddParameters(1, "@Component_Type", this.ComponentType);
                        db.AddParameters(2, "@Calculation_Type", this.CalculationType);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(5, "@Created_By", this.CreatedBy);
                        db.AddParameters(6, "@Company_Id", this.CompanyId);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Salary component Saved successfully", true, Type.NoError, "SalaryComponent | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Salary component could not be Saved", false, Type.Others, "SalaryComponent | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }

                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Salary component could not be Saved", false, Type.Others, "SalaryComponent | Save", System.Net.HttpStatusCode.InternalServerError, ex);

                    }

                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Update details of each salary components
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalaryComponents, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError);


            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {

                return new OutputMessage("Name must not be empty", false, Type.RequiredFields, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_SALARY_COMPONENTS_MST] set Component_Name=@Component_Name,Component_Type=@Component_Type,Calculation_Type=@Calculation_Type,
                         Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Component_Id=@id";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Component_Name", this.Name);
                        db.AddParameters(1, "@Component_Type", this.ComponentType);
                        db.AddParameters(2, "@Calculation_Type", this.CalculationType);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(5, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(6, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Salary component Updated successfully", true, Type.NoError, "SalaryComponent | Update", System.Net.HttpStatusCode.OK);


                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Salary component could not be Updated", false, Type.Others, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Salary component could not be Updated", false, Type.Others, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual salarycomponent from salarycomponent master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when details deleted successful otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalaryComponents, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_SALARY_COMPONENTS_MST where Component_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Salary component Deleted successfully", true, Type.NoError, "SalaryComponent | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Salary component could not be deleted", false, Type.Others, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {

                        return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "SalaryComponent | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this Salary component because it is referenced in other transactions", false, Entities.Type.Others, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Salary component could not be deleted", false, Entities.Type.Others, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. salary component could not be deleted", false, Type.Others, "SalaryComponent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }
    
        /// <summary>
        /// Retrieves all the salarycomponent from the salarycomponent Master
        /// </summary>
        /// <param name="CompanyId">Company id of salary component list</param>
        /// <returns>list of salary components</returns>
        public static List<SalaryComponent> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select s.Component_Id,s.Component_Name[Component_Name],s.Component_Type[Component_Type],s.Calculation_Type[Calculation_Type],
                               isnull(s.[Status],0)[Status],isnull(s.Created_By,0)[Created_By],
                               s.Created_Date,isnull(s.Modified_By,0)[Modified_By],s.Modified_Date, 
                               isnull(s.Company_Id,0)[Company_Id],l.Name[Company]
                               from TBL_SALARY_COMPONENTS_MST s
                               left join TBL_COMPANY_MST l on l.Company_Id = s.Company_Id 
                               where l.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<SalaryComponent> result = new List<SalaryComponent>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        SalaryComponent salary = new SalaryComponent();
                        salary.ID = item["Component_id"] != DBNull.Value ? Convert.ToInt32(item["Component_id"]) : 0;
                        salary.Name = Convert.ToString(item["Component_Name"]);
                        salary.ComponentType = Convert.ToString(item["Component_Type"]);
                        salary.CalculationType = Convert.ToString(item["Calculation_Type"]);
                        salary.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                     result.Add(salary);
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
                Application.Helper.LogException(ex, "salarycomponent |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single salarycomponents from the salarycomponents Master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>details of single salary component</returns>
        public static SalaryComponent GetDetails(int id,int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 s.Component_Id,s.Component_Name[Component_Name],s.Component_Type[Component_Type],
                        s.Calculation_Type[Calculation_Type],isnull(s.[Status],0)[Status],isnull(s.Created_By,0)[Created_By],
                        s.Created_Date,isnull(s.Modified_By,0)[Modified_By],s.Modified_Date, 
                        isnull(s.Company_Id,0)[Company_Id],l.Name[Company] 
                        from TBL_SALARY_COMPONENTS_MST s
                        left join TBL_COMPANY_MST l on l.Company_Id = s.Company_Id  
                        where l.Company_Id=@Company_Id and Component_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    SalaryComponent salary = new SalaryComponent();
                    DataRow item = dt.Rows[0];
                    salary.ID = item["Component_id"] != DBNull.Value ? Convert.ToInt32(item["Component_id"]) : 0;
                    salary.Name = Convert.ToString(item["Component_Name"]);
                    salary.ComponentType = Convert.ToString(item["Component_Type"]);
                    salary.CalculationType = Convert.ToString(item["Calculation_Type"]);
                    salary.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                   return salary;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "salarycomponent | GetDetails(int id,int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
   }
}
