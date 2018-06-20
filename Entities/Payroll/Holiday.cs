using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
   public class Holiday
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int TotalLeave { get; set; }
        #endregion Properties

        public OutputMessage Save()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"insert into TBL_HOLIDAYS (Name,Date,Description,Company_Id,Created_By,Created_Date)
                                  values(@Name,@Date,@Description,@Company_Id,@Created_By,GETUTCDATE())";
                    db.CreateParameters(5);
                    db.AddParameters(0, "@Name", this.Name);
                    db.AddParameters(1, "@Date", this.Date);
                    db.AddParameters(2, "@Description", this.Description);
                    db.AddParameters(3, "@Company_Id", this.CompanyId);
                    db.AddParameters(4, "@Created_By", this.CreatedBy);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Holiday marked successfully", true, Type.NoError, "Holiday | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong.Couldnot mark Holiday", false, Type.Others, "Holiday | Save", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public OutputMessage Update()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"update TBL_HOLIDAYS set Name=@Name,Date=@Date,Description=@Description,Company_Id=@Company_Id,
                                   Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Holiday_Id=@ID";
                                 
                    db.CreateParameters(6);
                    db.AddParameters(0, "@Name", this.Name);
                    db.AddParameters(1, "@Date", this.Date);
                    db.AddParameters(2, "@Description", this.Description);
                    db.AddParameters(3, "@Company_Id", this.CompanyId);
                    db.AddParameters(4, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(5, "@ID", this.Id);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Holiday marked successfully", true, Type.NoError, "Holiday | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong.Couldnot mark Holiday", false, Type.Others, "Holiday | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static List<Holiday> GetDetails(int CompanyId)
        {
            using(DBManager db=new DBManager())
            {
                try
                {
                    string query = @"select h.Holiday_Id,h.Name,h.[Date],h.[Description],isnull(h.Company_Id,0)[Company_Id],
                                  isnull( h.Created_By,0)[Created_By],h.Created_Date,
								  isnull(h.Modified_By,0)[Modified_By],h.Modified_Date 
                                  from TBL_HOLIDAYS h
                                  where h.Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<Holiday> result = new List<Holiday>();
                    if(dt!=null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            Holiday holiday = new Holiday();
                            holiday.Id = item["Holiday_Id"] != DBNull.Value ? Convert.ToInt32(item["Holiday_Id"]) : 0;
                            holiday.Name = Convert.ToString(item["Name"]);
                            holiday.Date = Convert.ToDateTime(item["Date"]);
                            holiday.startDate = Convert.ToDateTime(item["Date"]).ToString("dd MMM yyyy");
                            holiday.endDate = Convert.ToDateTime(item["Date"]).ToString("dd MMM yyyy");
                            holiday.Description = Convert.ToString(item["Description"]);
                            holiday.CompanyId = item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                            result.Add(holiday);
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
                    Application.Helper.LogException(ex, "Holiday | GetDetails(int CompanyId)");
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
