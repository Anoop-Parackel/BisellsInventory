using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Finance
{
    public class CostCenter
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion
        /// <summary>
        /// data For table
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDetails()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fcc_ID ID,Fcc_Name Name,Fcc_IsDisable Status,Fcc_SysDate SystemDate,Fcc_SysUser SystemUser from tbl_Fin_CostCenter").Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "CostCenter | GetDetails()");
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
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.CostCenter, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "CostCenter | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.RequiredFields, "CostCenter | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string sql = "select ISNULL(COUNT(Fcc_ID),0) Fcc_ID from tbl_Fin_CostCenter";
                        int Fcc_ID = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sql));
                              string query = @"INSERT INTO [dbo].[tbl_Fin_CostCenter]
                                                    ([Fcc_ID]
                                                    ,[Fcc_Name]
                                                    ,[Fcc_IsDisable]
                                                    ,[Fcc_SysDate]
                                                    ,[Fcc_SysUser])
                                              VALUES
                                                    (@Fcc_ID
                                                    ,@Fcc_Name
                                                    ,@Fcc_IsDisable
                                                    ,GETUTCDATE()
                                                    ,@Fcc_SysUser)";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Fcc_ID", Fcc_ID+1);
                        db.AddParameters(1, "@Fcc_Name", this.Name);
                        db.AddParameters(2, "@Fcc_IsDisable", this.Status);
                        db.AddParameters(3, "@Fcc_SysUser", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage(" Cost center saved successfully", true, Type.NoError, "CostCenter | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Cost Center could not be saved", false, Type.Others, "CostCenter | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Cost Center could not be saved", false, Type.Others, "CostCenter | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Details For Updating
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public static CostCenter GetDetails(int ItemID)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select TOP 1 Fcc_ID ID,Fcc_Name Name,Fcc_IsDisable Status,Fcc_SysUser SystemUser,Fcc_SysDate SystemDate from tbl_Fin_CostCenter where Fcc_ID= " + ItemID).Tables[0];
                DataRow CostCenterData = dt.Rows[0];
                CostCenter Cost = new CostCenter();
                Cost.ID = CostCenterData["ID"] != DBNull.Value ? Convert.ToInt32(CostCenterData["ID"]) : 0;
                Cost.Name = Convert.ToString(CostCenterData["Name"]);
                Cost.Status = CostCenterData["Status"] != DBNull.Value ? Convert.ToInt32(CostCenterData["Status"]) : 0;
                return Cost;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "CostCenter | GetDetails(int ItemID)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Update Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.CostCenter, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "CostCenter | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "CostCenter | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.Others, "CostCenter | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                   string query = @" UPDATE [dbo].[tbl_Fin_CostCenter]
                                   SET [Fcc_Name] = @Fcc_Name
                                      ,[Fcc_IsDisable] = @Fcc_IsDisable
                                      ,[Fcc_SysDate] = @Fcc_SysDate
                                      ,[Fcc_SysUser] = @Fcc_SysUser
                                 WHERE Fcc_ID=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Fcc_Name", this.Name);
                        db.AddParameters(1, "@Fcc_IsDisable", this.Status);
                        db.AddParameters(2, "@Fcc_SysUser", this.ModifiedBy);
                        db.AddParameters(3, "@id", this.ID);
                        db.AddParameters(4, "@Fcc_SysDate", DateTime.UtcNow);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Cost center Updated Successfully", true, Type.NoError, "CostCenter | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Cost Center could not be updated", false, Type.Others, "CostCenter | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Cost Center could not be updated", false, Type.Others, "CostCenter | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }

            }

        }
        /// <summary>
        /// Delete Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.CostCenter, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "CostCenter | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from tbl_Fin_CostCenter where Fcc_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Cost center deleted successfully", true, Type.NoError, "CostCenter | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Cost center could not be deleted", false, Type.Others, "CostCenter | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "CostCenter | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "CostCenter | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
        public static DataTable LoadCostCenter()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fcc_ID ID,Fcc_Name name from tbl_fin_costCenter").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "CostCenter | LoadCostCenter()");
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

