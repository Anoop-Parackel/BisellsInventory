using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
   public class Attendance
    {
        #region Properties
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public DateTime TimeIn { get; set; }
        public string TimeInString { get; set; }
        public DateTime TimeOut { get; set; }
        public string TimeOutString { get; set; }
        public bool IsPresent { get; set; }
        public int AttendanceStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Employee { get; set; }
        #endregion Properties

        #region Functions
        /// <summary>
        /// Function for saving attendance of each employee
        /// </summary>
        /// <returns>Return success alert when details saved successfull otherwise returns an error message</returns>
        public static OutputMessage Save(List<Attendance> attendance)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    foreach (Attendance at in attendance)
                    {
                        string query = @"if exists(select Employee_Id from TBL_ATTENDANCE where Employee_Id=@employee_Id and Date=@date)
                           update TBL_ATTENDANCE set Time_In=@time_In,Time_Out=@time_Out,Attendance_Status=@status,Company_Id=@Company_Id,Modified_By=@modified_By,
                           Modified_Date=GETUTCDATE() where
                            Employee_Id=@employee_Id and Date=@date
                            else 
                            insert into TBL_ATTENDANCE (Employee_Id,Date,Time_In,Time_Out,Attendance_Status,Company_Id,Created_By,Created_Date)
                            values(@employee_Id,@date,@time_In,@time_Out,@status,@Company_Id,@created_By,GETUTCDATE())";

                        db.CreateParameters(8);
                        db.AddParameters(0, "@employee_Id", at.EmployeeId);
                        db.AddParameters(1, "@date", at.Date);
                        db.AddParameters(2, "@time_In", DBNull.Value);
                        db.AddParameters(3, "@time_Out", DBNull.Value);
                        db.AddParameters(4, "@status", at.AttendanceStatus);
                        db.AddParameters(5, "@created_By", at.CreatedBy);
                        db.AddParameters(6, "@modified_By", at.ModifiedBy);
                        db.AddParameters(7, "@Company_Id", at.CompanyId);
                        db.Open();
                        int NoOfRowsAffected = db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                    }
                    return new OutputMessage("Attendance marked successfully", true, Type.NoError, "Attendance | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                catch(Exception ex)
                {
                    return new OutputMessage("Something went wrong.Couldnot mark attendance", false, Type.Others, "Attendance | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Function for retrieving employee attendance details
        /// </summary>
        /// <param name="CompanyId">Company Id filters the employees of particular company</param>
        /// <returns>list of employee attendance details</returns>
        public static List<Attendance> GetAttendanceDetails(int CompanyId,DateTime ?From,DateTime ?To)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"select at.Attendance_Id,isnull(at.Employee_Id,0)[Employee_Id],at.Date,at.Time_In,at.Time_Out,isnull(at.Attendance_Status,0)[Attendance_Status],
                                  isnull(at.Company_Id,0)[Company_Id],em.First_Name[Employee],co.Name[Company]
                                  from TBL_ATTENDANCE at
                                  inner join TBL_EMPLOYEE_MST em on em.Employee_Id=at.Employee_Id
                                  inner join TBL_COMPANY_MST co on co.Company_Id=at.Company_Id
                                  where at.Company_Id=@Company_Id  {#daterangefilter#}";
                    if (From != null && To != null)
                    {
                        query = query.Replace("{#daterangefilter#}", " and at.Date>=@fromdate and at.Date<=@todate ");
                        To = From.Value.AddDays(7);
                    }
                    else
                    {
                        To = DateTime.UtcNow;
                        From = new DateTime(To.Value.Year, To.Value.Month, 01);
                        query = query.Replace("{#daterangefilter#}", " and at.Date>=@fromdate and at.Date<=@todate ");
                    }
                    db.CreateParameters(3);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    db.AddParameters(1, "@fromdate", From);
                    db.AddParameters(2, "@todate", To);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<Attendance> result = new List<Attendance>();
                    if(dt!=null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            Attendance att = new Attendance();
                            att.AttendanceId = item["Attendance_Id"] != DBNull.Value ? Convert.ToInt32(item["Attendance_Id"]) : 0;
                            att.EmployeeId = item["Employee_Id"] != DBNull.Value ? Convert.ToInt32(item["Employee_Id"]) : 0;
                            att.Employee = Convert.ToString(item["Employee"]);
                            att.AttendanceStatus = item["Attendance_Status"]!=DBNull.Value? Convert.ToInt32(item["Attendance_Status"]):0;
                            att.DateString = item["Date"]!=DBNull.Value? Convert.ToDateTime(item["Date"]).ToString("dd-MMM-yyyy"):string.Empty;
                            att.TimeInString = item["Time_In"]!=DBNull.Value? Convert.ToDateTime(item["Time_In"]).ToString("dd-MMM-yyyy") :string.Empty;
                            att.TimeOutString = item["Time_Out"]!=DBNull.Value? Convert.ToDateTime(item["Time_Out"]).ToString("dd-MMM-yyyy"):string.Empty;
                            att.CompanyId = item["Company_Id"]!=DBNull.Value? Convert.ToInt32(item["Company_Id"]):0;
                            att.Company = Convert.ToString(item["Company"]);
                            result.Add(att);
                        }
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Attendance | GetAttendanceDetails(int CompanyId,DateTime ?From,DateTime ?To)");
                    return null;
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
