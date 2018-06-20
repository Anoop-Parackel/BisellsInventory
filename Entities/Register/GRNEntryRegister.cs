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
    public class GRNEntryRegister : Register, IRegister
    {
        #region Properties
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ItemID { get; set; }
        public int SupplierId { get; set; }
        public List<Item> Products { get; set; }
        public string InvoiceDateString { get; set; }
        public string FinancialYear { get; set; }
        public int CostCenterId { get; set; }
        public int JobId { get; set; }
        public bool Status { get; set; }
        public int ApprovedBy { get; set; }
        public int Order_Detail_ID { get; set; }
        public int Recieved_Qty { get; set; }
        public decimal Order_Rate { get; set; }
        public decimal Net { get; set; }
        public int GRNID { get; set; }
        public int Order_Qty { get; set; }
        public decimal TaxPercentage { get; set; }
        public string Supplier { get; set; }
        public int Detail_ID { get; set; }
        public string Item_Name { get; set; }
        public string Item_Code { get; set; }
        public string CompanyRegId { get; private set; }
        public string LocationRegId { get; private set; }
        public string SupplierTaxNo { get; private set; }
        public string SupplierAddress1 { get; private set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public string SupplierAddress2 { get; private set; }
        public string SupplierPhone { get; private set; }
        public string SupplierEmail { get; set; }
        public string LocationAddress1 { get; private set; }
        public string LocationAddress2 { get; private set; }
        public string LocationPhone { get; private set; }
        public string SupplierCountry { get; set; }
        public string SupplierState { get; set; }
        public int SupplierCountryId { get; set; }
        public int SupplierStateId { get; set; }
        public string CompanyAddress1 { get; private set; }
        public string CompanyAddress2 { get; private set; }
        public string CompanyEmail { get; private set; }
        public string SupplierMail { get; private set; }
        public string CompanyPhone { get; private set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        #endregion

        #region Functions
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.GRN, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                if (this.InvoiceDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Invoice Date to make a Entry.", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a Entry.", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.SupplierId == 0)
                {
                    return new OutputMessage("Select a Supplier to make a Entry.", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Entry.", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {
                    db.Open();
                    //Inserts the data to the register table
                    string query = @"INSERT INTO [dbo].[TBL_GRN_REGISTER]
			                        ([Location_ID],[Supplier_ID],[Tax_Amount],[Net_Amount],[Status],[Approved_By],[Approved_Date],[Created_By],[Created_Date],[Costcenter_Id],[Job_id],EntryNumber,Discount,TandC,Payment_Terms,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
                                    VALUES
			                        (@Location_ID,@Supplier_ID,@Tax_Amount,@Net_Amount,@Status,@Approved_By,@Approved_Date,@Created_By,@Created_Date,@Costcenter_Id,@Job_id,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','GRN'),@Discount,@TandC,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity;";
                    db.CreateParameters(25);
                    db.AddParameters(0, "@Location_ID", this.LocationId);
                    db.AddParameters(1, "@Supplier_ID", this.SupplierId);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Net_Amount", this.NetAmount);
                    db.AddParameters(4, "@Status", this.Status);
                    db.AddParameters(5, "@Approved_By", this.ApprovedBy);
                    db.AddParameters(6, "@Approved_Date", DateTime.UtcNow);
                    db.AddParameters(7, "@Created_By", this.CreatedBy);
                    db.AddParameters(8, "@Created_Date", DateTime.UtcNow);
                    db.AddParameters(9, "@Costcenter_Id", this.CostCenterId);
                    db.AddParameters(10, "@Job_Id", this.JobId);
                    db.AddParameters(11, "@Discount", this.Discount);
                    db.AddParameters(12, "@TandC", this.TermsandConditon);
                    db.AddParameters(13, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(14, "@Salutation", address.Salutation);
                        db.AddParameters(15, "@Contact_Name", address.ContactName);
                        db.AddParameters(16, "@Contact_Address1", address.Address1);
                        db.AddParameters(17, "@Contact_Address2", address.Address2);
                        db.AddParameters(18, "@Contact_City", address.City);
                        db.AddParameters(19, "@State_ID", address.StateID);
                        db.AddParameters(20, "@Country_ID", address.CountryID);
                        db.AddParameters(21, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(22, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(23, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(24, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));//Gets the Latest ID
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "GRN", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.ModifiedQuantity < 0)//if the recieved qty is less than zero
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100) * item.ModifiedQuantity;
                            item.Gross = item.ModifiedQuantity * item.MRP;
                            item.NetAmount = item.Gross + item.TaxAmount;
                            db.CleanupParameters();
                            query = @"INSERT INTO [dbo].[TBL_GRN_DETAILS]
                                    ([GRN_ID],[Item_ID],[Order_Detail_ID],[Ordered_Quantity],[Received_Quantity],[Rate],
                                    [Net_Amount],[Status],[Created_By],[Created_Date],Tax_Amount,Tax_Percentage,Description,Converted_From,Converted_Id)
                                    VALUES
                                    (@GRN_ID,@Item_ID,@Ordered_Quantity,@Received_Quantity,@Rate,
                                    @Net_Amount,@Status,@Created_By,@Created_Date,@Tax_Amount,@Tax_Percentage,@Description,@Converted_From,@Order_Detail_ID)";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@GRN_ID", identity);//***Latest Id From the Register Table
                            db.AddParameters(1, "@Item_ID", item.ItemID);
                            db.AddParameters(2, "@Order_Detail_ID", item.RequestDetailId); //Purchase quote detail ID
                            db.AddParameters(3, "@Ordered_Quantity", item.Quantity); //Ordered Qty
                            db.AddParameters(4, "@Received_Quantity", item.ModifiedQuantity); //Recieved Qty
                            db.AddParameters(5, "@Rate", item.MRP); //Rate From the Purchase Quote
                            db.AddParameters(6, "@Net_Amount", item.NetAmount);
                            db.AddParameters(7, "@Status", item.Status);
                            db.AddParameters(8, "@Created_By", this.CreatedBy);
                            db.AddParameters(9, "@Created_Date", DateTime.UtcNow);
                            db.AddParameters(10, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(11, "@Tax_Percentage", item.TaxPercentage);
                            db.AddParameters(12, "@Description", item.Description);
                            db.AddParameters(13, "@Converted_From", item.ConvertedFrom);
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
                            db.AddParameters(0, "@pqd_id", item.RequestDetailId);
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_GRN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ",[Net_Amount]=" + _NetAmount + " where GRN_ID=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select EntryNumber[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','GRN')[New_Order],GRN_ID from TBL_GRN_REGISTER where GRN_ID=" + identity);
                return new OutputMessage("GRN registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Entry | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["GRN_ID"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. GRN entry could not be saved", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }

        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.GRN, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "GRNEntryRegister | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a entry.", false, Type.Others, "GRNEntryRegister | Update", System.Net.HttpStatusCode.InternalServerError);
                }
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
                        return new OutputMessage("This invoice number has already exists.", false, Type.Others, "GRNEntryRegister | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    string query = @"delete from TBL_GRN_DETAILS where GRN_ID=@GRN_ID;
                                   UPDATE [dbo].[TBL_GRN_REGISTER] SET [Location_ID] = @Location_ID,[Supplier_ID] = @Supplier_ID,[Tax_Amount] = @Tax_Amount,
                                   [Net_Amount] = @Net_Amount,[Status] = @Status,[Approved_By] = @Approved_By,[Approved_Date] = @Approved_Date,
                                   [Costcenter_Id] = @Costcenter_Id,[Job_id] = @Job_id,[Discount] = @Discount,[Modified_By]=@Modified_By,
                                   [Modified_Date]=@Modified_Date,TandC=@TandC,Payment_Terms=@Payment_Terms,Salutation=@Salutation,Contact_Name=@Contact_Name,
                                   Contact_Address1=@Contact_Address1,Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,
                                   State_ID=@State_ID,Country_ID=@Country_ID,Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                   Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email
                                   WHERE GRN_ID=@GRN_ID";
                    db.BeginTransaction();
                    db.CreateParameters(27);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Net_Amount", this.NetAmount);
                    db.AddParameters(4, "@Status", this.Status);
                    db.AddParameters(5, "@Approved_By", this.CreatedBy);
                    db.AddParameters(6, "@Approved_Date", DateTime.UtcNow);
                    db.AddParameters(7, "@Discount", this.Discount);
                    db.AddParameters(8, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(9, "@Modified_Date", DateTime.UtcNow);
                    db.AddParameters(10, "@Costcenter_Id", this.CostCenterId);
                    db.AddParameters(11, "@Job_Id", this.JobId);
                    db.AddParameters(12, "@GRN_ID", this.GRNID);
                    db.AddParameters(13, "@TandC", this.TermsandConditon);
                    db.AddParameters(14, "@Payment_Terms", this.Payment_Terms);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(15, "@Salutation", address.Salutation);
                        db.AddParameters(16, "@Contact_Name", address.ContactName);
                        db.AddParameters(17, "@Contact_Address1", address.Address1);
                        db.AddParameters(18, "@Contact_Address2", address.Address2);
                        db.AddParameters(19, "@Contact_City", address.City);
                        db.AddParameters(20, "@State_ID", address.StateID);
                        db.AddParameters(21, "@Country_ID", address.CountryID);
                        db.AddParameters(22, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(23, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(24, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(26, "@Contact_Email", address.Email);
                    }
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.ModifiedQuantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            item.TaxAmount = item.MRP * (item.TaxPercentage / 100) * item.ModifiedQuantity;
                            item.Gross = item.ModifiedQuantity * item.MRP;
                            item.NetAmount = item.Gross + item.TaxAmount;
                            db.CleanupParameters();
                            query = @"INSERT INTO [dbo].[TBL_GRN_DETAILS]
                                        ([GRN_ID],[Item_ID],[Order_Detail_ID],[Ordered_Quantity],[Received_Quantity],[Rate],
                                         [Net_Amount],[Status],[Created_By],[Created_Date],Tax_Amount,Tax_Percentage,Description)
                                       VALUES
                                        (@GRN_ID,@Item_ID,@Order_Detail_ID,@Ordered_Quantity,@Received_Quantity,@Rate,
                                         @Net_Amount,@Status,@Created_By,@Created_Date,@Tax_Amount,@Tax_Percentage,@Description)";
                            db.CreateParameters(13);
                            db.AddParameters(0, "@GRN_ID", this.GRNID);//***
                            db.AddParameters(1, "@Item_ID", item.ItemID);
                            db.AddParameters(2, "@Order_Detail_ID", item.RequestDetailId);
                            db.AddParameters(3, "@Ordered_Quantity", item.Quantity);
                            db.AddParameters(4, "@Received_Quantity", item.ModifiedQuantity);
                            db.AddParameters(5, "@Rate", item.MRP);
                            db.AddParameters(6, "@Net_Amount", item.NetAmount);
                            db.AddParameters(7, "@Status", item.Status);
                            db.AddParameters(8, "@Created_By", this.CreatedBy);
                            db.AddParameters(9, "@Created_Date", DateTime.UtcNow);
                            db.AddParameters(10, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(11, "@Tax_Percentage", item.TaxPercentage);
                            db.AddParameters(12, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;
                            query = @"update TBL_PURCHASE_QUOTE_DETAILS set Status=1 where Pqd_Id=@pqd_id;
                                    declare @registerId int,@total int,@totalQouted int; 
                                    select @registerId= pq_id from TBL_PURCHASE_QUOTE_DETAILS where Pqd_Id=@pqd_id;
                                    select @total= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId;
                                    select @totalQouted= count(*) from TBL_PURCHASE_QUOTE_DETAILS where Pq_Id=@registerId and Status=1;
                                    begin update TBL_PURCHASE_QUOTE_REGISTER set Status=1 where Pq_Id=@registerId end";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@pqd_id", this.Order_Detail_ID);
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_GRN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ",[Net_Amount]=" + _NetAmount + " where GRN_ID=" + GRNID);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select EntryNumber[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','GRN')[New_Order],GRN_ID from TBL_GRN_REGISTER where GRN_ID=" + GRNID);
                return new OutputMessage("GRN has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "GRNEntryRegister | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["GRN_ID"] });
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

                        return new OutputMessage("Something went wrong. GRN entry could not be updated", false, Type.Others, "GRNEntryRegister | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. GRN entry could not be updated", false, Type.Others, "GRNEntryRegister | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            finally
            {
                db.Close();
            }
        }

        public static List<GRNEntryRegister> GetDetailsForConfirm(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pq.GRN_ID,isnull(pq.EntryNumber,0)[Quote_No],pq.Location_Id,pq.Supplier_Id,pq.Created_Date Entry_Date,pq.Created_Date,pq.Tax_Amount,pq.Net_Amount,
                              pq.Net_Amount Total,isnull(pq.Status,0)[Approved_Status],pq.Status,pqd.Grn_Detail_ID,pqd.Item_Id,pqd.Received_Quantity,pqd.Ordered_Quantity,
                              isnull(pqd.Tax_Amount,0)[P_Tax_Amount],isnull(pqd.Rate,0)[P_Gross_Amount],isnull(pqd.Net_Amount,0)[P_Net_Amount],pqd.Order_Detail_ID,isnull(l.Name,0)[Location],isnull(s.Name,0)[Supplier],
                              it.Name[Item],it.Item_Code,pqd.Rate,ISNULL(pqd.tax_percentage,1) Tax_percentage,pq.Costcenter_Id,pq.Job_id,s.Email[Sup_Email],pq.TandC,pq.Payment_Terms,pqd.Description
                              from TBL_GRN_REGISTER pq with(nolock)
                              left join TBL_GRN_DETAILS pqd with(nolock) on pqd.GRN_ID=pq.GRN_ID
                              left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              left join TBL_SUPPLIER_MST s with(nolock) on s.Supplier_Id=pq.Supplier_Id
                              left join tbl_item_mst it with(nolock) on it.Item_Id=pqd.Item_Id
                              where  pq.Location_Id=@Location_Id order by pq.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<GRNEntryRegister> result = new List<GRNEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        GRNEntryRegister register = new GRNEntryRegister();
                        register.GRNID = row["GRN_ID"] != DBNull.Value ? Convert.ToInt32(row["GRN_ID"]) : 0;
                        register.InvoiceNo = Convert.ToString(row["Quote_No"]);
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["Supplier_Id"] != DBNull.Value ? Convert.ToInt32(row["Supplier_Id"]) : 0;
                        register.EntryDate = row["Created_Date"] != DBNull.Value ? Convert.ToDateTime(row["Created_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Created_Date"] != DBNull.Value ? Convert.ToDateTime(row["Created_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Net = row["Total"] != DBNull.Value ? Convert.ToDecimal(row["Total"]) : 0;
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToBoolean(row["Status"]) : false;
                        register.Location = Convert.ToString(row["Location"]);
                        register.SupplierEmail = Convert.ToString(row["Sup_Email"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.CostCenterId= row["Costcenter_Id"] != DBNull.Value ? Convert.ToInt32(row["Costcenter_Id"]) : 0;
                        register.JobId= row["Job_id"] != DBNull.Value ? Convert.ToInt32(row["Job_id"]) : 0;
                        register.Status = row["Approved_Status"] != DBNull.Value ? Convert.ToBoolean(row["Approved_Status"]) : false;
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("GRN_ID") == register.GRNID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Grn_Detail_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Grn_Detail_ID"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.RequestDetailId = rowItem["Order_Detail_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Order_Detail_ID"]) : 0;
                            item.Quantity = rowItem["Ordered_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Ordered_Quantity"]) : 0;
                            item.ModifiedQuantity = rowItem["Received_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Received_Quantity"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.Description = Convert.ToString(rowItem["Description"]);
                            //item.Order_Rate = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                            //item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseQuote | GetDetailsForConfirm(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// delete details of GRN entry register and GRN entry details
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.GRN, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "GRNEntryRegister | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "GRNEntryRegister| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_GRN_DETAILS where GRN_ID=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_GRN_REGISTER where GRN_ID=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", this.ID, this.ModifiedBy);
                    return new OutputMessage("This GRN has been deleted", true, Type.NoError, "GRNEntryRegister | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "GRNEntryRegister | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "GRNEntryRegister | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong.GRN entry could not be deleted", false, Type.Others, "GRNEntryRegister | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// details of purchase quote register and purchase quote details for find function
        /// </summary>
        /// <param name="LocationId">location id of that particular list of item</param>
        /// <returns></returns>
        public static List<GRNEntryRegister> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pq.[GRN_ID],pq.Location_Id,pq.Supplier_Id,pq.EntryNumber,pq.Created_Date,pq.Approved_Date,pq.Tax_Amount,pq.Net_Amount Gross
                              ,pq.Net_Amount Gross_Amount,pq.Approved_By,pq.Approved_Date,isnull(pq.Status,0) [Approved_Status],
                              pq.Created_Date,isnull(pq.[Status],0)[Status],pqd.Grn_Detail_ID,pqd.Order_Detail_ID,pqd.Received_Quantity,pqd.Rate,pqd.Rate[CostPrice],l.Name[Location],c.Name[Company],
                              it.Name[ItemName],it.Item_Id,it.Item_Code[ItemCode],pqd.Tax_Amount [p_Tax_Amount],isnull(sup.Name,0)[Supplier],isnull(sup.Address1,0)[Sup_Address1]    
                              ,isnull(sup.Address2,0)[Sup_Address2],isnull(sup.Phone1,0)[Sup_Phone1],isnull(l.Address1,0)[Loc_Address1],isnull(l.Address2,0)[Loc_Address2],
                              isnull(l.Contact,0)[Loc_Contact],isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone],sup.Email
                              ,c.Reg_Id1[Company_RegistrationId],sup.Taxno1[Supplier_TaxNo],l.Reg_Id1[Location_RegistrationId],c.Logo[Company_Logo],c.Email[Comp_Email],pqd.Tax_Percentage Percentage
							  ,pqd.Net_Amount P_Net_Amount,pqd.Rate P_Gross_Amount,pqd.Tax_Amount P_Tax_Amount,pqd.Ordered_Quantity,pqd.Received_Quantity,pqd.Status P_Status,pq.TandC,pq.Payment_Terms
                              from TBL_GRN_REGISTER pq with(nolock)
                              left join TBL_GRN_DETAILS pqd with(nolock) on pqd.GRN_ID=pq.GRN_ID
                              left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              left join TBL_COMPANY_MST c with(nolock) on c.Company_Id=l.Company_Id
                              left join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pq.Supplier_Id
                              left join TBL_ITEM_MST it with(nolock) on it.Item_Id=pqd.Item_Id
                              where pq.Location_Id=@LocationId order by pq.Created_Date desc";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@LocationId", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<GRNEntryRegister> result = new List<GRNEntryRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        GRNEntryRegister register = new GRNEntryRegister();
                        register.GRNID = row["GRN_ID"] != DBNull.Value ? Convert.ToInt32(row["GRN_ID"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.EntryDateString = row["Approved_Date"] != DBNull.Value ? Convert.ToDateTime(row["Approved_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        //register.DueDateString = row["due_date"] != DBNull.Value ? Convert.ToDateTime(row["due_date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Gross"] != DBNull.Value ? Convert.ToDecimal(row["Gross"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.InvoiceNo = Convert.ToString(row["EntryNumber"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegistrationId"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.SupplierTaxNo = Convert.ToString(row["Supplier_TaxNo"]);
                        register.SupplierAddress1 = Convert.ToString(row["Sup_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Sup_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Sup_Phone1"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Contact"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                        register.SupplierMail = Convert.ToString(row["Email"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        //register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = Convert.ToBoolean(row["Status"]);
                        register.Company = Convert.ToString(row["company"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        //register.IsApproved = row["Approved_Status"] != DBNull.Value ? Convert.ToBoolean(row["Approved_Status"]) : false;
                        //register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        //register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("GRN_ID") == register.GRNID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            //item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Grn_Detail_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Grn_Detail_ID"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.RequestDetailId = rowItem["Order_Detail_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Order_Detail_ID"]) : 0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.Status = Convert.ToInt32(rowItem["P_Status"]);
                            item.MRP = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                            item.Quantity = rowItem["Ordered_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Ordered_Quantity"]) : 0;
                            item.TaxPercentage = rowItem["Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Percentage"]) : 0;
                            item.CostPrice = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.ModifiedQuantity = rowItem["Received_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Received_Quantity"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["ItemCode"]);
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
                Application.Helper.LogException(ex, "PurchaseQuote | GetDetails(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static GRNEntryRegister GetDetails(int Id, int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select gr.GRN_ID,gr.Location_ID,gr.EntryNumber,gr.Created_Date[Entry_Date],gr.Supplier_ID,gr.Tax_Amount,gr.Status,gr.Net_Amount,gr.Costcenter_Id,gr.Job_id,
                               gd.Grn_Detail_ID,gd.Item_ID,gd.Ordered_Quantity,gd.Received_Quantity,gd.Rate,gd.Net_Amount[P_Net_Amount],gd.Tax_Amount[P_Tax_Amount],
                               gd.Tax_Percentage,l.Name[Location],s.Name[Supplier],i.item_id,i.Name[Item],
                               i.Item_Code,
                               s.Taxno1[Sup_TaxNo],l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegNo]
                               ,coun.Name[Sup_Country],st.Name[Sup_State],gr.TandC,gr.Payment_Terms,gd.Description ,
							   gr.Salutation,gr.Contact_Name,gr.Contact_Address1,gr.Contact_Address2,gr.Contact_City,gr.State_ID,
							   gr.Country_ID,gr.Contact_Zipcode,gr.Contact_Phone1,gr.Contact_Phone2,gr.Contact_Email
							   from TBL_GRN_REGISTER gr
                               left join TBL_GRN_DETAILS gd on gd.GRN_ID=gr.GRN_ID
                               left join TBL_LOCATION_MST l on l.Location_Id=gr.Location_ID
                               left join TBL_SUPPLIER_MST s on s.Supplier_Id=gr.Supplier_ID
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=gr.Costcenter_Id
                               left join TBL_JOB_MST j on j.Job_Id=gr.Job_id
                               left join TBL_COUNTRY_MST coun on coun.Country_Id=gr.Country_Id
                               left join tbl_state_mst st on st.State_Id=gr.State_Id
                               left join TBL_ITEM_MST i on i.Item_Id=gd.Item_ID where gr.GRN_ID=@Id and gr.Location_ID=@Location_Id order by gr.Created_Date desc";
                #endregion Query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    GRNEntryRegister register = new GRNEntryRegister();
                    register.GRNID = row["GRN_ID"] != DBNull.Value ? Convert.ToInt32(row["GRN_ID"]) : 0;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.SupplierId = row["Supplier_Id"] != DBNull.Value ? Convert.ToInt32(row["Supplier_Id"]) : 0;
                    register.EntryDate = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]) : DateTime.MinValue;
                    register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                    register.Net = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Status = row["Status"] != DBNull.Value ? Convert.ToBoolean(row["Status"]) : false;
                    register.Location = Convert.ToString(row["Location"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                    register.LocationRegId = Convert.ToString(row["Loc_RegNo"]);
                    register.Supplier = Convert.ToString(row["Supplier"]);
                    register.SupplierTaxNo = Convert.ToString(row["Sup_TaxNo"]);
                    register.InvoiceNo = Convert.ToString(row["EntryNumber"]);
                    register.SupplierCountry = Convert.ToString(row["Sup_Country"]);
                    register.SupplierState = Convert.ToString(row["Sup_State"]);
                    register.InvoiceNo = Convert.ToString(row["EntryNumber"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    register.CostCenterId = row["Costcenter_Id"] != DBNull.Value ? Convert.ToInt32(row["Costcenter_Id"]) : 0;
                    register.JobId = row["Job_id"] != DBNull.Value ? Convert.ToInt32(row["Job_id"]) : 0;
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
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("GRN_ID") == register.GRNID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.DetailsID = rowItem["Grn_Detail_ID"] != DBNull.Value ? Convert.ToInt32(rowItem["Grn_Detail_ID"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                        item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                        item.Name = Convert.ToString(rowItem["Item"]);
                        item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                        item.Description = Convert.ToString(rowItem["Description"]);
                        item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                        //item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                        item.ModifiedQuantity = rowItem["Received_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Received_Quantity"]) : 0;
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Quantity = rowItem["Ordered_Quantity"] != DBNull.Value ? Convert.ToInt32(rowItem["Ordered_Quantity"]) : 0;
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

        public static OutputMessage SendMail(int grnId, string toAddress, int userId, string url)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.GRN, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "GRN | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "GRN | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                mail.Subject = "GRN";
                mail.Body = "Please Find the attached copy of GRN";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "GRN.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "GRN | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "GRN | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }
        #endregion Functions
    }
}
