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
    public class FinancialTransactions
    {
        #region Properties
        public int pageNumber { get; set; }
        public int recordPerPage { get; set; }
        public int filterType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string fromdatestring { get; set; }
        public string todatestring { get; set; }
        public string TransactionDateString { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int VoucherType { get; set; }
        public int ID { get; set; }
        public int ModifiedBy { get; set; }
        public int FromAccount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public int AccountID { get; set; }
        public int ChildID { get; set; }
        public string ChildText { get; set; }
        public int GroupID { get; set; }
        public DateTime ChequeClearDate { get; set; }
        public int Filter { get; set; }
        #endregion
        public void LoadAccountHeadsTypes(DropDownList ddl)
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string sql = @"SELECT Fah_Name, Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = 3
                           UNION SELECT ' --All Accounts--' AS Fah_Name, 0 AS Fah_ID ORDER BY Fah_Name";
                dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                ddl.DataSource = dt;
                ddl.DataValueField = "Fah_ID";
                ddl.DataTextField = "Fah_Name";
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | LoadAccountHeadsTypes(DropDownList ddl)");
            }
        }
        public static DataTable LoadVoucherTypesForFin()
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                dt = db.ExecuteDataSet(CommandType.Text, "SELECT Fvt_TypeName, Fvt_ID FROM tbl_Fin_VoucherType WHERE Fvt_Disable = 0 ORDER BY Fvt_TypeName").Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | LoadVoucherTypesForFin()");
                return null;
            }
        }
        public DataTable GridViewData()
        {

            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string sql = @"
                            declare @PageNo   INT =4 ,         
                            @RecsPerPage INT =40,
                            @FilterType  INT = @type,  -- 0-Today, 0-LastWeek, 0-LastMonth, 1-All, 0-Custom          
                            @DateFrom DATETIME = @date,          
                            @DateTo  DATETIME = @Todate,          
                            @FromID  INT = @frm,  --June 02 2009,From right Now there is no @ToID         
                            @ToID INT = @to,          
                            @CompanyID INT = @company,      
                            @VoucherType INT =@voucher

                            CREATE TABLE #TransTable          
                            (          
                            TrnID   INT IDENTITY (1,1),          
                            TrnDate   DATETIME,          
                            TrnVchType  VARCHAR(100),          
                            TrnVchID  INT,          
                            TrnVchNo  INT,          
                            TrnAmount  MONEY,          
                            TrnUser   VARCHAR(50),          
                            TrnDesc   VARCHAR(100),          
                            TrnIsDebit  BIT,          
                            TrnGroupID  INT,          
                            TrnFrmID  INT,          
                            TrnFrmCldID  INT,          
                            TrmFChild  VARCHAR(100),          
                            TrnToID   INT,          
                            TrnToCldID  INT,          
                            TrmTChild  VARCHAR(100),          
                            TrnNarration VARCHAR(1000),          
                            TrnIsCheque  BIT,          
                            TrnChequeNo  VARCHAR(50),          
                            TrnChequeDate DATETIME ,    
                            TrnFve_IsVoucher   INT ---(Fve_IsVoucher -21/Nov/2012 for edit voucher )    
                            ) 
                           
                           DECLARE @TmpID   FLOAT,          
                             @FirstRec   INT,           
                             @LastRec   INT,          
                             @AccID   INT,          
                             @AccSQLTable VARCHAR(100),          
                             @AccSQLID  VARCHAR(100),          
                             @AccSQLName  VARCHAR(255),          
                             @SQLStatement VARCHAR(4000)          
                                     
                           SET @TmpID = 0          
                           IF @VoucherType=0      
                           BEGIN          
                            IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate  ,Fve_IsVoucher---(Fve_IsVoucher -21/Nov/2012 for edit voucher )         
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)      
                               ORDER BY Fvt_TypeName          
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate   ,Fve_IsVoucher---(Fve_IsVoucher -21/Nov/2012 for edit voucher )          
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))          
                               ORDER BY Fvt_TypeName          
                             END        
                           END      
                           ELSE      
                           BEGIN      
                           IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate    ,Fve_IsVoucher---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_VoucherType=@VoucherType AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))            
                               ORDER BY Fvt_TypeName       
                                      
                                   
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate     ,Fve_IsVoucher---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))      
                                  AND Fve_VoucherType=@VoucherType                     
                               ORDER BY Fvt_TypeName          
                             END        
                           END        
                                     
                           --UPDATE #TransTable SET TrnFrmID = Fve_FrmTransID, TrnFrmCldID = Fve_FrmTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 0 AND Fve_FrmTransID > 0           
                           UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 0           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)          
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 0 AND Fve_FrmTransID = TrnFrmID          
                              
                            UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)  where    TrnFrmID is null      
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_FrmTransID = TrnFrmID and TrnAmount is null  
                               
                                      
                           --UPDATE #TransTable SET TrnToID = Fve_ToTransID, TrnToCldID = Fve_ToTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 1 AND Fve_ToTransID > 0           
                                     
                           UPDATE #TransTable SET TrnToID = (SELECT TOP 1 Fve_ToTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_ToTransID > 0 ORDER BY Fve_ID)    
                               
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnToCldID = Fve_ToTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_ToTransID = TrnToID          
                                     
                           UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnFrmID           
                           --UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnToID          
                               
                           IF @FromID > 0 AND @ToID > 0         
                           BEGIN      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID AND TrnToID <> @ToID    
                           END        
                           ELSE IF @FromID > 0      
                           BEGIN         
                            --DELETE FROM #TransTable WHERE TrnFrmID <> @FromID      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID --OR  TrnToID <> @FromID    
                           END        
                           ELSE IF @ToID > 0       
                           BEGIN       
                            DELETE FROM #TransTable WHERE TrnToID <> @ToID       
                           END       
                           DECLARE VoucherCursor CURSOR FOR SELECT Fah_ID, Fah_SQLTable, Fah_SQLID, Fah_SQLName FROM tbl_Fin_AccountHead WHERE Fah_SQLTable IS NOT NULL          
                           OPEN VoucherCursor FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                           WHILE @@FETCH_STATUS = 0          
                            BEGIN          
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' ['' + (SELECT ' + @AccSQLName +           
                           --   ' FROM ' + @AccSQLTable + ' WHERE ' + @AccSQLID + ' = #TransTable.TrnFrmCldID OR ' + @AccSQLID + ' = #TransTable.TrnToCldID ) + '']''           
                           --   WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --            
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' [iWARE SQL]'' WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --      
                           --  EXEC SP_SQLEXEC @SQLStatement     
                           PRINT 2    
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmFChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnFrmCldID) WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnFrmCldID > 0'        
                           PRINT @SQLStatement      
                            EXEC SP_SQLEXEC @SQLStatement      
                                
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmTChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnToCldID) WHERE TrnToID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnToCldID > 0'          
                           PRINT @SQLStatement    
                            EXEC SP_SQLEXEC @SQLStatement    
                                     
                             FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                            END          
                           CLOSE VoucherCursor          
                           DEALLOCATE VoucherCursor          
                             PRINT 1        
                           SELECT @TmpID = @PageNo          
                           --SELECT @FirstRec = (@PageNo - 1) * @RecsPerPage          
                           --SELECT @LastRec = (@PageNo * @RecsPerPage + 1)          
                                     
                           --SELECT TrnDate, TrnVchType, TrnVchID, TrnVchNo, TrnUser, TrnDesc, TrnAmount, TrnIsDebit, TrnGroupID,           
                           -- (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo           
                           -- FROM #TransTable WHERE TrnID > @FirstRec AND TrnID < @LastRec ORDER BY TrnDate, TrnVchType, TrnDesc          
                                     
                           SELECT  MAx(TrnID) TrnID, TrnDate, Max(TrnVchType) TrnVchType, MAx(TrnVchID) TrnVchID, MAX(TrnVchNo) TrnVchNo, MAX(TrnAmount) TrnAmount, MAX(TrnUser) TrnUser,     
                            MAX(TrnDesc + ISNULL(' [' + TrmFChild + ']', ISNULL(' [' + TrmTChild + ']', ''))) AS TrnDesc,     
                            TrnIsDebit, TrnGroupID, MAX(TrnFrmID) TrnFrmID, MAX(TrnFrmCldID) TrnFrmCldID, MAX(TrmFChild) TrmFChild, MAX(TrnToID) TrnToID, MAX(TrnToCldID) TrnToCldID, MAX(TrmTChild) TrmTChild, MAX(TrnNarration) TrnNarration, TrnIsCheque, MAX(TrnChequeNo) TrnChequeNo,     
                            MAX(TrnChequeDate) TrnChequeDate, (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo   ,TrnFve_IsVoucher        
                            FROM #TransTable group by TrnGroupID,TrnIsDebit,TrnIsCheque,TrnFve_IsVoucher,TrnDate  ORDER BY trnGroupID desc ";
                db.CreateParameters(7);
                db.AddParameters(0, "@type", this.filterType);
                db.AddParameters(1, "@date", this.fromdatestring);
                db.AddParameters(2, "@Todate", this.todatestring);
                db.AddParameters(3, "@voucher", this.VoucherType);
                db.AddParameters(4, "@company", this.CompanyId);
                db.AddParameters(5, "@frm", this.FromAccount);
                db.AddParameters(6, "@to", 0);
                dt = db.ExecuteQuery(CommandType.Text, sql);
                return dt;

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | GridViewData(GridView gv)");
                return null;
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
                               DELETE FROM TBL_FIN_SUPPLIER_PAYMENTS WHERE Fve_GroupID = @VGroupID  
                                
                               DELETE FROM TBL_FIN_CUSTOMER_RECEIPTS where  Fve_GroupID = @VGroupID  
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
                            return new OutputMessage("Financial transaction deleted successfully", true, Type.NoError, "FinancialTransactions | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Financial transaction could not be deleted", false, Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Id must not be empty", false, Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    return new OutputMessage("You cannot delete", false, Entities.Type.Others, "FinancialTransactions | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {

                    db.Close();

                }

            }
        }
        public void LoadChildHead(int Selectedhead, DropDownList ddlChildHead, HiddenField hiddenTableName, TextBox rowCostHead)
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string _query = "SELECT Fah_DataSQL, Fah_Type, Fah_SQLTable  FROM tbl_Fin_AccountHead WHERE Fah_ID = " + Selectedhead;
                dt = db.ExecuteQuery(CommandType.Text, _query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        row = dt.Rows[i];
                        hiddenTableName.Value = Convert.ToString(row["Fah_SQLTable"]);
                        if (Convert.ToString(row["Fah_Type"]) == "1")
                        {
                            return;
                        }
                        if (Convert.ToString(row["Fah_DataSQL"]) != "")
                        {
                            //DBManager db = new DBManager();
                            DataTable ds = new DataTable();
                            ds = db.ExecuteQuery(CommandType.Text, Convert.ToString(row["Fah_DataSQL"]));
                            ddlChildHead.Items.Clear();
                            rowCostHead.Visible = true;
                            if (ds != null && ds.Rows.Count > 0)
                            {
                                DataRow row1;
                                for (int j = 0; j < ds.Rows.Count; j++)
                                {
                                    row1 = ds.Rows[j];
                                    ddlChildHead.Items.Insert(j, new ListItem(Convert.ToString(row1[0]), Convert.ToString(row1[1])));
                                }
                            }
                        }
                        else
                        {
                            ddlChildHead.Items.Clear();
                            ddlChildHead.Items.Add(new ListItem("--Select--", "0"));
                            //cmdConsoleLedger.Visible = false;
                            rowCostHead.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | LoadChildHead(int Selectedhead, DropDownList ddlChildHead, HiddenField hiddenTableName, TextBox rowCostHead)");
            }
        }
        public string BindGrid(string hiddenChildId, string ddlChildHead, string ddlTransHead, string txtFromDate, string txtToDate, string ddlCostCenter)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string Costcenter = "";
                string _query = "";
                DataTable ds = new DataTable();
                DataTable dsTemp = new DataTable();
                int RecCnt = 0;
                double OpenBal = 0;
                bool IsDebit = false;
                double TotCredit = 0, TotDebit = 0, TotBal = 0;
                if (ddlChildHead != "0")
                    hiddenChildId = ddlChildHead;
                else
                    hiddenChildId = "0";
                if (ddlCostCenter == "0")
                {
                    Costcenter = "0";
                }
                else
                {
                    Costcenter = ddlCostCenter;
                }
                _query = "SP_Fin_AccountHeadTrans '" + ddlTransHead + "', '" + txtFromDate + "', " +
                        "'" + txtToDate + "', '" + hiddenChildId + "'" + ",0," + Costcenter;
                ds = db.ExecuteQuery(CommandType.Text, _query);
                System.Text.StringBuilder strGridData = new System.Text.StringBuilder();
                //strGridData.Append("<table cellpadding='0' cellspacing='0' border='1' width='98%'>");
                //strGridData.Append("<table class='table table - striped table - bordered dataTable' id='datatable' role='grid' aria-describedby='datatable_info'>");
                //strGridData.Append("<tr role='row'>");
                //strGridData.Append("<td>Date</td><td>Particulars</td><td>Debit</td><td>Credit</td></tr>");//<td>Narration</td><td>Cheque No</td><td>Cheque Date</td></tr>");
                strGridData.Append("<tr><td>Date</td><td>Particulars</td><td>Debit</td><td>Credit</td><td>Narration</td><td>Cheque No</td><td>Cheque Date</td><td >ReceiptNo</td><td>VoucherNo</td></tr>");
                if (ds != null && ds.Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        row = ds.Rows[i];
                        if (RecCnt == 0 && Convert.ToString(row["TransType"]) != "2")
                        {
                            if (Convert.ToString(row["AccNature"]) == "0" || Convert.ToString(row["AccNature"]) == "2")
                            {
                                if (Convert.ToString(row["CreditAmt"]) != "")
                                {
                                    OpenBal = Convert.ToDouble(row["CreditAmt"]);
                                    IsDebit = true;
                                }
                                else
                                {
                                    OpenBal = Convert.ToDouble(row["DebitAmt"]);
                                    IsDebit = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToString(row["DebitAmt"]) != "")
                                {
                                    OpenBal = Convert.ToDouble(row["DebitAmt"]);
                                    IsDebit = false;
                                }
                                else
                                {
                                    OpenBal = Convert.ToDouble(row["CreditAmt"]);
                                    IsDebit = true;
                                }
                            }
                            RecCnt = RecCnt + 1;
                            continue;
                        }
                        if (Convert.ToString(row["ChequeDate"]) != "")
                        {
                            //if (Convert.ToInt32(row["Isclear"]) == 0)
                            //{
                            //    strGridData.Append("<tr  bgcolor=#FFCC99>");
                            //}
                            if (Convert.ToInt32(row["Isclear"]) == 1)
                            {
                                strGridData.Append("<tr  bgcolor=#CCFF99>");
                            }
                            if (Convert.ToInt32(row["IsBounce"]) == 1)
                            {
                                strGridData.Append("<tr  bgcolor=#FFCC99>");
                            }
                        }
                        if (Convert.ToString(row["TransDate"]) != "")
                            strGridData.Append("<td>" + string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(row["TransDate"])) + "</td>");
                        else
                            strGridData.Append("<td>&nbsp;</td>");
                        if (Convert.ToString(row["GroupId"]) != "" && Convert.ToString(row["TransDate"]) != "")
                        {
                            //strGridData.Append("<td nowrap><a title='" + Convert.ToString(row["TransDesc"]) + "' class='hyperLinks' href='#' onclick=\"javascript:OpenTransaction('" + Convert.ToString(row["GroupId"]) + "','" + string.Format("{0:dd/MMM/yyyy}", Convert.ToDateTime(row["TransDate"])) + "','','');\">" + Convert.ToString(row["TransDesc"]) + "</a></td>");
                            string Desc = Convert.ToString(row["TransDesc"]);
                            if (Desc.Length > 35)
                            {
                                Desc = Desc.Insert(35, "<br>");
                            }
                            if (Convert.ToString(row["AccountID"]) != "0")
                            {
                                if (Convert.ToString(row["AccountID"]) == "2" || Convert.ToString(row["AccountID"]) == "1" )
                                {
                                    strGridData.Append("<td><a title='" + Convert.ToString(row["TransDesc"]) + "' class='hyperLinks' href='../Sales/Entry?UID=" + Convert.ToString(row["GroupId"]) + "&MODE=edit'>" + Desc + "</a></td>");
                                }
                                else if(Convert.ToString(row["AccountID"]) == "8" || Convert.ToString(row["AccountID"]) == "7")
                                {
                                    string a = Convert.ToString(row["AccountID"]);
                                    strGridData.Append("<td><a title='" + Convert.ToString(row["TransDesc"]) + "' class='hyperLinks' href='../Purchase/Entry?UID=" + Convert.ToString(row["GroupId"]) + "&MODE=edit'>" + Desc + "</a></td>");
                                }
                                else
                                {
                                    strGridData.Append("<td><a title='" + Convert.ToString(row["TransDesc"]) + "' class='hyperLinks' href='../Finance/Journal?ID=" + Convert.ToString(row["GroupId"]) + "'>" + Desc + "</a></td>");
                                }
                            }
                            else
                            {
                                strGridData.Append("<td><a title='" + Convert.ToString(row["TransDesc"]) + "' class='hyperLinks' href='../Finance/Journal?ID=" + Convert.ToString(row["GroupId"]) + "'>" + Desc + "</a></td>");
                            }
                        }
                        else
                        {
                            //strGridData.Append("<td nowrap>" + Convert.ToString(row["TransDesc"]) + "</td>");
                            string Desc = Convert.ToString(row["TransDesc"]);
                            if (Desc.Length > 35)
                            {
                                Desc = Desc.Insert(35, "<br>");
                            }
                            strGridData.Append("<td>" + Desc + "</td>");
                        }
                        if (Convert.ToString(row["CreditAmt"]) != "")
                        {
                            strGridData.Append("<td style='text-align:right'>" + string.Format("{0:n2}", Convert.ToDouble(row["CreditAmt"])) + "</td>");
                            TotCredit = TotCredit + Convert.ToDouble(row["CreditAmt"]);
                        }
                        else
                            strGridData.Append("<td>&nbsp;</td>");
                        if (Convert.ToString(row["DebitAmt"]) != "")
                        {
                            strGridData.Append("<td style='text-align:right'>" + string.Format("{0:n2}", Convert.ToDouble(row["DebitAmt"])) + "</td>");
                            TotDebit = TotDebit + Convert.ToDouble(row["DebitAmt"]);
                        }
                        else
                            strGridData.Append("<td>&nbsp;</td>");
                        if (Convert.ToString(row["ChequeDate"]) != "")
                        {
                            strGridData.Append("<td >" + Convert.ToString(row["Narration"]) + "&nbsp; </td>");
                            strGridData.Append("<td>" + Convert.ToString(row["ChequeNo"]) + "&nbsp;</td>");
                        }
                        else
                        {
                            strGridData.Append("<td>" + Convert.ToString(row["Narration"]) + "&nbsp;</td>");
                            strGridData.Append("<td>" + Convert.ToString(row["ChequeNo"]) + "&nbsp;</td>");
                        }
                        if (Convert.ToString(row["ChequeDate"]) != "")
                        {
                            strGridData.Append("<td>" + string.Format("{0:dd/MMM/yyyy}", row["ChequeDate"]) + "</td>");
                        }
                        else
                            strGridData.Append("<td>&nbsp;</td>");
                        if (Convert.ToString(row["ReceiptNo"]) != "")
                        {
                            strGridData.Append("<td >" + Convert.ToString(row["ReceiptNo"]) + "&nbsp; </td>");
                            strGridData.Append("<td>" + Convert.ToString(row["VoucherNo"]) + "&nbsp;</td>");
                        }
                        else
                        {
                            strGridData.Append("<td>&nbsp;</td>");
                            strGridData.Append("<td>&nbsp;</td>");
                        }
                        strGridData.Append("</tr>");
                    }
                    //Opening Balance
                    strGridData.Append("<tr class='boldText'>");
                    strGridData.Append("<td>&nbsp;</td><td align='right' nowrap><b>Opening Balance:</td>");
                    if (OpenBal > 0)
                    {
                        if (IsDebit == true)
                            strGridData.Append("<td align='right'><b>" + string.Format("{0:n2}", OpenBal) + "</td><td>&nbsp;</td>");
                        else
                            strGridData.Append("<td>&nbsp;</td><td align='right'><b>" + string.Format("{0:n2}", OpenBal) + "</td>");
                    }
                    else
                    {
                        IsDebit = !IsDebit;
                        OpenBal = OpenBal * -1;
                        if (IsDebit == true)
                            strGridData.Append("<td align='right'><b>" + string.Format("{0:n2}", OpenBal) + "</td><td>&nbsp;</td>");
                        else
                            strGridData.Append("<td>&nbsp;</td><td align='right'><b>" + string.Format("{0:n2}", OpenBal) + "</td>");
                    }
                    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    //strGridData.Append("</tr>");
                    //Current Totals
                    strGridData.Append("<tr class='boldText'>");
                    strGridData.Append("<td>&nbsp;</td><td align='right' nowrap><b>Current Total:</td>");
                    strGridData.Append("<td align='right'><b>" + string.Format("{0:n2}", TotCredit) + "</td><td align='right'><b>" + string.Format("{0:n2}", TotDebit) + "</td>");
                    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
                    //strGridData.Append("</tr>");
                    //Closing Balance
                    if (!IsDebit)
                        TotDebit = TotDebit + OpenBal;
                    else
                        TotCredit = TotCredit + OpenBal;
                    TotBal = TotCredit - TotDebit;
                    strGridData.Append("<tr class='boldText'>");
                    strGridData.Append("<td>&nbsp;</td><td align='right' nowrap><b>Closing Balance:</td>");
                    if (TotBal > 0)
                        strGridData.Append("<td align='right'><b>" + string.Format("{0:n2}", TotBal) + "</td><td>&nbsp;</td>");
                    else
                        strGridData.Append("<td>&nbsp;</td><td align='right'><b>" + string.Format("{0:n2}", (TotBal * -1)) + "</td>");
                    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>");
                    //strGridData.Append("</tr>");
                    //strGridData.Append("</table>");
                }
                return strGridData.ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | BindGrid(HiddenField hiddenChildId, DropDownList ddlChildHead, DropDownList ddlTransHead, TextBox txtFromDate, TextBox txtToDate, DropDownList ddlCostCenter, TextBox rowCostHead)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public string GetFinancialYear()
        {
            DBManager db = new DBManager();
            db.Open();
            string date = "";
            string sql = "select KeyValue from TBL_SETTINGS where Settings_Id=1";
            DataTable dt = db.ExecuteQuery(CommandType.Text, sql);
            int a, month;
            a = Convert.ToInt32(dt.Rows[0][0]);
            if (a == 1)
            {
                month = Convert.ToInt32(DateTime.Now.Month);
                if (month > 3)
                {
                    date = "01-Apr-" + DateTime.Now.Year.ToString();
                }
                else
                {
                    date = "01-Apr-" + (DateTime.Now.Year - 1).ToString();
                }
            }
            else
            {
                date = "01-Jan-" + DateTime.Now.Year.ToString();
            }
            return date;
        }
        public DataTable ShowDailyStatement()
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                db.CreateParameters(3);
                db.AddParameters(0, "@Fromdate", this.FromDate.ToString("dd/MM/yyyy"));
                db.AddParameters(1, "@Todate", this.ToDate.ToString("dd/MM/yyyy"));
                db.AddParameters(2, "@ShowTable", Convert.ToInt32(0));
                dt = db.ExecuteQuery(CommandType.Text, "USP_GetDailyStatementCash @Fromdate,@Todate,@ShowTable");
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | ShowDailyStatement()");
                return null;
            }
        }

        public DataTable SelectDistinct(string TableName, DataTable SourceTable, string FieldName)
        {
            DataTable dt = new DataTable(TableName);
            DataSet ds = new DataSet();
            dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);

            object LastValue = null;
            foreach (DataRow dr in SourceTable.Select("", FieldName))
            {
                if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                {
                    LastValue = dr[FieldName];
                    dt.Rows.Add(new object[] { LastValue });
                }
            }
            if (ds != null)
                ds.Tables.Add(dt);
            return dt;
        }
        private bool ColumnEqual(object A, object B)
        {

            // Compares two values to see if they are equal. Also compares DBNULL.Value.
            // Note: If your DataTable contains object fields, then you must extend this
            // function to handle them in a meaningful way if you intend to group on them.

            if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value
                return true;
            if (A == DBNull.Value || B == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (A.Equals(B));  // value type standard comparison
        }
        /// <summary>
        /// For Final Accounts Profit and Loss
        /// </summary>
        public string GetProfitAndLoss(HiddenField hiddenReportId, int reportId, HiddenField hiddenCompanyId, TextBox txtFrom, TextBox txtTo, DropDownList ddlCostCenter)
        {
            try
            {
                String Grp = "";
                String PrevGrp = "";
                String RightGrp = "";
                String PrevRightGrp = "";
                DBManager db = new DBManager();
                DataTable dtDistinctGrp = new DataTable();
                db.Open();
                DataTable ds = new DataTable();
                hiddenReportId.Value = Convert.ToString(reportId);
                string _query = "EXEC Sp_Fin_GenerateFinalAccounts " + hiddenCompanyId.Value + ", '" + txtFrom.Text + "','" + txtTo.Text + "'," + Convert.ToString(reportId) + ",''," + ddlCostCenter.SelectedValue;
                ds = db.ExecuteQuery(CommandType.Text, _query);
                System.Text.StringBuilder strGridData = new System.Text.StringBuilder();
                strGridData.Append("<table class='table table-hover table-striped' cellpadding='0' cellspacing='0' border='1' width='99%'>");
                strGridData.Append("<tr  class='leftColumnLink'>");
                if (reportId == 0)
                    strGridData.Append("<td><b>Particulars</td><td><b>Amount</td><td><b>Particulars</td><td><b>Amount</td></tr>");
                else
                    strGridData.Append("<td><b>Liabilities</td><td><b>Amount</td><td><b>Assets</td><td><b>Amount</td></tr>");
                dtDistinctGrp = SelectDistinct("Group", ds, "OutPurGrp");
                if (dtDistinctGrp != null && dtDistinctGrp.Rows.Count > 0)
                {
                    DataRow row;
                    for (int j = 0; j < dtDistinctGrp.Rows.Count; j++)
                    {
                        strGridData.Append("<tr>");
                        row = dtDistinctGrp.Rows[j];
                        DataRow[] rows = ds.Select("OutPurGrp=" + Convert.ToString(row["OutPurGrp"]));
                        DataRow[] rightRows = ds.Select("AcID<>-1 AND RptPos=1 AND OutPurGrp=" + Convert.ToString(row["OutPurGrp"]));
                        int rightRowCount = 0;
                        PrevGrp = "";
                        PrevRightGrp = "";
                        if (rows.Length > 0)
                        {
                            //  PrevGrp = Convert.ToString(rows["GrpName"]);
                            foreach (DataRow singleRow in rows)
                            {
                                //left side

                                if (Convert.ToString(singleRow["RptPos"]) == "0" && string.Format("{0:F2}", singleRow["CrAmount"]) == "0.00" && Convert.ToString(singleRow["AcID"]) != "-1")
                                {

                                    Grp = Convert.ToString(singleRow["GrpName"]);

                                    if (Convert.ToString(singleRow["AcID"]) == "1")
                                    {
                                        if (PrevGrp != Grp)
                                        {
                                            strGridData.Append("<tr><td class='boldText'><b>" + Grp + "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
                                            PrevGrp = Grp;
                                        }
                                    }
                                    strGridData.Append("<tr>");
                                    strGridData.Append("<td>" + Convert.ToString(singleRow["HeadName"]) + "</td><td align='right'>" + string.Format("{0:n2}", singleRow["DrAmount"]) + "</td>");




                                    //strGridData.Append("<td></td><td>&nbsp;</td><td>&nbsp;</td>");
                                    // right side
                                    if (rightRows.Length > 0 && rightRowCount < rightRows.Length)
                                    {
                                        RightGrp = Convert.ToString(rightRows[rightRowCount]["GrpName"]);
                                        if (Convert.ToString(singleRow["AcID"]) == "1")
                                        {
                                            if (PrevRightGrp != RightGrp)
                                            {
                                                strGridData.Append("<td nowrap class='boldText'><b>" + RightGrp + "</td><td></td>");
                                                PrevRightGrp = RightGrp;
                                            }
                                            else
                                            {

                                                strGridData.Append("<td nowrap>" + Convert.ToString(rightRows[rightRowCount]["HeadName"]) + "</td><td align='right' nowrap>" + string.Format("{0:n2}", rightRows[rightRowCount]["CrAmount"]) + "</td>");
                                                rightRowCount = rightRowCount + 1;

                                            }
                                        }
                                        else
                                        {
                                            strGridData.Append("<td nowrap>" + Convert.ToString(rightRows[rightRowCount]["HeadName"]) + "</td><td align='right' nowrap>" + string.Format("{0:n2}", rightRows[rightRowCount]["CrAmount"]) + "</td>");
                                            rightRowCount = rightRowCount + 1;
                                            PrevRightGrp = RightGrp;
                                        }

                                    }
                                    else
                                    {
                                        strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td>");
                                    }
                                    strGridData.Append("</tr>");
                                    //}
                                }
                                // right side
                                //else if (Convert.ToString(singleRow["RptPos"]) == "1" && string.Format("{0:F2}", singleRow["DrAmount"]) == "0.00" && Convert.ToString(singleRow["AcID"]) != "-1")
                                //{
                                //    strGridData.Append("<tr>");
                                //    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td>");
                                //    strGridData.Append("<td></td><td>" + Convert.ToString(singleRow["HeadName"]) + "</td><td align='right'>" + string.Format("{0:n2}", singleRow["CrAmount"]) + "</td>");
                                //    strGridData.Append("</tr>");
                                //}
                                //left side heading
                                else if (Convert.ToString(singleRow["RptPos"]) == "0" && string.Format("{0:F2}", singleRow["CrAmount"]) == "0.00" && Convert.ToString(singleRow["AcID"]) == "-1")
                                {
                                    while (rightRowCount < rightRows.Length)
                                    {
                                        strGridData.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td nowrap>" + Convert.ToString(rightRows[rightRowCount]["HeadName"]) + "</td><td align='right' nowrap>" + string.Format("{0:n2}", rightRows[rightRowCount]["CrAmount"]) + "</td></tr>");
                                        rightRowCount = rightRowCount + 1;
                                    }
                                    strGridData.Append("<tr>");
                                    strGridData.Append("<td><b>" + Convert.ToString(singleRow["HeadName"]) + "</b></td><td align='right'><b>" + string.Format("{0:n2}", singleRow["DrAmount"]) + "</b></td>");
                                    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td>");
                                    strGridData.Append("</tr>");
                                }
                                //right side heading
                                else if (Convert.ToString(singleRow["RptPos"]) == "1" && string.Format("{0:F2}", singleRow["DrAmount"]) == "0.00" && Convert.ToString(singleRow["AcID"]) == "-1")
                                {
                                    while (rightRowCount < rightRows.Length)
                                    {
                                        strGridData.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td></td><td nowrap>" + Convert.ToString(rightRows[rightRowCount]["HeadName"]) + "</td><td align='right' nowrap>" + string.Format("{0:n2}", rightRows[rightRowCount]["CrAmount"]) + "</td></tr>");
                                        rightRowCount = rightRowCount + 1;
                                    }
                                    strGridData.Append("<tr>");
                                    strGridData.Append("<td>&nbsp;</td><td>&nbsp;</td>");
                                    strGridData.Append("<td><b>" + Convert.ToString(singleRow["HeadName"]) + "</b></td><td align='right'><b>" + string.Format("{0:n2}", singleRow["CrAmount"]) + "</b></td>");
                                    strGridData.Append("</tr>");
                                }
                                //total heading
                                else if (Convert.ToString(singleRow["RptPos"]) == "" && string.Format("{0:F2}", singleRow["DrAmount"]) != "0.00" && string.Format("{0:F2}", singleRow["CrAmount"]) != "0.00" && Convert.ToString(singleRow["AcID"]) == "-1")
                                {
                                    while (rightRowCount < rightRows.Length)
                                    {
                                        strGridData.Append("<tr><td>&nbsp;</td><td>&nbsp;</td><td nowrap>" + Convert.ToString(rightRows[rightRowCount]["HeadName"]) + "</td><td align='right' nowrap>" + string.Format("{0:n2}", rightRows[rightRowCount]["CrAmount"]) + "</td></tr>");
                                        rightRowCount = rightRowCount + 1;
                                    }
                                    strGridData.Append("<tr>");
                                    strGridData.Append("<td class='boldText'><b>" + Convert.ToString(singleRow["HeadName"]) + "</td><td class='boldText' align='right'><b>" + string.Format("{0:n2}", singleRow["DrAmount"]) + "</td>");
                                    strGridData.Append("<td class='boldText'><b>" + Convert.ToString(singleRow["HeadName"]) + "</td><td class='boldText' align='right'><b>" + string.Format("{0:n2}", singleRow["CrAmount"]) + "</td>");
                                    strGridData.Append("</tr>");
                                    strGridData.Append("<tr><td colspan='4' style='height:8px'></td></tr>");
                                }
                            }
                        }
                    }
                }
                return strGridData.ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | GetProfitAndLoss(HiddenField hiddenReportId, int reportId, HiddenField hiddenCompanyId, TextBox txtFrom, TextBox txtTo, DropDownList ddlCostCenter)");
                return null;
            }
        }
        public string getbalanceSheet(HiddenField hiddenCompanyId, HiddenField hiddenReportId, TextBox txtFrom, TextBox txtTo, DropDownList ddlCostCenter, int reportId)
        {
            try
            {
                DataSet ds = new DataSet();
                hiddenReportId.Value = Convert.ToString(reportId);
                string _query = "EXEC Sp_Fin_GenerateFinalAccounts_BS " + hiddenCompanyId.Value + ", '" + txtFrom.Text + "','" + txtTo.Text + "'," + Convert.ToString(reportId) + ",''," + ddlCostCenter.SelectedValue;
                DBManager db = new DBManager();
                db.Open();
                string GroupName = "", prevGroupName = "";
                ds = db.ExecuteDataSet(CommandType.Text, _query);
                System.Text.StringBuilder strGridData = new System.Text.StringBuilder();
                strGridData.Append("<table class='table table-hover table-striped table-bordered' cellpadding='0' cellspacing='0' border='1' width='99%'>");
                strGridData.Append("<tr  class='leftColumnLink'>");
                strGridData.Append("<td><b>Liabilities</td><td align='right'><b>Amount</td><td></td><td><b>Assets</td><td align='right'><b>Amount</td></tr>");
                strGridData.Append("<tr><td colspan='2' valign='top' align='left'><table cellpadding='0' cellspacing='0' width='100%'>");
                //Left Side
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        row = ds.Tables[0].Rows[i];
                        if (Convert.ToString(row["GroupId"]) != "")
                        {
                            _query = "SELECT Fag_Name FROM tbl_fin_accountgroup WHERE Fag_ID=" + Convert.ToString(row["GroupId"]);
                            GroupName = Convert.ToString(db.ExecuteScalar(CommandType.Text, _query));
                        }
                        else
                            GroupName = Convert.ToString(row["HeadName"]);
                        if (GroupName != prevGroupName)
                        {
                            strGridData.Append("<tr><td align='left'><b>" + GroupName + "</td><td></td></tr>");
                            prevGroupName = GroupName;
                        }
                        strGridData.Append("<tr><td align='left'>&nbsp;&nbsp;&nbsp;" + Convert.ToString(row["HeadName"]) + "</td><td align='right'>" + string.Format("{0:n2}", row["DrAmount"]) + "</td></tr>");
                    }
                    strGridData.Append("</tr></table>");
                    ////Total Dr
                    //if (ds.Tables[2].Rows.Count > 0)
                    //{
                    //    row = ds.Tables[2].Rows[0];
                    //    strGridData.Append("<tr><td><b>" + Convert.ToString(row["HeadName"]) + "</td><td><b>" + Convert.ToString(row["DrAmount"]) + "</td></tr></table>");
                    //}
                }
                GroupName = "";
                prevGroupName = "";
                strGridData.Append("</td><td></td><td colspan='2' valign='top'><table cellpadding='0' cellspacing='0' width='100%'>");
                //Right Side
                if (ds != null && ds.Tables[1].Rows.Count > 0)
                {
                    DataRow row;
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        row = ds.Tables[1].Rows[i];
                        if (Convert.ToString(row["GroupId"]) != "")
                        {
                            _query = "SELECT Fag_Name FROM tbl_fin_accountgroup WHERE Fag_ID=" + Convert.ToString(row["GroupId"]);
                            GroupName = db.ExecuteScalar(CommandType.Text, _query).ToString(); ;
                        }
                        else
                            GroupName = Convert.ToString(row["HeadName"]);

                        if (GroupName != prevGroupName)
                        {
                            strGridData.Append("<tr><td align='left'><b>" + GroupName + "</td><td></td></tr>");
                            prevGroupName = GroupName;
                        }
                        strGridData.Append("<tr><td align='left'>&nbsp;&nbsp;&nbsp;" + Convert.ToString(row["HeadName"]) + "</td><td align='right'>" + string.Format("{0:n2}", row["CrAmount"]) + "</td></tr>");
                    }
                    strGridData.Append("</tr></table>");
                }
                //Total Dr & Cr
                DataRow rowDrCr;
                if (ds.Tables[2].Rows.Count > 0)
                {
                    rowDrCr = ds.Tables[2].Rows[0];
                    //strGridData.Append("<tr><td></td><td></td><td></td><td></td><td></td></tr>");
                    strGridData.Append("<tr><td><b>" + Convert.ToString(rowDrCr["HeadName"]) + "</td><td align='right'><b>" + string.Format("{0:n2}", rowDrCr["DrAmount"]) + "</td><td></td>");
                    strGridData.Append("<td><b>" + Convert.ToString(rowDrCr["HeadName"]) + "</td><td align='right'><b>" + string.Format("{0:n2}", rowDrCr["CrAmount"]) + "</td></tr>");
                }
                strGridData.Append("</table>");
                return strGridData.ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | getbalanceSheet(HiddenField hiddenCompanyId, HiddenField hiddenReportId, TextBox txtFrom, TextBox txtTo, DropDownList ddlCostCenter, int reportId)");
                return null;
            }
        }
        /// <summary>
        /// Used to return customer details for transation list in customer list
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public static List<FinancialTransactions> GetDetails(int customer_id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"SELECT * FROM [dbo].[tbl_Fin_TransactionEntry] where Fte_FahID=2 and [Fte_ChildID]=@customer_id";
                db.CreateParameters(1);
                db.AddParameters(0, "@customer_id", customer_id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<FinancialTransactions> result = new List<FinancialTransactions>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        FinancialTransactions finantialtransaction = new FinancialTransactions();
                        finantialtransaction.DebitAmount = item["Fte_DrAmt"] != DBNull.Value ? Convert.ToDecimal(item["Fte_DrAmt"]) : 0;
                        finantialtransaction.CreditAmount = item["Fte_CrAmt"] != DBNull.Value ? Convert.ToDecimal(item["Fte_CrAmt"]) : 0;
                        finantialtransaction.Description = Convert.ToString(item["Fte_Desc"]);
                        finantialtransaction.TransactionDateString = item["Fte_Date"] != DBNull.Value ? Convert.ToDateTime(item["Fte_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        result.Add(finantialtransaction);
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
                Application.Helper.LogException(ex, "Customer |  GetDetails(int customer_id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


        public static List<FinancialTransactions> GetDetailsForCustomerPayment(int customer_id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select * from tbl_Fin_VoucherEntry where Fve_FrmTransID=2 and Fve_FrmTransChildID=@customer_id and Fve_VoucherType=3";
                db.CreateParameters(1);
                db.AddParameters(0, "@customer_id", customer_id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<FinancialTransactions> result = new List<FinancialTransactions>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        FinancialTransactions finantialtransaction = new FinancialTransactions();
                        finantialtransaction.DebitAmount = item["Fve_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Fve_Amount"]) : 0;
                        finantialtransaction.CreditAmount = item["Fve_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Fve_Amount"]) : 0;
                        finantialtransaction.Description = Convert.ToString(item["Fve_Description"]);
                        finantialtransaction.TransactionDateString = item["Fve_Date"] != DBNull.Value ? Convert.ToDateTime(item["Fve_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        result.Add(finantialtransaction);
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
                Application.Helper.LogException(ex, "Customer |  GetDetails(int customer_id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<FinancialTransactions> GetDetailsSupplierwise(int SupplierId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select * from tbl_Fin_TransactionEntry where Fte_FahID=8 and Fte_ChildID=@Supplier_Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Supplier_Id", SupplierId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<FinancialTransactions> result = new List<FinancialTransactions>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        FinancialTransactions finantialtransaction = new FinancialTransactions();
                        finantialtransaction.DebitAmount = item["Fte_DrAmt"] != DBNull.Value ? Convert.ToDecimal(item["Fte_DrAmt"]) : 0;
                        finantialtransaction.CreditAmount = item["Fte_CrAmt"] != DBNull.Value ? Convert.ToDecimal(item["Fte_CrAmt"]) : 0;
                        finantialtransaction.Description = Convert.ToString(item["Fte_Desc"]);
                        finantialtransaction.TransactionDateString = item["Fte_Date"] != DBNull.Value ? Convert.ToDateTime(item["Fte_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        result.Add(finantialtransaction);
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
                Application.Helper.LogException(ex, "FinancialTransactions |  GetDetailsSupplierwise(int SupplierId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public DataTable GetTransactions()
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string sql = @"
                            declare @PageNo   INT =4 ,         
                            @RecsPerPage INT =40,
                            @FilterType  INT = @type,  -- 0-Today, 0-LastWeek, 0-LastMonth, 1-All, 0-Custom          
                            @DateFrom DATETIME = @date,          
                            @DateTo  DATETIME = @Todate,          
                            @FromID  INT = @frm,  --June 02 2009,From right Now there is no @ToID         
                            @ToID INT = @to,          
                            @CompanyID INT = @company,      
                            @VoucherType INT =@voucher

                            CREATE TABLE #TransTable          
                            (          
                            TrnID   INT IDENTITY (1,1),          
                            TrnDate   DATETIME,          
                            TrnVchType  VARCHAR(100),          
                            TrnVchID  INT,          
                            TrnVchNo  INT,          
                            TrnAmount  MONEY,          
                            TrnUser   VARCHAR(50),          
                            TrnDesc   VARCHAR(100),          
                            TrnIsDebit  BIT,          
                            TrnGroupID  INT,          
                            TrnFrmID  INT,          
                            TrnFrmCldID  INT,          
                            TrmFChild  VARCHAR(100),          
                            TrnToID   INT,          
                            TrnToCldID  INT,          
                            TrmTChild  VARCHAR(100),          
                            TrnNarration VARCHAR(1000),          
                            TrnIsCheque  BIT,          
                            TrnChequeNo  VARCHAR(50),          
                            TrnChequeDate DATETIME ,    
                            TrnFve_IsVoucher INT,---(Fve_IsVoucher -21/Nov/2012 for edit voucher )    
							Trn_DescriptionCredit varchar(1000),
							Trn_DescriptionDebit varchar(1000)
                            ) 
                           
                           DECLARE @TmpID   FLOAT,          
                             @FirstRec   INT,           
                             @LastRec   INT,          
                             @AccID   INT,          
                             @AccSQLTable VARCHAR(100),          
                             @AccSQLID  VARCHAR(100),          
                             @AccSQLName  VARCHAR(255),          
                             @SQLStatement VARCHAR(4000)          
                                     
                           SET @TmpID = 0          
                           IF @VoucherType=0      
                           BEGIN          
                            IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,Trn_DescriptionCredit,Trn_DescriptionDebit)          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate  ,Fve_IsVoucher,Fve_ExpenseDesc,Fve_IncomeDesc---(Fve_IsVoucher -21/Nov/2012 for edit voucher )         
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)      
                               ORDER BY Fvt_TypeName          
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,Trn_DescriptionCredit,Trn_DescriptionDebit )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate   ,Fve_IsVoucher,Fve_ExpenseDesc,Fve_IncomeDesc---(Fve_IsVoucher -21/Nov/2012 for edit voucher )          
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))          
                               ORDER BY Fvt_TypeName          
                             END        
                           END      
                           ELSE      
                           BEGIN      
                           IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,Trn_DescriptionCredit,Trn_DescriptionDebit)          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate    ,Fve_IsVoucher,Fve_ExpenseDesc,Fve_IncomeDesc---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_VoucherType=@VoucherType AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))            
                               ORDER BY Fvt_TypeName       
                                      
                                   
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,Trn_DescriptionCredit,Trn_DescriptionDebit )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate     ,Fve_IsVoucher,Fve_ExpenseDesc,Fve_IncomeDesc---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))      
                                  AND Fve_VoucherType=@VoucherType                     
                               ORDER BY Fvt_TypeName          
                             END        
                           END        
                                     
                           --UPDATE #TransTable SET TrnFrmID = Fve_FrmTransID, TrnFrmCldID = Fve_FrmTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 0 AND Fve_FrmTransID > 0           
                           UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 0           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)          
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 0 AND Fve_FrmTransID = TrnFrmID          
                              
                            UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)  where    TrnFrmID is null      
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_FrmTransID = TrnFrmID and TrnAmount is null  
                               
                                      
                           --UPDATE #TransTable SET TrnToID = Fve_ToTransID, TrnToCldID = Fve_ToTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 1 AND Fve_ToTransID > 0           
                                     
                           UPDATE #TransTable SET TrnToID = (SELECT TOP 1 Fve_ToTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_ToTransID > 0 ORDER BY Fve_ID)    
                               
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnToCldID = Fve_ToTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_ToTransID = TrnToID  
                            
                            UPDATE #TransTable SET TrnAmount = Fve_Amount from tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 and Fve_FrmTransID=TrnFrmID        
                                     
                           UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnFrmID           
                           --UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnToID          
                               
                           IF @FromID > 0 AND @ToID > 0         
                           BEGIN      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID AND TrnToID <> @ToID    
                           END        
                           ELSE IF @FromID > 0      
                           BEGIN         
                            --DELETE FROM #TransTable WHERE TrnFrmID <> @FromID      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID --OR  TrnToID <> @FromID    
                           END        
                           ELSE IF @ToID > 0       
                           BEGIN       
                            DELETE FROM #TransTable WHERE TrnToID <> @ToID       
                           END       
                           DECLARE VoucherCursor CURSOR FOR SELECT Fah_ID, Fah_SQLTable, Fah_SQLID, Fah_SQLName FROM tbl_Fin_AccountHead WHERE Fah_SQLTable IS NOT NULL          
                           OPEN VoucherCursor FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                           WHILE @@FETCH_STATUS = 0          
                            BEGIN          
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' ['' + (SELECT ' + @AccSQLName +           
                           --   ' FROM ' + @AccSQLTable + ' WHERE ' + @AccSQLID + ' = #TransTable.TrnFrmCldID OR ' + @AccSQLID + ' = #TransTable.TrnToCldID ) + '']''           
                           --   WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --            
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' [iWARE SQL]'' WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --      
                           --  EXEC SP_SQLEXEC @SQLStatement     
                           PRINT 2    
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmFChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnFrmCldID) WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnFrmCldID > 0'        
                           PRINT @SQLStatement      
                            EXEC SP_SQLEXEC @SQLStatement      
                                
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmTChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnToCldID) WHERE TrnToID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnToCldID > 0'          
                           PRINT @SQLStatement    
                            EXEC SP_SQLEXEC @SQLStatement    
                                     
                             FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                            END          
                           CLOSE VoucherCursor          
                           DEALLOCATE VoucherCursor          
                             PRINT 1        
                           SELECT @TmpID = @PageNo          
                           --SELECT @FirstRec = (@PageNo - 1) * @RecsPerPage          
                           --SELECT @LastRec = (@PageNo * @RecsPerPage + 1)          
                                     
                           --SELECT TrnDate, TrnVchType, TrnVchID, TrnVchNo, TrnUser, TrnDesc, TrnAmount, TrnIsDebit, TrnGroupID,           
                           -- (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo           
                           -- FROM #TransTable WHERE TrnID > @FirstRec AND TrnID < @LastRec ORDER BY TrnDate, TrnVchType, TrnDesc          
                                     
                           SELECT  MAx(TrnID) TrnID,CONVERT(varchar,CAST (TrnDate AS DATE),105) TrnDate, Max(TrnVchType) TrnVchType, MAx(TrnVchID) TrnVchID, MAX(TrnVchNo) TrnVchNo, MAX(TrnAmount) TrnAmount, MAX(TrnUser) TrnUser,     
                            MAX(TrnDesc + ISNULL(' [' + TrmFChild + ']', ISNULL(' [' + TrmTChild + ']', ''))) AS TrnDesc,     
                            TrnIsDebit, TrnGroupID, MAX(TrnFrmID) TrnFrmID, MAX(TrnFrmCldID) TrnFrmCldID, MAX(TrmFChild) TrmFChild, MAX(TrnToID) TrnToID, MAX(TrnToCldID) TrnToCldID, MAX(TrmTChild) TrmTChild, MAX(TrnNarration) TrnNarration, TrnIsCheque, isnull(MAX(TrnChequeNo),'') TrnChequeNo     
                            ,isnull(CONVERT(varchar,CAST (MAX(TrnChequeDate) AS DATE),105),'') TrnChequeDate, (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo   ,TrnFve_IsVoucher ,max(Trn_DescriptionCredit) Trn_DescriptionCredit,max(Trn_DescriptionDebit) Trn_DescriptionDebit       
                            FROM #TransTable group by TrnGroupID,TrnIsDebit,TrnIsCheque,TrnFve_IsVoucher,TrnDate  ORDER BY TrnDate desc";
                db.CreateParameters(7);
                db.AddParameters(0, "@type", this.filterType);
                db.AddParameters(1, "@date", this.fromdatestring);
                db.AddParameters(2, "@Todate", this.todatestring);
                db.AddParameters(3, "@voucher", this.VoucherType);
                db.AddParameters(4, "@company", this.CompanyId);
                db.AddParameters(5, "@frm", this.FromAccount);
                db.AddParameters(6, "@to", 0);
                dt = db.ExecuteQuery(CommandType.Text, sql);
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | GetTransactions()");
                return null;
            }
        }

        /// <summary>
        /// Get Datails For Editing
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAccountGroups(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fag_ID id,Fag_Name Name from tbl_Fin_AccountGroup where Fag_ComID=" + CompanyId).Tables[0];
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetAccountGroups(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// Get Datails For Editing
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public DataTable GetAccountHeads(int ParentID)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fah_ID,Fah_Name from tbl_Fin_AccountHead where fah_fagID=" + ParentID).Tables[0];
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetAccountHeads(int ParentID)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// Get Datails For Editing
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public static DataTable GetAccountGroupsByNature(string Nature, int Company)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    if (Nature != "-1")
                    {
                        return db.ExecuteDataSet(CommandType.Text, "select Fag_ID,Fag_Name from tbl_Fin_AccountGroup where fag_type=" + Nature + "and Fag_ComID=" + Company).Tables[0];
                    }
                    else
                    {
                        return db.ExecuteDataSet(CommandType.Text, "select Fag_ID,Fag_Name from tbl_Fin_AccountGroup where Fag_ComID=" + Company).Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetAccountGroupsByNature(string Nature,int Company)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static DataTable GetAccountHeadsByGroup(int Group, int Company)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    return db.ExecuteDataSet(CommandType.Text, "select Fah_ID,Fah_Name from tbl_Fin_AccountHead where fah_fagID=" + Group).Tables[0];
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetAccountHeadsByGroup(int Group, int Company)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static DataTable GetAccountHeadsChilds(int HeadID, int Company)
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
                                   set @count=(select Count(*) from tbl_Fin_AccountHead where Fah_DataSQL is not null)
                                   while @i<=@count
                                   begin
                                   ;with cte as(
                                   --select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null 
								   select ROW_NUMBER() over(order by fah_id) as Slno,Fah_ID from tbl_Fin_AccountHead where Fah_DataSQL is not null and fah_comID=3)
                                   select @id=Fah_ID from cte where Slno=@i
                                   set @sql=(select Fah_DataSQL from tbl_Fin_AccountHead where fah_id=@id)
                                    insert into #tempnew (name,id) exec(@sql)
                                    update #tempnew set parent=@id
                                    insert into #temptable select * from #tempnew
                                    truncate table #tempnew
                                    set @i=@i+1
                                    end
                                    select name,id,parent,Fah_SQLTable from #temptable a inner join tbl_Fin_AccountHead b on a.id=b.Fah_ID where id=@headID";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@headID", HeadID);
                    dt = db.ExecuteDataSet(CommandType.Text, sql).Tables[0];
                    return dt;
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetAccountHeadsChilds(int HeadID, int Company)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }

        public DataTable GetGroupLedgerData()
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    DataTable dt = new DataTable();
                    string sql = @"Sp_ViewGroupedLedger @AccID,@ChildId,@ChildName,@FrmDate,@ToDate,@GroupId,@ComId";
                    db.CreateParameters(7);
                    db.AddParameters(0, "@AccID", this.AccountID);
                    db.AddParameters(1, "@ChildId", this.ChildID);
                    db.AddParameters(2, "@ChildName", this.ChildText);
                    db.AddParameters(3, "@FrmDate", this.fromdatestring);
                    db.AddParameters(4, "@ToDate", this.todatestring);
                    db.AddParameters(5, "@GroupId", this.GroupID);
                    db.AddParameters(6, "@ComId", this.CompanyId);
                    dt = db.ExecuteProcedure(CommandType.Text, sql);
                    return dt;
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "FinancialTransactions | GetGroupLedgerData(dynamic Params)");
                    return null;

                }
                finally
                {
                    db.Close();
                }
            }
        }


        /// <summary>
        /// This Function is used to get the transactions involves cheque transactions
        /// </summary>
        /// <returns></returns>
        public DataTable GetChequeTransactions()
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string sql = @"
                            declare @PageNo   INT =4 ,         
                            @RecsPerPage INT =40,
                            @FilterType  INT = @type,  -- 0-Today, 0-LastWeek, 0-LastMonth, 1-All, 0-Custom          
                            @DateFrom DATETIME = @date,          
                            @DateTo  DATETIME = @Todate,          
                            @FromID  INT = @frm,  --June 02 2009,From right Now there is no @ToID         
                            @ToID INT = @to,          
                            @CompanyID INT = @company,      
                            @VoucherType INT =@voucher

                            CREATE TABLE #TransTable          
                            (          
                            TrnID   INT IDENTITY (1,1),          
                            TrnDate   DATETIME,          
                            TrnVchType  VARCHAR(100),          
                            TrnVchID  INT,          
                            TrnVchNo  INT,          
                            TrnAmount  MONEY,          
                            TrnUser   VARCHAR(50),          
                            TrnDesc   VARCHAR(100),          
                            TrnIsDebit  BIT,          
                            TrnGroupID  INT,          
                            TrnFrmID  INT,          
                            TrnFrmCldID  INT,          
                            TrmFChild  VARCHAR(100),          
                            TrnToID   INT,          
                            TrnToCldID  INT,          
                            TrmTChild  VARCHAR(100),          
                            TrnNarration VARCHAR(1000),          
                            TrnIsCheque  BIT,          
                            TrnChequeNo  VARCHAR(50),          
                            TrnChequeDate DATETIME ,    
                            TrnFve_IsVoucher   INT, ---(Fve_IsVoucher -21/Nov/2012 for edit voucher )    
							TrnFve_isBounce int,
							TrnFve_IsCleared int,
                            TrnDrawon varchar(50)
                            ) 
                           
                           DECLARE @TmpID   FLOAT,          
                             @FirstRec   INT,           
                             @LastRec   INT,          
                             @AccID   INT,          
                             @AccSQLTable VARCHAR(100),          
                             @AccSQLID  VARCHAR(100),          
                             @AccSQLName  VARCHAR(255),          
                             @SQLStatement VARCHAR(4000)          
                                     
                           SET @TmpID = 0          
                           IF @VoucherType=0      
                           BEGIN          
                            IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,TrnFve_isBounce, TrnFve_IsCleared,TrnDrawon)          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate  ,Fve_IsVoucher,Fve_Isbounce,Fve_IsCleared,Fve_Drawon---(Fve_IsVoucher -21/Nov/2012 for edit voucher )         
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)      
                               ORDER BY Fvt_TypeName          
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,TrnFve_isBounce, TrnFve_IsCleared,TrnDrawon )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate   ,Fve_IsVoucher,Fve_Isbounce,Fve_IsCleared,Fve_Drawon---(Fve_IsVoucher -21/Nov/2012 for edit voucher )          
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))          
                               ORDER BY Fvt_TypeName          
                             END        
                           END      
                           ELSE      
                           BEGIN      
                           IF @FilterType = 1          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,TrnFve_isBounce, TrnFve_IsCleared ,TrnDrawon )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate    ,Fve_IsVoucher,Fve_Isbounce,Fve_IsCleared,Fve_Drawon---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_VoucherType=@VoucherType AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))            
                               ORDER BY Fvt_TypeName       
                                      
                                   
                             END          
                            ELSE          
                             BEGIN          
                              INSERT INTO #TransTable ( TrnDate, TrnVchType, TrnVchNo, TrnUser, TrnIsDebit, TrnGroupID, TrnVchID, TrnNarration, TrnIsCheque, TrnChequeNo, TrnChequeDate,TrnFve_IsVoucher,TrnFve_isBounce, TrnFve_IsCleared,TrnDrawon )          
                               SELECT DISTINCT Fve_Date, Fvt_TypeName, Fve_Number, Fve_ByUser, Fvt_InitDr, Fve_GroupID, Fve_VoucherType, Fve_Description, Fve_IsCheque, Fve_ChequeNo, Fve_ChequeDate     ,Fve_IsVoucher,Fve_Isbounce,Fve_IsCleared,Fve_Drawon---(Fve_IsVoucher -21/Nov/2012 for edit voucher )        
                               FROM tbl_Fin_VoucherEntry INNER JOIN tbl_Fin_VoucherType ON Fve_VoucherType = Fvt_ID           
                               WHERE Fve_Date BETWEEN @DateFrom AND @DateTo           
                               AND (Fve_FrmTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID)          
                               OR Fve_ToTransID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_ComID = @CompanyID))      
                                  AND Fve_VoucherType=@VoucherType                     
                               ORDER BY Fvt_TypeName          
                             END        
                           END        
                                     
                           --UPDATE #TransTable SET TrnFrmID = Fve_FrmTransID, TrnFrmCldID = Fve_FrmTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 0 AND Fve_FrmTransID > 0           
                           UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 0           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)          
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 0 AND Fve_FrmTransID = TrnFrmID          
                              
                            UPDATE #TransTable SET TrnFrmID = (SELECT TOP 1 Fve_FrmTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_FrmTransID > 0 ORDER BY Fve_ID)  where    TrnFrmID is null      
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnFrmCldID = Fve_FrmTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_FrmTransID = TrnFrmID and TrnAmount is null  
                               
                                      
                           --UPDATE #TransTable SET TrnToID = Fve_ToTransID, TrnToCldID = Fve_ToTransChildID           
                           --  FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                           --  AND TrnIsDebit = 1 AND Fve_ToTransID > 0           
                                     
                           UPDATE #TransTable SET TrnToID = (SELECT TOP 1 Fve_ToTransID FROM tbl_Fin_VoucherEntry           
                             WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID AND #TransTable.TrnIsDebit = 1           
                             AND Fve_ToTransID > 0 ORDER BY Fve_ID)    
                               
                           UPDATE #TransTable SET TrnAmount = Fve_Amount, TrnToCldID = Fve_ToTransChildID           
                             FROM tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 AND Fve_ToTransID = TrnToID  
                            
                            UPDATE #TransTable SET TrnAmount = Fve_Amount from tbl_Fin_VoucherEntry WHERE Fve_VoucherType = TrnVchID AND Fve_GroupID = TrnGroupID           
                             AND TrnIsDebit = 1 and Fve_FrmTransID=TrnFrmID        
                                     
                           UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnFrmID           
                           --UPDATE #TransTable SET TrnDesc = Fah_Name FROM tbl_Fin_AccountHead WHERE Fah_ID = TrnToID          
                               
                           IF @FromID > 0 AND @ToID > 0         
                           BEGIN      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID AND TrnToID <> @ToID    
                           END        
                           ELSE IF @FromID > 0      
                           BEGIN         
                            --DELETE FROM #TransTable WHERE TrnFrmID <> @FromID      
                            DELETE FROM #TransTable WHERE TrnFrmID <> @FromID --OR  TrnToID <> @FromID    
                           END        
                           ELSE IF @ToID > 0       
                           BEGIN       
                            DELETE FROM #TransTable WHERE TrnToID <> @ToID       
                           END       
                           DECLARE VoucherCursor CURSOR FOR SELECT Fah_ID, Fah_SQLTable, Fah_SQLID, Fah_SQLName FROM tbl_Fin_AccountHead WHERE Fah_SQLTable IS NOT NULL          
                           OPEN VoucherCursor FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                           WHILE @@FETCH_STATUS = 0          
                            BEGIN          
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' ['' + (SELECT ' + @AccSQLName +           
                           --   ' FROM ' + @AccSQLTable + ' WHERE ' + @AccSQLID + ' = #TransTable.TrnFrmCldID OR ' + @AccSQLID + ' = #TransTable.TrnToCldID ) + '']''           
                           --   WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --            
                           --  SET @SQLStatement = 'UPDATE #TransTable SET TrnDesc = TrnDesc + '' [iWARE SQL]'' WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND (TrnFrmCldID > 0 OR TrnToCldID > 0)'          
                           --      
                           --  EXEC SP_SQLEXEC @SQLStatement     
                           PRINT 2    
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmFChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnFrmCldID) WHERE TrnFrmID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnFrmCldID > 0'        
                           PRINT @SQLStatement      
                            EXEC SP_SQLEXEC @SQLStatement      
                                
                            SET @SQLStatement = 'UPDATE #TransTable SET TrmTChild = (SELECT ' + @AccSQLName + ' FROM ' + @AccSQLTable + ' WHERE ' +     
                             @AccSQLID + ' = #TransTable.TrnToCldID) WHERE TrnToID = ' + CONVERT(VARCHAR, @AccID) + ' AND TrnToCldID > 0'          
                           PRINT @SQLStatement    
                            EXEC SP_SQLEXEC @SQLStatement    
                                     
                             FETCH NEXT FROM VoucherCursor INTO @AccID, @AccSQLTable, @AccSQLID, @AccSQLName          
                            END          
                           CLOSE VoucherCursor          
                           DEALLOCATE VoucherCursor          
                             PRINT 1        
                           SELECT @TmpID = @PageNo          
                           --SELECT @FirstRec = (@PageNo - 1) * @RecsPerPage          
                           --SELECT @LastRec = (@PageNo * @RecsPerPage + 1)          
                                     
                           --SELECT TrnDate, TrnVchType, TrnVchID, TrnVchNo, TrnUser, TrnDesc, TrnAmount, TrnIsDebit, TrnGroupID,           
                           -- (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo           
                           -- FROM #TransTable WHERE TrnID > @FirstRec AND TrnID < @LastRec ORDER BY TrnDate, TrnVchType, TrnDesc          
                                     
                           SELECT  MAx(TrnID) TrnID, TrnDate, Max(TrnVchType) TrnVchType, MAx(TrnVchID) TrnVchID, MAX(TrnVchNo) TrnVchNo, MAX(TrnAmount) TrnAmount, MAX(TrnUser) TrnUser,TrnDesc Account,     
                            MAX(TrnDesc + ISNULL(' [' + TrmFChild + ']', ISNULL(' [' + TrmTChild + ']', ''))) AS TrnDesc,     
                            TrnIsDebit, TrnGroupID, MAX(TrnFrmID) TrnFrmID, MAX(TrnFrmCldID) TrnFrmCldID, MAX(TrmFChild) TrmFChild, MAX(TrnToID) TrnToID, MAX(TrnToCldID) TrnToCldID, MAX(TrmTChild) TrmTChild, MAX(TrnNarration) TrnNarration, TrnIsCheque, MAX(TrnChequeNo) TrnChequeNo,     
                            MAX(TrnChequeDate) TrnChequeDate,TrnDrawon, (SELECT COUNT(TrnID) FROM #TransTable) AS TotNumbers, @TmpID + 1 AS PageNo   ,TrnFve_IsVoucher,isnull(max(TrnFve_isBounce),0) TrnFve_isBounce,max(TrnFve_IsCleared) TrnFve_IsCleared,case when (max(TrnFve_isBounce)=1) then 1 when (max(TrnFve_IsCleared)=1) then 2 else 0 end status
                            FROM #TransTable where TrnIsCheque=1 {#filter#} group by TrnGroupID,TrnIsDebit,TrnIsCheque,TrnFve_IsVoucher,TrnDate,TrnDrawon,TrnDesc  ORDER BY trnGroupID desc";
                db.CreateParameters(7);
                if (this.Filter==0)
                {
                    sql = sql.Replace("{#filter#}", "");
                }
                else if (this.Filter==1)
                {
                    sql = sql.Replace("{#filter#}", " and (case when (TrnFve_isBounce=1) then 1 when (TrnFve_IsCleared=1) then 2 else 0 end)=2");
                }
                else if (this.Filter==2)
                {
                    sql = sql.Replace("{#filter#}", " and (case when (TrnFve_isBounce=1) then 1 when (TrnFve_IsCleared=1) then 2 else 0 end)=1");
                }
                else if (this.Filter==3)
                {
                    sql = sql.Replace("{#filter#}", " and (case when (TrnFve_isBounce=1) then 1 when (TrnFve_IsCleared=1) then 2 else 0 end)=0");
                }
                db.AddParameters(0, "@type", this.filterType);
                db.AddParameters(1, "@date", this.fromdatestring);
                db.AddParameters(2, "@Todate", this.todatestring);
                db.AddParameters(3, "@voucher", this.VoucherType);
                db.AddParameters(4, "@company", this.CompanyId);
                db.AddParameters(5, "@frm", this.FromAccount);
                db.AddParameters(6, "@to", 0);
                dt = db.ExecuteQuery(CommandType.Text, sql);
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | GetChequeTransactions()");
                return null;
            }
        }

        public DataTable LoadChildHeadLedger(int Selectedhead)
        {
            try
            {
                DataTable dt = new DataTable();
                DBManager db = new DBManager();
                db.Open();
                string _query = "SELECT Fah_DataSQL, Fah_Type, Fah_SQLTable  FROM tbl_Fin_AccountHead WHERE Fah_ID = " + Selectedhead;
                dt = db.ExecuteQuery(CommandType.Text, _query);
                return dt;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "FinancialTransaction | LoadChildHeadLedger(int Selectedhead)");
                return null;
            }
        }

        public OutputMessage ClearCheque()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"update tbl_Fin_VoucherEntry set Fve_IsCleared=1,Fve_ChequeClearDate=@chequedate where Fve_GroupID=@GroupID";
                db.CreateParameters(2);
                db.AddParameters(0, "@chequedate", this.ChequeClearDate);
                db.AddParameters(1, "@GroupID", this.GroupID);
                db.Open();
                db.ExecuteQuery(CommandType.Text, query);
                return new OutputMessage("Cheque cleared", true, Type.NoError, "Financialtransacion | Clearcheque", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "financialtransaction | ClearCheque()");
                return null;
            }
        }

        public OutputMessage BounceCheque()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"update tbl_Fin_VoucherEntry set Fve_IsCleared=0,Fve_Isbounce=1 where Fve_GroupID=@GroupID";
                db.CreateParameters(1);
                db.AddParameters(0, "@GroupID", this.GroupID);
                db.Open();
                db.ExecuteQuery(CommandType.Text, query);
                return new OutputMessage("Cheque Bounced", true, Type.NoError, "Financialtransacion | BounceCheque", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "financialtransaction | BounceCheque()");
                return null;
            }
        }
    }
}
