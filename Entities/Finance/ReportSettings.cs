using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Finance
{
    public class ReportSettings
    {
        #region Properties
        public int ID { get; set; }
        public int AccountGroupID { get; set; }
        public int ReportID { get; set; }
        public int Postion { get; set; }
        public int Order { get; set; }
        public int isMinus { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion
        /// <summary>
        /// Details For the Table
        /// </summary>
        /// <returns></returns>
        public static DataTable GetData(int CompanyID)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select a.Frs_ObjectID ID,b.Fag_Name AccountGroupName,Frs_ReportID,Frs_Position Position,Frs_Order [Order],Frs_IsMinus from tbl_Fin_ReportSetting a inner join tbl_Fin_AccountGroup b on a.Frs_FagID=b.fag_id where b.fag_comID="+CompanyID).Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "reportsettings |GetData(int CompanyID)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.ReportSettings, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "ReportSettings | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"INSERT INTO [dbo].[tbl_Fin_ReportSetting]
                                                                         ([Frs_FagID]
                                                                         ,[Frs_ReportID]
                                                                         ,[Frs_Position]
                                                                         ,[Frs_Order]
                                                                         ,[Frs_IsMinus])
                                                                   VALUES
                                                                         (@Frs_FagID
                                                                         ,@Frs_ReportID
                                                                         ,@Frs_Position
                                                                         ,@Frs_Order
                                                                         ,@Frs_IsMinus)";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Frs_FagID", this.AccountGroupID);
                        db.AddParameters(1, "@Frs_ReportID", this.ReportID);
                        db.AddParameters(2, "@Frs_Position", this.Postion);
                        db.AddParameters(3, "@Frs_Order", this.Order);
                        db.AddParameters(4, "@Frs_IsMinus", this.isMinus);                 
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Report settings saved successfully", true, Type.NoError, "ReportSettings | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Report settings could not be saved", false, Type.Others, "ReportSettings | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Report settings could not be saved ", false, Type.Others, "ReportSettings | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Details for Updation
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static ReportSettings GetDetails(int Id)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select [Frs_ObjectID],[Frs_FagID],[Frs_ReportID],[Frs_Position],[Frs_Order],[Frs_IsMinus] from [tbl_Fin_ReportSetting] where [Frs_ObjectID]= " + Id).Tables[0];
                DataRow CostCenterData = dt.Rows[0];
                ReportSettings Cost = new ReportSettings();
                Cost.ID = CostCenterData["Frs_ObjectID"] != DBNull.Value ? Convert.ToInt32(CostCenterData["Frs_ObjectID"]) : 0;
                Cost.AccountGroupID = Convert.ToInt32(CostCenterData["Frs_FagID"]);
                Cost.isMinus = CostCenterData["Frs_IsMinus"] != DBNull.Value ? Convert.ToInt32(CostCenterData["Frs_IsMinus"]) : 0;
                Cost.Postion= Convert.ToInt32(CostCenterData["Frs_Position"]);
                Cost.Order= Convert.ToInt32(CostCenterData["Frs_Order"]);
                Cost.ReportID= Convert.ToInt32(CostCenterData["Frs_ReportID"]);
                return Cost;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "reportsettings |GetDetails(int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Delete Functionailty
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.ReportSettings, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "ReportSettings | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from tbl_Fin_ReportSetting where Frs_ObjectID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Report settings deleted successfully", true, Type.NoError, "ReportSettings | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Report settings could not be deleted", false, Type.Others, "ReportSettings | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "ReportSettings | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "ReportSettings | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
        /// <summary>
        /// Update Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.ReportSettings, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "ReportSettings | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id Must Not Be Empty", false, Type.Others, "ReportSettings | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"update [tbl_Fin_ReportSetting] set
                                                     [Frs_FagID]=@Frs_FagID
                                                     ,[Frs_ReportID]=@Frs_ReportID
                                                     ,[Frs_Position]=@Frs_Position
                                                     ,[Frs_Order]=@Frs_Order
                                                     ,[Frs_IsMinus]=@Frs_IsMinus
	                                                 where Frs_ObjectID=@ID";
                        db.CreateParameters(6);
                        db.AddParameters(0, "@Frs_FagID", this.AccountGroupID);
                        db.AddParameters(1, "@Frs_ReportID", this.ReportID);
                        db.AddParameters(2, "@Frs_Position", this.Postion);
                        db.AddParameters(3, "@Frs_Order", this.Order);
                        db.AddParameters(4, "@Frs_IsMinus", this.isMinus);
                        db.AddParameters(5, "@ID", this.ID);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Report settings updated successfully", true, Type.NoError, "ReportSettings | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Report settings could not be updated", false, Type.Others, "ReportSettings | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Report settings could not be updated", false, Type.Others, "ReportSettings | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }

            }

        }
    }
}
