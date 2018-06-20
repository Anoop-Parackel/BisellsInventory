using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Payroll
{
    public class HourlyTemplate
    {
        #region Properties
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Rate { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CompanyId { get; set; }
        #endregion Properties

        /// <summary>
        /// Save hourly template details
        /// </summary>
        /// <returns>return success alert when details saved successfully otherwise return error alert</returns>
        public OutputMessage Save()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.HourlyTemplate, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "HourlyTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Title))
            {
                return new OutputMessage("Title must not be Empty", false, Type.RequiredFields, "HourlyTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into TBL_HOURLY_WAGE_MST([Grade],[Rate],[Status],[Company_Id],[Created_By],[Created_Date])
                                        VALUES(@Grade,@Rate,@Status,@Company_Id,@Created_By,GETUTCDATE())";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Grade", this.Title);
                        db.AddParameters(1, "@Rate", this.Rate);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Company_Id", this.CompanyId);
                        db.AddParameters(4, "@Created_By", this.CreatedBy);
                        
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Hourly Template saved successfully", true, Type.NoError, "HourlyTemplate | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Hourly Template could not be saved", false, Type.Others, "HourlyTemplate | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }

                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Hourly Template could not be saved", false, Type.Others, "HourlyTemplate | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }

                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Update hourly template details
        /// </summary>
        /// <returns>Return success message when details updated successfully or otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.HourlyTemplate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID must not be empty", false, Type.Others, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError);


            }
            else if (string.IsNullOrWhiteSpace(this.Title))
            {

                return new OutputMessage("Title must not be empty", false, Type.RequiredFields, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_HOURLY_WAGE_MST] set [Grade]=@Grade,[Rate]=@Rate,
                         Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Hourly_Rate_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Grade", this.Title);
                        db.AddParameters(1, "@Rate", this.Rate);
                        db.AddParameters(2, "@Status", this.Status);
                        db.AddParameters(3, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(4, "@id", this.ID);

                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Hourly Template Updated successfully", true, Type.NoError, "HourlyTemplate | Update", System.Net.HttpStatusCode.OK);


                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Hourly Template could not be updated", false, Type.Others, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Hourly Template could not be updated", false, Type.Others, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Delete hourly template details 
        /// to delete an entry first you have to set the id of that particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error message</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.HourlyTemplate, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "HourlyTemplate | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_HOURLY_WAGE_MST where Hourly_Rate_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Hourly Template deleted successfully", true, Type.NoError, "HourlyTemplate | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Hourly Template could not be deleted", false, Type.Others, "HourlyTemplate | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {

                        return new OutputMessage("ID must not be zero for deletion", false, Type.Others, "HourlyTemplate | Update", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this hourly template because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "hourlytemplate | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "hourlytemplate | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.could not Delete hourly template", false, Type.Others, "hourlytemplate| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                  db.Close();

                }

            }
        }
        /// <summary>
        /// Retrieve all hourly template details from payroll master
        /// </summary>
        /// <param name="CompanyId">company id of that list</param>
        /// <returns>list of hourly template details</returns>
        public static List<HourlyTemplate> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select h.Hourly_Rate_Id,h.Grade,isnull(h.Rate,0)[Rate],isnull(h.Status,0)[Status],isnull(h.Company_Id,0)[Company_Id],
                               isnull(h.Created_By,0)[Created_by],h.Created_Date,isnull(h.Modified_By,0)[Modified_by],
                               h.Modified_Date,c.Name[Company] from TBL_HOURLY_WAGE_MST h
                               left join TBL_COMPANY_MST c on c.Company_Id=h.Company_Id
                               where h.Company_Id=@Company_Id order by h.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List <HourlyTemplate> result = new List<HourlyTemplate>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        HourlyTemplate hour = new HourlyTemplate();
                        hour.ID = item["Hourly_Rate_Id"] != DBNull.Value ? Convert.ToInt32(item["Hourly_Rate_Id"]) : 0;
                        hour.Title = Convert.ToString(item["Grade"]);
                        hour.CompanyId = item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                        hour.Rate = item["Rate"] != DBNull.Value ? Convert.ToInt32(item["Rate"]) : 0;
                        hour.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        result.Add(hour);
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
                Application.Helper.LogException(ex, "Hourly Template | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        ///  Retrieve a single template details from list of hourly template details
        /// </summary>
        /// <param name="id">Id of that particular entry</param>
        /// <param name="CompanyId">comapanyid of that particular entry</param>
        /// <returns>single entry of hourly template details</returns>
        public static HourlyTemplate GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 h.Hourly_Rate_Id,h.Grade,isnull(h.Rate,0)[Rate],isnull(h.Status,0)[Status],
                               isnull(h.Company_Id,0)[Company_Id],isnull(h.Created_By,0)[Created_By],h.Created_Date,
                               isnull(h.Modified_By,0)[Modified_by],h.Modified_Date,c.Name[Company]
                               from TBL_HOURLY_WAGE_MST h
                               left join TBL_COMPANY_MST c on c.Company_Id=h.Company_Id
                               where h.Company_Id=@Company_Id and h.Hourly_Rate_Id=@id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query); 
                if (dt != null)
                {

                    HourlyTemplate hour = new HourlyTemplate();
                    DataRow item = dt.Rows[0];
                    hour.ID = item["Hourly_Rate_Id"] != DBNull.Value ? Convert.ToInt32(item["Hourly_Rate_Id"]) : 0;
                    hour.Title = Convert.ToString(item["Grade"]);
                    hour.CompanyId = item["Company_Id"] != DBNull.Value ? Convert.ToInt32(item["Company_Id"]) : 0;
                    hour.Rate = item["Rate"] != DBNull.Value ? Convert.ToInt32(item["Rate"]) : 0;
                    hour.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                    return hour;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Hourly Template | GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
   }
}
