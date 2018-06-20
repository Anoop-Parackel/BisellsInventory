using Core.DBManager;
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
using Entities.Application;

namespace Entities.Register
{
    public class SalesEntryRegister : Register, IRegister
    {

        #region Properties
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public int ModeOfPayment { get; set; }
        public DateTime SalesDate { get; set; }
        public string SalesBillNo { get; set; }
        public string TaxType { get; set; }
        public int SalesPersonId { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string LocationAddress1 { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public string LocationAddress2 { get; set; }
        public string CompanyRegId { get; set; }
        public string CustomerState { get; set; }
        public string CustomerCountry { get; set; }
        public string LocationRegId { get; set; }
        public string CustomerTaxNo { get; set; }
        public string LocationPhone { get; set; }
        public string CustomerNo { get; set; }
        public decimal CashRecieved { get; set; }
        public decimal Balance { get; set; }
        public int DespatchId { get; set; }
        public string LPO { get; set; }
        public string DO { get; set; }
        public string Despatch { get; set; }
        public int VehicleId { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int FreightTaxId { get; set; }
        public decimal FreightAmount { get; set; }
        public int Cartons { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyEmail { get; set; }
        public int CustStateId { get; set; }
        public int Status { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int PaymentStatus { get; set; }
        public string FinancialYear { get; set; }
        public override decimal NetAmount { get; set; }
        public string Careof { get; set; }
        public List<Item> Products { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        //Additional Details
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public string Validity { get; set; }
        public string ETA { get; set; }
        #endregion Properties
        public SalesEntryRegister(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public SalesEntryRegister(int ID)
        {
            this.ID = ID;
        }
        public SalesEntryRegister()
        {

        }
        /// <summary>
        /// Used  to load request number as drop down in sales return
        /// </summary>
        /// <returns></returns>

        public static DataTable GetRequestNo(int LocationId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Location_Id", LocationId);
                    return db.ExecuteQuery(CommandType.Text, " select se_id [Sale_Entry_Id],sales_bill_no [Bill_No]from [TBL_SALES_ENTRY_REGISTER] where Location_Id=@Location_Id");

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "SalesEntry | GetRequestNo(int LocationId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// save sales entry register and sales entry details
        /// to save an entry the id must be zero
        /// </summary>
        /// <returns>return success alert when details saved successfully otherwise return error alert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesEntry, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesEntry | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make an Entry.", false, Type.Others, "Sales Entry | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make an entry.", false, Type.Others, "Sales Entry  | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select Customer to make an Entry", false, Type.Others, "Sales Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"insert into  TBL_SALES_ENTRY_REGISTER(Location_Id, Customer_Id , Mode_Of_Payment, Sales_Date, Sales_Bill_No, Tax_Amount, Gross_Amount, Net_Amount, Tax_Type,
	                             Other_Charges, Discount, Round_Off, Narration, Sales_Person_Id, Customer_Name, Customer_Address, Customer_No, Cash_Recieved,
	                             Balance_Amount, Despatch_Id, Vehicle_Id, Freight_Tax_Id, Freight_Amount, Cartons, [Status], Created_By, Created_Date,Cost_Center_Id,Job_Id,LPO,DO,TandC,Payment_Terms
                                ,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
                                values(@Location_Id ,@Customer_Id ,@Mode_Of_Payment,@Sales_Date,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SEN'),@Tax_Amount,@Gross_Amount,@Net_Amount,@Tax_Type,@Other_Charges,@Discount,@Round_Off,@Narration,@Sales_Person_Id,@Customer_Name,@Customer_Address,@Customer_No,@Cash_Recieved, @Balance_Amount,@Despatch_Id,@Vehicle_Id,@Freight_Tax_Id,@Freight_Amount,@Cartons,@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@LPO,@DO,@TandC,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email);select @@identity";
                    db.CreateParameters(42);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Mode_Of_Payment", this.ModeOfPayment);
                    db.AddParameters(3, "@Sales_Date", this.EntryDate);
                    db.AddParameters(4, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(5, "@Gross_Amount", this.Gross);
                    db.AddParameters(6, "@Net_Amount", this.NetAmount);
                    db.AddParameters(7, "@Tax_Type", this.TaxType);
                    db.AddParameters(8, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(9, "@Discount", this.Discount);
                    db.AddParameters(10, "@Round_Off", this.RoundOff);
                    db.AddParameters(11, "@Narration", this.Narration);
                    db.AddParameters(12, "@Sales_Person_Id", this.SalesPersonId);
                    db.AddParameters(13, "@Customer_Name", this.Customer);
                    db.AddParameters(14, "@Customer_Address", this.CustomerAddress);
                    db.AddParameters(15, "@Customer_No", this.CustomerNo);
                    db.AddParameters(16, "@Cash_Recieved", this.CashRecieved);
                    db.AddParameters(17, "@Balance_Amount", this.Balance);
                    db.AddParameters(18, "@Despatch_Id", this.DespatchId);
                    db.AddParameters(19, "@Vehicle_Id", this.VehicleId);
                    db.AddParameters(20, "@Freight_Tax_Id", this.FreightTaxId);
                    db.AddParameters(21, "@Freight_Amount", this.FreightAmount);
                    db.AddParameters(22, "@Cartons", this.Cartons);
                    db.AddParameters(23, "@Status", 0);
                    db.AddParameters(24, "@Created_By", this.CreatedBy);
                    db.AddParameters(25, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(26, "@Job_Id", this.JobId);
                    db.AddParameters(27, "@LPO", this.LPO);
                    db.AddParameters(28, "@DO", this.DO);
                    db.AddParameters(29, "@TandC", this.TermsandConditon);
                    db.AddParameters(30, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(31, "@Salutation", address.Salutation);
                        db.AddParameters(32, "@Contact_Name", address.ContactName);
                        db.AddParameters(33, "@Contact_Address1", address.Address1);
                        db.AddParameters(34, "@Contact_Address2", address.Address2);
                        db.AddParameters(35, "@Contact_City", address.City);
                        db.AddParameters(36, "@State_ID", address.StateID);
                        db.AddParameters(37, "@Country_ID", address.CountryID);
                        db.AddParameters(38, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(39, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(40, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(41, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "SEN", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in this.Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        string _query = @"select Track_Inventory from TBL_ITEM_MST where Item_Id=" + item.ItemID;
                        //int a = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, _query));
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            if (prod.TrackInventory==true) //get thetrack inventory in Getpricewithscheme function
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Entry|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            if (setting.AllowPriceEditingInSalesEntry)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"insert into TBL_SALES_ENTRY_DETAILS( Se_Id, item_id, Scheme_Id, Qty, Description,
                                   Rate, Discount, Tax_Id, Tax_Amount, Gross_Amount, Net_Amount, [Status],Mrp,[instance_id],Converted_From,Converted_Id,Is_Stock_Affected)
                                   values(@Se_Id,@item_id,@Scheme_Id,@Qty,@Desc,@Rate,
                                   @Discount,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@Mrp,@instance_id,@Converted_From,@Sqd_Id,@Is_Stock_Affected)";
                            db.CleanupParameters();
                            db.CreateParameters(20);
                            db.AddParameters(0, "@Sed_Id", item.DetailsID);
                            db.AddParameters(1, "@Se_Id", identity);
                            db.AddParameters(2, "@item_id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Rate", prod.SellingPrice);
                            db.AddParameters(6, "@Discount", this.Discount);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", prod.Gross);
                            db.AddParameters(10, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Sqd_Id", item.QuoteDetailId);
                            db.AddParameters(13, "@Mrp", prod.MRP);
                            db.AddParameters(14, "@instance_id", item.InstanceId);
                            db.AddParameters(15, "@Desc", item.Description);
                            //Used to Add the Previous table code if it is covertion Otherwise null will be passed
                            db.AddParameters(16, "@Converted_From", item.ConvertedFrom);
                            db.AddParameters(17, "@Is_Stock_Affected", item.AffectedinStock);
                            db.BeginTransaction();
                            db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                            
                            //query = @"update TBL_SALES_QUOTE_DETAILS set Status=1 where Sqd_Id=@sqd_id;
                            //        declare @registerId int,@total int,@totalQouted int; 
                            //        select @registerId= sq_id from TBL_SALES_QUOTE_DETAILS where Sqd_Id=@sqd_id;
                            //        select @total= count(*) from TBL_SALES_QUOTE_DETAILS where Sq_Id=@registerId;
                            //        select @totalQouted= count(*) from TBL_SALES_QUOTE_DETAILS where Sq_Id=@registerId and Status=1;
                            //        if(@total=@totalQouted) 
                            //        begin update TBL_SALES_QUOTE_REGISTER set Status=1 where Sq_Id=@registerId end";
                            //db.CreateParameters(1);
                            //db.AddParameters(0, "@sqd_id", item.QuoteDetailId);

                            //db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }
                    }

                    decimal _NetAmount = 0;

                    //Adding freight amount with net
                    this.NetAmount += this.FreightAmount;
                    dynamic Settings = Application.Settings.GetFeaturedSettings();

                    if (Settings.IsDiscountEnabled)
                    {

                        this.NetAmount = this.NetAmount - this.Discount;
                    }
                    else
                    {
                        this.Discount = 0;
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_ENTRY_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount+ " ,[Round_Off]=" + this.RoundOff + " where Se_Id=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_SALES_ENTRY_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Sales_Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SEN')[New_Order],Se_Id from TBL_SALES_ENTRY_REGISTER where Se_Id=" + identity);
                return new OutputMessage("Sales entry registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Entry | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Se_Id"] });

            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. sales entry could not be saved", false, Type.Others, "Sales Entry | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of sales entry register and sales entry details
        /// for update an entry the id of that particular entry must be grater than zero
        /// </summary>
        /// <returns>return success alert when the details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEntry, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make an Entry.", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update an entry.", false, Type.Others, "Sales Entry  | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in other transaction", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a Customer to update an entry", false, Type.Others, "Sales | Entry", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    #region
                    string query = @"delete from tbl_sales_entry_details where Se_Id=@Id;
                                  update TBL_SALES_ENTRY_REGISTER set Location_Id=@Location_Id,Customer_Id=@Customer_Id,Mode_Of_Payment=@Mode_Of_Payment,Sales_Date=@Sales_Date,Tax_Amount=@Tax_Amount,Gross_Amount=@Gross_Amount,
                                  Net_Amount=@Net_Amount,Tax_Type=@Tax_Type,Other_Charges=@Other_Charges,Discount=@Discount,Round_Off=@Round_Off,Narration=@Narration,Sales_Person_Id=@Sales_Person_Id,Customer_Name=@Customer_Name,Customer_Address=@Customer_Address,
                                  Customer_No=@Customer_No,Cash_Recieved=@Cash_Recieved,Balance_Amount=@Balance_Amount,Despatch_Id=@Despatch_Id,Vehicle_Id=@Vehicle_Id,Freight_Tax_Id=@Freight_Tax_Id,Freight_Amount=@Freight_Amount,Cartons=@Cartons
                                  ,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,LPO=@LPO,DO=@DO
                                   ,TandC=@TandC,Payment_Terms=@Payment_Terms,
                                    Contact_Name=@Contact_Name,contact_address1=@contact_address1,Contact_Address2=@Contact_Address2,
                                    Salutation=@Salutation,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                    Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email
								   where Se_Id=@Id";
                    #endregion
                    db.CreateParameters(42);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Mode_Of_Payment", this.ModeOfPayment);
                    db.AddParameters(3, "@Sales_Date", this.EntryDate);
                    db.AddParameters(4, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(5, "@Gross_Amount", this.Gross);
                    db.AddParameters(6, "@Net_Amount", this.NetAmount);
                    db.AddParameters(7, "@Tax_Type", this.TaxType);
                    db.AddParameters(8, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(9, "@Discount", this.Discount);
                    db.AddParameters(10, "@Round_Off", this.RoundOff);
                    db.AddParameters(11, "@Narration", this.Narration);
                    db.AddParameters(12, "@Sales_Person_Id", this.SalesPersonId);
                    db.AddParameters(13, "@Customer_Name", this.Customer);
                    db.AddParameters(14, "@Customer_Address", this.CustomerAddress);
                    db.AddParameters(15, "@Customer_No", this.CustomerNo);
                    db.AddParameters(16, "@Cash_Recieved", this.CashRecieved);
                    db.AddParameters(17, "@Balance_Amount", this.Balance);
                    db.AddParameters(18, "@Despatch_Id", this.DespatchId);
                    db.AddParameters(19, "@Vehicle_Id", this.VehicleId);
                    db.AddParameters(20, "@Freight_Tax_Id", this.FreightTaxId);
                    db.AddParameters(21, "@Freight_Amount", this.FreightAmount);
                    db.AddParameters(22, "@Cartons", this.Cartons);
                    db.AddParameters(23, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(24, "@Id", this.ID);
                    db.AddParameters(25, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(26, "@Job_Id", this.JobId);
                    db.AddParameters(27, "@LPO", this.LPO);
                    db.AddParameters(28, "@DO", this.DO);
                    db.AddParameters(29, "@TandC", this.TermsandConditon);
                    db.AddParameters(30, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(31, "@Salutation", address.Salutation);
                        db.AddParameters(32, "@Contact_Name", address.ContactName);
                        db.AddParameters(33, "@Contact_Address1", address.Address1);
                        db.AddParameters(34, "@Contact_Address2", address.Address2);
                        db.AddParameters(35, "@Contact_City", address.City);
                        db.AddParameters(36, "@State_ID", address.StateID);
                        db.AddParameters(37, "@Country_ID", address.CountryID);
                        db.AddParameters(38, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(39, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(40, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(41, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                    dynamic setting = Application.Settings.GetFeaturedSettings();

                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            if (prod.TrackInventory == true) //get thetrack inventory in Getpricewithscheme function
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Entry|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            if (setting.AllowPriceEditingInSalesEntry)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            //if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                            //{
                            //    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);
                            //}
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"insert into TBL_SALES_ENTRY_DETAILS( Se_Id, Sqd_Id, item_id, Scheme_Id, Qty, Description,
                                   Rate, Discount, Tax_Id, Tax_Amount, Gross_Amount, Net_Amount, [Status],Mrp,[instance_id])
                                   values(@Se_Id,@Sqd_Id,@item_id,@Scheme_Id,@Qty,@Desc,@Rate,
                                   @Discount,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@Mrp,@instance_id)";
                            db.CleanupParameters();
                            db.CreateParameters(16);
                            db.AddParameters(0, "@Sed_Id", item.DetailsID);
                            db.AddParameters(1, "@Se_Id", this.ID);
                            db.AddParameters(2, "@item_id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Rate", prod.SellingPrice);
                            db.AddParameters(6, "@Discount", this.Discount);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", prod.Gross);
                            db.AddParameters(10, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(11, "@Status", this.Status);
                            db.AddParameters(12, "@Sqd_Id", item.QuoteDetailId);
                            db.AddParameters(13, "@Mrp", prod.MRP);
                            db.AddParameters(14, "@instance_id", prod.InstanceId);
                            db.AddParameters(15, "@Desc", item.Description);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                            query = @"update TBL_SALES_QUOTE_DETAILS set Status=1 where Sqd_Id=@sqd_id;
                                    declare @registerId int,@total int,@totalQouted int; 
                                    select @registerId= sq_id from TBL_SALES_QUOTE_DETAILS where Sqd_Id=@sqd_id;
                                    select @total= count(*) from TBL_SALES_QUOTE_DETAILS where Sq_Id=@registerId;
                                    select @totalQouted= count(*) from TBL_SALES_QUOTE_DETAILS where Sq_Id=@registerId and Status=1;
                                    if(@total=@totalQouted) 
                                    begin update TBL_SALES_QUOTE_REGISTER set Status=1 where Sq_Id=@registerId end";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@sqd_id", item.QuoteDetailId);

                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }
                    }

                    decimal _NetAmount = 0;
                    //Adding freight amount with net
                    this.NetAmount += this.FreightAmount;

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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_ENTRY_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount  + " ,[Round_Off]=" + this.RoundOff + " where Se_Id=" + this.ID);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_SALES_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Sales_Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SEN')[New_Order],Se_Id from TBL_SALES_ENTRY_REGISTER where Se_Id=" + ID);
                return new OutputMessage("Sales entry updated successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Entry | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Se_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Sales entry could not be updated", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }
        }

        public OutputMessage UpdateDespatch()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEntry, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations

                if (this.DespatchId == 0)
                {
                    return new OutputMessage("Select despatch to update an entry", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);
                }

                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"update TBL_SALES_ENTRY_REGISTER set Despatch_Id=@Despatch_Id
								   where Se_Id=@Id";

                    db.CreateParameters(3);
                    db.AddParameters(0, "@Despatch_Id", this.DespatchId);
                    db.AddParameters(2, "@Id", this.ID);
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query);

                }


                return new OutputMessage("Despatch updated successfully", true, Type.NoError, "Sales Entry | Update", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new OutputMessage("Something went wrong. Despatch could not be updated", false, Type.Others, "Sales Entry | Update", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>                              
        /// delete details of sales entry register and sales entry details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEntry, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesEntry | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "SalesEntry | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (HasReference(this.ID))
            {

                return new OutputMessage("Cannot Delete. This request is alredy in a transaction", false, Type.Others, "Sales Entry | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string count = "select Count(Se_Id) from TBL_FIN_CUSTOMER_RECEIPTS where Se_Id=" + this.ID;
                    db.Open();
                    int a = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, count));
                    if (a==0)
                    {
                        string query = "delete from  TBL_SALES_ENTRY_DETAILS where Se_Id=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        db.BeginTransaction();
                        db.ExecuteNonQuery(CommandType.Text, query);
                        query = "delete from TBL_SALES_ENTRY_REGISTER where Se_Id=@id";
                        db.ExecuteNonQuery(CommandType.Text, query);
                        db.CommitTransaction();
                        Entities.Application.Helper.PostFinancials("TBL_SALES_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                        return new OutputMessage("Sales entry deleted successfully", true, Type.NoError, "SalesEntry | Delete", System.Net.HttpStatusCode.OK);
                    }
                    else
                    {
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.Others, "Sales Entry | Delete", System.Net.HttpStatusCode.InternalServerError);
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
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Entry | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Entry | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Sales entry could not be deleted", false, Type.Others, "Sales Entry | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }

                }

            }
        }
        /// <summary>
        /// Gets all the Sales Entry Register with list of Products
        /// </summary>
        /// <param name="LocationID">Location Id from where the entry generated</param>
        /// <returns></returns>
        public static List<SalesEntryRegister> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select se.Se_Id[SalesEntryId],se.Location_Id,se.Customer_Id,se.Mode_Of_Payment,se.Sales_Person_Id,se.Sales_Date,isnull(se.sales_bill_no,0) Invoice_No,
                            se.Tax_Amount,se.Gross_Amount,se.Net_Amount,se.Sales_Person_Id,se.Other_Charges,se.Round_Off,se.Despatch_Id,se.Cartons,se.Narration,c.Address1[Customer_Address],c.Name[Customer_Name],se.Customer_No
                            ,se.Balance_Amount,se.Vehicle_Id,se.Freight_Amount,se.Freight_Tax_Id,se.[Status],sed.Sed_Id,it.Item_Code,it.Item_Id,it.Name[Item],sed.item_id,sed.Instance_Id,sed.Sqd_Id,
                            sed.Qty,sed.Rate[Selling_Price],isnull(sed.Scheme_Id,0)[Scheme_Id],sed.Mrp,sed.Gross_Amount[P_Gross_Amount],sed.Tax_Amount[P_Tax_Amount],sed.Net_Amount[P_Net_Amount],
                            l.name[Location],cm.Name[Company],c.Address2[Cus_Address2],c.Phone1[Cus_Phone],l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],tx.Percentage[Tax_Percentage],d.Name[Despatch],
							isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone],sed.Scheme_Id 
							,l.Reg_Id1[Location_RegistrationId],cm.Reg_Id1[Company_RegistrationId],c.Taxno1[Customer_TaxNo],
							cm.Logo[Company_Logo],cm.Email[Company_Email]
							 from TBL_SALES_ENTRY_REGISTER se with(nolock)
                            left join tbl_sales_entry_details sed with(nolock) on sed.Se_Id=se.Se_Id
                            left join TBL_LOCATION_MST l with(nolock) on l.location_Id=se.Location_Id
                            left join tbl_company_mst cm with(nolock) on cm.company_Id=l.company_Id
                            left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=sed.Tax_Id
							left join TBL_ITEM_MST it with(nolock) on it.item_id=sed.Item_Id
                            left join TBL_CUSTOMER_MST c with(nolock) on c.Customer_Id=se.Customer_Id
							left join tbl_despatch_mst d with(nolock) on d.Despatch_Id=se.Despatch_Id
                            where se.Location_Id=@Location_Id order by se.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEntryRegister> result = new List<SalesEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEntryRegister register = new SalesEntryRegister();
                        register.ID = row["SalesEntryId"] != DBNull.Value ? Convert.ToInt32(row["SalesEntryId"]) : 0;
                        register.EntryDateString = row["Sales_Date"] != DBNull.Value ? Convert.ToDateTime(row["Sales_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.DespatchId = row["Despatch_Id"] != DBNull.Value ? Convert.ToInt32(row["Despatch_Id"]) : 0;
                        register.SalesPersonId = row["Sales_Person_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Person_Id"]) : 0;
                        register.ModeOfPayment = row["Mode_Of_Payment"] != DBNull.Value ? Convert.ToInt32(row["Mode_Of_Payment"]) : 0;
                        register.SalesDate = Convert.ToDateTime(row["Sales_Date"]);
                        register.Cartons = row["Cartons"] != DBNull.Value ? Convert.ToInt32(row["Cartons"]) : 0;
                        register.SalesBillNo = Convert.ToString(row["Invoice_No"]);
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Balance = row["Balance_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Balance_Amount"]) : 0;
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegistrationId"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CustomerTaxNo = Convert.ToString(row["Customer_TaxNo"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.VehicleId = row["Vehicle_Id"] != DBNull.Value ? Convert.ToInt32(row["Vehicle_Id"]) : 0;
                        register.Customer = Convert.ToString(row["Customer_Name"]);
                        register.CustomerAddress = Convert.ToString(row["Customer_Address"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.CustomerPhone = Convert.ToString(row["Cus_Phone"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Despatch = Convert.ToString(row["Despatch"]);
                        register.FreightAmount = row["Freight_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Freight_Amount"]) : 0;
                        register.FreightTaxId = row["Freight_Tax_Id"] != DBNull.Value ? Convert.ToInt32(row["Freight_Tax_Id"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("SalesEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
                            item.QuoteDetailId = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEntry | GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        public static List<SalesEntryRegister> GetDetails(int LocationID,int? CustomerId,DateTime? From,DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select se.Se_Id[SalesEntryId],se.Location_Id,se.Customer_Id,se.Mode_Of_Payment,se.Sales_Person_Id,se.Sales_Date,isnull(se.sales_bill_no,0) Invoice_No,se.LPO,se.DO,
                            se.Tax_Amount,se.Gross_Amount,se.Net_Amount,se.Sales_Person_Id,se.Other_Charges,se.Round_Off,se.Despatch_Id,se.Cartons,se.Narration,se.Customer_Address,se.Customer_Name,se.Customer_No
                            ,se.Balance_Amount,se.Vehicle_Id,se.Freight_Amount,se.Freight_Tax_Id,se.[Status],sed.Sed_Id,it.Item_Code,it.Item_Id,it.Name[Item],sed.item_id,sed.Instance_Id,sed.Sqd_Id,
                            sed.Qty,sed.Rate[Selling_Price],isnull(sed.Scheme_Id,0)[Scheme_Id],sed.Mrp,sed.Gross_Amount[P_Gross_Amount],sed.Tax_Amount[P_Tax_Amount],sed.Net_Amount[P_Net_Amount],
                            l.name[Location],cm.Name[Company],c.Address2[Cus_Address2],c.Phone1[Cus_Phone],l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],tx.Percentage[Tax_Percentage],d.Name[Despatch],
							isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone],sed.Scheme_Id,sed.Description
                            ,isnull(se.Discount,0)[Discount],isnull(se.Cost_center_Id,0)[Cost_center_Id],isnull(se.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job] 
							,c.Email[Cust_Email],se.TandC,se.Payment_Terms,c.Name[Cust] from TBL_SALES_ENTRY_REGISTER se with(nolock)
                            left join tbl_sales_entry_details sed with(nolock) on sed.Se_Id=se.Se_Id
                            left join TBL_LOCATION_MST l with(nolock) on l.location_Id=se.Location_Id
                            left join tbl_company_mst cm with(nolock) on cm.company_Id=l.company_Id
                            left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=sed.Tax_Id
							left join TBL_ITEM_MST it with(nolock) on it.item_id=sed.Item_Id
                            left join TBL_CUSTOMER_MST c with(nolock) on c.Customer_Id=se.Customer_Id
							left join tbl_despatch_mst d with(nolock) on d.Despatch_Id=se.Despatch_Id
							left join tbl_Fin_CostCenter cost on cost.Fcc_ID=se.Cost_center_Id
							left join TBL_JOB_MST j on j.Job_Id=se.Job_Id
                            where se.Location_Id=@Location_Id {#customerfilter#} {#daterangefilter#} order by se.Created_Date desc";
                #endregion query
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and se.Sales_Date>=@fromdate and se.Sales_Date<=@todate ");
                }
                else
                {
                    To = DateTime.UtcNow;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and se.Sales_Date>=@fromdate and se.Sales_Date<=@todate ");
                }
                if (CustomerId != null && CustomerId > 0)
                {
                    query = query.Replace("{#customerfilter#}", " and se.customer_id=@CustomerId ");
                }
                else
                {

                    query = query.Replace("{#customerfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.AddParameters(3, "@CustomerId", CustomerId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEntryRegister> result = new List<SalesEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEntryRegister register = new SalesEntryRegister();
                        register.ID = row["SalesEntryId"] != DBNull.Value ? Convert.ToInt32(row["SalesEntryId"]) : 0;
                        register.CostCenterId = row["Cost_center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.EntryDateString = row["Sales_Date"] != DBNull.Value ? Convert.ToDateTime(row["Sales_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.DespatchId = row["Despatch_Id"] != DBNull.Value ? Convert.ToInt32(row["Despatch_Id"]) : 0;
                        register.SalesPersonId = row["Sales_Person_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Person_Id"]) : 0;
                        register.ModeOfPayment = row["Mode_Of_Payment"] != DBNull.Value ? Convert.ToInt32(row["Mode_Of_Payment"]) : 0;
                        register.SalesDate = Convert.ToDateTime(row["Sales_Date"]);
                        register.Cartons = row["Cartons"] != DBNull.Value ? Convert.ToInt32(row["Cartons"]) : 0;
                        register.SalesBillNo = Convert.ToString(row["Invoice_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.CustomerEmail = Convert.ToString(row["Cust_Email"]);
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Balance = row["Balance_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Balance_Amount"]) : 0;
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.VehicleId = row["Vehicle_Id"] != DBNull.Value ? Convert.ToInt32(row["Vehicle_Id"]) : 0;
                        register.Customer = Convert.ToString(row["Cust"]);
                        register.CustomerAddress = Convert.ToString(row["Customer_Address"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.CustomerPhone = Convert.ToString(row["Customer_No"]);
                        register.LPO = Convert.ToString(row["LPO"]);
                        register.DO = Convert.ToString(row["DO"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Despatch = Convert.ToString(row["Despatch"]);
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.FreightAmount = row["Freight_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Freight_Amount"]) : 0;
                        register.FreightTaxId = row["Freight_Tax_Id"] != DBNull.Value ? Convert.ToInt32(row["Freight_Tax_Id"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("SalesEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
                            item.QuoteDetailId = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.Description= Convert.ToString(rowItem["Description"]);
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEntry | GetDetails(int LocationID,int? CustomerId,DateTime? From,DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// Used in delivery note for return details of entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="LocationID"></param>
        /// <returns></returns>
        public static SalesEntryRegister GetDetails(int id, int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select se.Se_Id[SalesEntryId],se.Location_Id,se.Customer_Id,se.Mode_Of_Payment,se.Sales_Person_Id,se.Sales_Date,isnull(se.sales_bill_no,0) Invoice_No,se.LPO,se.DO,se.Discount,ut.name [unit],
                               se.Tax_Amount,se.Gross_Amount,se.Net_Amount,se.Sales_Person_Id,se.Other_Charges,se.Round_Off,se.Despatch_Id,se.Cartons,se.Narration,c.Address1[Customer_Address],c.Name[Customer_Name],se.Customer_No
                               ,se.Balance_Amount,se.Vehicle_Id,se.Freight_Amount,se.Freight_Tax_Id,se.[Status],sed.Sed_Id,it.Item_Code,it.Item_Id,it.Name[Item],sed.Sqd_Id,
                               sed.Qty,isnull(sed.Scheme_Id,0)[Scheme_Id],sed.Rate[Selling_Price],sed.instance_id,sed.Mrp,sed.Gross_Amount[P_Gross_Amount],sed.Tax_Amount[P_Tax_Amount],sed.Net_Amount[P_Net_Amount],sed.Description,
                               l.name[Location],cm.Name[Company],c.Address2[Cus_Address2],c.Phone1[Cus_Phone],l.Address1[Loc_Address1],l.Address2[Loc_Address2],
                               l.Contact[Loc_Phone],tx.Percentage[Tax_Percentage],d.Name[Despatch],c.Email[Customer_Email] 
							   ,l.Reg_Id1[Loc_RegId],cm.Email[Comp_Email],c.Taxno1[Cust_TaxNo],se.State_Id[Cust_StateId],se.Country_Id,
							   s.Name[Cust_State],coun.Name[Cust_Country],isnull(se.Cost_center_Id,0)[Cost_center_Id],isnull(se.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job] 
							   ,sales.BalAmount,sales.AvaAmount,sales.Status[Payment_Status],j.Job_Name,se.TandC,se.Payment_Terms,se.Customer_Name Cust,se.Customer_Address[Cust_Address_From_Entry]
							   ,se.Contact_Name,se.Contact_Address1,se.Contact_Address2,se.Contact_City,se.Contact_Email,se.Contact_Phone1,se.Contact_Phone2,se.Contact_Zipcode,se.Salutation,emp.First_Name Sales_person
                               from TBL_SALES_ENTRY_REGISTER se with(nolock)
                               left join tbl_sales_entry_details sed with(nolock) on sed.Se_Id=se.Se_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.location_Id=se.Location_Id
                               left join tbl_company_mst cm with(nolock) on cm.company_Id=l.company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=sed.Tax_Id							
                               left join TBL_ITEM_MST it with(nolock) on it.item_id=sed.Item_Id
                               left join TBL_CUSTOMER_MST  c with(nolock) on c.Customer_Id=se.Customer_Id
							   left join TBL_STATE_MST s with(nolock) on s.State_Id=se.State_Id
							   left join TBL_COUNTRY_MST coun with(nolock) on coun.Country_Id=se.Country_Id
                               left join tbl_despatch_mst d with(nolock) on d.Despatch_Id=se.Despatch_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=se.Cost_center_Id
							   left join TBL_JOB_MST j on j.Job_Id=se.Job_Id
							   left join [VW_CUSTOMER_RECEIPTS] sales on sales.Se_Id=se.Se_Id
                               left join TBL_UNIT_MST ut on ut.Unit_id=it.Unit_id
                               left join TBL_EMPLOYEE_MST emp on emp.Employee_Id=se.Sales_Person_Id
                               where se.Location_Id=@Location_Id AND se.se_id=@id order by se.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@id", id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        DataRow row = dt.Rows[0];
                        SalesEntryRegister register = new SalesEntryRegister();
                        register.ID = row["SalesEntryId"] != DBNull.Value ? Convert.ToInt32(row["SalesEntryId"]) : 0;
                        register.CostCenterId = row["Cost_center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.EntryDateString = row["Sales_Date"] != DBNull.Value ? Convert.ToDateTime(row["Sales_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.DespatchId = row["Despatch_Id"] != DBNull.Value ? Convert.ToInt32(row["Despatch_Id"]) : 0;
                        register.SalesPersonId = row["Sales_Person_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Person_Id"]) : 0;
                        register.ModeOfPayment = row["Mode_Of_Payment"] != DBNull.Value ? Convert.ToInt32(row["Mode_Of_Payment"]) : 0;
                        register.SalesDate = Convert.ToDateTime(row["Sales_Date"]);
                        register.Cartons = row["Cartons"] != DBNull.Value ? Convert.ToInt32(row["Cartons"]) : 0;
                        register.SalesBillNo = Convert.ToString(row["Invoice_No"]);
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.VehicleId = row["Vehicle_Id"] != DBNull.Value ? Convert.ToInt32(row["Vehicle_Id"]) : 0;
                        register.Customer = Convert.ToString(row["Customer_Name"]);
                        register.CustStateId = row["Cust_StateId"]!=DBNull.Value? Convert.ToInt32(row["Cust_StateId"]):0;
                        register.LPO = Convert.ToString(row["LPO"]);
                        register.DO = Convert.ToString(row["DO"]);
                        register.CustomerTaxNo = Convert.ToString(row["Cust_TaxNo"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.CustomerState = Convert.ToString(row["Cust_State"]);
                        register.CustomerCountry = Convert.ToString(row["Cust_Country"]);
                        register.CustomerEmail = Convert.ToString(row["Customer_Email"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.FreightAmount = row["Freight_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Freight_Amount"]):0;
                        register.Discount = row["Discount"]!=DBNull.Value? Convert.ToDecimal(row["Discount"]):0;
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.CustomerAddress = Convert.ToString(row["Cust_Address_From_Entry"]);
                        register.CustomerNo = Convert.ToString(row["Customer_No"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.CustomerPhone = Convert.ToString(row["Cus_Phone"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.JobName = row["Job_Name"] != DBNull.Value ? Convert.ToString(row["Job_Name"]) : "";
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.Despatch = Convert.ToString(row["Despatch"]);
                        register.BalanceAmount = row["BalAmount"]!=DBNull.Value? Convert.ToDecimal(row["BalAmount"]):0;
                        register.PaymentStatus = row["Payment_Status"]!=DBNull.Value? Convert.ToInt32(row["Payment_Status"]):0;
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Payment_Terms= Convert.ToString(row["Payment_Terms"]);
                        register.Careof= Convert.ToString(row["Cust"]);
                        register.Employee = Convert.ToString(row["Sales_person"]);
                        List<Entities.Master.Address> addresslist = new List<Master.Address>();
                        Entities.Master.Address address = new Master.Address();
                        address.ContactName= Convert.ToString(row["Contact_Name"]);
                        address.Address1= Convert.ToString(row["Contact_Address1"]);
                        address.Address2= Convert.ToString(row["Contact_Address2"]);
                        address.City= Convert.ToString(row["Contact_City"]);
                        address.Phone1= Convert.ToString(row["Contact_Phone1"]);
                        address.Phone2= Convert.ToString(row["Contact_Phone2"]);
                        address.Zipcode= Convert.ToString(row["Contact_Zipcode"]);
                        address.Salutation= Convert.ToString(row["Salutation"]);
                        address.Email= Convert.ToString(row["Contact_Email"]);
                        address.State= Convert.ToString(row["Cust_State"]);
                        address.Email= Convert.ToString(row["Contact_Email"]);
                        address.Country= Convert.ToString(row["Cust_Country"]);
                        address.CountryID= row["Country_Id"] != DBNull.Value ? Convert.ToInt32(row["Country_Id"]) : 0;
                        address.StateID= row["Cust_StateId"] != DBNull.Value ? Convert.ToInt32(row["Cust_StateId"]) : 0;
                        addresslist.Add(address);
                        register.BillingAddress = addresslist;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("SalesEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count;j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.QuoteDetailId = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.Unit = Convert.ToString(rowItem["unit"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.SchemeId = rowItem["scheme_id"] != DBNull.Value ? Convert.ToInt32(rowItem["scheme_id"]) : 0;

                            products.Add(item);
                            dt.Rows.RemoveAt(0);
                        }
                        register.Products = products;

                        return register;

                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "SalesEntry | GetDetails(int id, int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }

        /// <summary>
        /// Checking for the reference sales Entry with other tables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool HasReference(int id)
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    DBManager db = new DBManager();
                    db.Open();
                    string query = @"select count(*) from TBL_SALES_RETURN_DETAILS srd with(nolock) join
                                     TBL_SALES_entry_DETAILS d with(nolock) on d.Sed_Id=srd.Sed_Id
                                     join  TBL_sales_entry_REGISTER ser with(nolock) on d.Se_Id=ser.Se_Id where
                                     ser.Se_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", id);
                    int ItmVal = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    if (ItmVal != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "SalesEntry | HasReference(int id)");
                    throw;
                }
            }




        }
        //no use
        //public static List<Item> GetItemsFromPurchaseWithScheme(int CustomerId,int LocationId)
        //{
        //    DBManager db = new DBManager();
        //    db.Open();
        //    DataTable dt = db.ExecuteDataSet(System.Data.CommandType.Text, @"[dbo].[USP_SEARCH_PRODUCTS_FROMSCHEME] '',"+CustomerId+","+LocationId).Tables[0];
        //    List<Item> result = new List<Item>();
        //    foreach (DataRow item in dt.Rows)
        //    {
        //        Item prod = new Item();
        //        prod.ItemID = item["ItemId"] != DBNull.Value ? Convert.ToInt32(item["ItemId"]) : 0;
        //        prod.PedId = item["Ped_Id"] != DBNull.Value ? Convert.ToInt32(item["Ped_Id"]) : 0;
        //        prod.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
        //        prod.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["Taxpercentage"]) : 0;
        //        prod.SellingPrice = item["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(item["Selling_Price"]) : 0;
        //        prod.MRP = item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
        //        prod.Stock = item["Stock"] != DBNull.Value ? Convert.ToDecimal(item["Stock"]) : 0;
        //        prod.SchemeId = item["scheme_id"] != DBNull.Value ? Convert.ToInt32(item["scheme_id"]) : 0;
        //        result.Add(prod);
        //    }
        //    return result;
        //}

        /// <summary>
        /// Used to get customer wise sales entry register for sales return
        /// </summary>
        /// <param name="LocationId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public static List<SalesEntryRegister> GetDetailsItemWise(int LocationId, int CustomerId, int ItemId, int InstanceId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @";with cte as (select DENSE_RANK() over (partition by ser.se_id  order by sed.sed_id )[Rank] ,ser.Se_Id, ser.Sales_Bill_No,ser.Sales_Date,ser.Tax_Amount,ser.Gross_Amount,ser.Net_Amount,sed.Item_Id,sed.Sed_Id,sed.Instance_Id
                               ,ser.Location_Id,(select count(*) from TBL_SALES_ENTRY_DETAILS se with(nolock) where se_id=ser.Se_Id  ) Total_items from [TBL_SALES_ENTRY_REGISTER] ser with(nolock) 
                                 left join TBL_SALES_ENTRY_DETAILS sed with(nolock) on sed.Se_Id=ser.Se_Id
                                 left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sed.Item_Id  where ser.Customer_Id=@customerId and itm.item_id=@itemId and sed.instance_id=@instance_id
                                 and ser.Location_Id=@Location_Id 
                               )
                               select * from cte where [Rank]=1";
                #endregion query
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@customerId", CustomerId);
                db.AddParameters(2, "@itemId", ItemId);
                db.AddParameters(3, "@instance_id", InstanceId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEntryRegister> result = new List<SalesEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEntryRegister register = new SalesEntryRegister();
                        register.ID = row["Se_Id"] != DBNull.Value ? Convert.ToInt32(row["Se_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SalesBillNo = Convert.ToString(row["Sales_Bill_No"]);
                        register.EntryDateString = row["Sales_Date"] != DBNull.Value ? Convert.ToDateTime(row["Sales_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.TotalItems = row["Total_items"] != DBNull.Value ? Convert.ToInt32(row["Total_items"]) : 0;
                        result.Add(register);
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "SalesEntry | GetDetailsItemWise(int LocationId, int CustomerId, int ItemId, int InstanceId)");
                return null;
            }
        }

        /// <summary>
        /// used for to get Sales entry details for Sales return
        /// </summary>
        /// <param name="LocationId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<SalesEntryRegister> GetDetailsFromEntry(int LocationId, int Id)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select ser.Se_Id,sed.sed_id,sed.Instance_Id,sed.Item_Id,itm.Name [item],itm.Item_Code,sed.Rate,sed.mrp,sed.Qty,sed.Tax_Amount,sed.Gross_Amount,sed.Net_Amount,tax.Percentage [Tax_Percentage] from TBL_SALES_ENTRY_details sed with (nolock)
					left join TBL_SALES_ENTRY_REGISTER ser with (nolock) on ser.se_id=sed.Se_Id
					left join TBL_TAX_MST tax with (nolock) on tax.Tax_Id=sed.Tax_Id		
					left join TBL_ITEM_MST itm with (nolock) on itm.Item_Id=sed.Item_Id
					where ser.Location_Id=@location and sed.Se_Id=@SeId   order by ser.Created_Date DESC";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@location", LocationId);
                db.AddParameters(1, "@SeId", Id);

                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEntryRegister> result = new List<SalesEntryRegister>();

                    DataRow row = dt.Rows[0];
                    SalesEntryRegister register = new SalesEntryRegister();
                    register.ID = row["Se_Id"] != DBNull.Value ? Convert.ToInt32(row["Se_Id"]) : 0;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Se_Id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.SedId = rowItem["sed_id"] != DBNull.Value ? Convert.ToInt32(rowItem["sed_id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.Name = rowItem["Item"].ToString();
                        item.MRP = rowItem["mrp"] != DBNull.Value ? Convert.ToInt32(rowItem["mrp"]) : 0;
                        item.SellingPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEntry | GetDetailsFromEntry(int LocationId, int Id)");
                return null;
            }
        }


        public static Item GetPriceFromSalesEntry(int sed_id)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@sed_id", sed_id);

                #region query
                string query = @"select ser.customer_id,sed.Sed_Id,sed.Item_Id,sed.Qty,sed.Instance_Id,sed.Tax_Id,sed.Mrp,sed.Rate ,stk.Stock,tax.Percentage [taxPercentage] from tbl_sales_entry_details sed with(nolock)
                                   join VW_STOCK stk on stk.item_id=sed.Item_id and stk.Instance_Id=sed.Instance_Id
                                   left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=sed.Tax_Id
                                   left join TBL_SALES_ENTRY_REGISTER ser with(nolock) on ser.se_id=sed.Se_Id
                                   where
                                  sed.Sed_Id=@sed_id ";
                #endregion
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);

                Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["Item_Id"] != DBNull.Value ? Convert.ToInt32(prod["Item_Id"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.Quantity = prod["qty"] != DBNull.Value ? Convert.ToInt32(prod["qty"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.TaxPercentage = prod["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(prod["Taxpercentage"]) : 0;
                item.SellingPrice = prod["Rate"] != DBNull.Value ? Convert.ToDecimal(prod["Rate"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
                item.Stock = prod["Stock"] != DBNull.Value ? Convert.ToDecimal(prod["Stock"]) : 0;

                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "SalesEntry | GetPriceFromSalesEntry(int sed_id)");
                return null;
            }
        }
    
        public  static OutputMessage SendMail(int SalesId,string toAddress,int userId,string url)
        {
           

                if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
                {
                    return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (!toAddress.IsValidEmail())
                {
                    return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Sales Entry | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                    mail.Subject = "Invoice";
                    mail.Body = "Please Find the attached copy of Invoice";
                    mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));
                    SmtpClient smtp = new SmtpClient()
                    {
                        Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                        Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                    };
                    smtp.Send(mail);
                    return new OutputMessage("Mail Send successfully", true, Type.NoError, "Sales Entry | Send Mail", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Sales Entry | Send Mail", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            
        }
        public static dynamic GetReceipts(int salesId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select r.Created_Date [date],s.Net_Amount,s.Sales_Bill_No, r.Amount [Paid_Amount],r.Fve_GroupID from   TBL_FIN_CUSTOMER_RECEIPTS r
                                 inner join TBL_SALES_ENTRY_REGISTER s on s.Se_Id=r.Se_Id
                                 where r.Se_Id=@seid";
                db.CreateParameters(1);
                db.AddParameters(0, "@seid", salesId);
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
                            Invoice = Convert.ToString(item["Sales_Bill_No"])
                        });
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Entities.Application.Helper.LogException(ex, "SalesEntryRegister | GetReceipts(int salesId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
    }
}
