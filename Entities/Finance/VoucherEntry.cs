using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Entities.Finance
{
    public class VoucherEntry
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
        public DateTime? ChequeDate { get; set; }
        public string Fve_CurSysUser { get; set; }
        public int FccID { get; set; }
        public int IsCleared { get; set; }
        public DateTime ChequeClearDate { get; set; }
        public string Drawon { get; set; }
        public int ReceiptNo { get; set; }
        public string Fve_UTR { get; set; }
        public Boolean Isbounce { get; set; }
        public DateTime Date { get; set; }
        public string username { get; set; }
        public int VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public string AccountHead { get; set; }
        public string AccountChild { get; set; }
        public string Amount { get; set; }
        public decimal AmountNew { get; set; }
        public string CostCenter { get; set; }
        public int ModifiedBy { get; set; }
        public int groupID { get; set; }
        public string Jobs { get; set; }
        public string EntryDesc { get; set; }
        #endregion
        /// <summary>
        /// To Load the Sub child of dropdownList
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="selected"></param>
        public static void LoadFromSub(DropDownList ddl, int selected)
        {
            try
            {
                DataSet ds1 = new DataSet();
                DBManager db = new DBManager();
                db.Open();
                string sql = "select Fah_DataSQL DataSQL, Fah_Type [Type], Fah_SQLTable SQLTable from tbl_Fin_AccountHead where Fah_ID=" + selected;
                ds1 = db.ExecuteDataSet(CommandType.Text, sql);
                string datasql = ds1.Tables[0].Rows[0]["DataSQL"].ToString();
                DataSet ds = new DataSet();
                ds = db.ExecuteDataSet(CommandType.Text, datasql);
                ddl.Items.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        row = ds.Tables[0].Rows[i];
                        ddl.Items.Insert(i, new ListItem(Convert.ToString(row[0]), Convert.ToString(row[1])));
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | LoadFromSub(DropDownList ddl, int selected)");
            }
        }
        /// <summary>
        /// Dataset to load Save Details
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
                Application.Helper.LogException(ex, "voucherentry | CreateDataset()");
                return null;
            }
        }
        /// <summary>
        /// Save function
        /// </summary>
        /// <param name="StrVType"></param>
        /// <param name="StrAccID"></param>
        /// <param name="StrChdID"></param>
        /// <param name="StrAmount"></param>
        /// <param name="StrCostCtr"></param>
        /// <returns></returns>
        public OutputMessage Save(string StrVType, string StrAccID, string StrChdID, string StrAmount, string StrCostCtr,string strJobs,string strDesc)
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
									 DECLARE @tblJobs TABLE(JID INT IDENTITY(1,1) NOT NULL,JValue INT)   
									 DECLARE @tblDescription TABLE(DID INT IDENTITY(1,1) NOT NULL,EntryDesc varchar(1000))   
                                          
                                     INSERT INTO @tblVType(VType) SELECT CONVERT(INT, Element) FROM SPLIT(@StrVType ,'|')      
                                     INSERT INTO @tblAccHeadID(AccHeadID) SELECT CONVERT(INT, Element) FROM SPLIT(@StrAccID ,'|')      
                                     INSERT INTO @tblChildHeadID(ChlHeadID) SELECT Element FROM SPLIT(@StrChdID, '|')      
                                     INSERT INTO @tblAmount(AmountVal) SELECT CONVERT(MONEY, Element) FROM SPLIT(@StrAmount, '|')      
                                     INSERT INTO @tblCostCenter(CCValue) SELECT Element FROM SPLIT(@StrCostCtr, '|')      
                                     INSERT INTO @tblJobs(JValue)  SELECT CONVERT(INT, Element) FROM SPLIT(@StrJob ,'|')
									 INSERT INTO @tblDescription(EntryDesc) SELECT Element from SPLIT(@strDesc,'|')
									      
                                     SELECT @GroupID = ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry      
                                          
                                          
                                    
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime,Job_ID,Fve_ExpenseDesc)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate,JValue,EntryDesc     
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID
									   LEFT Outer JOIN @tblJobs ON JID=TID
									   LEFT OUTER JOIN @tblDescription on DID=TID  WHERE VType = 0 ORDER BY TID 
                                          
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                        Fve_ToTransID, Fve_ToTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime,Job_ID,Fve_IncomeDesc)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate ,JValue,EntryDesc     
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID 
									   LEFT Outer JOIN @tblJobs ON JID=TID 
                                       LEFT OUTER JOIN @tblDescription on DID=TID
                                       WHERE VType = 1 ORDER BY TID      
                                         
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
                                     SELECT @GroupID AS GroupID,@VoucherNo AS VoucherNo";
                        db.CreateParameters(18);
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
                        db.AddParameters(16, "@StrJob", strJobs);
                        db.AddParameters(17, "@StrDesc", strDesc);
                        int groupID = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                        return new OutputMessage(" Voucher entry saved successfully", true, Type.NoError, "VoucherEntry | Save", System.Net.HttpStatusCode.OK, groupID);
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
        /// Datatable to load the table of entries
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDetails()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, @"select top 9999999 convert(varchar,a.Fve_Number) VoucherNo,a.Fve_GroupID,vt.Fvt_TypeName [Type],a.Fve_ID,max(b.Fah_Name) [From], max(c.Fah_Name) [To],convert(varchar,a.Fve_Date,103) [Date],a.Fve_Description Particular,Fve_Amount Amount from tbl_Fin_VoucherEntry a
                                                                   inner join tbl_Fin_VoucherType vt on vt.Fvt_ID = a.Fve_VoucherType
                                                                   left join tbl_Fin_AccountHead b on a.Fve_FrmTransID = b.Fah_ID
                                                                   left join tbl_Fin_AccountHead c on a.Fve_ToTransID = c.Fah_ID
                                                                   group by a.Fve_Number, a.Fve_GroupID, a.Fve_Date, a.Fve_Description, Fve_Amount, vt.Fvt_TypeName,Fve_ID order by a.Fve_Number desc").Tables[0];
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "voucherentry | GetDetails()");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// DataTable for loading the voucher Types
        /// </summary>
        /// <returns></returns>
        public static DataTable getVoucherTypesForEntry()
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                dt = db.ExecuteQuery(CommandType.Text, "SELECT Fvt_TypeName, Fvt_ID FROM tbl_Fin_VoucherType WHERE Fvt_DisplayInJournal= 1 AND Fvt_Disable = 0 ORDER BY Fvt_TypeName");
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry |getVoucherTypesForEntry()");
                return null;
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
                tAmount.Text = Amount.ToString();
                ddlFromMain.SelectedValue = dt.Rows[1][4].ToString();
                ddlToMain.SelectedValue = dt.Rows[0][5].ToString();
                try
                {
                    if (dt.Rows[0][7].ToString() != "0")
                    {
                        LoadFromSub(ddlToSub, Convert.ToInt32(ddlToMain.SelectedValue));
                        ddlToSub.SelectedValue = dt.Rows[0][7].ToString();
                    }
                    else
                    {
                        ddlToSub.Items.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "voucherentry | getDataForEdit(int groupID, TextBox tDate, TextBox tNarration, DropDownList ddlType, TextBox tAmount, DropDownList ddlFromMain, DropDownList ddlToMain, DropDownList ddlToSub, DropDownList ddlFromSub)");
                }
                try
                {
                    if (dt.Rows[1][6].ToString() != "0")
                    {
                        LoadFromSub(ddlFromSub, Convert.ToInt32(ddlFromMain.SelectedValue));
                        ddlFromSub.SelectedValue = dt.Rows[1][6].ToString();
                    }
                    else
                    {
                        ddlFromSub.Items.Clear();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

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
                            return new OutputMessage(" Voucher entry deleted successfully", true, Type.NoError, "FinancialTransactions | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Voucher edit could not be Deleted", false, Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

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
                        #region query
                        string query = @"delete from tbl_Fin_VoucherEntryCostCenter where Fvc_FveID in (select Fve_ID from tbl_Fin_VoucherEntry where Fve_GroupID=@GroupID)
									    delete from tbl_Fin_VoucherEntry where Fve_GroupID=@GroupID
                                        IF @UserName = NULL OR @UserName = ''      
                                      SET @UserName = suser_sname()      
                                          
                                     DECLARE @VNumbering  INT,      
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
                                                 
                                          
                                          
                                               
                                     --SELECT  @VoucherNo = ISNULL(MAX(Fve_Number), 0)       
                                     -- FROM tbl_Fin_VoucherEntry         
                                     --      WHERE Fve_VoucherType = @VoucherID       
                                    --  AND (Fve_Date BETWEEN @FDate AND @TDate)  

                                     IF @VoucherNo != 0  
										  SET @VoucherNo = @VoucherNo     
                                     ELSE       
                                          SET @VoucherNo = @VoucherNo
                                         END            
                                            
                                          
                                    ----------------------------------------------------------------------------------------------------        
                                          
                                          
                                          
                                          
                                     DECLARE @tblVType TABLE(TID INT IDENTITY(1,1) NOT NULL, VType INT)      
                                     DECLARE @tblAccHeadID TABLE (AID INT IDENTITY(1,1) NOT NULL, AccHeadID INT)      
                                     DECLARE @tblChildHeadID TABLE (CID INT IDENTITY(1,1) NOT NULL, ChlHeadID VARCHAR(15))      
                                     DECLARE @tblAmount TABLE (MID INT IDENTITY(1,1) NOT NULL, AmountVal MONEY)      
                                     DECLARE @tblCostCenter TABLE (CCID INT IDENTITY(1,1) NOT NULL, CCValue VARCHAR(1000))
									 DECLARE @tblJobs TABLE(JID INT IDENTITY(1,1) NOT NULL,JValue INT)   
									 DECLARE @tblDescription TABLE(DID INT IDENTITY(1,1) NOT NULL,EntryDesc varchar(1000))   
                                          
                                     INSERT INTO @tblVType(VType) SELECT CONVERT(INT, Element) FROM SPLIT(@StrVType ,'|')      
                                     INSERT INTO @tblAccHeadID(AccHeadID) SELECT CONVERT(INT, Element) FROM SPLIT(@StrAccID ,'|')      
                                     INSERT INTO @tblChildHeadID(ChlHeadID) SELECT Element FROM SPLIT(@StrChdID, '|')      
                                     INSERT INTO @tblAmount(AmountVal) SELECT CONVERT(MONEY, Element) FROM SPLIT(@StrAmount, '|')      
                                     INSERT INTO @tblCostCenter(CCValue) SELECT Element FROM SPLIT(@StrCostCtr, '|')      
                                     INSERT INTO @tblJobs(JValue)  SELECT CONVERT(INT, Element) FROM SPLIT(@StrJob ,'|')
									 INSERT INTO @tblDescription(EntryDesc) SELECT Element from SPLIT(@strDesc,'|')
									      
                                     --SELECT @GroupID = ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry      
                                          
                                          
                                    
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime,Job_ID,Fve_ExpenseDesc)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate,JValue,EntryDesc     
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID
									   LEFT Outer JOIN @tblJobs ON JID=TID
									   LEFT OUTER JOIN @tblDescription on DID=TID  WHERE VType = 0 ORDER BY TID 
                                          
                                     INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                        Fve_ToTransID, Fve_ToTransChildID, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime,Job_ID,Fve_IncomeDesc)      
                                        SELECT @VoucherID, @VoucherNo, case when @IsCheque = 1 then @ChequeDate else @VcrDate end, @UserName, @Narration, AmountVal, AccHeadID, ISNULL(ChlHeadID,NULL),       
                                       @IsCheque, @ChequeNo, @ChequeDate, @GroupID, @IsVoucherEntry, CCValue,@UserName,@Drawon,@receiptNo,@VcrDate ,JValue,EntryDesc     
                                       FROM @tblVType LEFT OUTER JOIN @tblAccHeadID ON AID = TID LEFT OUTER JOIN @tblChildHeadID ON CID = TID       
                                       LEFT OUTER JOIN @tblAmount ON MID = TID LEFT OUTER JOIN @tblCostCenter ON CCID = TID 
									   LEFT Outer JOIN @tblJobs ON JID=TID 
                                       LEFT OUTER JOIN @tblDescription on DID=TID
                                       WHERE VType = 1 ORDER BY TID      
                                         
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
                                     SELECT @GroupID AS GroupID,@VoucherNo AS VoucherNo";
                        #endregion
                        db.CreateParameters(19);
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
                        db.AddParameters(17, "@StrJob", this.Jobs);
                        db.AddParameters(18, "@StrDesc", this.EntryDesc);

                        int groupID = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                        //bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        return new OutputMessage(" Voucher entry updated successfully", true, Type.NoError, "VoucherEntry | Update", System.Net.HttpStatusCode.OK, groupID);
                        
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Voucher entry could not be Updated", false, Type.Others, "VoucherEntry | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }
                }
            }
        }
        public static DataTable GetAccountHeadsVoucher()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, @"  declare @i int=1
                                                                   declare @count int
                                                                   declare @id int
                                                                   create table #tempnew(name varchar(100),id int,parent int)
                                                                   create table #temptable(name varchar(100),id int,parent int)
                                                                   declare @sql varchar(100)
                                                                   set @count=(select Count(*) from tbl_Fin_AccountHead where Fah_DataSQL is not null)
                                                                   while @i<=@count
                                                                   begin
                                                                   ;with cte as(
                                                                   select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null )
                                                                   select @id=Fah_ID from cte where Slno=@i
                                                                   set @sql=(select Fah_DataSQL from tbl_Fin_AccountHead where fah_id=@id)
                                                                   insert into #tempnew (name,id) exec(@sql)
                                                                   update #tempnew set parent=@id
                                                                   insert into #temptable select * from #tempnew
                                                                   truncate table #tempnew
                                                                   set @i=@i+1
                                                                   end
                                                                   select fah_name Name,fah_id ID,0 parent from tbl_Fin_AccountHead where Fah_DataSQL is null and Fah_FagID!=45
                                                                   union all
                                                                   select * from #temptable").Tables[0];

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "voucherentry | DataTable GetAccountHeadsVoucher()");
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
            using (DBManager db = new DBManager())
            {
                try
                {
                    if ((!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherEntry, Security.PermissionTypes.Create)))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        db.Open();
                        #region query
                        string Sql = @"declare @vNumbering int
                                       declare @VStartNo int
                                       declare @VReStart int
                                       declare @FDate   DATETIME
                                       declare @TDate   DATETIME      
                                       declare @FStr   VARCHAR(20)     
                                       declare @TStr   VARCHAR(20)
                                       if @groupID=0
									   begin
                                       select @groupID=ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry 
                                       end
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
                                                                             AND (Fve_Date BETWEEN Convert(DATE,@FDate,103) AND Convert(DATE,@TDate,103))        
                                                                                   
                                                                                 
                                                                                 IF @VoucherNo = 0       
                                                                             SET @VoucherNo = @VStartNo       
                                                                            ELSE       
                                                                             SET @VoucherNo = @VoucherNo + 1        
                                                                                END      
                                       INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID,Fve_ToTransID,Fve_ToTransChildID,Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)
									   values(@voucherType,@VoucherNo,@VcrDate,1,@description,@amount,@frmID,@frmChildID,@ToID,@toChildID,@IsCheque,@chequeNumber,@chequeDate,@groupID,@isVoucher,@costCenter,1,@drawon,@reciept,GETDATE()) 			    
                                        declare @vouhcerID int
                                            select @vouhcerID=max(Fve_ID) from tbl_Fin_VoucherEntry
											
                                        INSERT INTO [tbl_Fin_VoucherEntryCostCenter] (Fvc_FveID, Fvc_FccID, Fvc_Amount)
                                                                values(@vouhcerID,1,@amount)
                                        SELECT @GroupID AS GroupID";
                        #endregion
                        db.BeginTransaction();
                        db.CreateParameters(19);
                        db.AddParameters(0, "@voucherID", Convert.ToInt32(this.VoucherTypeID));
                        db.AddParameters(1, "@numbering", Convert.ToInt32(this.VoucherNo));
                        db.AddParameters(2, "@voucherType", Convert.ToInt32(this.VoucherTypeID));
                        db.AddParameters(3, "@description", this.Description);
                        db.AddParameters(4, "@amount", this.AmountNew);
                        db.AddParameters(5, "@frmID", this.Frm_TransID);
                        if (this.Frm_TransID==0)
                        {
                            db.AddParameters(6, "@frmChildID", null);

                        }
                        else
                        {
                            db.AddParameters(6, "@frmChildID", this.FrmTransChildID);
                        }
                        db.AddParameters(7, "@ToID", this.ToTransID);
                        if (this.ToTransID==0)
                        {
                            db.AddParameters(8, "@toChildID", null);
                        }
                        else
                        {
                            db.AddParameters(8, "@toChildID", this.ToTransChildID);
                        }
                        db.AddParameters(9, "@IsCheque", this.IsCheque);
                        db.AddParameters(10, "@chequeDate", this.ChequeDate);
                        db.AddParameters(11, "@isVoucher", this.IsVoucher);
                        db.AddParameters(12, "@costCenter", this.CostCenter);
                        db.AddParameters(13, "@drawon", this.Drawon);
                        db.AddParameters(14, "@reciept", this.ReceiptNo);
                        db.AddParameters(15, "@VcrDate", this.Date);
                        db.AddParameters(16, "@VoucherNo", this.VoucherNo);
                        db.AddParameters(17, "@chequeNumber", this.ChequeNo);
                        db.AddParameters(18, "@groupID", this.groupID);
                        int GroupID = 0;
                        GroupID = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, Sql));
                        db.CommitTransaction();
                        return new OutputMessage("Saved Successfully", true, Type.NoError, "VoucherEntry | Save", System.Net.HttpStatusCode.OK, GroupID);
                    }
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Failed to save", false, Type.InsufficientPrivilege, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public void loadVoucherDetails(int GroupID,DropDownList ddlfrommain,DropDownList ddltomain,DropDownList ddlVoucherType,TextBox txtnarration,TextBox txtDate,TextBox txtAmount)
        {
            try
            {
                DBManager db = new DBManager();
                DataSet ds = new DataSet();
                db.Open();
                string sql = @"select fve_Date,Fve_frmTransID,fve_frmtransChildID,fve_totransID,fve_toTransChildId,Fve_Amount,fve_Description,fve_VoucherType from tbl_fin_voucherEntry where fve_groupID="+ GroupID + " order by Fve_FrmTransID desc";
                ds = db.ExecuteDataSet(CommandType.Text, sql);
                string from, To, fromChild, toChild;
                txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["fve_Date"]).ToString("dd-MMM-yyyy");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    from = ds.Tables[0].Rows[i]["Fve_frmTransID"].ToString();
                    To = ds.Tables[0].Rows[i]["fve_totransID"].ToString();
                    fromChild = ds.Tables[0].Rows[i]["fve_frmtransChildID"].ToString();
                    if (fromChild == "0")
                    {
                        from = "0|" + from;
                    }
                    else
                    {
                        from = from + "|" + fromChild;
                    }
                    toChild = ds.Tables[0].Rows[i]["fve_toTransChildId"].ToString();
                    if (toChild == "0")
                    {
                        To = "0|" + To;
                    }
                    else
                    {
                        To = To + "|" + toChild;
                    }
                    ddlfrommain.SelectedValue = from;
                    ddltomain.SelectedValue = To;
                    ddlVoucherType.SelectedValue = ds.Tables[0].Rows[i]["fve_VoucherType"].ToString();
                    txtnarration.Text = ds.Tables[0].Rows[i]["fve_Description"].ToString();
                    decimal Amount = Math.Round(Convert.ToDecimal(ds.Tables[0].Rows[0]["Fve_Amount"]));
                    txtAmount.Text = Amount.ToString();
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | loadVoucherDetails(int GroupID,DropDownList ddlfrommain,DropDownList ddltomain,DropDownList ddlVoucherType,TextBox txtnarration,TextBox txtDate,TextBox txtAmount)");
            }
        }
        public void DeleteVoucher(int GroupID)
        {
            try
            {
                DBManager db = new DBManager();
                string sql = @"delete from tbl_Fin_VoucherEntryCostCenter where fvc_fveid in(select Fve_ID from tbl_Fin_VoucherEntry where Fve_GroupID=@group)
                                delete from tbl_Fin_VoucherEntry where fve_groupID=@group";
                db.CreateParameters(1);
                db.AddParameters(0, "@group", GroupID);
                db.Open();
                db.ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | DeleteVoucher(int GroupID)");
            }
        }
        public OutputMessage Update()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if ((!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherEntry, Security.PermissionTypes.Create)))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        db.Open();
                        #region query
                        string Sql = @"declare @vNumbering int
                                       declare @VStartNo int
                                       declare @VReStart int
                                       declare @FDate   DATETIME
                                       declare @TDate   DATETIME      
                                       declare @FStr   VARCHAR(20)     
                                       declare @TStr   VARCHAR(20)
                                       if @groupID=0
									   begin
                                       select @groupID=ISNULL(MAX(Fve_GroupID),0) + 1 FROM tbl_Fin_VoucherEntry 
                                       end
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
                                                                             AND (Fve_Date BETWEEN Convert(DATE,@FDate,103) AND Convert(DATE,@TDate,103))        
                                                                                   
                                                                                 
                                                                                 IF @VoucherNo = 0       
                                                                             SET @VoucherNo = @VStartNo       
                                                                            ELSE       
                                                                             SET @VoucherNo = @VoucherNo + 1        
                                                                                END      
                                       INSERT INTO tbl_Fin_VoucherEntry (Fve_VoucherType, Fve_Number, Fve_Date, Fve_ByUser, Fve_Description, Fve_Amount,       
                                       Fve_FrmTransID, Fve_FrmTransChildID,Fve_ToTransID,Fve_ToTransChildID,Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate, Fve_GroupID, Fve_IsVoucher, Fve_FccID,Fve_CurSysUser,Fve_Drawon,Fve_ReceiptNo,Fve_CurDtTime)
									   values(@voucherType,@VoucherNo,@VcrDate,1,@description,@amount,@frmID,@frmChildID,@ToID,@toChildID,@IsCheque,@chequeNumber,@chequeDate,@groupID,@isVoucher,@costCenter,1,@drawon,@reciept,GETDATE()) 			    
                                        declare @vouhcerID int
                                            select @vouhcerID=max(Fve_ID) from tbl_Fin_VoucherEntry
											
                                        INSERT INTO [tbl_Fin_VoucherEntryCostCenter] (Fvc_FveID, Fvc_FccID, Fvc_Amount)
                                                                values(@vouhcerID,1,@amount)
                                        SELECT @GroupID AS GroupID";
                        #endregion
                        db.BeginTransaction();
                        db.CreateParameters(19);
                        db.AddParameters(0, "@voucherID", Convert.ToInt32(this.VoucherTypeID));
                        db.AddParameters(1, "@numbering", Convert.ToInt32(this.VoucherNo));
                        db.AddParameters(2, "@voucherType", Convert.ToInt32(this.VoucherTypeID));
                        db.AddParameters(3, "@description", this.Description);
                        db.AddParameters(4, "@amount", this.AmountNew);
                        db.AddParameters(5, "@frmID", this.Frm_TransID);
                        if (this.Frm_TransID == 0)
                        {
                            db.AddParameters(6, "@frmChildID", null);

                        }
                        else
                        {
                            db.AddParameters(6, "@frmChildID", this.FrmTransChildID);
                        }
                        db.AddParameters(7, "@ToID", this.ToTransID);
                        if (this.ToTransID == 0)
                        {
                            db.AddParameters(8, "@toChildID", null);
                        }
                        else
                        {
                            db.AddParameters(8, "@toChildID", this.ToTransChildID);
                        }
                        db.AddParameters(9, "@IsCheque", this.IsCheque);
                        db.AddParameters(10, "@chequeDate", this.ChequeDate);
                        db.AddParameters(11, "@isVoucher", this.IsVoucher);
                        db.AddParameters(12, "@costCenter", this.CostCenter);
                        db.AddParameters(13, "@drawon", this.Drawon);
                        db.AddParameters(14, "@reciept", this.ReceiptNo);
                        db.AddParameters(15, "@VcrDate", this.Date);
                        db.AddParameters(16, "@VoucherNo", this.VoucherNo);
                        db.AddParameters(17, "@chequeNumber", this.ChequeNo);
                        db.AddParameters(18, "@groupID", this.groupID);
                        //int GroupID = 0;
                        db.ExecuteScalar(CommandType.Text, Sql);
                        db.CommitTransaction();
                        return new OutputMessage("Saved Successfully", true, Type.NoError, "VoucherEntry | Save", System.Net.HttpStatusCode.OK);
                    }
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Failed to save", false, Type.InsufficientPrivilege, "VoucherEntry | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public decimal CalculateOpeningBalance(int selected,int child)
        {
            try
            {
                if (child==0)
                {
                    string sql = @"declare @balance decimal
                    select @balance=max(Fah_OpeningBal)-isnull(Sum(fve_amount),0) from tbl_Fin_AccountHead a
					inner join tbl_Fin_VoucherEntry b on a.Fah_ID=b.Fve_FrmTransID
					where b.Fve_FrmTransID="+selected+" select @balance+isnull(Sum(fve_amount),0) from tbl_Fin_AccountHead a inner join tbl_Fin_VoucherEntry b on a.Fah_Id=b.Fve_ToTransID where Fve_ToTransID="+selected;
                    DBManager db = new DBManager();
                    db.Open();
                    decimal Amount = 0;
                    Amount = Convert.ToDecimal(db.ExecuteScalar(CommandType.Text, sql));
                    return Math.Round(Amount);
                }
                else
                {
                    string sql = @"declare @balance decimal
                                    select @balance=Max(Fob_OpenBal)-Sum(isnull(fve_AMount,0)) from tbl_Fin_AccountOpeningBalance a inner join tbl_fin_Voucherentry b on b.Fve_FrmTransChildID=a.Fob_ChildID where b.Fve_FrmTransChildID="+child+" and b.Fve_FrmTransID="+selected+" select @balance+isnull(Sum(fve_amount),0) from tbl_Fin_AccountOpeningBalance a inner join tbl_Fin_VoucherEntry b on a.Fob_ChildID=b.Fve_ToTransChildID where Fve_ToTransID="+selected+" and Fve_ToTransChildID="+child;
                    DBManager db = new DBManager();
                    db.Open();
                    decimal Amount = 0;
                    Amount = Convert.ToDecimal(db.ExecuteScalar(CommandType.Text, sql));
                    return Math.Round(Amount);
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | CalculateOpeningBalance(int selected,int child)");
                return 0;
            }
        }
        public DataSet GetDatasetForEdit(int groupID)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = @"select Fve_FrmTransID ParticularsID,Fve_FrmTransChildID CostHead,Round(Fve_Amount,0) DebitAmt,0 creditAmt,Fve_Amount Amount,Fve_FccID CostCenter,b.Fah_Name particulars,
                                    case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit',a.Fve_GroupID,a.Fve_Number,a.Fve_Date from tbl_Fin_VoucherEntry a 
                                    left join tbl_Fin_AccountHead b on a.fve_frmtransID=b.Fah_ID 
                                    left join tbl_fin_accounthead c on a.Fve_ToTransID=c.Fah_ID
                                    where Fve_GroupID=@group and Fve_FrmTransID>0             ----- for debit Account
                                    union all
                                    select fve_toTransID,fve_totransChildID,0 DebitAmt,Round(Fve_Amount,0) creditAmt,fve_Amount,Fve_FccID CostCenter,b.fah_Name,case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit',a.Fve_GroupID,a.Fve_Number,a.Fve_Date
                                    from tbl_fin_VoucherEntry a
                                    inner join tbl_fin_accounthead b on a.Fve_ToTransID=b.Fah_ID
                                    where fve_totransID>0 and fve_groupID=@group";
                DBManager db = new DBManager();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@group", groupID);
                ds = db.ExecuteDataSet(CommandType.Text, sql);
                return ds;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | GetDatasetForEdit(int groupID)");
                return null;
            }
        }

        public static DataSet GetVoucherNumber(int VoucherType)
        {
            DBManager db = new DBManager();
            db.Open();
            try
            {
                //string VoucherNumber = "";
                DataSet ds = new DataSet();
                string sqlcmd = @"DECLARE @VNumbering  INT,      
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
                                       @RCnt   INT,
									   @VoucherNo int    
                                          

                                    -- Voucher Date Generation Settings      
                                          
                                          ---------------18/OCT/2012
                                    --        if @IsCheque=0
                                    --         begin
                                    --          set @ChequeDate=NULL
                                    --          set @ChequeNo=NULL
                                    ----            set 8=2
                                    --         end 
                                             ---------------------
                                    --      if @IsCheque<>0     ---------------21/Nov/2012
                                    --         begin
                                    --          set 8=5
                                    --         end 
                                          
                                     -----------------------------------------------------------Voucher Number Generation      
                                             
                                    --Voucher Type Settings       
                                          
                                     SELECT @VNumbering  = ISNULL(Fvt_Numbering,0),         
                                     @VStartNo  = ISNULL(Fvt_NoStatFrom,0),       
                                     @VReStart  = ISNULL(Fvt_RestartNo,0)       
                                     FROM tbl_Fin_VoucherType       
                                     WHERE Fvt_ID  = @VoucherType       
                                          
                                          
                                          
                                          
                                          
                                     IF @VNumbering = 1        -- Automatic numbering      
                                       BEGIN        
                                        IF @VReStart = 0     -- Restart by Year      
                                          BEGIN        
                                           SET @FStr = CONVERT(VARCHAR,YEAR(getdate()))        
                                            IF MONTH(getdate())<4       
                                       SET @FStr = CONVERT(VARCHAR,YEAR(getdate())-1)        
                                           SET @FStr = '01-APR-' + @FStr        
                                              
                                      SET @TStr = CONVERT(VARCHAR,YEAR(getdate())+1)        
                                            IF MONTH(getdate())<4       
                                       SET @TStr = CONVERT(VARCHAR,YEAR(getdate()))        
                                           SET @TStr = '31-MAR-' + @TStr        
                                            
                                       END        
                                      ELSE IF @VReStart = 1     -- Restart by Month      
                                         BEGIN        
                                          SET @FStr = '01-' +  CONVERT(VARCHAR,LEFT(DATENAME(MONTH,getdate()),3)) + '-' + CONVERT(VARCHAR,YEAR(getdate()))        
                                          SET @TStr =   CONVERT(VARCHAR,LEFT(DATENAME(MONTH,getdate()),3)) + '-' + CONVERT(VARCHAR,YEAR(getdate()))        
                                            
                                          IF MONTH(getdate()) = 2        
                                           BEGIN       
                                             IF (YEAR(getdate()) % 4) = 0       
                                       SET @TStr = '29-' + @TStr       
                                      ELSE       
                                       SET @TStr = '28-' + @TStr        
                                            END        
                                          ELSE IF MONTH(getdate()) = 4 OR MONTH(getdate()) = 6 OR MONTH(getdate()) = 9 OR MONTH(getdate()) = 11         
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
                                                 
                                          
                                          
                                               
                                     SELECT  @VoucherNo = ISNULL(MAX(Fve_Number), 0)+1      
                                      FROM tbl_Fin_VoucherEntry         
                                           WHERE Fve_VoucherType = @VoucherType       
                                      AND (Fve_Date BETWEEN @FDate AND @TDate)  

                                         END   
									select @VoucherNo;
                                    select Fvt_TypeName from tbl_Fin_VoucherType where Fvt_ID=@VoucherType";
                db.CreateParameters(1);
                db.AddParameters(0, "@VoucherType", VoucherType);
                ds=db.ExecuteDataSet(CommandType.Text, sqlcmd);
                //VoucherNumber = ds.Tables[1].Rows[0][0].ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                return ds;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | GetVoucherNumber(int VoucherType)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static DataSet GetDataset(int groupID)
        {
            try
            {
                DataSet ds = new DataSet();
                string sql = @"select Fve_FrmTransID ParticularsID,Fve_FrmTransChildID CostHead,0 DebitAmt,Round(Fve_Amount,0) creditAmt,Fve_Amount Amount,Fve_FccID CostCenter,b.Fah_Name particulars,
                                 case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit',d.Fvt_TypeName+':'+CONVERT(varchar,a.Fve_Number) Voucher,a.Fve_Number,a.Fve_GroupID,a.Fve_Date,
								 isnull(a.Job_ID,0) Job_ID,a.Fve_IsCheque,isnull(a.Fve_ChequeNo,'') Fve_ChequeNo,isnull(a.Fve_ChequeDate,'') Fve_ChequeDate,isnull(Fve_Drawon,'') Fve_Drawon,a.Fve_Description,a.Fve_VoucherType,a.Fve_ExpenseDesc from tbl_Fin_VoucherEntry a 
                                 left join tbl_Fin_AccountHead b on a.fve_frmtransID=b.Fah_ID 
                                 left join tbl_fin_accounthead c on a.Fve_ToTransID=c.Fah_ID
								 inner join tbl_Fin_VoucherType d on a.Fve_VoucherType=d.Fvt_ID
                                 where Fve_GroupID=@group and Fve_FrmTransID>0
                                 union all
                                 select fve_toTransID,fve_totransChildID,Round(Fve_Amount,0) DebitAmt,0 creditAmt,fve_Amount,Fve_FccID CostCenter,b.fah_Name,case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit' ,
								 c.Fvt_TypeName+':'+CONVERT(varchar,a.Fve_Number) Voucher
                                 ,a.Fve_Number,a.Fve_GroupID,a.Fve_Date,isnull(a.Job_ID,0) Job_ID,a.Fve_IsCheque,isnull(a.Fve_ChequeNo,'') Fve_ChequeNo,
								 isnull(a.Fve_ChequeDate,'') Fve_ChequeDate,isnull(Fve_Drawon,'') Fve_Drawon,a.Fve_Description,a.Fve_VoucherType,a.Fve_IncomeDesc from tbl_fin_VoucherEntry a
                                 inner join tbl_fin_accounthead b on a.Fve_ToTransID=b.Fah_ID
								 inner join tbl_Fin_VoucherType c on c.Fvt_ID=a.Fve_VoucherType
                                 where fve_totransID>0 and fve_groupID=@group order by CreditOrDebit asc";
                DBManager db = new DBManager();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@group", groupID);
                ds = db.ExecuteDataSet(CommandType.Text, sql);
                return ds;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | GetDatasetForEdit(int groupID)");
                return null;
            }
        }

        public static DataTable GetDataForPrint(int GroupID,int CompanyID)
        {
            DBManager db = new DBManager();
            try
            {
                string sqlcmd = @"select Fve_FrmTransID ParticularsID,Fve_FrmTransChildID CostHead,0 DebitAmt,Round(Fve_Amount,0) creditAmt,Fve_Amount Amount,Fve_FccID CostCenter,b.Fah_Name particulars,
                                    case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit',d.Fvt_TypeName+':'+CONVERT(varchar,a.Fve_Number) Voucher,e.Company_Id,e.Address1,
									e.Address2,e.City,e.Mobile_No1,f.Name [State],g.Name Country,e.Name Company,a.Fve_Date,e.Reg_Id1,d.Fvt_TypeName +' Entry' Heading from tbl_Fin_VoucherEntry a 
                                    left join tbl_Fin_AccountHead b on a.fve_frmtransID=b.Fah_ID 
                                    left join tbl_fin_accounthead c on a.Fve_ToTransID=c.Fah_ID
									inner join tbl_Fin_VoucherType d on a.Fve_VoucherType=d.Fvt_ID
									inner join TBL_COMPANY_MST e on b.Fah_ComID=e.Company_Id
									inner join TBL_STATE_MST f on f.State_Id=e.State_Id
									inner join TBL_COUNTRY_MST g on g.Country_id=e.country_ID
                                    where Fve_GroupID=@group and Fve_FrmTransID>0 and  b.fah_comID=@Company            ----- for debit Account
                                    union all
                                    select fve_toTransID,fve_totransChildID,Round(Fve_Amount,0) DebitAmt,0 creditAmt,fve_Amount,Fve_FccID CostCenter,b.fah_Name,case when(fve_frmtransID=0) then 1  else 0  end 'CreditOrDebit' ,c.Fvt_TypeName+':'+CONVERT(varchar,a.Fve_Number) Voucher
                                    ,d.Company_Id,d.Address1,
									d.Address2,d.City,d.Mobile_No1,e.Name [State],f.Name Country,d.Name Company,a.Fve_Date,d.Reg_Id1,c.Fvt_TypeName +' Entry' Heading
									from tbl_fin_VoucherEntry a
                                    inner join tbl_fin_accounthead b on a.Fve_ToTransID=b.Fah_ID
									inner join tbl_Fin_VoucherType c on c.Fvt_ID=a.Fve_VoucherType
									inner join tbl_company_mst d on d.company_id=b.fah_comID
									inner join tbl_state_mst e on e.State_ID=d.State_ID
									inner join tbl_country_mst f on f.country_id=d.country_id
                                    where fve_totransID>0 and fve_groupID=@group and b.fah_comID=@Company";
                db.Open();
                DataTable dt = new DataTable();
                db.CreateParameters(2);
                db.AddParameters(0, "@group", GroupID);
                db.AddParameters(1, "@Company", CompanyID);
                dt = db.ExecuteDataSet(CommandType.Text, sqlcmd).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "VoucherEntry | GetDataForPrint(int GroupID,int CompanyID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public DataTable GetJobs(int CompanyId)
        {

            using (DBManager db = new DBManager())
            {

                try
                {
                    db.Open();
                    string query = "SELECT Job_Id,Job_Name FROM TBL_JOB_MST where Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Job | GetJobs(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public DataTable LoadCostCenter()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fcc_ID ID,Fcc_Name name from tbl_fin_costCenter").Tables[0];

                }
                catch (Exception ex)
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

        public DataSet GetVoucherNo(int VoucherType)
        {
            DBManager db = new DBManager();
            db.Open();
            try
            {
                //string VoucherNumber = "";
                DataSet ds = new DataSet();
                string sqlcmd = @"DECLARE @VNumbering  INT,      
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
                                       @RCnt   INT,
									   @VoucherNo int    
                                          

                                    -- Voucher Date Generation Settings      
                                          
                                          ---------------18/OCT/2012
                                    --        if @IsCheque=0
                                    --         begin
                                    --          set @ChequeDate=NULL
                                    --          set @ChequeNo=NULL
                                    ----            set 8=2
                                    --         end 
                                             ---------------------
                                    --      if @IsCheque<>0     ---------------21/Nov/2012
                                    --         begin
                                    --          set 8=5
                                    --         end 
                                          
                                     -----------------------------------------------------------Voucher Number Generation      
                                             
                                    --Voucher Type Settings       
                                          
                                     SELECT @VNumbering  = ISNULL(Fvt_Numbering,0),         
                                     @VStartNo  = ISNULL(Fvt_NoStatFrom,0),       
                                     @VReStart  = ISNULL(Fvt_RestartNo,0)       
                                     FROM tbl_Fin_VoucherType       
                                     WHERE Fvt_ID  = @VoucherType       
                                          
                                          
                                          
                                          
                                          
                                     IF @VNumbering = 1        -- Automatic numbering      
                                       BEGIN        
                                        IF @VReStart = 0     -- Restart by Year      
                                          BEGIN        
                                           SET @FStr = CONVERT(VARCHAR,YEAR(getdate()))        
                                            IF MONTH(getdate())<4       
                                       SET @FStr = CONVERT(VARCHAR,YEAR(getdate())-1)        
                                           SET @FStr = '01-APR-' + @FStr        
                                              
                                      SET @TStr = CONVERT(VARCHAR,YEAR(getdate())+1)        
                                            IF MONTH(getdate())<4       
                                       SET @TStr = CONVERT(VARCHAR,YEAR(getdate()))        
                                           SET @TStr = '31-MAR-' + @TStr        
                                            
                                       END        
                                      ELSE IF @VReStart = 1     -- Restart by Month      
                                         BEGIN        
                                          SET @FStr = '01-' +  CONVERT(VARCHAR,LEFT(DATENAME(MONTH,getdate()),3)) + '-' + CONVERT(VARCHAR,YEAR(getdate()))        
                                          SET @TStr =   CONVERT(VARCHAR,LEFT(DATENAME(MONTH,getdate()),3)) + '-' + CONVERT(VARCHAR,YEAR(getdate()))        
                                            
                                          IF MONTH(getdate()) = 2        
                                           BEGIN       
                                             IF (YEAR(getdate()) % 4) = 0       
                                       SET @TStr = '29-' + @TStr       
                                      ELSE       
                                       SET @TStr = '28-' + @TStr        
                                            END        
                                          ELSE IF MONTH(getdate()) = 4 OR MONTH(getdate()) = 6 OR MONTH(getdate()) = 9 OR MONTH(getdate()) = 11         
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
                                                 
                                          
                                          
                                               
                                     SELECT  @VoucherNo = ISNULL(MAX(Fve_Number), 0)+1       
                                      FROM tbl_Fin_VoucherEntry         
                                           WHERE Fve_VoucherType = @VoucherType       
                                      AND (Fve_Date BETWEEN @FDate AND @TDate)
                                         END   
									select @VoucherNo Number;select Fvt_TypeName from tbl_Fin_VoucherType where Fvt_ID=@VoucherType";
                db.CreateParameters(1);
                db.AddParameters(0, "@VoucherType", VoucherType);
                ds = db.ExecuteDataSet(CommandType.Text, sqlcmd);
                //VoucherNumber = ds.Tables[1].Rows[0][0].ToString() + ":" + ds.Tables[0].Rows[0][0].ToString();
                return ds;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | GetVoucherNumber(int VoucherType)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static DataTable GetAccountHeadsVoucher(int VoucherType, int Credit, int Company)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    if (Credit == 1)
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
    }
}
