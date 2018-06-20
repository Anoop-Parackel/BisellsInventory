using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Entities.Payroll
{
    public class Employee
    {
        #region properties
        public int ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime DOB { get; set; }
        public string DobString { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public int NationalityId { get; set; }
        public string Nationality { get; set; }
        public string Mobile { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Designation { get; set; }
        public int DesignationId { get; set; }
        public string Grade { get; set; }
        public int Status { get; set; }
        public Byte[] PhotoUpload { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string PhotoPath { get; set; }
        public string Company { get; set; }
        public string PhotoBase64 { get; set; }
        public int SalaryType { get; set; }
        public int MonthlyTemplate { get; set; }
        public int HourlyTemplate { get; set; }
        public bool IsHourlyPaid { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of Employee for populating dropdown list of employee
        /// </summary>
        /// <param name="CompanyId">company id of that particular employee</param>
        /// <returns>dropdown list of employees</returns>
        public static DataTable GetEmployee(int CompanyId)
        {
            using (DBManager db = new Core.DBManager.DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Employee_Id],[First_Name] FROM [dbo].[TBL_EMPLOYEE_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Employee |  GetEmployee(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each employee
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Employee, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Employee | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.FirstName))
            {

                return new OutputMessage("Employee FirstName must not be empty", false, Type.Others, "Employee | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.PhotoBase64 == null)
            {
                return new OutputMessage("Upload an profile photo for employee", false, Type.Others, "Employee | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {

                using (DBManager db = new DBManager())
                {
                    try
                    {
                        if (this.PhotoBase64 != null)
                        {
                            try
                            {
                                string Basepath = WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                                string fullPath = Path.Combine(Basepath, "Resources\\Employees\\ProfileImages");
                                string guid = Guid.NewGuid().ToString();
                                string filePath = fullPath + "\\" + guid + ".jpeg";
                                if (!Directory.Exists(fullPath))
                                {
                                    Directory.CreateDirectory(fullPath);
                                }
                                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                                {
                                    byte[] file = (byte[])Convert.FromBase64String(this.PhotoBase64);
                                    fs.Write(file, 0, file.Length);
                                    fs.Flush();
                                    this.PhotoPath = "/Resources/Employees/ProfileImages/" + guid + ".jpeg";
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        db.Open();
                        string query = @"insert into [dbo].[TBL_EMPLOYEE_MST](Title,First_Name,Last_Name,Address,Date_Of_Birth,Gender,Email,Marital_Status,Blood_Group,Nationality,Mobile,City,Department_Id,Designation_Id,Status,Zip_Code,Created_By,Created_Date,Company_Id,photo_upload,Photo_Path ) 
                          values(@Title,@First_Name,@Last_Name,@Address,@Date_Of_Birth,@Gender,@Email,@Marital_Status,@Blood_Group,@Nationality,@Mobile,@City,@Department_Id,@Designation_Id,@Status,@Zip_Code,@Created_By,GETUTCDATE(),@Company_Id,@Photo_Upload,@Photo_Path)";

                        db.CreateParameters(20);
                        db.AddParameters(0, "@Title", this.Title);
                        db.AddParameters(1, "@First_Name", this.FirstName);
                        db.AddParameters(2, "@Last_Name", this.LastName);
                        db.AddParameters(3, "@Date_Of_Birth", this.DOB);
                        db.AddParameters(4, "@Gender", this.Gender);
                        db.AddParameters(5, "@Marital_Status", this.MaritalStatus);
                        db.AddParameters(6, "@Blood_Group", this.BloodGroup);
                        db.AddParameters(7, "@Nationality", this.NationalityId);
                        db.AddParameters(8, "@Mobile", this.Mobile);
                        db.AddParameters(9, "@Department_Id", this.DepartmentId);
                        db.AddParameters(10, "@Designation_Id", this.DesignationId);
                        db.AddParameters(11, "@City", this.City);
                        db.AddParameters(12, "@Status", this.Status);
                        db.AddParameters(13, "@Zip_Code", this.ZipCode);
                        db.AddParameters(14, "@Email", this.Email);
                        db.AddParameters(15, "@Address", this.Address);
                        db.AddParameters(16, "@Created_By", this.CreatedBy);
                        db.AddParameters(17, "@Company_Id", this.CompanyId);
                        db.AddParameters(18, "@Photo_Upload", (byte[])Convert.FromBase64String(this.PhotoBase64));
                        db.AddParameters(19, "@Photo_Path", this.PhotoPath);

                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Employee saved successfully", true, Type.NoError, "Emplpoyee | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Employee could not be saved", false, Type.Others, "Emplpoyee | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Employee could not be saved", false, Type.Others, "Emplpoyee | Save", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Update details of each employee
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of updated when details saved successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Employee, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Employee | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("ID must not be empty", false, Type.Others, "Employee | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.FirstName))
            {
                return new OutputMessage("Employee FirstName must not be empty", false, Type.Others, "Employee | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.PhotoBase64 == null)
            {
                return new OutputMessage("Upload an profile photo for employee", false, Type.Others, "Employee | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {

                using (DBManager db = new DBManager())
                {
                    try
                    {
                        if (this.PhotoBase64 != null)
                        {
                            try
                            {
                                string Basepath = WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                                string fullPath = Path.Combine(Basepath, "Resources\\Employees\\ProfileImages");
                                string guid = Guid.NewGuid().ToString();
                                string filePath = fullPath + "\\" + guid + ".jpeg";
                                if (!Directory.Exists(fullPath))
                                {
                                    Directory.CreateDirectory(fullPath);
                                }
                                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                                {
                                    byte[] file = (byte[])Convert.FromBase64String(this.PhotoBase64);
                                    fs.Write(file, 0, file.Length);
                                    fs.Flush();
                                    this.PhotoPath = "/Resources/Employees/ProfileImages/" + guid + ".jpeg";
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        db.Open();
                        string query = @"Update [dbo].[TBL_EMPLOYEE_MST] set Title=@Title,First_Name=@First_Name,Last_Name=@Last_Name,Address=@Address,Email=@Email,City=@City,Date_Of_Birth=@Date_Of_Birth,Gender=@Gender,Marital_Status=@Martial_Status,Blood_Group=@Blood_Group,Nationality=@Nationality,Mobile=@Mobile,Department_Id=@Department_Id,Designation_Id=@DesignationId,Grade=@Grade,Status=@Status,Photo_Upload=@Photo_Upload,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Photo_Path=@Photo_Path where [Employee_Id]=@id";
                        db.CreateParameters(20);
                        db.AddParameters(0, "@Title", this.Title);
                        db.AddParameters(1, "@First_Name", this.FirstName);
                        db.AddParameters(2, "@Last_Name", this.LastName);
                        db.AddParameters(3, "@Date_Of_Birth", this.DOB);
                        db.AddParameters(4, "@Gender", this.Gender);
                        db.AddParameters(5, "@Martial_Status", this.MaritalStatus);
                        db.AddParameters(6, "@Blood_Group", this.BloodGroup);
                        db.AddParameters(7, "@Nationality", this.NationalityId);
                        db.AddParameters(8, "@Mobile", this.Mobile);
                        db.AddParameters(9, "@Department_Id", this.DepartmentId);
                        db.AddParameters(10, "@DesignationId", this.DesignationId);
                        db.AddParameters(11, "@Grade", this.Grade);
                        db.AddParameters(12, "@Status", this.Status);
                        db.AddParameters(13, "@Photo_Upload", (byte[])Convert.FromBase64String(this.PhotoBase64));
                        db.AddParameters(14, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(15, "@id", this.ID);
                        db.AddParameters(16, "@Email", this.Email);
                        db.AddParameters(17, "@City", this.City);
                        db.AddParameters(18, "@Address", this.Address);
                        db.AddParameters(19, "@Photo_Path", this.PhotoPath);

                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Employee Updated successfully", true, Type.NoError, "Employee | Update", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Employee could not be updated", false, Type.Others, "Employee | Update", System.Net.HttpStatusCode.InternalServerError);


                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Employee could not be updated", false, Type.Others, "Employee | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual employee from employee master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Employee, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Employee | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from [TBL_EMPLOYEE_MST] where [Employee_Id]=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Employee Deleted successfully", true, Type.NoError, "Emplpoyee | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Employee could not be deleted", false, Type.Others, "Emplpoyee | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be Zero for Deleting", false, Type.Others, "Emplpoyee | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this employee because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "employee | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "employee | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Employee could not be deleted", false, Type.Others, "employee | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieve all employees from the employee master
        /// </summary>
        /// <param name="CompanyId">companyid of the employee list</param>
        /// <returns>list of employees</returns>
        public static List<Employee> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select I.Employee_Id,I.Title,I.First_Name,I.Last_Name,I.Date_Of_Birth,I.Gender,I.Marital_Status,
                               I.Blood_Group,isnull(I.Nationality,0)[Nationality],I.Mobile,isnull(I.Department_Id,0)[Department_Id], 
                               isnull(I.Designation_Id,0)[Designation_Id],isnull(I.[Status],0)[Status],isnull( I.Created_By,0)[Created_By],
                               I.Created_Date,isnull( I.Modified_By,0)[Modified_By],I.Modified_Date,
                               isnull(I.Company_Id,0)[Company_Id],i.Address,i.City,i.Email,i.Zip_Code,
                               dpt.Department[Department],cmp.Name[Company],cou.Name[Nationality_Name],desig.Designation[Designation_Name]
                               from TBL_EMPLOYEE_MST I
                               left join [dbo].[TBL_DEPARTMENT_MST] dpt on I.Department_Id = dpt.Department_Id
                               left join TBL_COMPANY_MST cmp on cmp.Company_Id = i.Company_Id
                               left join[dbo].[TBL_COUNTRY_MST] cou on i.Nationality = cou.Country_Id
                               left join[dbo].[TBL_DESIGNATION_MST] desig on i.Designation_Id = desig.Designation_id 
                               where cmp.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Employee> result = new List<Employee>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Employee Employee = new Employee();

                        Employee.ID = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                        Employee.Title = Convert.ToString(item["Title"]);
                        Employee.FirstName = Convert.ToString(item["First_Name"]);
                        Employee.LastName = Convert.ToString(item["Last_Name"]);
                        Employee.DOB = Convert.ToDateTime(item["Date_Of_Birth"]);
                        Employee.DobString = Convert.ToDateTime(item["Date_Of_Birth"]).ToShortDateString();
                        Employee.Gender = Convert.ToString(item["Gender"]);
                        Employee.BloodGroup = Convert.ToString(item["Blood_Group"]);
                        Employee.Nationality = Convert.ToString(item["Nationality_Name"]);
                        Employee.NationalityId = item["Nationality"] != DBNull.Value ? Convert.ToInt32(item["Nationality"]) : 0;
                        Employee.MaritalStatus = Convert.ToString(item["Marital_Status"]);
                        Employee.Mobile = Convert.ToString(item["Mobile"]);
                        Employee.DepartmentName = Convert.ToString(item["Department"]);
                        Employee.DepartmentId = item["Department_Id"] != DBNull.Value ? Convert.ToInt32(item["Department_Id"]) : 0;
                        Employee.Designation = Convert.ToString(item["Designation_Name"]);
                        Employee.DesignationId = item["Designation_Id"] != DBNull.Value ? Convert.ToInt32(item["Designation_Id"]) : 0;
                        Employee.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        Employee.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        result.Add(Employee);
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
                Application.Helper.LogException(ex, "Employee |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single employee from the list of employees
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>details of single employee</returns>
        public static Employee GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select I.Employee_Id,I.Title,I.First_Name,I.Last_Name,I.Date_Of_Birth,I.Gender,I.Marital_Status,
                               I.Blood_Group,isnull(I.Nationality,0)[Nationality],I.Mobile,
                               isnull(I.Department_Id,0)[Department_Id],isnull(I.Designation_Id,0)[Designation_Id],I.Grade,
                               i.Photo_Upload,isnull(I.Created_By,0)[Created_By],I.Created_Date,
                               isnull( I.Modified_By,0)[Modified_By],I.Modified_Date,isnull(I.Company_Id,0)[Company_Id],
                               i.Address,i.City,i.Email,i.Zip_Code,
                               dpt.Department[Department_Name],cmp.Name[Company],cou.Name[Nationality_Name], 
                               isnull(I.[Status],0)[Status],desig.Designation
                               from TBL_EMPLOYEE_MST I
                               left join [dbo].[TBL_DEPARTMENT_MST] dpt on I.Department_Id = dpt.Department_Id
                               left join TBL_COMPANY_MST cmp on cmp.Company_Id = i.Company_Id
                               left join[dbo].[TBL_COUNTRY_MST] cou on i.Nationality = cou.Country_Id
                               left join[dbo].[TBL_DESIGNATION_MST] desig on i.Designation_Id = desig.Designation_id where cmp.Company_Id=@Company_Id and [Employee_Id]=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    Employee Employee = new Employee();
                    DataRow item = dt.Rows[0];
                    Employee.ID = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                    Employee.Title = Convert.ToString(item["Title"]);
                    Employee.FirstName = Convert.ToString(item["First_Name"]);
                    Employee.LastName = Convert.ToString(item["Last_Name"]);
                    Employee.DOB = Convert.ToDateTime(item["Date_Of_Birth"]);
                    Employee.DobString = Convert.ToDateTime(item["Date_Of_Birth"]).ToShortDateString();
                    Employee.Gender = Convert.ToString(item["Gender"]);
                    Employee.BloodGroup = Convert.ToString(item["Blood_Group"]);
                    Employee.Nationality = Convert.ToString(item["Nationality_Name"]);
                    Employee.Email = Convert.ToString(item["Email"]);
                    Employee.ZipCode = Convert.ToString(item["Zip_Code"]);
                    Employee.Address = Convert.ToString(item["Address"]);
                    Employee.City = Convert.ToString(item["City"]);
                    Employee.Mobile = Convert.ToString(item["Mobile"]);
                    Employee.DepartmentId = item["Department_Id"] != DBNull.Value ? Convert.ToInt32(item["Department_Id"]) : 0;
                    Employee.MaritalStatus = Convert.ToString(item["Marital_Status"]);
                    Employee.DepartmentName = Convert.ToString(item["Department_Name"]);
                    Employee.DesignationId = item["Designation_Id"] != DBNull.Value ? Convert.ToInt32(item["Designation_Id"]) : 0;
                    Employee.Designation = Convert.ToString(item["Designation"]);
                    Employee.NationalityId = item["Nationality"] != DBNull.Value ? Convert.ToInt32(item["Nationality"]) : 0;
                    Employee.DobString = Employee.DOB.ToLongDateString();
                    Employee.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                    Employee.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                    Employee.PhotoBase64 = Convert.ToBase64String((byte[])item["Photo_Upload"]);
                    return Employee;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Employee |  GetDetails(int id, int CompanyId)");
                return null;

            }
            finally
            {
                db.Close();
            }
        }
        public static dynamic GetDetailsForManageSalary(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                dynamic Final = new ExpandoObject();
                db.Open();
                string query = @"select e.Employee_Id,e.First_Name,isnull(e.Department_Id,0)[Department_Id],isnull(e.Designation_Id,0)[Designation_Id],
                               desg.Designation[Designation],isnull(e.Company_Id,0)[Company_Id],isnull(e.Salary_Type,0)[Salary_Type],isnull(e.Monthly_Template,0)[Monthly_Template],
                               isnull(e.Hourly_Template,0)[Hourly_Template],c.Name[Company],dep.Department[Department],isnull(e.IsHourlyPaid,0)[IsHourlyPaid] 
                               from TBL_EMPLOYEE_MST e
                               left join TBL_COMPANY_MST c on c.Company_Id=e.Company_Id
                               left join TBL_DESIGNATION_MST desg on desg.Designation_id=e.Designation_Id 
                               left join TBL_DEPARTMENT_MST dep on dep.Department_Id=e.Department_Id where e.Company_Id=@Company_Id;
                               select Hourly_Rate_Id,isnull(Grade,0)[Grade] from TBL_HOURLY_WAGE_MST where Company_Id=@Company_Id and Status=1;
                               select Salary_Template_Id,isnull(Grade,0)[Grade] from [dbo].[TBL_PAYROLL_TEMPLATE_MST] where Company_Id=@Company_Id and Status=1;";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                List<Employee> result = new List<Employee>();
                if (ds.Tables[0] != null)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        Employee Employee = new Employee();

                        Employee.ID = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                        Employee.FirstName = Convert.ToString(item["First_Name"]);
                        Employee.DepartmentName = Convert.ToString(item["Department"]);
                        Employee.DepartmentId = item["Department_Id"] != DBNull.Value ? Convert.ToInt32(item["Department_Id"]) : 0;
                        Employee.Designation = Convert.ToString(item["Designation"]);
                        Employee.DesignationId = item["Designation_Id"] != DBNull.Value ? Convert.ToInt32(item["Designation_Id"]) : 0;
                        Employee.Company = Convert.ToString(item["Company"]);
                        Employee.CompanyId = item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                        Employee.SalaryType = item["Salary_Type"] != DBNull.Value ? Convert.ToInt32(item["Salary_Type"]) : 0;
                        Employee.HourlyTemplate = item["Hourly_Template"] != DBNull.Value ? Convert.ToInt32(item["Hourly_Template"]) : 0;
                        Employee.MonthlyTemplate = item["Monthly_Template"] != DBNull.Value ? Convert.ToInt32(item["Monthly_Template"]) : 0;
                        Employee.IsHourlyPaid = Convert.ToBoolean(item["IsHourlyPaid"]);
                        result.Add(Employee);
                    }
                    Final.Employees = result;
                }
                List<HourlyTemplate> hourlyTemplates = new List<Payroll.HourlyTemplate>();
                if (ds.Tables[1] != null)
                {
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        HourlyTemplate hourlyTemp = new Payroll.HourlyTemplate();
                        hourlyTemp.ID = Convert.ToInt32(item["Hourly_Rate_Id"]);
                        hourlyTemp.Title = Convert.ToString(item["Grade"]);
                        hourlyTemplates.Add(hourlyTemp);
                    }
                    Final.HourlyTemplates = hourlyTemplates;
                }
                List<PayRollTemplate> payrollTemplate = new List<Payroll.PayRollTemplate>();
                if (ds.Tables[2] != null)
                {
                    foreach (DataRow item in ds.Tables[2].Rows)
                    {
                        PayRollTemplate pay = new PayRollTemplate();
                        pay.ID = Convert.ToInt32(item["Salary_Template_Id"]);
                        pay.Grade = Convert.ToString(item["Grade"]);
                        payrollTemplate.Add(pay);

                    }
                    Final.PayrollTemplate = payrollTemplate;
                }

                return Final;


            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Employee |   GetDetailsForManageSalary(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static OutputMessage ManageSalary(List<Employee> Employees)
        {
            DBManager db = new DBManager();
            try
            {


                db.Open();
                foreach (Employee emp in Employees)
                {
                    if (emp.IsHourlyPaid)
                    {
                        emp.MonthlyTemplate = 0;
                    }
                    else
                    {
                        emp.HourlyTemplate = 0;
                    }
                    string query = @"update tbl_employee_mst set ishourlypaid=@ishourlypaid, Monthly_Template= @Monthly_Template,Hourly_Template=@Hourly_Template  where Employee_Id=@Id";
                    db.CreateParameters(4);
                    db.AddParameters(0, "@Hourly_Template", emp.HourlyTemplate);
                    db.AddParameters(1, "@Monthly_Template", emp.MonthlyTemplate);
                    db.AddParameters(2, "@Id", emp.ID);
                    db.AddParameters(3, "@ishourlypaid", emp.IsHourlyPaid);
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                }


                return new OutputMessage("Salary Updated successfully", true, Type.NoError, "Employee | ManageSalary(List<Employee> Employees)", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new OutputMessage("Something went wrong. Entry could not be updated", false, Type.Others, "Employee | ManageSalary(List<Employee> Employees)", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }
        }

        public static List<Employee> GetEmployeeDetails(int CompanyID) {
            List<Employee> Employess = new List<Employee>();
            string query = @"select Employee_Id,First_Name,Photo_Path from TBL_EMPLOYEE_MST where Company_Id=@Company_Id and Status<>0";
            DBManager db = new DBManager();
            db.CreateParameters(1);
            db.AddParameters(0, "@Company_Id", CompanyID);
            DataTable dt = new DataTable();
            try
            {
                db.Open();
                dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Employee emp = new Employee();
                        emp.FirstName = Convert.ToString(row["First_Name"]);
                        emp.ID = row["Employee_Id"] != null ? Convert.ToInt32(row["Employee_Id"]) : 0;
                        emp.PhotoPath = Convert.ToString(row["Photo_Path"]);
                        Employess.Add(emp);
                    }
                }
                return Employess;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Employee | GetEmployeeDetails");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
    }
}
