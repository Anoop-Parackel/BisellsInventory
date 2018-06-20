using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Application;

namespace Entities.Finance
{
    public class AccountHeadMaster
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public Decimal OpeningBalance { get; set; }
        public DateTime OpeningDate { get; set; }
        public string ContactPerson { get; set; }
        public int status { get; set; }
        public int Category { get; set; }
        public int ReverseHeadId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int IsDebit { get; set; }
        public int ParentId { get; set; }
        public int AccountGroupId { get; set; }
        public string Email { get; set; }
        public int AccountType { get; set; }
        public int AccountNature { get; set; }
        public int CompanyID { get; set; }
        public string datestring { get; set; }
        public string DataSQL { get; set; }
        public string AmountSQL { get; set; }
        public string SQLTable { get; set; }
        public string SQLID { get; set; }
        public string SQLName { get; set; }
        public string TransactionSQL { get; set; }
        #endregion
        /// <summary>
        /// Save Functionality
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.AccountHeadMaster, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "AccountHeadMaster | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Group name must not be empty", false, Type.RequiredFields, "AccountHeadMaster | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string sql = "select ISNULL(MAX(Fah_ID),0)+1 Fah_ID  from tbl_Fin_AccountHead";
                        int newID = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sql));
                        string query = @"INSERT INTO [dbo].[tbl_Fin_AccountHead]
                                          ([Fah_ParentID]                                        
                                          ,[Fah_FagID]
                                          ,[Fah_ID]
                                          ,[Fah_Name]
                                          ,[Fah_Address]
                                          ,[Fah_ContactPerson]
                                          ,[Fah_Description]
                                          ,[Fah_OpeningBal]
                                          ,[Fah_IsDebit]
                                          ,[Fah_OpeningDate]
                                          ,[Fah_Phone]
                                          ,[Fah_Email]
                                          ,[Fah_Nature]
                                          ,[Fah_Disable]
                                          ,[Fah_Depth]
                                          ,[Fah_DataSQL]
                                          ,[Fah_AmountSQL]
                                          ,[Fah_SQLTable]
                                          ,[Fah_SQLID]
                                          ,[Fah_SQLName]
                                          ,[Fah_Type]
                                          ,[Fah_CurSysUser]
                                          ,[Fah_CurDtTime]
                                          ,[Fah_TransactionSQL]
                                          ,[Fah_ComID]
                                          ,[Fah_ReverseHeadID]
                                          ,[Fah_Category]
                                          ,[Fah_IsOpeningUpdate])
                                    VALUES
                                          (@Fah_ParentID
                                          ,@Fah_FagID
                                          ,@Fah_ID
                                          ,@Fah_Name
                                          ,@Fah_Address
                                          ,@Fah_ContactPerson
                                          ,@Fah_Description
                                          ,@Fah_OpeningBal
                                          ,@Fah_IsDebit
                                          ,@Fah_OpeningDate
                                          ,@Fah_Phone
                                          ,@Fah_Email
                                          ,@Fah_Nature
                                          ,@Fah_Disable
                                          ,0
                                          ,@Fah_DataSQL
                                          ,@Fah_AmountSQL
                                          ,@Fah_SQLTable
                                          ,@Fah_SQLID
                                          ,@Fah_SQLName
                                          ,@Fah_Type
                                          ,@Fah_CurSysUser
                                          ,getdate()
                                          ,@Fah_TransactionSQL
                                          ,@Fah_ComID
                                          ,@Fah_ReverseHeadID
                                          ,@Fah_Category
                                          ,0)";
                        db.CreateParameters(25);
                        db.AddParameters(0, "@Fah_ParentID", this.ParentId);
                        db.AddParameters(1, "@Fah_FagID", this.AccountGroupId);
                        db.AddParameters(2, "@Fah_Name", this.Name);
                        db.AddParameters(3, "@Fah_Address", this.Address);
                        db.AddParameters(4, "@Fah_ContactPerson", this.ContactPerson);
                        db.AddParameters(5, "@Fah_Description", this.Description);
                        db.AddParameters(6, "@Fah_OpeningBal", this.OpeningBalance);
                        db.AddParameters(7, "@Fah_IsDebit", this.IsDebit);
                        db.AddParameters(8, "@Fah_OpeningDate", this.OpeningDate);
                        db.AddParameters(9, "@Fah_Phone", this.Phone);
                        db.AddParameters(10, "@Fah_Email", this.Email);
                        db.AddParameters(11, "@Fah_Nature", this.AccountNature);
                        db.AddParameters(12, "@Fah_Disable", this.status);
                        db.AddParameters(13, "@Fah_Type", this.AccountType);
                        db.AddParameters(14, "@Fah_ComID", this.CompanyID);
                        db.AddParameters(15, "@Fah_Category", this.Category);
                        db.AddParameters(16, "@Fah_ReverseHeadID", this.ReverseHeadId);
                        db.AddParameters(17, "@Fah_CurSysUser", this.CreatedBy);
                        if (this.DataSQL == "")
                        {
                            db.AddParameters(18, "@Fah_DataSQL", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(18, "@Fah_DataSQL", this.DataSQL);
                        }
                        if (this.AmountSQL == "")
                        {
                            db.AddParameters(19, "@Fah_AmountSQL", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(19, "@Fah_AmountSQL", this.AmountSQL);
                        }
                        if (this.SQLTable == "")
                        {
                            db.AddParameters(20, "@Fah_SQLTable", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(20, "@Fah_SQLTable", this.SQLTable);
                        }
                        if (this.SQLID == "")
                        {
                            db.AddParameters(21, "@Fah_SQLID", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(21, "@Fah_SQLID", this.SQLID);
                        }
                        if (this.SQLName == "")
                        {
                            db.AddParameters(22, "@Fah_SQLName", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(22, "@Fah_SQLName", this.SQLName);
                        }
                        if (this.TransactionSQL == "")
                        {
                            db.AddParameters(23, "@Fah_TransactionSQL", DBNull.Value);
                        }
                        else
                        {
                            db.AddParameters(23, "@Fah_TransactionSQL", this.TransactionSQL);
                        }
                        db.AddParameters(24, "@Fah_ID", newID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage(" Finance head saved successfully", true, Type.NoError, "AccountHeadMaster | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Finance head could not be saved", false, Type.Others, "AccountHeadMaster | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Finance head could not be saved", false, Type.Others, "AccountHeadMaster | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Details for the Table
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDetails()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "Select AccountHeadID ID,Name,OpeningBal Balance,Phone,[Status] from tbl_fin_AccountHead").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountHeadMaster |  GetDetails()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Details For Updation
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static AccountHeadMaster GetDetails(int ID)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"Select top 1 AccountHeadID,ParentID,AccountGroup_Id,Name,[Address],
                                   ContactPerson,[Description],Email,OpeningBal,IsDebit,OpeningDate,Phone,Nature,[Status],[Type],ReverseHeadID,
                                   Category,DataSQL,AmountSQL,SQLTable,SQLID,SQLName,TransactionSQL from tbl_fin_AccountHead where AccountHeadID=" + ID).Tables[0];
                DataRow AccountGroupdata = dt.Rows[0];
                AccountHeadMaster Account = new AccountHeadMaster();
                Account.ID = AccountGroupdata["AccountHeadID"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata["AccountHeadID"]) : 0;
                Account.Name = Convert.ToString(AccountGroupdata["Name"]);
                Account.Description = Convert.ToString(AccountGroupdata["Description"]);
                Account.AccountType = Convert.ToInt32(AccountGroupdata["Type"]);
                Account.ParentId = Convert.ToInt32(AccountGroupdata["ParentID"]);
                Account.Address = Convert.ToString(AccountGroupdata["Address"]);
                Account.ContactPerson = Convert.ToString(AccountGroupdata["ContactPerson"]);
                Account.OpeningBalance = Convert.ToDecimal(AccountGroupdata["OpeningBal"]);
                Account.IsDebit = Convert.ToInt32(AccountGroupdata["IsDebit"]);
                Account.OpeningDate = Convert.ToDateTime(AccountGroupdata["OpeningDate"]);
                Account.datestring = Convert.ToDateTime(AccountGroupdata["OpeningDate"]).ToShortDateString();
                Account.Phone = Convert.ToString(AccountGroupdata["Phone"]);
                Account.AccountType = Convert.ToInt32(AccountGroupdata["Type"]);
                Account.ReverseHeadId = Convert.ToInt32(AccountGroupdata["ReverseHeadID"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata["ReverseHeadID"]) : 0);
                Account.Category = Convert.ToInt32(AccountGroupdata["Category"]);
                Account.Email = Convert.ToString(AccountGroupdata["Email"]);
                Account.DataSQL = Convert.ToString(AccountGroupdata["DataSQL"]);
                Account.AmountSQL = Convert.ToString(AccountGroupdata["AmountSQL"]);
                Account.SQLTable = Convert.ToString(AccountGroupdata["SQLTable"]);
                Account.SQLID = Convert.ToString(AccountGroupdata["SQLID"]);
                Account.SQLName = Convert.ToString(AccountGroupdata["SQLName"]);
                Account.TransactionSQL = Convert.ToString(AccountGroupdata["TransactionSQL"]);
                Account.status = AccountGroupdata["Status"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata["Status"]) : 0;
                return Account;
            }

            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "AccountHeadMaster | GetDetails(int ID)");
                return null;
            }
            finally
            {
                db.Close();
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
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.AccountHeadMaster, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"select isnull(Is_System_Defined,0)[Is_System_Defined] from tbl_Fin_AccountHead where Fah_Object_ID=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        bool IsSystemDefined = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                        if (IsSystemDefined)
                        {
                            return new OutputMessage("You cannot delete this account head because it is system defined", false, Type.Others, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                        query = @"delete from tbl_Fin_AccountHead where fah_id=@ID";
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Finance head deleted successfully", true, Type.NoError, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Finance head could not be deleted", false, Type.Others, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "AccountHeadMaster | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

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
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.AccountGroup, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "AccountHeadMaster | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "AccountHeadMaster | Update", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Name must not be empty", false, Type.Others, "AccountHeadMaster | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"UPDATE [dbo].[tbl_Fin_AccountHead]
                                           SET [Fah_ParentID] = @Fah_ParentID
                                              ,[Fah_FagID] = @Fah_FagID
                                              ,[Fah_Name] = @Fah_Name
                                              ,[Fah_Address] = @Fah_Address
                                              ,[Fah_ContactPerson] = @Fah_ContactPerson
                                              ,[Fah_Description] = @Fah_Description
                                              ,[Fah_OpeningBal] = @Fah_OpeningBal
                                              ,[Fah_IsDebit] = @Fah_IsDebit
                                              ,[Fah_OpeningDate] = @Fah_OpeningDate
                                              ,[Fah_Phone] = @Fah_Phone
                                              ,[Fah_Email] = @Fah_Email
                                              ,[Fah_Nature] = @Fah_Nature
                                              ,[Fah_Disable] = @Fah_Disable
                                              ,[Fah_Depth] = 0
                                              ,[Fah_DataSQL] = @Fah_DataSQL
                                              ,[Fah_AmountSQL] = @Fah_AmountSQL
                                              ,[Fah_SQLTable] = @Fah_SQLTable
                                              ,[Fah_SQLID] = @Fah_SQLID
                                              ,[Fah_SQLName] = @Fah_SQLName
                                              ,[Fah_Type] = @Fah_Type
                                              ,[Fah_CurSysUser] = @Fah_CurSysUser
                                              ,[Fah_CurDtTime] = getdate()
                                              ,[Fah_TransactionSQL] = @Fah_TransactionSQL
                                              ,[Fah_ComID] = @Fah_ComID
                                              ,[Fah_ReverseHeadID] = @Fah_ReverseHeadID
                                              ,[Fah_Category] = @Fah_Category
                                              ,[Fah_IsOpeningUpdate] = 0
                                         WHERE Fah_ID=@id";
                        db.CreateParameters(25);
                        db.AddParameters(0, "@Fah_ParentID", this.ParentId);
                        db.AddParameters(1, "@Fah_FagID", this.AccountGroupId);
                        db.AddParameters(2, "@Fah_Name", this.Name);
                        db.AddParameters(3, "@Fah_Address", this.Address);
                        db.AddParameters(4, "@Fah_ContactPerson", this.ContactPerson);
                        db.AddParameters(5, "@Fah_Description", this.Description);
                        db.AddParameters(6, "@Fah_OpeningBal", this.OpeningBalance);
                        db.AddParameters(7, "@Fah_IsDebit", this.IsDebit);
                        db.AddParameters(8, "@Fah_OpeningDate", this.OpeningDate);
                        db.AddParameters(9, "@Fah_Phone", this.Phone);
                        db.AddParameters(10, "@Fah_Email", this.Email);
                        db.AddParameters(11, "@Fah_Nature", this.AccountNature);
                        db.AddParameters(12, "@Fah_Disable", this.status);
                        db.AddParameters(13, "@Fah_Type", this.AccountType);
                        db.AddParameters(14, "@Fah_ComID", this.CompanyID);
                        db.AddParameters(15, "@Fah_Category", this.Category);
                        db.AddParameters(16, "@Fah_ReverseHeadID", this.ReverseHeadId);
                        db.AddParameters(17, "@Fah_CurSysUser", this.ModifiedBy);
                        db.AddParameters(18, "@id", this.ID);
                        if (this.DataSQL == "")
                        {
                            db.AddParameters(19, "@Fah_DataSQL", null);
                        }
                        else
                        {
                            db.AddParameters(19, "@Fah_DataSQL", this.DataSQL);
                        }
                        if (this.AmountSQL == "")
                        {
                            db.AddParameters(20, "@Fah_AmountSQL", null);
                        }
                        else
                        {
                            db.AddParameters(20, "@Fah_AmountSQL", this.AmountSQL);
                        }
                        if (this.SQLTable == "")
                        {
                            db.AddParameters(21, "@Fah_SQLTable", null);
                        }
                        else
                        {
                            db.AddParameters(21, "@Fah_SQLTable", this.SQLTable);
                        }
                        if (this.SQLID == "")
                        {
                            db.AddParameters(22, "@Fah_SQLID", null);
                        }
                        else
                        {
                            db.AddParameters(22, "@Fah_SQLID", this.SQLID);
                        }
                        if (this.SQLName == "")
                        {
                            db.AddParameters(23, "@Fah_SQLName", null);
                        }
                        else
                        {
                            db.AddParameters(23, "@Fah_SQLName", this.SQLName);
                        }
                        if (this.TransactionSQL == "")
                        {
                            db.AddParameters(24, "@Fah_TransactionSQL", null);
                        }
                        else
                        {
                            db.AddParameters(24, "@Fah_TransactionSQL", this.TransactionSQL);
                        }
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Updated Successfully", true, Type.NoError, "AccountHeadMaster | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Finance head could not be updated", false, Type.Others, "AccountHeadMaster | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Finance head could not updated", false, Type.Others, "AccountHeadMaster | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }
                }
            }
        }
        public static dynamic GetGroupData(int GroupId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                DataTable AccountGroupdata = db.ExecuteDataSet(CommandType.Text, @"Select top 1 Fah_ID AccountHeadID,Fah_ParentID ParentID,Fah_FagID AccountGroup_Id,Fah_Name Name,Fah_Address [Address],Fah_ContactPerson ContactPerson,Fah_Description [Description],Fah_Email Email,Fah_OpeningBal OpeningBal,Fah_IsDebit IsDebit,Fah_OpeningDate OpeningDate,Fah_Phone Phone,Fah_Nature Nature,Fah_Disable [Status],Fah_Type [Type],Fah_ReverseHeadID ReverseHeadID,Fah_Category Category,Fah_DataSQL DataSQL,Fah_AmountSQL AmountSQL,Fah_SQLTable SQLTable,Fah_SQLID SQLID,Fah_SQLName SQLName,Fah_TransactionSQL TransactionSQL from tbl_Fin_AccountHead where Fah_ID=" + GroupId).Tables[0];
                dynamic Account = new ExpandoObject();
                Account.ID = AccountGroupdata.Rows[0]["AccountHeadID"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata.Rows[0]["AccountHeadID"]) : 0;
                Account.Name = Convert.ToString(AccountGroupdata.Rows[0]["Name"]);
                Account.Description = Convert.ToString(AccountGroupdata.Rows[0]["Description"]);
                Account.AccountType = Convert.ToInt32(AccountGroupdata.Rows[0]["Type"]);
                Account.ParentId = Convert.ToInt32(AccountGroupdata.Rows[0]["ParentID"]);
                Account.Address = Convert.ToString(AccountGroupdata.Rows[0]["Address"]);
                Account.ContactPerson = Convert.ToString(AccountGroupdata.Rows[0]["ContactPerson"]);
                Account.OpeningBalance = Math.Round(Convert.ToDecimal(AccountGroupdata.Rows[0]["OpeningBal"]));
                Account.IsDebit = Convert.ToInt32(AccountGroupdata.Rows[0]["IsDebit"]);
                Account.OpeningDate = Convert.ToDateTime(AccountGroupdata.Rows[0]["OpeningDate"]).ToString("dd/MMM/yyyy");
                Account.datestring = Convert.ToDateTime(AccountGroupdata.Rows[0]["OpeningDate"]).ToString("dd/MMM/yyyy");
                Account.Phone = Convert.ToString(AccountGroupdata.Rows[0]["Phone"]);
                Account.AccountNature = Convert.ToInt32(AccountGroupdata.Rows[0]["Nature"]);
                Account.ReverseHeadId = Convert.ToInt32(AccountGroupdata.Rows[0]["ReverseHeadID"]);
                Account.Category = Convert.ToInt32(AccountGroupdata.Rows[0]["Category"]);
                Account.Email = Convert.ToString(AccountGroupdata.Rows[0]["Email"]);
                Account.DataSQL = Convert.ToString(AccountGroupdata.Rows[0]["DataSQL"]);
                Account.AmountSQL = Convert.ToString(AccountGroupdata.Rows[0]["AmountSQL"]);
                Account.SQLTable = Convert.ToString(AccountGroupdata.Rows[0]["SQLTable"]);
                Account.SQLID = Convert.ToString(AccountGroupdata.Rows[0]["SQLID"]);
                Account.SQLName = Convert.ToString(AccountGroupdata.Rows[0]["SQLName"]);
                Account.TransactionSQL = Convert.ToString(AccountGroupdata.Rows[0]["TransactionSQL"]);
                Account.status = AccountGroupdata.Rows[0]["Status"] != DBNull.Value ? Convert.ToInt32(AccountGroupdata.Rows[0]["Status"]) : 0;
                Account.AccountGroupId = Convert.ToString(AccountGroupdata.Rows[0]["AccountGroup_Id"]);
                return Account;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountHeadMaster | GetGroupData(int GroupId)");
                return null;
            }
        }
        /// <summary>
        /// Function to Load the Drop Downlist
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static DataTable GetAccountHeads(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fah_ID ID,Fah_Name Name from tbl_Fin_AccountHead where Fah_ComID="+CompanyId).Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountHeadMaster | GetAccountHeads(int CompanyId)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// To Get the Account Groups in the DropDownList
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static DataTable GetAccountGroup(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select  Fag_ID AccountGroup_Id,Fag_Name Name from tbl_Fin_AccountGroup where Fag_ComID="+CompanyId).Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountHeadMaster | GetAccountGroup(int CompanyId)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }
        public TreeNode GetTree(int Company)
        {
            try
            {
                DBManager db = new DBManager();
                DataSet ds = new DataSet();
                string SQLString = "", strGroupId = "";
                SQLString = @"  declare @ExpenseOnly int=0
                            
                            CREATE TABLE #tblAccountHead
                                        	(
                                        	GroupID			INT,
                                        	GroupName		VARCHAR(100),
                                        	GroupParentID	INT,
                                        	AccountID		INT,
                                        	AccountName		VARCHAR(100),
                                        	AccountDepth	INT
                                        	)
                                        
                                        CREATE TABLE #tblAccountGroup
                                        	(
                                        	GrpID			INT,
                                        	GrpName			VARCHAR(100),
                                        	GrpParentID		INT,
                                        	GrpDepth		INT
                                        	)
                                        
                                        DECLARE @IncGrp		INT
                                        
                                        IF @ExpenseOnly = 0
                                        	BEGIN
                                        		INSERT INTO #tblAccountHead 
                                        				SELECT Fag_ID, Fag_Name, Fag_ParentID, Fah_ID, Fah_Name, Fah_Depth 
                                        				FROM tbl_Fin_AccountHead INNER JOIN tbl_Fin_AccountGroup ON Fag_ID = Fah_FagID AND Fag_ComID = @CompanyID
                                        	END
                                        ELSE
                                        	BEGIN
                                        		INSERT INTO #tblAccountHead 
                                        				SELECT Fag_ID, Fag_Name, Fag_ParentID, Fah_ID, Fah_Name, Fah_Depth 
                                        				FROM tbl_Fin_AccountHead INNER JOIN tbl_Fin_AccountGroup ON Fag_ID = Fah_FagID AND Fah_Nature = 2 AND Fag_ComID = @CompanyID
                                        	END
                                        
                                        IF @ExpenseOnly = 0
                                        	BEGIN
                                        		DECLARE IncludeGroupCursor CURSOR FOR 
                                        				SELECT Det.Fag_ID FROM tbl_Fin_AccountGroup Mst INNER JOIN tbl_Fin_AccountGroup Det 
                                        				ON Mst.Fag_ID = Det.Fag_ParentID WHERE Det.Fag_ID IN (SELECT Fah_FagID FROM tbl_Fin_AccountHead) AND 
                                        				Det.Fag_ParentID NOT IN (SELECT Fah_FagID FROM tbl_Fin_AccountHead)
                                        				AND Mst.Fag_ComID = @CompanyID
                                        	END
                                        ELSE
                                        	BEGIN
                                        		DECLARE IncludeGroupCursor CURSOR FOR 
                                        				SELECT Det.Fag_ID FROM tbl_Fin_AccountGroup Mst INNER JOIN tbl_Fin_AccountGroup Det 
                                        				ON Mst.Fag_ID = Det.Fag_ParentID WHERE Det.Fag_ID IN (SELECT Fah_FagID FROM tbl_Fin_AccountHead WHERE Fah_Nature = 2) AND 
                                        				Det.Fag_ParentID NOT IN (SELECT Fah_FagID FROM tbl_Fin_AccountHead WHERE Fah_Nature = 2)
                                        				AND Mst.Fag_ComID = @CompanyID
                                        	END
                                        
                                        OPEN IncludeGroupCursor FETCH NEXT FROM IncludeGroupCursor INTO @IncGrp
                                        WHILE @@FETCH_STATUS =0
                                        	BEGIN
                                        		INSERT INTO #tblAccountGroup SELECT GroupID, GroupName, ParentID, Depth FROM UDF_Fin_ListParentGroup(@IncGrp)
                                        		FETCH NEXT FROM IncludeGroupCursor INTO @IncGrp
                                        	END
                                        CLOSE IncludeGroupCursor
                                        DEALLOCATE IncludeGroupCursor
                                        
                                        DELETE FROM #tblAccountGroup WHERE GrpID IN (SELECT DISTINCT GroupID FROM #tblAccountHead )
                                        
                                        SELECT GroupID, GroupName, GroupParentID, AccountID, AccountName, AccountDepth FROM #tblAccountHead
                                        UNION
                                        SELECT GrpID, GrpName, GrpParentID, NULL, NULL, GrpDepth FROM #tblAccountGroup
                                        	ORDER BY GroupParentID, GroupName, AccountDepth, AccountName";
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@CompanyID", Company);
                ds = db.ExecuteDataSet(CommandType.Text, SQLString);
                TreeNode node = new TreeNode();
                TreeNode childNode = new TreeNode();
                TreeNode subChildNode = new TreeNode();
                TreeNode subChildNode1 = new TreeNode();
                TreeNode mainNode = new TreeNode("Account Head", "");
                //mainNode.ToolTip = "Account Head [" + pnlCompanyName.Text + "]";
                DataRow[] rows = ds.Tables[0].Select("0 = 0");
                if (rows.Length > 0)
                {
                    foreach (DataRow singleRow in rows)
                    {
                        if (strGroupId != Convert.ToString(singleRow["GroupID"]) && Convert.ToString(singleRow["GroupParentID"]) == "0")
                        {
                            node = new TreeNode(Convert.ToString(singleRow["groupName"]), Convert.ToString(singleRow["GroupID"]));
                            strGroupId = Convert.ToString(singleRow["GroupID"]);
                            node.ToolTip = Convert.ToString(singleRow["groupName"]);
                            mainNode.ChildNodes.Add(node);
                            DataRow[] rowsSub = ds.Tables[0].Select("GroupParentID = " + Convert.ToString(singleRow["GroupID"]));
                            if (rowsSub.Length > 0)
                            {
                                string strSubGroupId = "";
                                foreach (DataRow singleRowSub in rowsSub)
                                {
                                    if (strSubGroupId != Convert.ToString(singleRowSub["GroupID"]))
                                    {
                                        childNode = new TreeNode(Convert.ToString(singleRowSub["groupName"]), Convert.ToString(singleRowSub["GroupID"]));
                                        childNode.ToolTip = Convert.ToString(singleRowSub["groupName"]);
                                        node.ChildNodes.Add(childNode);
                                        strSubGroupId = Convert.ToString(singleRowSub["GroupID"]);
                                        DataRow[] rowsSubChild = ds.Tables[0].Select("GroupParentID = " + Convert.ToString(singleRowSub["GroupID"]));
                                        if (rowsSubChild.Length > 0)
                                        {
                                            string strSubChildGroupId = "";
                                            foreach (DataRow singleRowSubChild in rowsSubChild)
                                            {
                                                if (strSubChildGroupId != Convert.ToString(singleRowSubChild["GroupID"]))
                                                {
                                                    subChildNode = new TreeNode(Convert.ToString(singleRowSubChild["groupName"]), Convert.ToString(singleRowSubChild["GroupID"]));
                                                    subChildNode.ToolTip = Convert.ToString(singleRowSubChild["groupName"]);
                                                    childNode.ChildNodes.Add(subChildNode);
                                                    strSubChildGroupId = Convert.ToString(singleRowSubChild["GroupID"]);

                                                    DataRow[] rowsSubDetails1 = ds.Tables[0].Select("GroupId = " + Convert.ToString(singleRowSubChild["GroupId"]));
                                                    foreach (DataRow singleRowSubDetail1 in rowsSubDetails1)
                                                    {
                                                        subChildNode1 = new TreeNode(Convert.ToString(singleRowSubDetail1["AccountName"]), Convert.ToString(singleRowSubDetail1["AccountID"]));
                                                        subChildNode1.ToolTip = Convert.ToString(singleRowSubDetail1["AccountName"]);
                                                        subChildNode.ChildNodes.Add(subChildNode1);
                                                    }
                                                }
                                            }
                                        }
                                        DataRow[] rowsSubDetails = ds.Tables[0].Select("GroupId = " + Convert.ToString(singleRowSub["GroupId"]));
                                        foreach (DataRow singleRowSubDetail in rowsSubDetails)
                                        {
                                            subChildNode = new TreeNode(Convert.ToString(singleRowSubDetail["AccountName"]), Convert.ToString(singleRowSubDetail["AccountID"]));
                                            subChildNode.ToolTip = Convert.ToString(singleRowSubDetail["AccountName"]);
                                            childNode.ChildNodes.Add(subChildNode);
                                        }
                                    }
                                }
                            }
                            DataRow[] rowsDetails = ds.Tables[0].Select("GroupId = " + Convert.ToString(singleRow["GroupId"]));
                            foreach (DataRow singleRowDetail in rowsDetails)
                            {
                                childNode = new TreeNode(Convert.ToString(singleRowDetail["AccountName"]), Convert.ToString(singleRowDetail["AccountID"]));
                                childNode.ToolTip = Convert.ToString(singleRowDetail["AccountName"]);
                                node.ChildNodes.Add(childNode);
                            }
                            //treeAccountGroup.Nodes.Add(node);
                        }
                    }
                }
                return mainNode;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountHeadMaster | GetTree(int Company)");
                return null;
            }
        }
        public static int getNature(int id)
        {
            try
            {
                DBManager db = new DBManager();
                int Type = 0;
                string sql = @"select fag_Type from tbl_fin_accountgroup where fag_id=" + id;
                db.Open();
                Type = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, sql));
                return Type;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountHeadMaster | getNature(int id)");
                return 0;
            }
        }

        public static dynamic GetHeadTree(int CompanyID)
        {
            try
            {
                DBManager db = new DBManager();
                db.Open();
                DataTable ds = new DataTable();
                string SQLString = "";
                List<object> obj = new List<object>();
                //treeAccountGroup.Nodes.Clear();
                SQLString = @"select Fah_ID,Fah_Name,Fah_FagID,b.Fag_ParentID,b.Fag_ID,b.Fag_Name,isnull(c.Fag_Name,'') parent,isnull(c.Fag_ID,0) parentID from tbl_Fin_AccountHead a inner join tbl_Fin_AccountGroup b on a.Fah_FagID=b.Fag_ID
                                left join tbl_Fin_AccountGroup c on c.Fag_ID=b.Fag_ParentID where Fah_ComID=" + CompanyID;
                ds = db.ExecuteQuery(CommandType.Text, SQLString);
                if (ds!=null)
                {
                    List<object> HeadTree = new List<object>();
                    for (int i = 0; i < ds.Rows.Count;)
                    {
                        dynamic HeadData = new ExpandoObject();
                        DataRow row = ds.Rows[i];
                        HeadData.parent = row["parent"] != DBNull.Value ? Convert.ToString(row["parent"]) : "";
                        HeadData.parentID= row["parentID"] != DBNull.Value ? Convert.ToInt32(row["parentID"]) : 0;
                        HeadData.Head_ParentID= row["Fah_FagID"] != DBNull.Value ? Convert.ToInt32(row["Fah_FagID"]) : 0;
                        HeadData.Fag_Name = row["Fag_Name"] != DBNull.Value ? Convert.ToString(row["Fag_Name"]) : "";
                        DataTable inProducts = ds.AsEnumerable().Where(x => x.Field<int>("Fah_FagID") == HeadData.Head_ParentID).CopyToDataTable();
                        List<object> Child = new List<object>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            dynamic ChildData = new ExpandoObject();
                            ChildData.GroupID= rowItem["Fag_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Fag_ID"]) : 0;
                            ChildData.GroupName = rowItem["Fag_Name"] != DBNull.Value ? Convert.ToString(rowItem["Fag_Name"]) : "";
                            ChildData.HeadID= rowItem["Fah_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Fah_ID"]) : 0;
                            ChildData.HeadName= rowItem["Fah_Name"] != DBNull.Value ? Convert.ToString(rowItem["Fah_Name"]) : "";
                            ChildData.ParentID= rowItem["Fah_FagID"] != DBNull.Value ? Convert.ToInt32(rowItem["Fah_FagID"]) : 0;
                            DataTable ChildHeads = ds.AsEnumerable().Where(x => x.Field<int>("Fah_FagID") == ChildData.ParentID).CopyToDataTable();
                            List<object> headNodes = new List<object>();
                            for (int k = 0; k < ChildHeads.Rows.Count; k++)
                            {
                                dynamic Head = new ExpandoObject();
                                Head.HeadID = rowItem["Fah_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Fah_ID"]) : 0;
                                Head.HeadName = rowItem["Fah_Name"] != DBNull.Value ? Convert.ToString(rowItem["Fah_Name"]) : "";
                                Head.ParentID = rowItem["Fah_FagID"] != DBNull.Value ? Convert.ToInt32(rowItem["Fah_FagID"]) : 0;
                                ChildData.ChildHead = Head;
                                ds.Rows.RemoveAt(0);
                            }
                            //ChildData.
                            Child.Add(ChildData);
                        }
                        HeadData.Chidren = Child;
                        HeadTree.Add(HeadData);
                    }
                    return HeadTree;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountHeadMaster | GetHeadTree(int CompanyID)");
                return null;
            }
        }

        private static dynamic GetChildModules(DataRow parent, dynamic group)
        {
            dynamic childModules = new List<dynamic>();
            if (parent.GetChildRows("children").Length > 0)
            {
                DataRow[] children = parent.GetChildRows("children");
                foreach (DataRow item in children)
                {
                    dynamic mod = new ExpandoObject();
                    mod.HeadName = item["Fah_Name"].ToString();
                    mod.HeadID = Convert.ToInt32(item["Fah_ID"]);
                    if (item.GetChildRows("children").Length > 0)
                    {
                        mod.HasChildren = true;
                        mod.Children = GetChildModules(item, mod);
                    }
                    else
                    {
                        mod.HasChildren = false;
                    }
                    ((List<dynamic>)(childModules)).Add(mod);
                }
            }
            return childModules;
        }

        public DataTable GetAccountHeadsVoucher(int VoucherType,int Credit,int Company)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    if (Credit==1)
                    {
                        string sql = @"declare @i int=1
                                   declare @count int
                                   declare @id int
                                   create table #tempnew(name varchar(100),parent int,id int)
                                   create table #temptable(name varchar(100),id int,parent int)
                                   declare @sql varchar(100)
                                   set @count=(select Count(*) from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and vtl_allowCr=1)
                                   while @i<=@count
                                   begin
                                   ;with cte as(
                                   --select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null 
								   select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and vtl_allowCr=1 and fah_comID=3)
                                   select @id=Fah_ID from cte where Slno=@i
                                   set @sql=(select Fah_DataSQL from tbl_Fin_AccountHead where fah_id=@id)
                                    insert into #tempnew (name,id) exec(@sql)
                                    update #tempnew set parent=@id
                                    insert into #temptable select * from #tempnew
                                    truncate table #tempnew
                                    set @i=@i+1
                                    end
									select fah_name Name,fah_id parent,0 ID from tbl_Fin_AccountHead a inner join tbl_fin_voucherTypelink b on a.fah_id=b.Vtl_fahID where Fah_DataSQL is null and Fah_FagID!=45 and vtl_FvtID=@voucher and vtl_allowCR=1
                                    --select fah_name Name,fah_id ID,0 parent from tbl_Fin_AccountHead where Fah_DataSQL is null and Fah_FagID!=45
                                    union all
                                    select * from #temptable";
                        db.CreateParameters(2);
                        db.AddParameters(0, "@voucher", VoucherType);
                        db.AddParameters(1, "@company", Company);
                        dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                        return dt;
                    }
                    else
                    {
                        string sql = @"declare @i int=1
                                   declare @count int
                                   declare @id int
                                   create table #tempnew(name varchar(100),parent int,id int)
                                   create table #temptable(name varchar(100),id int,parent int)
                                   declare @sql varchar(100)
                                   set @count=(select Count(*) from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and Vtl_AllowDr=1)
                                   while @i<=@count
                                   begin
                                   ;with cte as(
                                   --select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null 
								   select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and Vtl_AllowDr=1 and fah_comID=3)
                                   select @id=Fah_ID from cte where Slno=@i
                                   set @sql=(select Fah_DataSQL from tbl_Fin_AccountHead where fah_id=@id)
                                    insert into #tempnew (name,id) exec(@sql)
                                    update #tempnew set parent=@id
                                    insert into #temptable select * from #tempnew
                                    truncate table #tempnew
                                    set @i=@i+1
                                    end
									select fah_name Name,fah_id parent,0 ID from tbl_Fin_AccountHead a inner join tbl_fin_voucherTypelink b on a.fah_id=b.Vtl_fahID where Fah_DataSQL is null and Fah_FagID!=45 and vtl_FvtID=@voucher and Vtl_AllowDr=1
                                    --select fah_name Name,fah_id ID,0 parent from tbl_Fin_AccountHead where Fah_DataSQL is null and Fah_FagID!=45
                                    union all
                                    select * from #temptable";
                        db.CreateParameters(2);
                        db.AddParameters(0, "@voucher", VoucherType);
                        db.AddParameters(1, "@company", Company);
                        dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                        return dt;
                    }
                    
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountHeadMaster | GetAccountHeadsVoucher(int VoucherType,int Company)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static DataTable GetAccountHeadsVoucher(int VoucherType, int Company)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    
                        string sql = @"declare @i int=1
                                   declare @count int
                                   declare @id int
                                   create table #tempnew(name varchar(100),parent int,id int)
                                   create table #temptable(name varchar(100),id int,parent int)
                                   declare @sql varchar(100)
                                   set @count=(select Count(*) from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and vtl_allowCr=1)
                                   while @i<=@count
                                   begin
                                   ;with cte as(
                                   --select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null 
								   select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead a inner join tbl_fin_vouchertypelink b on a.fah_id=b.Vtl_FahID where Fah_DataSQL is not null and b.vtl_FvtID=@voucher and vtl_allowCr=1 and fah_comID=3)
                                   select @id=Fah_ID from cte where Slno=@i
                                   set @sql=(select Fah_DataSQL from tbl_Fin_AccountHead where fah_id=@id)
                                    insert into #tempnew (name,id) exec(@sql)
                                    update #tempnew set parent=@id
                                    insert into #temptable select * from #tempnew
                                    truncate table #tempnew
                                    set @i=@i+1
                                    end
									select fah_name Name,fah_id parent,0 ID from tbl_Fin_AccountHead a inner join tbl_fin_voucherTypelink b on a.fah_id=b.Vtl_fahID where Fah_DataSQL is null and Fah_FagID!=45 and vtl_FvtID=@voucher and vtl_allowCR=1
                                    --select fah_name Name,fah_id ID,0 parent from tbl_Fin_AccountHead where Fah_DataSQL is null and Fah_FagID!=45
                                    union all
                                    select * from #temptable";
                        db.CreateParameters(2);
                        db.AddParameters(0, "@voucher", VoucherType);
                        db.AddParameters(1, "@company", Company);
                        dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                        return dt;
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountHeadMaster | GetAccountHeadsVoucher(int VoucherType,int Company)");
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
