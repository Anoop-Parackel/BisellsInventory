using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
  public class PayRollTemplate
    {
        #region Properties
        public int ID { get; set; }
        public string Grade { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal HouseRentAllowance { get; set; }
        public decimal MedicalAllowance { get; set; }
        public decimal TravellingAllowance { get; set; }
        public decimal DearnessAllowance { get; set; }
        public decimal SecurityDeposit { get; set; }
        public decimal ProvidentFund { get; set; }
        public decimal TaxDeduction { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetSalary { get; set; }
        public decimal Incentives { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Employee { get; set; }
        public int ModifiedBy { get; set; }
        public string EmployeeLastName { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public int TotalLeave { get; set; }
        public int TotalHolidays { get; set; }
        public DateTime Date { get; set; }
        public string Company { get; set; }
        public bool PaymentStatus { get; set; }
        public decimal LeaveDeduction { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalAttendance { get; set; }
        #endregion Properties

        /// <summary>
        /// Save Payroll template details
        /// </summary>
        /// <returns>returns success alert when details saved successfully or return errr alert</returns>
        public OutputMessage Save()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PayRollTemplate, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "PayRollTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Grade))
            {
                return new OutputMessage("PayRoll template name must not be empty", false, Type.RequiredFields, "PayRollTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into TBL_PAYROLL_TEMPLATE_MST (Grade,Basic_Salary,Overtime_Rate,House_Rent_Allowance,Medical_Allowance,Travelling_Allowance,Dearness_Allowance,Security_Deposit,      
                                     Provident_Fund,Tax_Deduction,Gross_Salary,Total_Allowance,Total_Deduction,Net_Salary,Incentives,Status,Company_Id,     
                                     Created_By,Created_Date)values(@Grade,@Basic_Salary,@Overtime_Rate,@House_Rent_Allowance,@Medical_Allowance,@Travelling_Allowance,@Dearness_Allowance,@Security_Deposit,      
                                     @Provident_Fund,@Tax_Deduction,@Gross_Salary,@Total_Allowance,@Total_Deduction,@Net_Salary,@Incentives,@Status,@Company_Id,     
                                     @Created_By,GETUTCDATE())";
                        db.CreateParameters(18);
                        db.AddParameters(0, "@Grade", this.Grade);
                        db.AddParameters(1, "@Basic_Salary", this.BasicSalary);
                        db.AddParameters(2, "@Overtime_Rate", this.OvertimeRate);
                        db.AddParameters(3, "@House_Rent_Allowance", this.HouseRentAllowance);
                        db.AddParameters(4, "@Medical_Allowance", this.MedicalAllowance);
                        db.AddParameters(5, "@Travelling_Allowance", this.TravellingAllowance);
                        db.AddParameters(6, "@Dearness_Allowance", this.DearnessAllowance);
                        db.AddParameters(7, "@Security_Deposit", this.SecurityDeposit);
                        db.AddParameters(8, "@Provident_Fund", this.ProvidentFund);
                        db.AddParameters(9, "@Tax_Deduction", this.TaxDeduction);
                        db.AddParameters(10, "@Gross_Salary", this.GrossSalary);
                        db.AddParameters(11, "@Total_Allowance", this.TotalAllowance);
                        db.AddParameters(12, "@Total_Deduction", this.TotalDeduction);
                        db.AddParameters(13, "@Net_Salary", this.NetSalary);
                        db.AddParameters(14, "@Incentives", this.Incentives);
                        db.AddParameters(15, "@Status", this.Status);
                        db.AddParameters(16, "@Company_Id", this.CompanyId);
                        db.AddParameters(17, "@Created_By", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Payroll template saved successfully", true, Type.NoError, "PayRollTemplate | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Payroll template could not be saved", false, Type.Others, "PayRollTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }

                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Payroll template could not be saved", false, Type.Others, "PayRollTemplate | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }

                    finally
                    {
                       db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// update each payroll template details
        /// </summary>
        /// <returns>returns success alert when details updated successfully or returns error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PayRollTemplate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError);


            }
            else if (string.IsNullOrWhiteSpace(this.Grade))
            {

                return new OutputMessage("Payroll template name must not be empty", false, Type.RequiredFields, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"update TBL_PAYROLL_TEMPLATE_MST set Grade=@Grade,Basic_Salary=@Basic_Salary,Overtime_Rate=@Overtime_Rate,
                                   House_Rent_Allowance=@House_Rent_Allowance,Medical_Allowance=@Medical_Allowance,Travelling_Allowance=@Travelling_Allowance,
                                   Dearness_Allowance=@Dearness_Allowance,Security_Deposit=@Security_Deposit,Provident_Fund=@Provident_Fund,Tax_Deduction=@Tax_Deduction,
                                   Gross_Salary=@Gross_Salary,Total_Allowance=@Total_Allowance,Total_Deduction=@Total_Deduction,Net_Salary=@Net_Salary,Incentives=@Incentives,
                                   Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Salary_Template_Id=@id";
                        db.CreateParameters(18);
                        db.AddParameters(0, "@Grade", this.Grade);
                        db.AddParameters(1, "@Basic_Salary", this.BasicSalary);
                        db.AddParameters(2, "@Overtime_Rate", this.OvertimeRate);
                        db.AddParameters(3, "@House_Rent_Allowance", this.HouseRentAllowance);
                        db.AddParameters(4, "@Medical_Allowance", this.MedicalAllowance);
                        db.AddParameters(5, "@Travelling_Allowance", this.TravellingAllowance);
                        db.AddParameters(6, "@Dearness_Allowance", this.DearnessAllowance);
                        db.AddParameters(7, "@Security_Deposit", this.SecurityDeposit);
                        db.AddParameters(8, "@Provident_Fund", this.ProvidentFund);
                        db.AddParameters(9, "@Tax_Deduction", this.TaxDeduction);
                        db.AddParameters(10, "@Gross_Salary", this.GrossSalary);
                        db.AddParameters(11, "@Total_Allowance", this.TotalAllowance);
                        db.AddParameters(12, "@Total_Deduction", this.TotalDeduction);
                        db.AddParameters(13, "@Net_Salary", this.NetSalary);
                        db.AddParameters(14, "@Incentives", this.Incentives);
                        db.AddParameters(15, "@Status", this.Status);
                        db.AddParameters(16, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(17, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Payroll template updated successfully", true, Type.NoError, "PayRollTemplate | Update", System.Net.HttpStatusCode.OK);


                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Payroll template could not be updated", false, Type.Others, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Payroll template could not update", false, Type.Others, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Delete each payroll template details
        /// For delete a payroll template entry first you have to set the particular id
        /// </summary>
        /// <returns>returns success alert when details deleted successfully or returns an error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PayRollTemplate, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "PayRollTemplate | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_PAYROLL_TEMPLATE_MST where Salary_Template_Id=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Payroll template deleted successfully", true, Type.NoError, "PayRollTemplate | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Payroll Template could not be deleted", false, Type.Others, "PayRollTemplate | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {

                        return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "PayRollTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this payrolltemplate because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "payrolltemplate| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "payrolltemplate| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Payroll template could not be deleted", false, Type.Others, "payrolltemplate| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }
        /// <summary>
        /// Retrieve all payroll template details from payroll master
        /// </summary>
        /// <param name="CompanyId">company of that particular entry</param>
        /// <returns>list of payroll template details</returns>

        public static List<PayRollTemplate> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select p.Salary_Template_Id,p.Grade,isnull(p.Basic_Salary,0)[Basic_Salary],isnull(p.Overtime_Rate,0)[Overtime_Rate],
                               isnull(p.House_Rent_Allowance,0)[House_Rent_Allowance],isnull(p.Medical_Allowance,0)[Medical_Allowance],isnull(p.Gross_Salary,0)[Gross_Salary],
                               isnull(p.Travelling_Allowance,0)[Travelling_Allowance],isnull(p.Dearness_Allowance,0)[Dearness_Allowance],isnull(p.Net_Salary,0)[Net_Salary],
                               isnull(p.Security_Deposit,0)[Security_Deposit],isnull(p.Provident_Fund,0)[Provident_Fund],isnull(p.Tax_Deduction,0)[Tax_Deduction],
                               isnull(p.Total_Allowance,0)[Total_Allowance],isnull(p.Total_Deduction,0)[Total_Deduction],isnull(p.Incentives,0)[Incentives],
                               isnull(p.Status,0)[Status],isnull(p.Company_Id,0)[Company_Id],c.Name[Company] 
							   from TBL_PAYROLL_TEMPLATE_MST p
                               left join TBL_COMPANY_MST c on c.Company_Id=p.Company_Id
                               where p.Company_Id=@Company_Id order by p.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                List<PayRollTemplate> payroll = new List<PayRollTemplate>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        PayRollTemplate pay = new PayRollTemplate();
                        pay.ID = item["Salary_Template_Id"] != DBNull.Value ? Convert.ToInt32(item["Salary_Template_Id"]) : 0;
                        pay.Grade = Convert.ToString(item["Grade"]);
                        pay.BasicSalary= item["Basic_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Basic_Salary"]) : 0;
                        pay.OvertimeRate= item["Overtime_Rate"] != DBNull.Value ? Convert.ToDecimal(item["Overtime_Rate"]) : 0;
                        pay.HouseRentAllowance = item["House_Rent_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["House_Rent_Allowance"]) : 0;
                        pay.MedicalAllowance= item["Medical_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Medical_Allowance"]) : 0;
                        pay.TravellingAllowance= item["Travelling_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Travelling_Allowance"]) : 0;
                        pay.DearnessAllowance= item["Dearness_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Dearness_Allowance"]) : 0;
                        pay.SecurityDeposit= item["Security_Deposit"] != DBNull.Value ? Convert.ToDecimal(item["Security_Deposit"]) : 0;
                        pay.ProvidentFund= item["Provident_Fund"] != DBNull.Value ? Convert.ToDecimal(item["Provident_Fund"]) : 0;
                        pay.TaxDeduction= item["Tax_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Deduction"]) : 0;
                        pay.GrossSalary= item["Gross_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Gross_Salary"]) : 0;
                        pay.TotalAllowance= item["Total_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Total_Allowance"]) : 0;
                        pay.TotalDeduction= item["Total_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Total_Deduction"]) : 0;
                        pay.NetSalary= item["Net_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Net_Salary"]) : 0;
                        pay.Incentives= item["Incentives"] != DBNull.Value ? Convert.ToDecimal(item["Incentives"]) : 0;
                        pay.Status= item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        pay.CompanyId= item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                        pay.Company = Convert.ToString(item["Company"]);
                        payroll.Add(pay);
                    }
                    return payroll;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "payrolltemplate |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single template details from list of payroll template details
        /// </summary>
        /// <param name="id">Id of that particular entry</param>
        /// <param name="CompanyId">comapanyid of that particular entry </param>
        /// <returns>single entry of payroll template details</returns>
        public static PayRollTemplate GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 p.Salary_Template_Id,p.Grade,isnull(p.Basic_Salary,0)[Basic_Salary],isnull(p.Overtime_Rate,0)[Overtime_Rate],
                               isnull(p.House_Rent_Allowance,0)[House_Rent_Allowance],isnull(p.Medical_Allowance,0)[Medical_Allowance],isnull(p.Travelling_Allowance,0)[Travelling_Allowance],
                               isnull(p.Dearness_Allowance,0)[Dearness_Allowance],isnull(p.Security_Deposit,0)[Security_Deposit],isnull(p.Provident_Fund,0)[Provident_Fund],
                               isnull(p.Tax_Deduction,0)[Tax_Deduction],isnull(p.Gross_Salary,0)[Gross_Salary],isnull(p.Total_Allowance,0)[Total_Allowance],
                               isnull(p.Total_Deduction,0)[Total_Deduction],isnull(p.Net_Salary,0)[Net_Salary],isnull(p.Incentives,0)[Incentives],isnull(p.Status,0)[Status],
                               isnull(p.Company_Id,0)[Company_Id],isnull(c.Name,0)[Company] from TBL_PAYROLL_TEMPLATE_MST p
                               left join TBL_COMPANY_MST c on c.Company_Id=p.Company_Id
                               where p.Company_Id=@Company_Id and p.Salary_Template_Id=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    PayRollTemplate pay = new PayRollTemplate();
                    DataRow item = dt.Rows[0];
                    pay.ID = item["Salary_Template_Id"] != DBNull.Value ? Convert.ToInt32(item["Salary_Template_Id"]) : 0;
                    pay.Grade = Convert.ToString(item["Grade"]);
                    pay.BasicSalary = item["Basic_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Basic_Salary"]) : 0;
                    pay.OvertimeRate = item["Overtime_Rate"] != DBNull.Value ? Convert.ToDecimal(item["Overtime_Rate"]) : 0;
                    pay.HouseRentAllowance = item["House_Rent_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["House_Rent_Allowance"]) : 0;
                    pay.MedicalAllowance = item["Medical_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Medical_Allowance"]) : 0;
                    pay.TravellingAllowance = item["Travelling_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Travelling_Allowance"]) : 0;
                    pay.DearnessAllowance = item["Dearness_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Dearness_Allowance"]) : 0;
                    pay.SecurityDeposit = item["Security_Deposit"] != DBNull.Value ? Convert.ToDecimal(item["Security_Deposit"]) : 0;
                    pay.ProvidentFund = item["Provident_Fund"] != DBNull.Value ? Convert.ToDecimal(item["Provident_Fund"]) : 0;
                    pay.TaxDeduction = item["Tax_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Deduction"]) : 0;
                    pay.GrossSalary = item["Gross_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Gross_Salary"]) : 0;
                    pay.TotalAllowance = item["Total_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Total_Allowance"]) : 0;
                    pay.TotalDeduction = item["Total_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Total_Deduction"]) : 0;
                    pay.NetSalary = item["Net_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Net_Salary"]) : 0;
                    pay.Incentives = item["Incentives"] != DBNull.Value ? Convert.ToDecimal(item["Incentives"]) : 0;
                    pay.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                    pay.CompanyId = item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                    pay.Company = Convert.ToString(item["Company"]);
                    return pay;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "payrolltemplate |  GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<PayRollTemplate> GetDetailsForPaySlip(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select p.Grade,isnull(p.Salary_Template_Id,0)[Salary_Template_Id],isnull(p.Basic_Salary,0)[Basic_Salary],
                               isnull(p.House_Rent_Allowance,0)[House_Rent_Allowance],isnull(p.Medical_Allowance,0)[Medical_Allowance],
                               isnull(p.Travelling_Allowance,0)[Travelling_Allowance],isnull(p.Dearness_Allowance,0)[Dearness_Allowance],
                               isnull(p.Provident_Fund,0)[Provident_Fund],isnull(p.Tax_Deduction,0)[Tax_Deduction],isnull(p.Net_Salary,0)[Net_Salary],
                               e.First_Name[Employee] 
                               from TBL_PAYROLL_TEMPLATE_MST p
                               inner join TBL_EMPLOYEE_MST e on e.Monthly_Template=p.Salary_Template_Id where p.Company_Id=@Company_Id
                               order by p.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<PayRollTemplate> payroll = new List<PayRollTemplate>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        PayRollTemplate pay = new PayRollTemplate();
                        pay.ID = item["Salary_Template_Id"] != DBNull.Value ? Convert.ToInt32(item["Salary_Template_Id"]) : 0;
                        pay.Grade = Convert.ToString(item["Grade"]);
                        pay.Employee = Convert.ToString(item["Employee"]);
                        pay.BasicSalary = item["Basic_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Basic_Salary"]) : 0;
                        pay.HouseRentAllowance = item["House_Rent_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["House_Rent_Allowance"]) : 0;
                        pay.MedicalAllowance = item["Medical_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Medical_Allowance"]) : 0;
                        pay.TravellingAllowance = item["Travelling_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Travelling_Allowance"]) : 0;
                        pay.DearnessAllowance = item["Dearness_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Dearness_Allowance"]) : 0;
                        pay.ProvidentFund = item["Provident_Fund"] != DBNull.Value ? Convert.ToDecimal(item["Provident_Fund"]) : 0;
                        pay.TaxDeduction = item["Tax_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Deduction"]) : 0;
                        pay.NetSalary = item["Net_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Net_Salary"]) : 0;
                        payroll.Add(pay);
                    }
                    return payroll;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "payrollTemplate | GetDetailsForPaySlip(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<PayRollTemplate> GetDetailsForPayment(int CompanyId,DateTime From,DateTime To)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"declare @Total_Holiday int
                                  select @Total_Holiday=count(Date) from TBL_HOLIDAYS where date>=@From and Date<=@To
                                  select isnull(I.Employee_Id,0)[Employee_Id],I.Title,I.First_Name,I.Last_Name,isnull(I.[Status],0)[Status],
                                  isnull(I.Company_Id,0)[Company_Id],
                                  pay.Grade[Monthly_Template],hou.Grade[Hourly_Template],hou.Hourly_Rate_Id,isnull(pay.Salary_Template_Id,0)[Salary_Template_Id],
                                  isnull( pay.Basic_Salary,0)[Monthly_BP],isnull(pay.Dearness_Allowance,0)[Dearness_Allowance],isnull(pay.Gross_Salary,0)[Gross_Salary],
								  isnull(pay.House_Rent_Allowance,0)[House_Rent_Allowance],isnull(pay.Incentives,0)[Incentives],
                                  isnull(pay.Medical_Allowance,0)[Medical_Allowance],isnull(pay.Net_Salary,0)[Net_Salary],
								  isnull(pay.Overtime_Rate,0)[Overtime_Rate],isnull(pay.Provident_Fund,0)[Provident_Fund],
								  isnull(pay.Security_Deposit,0)[Security_Deposit],isnull(pay.Tax_Deduction,0)[Tax_Deduction],
                                  isnull(pay.Total_Allowance,0)[Total_Allowance],isnull(pay.Total_Deduction,0)[Total_Deduction],
								  isnull(pay.Travelling_Allowance,0)[Travelling_Allowance],isnull(hou.Rate,0)[Hourly_Rate],cmp.Name[Company],
                                  (select count(*) from TBL_ATTENDANCE where Attendance_Status=2 and date>=@From and Date<=@To and Employee_Id=i.Employee_Id) as Total_Leave,
                                  (@Total_Holiday) as Total_Holidays
                                  from TBL_EMPLOYEE_MST I
                                  inner join TBL_COMPANY_MST cmp on cmp.Company_Id = i.Company_Id
                                  inner join TBL_PAYROLL_TEMPLATE_MST pay on pay.Salary_Template_Id=i.Monthly_Template
                                  left join TBL_HOURLY_WAGE_MST hou on hou.Hourly_Rate_Id=i.Hourly_Template 
                                  where i.Company_Id=@Company_Id and i.Status<>0
                                  order by i.Created_Date desc";

                    db.CreateParameters(3);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    db.AddParameters(1, "@From", From);
                    db.AddParameters(2, "@To", To);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<PayRollTemplate> payroll = new List<PayRollTemplate>();
                    if(dt!=null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            PayRollTemplate pay = new PayRollTemplate();
                            pay.ID = item["Salary_Template_Id"]!=DBNull.Value?Convert.ToInt32(item["Salary_Template_Id"]):0;
                            pay.Employee = Convert.ToString(item["First_Name"]);
                            pay.EmployeeLastName = Convert.ToString(item["Last_Name"]);
                            pay.EmployeeId = item["Employee_Id"]!=DBNull.Value? Convert.ToInt32(item["Employee_Id"]):0;
                            pay.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                            pay.Company = Convert.ToString(item["Company"]);
                            pay.Grade = Convert.ToString(item["Monthly_Template"]);
                            pay.BasicSalary = item["Monthly_BP"] != DBNull.Value ? Convert.ToDecimal(item["Monthly_BP"]):0;
                            pay.Incentives = item["Incentives"] != DBNull.Value ? Convert.ToDecimal(item["Incentives"]):0;
                            pay.DearnessAllowance = item["Dearness_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Dearness_Allowance"]):0;
                            pay.GrossSalary = item["Gross_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Gross_Salary"]):0;
                            pay.HouseRentAllowance = item["House_Rent_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["House_Rent_Allowance"]):0;
                            pay.Incentives = item["Incentives"] != DBNull.Value ? Convert.ToDecimal(item["Incentives"]) : 0;
                            pay.TotalAllowance = item["Total_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Total_Allowance"]) : 0;
                            pay.TotalDeduction = item["Total_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Total_Deduction"]) : 0;
                            pay.TravellingAllowance = item["Travelling_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Travelling_Allowance"]) : 0;
                            pay.GrossSalary = item["Gross_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Gross_Salary"]) : 0;
                            pay.NetSalary = item["Net_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Net_Salary"]) : 0;
                            pay.MedicalAllowance = item["Medical_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Medical_Allowance"]) : 0;
                            pay.OvertimeRate = item["Overtime_Rate"] != DBNull.Value ? Convert.ToDecimal(item["Overtime_Rate"]) : 0;
                            pay.ProvidentFund = item["Provident_Fund"] != DBNull.Value ? Convert.ToDecimal(item["Provident_Fund"]) : 0;
                            pay.SecurityDeposit = item["Security_Deposit"] != DBNull.Value ? Convert.ToDecimal(item["Security_Deposit"]) : 0;
                            pay.TaxDeduction = item["Tax_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Deduction"]) : 0;
                            pay.TotalLeave =item["Total_Leave"]!=DBNull.Value? Convert.ToInt32(item["Total_Leave"]):0;
                            pay.TotalHolidays =item["Total_Holidays"]!=DBNull.Value? Convert.ToInt32(item["Total_Holidays"]):0;
                            payroll.Add(pay);
                         }
                        return payroll;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "payrolltemplate |  GetDetailsForPayment(int CompanyId,DateTime From,DateTime To)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static List<PayRollTemplate> GetDetailsFromPayslip(int CompanyId,DateTime From,DateTime To)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"select p.PaySlip_Id,isnull(p.Employee_Id,0)[Employee_Id],isnull(p.Basic_Pay,0)[Basic_Pay],isnull(p.Dearness_Allowance,0)[Dearness_Allowance],
                                  isnull(p.Gross,0)[Gross],isnull(p.House_Rent_Allowance,0)[House_Rent_Allowance],isnull(p.Incentives,0)[Incentives],
                                  isnull(p.Leave_Deduction,0)[Leave_Deduction],isnull(p.Medical_Allowance,0)[Medical_Allowance],isnull(p.Net_Salary,0)[Net_Salary],
                                  isnull(p.No_Of_Holidays,0)[No_Of_Holidays],isnull(p.No_Of_Leave,0)[No_Of_Leave],isnull(p.No_Of_Working_Days,0)[No_Of_Working_Days],
                                  isnull(p.Provident_Fund,0)[Provident_Fund],p.Salary_Template,isnull(p.Security_Deposit,0)[Security_Deposit],p.Status,
                                  isnull(p.Tax_Deduction,0)[Tax_Deduction],isnull(p.Total_Allowance,0)[Total_Allowance],isnull(p.Total_Attendance,0)[Total_Attendance],
                                  isnull(p.Travelling_Allowance,0)[Travelling_Allowance],e.First_Name[Employee],e.Last_Name[Emp_LNAme],isnull(p.Total_Deduction,0)[Total_Deduction]
                                  from TBL_PAYSLIP p
                                  left join TBL_EMPLOYEE_MST e on e.Employee_Id=p.Employee_Id
                                  where e.Status<>0  and p.Date>=@From and p.Date<=@To";
                    db.CreateParameters(3);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    db.AddParameters(1, "@From", From);
                    db.AddParameters(2, "@To", To);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<PayRollTemplate> payroll = new List<PayRollTemplate>();
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            PayRollTemplate pay = new PayRollTemplate();
                            pay.ID = item["PaySlip_Id"] != DBNull.Value ? Convert.ToInt32(item["PaySlip_Id"]) : 0;
                            pay.Employee = Convert.ToString(item["Employee"]);
                            pay.EmployeeLastName = Convert.ToString(item["Emp_LNAme"]);
                            pay.EmployeeId = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                            pay.PaymentStatus = Convert.ToBoolean(item["Status"]);
                            pay.Grade = Convert.ToString(item["Salary_Template"]);
                            pay.BasicSalary = item["Basic_Pay"] != DBNull.Value ? Convert.ToDecimal(item["Basic_Pay"]) : 0;
                            pay.Incentives = item["Incentives"] != DBNull.Value ? Convert.ToDecimal(item["Incentives"]) : 0;
                            pay.DearnessAllowance = item["Dearness_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Dearness_Allowance"]) : 0;
                            pay.GrossSalary = item["Gross"] != DBNull.Value ? Convert.ToDecimal(item["Gross"]) : 0;
                            pay.HouseRentAllowance = item["House_Rent_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["House_Rent_Allowance"]) : 0;
                            pay.TotalAllowance = item["Total_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Total_Allowance"]) : 0;
                            pay.TotalDeduction = item["Total_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Total_Deduction"]) : 0;
                            pay.TravellingAllowance = item["Travelling_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Travelling_Allowance"]) : 0;
                            pay.GrossSalary = item["Gross"] != DBNull.Value ? Convert.ToDecimal(item["Gross"]) : 0;
                            pay.NetSalary = item["Net_Salary"] != DBNull.Value ? Convert.ToDecimal(item["Net_Salary"]) : 0;
                            pay.MedicalAllowance = item["Medical_Allowance"] != DBNull.Value ? Convert.ToDecimal(item["Medical_Allowance"]) : 0;
                            pay.ProvidentFund = item["Provident_Fund"] != DBNull.Value ? Convert.ToDecimal(item["Provident_Fund"]) : 0;
                            pay.SecurityDeposit = item["Security_Deposit"] != DBNull.Value ? Convert.ToDecimal(item["Security_Deposit"]) : 0;
                            pay.TaxDeduction = item["Tax_Deduction"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Deduction"]) : 0;
                            pay.TotalLeave =item["No_Of_Leave"]!=DBNull.Value? Convert.ToInt32(item["No_Of_Leave"]):0;
                            pay.TotalHolidays =item["No_Of_Holidays"]!=DBNull.Value? Convert.ToInt32(item["No_Of_Holidays"]):0;
                            pay.TotalWorkingDays =item["No_Of_Working_Days"]!=DBNull.Value? Convert.ToInt32(item["No_Of_Working_Days"]):0;
                            pay.TotalAttendance =item["Total_Attendance"]!=DBNull.Value? Convert.ToInt32(item["Total_Attendance"]):0;
                            pay.LeaveDeduction =item["Leave_Deduction"]!=DBNull.Value? Convert.ToDecimal(item["Leave_Deduction"]):0;
                            payroll.Add(pay);
                        }
                        return payroll;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "payrolltemplate |  GetDetailsFromPayslip(int CompanyId,DateTime From,DateTime To)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }


        public static OutputMessage SavePaySlip(List<PayRollTemplate> pay)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    foreach (PayRollTemplate p in pay)
                    {
                        string query = @"insert into TBL_PAYSLIP (Employee_Id,Salary_Template,Basic_Pay,House_Rent_Allowance,Medical_Allowance,Travelling_Allowance,Dearness_Allowance,
                                      Security_Deposit,Provident_Fund,Tax_Deduction,Total_Allowance,Total_Deduction,Incentives,Gross,Leave_Deduction,Net_Salary,No_Of_Leave,
                                      No_Of_Working_Days,No_Of_Holidays,Total_Attendance,[Status],Created_By,Created_Date,Company_Id,Date)

                                      values( @Employee_Id,@Salary_Template,@Basic_Pay,@House_Rent_Allowance,@Medical_Allowance,@Travelling_Allowance,@Dearness_Allowance,
                                      @Security_Deposit,@Provident_Fund,@Tax_Deduction,@Total_Allowance,@Total_Deduction,@Incentives,@Gross,@Leave_Deduction,@Net_Salary,@No_Of_Leave,
                                      @No_Of_Working_Days,@No_Of_Holidays,@Total_Attendance,@Status,@Created_By,GETUTCDATE(),@Company_Id,@Date)";
                        db.CreateParameters(24);
                        db.AddParameters(0, "@Employee_Id", p.EmployeeId);
                        db.AddParameters(1, "@Salary_Template", p.Grade);
                        db.AddParameters(2, "@Basic_Pay", p.BasicSalary);
                        db.AddParameters(3, "@House_Rent_Allowance", p.HouseRentAllowance);
                        db.AddParameters(4, "@Medical_Allowance", p.MedicalAllowance);
                        db.AddParameters(5, "@Travelling_Allowance", p.TravellingAllowance);
                        db.AddParameters(6, "@Dearness_Allowance", p.DearnessAllowance);
                        db.AddParameters(7, "@Security_Deposit", p.SecurityDeposit);
                        db.AddParameters(8, "@Provident_Fund", p.ProvidentFund);
                        db.AddParameters(9, "@Tax_Deduction", p.TaxDeduction);
                        db.AddParameters(10, "@Total_Allowance", p.TotalAllowance);
                        db.AddParameters(11, "@Incentives", p.Incentives);
                        db.AddParameters(12, "@Gross", p.GrossSalary);
                        db.AddParameters(13, "@Leave_Deduction", p.LeaveDeduction);
                        db.AddParameters(14, "@Net_Salary", p.NetSalary);
                        db.AddParameters(15, "@No_Of_Leave", p.TotalLeave);
                        db.AddParameters(16, "@No_Of_Working_Days", p.TotalWorkingDays);
                        db.AddParameters(17, "@No_Of_Holidays", p.TotalHolidays);
                        db.AddParameters(18, "@Total_Attendance", p.TotalAttendance);
                        db.AddParameters(19, "@Status", p.PaymentStatus);
                        db.AddParameters(20, "@Created_By", p.CreatedBy);
                        db.AddParameters(21, "@Company_Id", p.CompanyId);
                        db.AddParameters(22, "@Date", p.Date);
                        db.AddParameters(23, "@Total_Deduction", p.TotalDeduction);
                        db.Open();
                        db.ExecuteNonQuery(CommandType.Text, query);
                    }
                    return new OutputMessage("Payslip saved successfully", true, Type.NoError, "PayrollTemplate | SavePaySlip", System.Net.HttpStatusCode.OK);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "PayrollTemplate | SavePaySlip(List<PayRollTemplate> pay)");
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
