using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Finance
{

    public class VoucherType
    {
        #region Properties
           public int ID { get; set; }
           public string Name { get; set; }
           public int Disable { get; set; }
           public int Numbering { get; set; }
           public int NumberStartFrom { get; set; }
           public int RestartNumber { get; set; }
           public int IsDebit { get; set; }
           public int DisplayInJournal { get; set; }
           public int CreatedBy { get; set; }
           public int ModifiedBy { get; set; }
           public DateTime CreatedDate { get; set; }
           public DateTime ModifiedDate { get; set; }
        #endregion
        /// <summary>
        /// For Saving the Voucher Type
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherType, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "VoucherType | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Voucher Name must not be empty", false, Type.RequiredFields, "VoucherType | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string sql = "select ISNULL(Max(Fvt_ID),0)+1 Fvt_ID from tbl_Fin_VoucherType";
                        int newID = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sql));
                               string query = @"INSERT INTO [dbo].[tbl_Fin_VoucherType]
                                     ([Fvt_ID]
                                     ,[Fvt_TypeName]
                                     ,[Fvt_Disable]
                                     ,[Fvt_Numbering]
                                     ,[Fvt_NoStatFrom]
                                     ,[Fvt_RestartNo]
                                     ,[Fvt_InitDr]
                                     ,[Fvt_DisplayInJournal])
                               VALUES
                                     (@Fvt_ID
                                     ,@Fvt_TypeName
                                     ,@Fvt_Disable
                                     ,@Fvt_Numbering
                                     ,@Fvt_NoStatFrom
                                     ,@Fvt_RestartNo
                                     ,@Fvt_InitDr
                                     ,@Fvt_DisplayInJournal)";
                        db.CreateParameters(8);
                        db.AddParameters(0, "@Fvt_TypeName", this.Name);
                        db.AddParameters(1, "@Fvt_Disable", this.Disable);
                        db.AddParameters(2, "@Fvt_Numbering", this.Numbering);
                        db.AddParameters(3, "@Fvt_NoStatFrom", this.NumberStartFrom);
                        db.AddParameters(4, "@Fvt_RestartNo", this.RestartNumber);
                        db.AddParameters(5, "@Fvt_InitDr", this.IsDebit);
                        db.AddParameters(6, "@Fvt_DisplayInJournal", this.DisplayInJournal);
                        db.AddParameters(7, "@Fvt_ID", newID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Saved successfully", true, Type.NoError, "VoucherType | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherType | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherType | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// To Get All the voucher Types
        /// </summary>
        /// <returns></returns>
        public static DataTable GetVoucherType()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fvt_ID ID,Fvt_TypeName Name,Fvt_NoStatFrom [Numbering Starts From],Fvt_Disable Status from tbl_Fin_VoucherType").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "vouchertype | GetVoucherType()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// To get the details for updating
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static VoucherType GetDetails(int ItemID, int CompanyId)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select Top 1 Fvt_ID Voucher_Type_Id,Fvt_TypeName TypeName,Fvt_Disable Disable,Fvt_Numbering Numbering,Fvt_NoStatFrom NoStatFrom,Fvt_RestartNo RestartNo,Fvt_InitDr Is_Debit,Fvt_DisplayInJournal Display_In_Journal from tbl_Fin_VoucherType where Fvt_ID =" + ItemID).Tables[0];
                DataRow VoucherTypeData = dt.Rows[0];
                VoucherType Voucher = new VoucherType();
                Voucher.ID = VoucherTypeData["Voucher_Type_Id"] != DBNull.Value ? Convert.ToInt32(VoucherTypeData["Voucher_Type_Id"]) : 0;
                Voucher.Name = Convert.ToString(VoucherTypeData["TypeName"]);
                Voucher.Disable = VoucherTypeData["Disable"] != DBNull.Value ? Convert.ToInt32(VoucherTypeData["Disable"]) : 0;
                Voucher.Numbering = Convert.ToInt32(VoucherTypeData["Numbering"]);
                Voucher.NumberStartFrom = Convert.ToInt32(VoucherTypeData["NoStatFrom"]);
                Voucher.RestartNumber = Convert.ToInt32(VoucherTypeData["RestartNo"]);
                Voucher.IsDebit = VoucherTypeData["Is_Debit"] != DBNull.Value ? Convert.ToInt32(VoucherTypeData["Is_Debit"]) : 0;
                Voucher.DisplayInJournal = VoucherTypeData["Display_In_Journal"] != DBNull.Value ? Convert.ToInt32(VoucherTypeData["Display_In_Journal"]) : 0;
                return Voucher;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "vouchertype | GetDetails(int ItemID, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Update Function
        /// </summary>
        /// <returns></returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.VoucherType, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherType | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id Must Not Be Empty", false, Type.Others, "VoucherType | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name Must Not Be Empty", false, Type.Others, "VoucherType | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"UPDATE [dbo].[tbl_Fin_VoucherType]
                                           SET [Fvt_TypeName] = @Fvt_TypeName
                                              ,[Fvt_Disable] = @Fvt_Disable
                                              ,[Fvt_Numbering] = @Fvt_Numbering
                                              ,[Fvt_NoStatFrom] = @Fvt_NoStatFrom
                                              ,[Fvt_RestartNo] = @Fvt_RestartNo
                                              ,[Fvt_InitDr] = @Fvt_InitDr
                                              ,[Fvt_DisplayInJournal] = @Fvt_DisplayInJournal
                                           WHERE Fvt_ID=@id";
                        db.CreateParameters(8);
                        db.AddParameters(0, "@Fvt_TypeName", this.Name);
                        db.AddParameters(1, "@Fvt_Disable", this.Disable);
                        db.AddParameters(2, "@Fvt_Numbering", this.Numbering);
                        db.AddParameters(3, "@Fvt_NoStatFrom", this.NumberStartFrom);
                        db.AddParameters(4, "@Fvt_RestartNo", this.RestartNumber);
                        db.AddParameters(5, "@Fvt_InitDr", this.IsDebit);
                        db.AddParameters(6, "@Fvt_DisplayInJournal", this.DisplayInJournal);
                        db.AddParameters(7, "@id", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Voucher Type has been Updated Successfully", true, Type.NoError, "VoucherType | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.could not Update VoucherType", false, Type.Others, "VoucherType | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong.could not Update VoucherType", false, Type.Others, "VoucherType | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// For Deleting the data
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.VoucherType, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherType | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from tbl_Fin_VoucherType where Fvt_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("VoucherType has been Deleted Successfully", true, Type.NoError, "VoucherType | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.could not Delete Vocher Type", false, Type.Others, "VoucherType | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "VoucherType | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete this VoucherType", false, Entities.Type.Others, "VoucherType | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
        public static DataTable GetVoucherTypes(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fvt_ID id,Fvt_TypeName name from tbl_Fin_VoucherType").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "vouchertype | GetVoucherTypes(int CompanyId)");
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
