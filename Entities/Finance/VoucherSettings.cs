using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Finance
{
    public class VoucherSettings
    {
        #region Properties
        public int ID { get; set; }
        public int VoucherTypeID { get; set; }
        public int AccountHeadID { get; set; }
        public string AllowCr { get; set; }
        public string AllowDr { get; set; }
        public string CrPart { get; set; }
        public string DrPart { get; set; }
        public int CompanyID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string HeadID { get; set; }
        public string byGroup { get; set; }
        public string TypeID { get; set; }
        #endregion
        /// <summary>
        /// Details To get The voucher Settings
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="voucherType"></param>
        /// <param name="isGroup"></param>
        /// <returns></returns>
        public  DataSet GetVoucherSetings(int companyId,int voucherType,int isGroup)
        {
            DataSet ds = new DataSet();
            DBManager db = new DBManager();
            try
            {
                db.Open();
                if (isGroup == 0)
                {
                    string sql = @"SELECT a.fah_ID ID,fah_Name Name,ISNULL(vtl_AllowDr,0) As AllowDr ,ISNULL(vtl_AllowCr,0) As AllowCr , 
                                                        0 As TotalHead, 0 As DrHead,0 As CrHead FROM tbl_Fin_AccountHead a 
                                                        LEFT JOIN tbl_Fin_VoucherTypeLink b ON a.fah_ID = b.Vtl_FahID
                                                        AND b.Vtl_FvtID = " + voucherType + " WHERE Fah_ComID = "+companyId+" AND Fah_Disable =0 ORDER BY fah_Name";
                    ds = db.ExecuteDataSet(CommandType.Text, sql);
                }
                else
                {
                    string sql = @"SELECT a.Fah_FagID ID, a.Fah_Name Name, COUNT(a.fah_id) As TotalHead, 
                               ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0) As DrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0)>0 THEN 1 ELSE 0 END As AllowDr,
                               ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0) As CrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0)>0 THEN 1 ELSE 0 END As AllowCr
                               FROM tbl_Fin_AccountHead a
                               INNER JOIN tbl_Fin_AccountGroup b ON a.Fah_FagID = b.Fag_ID 
                               LEFT JOIN tbl_Fin_VoucherTypeLink c ON a.Fah_ID = c.Vtl_FahID AND c.Vtl_FvtID = " + voucherType + " where fah_ComID=3 GROUP BY a.Fah_FagID,a.fah_Name ORDER BY a.fah_Name";
                    ds = db.ExecuteDataSet(CommandType.Text, sql);
                }
                return ds;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "voucherentry | GetVoucherSetings(int companyId,int voucherType,int isGroup)");
                return null;
            }
       
        }
        /// <summary>
        /// Saves data
        /// </summary>
        /// <param name="byGroup"></param>
        /// <param name="TypeID"></param>
        /// <param name="HeadID"></param>
        /// <param name="AllowDr"></param>
        /// <param name="AllowCr"></param>
        /// <param name="DrPart"></param>
        /// <param name="CrPart"></param>
        /// <returns></returns>
        public OutputMessage Save(string byGroup,string TypeID,string HeadID,string AllowDr, string AllowCr,string DrPart,string CrPart)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherSettings, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "VoucherSettings | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"DECLARE @tblHead TABLE(Hid int identity(1,1) Not Null,HeadID int)
	                    DECLARE @tblAllowDr TABLE (Did int identity(1,1) not null , Dr int)
	                    DECLARE @tblAllowCr TABLE (Cid int identity(1,1) not null , Cr int)
	                    
	                    INSERT INTO @tblHead(HeadID) SELECT CONVERT(INT,element) FROM Split(@HeadID ,'|')
	                    INSERT INTO @tblAllowDr(Dr) SELECT CONVERT(INT,element) FROM Split(@AllowDr ,'|')
	                    INSERT INTO @tblAllowCr(Cr) SELECT CONVERT(INT,element) FROM Split(@AllowCr, '|')

	                    IF @ByGroup = 0 
	                    BEGIN
	                    	DELETE FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID

	                    	INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    		   ([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    	SELECT @TypeID , HeadID , Dr , Cr FROM @tblHead
	                    			INNER JOIN @tblAllowDr ON Did = Hid
	                    			INNER JOIN @tblAllowCr ON Cid = Hid
	                    END
	                    ELSE IF @ByGroup = 1
	                    BEGIN
	                    	DECLARE @tblDrPart TABLE(Dpid int identity(1,1) Not Null, DrPart int)
	                    	DECLARE @tblCrPart TABLE(Cpid int identity(1,1) Not Null, CrPart int)

	                    	INSERT INTO @tblDrPart(DrPart) SELECT CONVERT(INT,element) FROM Split(@DrPart ,'|')
	                    	INSERT INTO @tblCrPart(CrPart) SELECT CONVERT(INT,element) FROM Split(@CrPart ,'|')
	                    	
	                    	DECLARE @Index INT
	                    	DECLARE @GrpID INT
	                    	DECLARE @isPartial INT
	                    	DECLARE @AlwDr INT

	                    	DECLARE tmpCur CURSOR FOR SELECT HeadID,Hid FROM @tblHead
	                    	OPEN tmpCur
	                    	FETCH NEXT FROM tmpCur INTO @GrpID , @Index
	                    	WHILE @@FETCH_STATUS = 0
	                    	BEGIN
	                    		SELECT @AlwDr = Dr FROM @tblAllowDr WHERE Did = @Index
	                    		SELECT @isPartial = DrPart FROM @tblDrPart WHERE Dpid = @Index
	                    	
	                    		IF  @isPartial = 0
	                    		BEGIN
	                    			INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    				([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    			SELECT @TypeID , Fah_ID , 0, 0 FROM tbl_Fin_AccountHead 
	                    				WHERE Fah_ID NOT IN (SELECT Vtl_FahID FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID)
	                    				AND Fah_FagID = @GrpID AND Fah_ComID = @CompanyID AND Fah_Disable =0

	                    			UPDATE tbl_Fin_VoucherTypeLink SET Vtl_AllowDr = @AlwDr WHERE Vtl_FvtID = @TypeID 
	                    					AND Vtl_FahID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_FagID = @GrpID AND Fah_Disable =0)

	                    			
	                    		END
	                    		
	                    		SELECT @AlwDr = Cr FROM @tblAllowCr WHERE Cid = @Index
	                    		SELECT @isPartial = CrPart FROM @tblCrPart WHERE Cpid = @Index
	                    		IF  @isPartial = 0
	                    		BEGIN
	                    			INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    				([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    			SELECT @TypeID , Fah_ID , 0, 0 FROM tbl_Fin_AccountHead 
	                    				WHERE Fah_ID NOT IN (SELECT Vtl_FahID FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID)
	                    				AND Fah_FagID = @GrpID AND Fah_ComID = @CompanyID AND Fah_Disable =0

	                    			UPDATE tbl_Fin_VoucherTypeLink SET Vtl_AllowCr = @AlwDr WHERE Vtl_FvtID = @TypeID 
	                    				AND Vtl_FahID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_FagID = @GrpID AND Fah_Disable =0)

	                    		END
	                    		
	                    		FETCH NEXT FROM tmpCur INTO @GrpID , @Index
	                    	END
	                    END	";
                        db.CreateParameters(9);
                        db.AddParameters(0, "@ByGroup", byGroup);
                        db.AddParameters(1, "@CompanyID", this.CompanyID);
                        db.AddParameters(2, "@TypeID", TypeID);
                        db.AddParameters(3, "@HeadID", HeadID);
                        db.AddParameters(4, "@AllowDr", AllowDr);
                        db.AddParameters(5, "@AllowCr", AllowCr);
                        db.AddParameters(6, "@DrPart", DrPart);
                        db.AddParameters(7, "@CrPart", CrPart);
                        db.AddParameters(8, "@CreatedBy", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Saved successfully", true, Type.NoError, "VoucherSettings | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherSettings | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherSettings | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        public static DataSet GetVoucherSetingsTable(int companyId, int voucherType, int isGroup)
        {
            DataSet ds = new DataSet();
            DBManager db = new DBManager();
            try
            {
                db.Open();
                if (isGroup == 0)
                {
                    string sql = @"SELECT a.Fah_ID ID, a.Fah_Name Name, COUNT(a.fah_id) As TotalHead, 
                               ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0) As DrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0)>0 THEN 1 ELSE 0 END As AllowDr,
                               ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0) As CrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0)>0 THEN 1 ELSE 0 END As AllowCr
                               FROM tbl_Fin_AccountHead a
                               LEFT JOIN tbl_Fin_VoucherTypeLink c ON a.Fah_ID = c.Vtl_FahID AND c.Vtl_FvtID = " + voucherType + " where fah_ComID=" + companyId+ "GROUP BY a.Fah_ID,a.fah_Name ORDER BY a.fah_Name";
                    ds = db.ExecuteDataSet(CommandType.Text, sql);
                }
                else
                {
                    string sql = @"SELECT a.Fah_FagID ID, b.fag_Name Name, COUNT(a.fah_id) As TotalHead, 
                               ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0) As DrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowDr)),0)>0 THEN 1 ELSE 0 END As AllowDr,
                               ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0) As CrHead , CASE WHEN ISNULL(SUM(CONVERT(INT,vtl_AllowCr)),0)>0 THEN 1 ELSE 0 END As AllowCr
                               FROM tbl_Fin_AccountHead a
                               INNER JOIN tbl_Fin_AccountGroup b ON a.Fah_FagID = b.Fag_ID 
                               LEFT JOIN tbl_Fin_VoucherTypeLink c ON a.Fah_ID = c.Vtl_FahID AND c.Vtl_FvtID = " + voucherType + " where fah_ComID="+companyId+ " GROUP BY a.Fah_FagID,b.fag_Name ORDER BY b.fag_Name";
                    ds = db.ExecuteDataSet(CommandType.Text, sql);
                }
                return ds;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.VoucherSettings, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "VoucherSettings | Create", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"DECLARE @tblHead TABLE(Hid int identity(1,1) Not Null,HeadID int)
	                    DECLARE @tblAllowDr TABLE (Did int identity(1,1) not null , Dr int)
	                    DECLARE @tblAllowCr TABLE (Cid int identity(1,1) not null , Cr int)
	                    
	                    INSERT INTO @tblHead(HeadID) SELECT CONVERT(INT,element) FROM Split(@HeadID ,'|')
	                    INSERT INTO @tblAllowDr(Dr) SELECT CONVERT(INT,element) FROM Split(@AllowDr ,'|')
	                    INSERT INTO @tblAllowCr(Cr) SELECT CONVERT(INT,element) FROM Split(@AllowCr, '|')

	                    IF @ByGroup = 0 
	                    BEGIN
	                    	DELETE FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID

	                    	INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    		   ([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    	SELECT @TypeID , HeadID , Dr , Cr FROM @tblHead
	                    			INNER JOIN @tblAllowDr ON Did = Hid
	                    			INNER JOIN @tblAllowCr ON Cid = Hid
	                    END
	                    ELSE IF @ByGroup = 1
	                    BEGIN
	                    	DECLARE @tblDrPart TABLE(Dpid int identity(1,1) Not Null, DrPart int)
	                    	DECLARE @tblCrPart TABLE(Cpid int identity(1,1) Not Null, CrPart int)

	                    	INSERT INTO @tblDrPart(DrPart) SELECT CONVERT(INT,element) FROM Split(@DrPart ,'|')
	                    	INSERT INTO @tblCrPart(CrPart) SELECT CONVERT(INT,element) FROM Split(@CrPart ,'|')
	                    	
	                    	DECLARE @Index INT
	                    	DECLARE @GrpID INT
	                    	DECLARE @isPartial INT
	                    	DECLARE @AlwDr INT

	                    	DECLARE tmpCur CURSOR FOR SELECT HeadID,Hid FROM @tblHead
	                    	OPEN tmpCur
	                    	FETCH NEXT FROM tmpCur INTO @GrpID , @Index
	                    	WHILE @@FETCH_STATUS = 0
	                    	BEGIN
	                    		SELECT @AlwDr = Dr FROM @tblAllowDr WHERE Did = @Index
	                    		SELECT @isPartial = DrPart FROM @tblDrPart WHERE Dpid = @Index
	                    	
	                    		IF  @isPartial = 0
	                    		BEGIN
	                    			INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    				([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    			SELECT @TypeID , Fah_ID , 0, 0 FROM tbl_Fin_AccountHead 
	                    				WHERE Fah_ID NOT IN (SELECT Vtl_FahID FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID)
	                    				AND Fah_FagID = @GrpID AND Fah_ComID = @CompanyID AND Fah_Disable =0

	                    			UPDATE tbl_Fin_VoucherTypeLink SET Vtl_AllowDr = @AlwDr WHERE Vtl_FvtID = @TypeID 
	                    					AND Vtl_FahID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_FagID = @GrpID AND Fah_Disable =0)

	                    			
	                    		END
	                    		
	                    		SELECT @AlwDr = Cr FROM @tblAllowCr WHERE Cid = @Index
	                    		SELECT @isPartial = CrPart FROM @tblCrPart WHERE Cpid = @Index
	                    		IF  @isPartial = 0
	                    		BEGIN
	                    			INSERT INTO [tbl_Fin_VoucherTypeLink]
	                    				([Vtl_FvtID],[Vtl_FahID],[Vtl_AllowDr],[Vtl_AllowCr])
	                    			SELECT @TypeID , Fah_ID , 0, 0 FROM tbl_Fin_AccountHead 
	                    				WHERE Fah_ID NOT IN (SELECT Vtl_FahID FROM tbl_Fin_VoucherTypeLink WHERE Vtl_FvtID = @TypeID)
	                    				AND Fah_FagID = @GrpID AND Fah_ComID = @CompanyID AND Fah_Disable =0

	                    			UPDATE tbl_Fin_VoucherTypeLink SET Vtl_AllowCr = @AlwDr WHERE Vtl_FvtID = @TypeID 
	                    				AND Vtl_FahID IN (SELECT Fah_ID FROM tbl_Fin_AccountHead WHERE Fah_FagID = @GrpID AND Fah_Disable =0)

	                    		END
	                    		
	                    		FETCH NEXT FROM tmpCur INTO @GrpID , @Index
	                    	END
	                    END	";
                        db.CreateParameters(9);
                        db.AddParameters(0, "@ByGroup", this.byGroup);
                        db.AddParameters(1, "@CompanyID", this.CompanyID);
                        db.AddParameters(2, "@TypeID", this.TypeID);
                        db.AddParameters(3, "@HeadID", this.HeadID);
                        db.AddParameters(4, "@AllowDr", this.AllowDr);
                        db.AddParameters(5, "@AllowCr", this.AllowCr);
                        db.AddParameters(6, "@DrPart", this.DrPart);
                        db.AddParameters(7, "@CrPart", this.CrPart);
                        db.AddParameters(8, "@CreatedBy", this.CreatedBy);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Saved successfully", true, Type.NoError, "VoucherSettings | Save", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherSettings | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. could not save", false, Type.Others, "VoucherSettings | Save", System.Net.HttpStatusCode.InternalServerError, ex);

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
