using Core.DBManager;
using Entities.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
 public  class OfficeShift
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime MondayInTime { get; set; }
        public DateTime MondayOutTime { get; set; }
        public DateTime TuesdayInTime { get; set; }
        public DateTime TuesdayOutTime { get; set; }
        public DateTime WednesdayInTime { get; set; }
        public DateTime WednesdayOutTime { get; set; }
        public DateTime ThursdayInTime { get; set; }
        public DateTime ThursdayOutTime { get; set; }
        public DateTime FridayInTime { get; set; }
        public DateTime FridayOutTime { get; set; }
        public DateTime SaturdayInTime { get; set; }
        public DateTime SaturdayOuttime { get; set; }
        public DateTime SundayInTime { get; set; }
        public DateTime SundayOutTime { get; set; }
        public string MondayOutShift { get; set; }
        public string TuesdayOutShift { get; set; }
        public string WednesdayOutShift { get; set; }
        public string ThursdayOutShift { get; set; }
        public string FridayOutShift { get; set; }
        public string SaturdayOutShift { get; set; }
        public string SundayOutShift { get; set; }
        public string MondayShift { get; set; }
        public string TuesdayShift { get; set; }
        public string WednesdayShift { get; set; }
        public string ThursdayShift { get; set; }
        public string FridayShift { get; set; }
        public string SaturdayShift { get; set; }
        public string SundayShift { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion Properties

        /// <summary>
        /// save details of office shifts
        /// </summary>
        /// <returns>return success alert when details saved successfully otherwise return an error alert</returns>
        public OutputMessage Save()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.OfficeShift, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "OfficeShift | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Shift name must not be Empty", false, Type.RequiredFields, "OfficeShift | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into TBL_OFFICE_SHIFT_MST (Shift_Name,Monday_In_Time,Monday_Out_Time,Tuesday_In_Time,Tuesday_Out_Time,Wednesday_In_Time,Wednesday_Out_Time,  
                                      Thursday_In_Time,Thursday_Out_Time,Friday_In_Time,Friday_Out_Time,Saturday_In_Time,Saturday_Out_Time,Sunday_In_Time,Sunday_Out_time,  
                                      Company_Id,Created_By,Created_Date )values(@Shift_Name,@Monday_In_Time,@Monday_Out_Time,@Tuesday_In_Time,@Tuesday_Out_Time,@Wednesday_In_Time,@Wednesday_Out_Time,  
                                      @Thursday_In_Time,@Thursday_Out_Time,@Friday_In_Time,@Friday_Out_Time,@Saturday_In_Time,@Saturday_Out_Time,@Sunday_In_Time,@Sunday_Out_time,  
                                      @Company_Id,@Created_By,GETUTCDATE())";
                        db.CreateParameters(17);
                        db.AddParameters(0, "@Shift_Name", this.Name);
                        db.AddParameters(1, "@Monday_In_Time", this.MondayInTime);
                        db.AddParameters(2, "@Monday_Out_Time", this.MondayOutTime);
                        db.AddParameters(3, "@Tuesday_In_Time", this.TuesdayInTime);
                        db.AddParameters(4, "@Tuesday_Out_Time", this.TuesdayOutTime);
                        db.AddParameters(5, "@Wednesday_In_Time", this.WednesdayInTime);
                        db.AddParameters(6, "@Wednesday_Out_Time", this.WednesdayOutTime);
                        db.AddParameters(7, "@Thursday_In_Time", this.ThursdayInTime);
                        db.AddParameters(8, "@Thursday_Out_Time", this.ThursdayOutTime);
                        db.AddParameters(9, "@Friday_In_Time", this.FridayInTime);
                        db.AddParameters(10, "@Friday_Out_Time", this.FridayOutTime);
                        db.AddParameters(11, "@Saturday_In_Time", this.SaturdayInTime); 
                        db.AddParameters(12, "@Saturday_Out_Time", this.SaturdayOuttime); 
                        db.AddParameters(13, "@Sunday_In_Time", this.SundayInTime);
                        db.AddParameters(14, "@Sunday_Out_time", this.SundayOutTime);
                        db.AddParameters(15, "@Company_Id", this.CompanyId);
                        db.AddParameters(16, "@Created_By", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Office shift Saved successfully", true, Type.NoError, "OfficeShift | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Office shift could not be Saved", false, Type.Others, "OfficeShift | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }

                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Office shift could not be Saved", false, Type.Others, "OfficeShift | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }

                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// update details of office shift details
        /// </summary>
        /// <returns>return success alert when the details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.OfficeShift, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError);


            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {

                return new OutputMessage("Shift Name must not be empty", false, Type.RequiredFields, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"update TBL_OFFICE_SHIFT_MST set Shift_Name=@Shift_Name,Monday_In_Time=@Monday_In_Time,Monday_Out_Time=@Monday_Out_Time,Tuesday_In_Time=@Tuesday_In_Time,Tuesday_Out_Time=@Tuesday_Out_Time,
                                      Wednesday_In_Time=@Wednesday_In_Time,Wednesday_Out_Time=@Wednesday_Out_Time,Thursday_In_Time=@Thursday_In_Time,Thursday_Out_Time=@Thursday_Out_Time,Friday_In_Time=@Friday_In_Time,Friday_Out_Time=@Friday_Out_Time,
                                      Saturday_In_Time=@Saturday_In_Time,Saturday_Out_Time=@Saturday_Out_Time,Sunday_In_Time=@Sunday_In_Time,Sunday_Out_time=@Sunday_Out_time,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Office_Shift_Id=@id";
                        db.CreateParameters(17);
                        db.AddParameters(0, "@Shift_Name", this.Name);
                        db.AddParameters(1, "@Monday_In_Time", this.MondayInTime);
                        db.AddParameters(2, "@Monday_Out_Time", this.MondayOutTime);
                        db.AddParameters(3, "@Tuesday_In_Time", this.TuesdayInTime);
                        db.AddParameters(4, "@Tuesday_Out_Time", this.TuesdayOutTime);
                        db.AddParameters(5, "@Wednesday_In_Time", this.WednesdayInTime);
                        db.AddParameters(6, "@Wednesday_Out_Time", this.WednesdayOutTime);
                        db.AddParameters(7, "@Thursday_In_Time", this.ThursdayInTime);
                        db.AddParameters(8, "@Thursday_Out_Time", this.ThursdayOutTime);
                        db.AddParameters(9, "@Friday_In_Time", this.FridayInTime);
                        db.AddParameters(10, "@Friday_Out_Time", this.FridayOutTime);
                        db.AddParameters(11, "@Saturday_In_Time", this.SaturdayInTime);
                        db.AddParameters(12, "@Saturday_Out_Time", this.SaturdayOuttime);
                        db.AddParameters(13, "@Sunday_In_Time", this.SundayInTime);
                        db.AddParameters(14, "@Sunday_Out_time", this.SundayOutTime);
                        db.AddParameters(15, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(16, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Office shift Updated successfully", true, Type.NoError, "OfficeShift | Update", System.Net.HttpStatusCode.OK);


                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Office shift could not be Updated", false, Type.Others, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Office shift could not be Updated", false, Type.Others, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// delete each office shifts 
        /// to delete an entry first you have to set  id of the particular entry
        /// </summary>
        /// <returns>return success alert when office shift delete successfully otherwise return error message</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.OfficeShift, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "OfficeShift | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_OFFICE_SHIFT_MST where Office_Shift_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Office shift Deleted successfully", true, Type.NoError, "OfficeShift | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                         return new OutputMessage("Something went wrong. Office shift could not be Deleted", false, Type.Others, "OfficeShift | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {

                     return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "OfficeShift | Update", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this officeshift because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "officeshift | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "officeshift | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Office shift could not be deleted", false, Type.Others, "officeshift | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }
        /// <summary>
        /// Retrieve all officeshift details from payroll master
        /// </summary>
        /// <param name="CompanyId">company id of that list</param>
        /// <returns>list of office shift details</returns>
        public static List<OfficeShift> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select s.Office_Shift_Id,s.Shift_Name,s.Monday_In_Time,s.Monday_Out_Time, s.Tuesday_In_Time,s.Tuesday_Out_Time,s.Wednesday_In_Time,
                               s.Wednesday_Out_Time,s.Thursday_In_Time,s.Thursday_Out_Time, s.Friday_In_Time,s.Friday_Out_Time,s.Saturday_In_Time,
                               s.Saturday_Out_Time,s.Sunday_In_Time,s.Sunday_Out_time, l.Name[Company] 
                               from TBL_OFFICE_SHIFT_MST s
                               left join TBL_COMPANY_MST l on l.Company_Id = s.Company_Id where l.Company_Id=@Company_Id order by s.Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<OfficeShift> result = new List<OfficeShift>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        OfficeShift off = new OfficeShift();
                        off.ID = item["Office_Shift_Id"] != DBNull.Value ? Convert.ToInt32(item["Office_Shift_Id"]) : 0;
                        off.Name = Convert.ToString(item["Shift_Name"]);
                        if(item["Monday_In_Time"] != DBNull.Value && item["Monday_Out_Time"] != DBNull.Value)
                        {
                            off.MondayShift = Convert.ToDateTime(item["Monday_In_Time"]).ToString("H:mm")+" - "+Convert.ToDateTime(item["Monday_Out_Time"]).ToString("H:mm");
                        }
                      if(item["Tuesday_In_Time"] != DBNull.Value && item["Tuesday_Out_Time"] != DBNull.Value)
                        {
                            off.TuesdayShift=Convert.ToDateTime(item["Tuesday_In_Time"]).ToString("H:mm")+" - "+ Convert.ToDateTime(item["Tuesday_Out_Time"]).ToString("H:mm");
                        }
                       if(item["Wednesday_In_Time"] != DBNull.Value && item["Wednesday_Out_Time"] != DBNull.Value)
                        {
                            off.WednesdayShift = Convert.ToDateTime(item["Wednesday_In_Time"]).ToString("H:mm") + " - " + Convert.ToDateTime(item["Wednesday_Out_Time"]).ToString("H:mm");
                        }
                      if(item["Thursday_In_Time"] != DBNull.Value && item["Thursday_Out_Time"] != DBNull.Value)
                        {
                            off.ThursdayShift = Convert.ToDateTime(item["Thursday_In_Time"]).ToString("H:mm") + " - " + Convert.ToDateTime(item["Thursday_Out_Time"]).ToString("H:mm");
                        }                      
                      if(item["Friday_In_Time"] != DBNull.Value && item["Friday_Out_Time"] != DBNull.Value)
                        {
                            off.FridayShift = Convert.ToDateTime(item["Friday_In_Time"]).ToString("H:mm") + " - " + Convert.ToDateTime(item["Friday_Out_Time"]).ToString("H:mm");
                        }
                      if(item["Saturday_In_Time"] != DBNull.Value && item["Saturday_Out_Time"] != DBNull.Value)
                        {
                            off.SaturdayShift= Convert.ToDateTime(item["Saturday_In_Time"]).ToString("H:mm") + " - " + Convert.ToDateTime(item["Saturday_Out_Time"]).ToString("H:mm");
                        }
                        if(item["Sunday_In_Time"] != DBNull.Value && item["Sunday_Out_time"] != DBNull.Value)
                        {
                            off.SundayShift= Convert.ToDateTime(item["Sunday_In_Time"]).ToString("H:mm") + " - " + Convert.ToDateTime(item["Sunday_Out_time"]).ToString("H:mm");
                        }

                        result.Add(off);
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
                Application.Helper.LogException(ex, "OfficeShift  |  GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single office shift entry from list of office shift details
        /// </summary>
        /// <param name="id">Id of that particular entry</param>
        /// <param name="CompanyId">comapanyid of that particular entry</param>
        /// <returns>single entry of office shift details</returns>
        public static OfficeShift GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 s.Office_Shift_Id,s.Shift_Name,s.Monday_In_Time,s.Monday_Out_Time,
                               s.Tuesday_In_Time,s.Tuesday_Out_Time,s.Wednesday_In_Time,
                               s.Wednesday_Out_Time,s.Thursday_In_Time,s.Thursday_Out_Time,
                               s.Friday_In_Time,s.Friday_Out_Time,s.Saturday_In_Time,
                               s.Saturday_Out_Time,s.Sunday_In_Time,s.Sunday_Out_time,
                               l.Name[Company] from TBL_OFFICE_SHIFT_MST s
                               left join TBL_COMPANY_MST l on l.Company_Id = s.Company_Id where l.Company_Id=@Company_Id and Office_Shift_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                if (dt != null)
                {
                    OfficeShift off = new OfficeShift();
                    DataRow item = dt.Rows[0];
                    off.ID = item["Office_Shift_Id"] != DBNull.Value ? Convert.ToInt32(item["Office_Shift_Id"]) : 0;
                    off.Name = Convert.ToString(item["Shift_Name"]).ToString();
                    off.MondayShift = item["Monday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Monday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.MondayOutShift = item["Monday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Monday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.TuesdayShift = item["Tuesday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Tuesday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.TuesdayOutShift = item["Tuesday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Tuesday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.WednesdayShift = item["Wednesday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Wednesday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.WednesdayOutShift = item["Wednesday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Wednesday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.ThursdayShift = item["Thursday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Thursday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.ThursdayOutShift = item["Thursday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Thursday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.FridayShift = item["Friday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Friday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.FridayOutShift = item["Friday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Friday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.SaturdayShift = item["Saturday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Saturday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.SaturdayOutShift = item["Saturday_Out_Time"] != DBNull.Value ? Convert.ToDateTime(item["Saturday_Out_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.SundayShift = item["Sunday_In_Time"] != DBNull.Value ? Convert.ToDateTime(item["Sunday_In_Time"]).ToString("HH:mm:ss") : string.Empty;
                    off.SundayOutShift = item["Sunday_Out_time"] != DBNull.Value ? Convert.ToDateTime(item["Sunday_Out_time"]).ToString("HH:mm:ss") : string.Empty;

                    return off;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "OfficeShift  |  GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
