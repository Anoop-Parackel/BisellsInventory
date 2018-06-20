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
    public class VoucherEdit
    {
        #region Properties
        public int GroupID { get; set; }
        public int VoucherType { get; set; }
        public DateTime VoucherDate { get; set; }
        public string VoucherDateString { get; set; }
        public string Narration { get; set; }
        public List<VoucherEdit> Voucher { get; set; }
        public int ModifiedBy { get; set; }
        public int ID { get; set; }

        #endregion
        public DataTable getdata(int groupID,DropDownList ddl,TextBox t1,TextBox t2)
        {
            try
            {
                DBManager db = new DBManager();
                DataTable dt = new DataTable();
                string sql = @" CREATE TABLE #VoucherTable    
                              (  
                               ID        int  identity(1,1),
                               ObjectID  INT,
                               VoucherNo INT ,    
                               AccountNo INT,    
                               AccountName VARCHAR(100),    
                               ChildID  INT, 
                               ChildName varchar(200),  
                               ChildSQL  varchar(8000), 
                               VoucherType INT,    
                               VoucherName VARCHAR(100),  
                               CostCenterID int,  
                               Narration VARCHAR(1000),    
                               DrAmount MONEY ,    
                               CrAmount MONEY ,    
                               TransDate DateTime,    
                               PreFix  VARCHAR(10),  
                               SumDrAmount MONEY,
                               SumCrAmount MONEY,
                               HasChild  int,
                               HasTrans  int,
                               HasGeneral int,
                               GroupID   int,
                               IsCheque  int,
                               ChequeNumber varchar(100),
                               ChequeDate   DateTime
                               --CostCenterName   varchar(200)  
                              )    
                            
                            Declare @ChildStr varchar(max)
                            	
                            
                            
                            INSERT INTO #VoucherTable    
                            ( ObjectID,VoucherNo, AccountNo, AccountName,ChildID, VoucherType, VoucherName, Narration,    
                             DrAmount,   CrAmount, TransDate, PreFix ,GroupID ,IsCheque, ChequeNumber, ChequeDate--,CostCenterID--,CostCenterName
                            )    
                                
                                
                                
                            SELECT  Fve_ID,Fve_Number, Fah_ID,  Fah_Name,Fve_FrmTransChildID, Fve_VoucherType,Fvt_TypeName, Fve_Description,    
                             0, ISNULL(Fve_Amount,0),  Fve_Date, 'Cr' ,Fve_GroupID ,Fve_IsCheque  ,Fve_ChequeNo,Fve_ChequeDate--,Fve_FccID
                             --, (Select Fcc_Name from tbl_Fin_CostCenter where Fcc_ID=Fve_FccID) as Fcc_Name
                                   
                             FROM Tbl_Fin_VoucherEntry    
                             JOIN Tbl_Fin_AccountHead ON  Fve_FrmTransID  = Fah_ID    
                             JOIN Tbl_Fin_VoucherType ON  Fve_VoucherType  = Fvt_ID    
                             WHERE Fve_GroupID =   @FveGroupID    
                            UNION ALL    
                            SELECT  Fve_ID,Fve_Number, Fah_ID,  Fah_Name,Fve_ToTransChildID, Fve_VoucherType,Fvt_TypeName, Fve_Description,    
                             ISNULL(Fve_Amount,0),0,  Fve_Date, 'Dr'  ,Fve_GroupID  ,Fve_IsCheque  ,Fve_ChequeNo,Fve_ChequeDate--,Fve_FccID
                             --, (Select Fcc_Name from tbl_Fin_CostCenter where Fcc_ID=Fve_FccID) as Fcc_Name
                                   
                             FROM Tbl_Fin_VoucherEntry    
                             JOIN Tbl_Fin_AccountHead ON  Fve_ToTransID  = Fah_ID    
                             JOIN Tbl_Fin_VoucherType ON  Fve_VoucherType  = Fvt_ID    
                             WHERE Fve_GroupID =   @FveGroupID 
                            
                            
                            
                            --update #VoucherTable set ChildID=AccountNo 
                            --				where Accountno in (select Fah_ID from tbl_Fin_AccountHead where Fah_GeneralType =1  )
                            
                            if exists (select ObjectID from #VoucherTable where ChildID>0)
                            begin
                            UPDATE #VoucherTable set	ChildSQL	= 'UPDATE #VoucherTable set ChildName = ( Select '+ Fah_SQLName+' from ' + Fah_SQLTable +' Where ' + Cast(Fah_SQLID as Varchar) + '= ' + Cast(ChildID as Varchar) +') WHERE  ChildID ='+ Cast(ChildID as Varchar)
                            					 from	tbl_Fin_AccountHead
                                                 where  AccountNo	=	Fah_ID
                                                 and    ChildID		>0	
                            
                            
                            Declare @i as int,@j As int
                            Declare @SQlstr as varchar(Max)
                            set @i=1
                            set @SQlstr=NULL
                            Select @j=Count (*) from #VoucherTable
                            
                            while @i<=@j
                            begin
                            
                            
                            select @SQlstr=ChildSQL from #VoucherTable where ChildID>0 and ID= @i 
                            IF @SQlstr IS NOT NULL
                            BEGIN
                            print @SQlstr
                            exec sp_SqlExec @SQlstr
                            set @SQlstr=NULL
                            END
                            set @i=@i+1
                            end
                            
                            
                            end
                            --select * from #VoucherTable
                            --Exec Sp_SqlExec 'Select from #VoucherTable'
                            
                            
                            UPDATE #VoucherTable set SumDrAmount=(select SUM(DrAmount) from #VoucherTable)
                            						,SumCrAmount=(select SUM(CrAmount) from #VoucherTable)
                            
                            
                            UPDATE #VoucherTable set HasChild= Case when Exists(Select Fah_DataSql from tbl_Fin_AccountHead where Fah_ID=#VoucherTable.AccountNo and Fah_DataSql is not null)
                            										Then 1
                            										Else 0	
                            								   End
                            
                            
                            --UPDATE #VoucherTable set HasTrans= Case when Exists(Select Pay_VchGroupID	from tbl_Fin_Payments		where Pay_VchGroupID=#VoucherTable.GroupID)			then 1
                            --										when Exists(Select SPay_VchGroupID	from tbl_Fin_SalesPayments	where SPay_VchGroupID=#VoucherTable.GroupID)		then 1
                            --										when Exists(Select SGPay_VchGroupID from tbl_Fin_SalesGeneral	where SGPay_VchGroupID=#VoucherTable.GroupID)		then 1
                            --										when Exists(Select Sap_VchGroupID	from [tbl_SupplierAdvancePayment] where Sap_VchGroupID=#VoucherTable.GroupID)	then 1
                            --										when Exists(Select Pay_VchGroupID	from tbl_Fin_Payments		where Pay_VchGroupID=#VoucherTable.GroupID)			then 1
                            --									Else 0
                            --									End
                            
                            --UPDATE #VoucherTable set HasGeneral=1 where Accountno in (select Fah_ID from tbl_Fin_AccountHead where Fah_GeneralType =1  )
                            ---17-Dec-2012
                            --UPDATE #VoucherTable set HasTrans= 		Case when Exists(Select fpm_VoucherGroupID	from tbl_Fin_PaymentMaster 	        where fpm_VoucherGroupID=#VoucherTable.GroupID)			then 1	
                            --											 when Exists(Select fdc_VoucherGroupID	from tbl_Fin_DebitCreditNoteEntry  	where fdc_VoucherGroupID=#VoucherTable.GroupID)			then 1											
                            --                                        Else  0
                            --                                        End
                              UPDATE #VoucherTable set HasTrans=0
                                                                  
                                                                    
                            									
                            SELECT * FROM   #VoucherTable     
                            ORDER BY DrAmount desc  
                            ";
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@FveGroupID", groupID);
                dt = db.ExecuteQuery(CommandType.Text, sql);
                VoucherEdit v = new VoucherEdit();
                v.VoucherType = Convert.ToInt32(dt.Rows[0][8]);
                v.VoucherDateString = Convert.ToString(dt.Rows[0][14]);
                v.Narration= Convert.ToString(dt.Rows[0][11]);
                ddl.SelectedValue= Convert.ToString(dt.Rows[0][8]);
                t1.Text= Convert.ToString(dt.Rows[0][11]);
                t2.Text = Convert.ToDateTime(dt.Rows[0][14]).ToString("dd/MMM/yyyy");
                return dt;      
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucheredit | getdata(int groupID,DropDownList ddl,TextBox t1,TextBox t2)");
                return null;
            }
        }
        public OutputMessage UpdateVoucher(string ObjectID,string ChildAccountID,string DrAmount,string CrAmount)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.VoucherEdit, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be empty", false, Type.Others, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"										 BEGIN TRY  
                                         BEGIN TRANSACTION 
                                         --Rollback TRANSACTION 
                                         
                                         CREATE TABLE #VoucherTable    
                                           (V_ID        INT identity(1,1),
                                            ObjectID  INT,
                                            GroupID   INT,
                                            ChildID   int,
                                            AccountNo INT,    
                                            DrAmount MONEY ,    
                                            CrAmount MONEY ,    
                                            SumDrAmount MONEY,
                                            SumCrAmount MONEY,
                                            AccountHead int,
                                            TransType  varchar(5),
                                            DrOrCR      int  --Dr=0Cr=1
                                           )    
                                         
                                         --Declare @FveGroupID int
                                         
                                         DECLARE @tblObject		TABLE(ObjectID	INT,	O_ID int IDENTITY(1,1))  
                                         DECLARE @tblAccountID	TABLE(AccountID INT,	A_ID int IDENTITY(1,1))  
                                         DECLARE @tblDrAmount		TABLE(DrAmt	Money,	D_ID int IDENTITY(1,1)) 
                                         DECLARE @tblCrAmount		TABLE(CrAmt	Money,	C_ID int IDENTITY(1,1))
                                         
                                          
                                         INSERT INTO @tblObject(ObjectID)		SELECT CONVERT(INT, Element) FROM SPLIT(@ObjectStr		,'|')   
                                         INSERT INTO @tblAccountID(AccountID)	SELECT CONVERT(INT, Element) FROM SPLIT(@ChildAccountIDStr	,'|')   
                                         INSERT INTO @tblDrAmount(DrAmt)		SELECT CONVERT(money, Element) FROM SPLIT(@DrAmountStr	,'|')   
                                         INSERT INTO @tblCrAmount(CrAmt)		SELECT CONVERT(money, Element) FROM SPLIT(@CrAmountStr	,'|')   
                                         
                                         
                                         --INSERT INTO #VoucherTable (V_ID,ObjectID) select O_ID ,ObjectID from @tblObject where ObjectID>0
                                         
                                         
                                         INSERT INTO #VoucherTable (ObjectID,AccountNo,DrAmount,CrAmount) 
                                         						select	ObjectID,AccountID,DrAmt,CrAmt 
                                         						from	@tblObject  
                                         						Left JOIN	@tblAccountID	ON O_ID=A_ID
                                         						join	@tblDrAmount	ON O_ID=D_ID
                                                                 join	@tblCrAmount	ON O_ID=C_ID
                                         
                                         
                                         
                                         UPDATE #VoucherTable set SumDrAmount=(select SUM(DrAmount) from #VoucherTable)
                                         						,SumCrAmount=(select SUM(CrAmount) from #VoucherTable)
                                         
                                         
                                         UPDATE #VoucherTable SET GroupID=Fve_GroupID
                                         						from tbl_Fin_VoucherEntry
                                                                 where #VoucherTable.ObjectID=Fve_ID 
                                         
                                         UPDATE #VoucherTable SET DrOrCr =case when isnull(Fve_FrmTransID,0)>0 then 1 else 0 end
                                         					    from tbl_Fin_VoucherEntry
                                                                 where #VoucherTable.ObjectID=Fve_ID 
                                         UPDATE #VoucherTable SET AccountHead= Case	when DrOrCr = 1 
                                         											then isnull(Fve_FrmTransID,0) 
                                         											else isnull(Fve_ToTransID,0) 
                                         									  End
                                         						from tbl_Fin_VoucherEntry
                                                                 where #VoucherTable.ObjectID=Fve_ID 
                                         
                                         
                                         UPDATE #VoucherTable SET ChildID= Case  when isnull(Fve_ToTransChildID,0)>0  then isnull(Fve_ToTransChildID,0)
                                         										when isnull(Fve_FrmTransChildID,0)>0 then isnull(Fve_FrmTransChildID,0)
                                         								  Else 0
                                         								  End
                                                                 from tbl_Fin_VoucherEntry
                                                                 where #VoucherTable.ObjectID=Fve_ID 
                                          
                                         
                                         
                                         --select * from #VoucherTable
                                         --join tbl_Fin_VoucherEntry on Fve_ID=ObjectID
                                         --select * from tbl_Fin_VoucherEntry where Fve_ID in (5,6)
                                         
                                         --IF  Exists(Select * from tbl_Fin_VoucherEntry ve inner join #VoucherTable vt on vt.ObjectID=ve.Fve_ID where Fve_IsVoucher<>1 )
                                         --begin
                                         --Rollback Transaction 
                                         
                                         --    Return 0
                                         --end
                                         
                                         if Exists(select top 1  ObjectID from  #VoucherTable where SumDrAmount=SumCrAmount)
                                         BEGIN
                                         print 'head from'
                                         update tbl_Fin_VoucherEntry Set Fve_FrmTransChildID= AccountNo
                                         								FROM    #VoucherTable
                                                                         WHERE	Fve_ID=ObjectID
                                         								AND		Fve_FrmTransID>0 
                                                                         AND		AccountNo>0
                                         								AND AccountHead NOT  IN (2,8)
                                         
                                         					
                                                                         --AND		isnull(Fve_FrmTransChildID,0)=0
                                         
                                         
                                         Print @@Rowcount
                                         update tbl_Fin_VoucherEntry Set Fve_ToTransChildID= AccountNo
                                         								FROM    #VoucherTable
                                                                         WHERE	Fve_ID=ObjectID
                                         								AND     Fve_ToTransID>0 
                                                                         AND		AccountNo>0
                                         								AND AccountHead NOT  IN (2,8)
                                                                         --AND		isnull(Fve_ToTransChildID,0)=0
                                         
                                         
                                         
                                         
                                         
                                         
                                         update tbl_Fin_VoucherEntry Set Fve_Amount= DrAmount
                                         								FROM    #VoucherTable
                                                                         WHERE	Fve_ID=ObjectID
                                         								AND     Fve_ToTransID>0 
                                                                         AND		DrAmount>0
                                                                         
                                         
                                         update tbl_Fin_VoucherEntry Set Fve_Amount= CrAmount
                                         								FROM    #VoucherTable
                                                                         WHERE	Fve_ID=ObjectID
                                         								AND     Fve_FrmTransID>0 
                                                                         AND		CrAmount>0
                                                                         
                                         update a  set a.Ppo_Amount= case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end,a.Ppo_OpenAmount = case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end FROM tbl_SalesPaymentOpening A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Ppo_VchGroupID    
                                         
                                         update a set a.Pay_SupID = c.AccountNo, a.Pay_Amount=case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end  FROM TBL_FIN_CUSTOMER_RECEIPTS A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Fve_GroupID 
                                         
                                         update a set  a.Pay_SupID = c.AccountNo,a.Pay_Amount =case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end  FROM tbl_Fin_Payments A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Pay_VchGroupID
                                         
                                         update a set a.Ppo_Amount= case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end,a.Ppo_OpenAmount = case  when c.CrAmount>0 then c.CrAmount else c.DrAmount end   FROM tbl_PurchasePaymentOpening A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Ppo_VchGroupID 
                                         
                                         
                                         
                                         update a  set a.Ppo_SupID = c.AccountNo FROM tbl_SalesPaymentOpening A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Ppo_VchGroupID     AND AccountHead NOT  IN (2,8)
                                         
                                         update a set a.Pay_SupID = c.AccountNo  FROM tbl_Fin_CustomerReceipts A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Pay_VchGroupID  AND AccountHead NOT  IN (2,8)
                                         
                                         update a set  a.Pay_SupID = c.AccountNo  FROM tbl_Fin_Payments A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Pay_VchGroupID AND AccountHead NOT  IN (2,8)
                                         
                                         update a set a.Ppo_SupID = c.AccountNo   FROM tbl_PurchasePaymentOpening A 
                                         INNER JOIN #VoucherTable C ON C.GroupID =A.Ppo_VchGroupID  AND AccountHead NOT  IN (2,8)
                                         
                                         
                                         ------------------
                                         --select * from tbl_Fin_Payments 
                                         --where Pay_VchGroupID in( select  #VoucherTable 
                                         ------------------		
                                         								
                                         select * from #VoucherTable
                                         join tbl_Fin_VoucherEntry on Fve_ID=ObjectID                             
                                         END
                                         ELSE
                                         BEGIN
                                         
                                         Rollback Transaction   
                                         
                                         END
                                         
                                         
                                         
                                         --Rollback Transaction   
                                         COMMIT TRANSACTION  
                                         
                                           
                                         END TRY  
                                         BEGIN CATCH  
                                         print @@error
                                         ROLLBACK TRANSACTION  
                                            
                                         END CATCH ";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@ObjectStr", ObjectID);
                        db.AddParameters(1, "@ChildAccountIDStr", ChildAccountID);
                        db.AddParameters(2, "@DrAmountStr", DrAmount);
                        db.AddParameters(3, "@CrAmountStr", CrAmount);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage(" Voucher updated successfully", true, Type.NoError, "VoucherEdit | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Voucher could not be updated", false, Type.Others, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Voucher could not be updated", false, Type.Others, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
        }
        public void updatehead(int ObjectID,int AccountID)
        {
            try
            {
                DBManager db = new DBManager();
                string sql = @"Declare 
                                      @IsUpdated      int,
                                      @TransGroupID   int
                                      Set @IsUpdated=0
                                      ----------------------------------------------
                                      
                                      IF  Exists(Select * from tbl_fin_voucherentry where Fve_ID=@ObjectID and Fve_IsVoucher<>1 )
                                         print 0;
                                      
                                      
                                      
                                      if exists(Select Fve_FrmTransID from tbl_Fin_VoucherEntry where Fve_ID=@ObjectID and isnull(Fve_FrmTransID,0)>0)
                                      begin
                                      update tbl_Fin_VoucherEntry Set Fve_FrmTransID= @AccountID,Fve_FrmTransChildID=0
                                      --,Fve_FrmTransChildID= Case when @HasChild=1 Then 1 Else 0 End
                                      								WHERE	Fve_ID=@ObjectID
                                      								AND		Fve_FrmTransID>0 
                                                                      AND		@AccountID>0
                                                                      and     Fve_FrmTransID<>@AccountID
                                      if @@RowCount>0
                                      Set @IsUpdated=1
                                                                      
                                      end
                                      else
                                      begin
                                      update tbl_Fin_VoucherEntry Set Fve_ToTransID= @AccountID,Fve_ToTransChildID=0
                                      --,Fve_ToTransChildID=   Case when @HasChild=1 Then 1	Else 0 End
                                      								WHERE	Fve_ID=@ObjectID
                                      								AND     Fve_ToTransID>0 
                                                                      AND		@AccountID>0
                                                                      and     Fve_ToTransID<>@AccountID
                                      if @@RowCount>0
                                      Set @IsUpdated=1
                                          print @IsUpdated                           
                                      end";
                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@ObjectID", ObjectID);
                db.AddParameters(1, "@AccountID", AccountID);
                db.ExecuteQuery(CommandType.Text, sql);
                db.Close();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucheredit | updatehead(int ObjectID,int AccountID)");
            }
        }
        public OutputMessage RemoveVoucher(int groupID)
        {
            try
            {
                if (groupID!=0)
                {
                    DBManager db = new DBManager();
                    string sql = @"Delete from tbl_Fin_VoucherEntry 
                          where Fve_GroupID =@GroupID
                          and Fve_IsVoucher=1
                          
                          DELETE FROM tbl_Fin_CustomerReceipts WHERE tbl_Fin_CustomerReceipts.Pay_VchGroupID = @GroupID
                          DELETE FROM tbl_Fin_Payments WHERE tbl_Fin_Payments.Pay_VchGroupID = @GroupID
                          DELETE FROM tbl_SalesPaymentOpening WHERE tbl_SalesPaymentOpening.Ppo_VchGroupID = @GroupID
                          DELETE FROM tbl_PurchasePaymentOpening WHERE tbl_PurchasePaymentOpening.Ppo_VchGroupID = @GroupID
                          
                          if @@RowCount>0
                          return 1
                          else
                          return 0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@GroupID", groupID);
                    db.ExecuteQuery(CommandType.Text, sql);
                    return new OutputMessage(" Voucher edit deleted successfully", true, Type.NoError, "VoucherEdit | Delete", System.Net.HttpStatusCode.OK);

                }
                else
                {
                    return new OutputMessage("Something went wrong. Voucher edit could not be deleted", false, Type.Others, "VoucherEdit | Delete", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new OutputMessage("Something went wrong. Voucher edit could not be deleted", false, Type.Others, "VoucherEdit | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
            }
        }
        public OutputMessage UpdateVoucherEntry(int GroupID, int VoucherID, DateTime VoucherDate, int IsCheque, string ChqNumber, DateTime? ChqDate, string Narration, string UserID)
        {
            try
            {
                if (GroupID != 0)
                {
                    DBManager db = new DBManager();
                    db.Open();
                    string sql = @"Declare @VoucherNumber int

			                --Select @VoucherNumber=isnull(MAX(Fve_Number),0)+1	from	tbl_Fin_VoucherEntry 
			                --												where	Fve_VoucherType	=@VoucherID
			                --												--and     YEAR(Fve_Date)=YEAR(@VoucherDate) 	
			                												
                            Select top 1 	 @VoucherNumber=isnull((Fve_Number),0)+1	from	tbl_Fin_VoucherEntry 
			                												where	Fve_VoucherType	=@VoucherID
			                												order by 	Fve_ID desc													
			                											    
                            begin try
			                Begin transaction
			                print '1'
			                	update tbl_Fin_VoucherEntry 
			                		   set	Fve_VoucherType	=@VoucherID,		
			                				Fve_Date		=case when @IsCheque = 1 then @ChqDate else @VoucherDate end ,	
			                				--Fve_FccID       =@CostCenterID,
			                				Fve_IsCheque	=@IsCheque,   	
			                				Fve_ChequeNo	=@ChqNumber,  
			                				Fve_ChequeDate	=@ChqDate,  
			                				Fve_Description	=@Narration,
			                				Fve_Number      =Case When Fve_VoucherType	=@VoucherID then Fve_Number 
			                									  Else @VoucherNumber
			                								 End,
			                				Fve_CurDtTime = @VoucherDate	
			                		  where Fve_GroupID	=@GroupId
			                		  
			                	  if @@RowCount>0
			                	  begin
			                      Commit Transaction
			                	  print 1
			                	  end
			                	  else
			                	  begin
			                	  Rollback  Transaction
			                	  print 0
			                	  end
		
			                  end try
			                  begin catch 
			                  Rollback  Transaction
			                  print 0
			                  end catch";
                    db.CreateParameters(8);
                    db.AddParameters(0, "@GroupID", GroupID);
                    db.AddParameters(1, "@VoucherID", VoucherID);
                    db.AddParameters(2, "@VoucherDate", VoucherDate);
                    db.AddParameters(3, "@CostCenterID", 0);
                    db.AddParameters(4, "@IsCheque", IsCheque);
                    db.AddParameters(5, "@ChqNumber", ChqNumber);
                    db.AddParameters(6, "@ChqDate", ChqDate);
                    db.AddParameters(7, "@Narration", Narration);
                    db.ExecuteQuery(CommandType.Text, sql);
                    return new OutputMessage(" Voucher edit deleted successfully", true, Type.NoError, "VoucherEdit | Update", System.Net.HttpStatusCode.OK);

                }
                else
                {
                    return new OutputMessage("Something went wrong. Voucher edit could not be deleted", false, Type.Others, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return new OutputMessage("Something went wrong. Voucher edit could not be deleted", false, Type.Others, "VoucherEdit | Update", System.Net.HttpStatusCode.InternalServerError,ex);
            }
        }
    }
}
