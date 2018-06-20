using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
using System.Data;

namespace Entities.Payroll
{
   public class AdvanceSalary
    {
        #region Properties
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public  DateTime MonthAndYear{ get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Employee { get; set; }
        public string MonthString { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion Properties

        #region Functions
        /// <summary>
        /// Function for saving the details
        /// </summary>
        /// <returns>Return success alert when details inserted successfull otherwise returns an error alert</returns>
        public OutputMessage Save()
        {
           
         if (this.EmployeeId==0)
            {
                return new OutputMessage("Employee must not be empty", false, Type.InsufficientPrivilege, "AdvanceSalary | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        string query = @"insert into [TBL_ADVANCE_SALARY_MST] ([Employee_Id],[Amount],[Month_And_Year],[Status],[Company_Id],[Created_By],[Created_Date])
                                      values(@Employee_Id,@Amount,@Month_And_Year,@Status,@Company_Id,@Created_By,GETUTCDATE())";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Employee_Id", this.EmployeeId);
                        db.AddParameters(1, "@Amount", this.Amount);
                        db.AddParameters(2, "@Month_And_Year", this.MonthAndYear);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        db.AddParameters(5, "@Created_By", this.CreatedBy);
                        db.Open();
                        int NoOfRowsAffected = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query));
                        if(NoOfRowsAffected >= 1)
                        {
                            return new OutputMessage("Advance Salary successfully saved", true, Type.InsufficientPrivilege, "AdvanceSalary | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Couldnot save the details", false, Type.InsufficientPrivilege, "AdvanceSalary | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch(Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Couldnot save the details", false, Type.InsufficientPrivilege, "AdvanceSalary | Save", System.Net.HttpStatusCode.InternalServerError);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// Update details of salary managements
        /// </summary>
        /// <returns>Return success alert when details updated successfull otherwise return an error alert</returns>
        public OutputMessage Update()
        {

            if (this.EmployeeId == 0)
            {
                return new OutputMessage("Employee must not be empty", false, Type.InsufficientPrivilege, "AdvanceSalary | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        string query = @"update [TBL_ADVANCE_SALARY_MST] set [Employee_Id]=@Employee_Id,[Amount]=@Amount,[Month_And_Year]=@Month_And_Year,
                                       [Status]=@Status,[Modified_By]=@Modified_By,[Modified_Date]=GETUTCDATE() where Advance_Salary_Id=@id";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Employee_Id", this.EmployeeId);
                        db.AddParameters(1, "@Amount", this.Amount);
                        db.AddParameters(2, "@Month_And_Year", this.MonthAndYear);
                        db.AddParameters(3, "@Status", this.Status);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        db.AddParameters(5, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(6, "@id", this.Id);
                        db.Open();
                        int NoOfRowsAffected = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query));
                        if (NoOfRowsAffected >= 1)
                        {
                            return new OutputMessage("Advance Salary successfully Updated", true, Type.InsufficientPrivilege, "AdvanceSalary | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.Couldnot Update the details", false, Type.InsufficientPrivilege, "AdvanceSalary | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.Couldnot Update the details", false, Type.InsufficientPrivilege, "AdvanceSalary | Update", System.Net.HttpStatusCode.InternalServerError);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        /// <summary>
        /// List of manage salary details
        /// </summary>
        /// <param name="CompanyId">Filter data according to given companyId</param>
        /// <returns>list of manage salary details</returns>
        public static List<AdvanceSalary> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select a.Advance_Salary_Id,a.Amount,a.Employee_Id,a.Month_And_Year,a.Status,a.Company_Id,e.First_Name[Employee] from [TBL_ADVANCE_SALARY_MST] a
                              left join TBL_EMPLOYEE_MST e on e.Employee_Id=a.Employee_Id where a.Company_Id=@company_id order by a.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<AdvanceSalary> result = new List<AdvanceSalary>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        AdvanceSalary salary = new AdvanceSalary();
                        salary.Id = item["Advance_Salary_Id"] != DBNull.Value ? Convert.ToInt32(item["Advance_Salary_Id"]) : 0;
                        salary.EmployeeId = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                        salary.Employee = Convert.ToString(item["Employee"]);
                        salary.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        salary.MonthAndYear =Convert.ToDateTime(item["Month_And_Year"]);
                        salary.MonthString = item["Month_And_Year"] != DBNull.Value ? Convert.ToDateTime(item["Month_And_Year"]).ToString("MMM/yyyy") : string.Empty;
                        salary.Amount = item["Amount"] != DBNull.Value ? Convert.ToDecimal(item["Amount"]) : 0;
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
                Application.Helper.LogException(ex, "AdvanceSalary | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Details of a single entry
        /// </summary>
        /// <param name="Id">Id of that particular entry</param>
        /// <param name="CompanyId">Company Id of that particular entry</param>
        /// <returns></returns>
        public static AdvanceSalary GetDetails(int Id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select a.Advance_Salary_Id,a.Amount,a.Employee_Id,a.Month_And_Year,a.Status,a.Company_Id,e.First_Name[Employee] from [TBL_ADVANCE_SALARY_MST] a
                              left join TBL_EMPLOYEE_MST e on e.Employee_Id=a.Employee_Id where a.Company_Id=@company_id and Advance_Salary_Id=@id order by a.Created_Date desc";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                        AdvanceSalary salary = new AdvanceSalary();
                        DataRow item = dt.Rows[0];
                        salary.Id = item["Advance_Salary_Id"] != DBNull.Value ? Convert.ToInt32(item["Advance_Salary_Id"]) : 0;
                        salary.EmployeeId = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                        salary.Employee = Convert.ToString(item["Employee"]);
                        salary.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        salary.MonthAndYear = Convert.ToDateTime(item["Month_And_Year"]);
                        salary.MonthString = item["Month_And_Year"] != DBNull.Value ? Convert.ToDateTime(item["Month_And_Year"]).ToString("MMM/yyyy") : string.Empty;
                        salary.Amount = item["Amount"] != DBNull.Value ? Convert.ToDecimal(item["Amount"]) : 0;
                        return salary;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AdvanceSalary | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for removing the details
        /// </summary>
        /// <returns>Return a success alert when details deleted successfull otherwise returns an error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.Id != 0)
                    {
                        db.Open();
                        string query = @"delete from [TBL_ADVANCE_SALARY_MST] where Advance_Salary_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.Id);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Advance salary Deleted Successfully", true, Type.Others, "Advance salary  | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Please Select a Advance salary . Could not Delete Advance salary ", false, Type.Others, "Advance salary  | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Something went wrong.could not Delete Advance salary ", false, Type.Others, "Advance salary  | Delete", System.Net.HttpStatusCode.InternalServerError);
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

                            return new OutputMessage("You cannot delete this brand because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Advance salary  | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Advance salary  | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.could not Delete brand", false, Type.Others, "Advance salary | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }
        #endregion Functions
    }
}
