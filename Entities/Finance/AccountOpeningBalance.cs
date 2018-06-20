using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Entities.Finance
{
    public class AccountOpeningBalance
    {
        #region Properties
        public int ID { get; set; }
        public string SQLtable { get; set; }
        public string SQLID { get; set; } 
        public string SQLName { get; set; }
        public int AccountHeadID { get; set; }
        public int ChildheadID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime OpeningDate { get; set; }
        public string Datestring { get; set; }
        public decimal Balance { get; set; } 
        public int isDebit { get; set; }
        public int ModifiedBy { get; set; }
        #endregion

        public static void LoadAccountHeadsForOpening(DropDownList ddl)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    dt = db.ExecuteQuery(CommandType.Text, "select Fah_ID,Fah_Name from tbl_Fin_AccountHead where Fah_DataSQL is not null ORDER BY Fah_Name");
                    ddl.DataSource = dt;
                    ddl.DataTextField = "Fah_Name";
                    ddl.DataValueField = "Fah_ID";
                    ddl.DataBind();
                    ddl.Items.Add(new ListItem("--Select--", "0"));
                    ddl.SelectedValue = "0";
                    //return db.ExecuteDataSet(CommandType.Text, "select Fah_ID,Fah_Name from tbl_Fin_AccountHead where Fah_DataSQL is not null ORDER BY Fah_Name").Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountOpeningBalance | LoadAccountHeadsForOpening(DropDownList ddl)");
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static DataTable GetDetails()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select a.Fob_ID ID,b.Fah_Name Name,a.Fob_OpenBal OpenBalance,convert(varchar,Fob_OpenDate,103) OpeningDate from tbl_Fin_AccountOpeningBalance a inner join tbl_Fin_AccountHead b on a.Fob_ChildID=b.Fah_ID").Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountOpeningBalance | GetDetails()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static AccountOpeningBalance GetDetails(int ItemID,int flag)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                DataTable dt = db.ExecuteDataSet(CommandType.Text, @"select TOP 1 Fob_ID ID,Fob_IsDebit,Fob_ChildID ChildID,Fob_OpenBal Balance,Fob_SQLTable [Table],Convert(varchar,Fob_OpenDate,103) OpenDate,Fah_ID HeadID from tbl_Fin_AccountOpeningBalance a inner join tbl_Fin_AccountHead b on a.Fob_SQLTable=b.Fah_SQLTable where Fob_ID=" + ItemID).Tables[0];
                DataRow AccountOpening = dt.Rows[0];
                AccountOpeningBalance Account = new AccountOpeningBalance();
                Account.ID = AccountOpening["ID"] != DBNull.Value ? Convert.ToInt32(AccountOpening["ID"]) : 0;
                Account.SQLtable = Convert.ToString(AccountOpening["Table"]);
                Account.OpeningDate = Convert.ToDateTime(AccountOpening["OpenDate"]);
                Account.Datestring= AccountOpening["OpenDate"] != DBNull.Value ? Convert.ToDateTime(AccountOpening["OpenDate"]).ToString("dd/MMM/yyyy") : string.Empty;
                Account.AccountHeadID = Convert.ToInt32(AccountOpening["HeadID"]);
                Account.ChildheadID = Convert.ToInt32(AccountOpening["ChildID"]);
                Account.Balance = Convert.ToDecimal(AccountOpening["Balance"]);
                Account.isDebit = Convert.ToInt32(AccountOpening["Fob_IsDebit"]);

                return Account;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountOpeningBalance | GetDetails(int ItemID,int flag)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        public static DataTable GetDetails(int ItemID)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@newID", ItemID);
                string sql1 = @" declare @sqltable varchar(100)
                                 declare @chldID varchar(100)
                                 declare @id varchar(10)
                                 declare @childhead varchar(100)
                                 declare @head varchar(100)
                                 declare @sql varchar(max)
                                 set @id=@newID
                                 set @childhead = (select top 1 b.Fah_SQLID from tbl_Fin_AccountOpeningBalance a inner join tbl_Fin_AccountHead b on a.Fob_FahID = b.Fah_ID where Fob_FahID = @id)
                                 set @sqltable = (select top 1 Fob_SQLTable from tbl_Fin_AccountOpeningBalance where Fob_FahID = @id)
                                 set @chldID = (select top 1 Fob_ChildID from tbl_Fin_AccountOpeningBalance where Fob_FahID = @id)
                                 set @head = (select top 1 b.Fah_SQLName from tbl_Fin_AccountOpeningBalance a inner join tbl_Fin_AccountHead b on a.Fob_FahID = b.Fah_ID where Fob_FahID = @id)
                                 set @sql = 'select a.Fob_ID ID,a.Fob_OpenBal OpenBalance,convert(varchar,Fob_OpenDate,103) OpeningDate,b.' + @head + ' Name from tbl_Fin_AccountOpeningBalance a inner join ' + @sqltable + ' b on a.Fob_ChildID=b.' + @childhead + ' where Fob_FahID='+@id
                                 select @sql cmd ";
                DataTable dt = db.ExecuteQuery(CommandType.Text, sql1);
                string sql = dt.Rows[0][0].ToString();
                db.CleanupParameters();
                dt = db.ExecuteQuery(CommandType.Text, sql);
                return dt;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "AccountOpeningBalance | GetDetails(int ItemID)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        public string LoadAccountChildHeads(DropDownList ddl,int Selected)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    DataSet ds = new DataSet();
                    string cmd = "SELECT Fah_DataSQL, Fah_SQLTable, Fah_SQLID, Fah_SQLName FROM tbl_Fin_AccountHead WHERE Fah_ID =" + Selected;
                    dt = db.ExecuteQuery(CommandType.Text, cmd);
                    this.SQLtable = dt.Rows[0][1].ToString();
                    this.SQLName = dt.Rows[0][2].ToString();
                    if (dt.Rows.Count>0)
                    {
                        cmd = dt.Rows[0][0].ToString();
                        ds = db.ExecuteDataSet(CommandType.Text, cmd);
                        ddl.Items.Clear();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow row;
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                row = ds.Tables[0].Rows[i];
                                ddl.Items.Add(new ListItem(Convert.ToString(row[0]), Convert.ToString(row[1])));
                            }
                        }
                        ddl.Items.Add(new ListItem("--Select--", "0"));
                        ddl.SelectedValue = "0";

                        return this.SQLtable;
                    }
                    else
                    {
                        return null;
                    }
                    //return db.ExecuteDataSet(CommandType.Text, "select Fah_ID,Fah_Name from tbl_Fin_AccountHead where Fah_DataSQL is not null ORDER BY Fah_Name").Tables[0];
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "AccountOpeningBalance | LoadAccountChildHeads(DropDownList ddl,int Selected)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.OpeningBalance, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "OpeningBalance | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"INSERT INTO tbl_Fin_AccountOpeningBalance   
                                          (Fob_FahID, Fob_SQLTable, Fob_ChildID, Fob_OpenBal, Fob_OpenDate, Fob_IsDebit,Fob_CurSysUser)   
                                          VALUES (@p_Fob_FahID, @p_Fob_SQLTable, @p_Fob_ChildID, @p_Fob_OpenBal,@p_Fob_OpenDate,  
                                          @p_Fob_IsDebit,@p_Fob_CurSysUser)";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@p_Fob_FahID", this.AccountHeadID);
                        db.AddParameters(1, "@p_Fob_SQLTable", this.SQLtable);
                        db.AddParameters(2, "@p_Fob_ChildID", this.ChildheadID);
                        db.AddParameters(3, "@p_Fob_OpenBal", this.Balance);
                        db.AddParameters(4, "@p_Fob_OpenDate", this.OpeningDate);
                        db.AddParameters(5, "@p_Fob_IsDebit", this.isDebit);
                        db.AddParameters(6, "@p_Fob_CurSysUser", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage(" Opening balance saved successfully", true, Type.NoError, "OpeningBalance | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Opening balance could not be saved ", false, Type.Others, "OpeningBalance | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Opening balance could not be saved", false, Type.Others, "OpeningBalance | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.OpeningBalance, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "OpeningBalance | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "OpeningBalance | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"UPDATE tbl_Fin_AccountOpeningBalance SET Fob_OpenBal = @p_Fob_OpenBal,  
                                          Fob_OpenDate = @p_Fob_OpenDate, Fob_IsDebit = @p_Fob_IsDebit   
                                          WHERE Fob_ID = @p_Fob_ID ";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@p_Fob_OpenBal", this.Balance);
                        db.AddParameters(1, "@p_Fob_OpenDate", this.OpeningDate);
                        db.AddParameters(2, "@p_Fob_IsDebit", this.isDebit);
                        db.AddParameters(3, "@p_Fob_ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Opening balance updated successfully", true, Type.NoError, "OpeningBalance | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Opening balance could not be updated", false, Type.Others, "OpeningBalance | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Opening balance could not be updated", false, Type.Others, "OpeningBalance | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.OpeningBalance, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "OpeningBalance | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from tbl_Fin_AccountOpeningBalance where Fob_ID=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Opening balance deleted successfully", true, Type.NoError, "OpeningBalance | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Opening balance could not be deleted", false, Type.Others, "OpeningBalance | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "OpeningBalance | Delete", System.Net.HttpStatusCode.InternalServerError);

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
    }
}
