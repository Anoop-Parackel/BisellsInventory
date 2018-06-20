using Core.DBManager;
using Entities.Application;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
 public class DebitNote : Register, IRegister
    {
        #region Properties
        public int ID { get; set; }
        public int SupplierId { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ReturnFrom { get; set; }
        public int ReturnType { get; set; }
        public int Payment_Terms { get; set; }
        public string BillNo { get; set; }
        public string Supplier { get; set; }
        public string DebitNoteNo { get; set; }
        public string FinancialYear { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string SupplierAddress1 { get; set; }
        public string SupplierAddress2 { get; set; }
        public int CargoId { get; set; }
        public int Cartons { get; set; }
        public decimal Weight { get; set; }
        public string Remarks { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierPhone { get; set; }
        public string LocationAddress1 { get; set; }
        public string CompanyEmail { get; set; }
        public string LocationAddress2 { get; set; }
        public int SupplierStateId { get; set; }
        public string SupplierState { get; set; }
        public string SupplierCountry { get; set; }
        public string LocationRegId { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string ReturnDateString { get; set; }
        public string LocationPhone { get; set; }
        public string SupplierTaxNo { get; set; }
        public string TermsandConditon { get; set; }
        public List<Item> Products { get; set; }
        public string CompanyRegId { get; private set; }
        public string CompanyAddress1 { get; private set; }
        public string CompanyAddress2 { get; private set; }
        public string CompanyPhone { get; private set; }
        #endregion Properties

        #region Functions
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseDebitNote, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "DebitNote | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                //Main Validations. Use ladder-if after this "if" for more validations
               if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid return date to make a Entry.", false, Type.Others, "DebitNote | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.SupplierId == 0)
                {
                    return new OutputMessage("Select a Supplier to make a Entry.", false, Type.Others, "DebitNote| Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Entry.", false, Type.Others, "DebitNote | Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {
                    db.Open();
                    string query = @"insert into TBL_PURCHASE_RETURN_REGISTER(Return_Date,Location_Id,Supplier_Id,Return_From,Bill_No, Tax_Amount,Gross_Amount,     
                    Net_Amount,Round_Off,Other_Charges,Narration,Cargo_Id,Cartons,Weight,Status,Created_By,Created_Date,     	 
                    Return_Type,Cost_Center_Id,Job_Id,TandC,Payment_Terms,Salutation,Contact_Name,
                    Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,
                    Contact_Email)values(@Return_Date,@Location_Id, @Supplier_Id, @Return_From,  
                    [dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRT'), @Tax_Amount,@Gross_Amount,@Net_Amount,@Round_Off,@Other_Charges,@Narration,@Cargo_Id, @Cartons,@Weight,@Status, @Created_By,GETUTCDATE(),@Return_Type,@Cost_Center_Id,@Job_Id, @TandC, @Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity";
                    db.CreateParameters(31);
                    db.AddParameters(0, "@Return_Date", this.ReturnDate);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(3, "@Return_From", this.ReturnFrom);
                    db.AddParameters(4, "@Return_Type", this.ReturnType);
                    db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(6, "@Gross_Amount", this.Gross);
                    db.AddParameters(7, "@Net_Amount", this.NetAmount);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(10, "@Narration", this.Narration);
                    db.AddParameters(11, "@Cargo_Id", this.CargoId);
                    db.AddParameters(12, "@Cartons", this.Cartons);
                    db.AddParameters(13, "@Weight", this.Weight);
                    db.AddParameters(14, "@Status", this.Status);
                    db.AddParameters(15, "@Created_By", this.CreatedBy);
                    db.AddParameters(16, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(17, "@Job_Id", this.JobId);
                    db.AddParameters(18, "@TandC", this.TermsandConditon);
                    db.AddParameters(19, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(20, "@Salutation", address.Salutation);
                        db.AddParameters(21, "@Contact_Name", address.ContactName);
                        db.AddParameters(22, "@Contact_Address1", address.Address1);
                        db.AddParameters(23, "@Contact_Address2", address.Address2);
                        db.AddParameters(24, "@Contact_City", address.City);
                        db.AddParameters(25, "@State_ID", address.StateID);
                        db.AddParameters(26, "@Country_ID", address.CountryID);
                        db.AddParameters(27, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(28, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(29, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(30, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PRT", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInPurchaseEntry)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            item.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            item.Gross = item.Quantity * prod.CostPrice;
                            item.NetAmount = item.Gross + item.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_RETURN_DETAILS(Pr_Id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Remarks,Location_Id,Status,
                            Return_From,Item_Id,Instance_Id) values(@Pr_Id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Remarks,@Location_Id,
                            @Status,@Return_From,@Item_Id,@Instance_Id);";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Pr_Id", identity);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.AddParameters(2, "@Quantity", item.Quantity);
                            db.AddParameters(3, "@Mrp", item.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", item.Gross);
                            db.AddParameters(8, "@Net_Amount", item.NetAmount);
                            db.AddParameters(9, "@Remarks", 0);
                            db.AddParameters(10, "@Location_Id", this.LocationId);
                            db.AddParameters(11, "@Return_From", item.ReturnFrom);
                            db.AddParameters(12, "@Status", 0);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;
                        }
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_RETURN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRT')[New_Order],Pr_Id from [TBL_PURCHASE_RETURN_REGISTER] where Pr_Id=" + identity);
                return new OutputMessage("Details has been saved as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "DebitNote | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pr_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Debit note could not be saved", false, Type.Others, "DebitNote | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }

        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseDebitNote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make Return.", false, Type.Others, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    string query = @"delete from [TBL_PURCHASE_RETURN_DETAILS] where Pr_Id=@Pr_Id;
                                   Update TBL_PURCHASE_RETURN_REGISTER set Return_Date=@Return_Date,Location_Id=@Location_Id,Supplier_Id=@Supplier_Id,Return_From=@Return_From,Tax_Amount=@Tax_Amount,
                                   Gross_Amount=@Gross_Amount,Net_Amount=@Net_Amount,Round_Off=@Round_Off,Other_Charges=@Other_Charges,Narration=@Narration,Cargo_Id=@Cargo_Id,Cartons=@cartons,Weight=@Weight,
                                   Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Return_Type=@Return_Type,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,
                                   TandC=@TandC,Payment_Terms=@Payment_Terms,Salutation=@Salutation,Contact_Name=@Contact_Name,
                                   Contact_Address1=@Contact_Address1,Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,
                                   State_ID=@State_ID,Country_ID=@Country_ID,Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                   Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email where Pr_Id=@Pr_Id";
                    db.CreateParameters(32);
                    db.AddParameters(0, "@Return_Date", this.ReturnDate);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(3, "@Return_From", this.ReturnFrom);
                    db.AddParameters(4, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(5, "@Gross_Amount", this.Gross);
                    db.AddParameters(6, "@Net_Amount", this.NetAmount);
                    db.AddParameters(7, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Cargo_Id", this.CargoId);
                    db.AddParameters(10, "@Narration", this.Narration);
                    db.AddParameters(11, "@Status", this.Status);
                    db.AddParameters(12, "@Cartons", this.Cartons);
                    db.AddParameters(13, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(14, "@Return_Type", this.ReturnType);
                    db.AddParameters(15, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(16, "@Job_Id", this.JobId);
                    db.AddParameters(17, "@TandC", this.TermsandConditon);
                    db.AddParameters(18, "@Payment_Terms", this.Payment_Terms);
                    db.AddParameters(19, "@Weight", this.Weight);
                    db.AddParameters(20, "@Pr_Id", this.ID);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(21, "@Salutation", address.Salutation);
                        db.AddParameters(22, "@Contact_Name", address.ContactName);
                        db.AddParameters(23, "@Contact_Address1", address.Address1);
                        db.AddParameters(24, "@Contact_Address2", address.Address2);
                        db.AddParameters(25, "@Contact_City", address.City);
                        db.AddParameters(26, "@State_ID", address.StateID);
                        db.AddParameters(27, "@Country_ID", address.CountryID);
                        db.AddParameters(28, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(29, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(30, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(31, "@Contact_Email", address.Email);
                    }
                    db.Open();
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInPurchaseEntry)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            item.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            item.Gross = item.Quantity * prod.CostPrice;
                            item.NetAmount = item.Gross + item.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_RETURN_DETAILS(Pr_Id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Remarks,Location_Id,Status,
                            Return_From,Item_Id,Instance_Id) values(@Pr_Id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Remarks,@Location_Id,
                            @Status,@Return_From,@Item_Id,@Instance_Id);";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Pr_Id", this.ID);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.AddParameters(2, "@Quantity", item.Quantity);
                            db.AddParameters(3, "@Mrp", item.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", this.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", item.Gross);
                            db.AddParameters(8, "@Net_Amount", item.NetAmount);
                            db.AddParameters(9, "@Remarks", 0);
                            db.AddParameters(10, "@Location_Id", this.LocationId);
                            db.AddParameters(11, "@Return_From", item.ReturnFrom);
                            db.AddParameters(12, "@Status", 0);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;
                        }
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_RETURN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + ID);
                }

                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRT')[New_Order],Pr_Id from [TBL_PURCHASE_RETURN_REGISTER] where Pr_Id=" + ID);
                return new OutputMessage("Debit Note has been updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "DebitNote | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pr_Id"] });
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot update this entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Debit note could not be updated", false, Type.Others, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Debit note could not be updated", false, Type.Others, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            finally
            {
                db.Close();
            }

        }
        public static List<DebitNote> GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)
        {
            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pr.Pr_Id,isnull(pr.Return_Date,0) Return_Date,pr.Location_Id,pr.Supplier_Id,pr.Bill_No,pr.Return_Type,pr.Tax_Amount,it.Name[Item],sup.Name[Supplier]
                               ,pr.Gross_Amount,pr.Round_Off,pr.Net_Amount,pr.Other_Charges,pr.Narration,pr.[Status],prd.pr_Id,prd.item_id,prd.Quantity
                               ,prd.MRP,prd.Rate[Selling_Price],pr.Tax_Amount[P_Tax_Amount],prd.Instance_Id,prd.Gross_Amount[P_Gross_Amount],prd.Net_Amount[P_Net_Amount],l.Name[Location],
                               cm.Name[Company],tx.Percentage[Tax_Percentage],
							   it.Name[Item],it.Item_Code, 
							   l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],
                               cm.Address1[Company_Address1],cm.Address2[Company_Address2],cm.Mobile_No1[Company_Phone],cm.Reg_Id1[Company_RegId],
                               sup.Address1[Cus_Address1],sup.Address2[Cus_Address2],sup.Phone1[Cus_Phone],sup.Taxno1[Cus_taxNo],sup.Name[Customer]
							   ,cm.Logo[Company_Logo],cm.Email[Company_Email],isnull(pr.Cost_Center_Id,0)[Cost_Center_Id],isnull(pr.Job_Id,0)[Job_Id],cost.fcc_Name[Cost_Center],j.job_name[Job]
							     ,sup.Email[Cust_Email],pr.TandC,pr.Payment_Terms from TBL_PURCHASE_RETURN_REGISTER pr with(nolock)
                               left join TBL_PURCHASE_RETURN_DETAILS prd with(nolock) on prd.pr_Id=pr.pr_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pr.Location_Id
							   left join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pr.Supplier_Id
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=prd.Tax_Id                              
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=prd.Item_Id
							 
							   left join tbl_fin_CostCenter cost on cost.Fcc_ID=pr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.job_id=pr.job_id
                               where pr.Location_Id=@Location_Id {#supplierfilter#} {#daterangefilter#} order by pr.Return_Date desc";
                #endregion Query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and pr.Return_Date>=@fromdate and pr.Return_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and pr.Return_Date>=@fromdate and pr.Return_Date<=@todate ");
                }
                if (SupplierId != null && SupplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and pr.Supplier_Id=@Supplier_Id ");
                }
                else
                {

                    query = query.Replace("{#supplierfilter#}", string.Empty);
                }

                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", from);
                db.AddParameters(2, "@todate", to);
                db.AddParameters(3, "@Supplier_Id", SupplierId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<DebitNote> result = new List<DebitNote>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        DebitNote register = new DebitNote();
                        register.ID = row["Pr_Id"] != DBNull.Value ? Convert.ToInt32(row["Pr_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.BillNo = Convert.ToString(row["Bill_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.ReturnDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Pr_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_Id"]) : 0;
                            item.DetailsID = rowItem["Pr_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Pr_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = Convert.ToDecimal(rowItem["Selling_Price"]);
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Quantity"] != DBNull.Value ? Convert.ToDecimal(rowItem["Quantity"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            products.Add(item);
                            dt.Rows.RemoveAt(0);
                        }
                        register.Products = products;
                        result.Add(register);
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "DebitNote | GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)");
                return null;
            }
        }

        public static DebitNote GetDetails(int Id, int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pr.Pr_Id,isnull(pr.Return_Date,0) Return_Date,pr.Location_Id,pr.Supplier_Id,pr.Bill_No,pr.Return_Type,
                               pr.Tax_Amount,it.Name[Item],sup.Name[Supplier],pr.Gross_Amount,pr.Round_Off,pr.Net_Amount,pr.Other_Charges,pr.Narration,
                               pr.[Status],prd.pr_Id,prd.item_id,prd.Quantity,prd.MRP,prd.Rate[Selling_Price],pr.Tax_Amount[P_Tax_Amount],prd.Instance_Id,
                               prd.Gross_Amount[P_Gross_Amount],prd.Net_Amount[P_Net_Amount],l.Name[Location],cm.Name[Company],tx.Percentage[Tax_Percentage],
                               it.Name[Item],it.Item_Code,l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId], 
                               cm.Address1[Company_Address1],cm.Address2[Company_Address2],cm.Mobile_No1[Company_Phone],cm.Reg_Id1[Company_RegId],
                               sup.Taxno1[Cus_taxNo],cm.Logo[Company_Logo],cm.Email[Company_Email],isnull(pr.Cost_Center_Id,0)[Cost_Center_Id],
                               isnull(pr.Job_Id,0)[Job_Id],cost.fcc_Name[Cost_Center],j.job_name[Job],pr.TandC,pr.Payment_Terms,pr.Salutation,
                               pr.Contact_Name,pr.Contact_Address1,pr.Contact_Address2,pr.Contact_City,pr.Contact_Email,pr.Contact_Zipcode,
                               pr.Contact_Phone1,pr.Contact_Phone2,pr.Country_Id,pr.State_Id,coun.Name[Sup_Country],st.Name[Sup_State]
                               from TBL_PURCHASE_RETURN_REGISTER pr with(nolock)
                               left join TBL_PURCHASE_RETURN_DETAILS prd with(nolock) on prd.pr_Id=pr.pr_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pr.Location_Id
                               left join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pr.Supplier_Id
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=prd.Tax_Id                              
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=prd.Item_Id
                               left join TBL_COUNTRY_MST coun on coun.Country_Id=pr.Country_ID
                               left join TBL_STATE_MST st on st.State_Id=pr.State_ID
                               left join tbl_fin_CostCenter cost on cost.Fcc_ID=pr.Cost_Center_Id
                               left join TBL_JOB_MST j on j.job_id=pr.job_id
                               where pr.Location_Id=@Location_Id and pr.Pr_Id=@Pe_Id order by pr.Created_Date desc";
                #endregion Query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Pe_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    DataRow row = dt.Rows[0];
                    DebitNote register = new DebitNote();
                    register.ID = row["Pr_Id"] != DBNull.Value ? Convert.ToInt32(row["Pr_Id"]) : 0;
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                    register.BillNo = Convert.ToString(row["Bill_No"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.ReturnDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                    register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                    register.Supplier = Convert.ToString(row["Supplier"]);
                    register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                    List<Entities.Master.Address> addresslist = new List<Master.Address>();
                    Entities.Master.Address address = new Master.Address();
                    address.ContactName = Convert.ToString(row["Contact_Name"]);
                    address.Address1 = Convert.ToString(row["Contact_Address1"]);
                    address.Address2 = Convert.ToString(row["Contact_Address2"]);
                    address.City = Convert.ToString(row["Contact_City"]);
                    address.Email = Convert.ToString(row["Contact_Email"]);
                    address.Phone1 = Convert.ToString(row["Contact_Phone1"]);
                    address.Phone2 = Convert.ToString(row["Contact_Phone2"]);
                    address.Zipcode = Convert.ToString(row["Contact_Zipcode"]);
                    address.Salutation = Convert.ToString(row["Salutation"]);
                    address.State = Convert.ToString(row["Sup_State"]);
                    address.Country = Convert.ToString(row["Sup_Country"]);
                    address.CountryID = row["Country_ID"] != DBNull.Value ? Convert.ToInt32(row["Country_ID"]) : 0;
                    address.StateID = row["State_ID"] != DBNull.Value ? Convert.ToInt32(row["State_ID"]) : 0;
                    addresslist.Add(address);
                    register.BillingAddress = addresslist;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Pr_Id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_Id"]) : 0;
                        item.DetailsID = rowItem["Pr_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Pr_Id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.Name = Convert.ToString(rowItem["Item"]);
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                        item.CostPrice = Convert.ToDecimal(rowItem["Selling_Price"]);
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
                        item.Quantity = rowItem["Quantity"] != DBNull.Value ? Convert.ToDecimal(rowItem["Quantity"]) : 0;
                        item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                        products.Add(item);
                        dt.Rows.RemoveAt(0);
                    }
                    register.Products = products;
                    return register;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "DebitNote | GetDetails(int Id,int LocationId)");
                return null;
            }
        }

        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseDebitNote, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "DebitNote | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "DebitNote| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string count = @"select count(*) from TBL_FIN_SUPPLIER_PAYMENTS where Pe_Id=" + this.ID;
                    db.Open();
                    int a = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, count));
                    if (a == 0)
                    {
                        string query = "delete from TBL_PURCHASE_RETURN_DETAILS where Pr_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);

                        db.BeginTransaction();
                        db.ExecuteNonQuery(CommandType.Text, query);
                        query = "delete from  TBL_PURCHASE_RETURN_REGISTER  where Pr_Id=@ID";
                        db.ExecuteNonQuery(CommandType.Text, query);
                        db.CommitTransaction();
                        Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", this.ID, this.ModifiedBy);
                        return new OutputMessage("Debit note request has been deleted", true, Type.NoError, "DebitNote | Delete", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.Others, "DebitNote | Delete", System.Net.HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "DebitNote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Debit note could not be deleted", false, Type.RequiredFields, "DebitNote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Debit note could not deleted", false, Type.Others, " DebitNote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static OutputMessage SendMail(int PurchaseId, string toAddress, int userId, string url)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseDebitNote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "DebitNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "DebitNote | Send Mail", System.Net.HttpStatusCode.InternalServerError);
            }
            try
            {
                DBManager db = new DBManager();

                //getting the email content from print page
                WebClient client = new WebClient();
                UTF8Encoding utf8 = new UTF8Encoding();
                string htmlMarkup = utf8.GetString(client.DownloadData(url));
                //creating PDF 
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertUrl(url);
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    bytes = ms.ToArray();
                    ms.Close();
                    doc.DetachStream();
                }
                string DebitNoteNo = "";
                string query = @"select bill_no from TBL_PURCHASE_RETURN_REGISTER where Pr_Id =@Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Id", PurchaseId);
                db.Open();
                DebitNoteNo = Convert.ToString(db.ExecuteScalar(CommandType.Text, query));
                db.Close();
                //sending the mail
                query = @"SELECT * from 
                                       (SELECT keyValue AS Email_Id FROM TBL_SETTINGS where KeyID=106) AS Email_Id,
                                       (SELECT keyValue AS Email_Password FROM TBL_SETTINGS where KeyID=107) AS Email_Password,
                                       (SELECT keyValue AS Email_Host FROM TBL_SETTINGS where KeyID=108) AS Email_Host,
                                       (SELECT keyValue AS Email_Port FROM TBL_SETTINGS where KeyID=109) AS Email_Port";
                DataTable dt = new DataTable();
                db.Open();
                dt = db.ExecuteQuery(CommandType.Text, query);
                MailMessage mail = new MailMessage(Convert.ToString(dt.Rows[0]["Email_Id"]), toAddress);
                mail.IsBodyHtml = false;
                mail.Subject = "Debit Note";
                mail.Body = "Please Find the attached copy of Debit Note";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Debit Note "+DebitNoteNo+".pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "Purchase Retrun | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Purchase Return | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }
        #endregion Functions
    }
}
