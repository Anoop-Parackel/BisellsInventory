using Core.DBManager;
using Entities.Application;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
    public class PurchaseEntryRegister : Register, IRegister
    {

        #region Properties
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string InvoiceNo { get; set; }
        public string EntryNo { get; set; }
        public string FinancialYear { get; set; }
        public bool IsMigrated { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDateString { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string SupplierAddress1 { get; set; }
        public string SupplierAddress2 { get; set; }
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
        public decimal BalanceAmount { get; set; }
        public int PaymentStatus { get; set; }
        public decimal PaidAmount { get; set; }
        public int JobId { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public string JobName { get; set; }
        public string LocationPhone { get; set; }
        public string SupplierTaxNo { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public List<Item> Products { get; set; }
        public override decimal NetAmount { get; set; }
        public string CompanyRegId { get; private set; }
        public string CompanyAddress1 { get; private set; }
        public string CompanyAddress2 { get; private set; }
        public string CompanyPhone { get; private set; }
        #endregion Properties

        #region Constructor
        public PurchaseEntryRegister(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public PurchaseEntryRegister(int ID)
        {
            this.ID = ID;
        }
        public PurchaseEntryRegister()
        {

        }
        #endregion Constructors

        #region Functions
        /// <summary>
        /// save details of purchase entry register and purchase entry details
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseEntry, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseEntry | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;

                //Main Validations. Use ladder-if after this "if" for more validations
                if (string.IsNullOrWhiteSpace(this.InvoiceNo))

                {
                    return new OutputMessage("Enter an Invoice Number to make a Entry.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.InvoiceDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Invoice Date to make a Entry.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a Entry.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.SupplierId == 0)
                {
                    return new OutputMessage("Select a Supplier to make a Entry.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Entry.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {
                    db.Open();

                    string qry = @"select isnull(count(*),0) from TBL_PURCHASE_ENTRY_REGISTER where Supplier_Id=@Supplier_Id and Invoice_No=@invoicenumber and Is_Migrated<>1";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@invoicenumber", this.InvoiceNo);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    int hasSupplierExist = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, qry));
                    if (hasSupplierExist > 0)
                    {
                        return new OutputMessage("This invoice number has already exists.", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    string query = @"insert into TBL_PURCHASE_ENTRY_REGISTER (Location_Id, Supplier_Id,Invoice_No,Invoice_Date,Entry_Date,Total_TaxAmount,Total_GrossAmount,Discount_Amount,  
                                   Other_Charges,Round_Off,Net_Amount,Narration,[Status],Created_By,Created_Date,Entry_No,Is_Migrated,Cost_Center_Id,Job_Id,TandC,Payment_Terms,Salutation,Contact_Name,
                                   Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2, Contact_Email) 
                                   values(@Location_Id, @Supplier_Id,@Invoice_No,@Invoice_Date,@Entry_Date,@Total_TaxAmount,@Total_GrossAmount,@Discount_Amount,@Other_Charges,  
                                   @Round_Off,@Net_Amount,@Narration,@Status,@Created_By,GETUTCDATE(),[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PEN'),@Is_Migrated,@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity;";
                    db.CreateParameters(30);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Invoice_Date", this.InvoiceDateString);
                    db.AddParameters(3, "@Entry_Date", this.EntryDateString);
                    db.AddParameters(4, "@Total_TaxAmount", this.TaxAmount);
                    db.AddParameters(5, "@Total_GrossAmount", this.Gross);
                    db.AddParameters(6, "@Discount_Amount", this.Discount);
                    db.AddParameters(7, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Net_Amount", this.NetAmount);
                    db.AddParameters(10, "@Narration", this.Narration);
                    db.AddParameters(11, "@Status", this.Status);
                    db.AddParameters(12, "@Created_By", this.CreatedBy);
                    db.AddParameters(13, "@Invoice_No", this.InvoiceNo);
                    db.AddParameters(14, "@Is_Migrated", this.IsMigrated);
                    db.AddParameters(15, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(16, "@Job_Id", this.JobId);
                    db.AddParameters(17, "@TandC", this.TermsandConditon);
                    db.AddParameters(18, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(19, "@Salutation", address.Salutation);
                        db.AddParameters(20, "@Contact_Name", address.ContactName);
                        db.AddParameters(21, "@Contact_Address1", address.Address1);
                        db.AddParameters(22, "@Contact_Address2", address.Address2);
                        db.AddParameters(23, "@Contact_City", address.City);
                        db.AddParameters(24, "@State_ID", address.StateID);
                        db.AddParameters(25, "@Country_ID", address.CountryID);
                        db.AddParameters(26, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(27, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(28, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(29, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PEN", db);
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
                            query = @"insert into TBL_PURCHASE_ENTRY_DETAILS(Pe_Id,Item_Id,Qty,Mrp,Rate,Selling_Price,Tax_Id,
                                    Tax_Amount,Gross_Amount,Net_Amount,Status,instance_id,Is_GRN,Description,Converted_From,Converted_Id,Is_Stock_Affected)
                                    values(@Pe_Id,@Item_Id,@Qty,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Status,@instance_id,@Is_GRN,@Description,@Converted_From,@Pqd_Id,@Is_Stock_Affected);
                                    update TBL_GRN_DETAILS set Received_Quantity=@Qty where Grn_Detail_ID=@Ped_Id";
                            db.CreateParameters(18);
                            db.AddParameters(0, "@Ped_Id", item.DetailsID);//***
                            db.AddParameters(1, "@Pe_Id", identity);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", item.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Selling_Price", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", item.Gross);
                            db.AddParameters(10, "@Net_Amount", item.NetAmount);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Pqd_Id", item.QuoteDetailId);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.AddParameters(14, "@Is_GRN", item.IsGRN);
                            db.AddParameters(15, "@Description", item.Description);
                            db.AddParameters(16, "@Converted_From", item.ConvertedFrom);
                            db.AddParameters(17, "@Is_Stock_Affected", item.AffectedinStock);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;

                            //Updates the Purchase quote details status to 1 and if all the data in register is 1 
                            //then set the status of register to 1
                            //query = @"update TBL_PURCHASE_QUOTE_DETAILS set Status=1 where Pqd_Id=@pqd_id;
                            //        declare @registerId int,@total int,@totalQouted int; 
                            //        select @registerId= pq_id from TBL_PURCHASE_QUOTE_DETAILS where Pqd_Id=@pqd_id;
                            //        select @total= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId;
                            //        select @totalQouted= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId and Status=1;
                            //        if(@total=@totalQouted) 
                            //        begin update TBL_PURCHASE_QUOTE_REGISTER set Status=1 where Pq_Id=@registerId end";
                            //db.CreateParameters(1);
                            //db.AddParameters(0, "@pqd_id", item.QuoteDetailId);

                            //db.ExecuteProcedure(System.Data.CommandType.Text, query);

                            ////Updates the TBL_GRN_DETAILS status to 1 and if all the data in register is 1
                            ////then updates the status of register to 1
                            //query = @"update TBL_GRN_DETAILS set Status=1 where Grn_Detail_ID=@pqd_id;
                            //        declare @registerId int,@total int,@totalQouted int; 
                            //        select @registerId= GRN_ID from TBL_GRN_DETAILS where Grn_Detail_ID=@pqd_id;
                            //        select @total= count(*) from TBL_GRN_DETAILS where GRN_ID=@registerId;
                            //        select @totalQouted= count(*) from TBL_GRN_DETAILS where GRN_ID=@registerId and Status=1;
                            //        if(@total=@totalQouted) 
                            //        begin update TBL_GRN_REGISTER set Status=1 where GRN_ID=@registerId end";
                            //db.CreateParameters(1);
                            //db.AddParameters(0, "@pqd_id", item.DetailsID);

                            //db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }
                    }
                    decimal _NetAmount = 0;
                    dynamic Settings = Application.Settings.GetFeaturedSettings();
                    if (!Settings.IsDiscountEnabled)
                    {
                        this.Discount = 0;
                    }
                    else
                    {
                        this.NetAmount = this.NetAmount - this.Discount;
                    }
                    if (Settings.AutoRoundOff)
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        this.NetAmount = Math.Round(this.NetAmount, 2);
                        _NetAmount = this.NetAmount + this.RoundOff;
                    }
                    else
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_ENTRY_REGISTER] set [Total_TaxAmount]=" + this.TaxAmount + ", [Total_GrossAmount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pe_Id=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PEN')[New_Order],Pe_Id from tbl_purchase_entry_register where Pe_Id=" + identity);
                return new OutputMessage("Purchase entry has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Entry | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pe_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Entry could not be saved", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Update purchase entry register and purchase entry details 
        ///to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseEntry, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseEntry | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Entry.", false, Type.Others, "Purchase Entry | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (string.IsNullOrWhiteSpace(this.InvoiceNo))
                {
                    return new OutputMessage("Enter Invoice Number to update an Entry", false, Type.Others, "Purchase Entry | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                //else if (HasReference(this.ID))
                //{
                //    db.RollBackTransaction();

                //    return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "Purchase Entry | Update", System.Net.HttpStatusCode.InternalServerError);

                //}
                else
                {
                    db.Open();

                    string qry = @"select isnull(count(*),0) from TBL_PURCHASE_ENTRY_REGISTER where Supplier_Id=@Supplier_Id and Invoice_No=@invoicenumber and  pe_id not in( @pe_id) and Is_Migrated<>1";
                    db.CreateParameters(3);
                    db.AddParameters(0, "@invoicenumber", this.InvoiceNo);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@pe_id", this.ID);
                    int hasSupplierExist = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, qry));
                    if (hasSupplierExist > 0)
                    {
                        return new OutputMessage("This invoice number has already exists", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    string query = @"delete from TBL_PURCHASE_ENTRY_DETAILS where Pe_Id=@Id;
                                   update TBL_PURCHASE_ENTRY_REGISTER set Location_Id=@Location_Id,Supplier_Id=@Supplier_Id,Invoice_No=@Invoice_No,Invoice_Date=@Invoice_Date,Entry_Date=@Entry_Date,Total_TaxAmount=@Total_TaxAmount,
                                   Total_GrossAmount=@Total_GrossAmount,Discount_Amount=@Discount_Amount,Other_Charges=@Other_Charges,Round_Off=@Round_Off,Net_Amount=@Net_Amount,Narration=@Narration,[Status]=@Status,
                                   Modified_By=@Modified_By,Is_Migrated=@Is_Migrated,Modified_Date=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,TandC=@TandC,Payment_Terms=@Payment_Terms,
                                   Salutation=@Salutation,Contact_Name=@Contact_Name,Contact_Address1=@Contact_Address1,Contact_Address2=@Contact_Address2,
                                   Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,Contact_Zipcode=@Contact_Zipcode,
                                   Contact_Phone1=@Contact_Phone1,Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email where Pe_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(31);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Invoice_No", this.InvoiceNo);
                    db.AddParameters(3, "@Invoice_Date", this.InvoiceDate);
                    db.AddParameters(4, "@Entry_Date", this.EntryDate);
                    db.AddParameters(5, "@Total_TaxAmount", this.TaxAmount);
                    db.AddParameters(6, "@Total_GrossAmount", this.Gross);
                    db.AddParameters(7, "@Discount_Amount", this.Discount);
                    db.AddParameters(8, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(9, "@Round_Off", this.RoundOff);
                    db.AddParameters(10, "@Net_Amount", this.NetAmount);
                    db.AddParameters(11, "@Narration", this.Narration);
                    db.AddParameters(12, "@Status", this.Status);
                    db.AddParameters(13, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(14, "@Is_Migrated", this.IsMigrated);
                    db.AddParameters(15, "@Id", this.ID);
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
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Update ", System.Net.HttpStatusCode.InternalServerError);
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
                            query = @"insert into TBL_PURCHASE_ENTRY_DETAILS(Pe_Id,Item_Id,Qty,Mrp,Rate,Selling_Price,Tax_Id,
                                    Tax_Amount,Gross_Amount,Net_Amount,Status,Pqd_Id,instance_id,Is_GRN,Description)
                                    values(@Pe_Id,@Item_Id,@Qty,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Status,@Pqd_Id,@instance_id,@Is_GRN,@Description)";
                            db.CreateParameters(16);
                            db.AddParameters(0, "@Ped_Id", item.DetailsID);//**
                            db.AddParameters(1, "@Pe_Id", ID);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", item.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Selling_Price", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", item.Gross);
                            db.AddParameters(10, "@Net_Amount", item.NetAmount);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Pqd_Id", item.QuoteDetailId);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.AddParameters(14, "@Is_GRN", item.IsGRN);
                            db.AddParameters(15, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;
                            query = @"update TBL_PURCHASE_QUOTE_DETAILS set Status=1 where Pqd_Id=@pqd_id;
                                    declare @registerId int,@total int,@totalQouted int; 
                                    select @registerId= pq_id from TBL_PURCHASE_QUOTE_DETAILS where Pqd_Id=@pqd_id;
                                    select @total= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId;
                                    select @totalQouted= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId and Status=1;
                                    if(@total=@totalQouted) 
                                    begin update TBL_PURCHASE_QUOTE_REGISTER set Status=1 where Pq_Id=@registerId end";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@pqd_id", item.QuoteDetailId);

                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }

                    }
                    decimal _NetAmount = 0;

                    dynamic Settings = Application.Settings.GetFeaturedSettings();
                    if (!Settings.IsDiscountEnabled)
                    {
                        this.Discount = 0;
                    }
                    else
                    {
                        this.NetAmount = this.NetAmount - this.Discount;
                    }
                    if (Settings.AutoRoundOff)
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {

                        this.NetAmount = Math.Round(this.NetAmount, 2);
                        _NetAmount = this.NetAmount + this.RoundOff;
                    }
                    else
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_ENTRY_REGISTER] set [Total_TaxAmount]=" + this.TaxAmount + ", [Total_GrossAmount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pe_Id=" + ID);
                }

                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PEN')[New_Order],Pe_Id from tbl_purchase_entry_register where Pe_Id=" + ID);
                return new OutputMessage("Purchase entry has been updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Entry | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pe_Id"] });
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "PurchaseEntry | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Purchase entry could not be updated", false, Type.Others, "Purchase Entry | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Purchase entry could not be updated", false, Type.Others, "Purchase Entry | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// retrieve purchase entry register and purchase entry details for find function
        /// </summary>
        /// <param name="LocationId">location id of that particular item list</param>
        /// <returns></returns>
        public static List<PurchaseEntryRegister> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],isnull(pe.Invoice_No,0) Invoice_No,pe.Invoice_Date[Invoice_Date],
                                pe.Entry_Date[Entry_Date],isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount]
                               ,isnull(pe.Discount_Amount,0)[Discount_Amount],isnull(pe.Entry_No,0)[Entry_No],isnull(pe.Other_Charges,0)[Other_Charges],
							   isnull(pe.Round_Off,0)[Round_Off],isnull(pe.Net_Amount,0)[Net_Amount],pe.Narration,isnull( pe.[Status],0)[Status],
                               isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],isnull(ped.Item_Id,0)[Item_Id],ped.Pqd_Id,isnull(ped.Qty,0)[Qty]
                               ,isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],isnull(ped.Selling_Price,0)[Selling_Price],isnull(ped.Tax_Amount,0)[P_Tax_Amount],
							   isnull(ped.Gross_Amount,0)[P_Gross_Amount],isnull(ped.Net_Amount,0)[P_Net_Amount],isnull(ped.instance_id,0)[instance_id],
                               l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],sup.Name[Supplier],
                               it.Item_Id,it.Item_Code,it.Name[Item],sup.Address1[Sup_Address1],sup.Address2[Sup_Address2],sup.Phone1[Sup_Phone],
							   l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],sup.Taxno1[Supplier_TaxNo]
                               ,l.Reg_Id1[Location_RegistrationId],cmp.Email[Comp_Email],pe.TandC,pe.Payment_Terms from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               left join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               left join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               where  pe.Location_Id=@Location_Id and pe.Is_Migrated<>1 order by pe.Created_Date desc";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseEntryRegister> result = new List<PurchaseEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseEntryRegister register = new PurchaseEntryRegister();
                        register.ID = row["PurchaseEntryId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseEntryId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.InvoiceNo = Convert.ToString(row["Invoice_No"]);
                        register.InvoiceDateString = row["Invoice_Date"] != DBNull.Value ? Convert.ToDateTime(row["Invoice_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_TaxAmount"]) : 0;
                        register.Gross = row["Total_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_GrossAmount"]) : 0;
                        register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.SupplierTaxNo = Convert.ToString(row["Supplier_TaxNo"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.SupplierAddress1 = Convert.ToString(row["Sup_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Sup_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Sup_Phone"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_Id"]) : 0;
                            item.DetailsID = rowItem["ped_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["ped_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.QuoteDetailId = rowItem["Pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Pqd_Id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseEntry | GetDetails(int LocationId)");
                return null;
            }
        }
        /// <summary>
        /// Retrieve list of purchase entries 
        /// </summary>
        /// <param name="LocationID">Id of the location</param>
        /// <param name="SupplierId">Id of Supplier</param>
        /// <param name="from">From date to filter entries</param>
        /// <param name="to">To date to filter entries</param>
        /// <returns>list of purchase entries</returns>
        public static List<PurchaseEntryRegister> GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)
        {
            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],isnull(pe.Invoice_No,0) Invoice_No,pe.Invoice_Date[Invoice_Date],
                               pe.Entry_Date[Entry_Date],isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount]
                               ,isnull(pe.Discount_Amount,0)[Discount_Amount],isnull(pe.Entry_No,0)[Entry_No],isnull(pe.Other_Charges,0)[Other_Charges],
                               isnull(pe.Round_Off,0)[Round_Off],isnull(pe.Net_Amount,0)[Net_Amount],pe.Narration,isnull( pe.[Status],0)[Status],
                               isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],isnull(ped.Item_Id,0)[Item_Id],ped.Pqd_Id,isnull(ped.Qty,0)[Qty]
                               ,isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],isnull(ped.Selling_Price,0)[Selling_Price],isnull(ped.Tax_Amount,0)[P_Tax_Amount],
                               isnull(ped.Gross_Amount,0)[P_Gross_Amount],isnull(ped.Net_Amount,0)[P_Net_Amount],isnull(ped.instance_id,0)[instance_id],
                               l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],sup.Name[Supplier],
                               it.Item_Id,it.Item_Code,it.Name[Item],sup.Address1[Sup_Address1],sup.Address2[Sup_Address2],sup.Phone1[Sup_Phone],
                               cmp.Address1[Comp_Address1],cmp.Address2[Comp_Address2],cmp.Mobile_No1[Comp_Phone],
                               l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],ped.Description,
                               isnull(pe.Cost_Center_Id,0)[Cost_Center_Id],isnull(pe.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job],ISNULL(ped.Is_GRN,0) Is_GRN,sup.email[Sup_Email],pe.TandC,pe.Payment_Terms
                               from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               inner join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               inner join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               inner join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               inner join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               inner join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               left join tbl_fin_CostCenter cost on cost.Fcc_ID=pe.Cost_Center_Id
                               left join TBL_JOB_MST j on j.job_id=pe.job_Id
                               where  pe.Location_Id=@Location_Id and pe.Is_Migrated<>1 {#supplierfilter#} {#daterangefilter#} order by pe.Created_Date desc";
                #endregion Query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and pe.Entry_Date>=@fromdate and pe.Entry_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and pe.Entry_Date>=@fromdate and pe.Entry_Date<=@todate ");
                }
                if (SupplierId != null && SupplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and pe.Supplier_Id=@Supplier_Id ");
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
                    List<PurchaseEntryRegister> result = new List<PurchaseEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseEntryRegister register = new PurchaseEntryRegister();
                        register.ID = row["PurchaseEntryId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseEntryId"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.InvoiceNo = Convert.ToString(row["Invoice_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.InvoiceDateString = row["Invoice_Date"] != DBNull.Value ? Convert.ToDateTime(row["Invoice_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_TaxAmount"]) : 0;
                        register.Gross = row["Total_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_GrossAmount"]) : 0;
                        register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.SupplierAddress1 = Convert.ToString(row["Sup_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Sup_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Sup_Phone"]);
                        register.SupplierEmail = Convert.ToString(row["Sup_Email"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_Id"]) : 0;
                            item.DetailsID = rowItem["ped_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["ped_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.QuoteDetailId = rowItem["Pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Pqd_Id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.IsGRN = rowItem["Is_GRN"] !=DBNull.Value? Convert.ToInt32(rowItem["Is_GRN"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseEntry | GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)");
                return null;
            }
        }
        /// <summary>
        /// Retrieve a particular entry from the list
        /// </summary>
        /// <param name="Id">Id of the particular entry</param>
        /// <param name="LocationId">Id of location</param>
        /// <returns>Single purchase entry details</returns>
        public static PurchaseEntryRegister GetDetails(int Id, int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],
                               isnull(pe.Invoice_No,0) Invoice_No,pe.Invoice_Date[Invoice_Date],pe.Entry_Date[Entry_Date],
                               isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount],
                               isnull(pe.Discount_Amount,0)[Discount_Amount],isnull(pe.Entry_No,0)[Entry_No],isnull(pe.Other_Charges,0)[Other_Charges],
                               isnull(pe.Round_Off,0)[Round_Off],isnull(pe.Net_Amount,0)[Net_Amount],pe.Narration,isnull( pe.[Status],0)[Status],
                               isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],isnull(ped.Item_Id,0)[Item_Id],ped.Pqd_Id,
                               isnull(ped.Qty,0)[Qty],isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],isnull(ped.Selling_Price,0)[Selling_Price],
                               isnull(ped.Tax_Amount,0)[P_Tax_Amount],isnull(ped.Gross_Amount,0)[P_Gross_Amount],isnull(ped.Net_Amount,0)[P_Net_Amount],
                               isnull(ped.instance_id,0)[instance_id],l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],
                               sup.Name[Supplier],it.Item_Id,it.Item_Code,it.Name[Item],l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],
                               l.Reg_Id1[Loc_RegId],sup.Taxno1[Sup_TaxNo],cmp.Email[Comp_Email],coun.Name[Sup_Country],st.Name[Sup_State],ped.Description,
                               isnull(pe.Cost_Center_Id,0)[Cost_Center_Id],isnull(pe.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job],
                               isnull(ped.Is_GRN,0) Is_GRN,supview.BalAmount,supview.AvaAmount,supview.Status[Payment_Status],pe.TandC,pe.Payment_Terms,
                               pe.Salutation,pe.Contact_Name,pe.Contact_Address1,pe.Contact_Address2,pe.Contact_City,pe.State_ID,pe.Country_ID,
                               pe.Contact_Zipcode,pe.Contact_Phone1,pe.Contact_Phone2,pe. Contact_Email
                               from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               inner join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               inner join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               inner join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               inner join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               left join tbl_country_mst coun with(nolock) on coun.Country_Id=pe.Country_Id
                               left join tbl_state_mst st with(nolock) on st.State_Id=pe.State_Id
                               inner join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               left join tbl_fin_CostCenter cost on cost.Fcc_ID=pe.Cost_Center_Id
                               left join [VW_SUPPLIER_PAYMENTS] supview on supview.Pe_Id=pe.Pe_Id
                               left join TBL_JOB_MST j on j.job_id=pe.job_Id
                               where pe.Location_Id=@Location_Id and pe.Pe_Id=@Pe_Id and pe.Is_Migrated<>1 order by pe.Created_Date desc";
                #endregion Query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Pe_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    DataRow row = dt.Rows[0];
                    PurchaseEntryRegister register = new PurchaseEntryRegister();
                    register.ID = row["PurchaseEntryId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseEntryId"]) : 0;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                    register.InvoiceNo = Convert.ToString(row["Invoice_No"]);
                    register.InvoiceDateString = row["Invoice_Date"] != DBNull.Value ? Convert.ToDateTime(row["Invoice_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.TaxAmount = row["Total_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_TaxAmount"]) : 0;
                    register.Gross = row["Total_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_GrossAmount"]) : 0;
                    register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                    register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.EntryNo = Convert.ToString(row["Entry_No"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                    register.SupplierState = Convert.ToString(row["Sup_State"]);
                    register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                    register.SupplierTaxNo = Convert.ToString(row["Sup_TaxNo"]);
                    register.Supplier = Convert.ToString(row["Supplier"]);
                    register.Location = Convert.ToString(row["Location"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                    register.Company = Convert.ToString(row["Company"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    register.BalanceAmount = row["BalAmount"]!=DBNull.Value? Convert.ToDecimal(row["BalAmount"]):0;
                    register.PaymentStatus = row["Payment_Status"]!=DBNull.Value? Convert.ToInt32(row["Payment_Status"]):0;
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
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_Id"]) : 0;
                        item.DetailsID = rowItem["ped_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["ped_Id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.QuoteDetailId = rowItem["Pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Pqd_Id"]) : 0;
                        item.Name = rowItem["Item"].ToString();
                        item.Description = Convert.ToString(rowItem["Description"]);
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                        item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                        item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                        item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                        item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                        item.IsGRN = rowItem["Is_GRN"] != DBNull.Value ? Convert.ToInt32(rowItem["Is_GRN"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseEntry | GetDetails(int Id,int LocationId)");
                return null;
            }
        }
        /// <summary>
        /// get item And supplier wise details for purchase return
        /// </summary>
        /// <param name="LocationId">Id of location</param>
        /// <returns>list of details</returns>
        public static List<PurchaseEntryRegister> GetDetailsItemWise(int LocationId, int SupplierId, int ItemId, int instanceId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select ped.pe_id,ped_id,ped.Item_Id,Invoice_No,ped.instance_id,per.Entry_No,Invoice_Date,
                               ped.Gross_Amount,ped.mrp,per.Entry_Date,per.Supplier_Id,ped.Ped_Id,tax.Percentage [Tax_Percentage],
                               sup.Name [supplier], per.location_id,ped.Pe_Id,ped.Qty,ped.Rate,ped.Selling_Price,ped.rate,
                               itm.name [item],itm.Item_Code ,ped.Tax_Amount,per.Narration,per.Net_Amount,per.status,per.Round_Off,per.TandC,per.Payment_Terms,
				               per.Total_GrossAmount,per.Total_TaxAmount,(select COUNT(*) from TBL_PURCHASE_ENTRY_DETAILS where Pe_Id=per.Pe_Id) Total_items
                               from TBL_PURCHASE_ENTRY_DETAILS ped with (nolock)
                               inner join TBL_PURCHASE_ENTRY_REGISTER per with (nolock) on per.Pe_Id=ped.Pe_Id
	                           left join TBL_TAX_MST tax with (nolock) on tax.Tax_Id=ped.Tax_Id
                               left join TBL_SUPPLIER_MST sup with (nolock) on sup.Supplier_Id=per.Supplier_Id
		                       left join TBL_ITEM_MST itm with (nolock) on itm.item_id=ped.Item_Id
                               where ped.item_id=@Item_Id and per.Supplier_Id=@Supplier_Id 
                               and per.Location_Id=@Location_Id and ped.instance_id=@instanceId order by per.Created_Date desc";
                #endregion query
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Supplier_Id", SupplierId);
                db.AddParameters(2, "@Item_Id", ItemId);
                db.AddParameters(3, "@instanceId", instanceId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseEntryRegister> result = new List<PurchaseEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseEntryRegister register = new PurchaseEntryRegister();
                        register.ID = row["pe_id"] != DBNull.Value ? Convert.ToInt32(row["pe_id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.InvoiceNo = Convert.ToString(row["Invoice_No"]);
                        register.InvoiceDateString = row["Invoice_Date"] != DBNull.Value ? Convert.ToDateTime(row["Invoice_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_TaxAmount"]) : 0;
                        register.Gross = row["Total_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(row["Total_GrossAmount"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        register.TotalItems = row["Total_items"] != DBNull.Value ? Convert.ToInt32(row["Total_items"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("pe_id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.PedId = rowItem["ped_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["ped_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseEntry | GetDetailsItemWise(int LocationId, int SupplierId, int ItemId,int instanceId)");
                return null;
            }
        }
        /// <summary>
        /// used for to get purchase entry details for purchase return
        /// </summary>
        /// <param name="LocationId">Id of location</param>
        /// <param name="Id">Id of the entry</param>
        /// <returns>list of entries</returns>
        public static List<PurchaseEntryRegister> GetDetailsFromEntry(int LocationId, int Id)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select ped.ped_id,ped.pe_id,ped.Item_Id,ped.Instance_Id,ped.mrp,ped.rate,ped.Selling_Price,
                               ped.Tax_Id,ped.Gross_Amount,ped.Net_Amount,itm.Name [item],itm.Item_Code,tax.Percentage [tax_percentage],
				               ped.Qty,ped.Tax_Amount from TBL_PURCHASE_ENTRY_DETAILS ped with (nolock)
				               left join TBL_PURCHASE_ENTRY_REGISTER per on per.Pe_Id=ped.Pe_Id
				               left join TBL_TAX_MST tax with (nolock) on tax.Tax_Id=ped.Tax_Id
					           left join TBL_ITEM_MST itm with (nolock) on itm.item_id=ped.Item_Id
					         where ped.pe_id=@peId and per.Location_Id=@location  order by per.Created_Date DESC";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@location", LocationId);
                db.AddParameters(1, "@peId", Id);

                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseEntryRegister> result = new List<PurchaseEntryRegister>();

                    DataRow row = dt.Rows[0];
                    PurchaseEntryRegister register = new PurchaseEntryRegister();
                    register.ID = row["pe_id"] != DBNull.Value ? Convert.ToInt32(row["pe_id"]) : 0;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("pe_id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.PedId = rowItem["ped_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["ped_Id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.InstanceId = rowItem["Instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Instance_Id"]) : 0;
                        item.Name = rowItem["Item"].ToString();
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                        item.CostPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                        item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
                        item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                        item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                        products.Add(item);
                        dt.Rows.RemoveAt(0);
                    }
                    register.Products = products;
                    result.Add(register);

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "PurchaseEntry | GetDetailsFromEntry(int LocationId, int Id)");
                return null;
            }
        }
        /// <summary>
        /// delete details of purchase entry register and purchase entry details
        /// </summary>
        /// <returns>success alert when details deleted successfully otherwise returns an error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseEntry, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "PurchaseEntry| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string count = @"select count(*) from TBL_FIN_SUPPLIER_PAYMENTS where Pe_Id=" + this.ID;
                    db.Open();
                    int a = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, count));
                    if (a==0)
                    {
                        string query = "delete from TBL_PURCHASE_ENTRY_DETAILS where Pe_Id=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        
                        db.BeginTransaction();
                        db.ExecuteNonQuery(CommandType.Text, query);
                        query = "delete from  TBL_PURCHASE_ENTRY_REGISTER  where pe_Id=@id";
                        db.ExecuteNonQuery(CommandType.Text, query);
                        db.CommitTransaction();
                        Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                        return new OutputMessage("Purchase Entry Request has been deleted", true, Type.NoError, "PurchaseEntry | Delete", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.Others, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError);
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
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong.could not delete Purchase Entry", false, Type.Others, "Purchase Entry | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Function for sending mail
        /// </summary>
        /// <param name="PurchaseId">Id of the entry</param>
        /// <param name="toAddress">To address</param>
        /// <param name="userId">Id of user</param>
        /// <param name="url">Url of the page to mail</param>
        /// <returns>return success alert when mail send successfully otherwise returns an error alert</returns>
        public static OutputMessage SendMail(int PurchaseId, string toAddress, int userId, string url)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseEntry | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Purchase Entry | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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

                //sending the mail
                string query = @"SELECT * from 
                                       (SELECT keyValue AS Email_Id FROM TBL_SETTINGS where KeyID=106) AS Email_Id,
                                       (SELECT keyValue AS Email_Password FROM TBL_SETTINGS where KeyID=107) AS Email_Password,
                                       (SELECT keyValue AS Email_Host FROM TBL_SETTINGS where KeyID=108) AS Email_Host,
                                       (SELECT keyValue AS Email_Port FROM TBL_SETTINGS where KeyID=109) AS Email_Port";
                DataTable dt = new DataTable();
                db.Open();
                dt = db.ExecuteQuery(CommandType.Text, query);
                MailMessage mail = new MailMessage(Convert.ToString(dt.Rows[0]["Email_Id"]), toAddress);
                mail.IsBodyHtml = false;
                mail.Subject = "Purchase Bill";
                mail.Body = "Please Find the attached copy of Purchase Bill";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Purchase Bill.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "Purchase Entry | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Purchase Entry | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        /// <summary>
        /// check for the Purchase Entry is refered in another transactions
        /// </summary>
        /// <param name="PurchaseEntryid (id)"></param>
        /// <returns>bool which either have refernce or not</returns>
        //bool HasReference(int id)
        //{
        //    if (id == 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            DBManager db = new DBManager();
        //            db.Open();
        //            string query = @" ;with cte as (
        //                                 select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_SALES_ENTRY_DETAILS sed with(nolock) on sed.Ped_Id=ped.Ped_Id
        //                              join TBL_SALES_ENTRY_REGISTER sr with(nolock) on sed.Se_Id=sr.Se_Id where sr.Deleted<>1 AND ped.Pe_Id=@id
        //                             	union
        //                             select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_SALES_QUOTE_DETAILS sqd with(nolock) on sqd.Ped_Id=ped.Ped_Id
        //                              join TBL_SALES_QUOTE_REGISTER sqr with(nolock) on sqd.Sq_Id=sqr.Sq_Id where sqr.Deleted<>1 AND ped.Pe_Id=@id
        //                                 union 
        //                             	select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_SALES_request_DETAILS srd with(nolock) on srd.Ped_Id=ped.Ped_Id
        //                              join TBL_SALES_request_register srr with(nolock) on srd.Sr_Id=srr.Sr_Id where srr.Deleted<>1 AND ped.Pe_Id=@id
        //                             union
        //                             select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_SALES_return_DETAILS srd with(nolock) on srd.Ped_Id=ped.Ped_Id
        //                              join TBL_SALES_return_register srr with(nolock) on srd.Sret_Id=srr.Sret_Id where srr.Deleted<>1 AND ped.Pe_Id=@id
        //                             union 
        //                             select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join
        //                             TBL_TRANSFER_IN_DETAILS trd with(nolock) on trd.Ped_Id=ped.Ped_Id
        //                             join TBL_TRANSFER_IN_REGISTER tr with(nolock) on trd.Transfer_In_Id=tr.Transfer_In_Id where trd.Deleted<>1 AND ped.Pe_Id=@id
        //	 union
        //	 select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join
        //                             TBL_TRANSFER_out_DETAILS trd with(nolock) on trd.Ped_Id=ped.Ped_Id
        //                             join TBL_TRANSFER_out_REGISTER tr with(nolock) on trd.Transfer_out_Id=tr.Transfer_out_Id where trd.Deleted<>1 AND ped.Pe_Id=@id
        //                             union
        //                              select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_PURCHASE_return_details prd with(nolock) on prd.Ped_Id=ped.Ped_Id
        //                              join TBL_PURCHASE_return_register prr with(nolock) on prd.Pr_Id=prr.Pr_Id where prr.Deleted<>1 AND ped.Pe_Id=@id
        //                             union
        //                             select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join 
        //                              TBL_PURCHASE_return_details prd with(nolock) on prd.Ped_Id=ped.Ped_Id
        //                              join TBL_PURCHASE_return_register prr with(nolock) on prd.Pr_Id=prr.Pr_Id where prr.Deleted<>1 AND ped.Pe_Id=@id
        //	  union
        //	   select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join
        //                             TBL_register_IN_DETAILS rd with(nolock) on rd.Ped_Id=ped.Ped_Id
        //                             join TBL_register_IN_REGISTER r with(nolock) on rd.Reg_In_Id=r.Reg_In_Id where rd.Deleted<>1 AND ped.Pe_Id=@id
        //	 union
        //	 select count(*) a from TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) join
        //                             TBL_register_out_DETAILS rd with(nolock) on rd.Ped_Id=ped.Ped_Id
        //                             join TBL_register_out_REGISTER r with(nolock) on rd.Reg_out_Id=r.Reg_out_Id where rd.Deleted<>1 AND ped.Pe_Id=@id
        //                                 )
        //                             select sum(a) from cte";
        //            db.CreateParameters(1);
        //            db.AddParameters(0, "@id", id);
        //            int ItmVal = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
        //            if (ItmVal != 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}
        public static dynamic GetPayments(int PurchaseId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @" select r.Created_Date[date],s.Net_Amount,s.Entry_No, r.Amount [Paid_Amount],r.Fve_GroupID from  TBL_FIN_SUPPLIER_PAYMENTS r
                  inner join TBL_PURCHASE_ENTRY_REGISTER s on s.Pe_Id=r.Pe_Id
                  where r.Pe_Id=@peid";
                db.CreateParameters(1);
                db.AddParameters(0, "@peid", PurchaseId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                db.Close();
                List<object> result = new List<object>();
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new
                        {
                            VoucherDate = Convert.ToDateTime(item["date"]).ToString("dd MMM yyyy"),
                            NetAmount = Convert.ToDecimal(item["net_amount"]),
                            PaidAmount = Convert.ToDecimal(item["paid_amount"]),
                            VoucherGroupId = Convert.ToInt32(item["Fve_GroupID"]),
                            Invoice = Convert.ToString(item["Entry_No"])
                        });
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "PurchaseEntryRegister | GetPayments(int PurchaseId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        #endregion Functions
    }
}

