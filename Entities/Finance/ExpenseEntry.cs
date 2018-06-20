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
    public class ExpenseEntry
    {
        #region Properties
        public int ID { get; set; }
        public int CreatedBy { get; set; }
        public string Description { get; set; }
        public int VoucherTypeID { get; set; }
        public int Number { get; set; }
        public int AccountGroupID { get; set; }
        public int IsVoucher { get; set; }
        public int Frm_TransID { get; set; }
        public int FrmTransChildID { get; set; }
        public int IncomeDesc { get; set; }
        public int ToTransID { get; set; }
        public int ToTransChildID { get; set; }
        public int ExpenseDesc { get; set; }
        public int IsCheque { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string Fve_CurSysUser { get; set; }
        public int FccID { get; set; }
        public int IsCleared { get; set; }
        public DateTime ChequeClearDate { get; set; }
        public string Drawon { get; set; }
        public int ReceiptNo { get; set; }
        public string Fve_UTR { get; set; }
        public Boolean Isbounce { get; set; }
        public string Date { get; set; }
        public string username { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public string AccountHead { get; set; }
        public string AccountChild { get; set; }
        public string Amount { get; set; }
        public string CostCenter { get; set; }
        public int ModifiedBy { get; set; }
        #endregion
        /// <summary>
        /// Dataset For loading The save details
        /// </summary>
        /// <returns></returns>
        public DataSet CreateDataset()
        {
            try
            {
                DataSet dsTemp = new DataSet();
                DataTable dt = new DataTable("tempTable");
                DataColumn[] Keys = new DataColumn[1];
                DataColumn dc = new DataColumn("ID", System.Type.GetType("System.Int32"));
                dt.Columns.Add(dc);
                DataColumn dc4 = new DataColumn("ParticularsID", System.Type.GetType("System.String"));
                dt.Columns.Add(dc4);
                DataColumn dc1 = new DataColumn("Particulars", System.Type.GetType("System.String"));
                dt.Columns.Add(dc1);
                DataColumn dc2 = new DataColumn("DebitAmt", System.Type.GetType("System.String"));
                dt.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn("CreditAmt", System.Type.GetType("System.String"));
                dt.Columns.Add(dc3);
                DataColumn dc5 = new DataColumn("CreditOrDebit", System.Type.GetType("System.String"));
                dt.Columns.Add(dc5);
                DataColumn dc6 = new DataColumn("CostHead", System.Type.GetType("System.String"));
                dt.Columns.Add(dc6);
                DataColumn dc7 = new DataColumn("CostCenter", System.Type.GetType("System.String"));
                dt.Columns.Add(dc7);
                DataColumn dc8 = new DataColumn("Amount", System.Type.GetType("System.String"));
                dt.Columns.Add(dc8);
                dsTemp.Tables.Add(dt);
                dsTemp.AcceptChanges();
                dsTemp.Tables[0].Columns["ID"].AutoIncrement = true;
                dsTemp.Tables[0].Columns["ID"].AutoIncrementSeed = 1;
                dsTemp.Tables[0].PrimaryKey = Keys;
                return dsTemp;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ExpenseEntry | CreateDataset()");
                return null;
            }
        }
        /// <summary>
        /// Save Function
        /// </summary>
        /// <param name="StrVType">Loads The Voucher Type</param>
        /// <param name="StrAccID">Loads The Main Account ID</param>
        /// <param name="StrChdID">Loads The Child Id </param>
        /// <param name="StrAmount">Stores the net Amount of Transfer </param>
        /// <param name="StrCostCtr">Stores theCostCenter</param>
        /// <returns></returns>
        public OutputMessage Save(string StrVType, string StrAccID, string StrChdID, string StrAmount, string StrCostCtr)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherEntry, Security.PermissionTypes.Create))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else
                    {
                        db.Open();
                        string query = @" IF @UserName = NULL OR @UserName = ''      
                                      SET @UserName = suser_sname()      
                                          
                                     DECLARE @GroupID  INT,      
                                       @VNumbering  INT,      
                                       @VStartNo  INT,      
                                       @VReStart  INT,      
                                       @FDate   DATETIME,      
                                       @TDate   DATETIME,      
                                       @FStr   VARCHAR(20),      
                                       @TStr   VARCHAR(20),      
                                       @CCValue  VARCHAR(500),      
                                       @VEtyID   INT,      
                                       @CCtrID   INT,      
                                       @CCtrAmt  MONEY,      
                                       @RCnt   INT      
                                          
                                          
                                          
                                          
                                            
                                    -- Voucher Date Generation Settings      
                                          
                                          ---------------18/OCT/2012
                                    --        if @IsCheque=0
                                    --         begin
                                    --          set @ChequeDate=NULL
                                    --          set @ChequeNo=NULL
                                    ----            set @VoucherID=2
                                    --         end 
                                             ---------------------
                                    --      if @IsCheque<>0     ---------------21/Nov/2012
                                    --         begin
                                    --          set @VoucherID=5
                                    --         end 
                                          
                                     -----------------------------------------------------------Voucher Number Generation      
                                             
                                    --Voucher Type Settings       
                                          
                                     SELECT @VNumbering  = ISNULL(Fvt_Numbering,0),         
                                     @VStartNo  = ISNULL(Fvt_NoStatFrom,0),       
                                     @VReStart  = ISNULL(Fvt_RestartNo,0)       
                                     FROM tbl_Fin_VoucherType       
                                     WHERE Fvt_ID  = @VoucherID       
                                          
                                          
                                          
                                          
                                          
                                     IF @VNumbering = 1        -- Automatic numbering      
                                       BEGIN        
                                        IF @VReStart = 0     -- Restart by Year      
                                          BEGIN        
                                           SET @FStr = CONVERT(VARCHAR,YEAR(@VcrDate))        
                                            IF MONTH(@VcrDate)<4       
                                       SET @FStr = CONVERT(VARCHAR,YEAR(@VcrDate)-1)        
                                           SET @FStr = '01-APR-' + @FStr        
                                              
                                      SET @TStr = CONVERT(VARCHAR,YEAR(@VcrDate)+1)        
                                            IF MONTH(@VcrDate)<4       
                                       SET @TStr = CONVERT(VARCHAR,YEAR(@VcrDate))        
                                           SET @TStr = '31-MAR-' + @TStr        
                                            
                                       END        
                                      ELSE IF @VReStart = 1     -- Restart by Month      
                                         BEGIN        
                                          SET @FStr = '01-' +  CONVERT(VARCHAR,LEFT(DATENAME(MONTH,@VcrDate),3)) + '-' + CONVERT(VARCHAR,YEAR(@VcrDate))        
                                          SET @TStr =   CONVERT(VARCHAR,LEFT(DATENAME(MONTH,@VcrDate),3)) + '-' + CONVERT(VARCHAR,YEAR(@VcrDate))        
                                            
                                          IF MONTH(@VcrDate) = 2        
                                           BEGIN       
                                             IF (YEAR(@VcrDate) % 4) = 0       
                                       SET @TStr = '29-' + @TStr       
                                      ELSE       
                                       SET @TStr = '28-' + @TStr        
                                            END        
                                          ELSE IF MONTH(@VcrDate) = 4 OR MONTH(@VcrDate) = 6 OR MONTH(@VcrDate) = 9 OR MONTH(@VcrDate) = 11         
                                           BEGIN        
                                             SET @TStr = '30-' + @TStr         
                                           END        
                                          ELSE        
                                           BEGIN        
                                             SET @TStr = '31-' + @TStr         
                                           END        
                                      END      
                                          
                                         SET @FDate = CONVERT(SMALLDATETIME, @FStr)        
                                         SET @TDate = CONVERT(SMALLDATETIME, @TStr)        
                                                 
                                          
                                          
                                               
                                     SELECT  @VoucherNo = ISNULL(MAX(Fve_Number), 0)       
                                      FROM tbl_Fin_VoucherEntry         
                                           WHERE Fve_VoucherType = @VoucherID       
                                      AND (Fve_Date BETWEEN @FDate AND @TDate)        
                                            
                                          
                                          
                                          
                                          
                                          IF @VoucherNo = 0       
                                      SET @VoucherNo = @VStartNo       
                                     ELSE       
                                      SET @VoucherNo = @VoucherNo + 1        
                                         END        
                                           
                                          
                                    ----------------------------------------------------------------------------------------------------        
                                          
                                          
                                          
                                          
                                     DECLARE @tblVType TABLE(TID INT IDENTITY(1,1) NOT NULL, VType INT)      
                                     DECLARE @tblAccHeadID TABLE (AID INT IDENTITY(1,1) NOT NULL, AccHeadID INT)      
                                     DECLARE @tblChildHeadID TABLE (CID INT IDENTITY(1,1) NOT NULL, ChlHeadID VARCHAR(15))      
                                     DECLARE @tblAmount TABLE (MID INT IDENTITY(1,1) NOT NULL, AmountVal MONEY)      
                                     DECLARE @tblCostCenter TABLE (CCID INT IDENTITY(1,1) NOT NULL, CCValue VARCHAR(1000))      
                                          
                                     INSERT INTO @tblVType(VType) SELECT CONVERT(INT, Element) FROM SPLIT(@StrVType ,'|')      
                                     INSERT INTO @tblAccHeadID(AccHeadID) SELECT CONVERT(INT, Element) FROM SPLIT(@StrAccID ,'|')      
                                     INSERT INTO @tblChildHeadID(ChlHeadID) SELECT Element FROM SPLIT(@StrChdID, '|')      
                                     INSERT INTO @tblAmount(AmountVal) SELECT CONVERT(MONEY, Element) FROM SPLIT(@StrAmount, '|')      
                                     INSERT INTO @tblCostCenter(CCValue) SELECT Element FROM SPLIT(@StrCostCtr, '|')      
                                          
                                     SELECT @GroupID = ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry      
                                          
                                          
                                    
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate      
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID WHERE VType = 0 ORDER BY TID      
                                          
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                        Fve_ToTransID, Fve_ToTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate      
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID WHERE VType = 1 ORDER BY TID      
                                         
                                    -- UPDATING Cost Center Values      
                                     CREATE TABLE #TmpTableCostCenters      
                                       (      
                                       CenterValues VARCHAR(20)      
                                       )      
                                     CREATE TABLE #TmpTableEntry         
                                       (      
                                       ObjID  INT IDENTITY (1,1) NOT NULL,       
                                       CostCtID VARCHAR(10)      
                                       )      
                                          
                                     SET @RCnt = 0      
                                          
                                     DECLARE CostCenter CURSOR FOR SELECT Fve_ID, Fve_FccID FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @GroupID AND (Fve_FccID IS NOT NULL OR Fve_FccID <> '')      
                                     OPEN CostCenter FETCH NEXT FROM CostCenter INTO @VEtyID, @CCValue      
                                     WHILE @@FETCH_STATUS = 0      
                                      BEGIN      
                                       DELETE FROM #TmpTableCostCenters      
                                       INSERT INTO #TmpTableCostCenters SELECT Element FROM SPLIT(@CCValue, '^')      
                                             
                                       DECLARE CursorCCtrValues CURSOR FOR SELECT CenterValues FROM #TmpTableCostCenters      
                                       OPEN CursorCCtrValues FETCH NEXT FROM CursorCCtrValues INTO @CCValue      
                                       WHILE @@FETCH_STATUS = 0      
                                        BEGIN      
                                         IF @CCValue = '' GOTO GetNextCostCenterValue      
                                               
                                         DELETE FROM #TmpTableEntry      
                                         INSERT INTO #TmpTableEntry (CostCtID) SELECT Element FROM SPLIT(@CCValue, '`')      
                                               
                                         SET @RCnt = @RCnt + 1      
                                         SELECT @CCtrID = CONVERT(INT, CostCtID) FROM #TmpTableEntry WHERE ObjID = @RCnt      
                                         SET @RCnt = @RCnt + 1      
                                         SELECT @CCtrAmt = CONVERT(MONEY, CostCtID) FROM #TmpTableEntry WHERE ObjID = @RCnt      
                                          
                                         INSERT INTO [tbl_Fin_VoucherEntryCostCenter] (Fvc_FveID, Fvc_FccID, Fvc_Amount)      
                                          VALUES (@VEtyID, @CCtrID, @CCtrAmt)      
                                        GetNextCostCenterValue:      
                                         FETCH NEXT FROM CursorCCtrValues INTO @CCValue      
                                        END      
                                       CLOSE CursorCCtrValues      
                                       DEALLOCATE CursorCCtrValues      
                                          
                                       FETCH NEXT FROM CostCenter INTO @VEtyID, @CCValue      
                                      END      
                                     CLOSE CostCenter      
                                     DEALLOCATE CostCenter      
                                           
                                     -- Returning Group ID for Payment Entry <BKA : 23 Jul 2009>      
                                     SELECT @GroupID AS GroupID,@VoucherNo AS VoucherNo      
                                     -----------------------------------------------------------";
                        db.CreateParameters(16);
                        db.AddParameters(0, "@VcrDate", Convert.ToDateTime(this.Date));
                        db.AddParameters(1, "@UserName", this.username);
                        db.AddParameters(2, "@Narration", this.Description);
                        db.AddParameters(3, "@StrVType", StrVType);
                        db.AddParameters(4, "@StrAccID", StrAccID);
                        db.AddParameters(5, "@StrChdID", StrChdID);
                        db.AddParameters(6, "@StrAmount", StrAmount);
                        db.AddParameters(7, "@StrCostCtr", StrCostCtr);
                        db.AddParameters(8, "@IsCheque", this.IsCheque);
                        db.AddParameters(9, "@ChequeNo", this.ChequeNo);
                        db.AddParameters(10, "@ChequeDate", this.ChequeDate);
                        db.AddParameters(11, "@VoucherNo", this.VoucherNo);
                        db.AddParameters(12, "@VoucherID", this.VoucherTypeID);
                        db.AddParameters(13, "@IsVoucherEntry", 1);
                        db.AddParameters(14, "@Drawon", this.Drawon);
                        db.AddParameters(15, "@receiptNo", this.ReceiptNo);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Expense entry saved successfully", true, Type.NoError, "VoucherEntry | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Expense entry could not be save", false, Type.Others, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot Save", false, Entities.Type.Others, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {
                    db.Close();
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
                    return db.ExecuteDataSet(CommandType.Text, @"select top 9999999 convert(varchar,a.Fve_Number) VoucherNo,a.Fve_GroupID,vt.Fvt_TypeName [Type],max(b.Fah_Name) [From], max(c.Fah_Name) [To],convert(varchar,a.Fve_Date,103) [Date],a.Fve_Description Particular,Fve_Amount Amount from tbl_Fin_VoucherEntry a
                                                                   inner join tbl_Fin_VoucherType vt on vt.Fvt_ID = a.Fve_VoucherType
                                                                   left join tbl_Fin_AccountHead b on a.Fve_FrmTransID = b.Fah_ID
                                                                   left join tbl_Fin_AccountHead c on a.Fve_ToTransID = c.Fah_ID
																   where vt.Fvt_TypeName='B P' OR vt.Fvt_TypeName='C P'
                                                                   group by a.Fve_Number, a.Fve_GroupID, a.Fve_Date, a.Fve_Description, Fve_Amount, vt.Fvt_TypeName
                                                                   order by a.Fve_Number desc").Tables[0];

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "ExpenseEntry | GetDetails()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public void getDataForEdit(int groupID, TextBox tDate, TextBox tNarration, DropDownList ddlType, TextBox tAmount, DropDownList ddlFromMain, DropDownList ddlToMain, DropDownList ddlToSub, DropDownList ddlFromSub)
        {
            try
            {
                DBManager db = new DBManager();
                DataTable dt = new DataTable();
                string sql = @"select top 9999999 convert(varchar,a.Fve_Number) VoucherNo,a.Fve_GroupID,vt.Fvt_ID [Type],max(Fve_Date) Fve_Date,b.fah_ID [From], c.Fah_ID [To],isnull(a.Fve_FrmTransChildID,'') Fve_FrmTransChildID,isnull(a.Fve_ToTransChildID,'') Fve_ToTransChildID, convert(varchar,a.Fve_Date,103) [Date],a.Fve_Description Particular,Fve_Amount Amount from tbl_Fin_VoucherEntry a
                                                                   inner join tbl_Fin_VoucherType vt on vt.Fvt_ID = a.Fve_VoucherType
                                                                   left join tbl_Fin_AccountHead b on a.Fve_FrmTransID = b.Fah_ID
                                                                   left join tbl_Fin_AccountHead c on a.Fve_ToTransID = c.Fah_ID
																   left join tbl_Fin_AccountHead d on a.Fve_FrmTransID=d.Fah_ID
																   where Fve_GroupID=@groupID
                                                                   group by a.Fve_Number,b.fah_ID, a.Fve_GroupID, a.Fve_Date,c.Fah_ID, a.Fve_Description,d.Fah_Name, Fve_Amount, vt.Fvt_ID,Fve_FrmTransChildID,Fve_ToTransChildID
                                                                   order by a.Fve_Number desc";
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@groupID", groupID);
                dt = db.ExecuteQuery(CommandType.Text, sql);
                DateTime date = Convert.ToDateTime(dt.Rows[0][8]);
                string datestring = string.Format("{0:dd-MMM-yyyy}", date);
                decimal Amount = Math.Round(Convert.ToDecimal(dt.Rows[0][10]));
                tDate.Text = datestring;
                tNarration.Text = dt.Rows[0][9].ToString();
                ddlType.SelectedValue = dt.Rows[0][2].ToString();
                getAccountGroup(ddlFromMain, Convert.ToInt32(ddlType.SelectedValue));
                tAmount.Text = Amount.ToString();
                string Selected= dt.Rows[1][4].ToString();
                LoadToHead(ddlToMain);
                ddlToMain.SelectedValue = dt.Rows[0][5].ToString();
                try
                {
                    if (dt.Rows[1][4].ToString()!="0")
                    {
                        
                        ddlFromMain.SelectedValue = Selected;
                        //LoadFromSub(ddlToSub, Convert.ToInt32(ddlToMain.SelectedValue));
                        //ddlToSub.SelectedValue = dt.Rows[0][7].ToString();
                    }
                    else
                    {
                        ddlToSub.Items.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "ExpenseEntry | getDataForEdit(int groupID, TextBox tDate, TextBox tNarration, DropDownList ddlType, TextBox tAmount, DropDownList ddlFromMain, DropDownList ddlToMain, DropDownList ddlToSub, DropDownList ddlFromSub)");
                }
                try
                {
                    if (dt.Rows[1][6].ToString() != "0")
                    {
                        //LoadFromSub(ddlFromSub, Convert.ToInt32(ddlFromMain.SelectedValue));
                        //ddlFromSub.SelectedValue = dt.Rows[1][6].ToString();
                    }
                    else
                    {
                        ddlFromSub.Items.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "ExpenseEntry | getDataForEdit(int groupID, TextBox tDate, TextBox tNarration, DropDownList ddlType, TextBox tAmount, DropDownList ddlFromMain, DropDownList ddlToMain, DropDownList ddlToSub, DropDownList ddlFromSub)");
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ExpenseEntry | getDataForEdit(int groupID, TextBox tDate, TextBox tNarration, DropDownList ddlType, TextBox tAmount, DropDownList ddlFromMain, DropDownList ddlToMain, DropDownList ddlToSub, DropDownList ddlFromSub)");
            }

        }
        /// <summary>
        /// To fill the from main DropDown list
        /// </summary>
        /// <param name="ddl">The drop down to Be Loaded</param>
        /// <param name="select">The Selected Value of ddlVouchertype</param>
        public static void getAccountGroup(DropDownList ddl,int select)
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                int start = 0;
                //int End = 0;
                db.Open();
                string sql = @"declare @keyvalue varchar(50)
                            set @keyvalue = (select KeyValue from TBL_SETTINGS where Settings_Id = 5)
                            SELECT Element FROM SPLIT(@keyvalue, '|')";
                dt = db.ExecuteQuery(CommandType.Text, sql);
                if (dt.Rows.Count > 0)
                {
                    if (select == 1)
                    {
                        start = Convert.ToInt32(dt.Rows[0][0]);
                    }
                    else
                    {
                        start = Convert.ToInt32(dt.Rows[1][0]);
                    }
                }          
                db.CreateParameters(1);
                db.AddParameters(0, "@start", start);
                //db.AddParameters(1, "@end", End);
                dt = db.ExecuteQuery(CommandType.Text, @"SELECT Fah_ID id,Fah_Name Name
                FROM tbl_Fin_AccountHead INNER JOIN tbl_Fin_AccountGroup ON Fag_ID = Fah_FagID and Fag_ID =@start");
                ddl.DataSource = dt;
                ddl.DataValueField = "ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
                ddl.Items.Add(new ListItem("--Select--", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ExpenseEntry | getAccountGroup(DropDownList ddl,int select)");
            }
        }
        public static void LoadToHead(DropDownList ddl)
        {
            try
            {
                DBManager db = new DBManager();
                DataTable dt = new DataTable();
                
                //int End = 0;
                db.Open();
                string sql = @"SELECT Fah_ID id,Fah_Name Name
                FROM tbl_Fin_AccountHead INNER JOIN tbl_Fin_AccountGroup ON Fag_ID = Fah_FagID and Fah_Nature=2 and Fag_ID between 53 and 59";
                dt = db.ExecuteQuery(CommandType.Text, sql);
                ddl.DataSource = dt;
                ddl.DataValueField = "ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
                ddl.Items.Add(new ListItem("--Select--", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ExpenseEntry | LoadToHead(DropDownList ddl)");
            }
        }
        /// <summary>
        /// To Load the dropdownlist
        /// </summary>
        /// <param name="ddl">DropDown list to be Filled</param>
        public static void getVoucherTypes(DropDownList ddl)
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                dt = db.ExecuteQuery(CommandType.Text, @"SELECT Fvt_TypeName Name, Fvt_ID ID FROM tbl_Fin_VoucherType WHERE Fvt_DisplayInJournal= 1 AND Fvt_Disable = 0 and (Fvt_ID=1 or Fvt_ID=3) ORDER BY Fvt_TypeName");
                ddl.DataSource = dt;
                ddl.DataValueField = "ID";
                ddl.DataTextField = "Name";
                ddl.DataBind();
                ddl.Items.Add(new ListItem("--Select--", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "ExpenseEntry | getVoucherTypes(DropDownList ddl)");
            }
        }
        public OutputMessage Update(string StrVType, string StrAccID, string StrChdID, string StrAmount, string StrCostCtr)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.VoucherEntry, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherEntry | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id Must Not Be Empty", false, Type.Others, "VoucherEntry | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"                                   IF @UserName = NULL OR @UserName = ''      
                                      SET @UserName = suser_sname()      
                                          
                                     DECLARE       
                                       @VNumbering  INT,      
                                       @VStartNo  INT,      
                                       @VReStart  INT,      
                                       @FDate   DATETIME,      
                                       @TDate   DATETIME,      
                                       @FStr   VARCHAR(20),      
                                       @TStr   VARCHAR(20),      
                                       @CCValue  VARCHAR(500),      
                                       @VEtyID   INT,      
                                       @CCtrID   INT,      
                                       @CCtrAmt  MONEY,      
                                       @RCnt   INT      
                                          
                                          
                                          
                                          
                                            
                                    -- Voucher Date Generation Settings      
                                          
                                          ---------------18/OCT/2012
                                    --        if @IsCheque=0
                                    --         begin
                                    --          set @ChequeDate=NULL
                                    --          set @ChequeNo=NULL
                                    ----            set @VoucherID=2
                                    --         end 
                                             ---------------------
                                    --      if @IsCheque<>0     ---------------21/Nov/2012
                                    --         begin
                                    --          set @VoucherID=5
                                    --         end 
                                          
                                     -----------------------------------------------------------Voucher Number Generation      
                                             
                                    --Voucher Type Settings       
                                          
                                     SELECT @VNumbering  = ISNULL(Fvt_Numbering,0),         
                                     @VStartNo  = ISNULL(Fvt_NoStatFrom,0),       
                                     @VReStart  = ISNULL(Fvt_RestartNo,0)       
                                     FROM tbl_Fin_VoucherType       
                                     WHERE Fvt_ID  = @VoucherID       
                                          
                                          
                                          
                                          
                                          
                                     IF @VNumbering = 1        -- Automatic numbering      
                                       BEGIN        
                                        IF @VReStart = 0     -- Restart by Year      
                                          BEGIN        
                                           SET @FStr = CONVERT(VARCHAR,YEAR(@VcrDate))        
                                            IF MONTH(@VcrDate)<4       
                                       SET @FStr = CONVERT(VARCHAR,YEAR(@VcrDate)-1)        
                                           SET @FStr = '01-APR-' + @FStr        
                                              
                                      SET @TStr = CONVERT(VARCHAR,YEAR(@VcrDate)+1)        
                                            IF MONTH(@VcrDate)<4       
                                       SET @TStr = CONVERT(VARCHAR,YEAR(@VcrDate))        
                                           SET @TStr = '31-MAR-' + @TStr        
                                            
                                       END        
                                      ELSE IF @VReStart = 1     -- Restart by Month      
                                         BEGIN        
                                          SET @FStr = '01-' +  CONVERT(VARCHAR,LEFT(DATENAME(MONTH,@VcrDate),3)) + '-' + CONVERT(VARCHAR,YEAR(@VcrDate))        
                                          SET @TStr =   CONVERT(VARCHAR,LEFT(DATENAME(MONTH,@VcrDate),3)) + '-' + CONVERT(VARCHAR,YEAR(@VcrDate))        
                                            
                                          IF MONTH(@VcrDate) = 2        
                                           BEGIN       
                                             IF (YEAR(@VcrDate) % 4) = 0       
                                       SET @TStr = '29-' + @TStr       
                                      ELSE       
                                       SET @TStr = '28-' + @TStr        
                                            END        
                                          ELSE IF MONTH(@VcrDate) = 4 OR MONTH(@VcrDate) = 6 OR MONTH(@VcrDate) = 9 OR MONTH(@VcrDate) = 11         
                                           BEGIN        
                                             SET @TStr = '30-' + @TStr         
                                           END        
                                          ELSE        
                                           BEGIN        
                                             SET @TStr = '31-' + @TStr         
                                           END        
                                      END      
                                          
                                         SET @FDate = CONVERT(SMALLDATETIME, @FStr)        
                                         SET @TDate = CONVERT(SMALLDATETIME, @TStr)        
                                                 
                                          
                                          
                                               
                                     SELECT  @VoucherNo = ISNULL(MAX(Fve_Number), 0)       
                                      FROM tbl_Fin_VoucherEntry         
                                           WHERE Fve_VoucherType = @VoucherID       
                                      AND (Fve_Date BETWEEN @FDate AND @TDate)        
                                            
                                          
                                          
                                          
                                          
                                          IF @VoucherNo = 0       
                                      SET @VoucherNo = @VStartNo       
                                     ELSE       
                                      SET @VoucherNo = @VoucherNo + 1        
                                         END        
                                           
                                          
                                    ----------------------------------------------------------------------------------------------------        
                                          
                                          
                                          
                                          
                                     DECLARE @tblVType TABLE(TID INT IDENTITY(1,1) NOT NULL, VType INT)      
                                     DECLARE @tblAccHeadID TABLE (AID INT IDENTITY(1,1) NOT NULL, AccHeadID INT)      
                                     DECLARE @tblChildHeadID TABLE (CID INT IDENTITY(1,1) NOT NULL, ChlHeadID VARCHAR(15))      
                                     DECLARE @tblAmount TABLE (MID INT IDENTITY(1,1) NOT NULL, AmountVal MONEY)      
                                     DECLARE @tblCostCenter TABLE (CCID INT IDENTITY(1,1) NOT NULL, CCValue VARCHAR(1000))      
                                          
                                     INSERT INTO @tblVType(VType) SELECT CONVERT(INT, Element) FROM SPLIT(@StrVType ,'|')      
                                     INSERT INTO @tblAccHeadID(AccHeadID) SELECT CONVERT(INT, Element) FROM SPLIT(@StrAccID ,'|')      
                                     INSERT INTO @tblChildHeadID(ChlHeadID) SELECT Element FROM SPLIT(@StrChdID, '|')      
                                     INSERT INTO @tblAmount(AmountVal) SELECT CONVERT(MONEY, Element) FROM SPLIT(@StrAmount, '|')      
                                     INSERT INTO @tblCostCenter(CCValue) SELECT Element FROM SPLIT(@StrCostCtr, '|')      
                                          
                                     --SELECT @GroupID = ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry      
                                          
                                     delete from tbl_Fin_VoucherEntry where Fve_GroupID=@GroupID  
                                    
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate      
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID WHERE VType = 0 ORDER BY TID      
                                          
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                        Fve_ToTransID, Fve_ToTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate      
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID WHERE VType = 1 ORDER BY TID      
                                         
                                    -- UPDATING Cost Center Values 
									
									delete FROM [tbl_Fin_VoucherEntryCostCenter] WHERE Fvc_FveID IN   
									(SELECT Fve_ID FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @groupID)     
                                     CREATE TABLE #TmpTableCostCenters      
                                       (      
                                       CenterValues VARCHAR(20)      
                                       )      
                                     CREATE TABLE #TmpTableEntry         
                                       (      
                                       ObjID  INT IDENTITY (1,1) NOT NULL,       
                                       CostCtID VARCHAR(10)      
                                       )      
                                     
									 
									      
                                     SET @RCnt = 0      
                                     
                                     DECLARE CostCenter CURSOR FOR SELECT Fve_ID, Fve_FccID FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @GroupID AND (Fve_FccID IS NOT NULL OR Fve_FccID <> '')      
                                     OPEN CostCenter FETCH NEXT FROM CostCenter INTO @VEtyID, @CCValue      
                                     WHILE @@FETCH_STATUS = 0      
                                      BEGIN      
                                       DELETE FROM #TmpTableCostCenters      
                                       INSERT INTO #TmpTableCostCenters SELECT Element FROM SPLIT(@CCValue, '^')      
                                             
                                       DECLARE CursorCCtrValues CURSOR FOR SELECT CenterValues FROM #TmpTableCostCenters      
                                       OPEN CursorCCtrValues FETCH NEXT FROM CursorCCtrValues INTO @CCValue      
                                       WHILE @@FETCH_STATUS = 0      
                                        BEGIN      
                                         IF @CCValue = '' GOTO GetNextCostCenterValue      
                                               
                                         DELETE FROM #TmpTableEntry      
                                         INSERT INTO #TmpTableEntry (CostCtID) SELECT Element FROM SPLIT(@CCValue, '`')      
                                               
                                         SET @RCnt = @RCnt + 1      
                                         SELECT @CCtrID = CONVERT(INT, CostCtID) FROM #TmpTableEntry WHERE ObjID = @RCnt      
                                         SET @RCnt = @RCnt + 1      
                                         SELECT @CCtrAmt = CONVERT(MONEY, CostCtID) FROM #TmpTableEntry WHERE ObjID = @RCnt      
                                          
                                         INSERT INTO [tbl_Fin_VoucherEntryCostCenter] (Fvc_FveID, Fvc_FccID, Fvc_Amount)      
                                          VALUES (@VEtyID, @CCtrID, @CCtrAmt)      
                                        GetNextCostCenterValue:      
                                         FETCH NEXT FROM CursorCCtrValues INTO @CCValue      
                                        END      
                                       CLOSE CursorCCtrValues      
                                       DEALLOCATE CursorCCtrValues      
                                          
                                       FETCH NEXT FROM CostCenter INTO @VEtyID, @CCValue      
                                      END      
                                     CLOSE CostCenter      
                                     DEALLOCATE CostCenter      
                                               
                                     SELECT @GroupID AS GroupID,@VoucherNo AS VoucherNo      
                                     -----------------------------------------------------------";
                        db.CreateParameters(17);
                        db.AddParameters(0, "@VcrDate", Convert.ToDateTime(this.Date));
                        db.AddParameters(1, "@UserName", this.username);
                        db.AddParameters(2, "@Narration", this.Description);
                        db.AddParameters(3, "@StrVType", StrVType);
                        db.AddParameters(4, "@StrAccID", StrAccID);
                        db.AddParameters(5, "@StrChdID", StrChdID);
                        db.AddParameters(6, "@StrAmount", StrAmount);
                        db.AddParameters(7, "@StrCostCtr", StrCostCtr);
                        db.AddParameters(8, "@IsCheque", this.IsCheque);
                        db.AddParameters(9, "@ChequeNo", this.ChequeNo);
                        db.AddParameters(10, "@ChequeDate", this.ChequeDate);
                        db.AddParameters(11, "@VoucherNo", this.VoucherNo);
                        db.AddParameters(12, "@VoucherID", this.VoucherTypeID);
                        db.AddParameters(13, "@IsVoucherEntry", 1);
                        db.AddParameters(14, "@Drawon", this.Drawon);
                        db.AddParameters(15, "@receiptNo", this.ReceiptNo);
                        db.AddParameters(16, "@GroupID", this.ID);

                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Expense entry updated successfully", true, Type.NoError, "VoucherEntry | Update", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Expense entry could not be Updated", false, Type.Others, "VoucherEntry | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Expense entry could not be Updated", false, Type.Others, "VoucherEntry | Update", System.Net.HttpStatusCode.InternalServerError,ex);

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
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.FinancialTransactions, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"INSERT INTO [tbl_Fin_VoucherDelete] ([Fvd_ID], [Fvd_Date], [Fvd_ByUser], [Fvd_Description], [Fvd_VoucherType],   
                              [Fvd_Number], [Fvd_Amount], [Fvd_GroupID], [Fvd_IsVoucher], [Fvd_FrmTransID], [Fvd_FrmTransChildID],   
                              [Fvd_IncomeDesc], [Fvd_ToTransID], [Fvd_ToTransChildID], [Fvd_ExpenseDesc], [Fvd_IsCheque], [Fvd_ChequeNo],   
                              [Fvd_ChequeDate], [Fvd_CurSysUser], [Fvd_CurDtTime], [Fvd_FccID], [Fvd_IsCleared], [Fvd_ChequeClearDate],   
                              [Fvd_DelSysUser])  
                             SELECT Fve_ID, Fve_Date, Fve_ByUser, Fve_Description, Fve_VoucherType, Fve_Number, Fve_Amount, Fve_GroupID,  
                              Fve_IsVoucher, Fve_FrmTransID, Fve_FrmTransChildID, Fve_IncomeDesc, Fve_ToTransID, Fve_ToTransChildID,   
                              Fve_ExpenseDesc, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_CurSysUser, Fve_CurDtTime, Fve_FccID,   
                              Fve_IsCleared, Fve_ChequeClearDate, @UserName FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @VGroupID  
                              
                             INSERT INTO [tbl_Fin_VoucherDeleteCostCenter]( [Fdc_FvdID], [Fdc_FccID], [Fdc_Amount])  
                              SELECT Fvc_FveID, Fvc_FccID, Fvc_Amount FROM [tbl_Fin_VoucherEntryCostCenter] WHERE Fvc_FveID IN   
                              (SELECT Fve_ID FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @VGroupID)  
                              
                              --update  a set a.deldate = null,a.[status]=7,netamt=null,TotalAmount=null,discount=null,invoiceno=invoiceno + 'Deleted' from jobcard a where id  = ( select top 1 fve_frmtranschildid from tbl_Fin_VoucherEntry where fve_frmtransid = 2  
                              --and Fve_GroupID=@VGroupID
                              --)
                            
                             DELETE FROM [tbl_Fin_VoucherEntryCostCenter] WHERE Fvc_FveID IN   
                              (SELECT Fve_ID FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @VGroupID)   
                              
                             DELETE FROM tbl_Fin_VoucherEntry WHERE Fve_GroupID = @VGroupID 
                              -- Removing the Payment Bills Linked to the Voucher Entry 
                              -- DELETE FROM tbl_Fin_Payments WHERE Pay_VchGroupID = @VGroupID  
                                
                               --DELETE FROM tbl_Fin_CustomerReceipts where  Pay_VchGroupID = @VGroupID  
                               -----------------------------------------------------------------------------  
                                 
                               -- Removing the Payment Bills Linked to the Voucher Entry  
                               -- RETURN BILLS  
                               -- DELETE FROM tbl_Fin_PaymentsReturn WHERE Pyr_VchGroupID = @VGroupID";
                        db.CreateParameters(2);
                        db.AddParameters(0, "@VGroupID", this.ID);
                        db.AddParameters(1, "@UserName", this.ModifiedBy);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Expense entry deleted successfully", true, Type.NoError, "FinancialTransactions | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Expense entry could not be deleted", false, Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
    }
}
