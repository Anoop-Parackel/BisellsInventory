using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities.Register;
using System.Net;
using System.Net.Mail;
using SelectPdf;
using System.IO;
using System.Threading;
using Entities.Application;

namespace Entities.Register
{
    public class PurchaseQuoteRegister : Register, IRegister
    {
        #region Properties
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string QuoteNumber { get; set; }
        public string RequestNo { get; set; }
        public string FinancialYear { get; set; }
        public int Status { get; set; }
        public string DueDateString { get; set; }
        public bool IsApproved { get; set; }
        public int ApprovedStatus { get; set; }
        public bool Priority { get; set; }
        public string SupplierAddress1 { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierAddress2 { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierMail { get; set; }
        public string LocationAddress1 { get; set; }
        public string CompanyRegId { get; set; }
        public string CompanyEmail { get; set; }
        public string LocationRegId { get; set; }
        public int SuppStateId { get; set; }
        public string SuppState { get; set; }
        public string SuppCountry { get; set; }
        public string CompanyLogo { get; set; }
        public string SupplierTaxNo { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public bool Mail { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string CompanyPhone { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public override decimal NetAmount { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public string ETA { get; set; }
        public List<Item> Products { get; set; }
        #endregion Properties
        public PurchaseQuoteRegister(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public PurchaseQuoteRegister(int ID)
        {
            this.ID = ID;
        }
        public PurchaseQuoteRegister()
        {

        }



        /// <summary>
        /// Save individual PurchaseQuote Register and PurchaseQuote Details
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>return success alert when deatils saves successfully otherwise return error alert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Date to make purchase Quote.", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.SupplierId == 0)
                {
                    return new OutputMessage("Select a Supplier to make purchase Quote.", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.LocationId == 0)
                {
                    return new OutputMessage("Select Location to make purchase Quote.", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make purchase Quote.", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"insert into TBL_PURCHASE_QUOTE_REGISTER ([Location_Id],[Supplier_Id],[Quote_No],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Narration],[Round_Off],[Approved_Status],[Created_By],[Created_Date],[Status],[Due_Date],[entry_date],Cost_Center_Id,Job_Id,TandC,Payment_Terms,ETA,Salutation,Contact_Name,
                                   Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
                                   values(@Location_Id,@Supplier_Id,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PQT'),@Tax_Amount,@Gross_Amount,@Net_Amount,@Narration,@Round_Off,@Approved_Status,@Created_By,GETUTCDATE(),@Status,@Due_Date,@entry_date,@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@ETA,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity";
                    db.BeginTransaction();
                    db.CreateParameters(28);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Approved_Status", 0);
                    db.AddParameters(8, "@Created_By", this.CreatedBy);
                    db.AddParameters(9, "@Status", this.Status);
                    db.AddParameters(10, "@Due_Date", this.DueDateString);
                    db.AddParameters(11, "@entry_date", this.EntryDateString);
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.AddParameters(14, "@TandC", this.TermsandConditon);
                    db.AddParameters(15, "@Payment_Terms", this.Payment_Terms);
                    db.AddParameters(16, "@ETA", this.ETA);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(17, "@Salutation", address.Salutation);
                        db.AddParameters(18, "@Contact_Name", address.ContactName);
                        db.AddParameters(19, "@Contact_Address1", address.Address1);
                        db.AddParameters(20, "@Contact_Address2", address.Address2);
                        db.AddParameters(21, "@Contact_City", address.City);
                        db.AddParameters(22, "@State_ID", address.StateID);
                        db.AddParameters(23, "@Country_ID", address.CountryID);
                        db.AddParameters(24, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(25, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(26, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(27, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PQT", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInPurchaseQuote)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_QUOTE_DETAILS ([Pq_Id],[Item_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],[Gross_Amount],
                                    [Net_Amount],[Status],[Modified_Qty],Prd_Id,instance_id,Description)
                                    values(@Pq_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                    @Status,@Modified_Qty,@Prd_Id,@instance_id,@Description)";
                            db.CreateParameters(15);
                            db.AddParameters(0, "@Pqd_Id", item.DetailsID);
                            db.AddParameters(1, "@Pq_Id", identity);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Status", 0);
                            db.AddParameters(11, "@Modified_Qty", prod.ModifiedQuantity);
                            db.AddParameters(12, "@Prd_Id", item.RequestDetailId);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.AddParameters(14, "@Description", item.Description);
                            db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                        }
                    }
                    decimal _NetAmount = 0;

                    if (Application.Settings.IsAutoRoundOff())
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

                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_QUOTE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pq_Id=" + identity);
                }

                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Quote_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PQT')[New_Order],Pq_Id from TBL_PURCHASE_QUOTE_REGISTER where Pq_Id=" + identity);
                return new OutputMessage("Purchase quote has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Quote | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pq_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Purchase quote could not be saved", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Update individual PurchaseQuote register and PurchaseQuote Details
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Date to update purchase Quote.", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.SupplierId == 0)
                {
                    return new OutputMessage("Select a Supplier to update purchase Quote.", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.LocationId == 0)
                {
                    return new OutputMessage("Select Location to update purchase Quote.", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update purchase Quote.", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage(" Quote cannot be update. This request is alredy in a transaction", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"delete from tbl_purchase_quote_details where Pq_Id=@id;
                                   update TBL_PURCHASE_QUOTE_REGISTER set [Location_Id]=@Location_Id,[Supplier_Id]=@Supplier_Id,
                                   [Narration]=@Narration,[Modified_By]=@Modified_By,[Modified_Date]=GETUTCDATE(),[Status]=@Status,
                                   [entry_Date]=@entry_Date,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,due_date=@Due_Date 
                                   ,TandC=@TandC,Payment_Terms=@Payment_Terms,ETA=@ETA,Salutation=@Salutation,Contact_Name=@Contact_Name,
                                   Contact_Address1=@Contact_Address1,Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,
                                   State_ID=@State_ID,Country_ID=@Country_ID,Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                   Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email where Pq_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(28);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(8, "@Status", this.Status);
                    db.AddParameters(9, "@id", this.ID);
                    db.AddParameters(10, "@entry_Date", this.EntryDate);
                    db.AddParameters(11, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(12, "@Job_Id", this.JobId);
                    db.AddParameters(13, "@Due_Date", this.DueDateString);
                    db.AddParameters(14, "@TandC", this.TermsandConditon);
                    db.AddParameters(15, "@Payment_Terms", this.Payment_Terms);
                    db.AddParameters(16, "@ETA", this.ETA);
                    foreach (Entities.Master.Address address in this.BillingAddress)
                    {
                        db.AddParameters(17, "@Salutation", address.Salutation);
                        db.AddParameters(18, "@Contact_Name", address.ContactName);
                        db.AddParameters(19, "@Contact_Address1", address.Address1);
                        db.AddParameters(20, "@Contact_Address2", address.Address2);
                        db.AddParameters(21, "@Contact_City", address.City);
                        db.AddParameters(22, "@State_ID", address.StateID);
                        db.AddParameters(23, "@Country_ID", address.CountryID);
                        db.AddParameters(24, "@Contact_Zipcode", address.Zipcode);
                        db.AddParameters(25, "@Contact_Phone1", address.Phone1);
                        db.AddParameters(26, "@Contact_Phone2", address.Phone2);
                        db.AddParameters(27, "@Contact_Email", address.Email);
                    }
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Quote | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInPurchaseQuote)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_QUOTE_DETAILS ([Pq_Id],[Item_Id],[Qty],[Mrp],[Rate],[Tax_Id],
                                    [Tax_Amount],[Gross_Amount],[Net_Amount],[Status],[Modified_Qty],Prd_Id,instance_id,Description)
                                    values(@Pq_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,
                                    @Modified_Qty,@Prd_Id,@instance_id,@Description)";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Pq_Id", this.ID);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.AddParameters(2, "@Qty", item.Quantity);
                            db.AddParameters(3, "@Mrp", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", prod.Gross);
                            db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(9, "@Status", 0);
                            db.AddParameters(10, "@Modified_Qty", prod.ModifiedQuantity);
                            db.AddParameters(11, "@Prd_Id", item.RequestDetailId);
                            db.AddParameters(12, "@instance_id", item.InstanceId);
                            db.AddParameters(13, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                        }

                    }
                    decimal _NetAmount = 0;
                    if (Application.Settings.IsAutoRoundOff())
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

                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_QUOTE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pq_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Quote_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PQT')[New_Order],Pq_Id from tbl_purchase_quote_register where Pq_Id=" + ID);
                return new OutputMessage("Purchase Quote has been updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Quote | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pq_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Purchase quote could not be updated", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Purchase quote could not be updated", false, Type.Others, "Purchase Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }

            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Save bulk of registers and details
        /// </summary>
        /// <param name="Registers"></param>
        /// <returns>Outmessages</returns>

        public static OutputMessage BulkSave(dynamic Registers)
        {

            DBManager db = new DBManager();
            try
            {
                if (!Security.Permissions.AuthorizeUser((int)Registers.CreatedBy, Security.BusinessModules.PurchaseQuoteBuilder, Security.PermissionTypes.Create))
                {
                    return new OutputMessage("Insufficient Privilege. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote|Bulk Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else if (Registers.Qoutes.Count == 0)
                {
                    return new OutputMessage("Build a quote to save", false, Type.RequiredFields, "PurchaseQuote|Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (((DateTime)Registers.EntryDate).Year < 1900)
                {
                    return new OutputMessage("Entry date is invalid", false, Type.RequiredFields, "PurchaseQuote|Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    db.BeginTransaction();
                    for (int i = 0; i < Registers.Qoutes.Count; i++)
                    {

                        if ((Registers.Qoutes[i].DueDate) == null || ((DateTime)Registers.Qoutes[i].DueDate).Year < 1900)
                        {
                            Registers.Qoutes[i].DueDate = ((DateTime)Registers.EntryDate).AddDays(7);
                        }
                        if (Registers.Qoutes[i].Products.Count != 0 && Registers.Qoutes[i].Products != null)
                        {
                            int SupplierId = Registers.Qoutes[i].SupplierId != DBNull.Value ? Convert.ToInt32(Registers.Qoutes[i].SupplierId) : 0;
                            int LocationId = Registers.LocationId != DBNull.Value ? Convert.ToInt32(Registers.LocationId) : 0;
                            int Status = Registers.Status != DBNull.Value ? Convert.ToInt32(Registers.Status) : 0;
                            DateTime EntryDate = Registers.EntryDate != DBNull.Value ? Convert.ToDateTime(Registers.EntryDate) : null;
                            int CreatedBy = Registers.CreatedBy != DBNull.Value ? Convert.ToInt32(Registers.CreatedBy) : 0;
                            DateTime DueDate = Registers.Qoutes[i].DueDate != null ? Convert.ToDateTime(Registers.Qoutes[i].DueDate) : null;
                            string narration = Registers.Qoutes[i].Narration != DBNull.Value ? Convert.ToString(Registers.Qoutes[i].Narration) : null;
                            string query = @"insert into TBL_PURCHASE_QUOTE_REGISTER(Location_Id,Quote_No,Supplier_Id,Tax_Amount,Gross_Amount,Net_Amount,Narration,Round_Off,
                                           Approved_Status,Created_By,Created_Date,[Status],[due_date],[entry_date]) values(@Location_Id,[dbo].UDF_Generate_Sales_Bill(" + (int)Registers.CompanyId + "," + (string)Registers.FinancialYear + ",'PQT'),@Supplier_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Narration,@Round_Off,@Approved_Status,@Created_By,GETUTCDATE(),@Status,@DueDate,@EntryDate);select @@IDENTITY;";
                            decimal RTotalTax = 0;
                            decimal RGross = 0;
                            db.CreateParameters(12);
                            db.AddParameters(0, "@Location_Id", LocationId);
                            db.AddParameters(1, "@Supplier_Id", SupplierId);
                            db.AddParameters(2, "@Tax_Amount", RTotalTax);
                            db.AddParameters(3, "@Gross_Amount", RGross);
                            db.AddParameters(4, "@Net_Amount", DBNull.Value);
                            db.AddParameters(5, "@Round_Off", DBNull.Value);
                            db.AddParameters(6, "@Approved_Status", 0);
                            db.AddParameters(7, "@Created_By", CreatedBy);
                            db.AddParameters(8, "@Status", Status);
                            db.AddParameters(9, "@Narration", narration);
                            db.AddParameters(10, "@DueDate", DueDate);
                            db.AddParameters(11, "@EntryDate", EntryDate);
                            int Identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                            UpdateOrderNumber((int)Registers.CompanyId, (string)Registers.FinancialYear, "PQT", db);
                            for (int j = 0; j < Registers.Qoutes[i].Products.Count; j++)
                            {
                                if (Identity > 0)
                                {

                                    query = @"insert into TBL_PURCHASE_QUOTE_DETAILS (Pq_Id,Item_Id,Qty,Mrp,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,
                                    [Priority],[Status],Prd_Id,instance_id) values (@Pq_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                    @Priority,@Status,@Prd_Id,@instance_id)";
                                    DBManager db1 = new DBManager();
                                    db1.Open();
                                    Item prod = new Item();
                                    DataTable dt = db1.ExecuteDataSet(System.Data.CommandType.Text, @" select m.Item_Id,m.Tax_id,d.Mrp,d.Rate,ta.Percentage,d.Qty[Quantity],d.instance_id from TBL_PURCHASE_REQUEST_DETAILS d left join TBL_ITEM_MST m on d.Item_Id=m.Item_Id
                                                            left join TBL_TAX_MST ta on ta.tax_Id=d.Tax_Id where prd_id=" + Registers.Qoutes[i].Products[j].ID).Tables[0];
                                    bool Priority = Registers.Priority != DBNull.Value ? Convert.ToBoolean(Registers.Priority) : false;
                                    prod.InstanceId = dt.Rows[0]["instance_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["instance_id"]) : 0;
                                    prod.ItemID = dt.Rows[0]["Item_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Item_Id"]) : 0;
                                    prod.TaxId = dt.Rows[0]["Tax_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Tax_id"]) : 0;
                                    prod.MRP = dt.Rows[0]["Mrp"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Mrp"]) : 0;
                                    prod.CostPrice = dt.Rows[0]["Rate"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Rate"]) : 0;
                                    prod.TaxPercentage = dt.Rows[0]["Percentage"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Percentage"]) : 0;
                                    prod.Quantity = dt.Rows[0]["Quantity"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Quantity"]) : 0;
                                    prod.RequestDetailId = (int)Registers.Qoutes[i].Products[j].ID;
                                    db.CleanupParameters();

                                    prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100);
                                    prod.Gross = (decimal)Registers.Qoutes[i].Products[j].Qty * prod.CostPrice;
                                    prod.NetAmount = prod.Gross + (prod.TaxAmount * (decimal)Registers.Qoutes[i].Products[j].Qty);
                                    RTotalTax += prod.TaxAmount * (decimal)Registers.Qoutes[i].Products[j].Qty;
                                    RGross += prod.Gross;
                                    db.CreateParameters(13);
                                    db.AddParameters(0, "@Pq_Id", Identity);
                                    db.AddParameters(1, "@Item_Id", prod.ItemID);
                                    db.AddParameters(2, "@Qty", (decimal)Registers.Qoutes[i].Products[j].Qty);
                                    db.AddParameters(3, "@Mrp", prod.MRP);
                                    db.AddParameters(4, "@Rate", prod.CostPrice);
                                    db.AddParameters(5, "@Tax_Id", prod.TaxId);
                                    db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                                    db.AddParameters(7, "@Gross_Amount", prod.Gross);
                                    db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                                    db.AddParameters(9, "@Priority", Priority);
                                    db.AddParameters(10, "@Status", Status);
                                    db.AddParameters(11, "@Prd_Id", prod.RequestDetailId);
                                    db.AddParameters(12, "@instance_id", prod.InstanceId);
                                    db.ExecuteNonQuery(CommandType.Text, query);

                                    //Setting status in purchase request details
                                    db.CleanupParameters();
                                    db.ExecuteNonQuery(CommandType.Text, @"update TBL_PURCHASE_REQUEST_DETAILS set Request_Status=1 where Prd_Id=" + Registers.Qoutes[i].Products[j].ID + ";declare @requestId int,@total int,@totalQouted int; select @requestId= pr_id from TBL_PURCHASE_REQUEST_DETAILS where Prd_Id=" + Registers.Qoutes[i].Products[j].ID + ";select @total= count(*) from TBL_PURCHASE_REQUEST_DETAILS where Pr_Id=@requestId;select @totalQouted= count(*) from TBL_PURCHASE_REQUEST_DETAILS where Pr_Id=@requestId and Request_Status=1;if(@total=@totalQouted) begin update TBL_PURCHASE_REQUEST_REGISTER set Request_Status=1 where Pr_Id=@requestId end");

                                }
                                else
                                {
                                    db.RollBackTransaction();
                                    return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "PurchaseQuote|BulkSave", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }

                            decimal NetAmount = RTotalTax + RGross;
                            decimal RNet = NetAmount;
                            RNet = Math.Round(RNet);
                            decimal RoundOff = RNet - NetAmount;
                            db.ExecuteNonQuery(CommandType.Text, @"Update TBL_PURCHASE_QUOTE_REGISTER set Tax_Amount=" + RTotalTax + " , Gross_Amount=" + RGross + " , Net_Amount=" + NetAmount + ",[Round_Off]=" + RoundOff + " where Pq_Id=" + Identity);

                        }
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Purchase Orders created successfully ", true, Type.NoError, "PurchaseQuote|BulkSave", System.Net.HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();

                return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "PurchaseQuote|BulkSave", System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            finally
            {
                db.Close();

            }

        }

        public static OutputMessage BulkSaveFromIndent(dynamic Registers)
        {

            DBManager db = new DBManager();
            try
            {
                if (!Security.Permissions.AuthorizeUser((int)Registers.CreatedBy, Security.BusinessModules.PurchaseQuoteBuilder, Security.PermissionTypes.Create))
                {
                    return new OutputMessage("Insufficient Privilege. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote|BulkSaveFromIndent", System.Net.HttpStatusCode.InternalServerError);
                }

                else if (Registers.Qoutes.Count == 0)
                {
                    return new OutputMessage("Build a quote to save", false, Type.RequiredFields, "PurchaseQuote|BulkSaveFromIndent", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (((DateTime)Registers.EntryDate).Year < 1900)
                {
                    return new OutputMessage("Entry date is invalid", false, Type.RequiredFields, "PurchaseQuote|BulkSaveFromIndent", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    db.BeginTransaction();
                    for (int i = 0; i < Registers.Qoutes.Count; i++)
                    {

                        if ((Registers.Qoutes[i].DueDate) == null || ((DateTime)Registers.Qoutes[i].DueDate).Year < 1900)
                        {
                            Registers.Qoutes[i].DueDate = ((DateTime)Registers.EntryDate).AddDays(7);
                        }
                        if (Registers.Qoutes[i].Products.Count != 0 && Registers.Qoutes[i].Products != null)
                        {
                            int SupplierId = Registers.Qoutes[i].SupplierId != DBNull.Value ? Convert.ToInt32(Registers.Qoutes[i].SupplierId) : 0;
                            int LocationId = Registers.LocationId != DBNull.Value ? Convert.ToInt32(Registers.LocationId) : 0;
                            int Status = Registers.Status != DBNull.Value ? Convert.ToInt32(Registers.Status) : 0;
                            DateTime EntryDate = Registers.EntryDate != DBNull.Value ? Convert.ToDateTime(Registers.EntryDate) : null;
                            int CreatedBy = Registers.CreatedBy != DBNull.Value ? Convert.ToInt32(Registers.CreatedBy) : 0;
                            DateTime DueDate = Registers.Qoutes[i].DueDate != null ? Convert.ToDateTime(Registers.Qoutes[i].DueDate) : null;
                            string narration = Registers.Qoutes[i].Narration != DBNull.Value ? Convert.ToString(Registers.Qoutes[i].Narration) : null;
                            string query = @"insert into TBL_PURCHASE_QUOTE_REGISTER(Location_Id,Quote_No,Supplier_Id,Tax_Amount,Gross_Amount,Net_Amount,Narration,Round_Off,
                                           Approved_Status,Created_By,Created_Date,[Status],[due_date],[entry_date]) values(@Location_Id,[dbo].UDF_Generate_Sales_Bill(" + (int)Registers.CompanyId + "," + (string)Registers.FinancialYear + ",'PQT'),@Supplier_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Narration,@Round_Off,@Approved_Status,@Created_By,GETUTCDATE(),@Status,@DueDate,@EntryDate);select @@IDENTITY;";
                            decimal RTotalTax = 0;
                            decimal RGross = 0;
                            db.CreateParameters(12);
                            db.AddParameters(0, "@Location_Id", LocationId);
                            db.AddParameters(1, "@Supplier_Id", SupplierId);
                            db.AddParameters(2, "@Tax_Amount", RTotalTax);
                            db.AddParameters(3, "@Gross_Amount", RGross);
                            db.AddParameters(4, "@Net_Amount", DBNull.Value);
                            db.AddParameters(5, "@Round_Off", DBNull.Value);
                            db.AddParameters(6, "@Approved_Status", 0);
                            db.AddParameters(7, "@Created_By", CreatedBy);
                            db.AddParameters(8, "@Status", Status);
                            db.AddParameters(9, "@Narration", narration);
                            db.AddParameters(10, "@DueDate", DueDate);
                            db.AddParameters(11, "@EntryDate", EntryDate);
                            int Identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                            UpdateOrderNumber((int)Registers.CompanyId, (string)Registers.FinancialYear, "PQT", db);
                            for (int j = 0; j < Registers.Qoutes[i].Products.Count; j++)
                            {
                                if (Identity > 0)
                                {

                                    query = @"insert into TBL_PURCHASE_QUOTE_DETAILS (Pq_Id,Item_Id,Qty,Mrp,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,
                                    [Priority],[Status],Prd_Id,instance_id) values (@Pq_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                    @Priority,@Status,@Prd_Id,@instance_id)";
                                    DBManager db1 = new DBManager();
                                    db1.Open();
                                    Item prod = new Item();
                                    DataTable dt = db1.ExecuteDataSet(System.Data.CommandType.Text, @" select m.Item_Id,m.Tax_id,d.Mrp,d.Rate,ta.Percentage,d.Qty[Quantity],d.instance_id from TBL_PURCHASE_INDENT_DETAILS d left join TBL_ITEM_MST m on d.Item_Id=m.Item_Id
                                                            left join TBL_TAX_MST ta on ta.tax_Id=d.Tax_Id where Pid_Id=" + Registers.Qoutes[i].Products[j].ID).Tables[0];
                                    bool Priority = Registers.Priority != DBNull.Value ? Convert.ToBoolean(Registers.Priority) : false;
                                    prod.InstanceId = dt.Rows[0]["instance_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["instance_id"]) : 0;
                                    prod.ItemID = dt.Rows[0]["Item_Id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Item_Id"]) : 0;
                                    prod.TaxId = dt.Rows[0]["Tax_id"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Tax_id"]) : 0;
                                    prod.MRP = dt.Rows[0]["Mrp"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Mrp"]) : 0;
                                    prod.CostPrice = Registers.Qoutes[i].Products[j].Rate;//dt.Rows[0]["Rate"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Rate"]) : 0;
                                    prod.TaxPercentage = dt.Rows[0]["Percentage"] != DBNull.Value ? Convert.ToDecimal(dt.Rows[0]["Percentage"]) : 0;
                                    prod.Quantity = dt.Rows[0]["Quantity"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Quantity"]) : 0;
                                    prod.RequestDetailId = (int)Registers.Qoutes[i].Products[j].ID;
                                    db.CleanupParameters();

                                    prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100);
                                    prod.Gross = (decimal)Registers.Qoutes[i].Products[j].Qty * prod.CostPrice;
                                    prod.NetAmount = prod.Gross + (prod.TaxAmount * (decimal)Registers.Qoutes[i].Products[j].Qty);
                                    RTotalTax += prod.TaxAmount * (decimal)Registers.Qoutes[i].Products[j].Qty;
                                    RGross += prod.Gross;
                                    db.CreateParameters(13);
                                    db.AddParameters(0, "@Pq_Id", Identity);
                                    db.AddParameters(1, "@Item_Id", prod.ItemID);
                                    db.AddParameters(2, "@Qty", (decimal)Registers.Qoutes[i].Products[j].Qty);
                                    db.AddParameters(3, "@Mrp", prod.MRP);
                                    db.AddParameters(4, "@Rate", prod.CostPrice);
                                    db.AddParameters(5, "@Tax_Id", prod.TaxId);
                                    db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                                    db.AddParameters(7, "@Gross_Amount", prod.Gross);
                                    db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                                    db.AddParameters(9, "@Priority", Priority);
                                    db.AddParameters(10, "@Status", Status);
                                    db.AddParameters(11, "@Prd_Id", prod.RequestDetailId);
                                    db.AddParameters(12, "@instance_id", prod.InstanceId);
                                    db.ExecuteNonQuery(CommandType.Text, query);

                                    //Setting status in purchase request details
                                    db.CleanupParameters();
                                    db.ExecuteNonQuery(CommandType.Text, @"update TBL_PURCHASE_INDENT_DETAILS set Request_Status=1 where Pid_Id=" + Registers.Qoutes[i].Products[j].ID + ";declare @requestId int,@total int,@totalQouted int; select @requestId= Pi_Id from TBL_PURCHASE_INDENT_DETAILS where Pid_Id=" + Registers.Qoutes[i].Products[j].ID + ";select @total= count(*) from TBL_PURCHASE_INDENT_DETAILS where Pi_Id=@requestId;select @totalQouted= count(*) from TBL_PURCHASE_INDENT_DETAILS where Pi_Id=@requestId and Request_Status=1;if(@total=@totalQouted) begin update TBL_PURCHASE_INDENT_REGISTER set Request_Status=1 where Pi_Id=@requestId end");

                                }
                                else
                                {
                                    db.RollBackTransaction();
                                    return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "PurchaseQuote|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }

                            decimal NetAmount = RTotalTax + RGross;
                            decimal RNet = NetAmount;
                            RNet = Math.Round(RNet);
                            decimal RoundOff = RNet - NetAmount;
                            db.ExecuteNonQuery(CommandType.Text, @"Update TBL_PURCHASE_QUOTE_REGISTER set Tax_Amount=" + RTotalTax + " , Gross_Amount=" + RGross + " , Net_Amount=" + NetAmount + ",[Round_Off]=" + RoundOff + " where Pq_Id=" + Identity);

                        }
                    }
                    db.CommitTransaction();
                    return new OutputMessage("Purchase Orders created successfully ", true, Type.NoError, "PurchaseQuote|BulkSaveFromIndent", System.Net.HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();

                return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "PurchaseQuote|BulkSaveFromIndent", System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            finally
            {
                db.Close();

            }

        }


        public OutputMessage Confirm()
        {


            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Confirm", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for Confirming", false, Type.RequiredFields, "PurchaseQuote| Confirm", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "update TBL_PURCHASE_QUOTE_REGISTER set Approved_Status=1,ApprovedBy_Id=@Modified_By,Approved_Time=getutcdate() where pq_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    db.ExecuteNonQuery(CommandType.Text, query);

                    return new OutputMessage("Purchase Quote Request has been Confirmed", true, Type.NoError, "PurchaseQuote | Confirm", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseQuote | Confirm", System.Net.HttpStatusCode.InternalServerError, ex);

                }
            }

        }
        public OutputMessage ToggleConfirm()
        {


            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | ToggleConfirm", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for Confirming", false, Type.RequiredFields, "PurchaseQuote| ToggleConfirm", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = " update TBL_PURCHASE_QUOTE_REGISTER set Approved_Status=isnull(Approved_Status,0)^1,ApprovedBy_Id=@Modified_By,Approved_Time=GETUTCDATE() where Pq_Id=@id;select Approved_Status from TBL_PURCHASE_QUOTE_REGISTER where pq_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    bool approvedstatus = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                    if (approvedstatus)
                    {
                        return new OutputMessage("Quote confirmed successfully ", true, Type.NoError, "PurchaseQuote | ToggleConfirm", System.Net.HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return new OutputMessage("Approval Reverted", true, Type.NoError, "PurchaseQuote | ToggleConfirm", System.Net.HttpStatusCode.OK, false);
                    }
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseQuote | ToggleConfirm", System.Net.HttpStatusCode.InternalServerError, ex);

                }
            }

        }
        public static List<PurchaseQuoteRegister> GetDetailsForConfirm(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pq.Pq_Id,isnull(pq.Quote_No,0)[Quote_No],pq.Location_Id,pq.Supplier_Id,pq.Entry_Date,pq.Due_Date,pq.Tax_Amount,pq.Gross_Amount,
                              pq.Net_Amount,isnull(pq.Approved_Status,0)[Approved_Status],pq.Status,pqd.Pqd_Id,pqd.Item_Id,pqd.Qty,
                              isnull(pqd.Tax_Amount,0)[P_Tax_Amount],isnull(pqd.Gross_Amount,0)[P_Gross_Amount],isnull(pqd.Net_Amount,0)[P_Net_Amount],pqd.Prd_Id,isnull(l.Name,0)[Location],isnull(s.Name,0)[Supplier],
                              it.Name[Item],it.Item_Code,pqd.Mrp,pqd.Rate,t.Percentage[Tax_Percentage],isnull(pq.Mail,0)[Mail],pq.TandC,Pq.ETA,pq.Payment_Terms
                              from TBL_PURCHASE_QUOTE_REGISTER pq with(nolock)
                              inner join TBL_PURCHASE_QUOTE_DETAILS pqd with(nolock) on pqd.Pq_Id=pq.Pq_Id
                              inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              inner join TBL_SUPPLIER_MST s with(nolock) on s.Supplier_Id=pq.Supplier_Id
                              inner join tbl_item_mst it with(nolock) on it.Item_Id=pqd.Item_Id
                              inner join tbl_tax_mst t with(nolock) on t.Tax_Id=pqd.Tax_Id
                              where  pq.Location_Id=@Location_Id  order by pq.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseQuoteRegister> result = new List<PurchaseQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseQuoteRegister register = new PurchaseQuoteRegister();
                        register.ID = row["Pq_Id"] != DBNull.Value ? Convert.ToInt32(row["Pq_Id"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["Supplier_Id"] != DBNull.Value ? Convert.ToInt32(row["Supplier_Id"]) : 0;
                        register.EntryDate = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.DueDateString = row["due_date"] != DBNull.Value ? Convert.ToDateTime(row["due_date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Mail = Convert.ToBoolean(row["Mail"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.ETA = Convert.ToString(row["ETA"]);
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Pq_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["pqd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.RequestDetailId = rowItem["Prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Prd_Id"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
        /// delete details of purchase quote register and purchase quote details
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "PurchaseQuote| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (HasReference(this.ID))
            {
                return new OutputMessage("Cannot be Delete. This request is alredy in a transaction", false, Type.Others, "Purchase Quote | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_PURCHASE_QUOTE_DETAILS  where pq_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_PURCHASE_QUOTE_REGISTER where pq_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Purchase quote request deleted successfully", true, Type.NoError, "PurchaseQuote | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this request because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "PurchaseEntry | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "PurchaseQuote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Purchase quote request could not be deleted", false, Type.Others, "Purchase Quote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
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
        public static List<PurchaseQuoteRegister> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pq.Pq_Id[PurchaseQuoteId],pq.Location_Id,pq.Supplier_Id,pq.Quote_No,pq.Entry_date,pq.due_date,pq.Tax_Amount,pq.Gross_Amount,pq.Round_Off
                              ,pq.Net_Amount,pq.Narration,pq.Round_Off,pq.ApprovedBy_Id,pq.Approved_Time,isnull(pq.Approved_Status,0) [Approved_Status],
                              pq.Created_Date,isnull(pq.[Status],0)[Status],pqd.Pqd_Id,pqd.Prd_Id,pqd.Qty,pqd.Mrp,pqd.Rate[CostPrice],pqd.instance_Id,l.Name[Location],c.Name[Company],
                              t.Percentage  [TaxPercentage],it.Name[ItemName],it.Item_Id,it.Item_Code[ItemCode],pqd.Tax_Amount[p_Tax_Amount],pqd.Gross_Amount[P_Gross_Amount]
                              ,pqd.Net_Amount[P_Net_Amount],isnull(pqd.[Status],0)[P_Status],isnull(sup.Name,0)[Supplier],isnull(sup.Address1,0)[Sup_Address1]    
                              ,isnull(sup.Address2,0)[Sup_Address2],isnull(sup.Phone1,0)[Sup_Phone1],isnull(l.Address1,0)[Loc_Address1],isnull(l.Address2,0)[Loc_Address2],
                              isnull(l.Contact,0)[Loc_Contact],isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone],sup.Email
                              ,c.Reg_Id1[Company_RegistrationId],sup.Taxno1[Supplier_TaxNo],l.Reg_Id1[Location_RegistrationId],c.Logo[Company_Logo],c.Email[Comp_Email],pq.TandC,Pq.ETA,pq.Payment_Terms,pqd.Description
							  from TBL_PURCHASE_QUOTE_REGISTER pq with(nolock)
                              left join TBL_PURCHASE_QUOTE_DETAILS pqd with(nolock) on pqd.Pq_Id=pq.Pq_Id
                              left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              left join TBL_COMPANY_MST c with(nolock) on c.Company_Id=l.Company_Id
                              left join TBL_TAX_MST t with(nolock) on t.Tax_Id=pqd.Tax_Id
                              left join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pq.Supplier_Id
                              left join TBL_ITEM_MST it with(nolock) on it.Item_Id=pqd.Item_Id
                              left join TBL_PURCHASE_ENTRY_DETAILS pe with(nolock) on pqd.Pqd_Id=pe.Pqd_Id
                              where pq.Location_Id=@LocationId order by pq.Created_Date desc";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@LocationId", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseQuoteRegister> result = new List<PurchaseQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseQuoteRegister register = new PurchaseQuoteRegister();
                        register.ID = row["PurchaseQuoteId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseQuoteId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.DueDateString = row["due_date"] != DBNull.Value ? Convert.ToDateTime(row["due_date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
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
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.Company = Convert.ToString(row["company"]);
                        register.IsApproved = row["Approved_Status"] != DBNull.Value ? Convert.ToBoolean(row["Approved_Status"]) : false;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.ETA = Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseQuoteId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["pqd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.RequestDetailId = rowItem["Prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Prd_Id"]) : 0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.Status = Convert.ToInt32(rowItem["P_Status"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["ItemCode"]);
                            item.Description= Convert.ToString(rowItem["Description"]);
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

        public static List<PurchaseQuoteRegister> GetDetails(int LocationId, int? SupplierId, DateTime? from, DateTime? to)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select pq.Pq_Id[PurchaseQuoteId],pq.Location_Id,pq.Supplier_Id,pq.Quote_No,pq.Entry_date,pq.due_date,pq.Tax_Amount,pq.Gross_Amount,pq.Round_Off
                              ,pq.Net_Amount,pq.Narration,pq.Round_Off,pq.ApprovedBy_Id,pq.Approved_Time,isnull(pq.Approved_Status,0) [Approved_Status],
                              pq.Created_Date,isnull(pq.[Status],0)[Status],pqd.Pqd_Id,pqd.Prd_Id,pqd.Qty,pqd.Mrp,pqd.Rate[CostPrice],pqd.instance_Id,l.Name[Location],c.Name[Company],
                              t.Percentage  [TaxPercentage],it.Name[ItemName],it.Item_Id,it.Item_Code[ItemCode],pqd.Tax_Amount[p_Tax_Amount],pqd.Gross_Amount[P_Gross_Amount]
                              ,pqd.Net_Amount[P_Net_Amount],isnull(pqd.[Status],0)[P_Status],isnull(sup.Name,0)[Supplier],isnull(sup.Address1,0)[Sup_Address1]    
                              ,isnull(sup.Address2,0)[Sup_Address2],isnull(sup.Phone1,0)[Sup_Phone1],isnull(l.Address1,0)[Loc_Address1],isnull(l.Address2,0)[Loc_Address2],
                              isnull(l.Contact,0)[Loc_Contact],isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone],sup.Email,
                              isnull(pq.Cost_Center_Id,0)[Cost_Center_Id],isnull(pq.Job_Id,0)[Job_Id],j.Job_Name[Job],cost.Fcc_Name[Cost_Center],pq.TandC,Pq.ETA,pq.Payment_Terms,pqd.Description
							  from TBL_PURCHASE_QUOTE_REGISTER pq with(nolock)
                              inner join TBL_PURCHASE_QUOTE_DETAILS pqd with(nolock) on pqd.Pq_Id=pq.Pq_Id
                              inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              inner join TBL_COMPANY_MST c with(nolock) on c.Company_Id=l.Company_Id
                              inner join TBL_TAX_MST t with(nolock) on t.Tax_Id=pqd.Tax_Id
                              inner join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pq.Supplier_Id
                              inner join TBL_ITEM_MST it with(nolock) on it.Item_Id=pqd.Item_Id
                              left join TBL_PURCHASE_ENTRY_DETAILS pe with(nolock) on pqd.Pqd_Id=pe.Pqd_Id
							  left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pq.Cost_Center_Id
							  left join TBL_JOB_MST j on j.Job_Id=pq.Job_Id
                              where pq.Location_Id=@LocationId {#supplierfilter#} {#daterangefilter#} order by pq.Created_Date desc";
                #endregion Query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and pq.Entry_Date>=@fromdate and pq.Entry_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and pq.Entry_Date>=@fromdate and pq.Entry_Date<=@todate ");
                }
                if (SupplierId != null && SupplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and pq.Supplier_Id=@Supplier_Id ");
                }
                else
                {

                    query = query.Replace("{#supplierfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@LocationId", LocationId);
                db.AddParameters(1, "@fromdate", from);
                db.AddParameters(2, "@todate", to);
                db.AddParameters(3, "@Supplier_Id", SupplierId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseQuoteRegister> result = new List<PurchaseQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseQuoteRegister register = new PurchaseQuoteRegister();
                        register.ID = row["PurchaseQuoteId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseQuoteId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.DueDateString = row["due_date"] != DBNull.Value ? Convert.ToDateTime(row["due_date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.SupplierAddress1 = Convert.ToString(row["Sup_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Sup_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Sup_Phone1"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Contact"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.SupplierMail = Convert.ToString(row["Email"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.Company = Convert.ToString(row["company"]);
                        register.IsApproved = row["Approved_Status"] != DBNull.Value ? Convert.ToBoolean(row["Approved_Status"]) : false;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        register.ETA = Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseQuoteId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["pqd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.RequestDetailId = rowItem["Prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Prd_Id"]) : 0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.Status = Convert.ToInt32(rowItem["P_Status"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["ItemCode"]);
                            item.Description= Convert.ToString(rowItem["Description"]);
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
                Application.Helper.LogException(ex, "PurchaseQuote | GetDetails(int LocationId, int? SupplierId, DateTime? from, DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


        public static PurchaseQuoteRegister GetDetails(int Id, int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pq.Pq_Id[PurchaseQuoteId],ut.name [unit],pq.Location_Id,pq.Supplier_Id,pq.Quote_No,pq.Entry_date,pq.due_date,pq.Tax_Amount,pq.Gross_Amount,pq.Round_Off
                              ,pq.Net_Amount,pq.Narration,pq.Round_Off,pq.ApprovedBy_Id,pq.Approved_Time,isnull(pq.Approved_Status,0) [Approved_Status],
                              pq.Created_Date,isnull(pq.[Status],0)[Status],pqd.Pqd_Id,pqd.Prd_Id,pqd.Qty,pqd.Mrp,pqd.Rate[CostPrice],pqd.instance_Id,l.Name[Location],c.Name[Company],
                              t.Percentage  [TaxPercentage],it.Name[ItemName],it.Item_Id,it.Item_Code[ItemCode],pqd.Tax_Amount[p_Tax_Amount],pqd.Gross_Amount[P_Gross_Amount]
                              ,pqd.Net_Amount[P_Net_Amount],isnull(pqd.[Status],0)[P_Status],isnull(sup.Name,0)[Supplier],
                              isnull(l.Address1,0)[Loc_Address1],isnull(l.Address2,0)[Loc_Address2],
                              isnull(l.Contact,0)[Loc_Contact],l.Reg_Id1[Loc_RegId],sup.Taxno1[Sup_TaxNo],c.Email[Comp_Email],
							   coun.Name[Sup_Country],st.Name[Sup_State],
							  isnull(pq.Cost_Center_Id,0)[Cost_Center_Id],isnull(pq.Job_Id,0)[Job_Id],j.Job_Name[Job],
							  cost.Fcc_Name[Cost_Center],pq.TandC,Pq.ETA,pq.Payment_Terms,pqd.Description,
							  pq.Salutation,pq.Contact_Name,pq.Contact_Address1,pq.Contact_Address2,pq.Contact_City,pq.State_ID,pq.Country_ID,pq.Contact_Zipcode,
                              pq.Contact_Phone1,pq.Contact_Phone2,pq.Contact_Email
							  from TBL_PURCHASE_QUOTE_REGISTER pq with(nolock)
                              inner join TBL_PURCHASE_QUOTE_DETAILS pqd with(nolock) on pqd.Pq_Id=pq.Pq_Id
                              inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pq.Location_Id
                              inner join TBL_COMPANY_MST c with(nolock) on c.Company_Id=l.Company_Id
                              inner join TBL_TAX_MST t with(nolock) on t.Tax_Id=pqd.Tax_Id
							  inner join TBL_SUPPLIER_MST sup on sup.Supplier_Id=pq.Supplier_Id
							  inner join TBL_COUNTRY_MST coun with(nolock) on coun.Country_Id=sup.Country_Id
							  left join TBL_STATE_MST st with(nolock) on st.State_Id=sup.State_Id
                              inner join TBL_ITEM_MST it with(nolock) on it.Item_Id=pqd.Item_Id
                              left join TBL_PURCHASE_ENTRY_DETAILS pe with(nolock) on pqd.Pqd_Id=pe.Pqd_Id
							  left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pq.Cost_Center_Id
							  left join TBL_JOB_MST j on j.Job_Id=pq.Job_Id
							  left join tbl_unit_mst ut on ut.unit_id=it.unit_id
                              where pq.Location_Id=@LocationId and pq.Pq_Id=@Pq_Id order by pq.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@LocationId", LocationId);
                db.AddParameters(1, "@Pq_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    DataRow row = dt.Rows[0];
                    PurchaseQuoteRegister register = new PurchaseQuoteRegister();
                    register.ID = row["PurchaseQuoteId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseQuoteId"]) : 0;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                    register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.DueDateString = row["due_date"] != DBNull.Value ? Convert.ToDateTime(row["due_date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                    register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Supplier = Convert.ToString(row["Supplier"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                    register.Location = Convert.ToString(row["Location"]);
                    register.SuppState = Convert.ToString(row["Sup_State"]);
                    register.SuppCountry = Convert.ToString(row["Sup_Country"]);
                    register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                    register.SupplierTaxNo = Convert.ToString(row["Sup_TaxNo"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Contact"]);
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.Status = Convert.ToInt32(row["Status"]);
                    register.Company = Convert.ToString(row["company"]);
                    register.IsApproved = row["Approved_Status"] != DBNull.Value ? Convert.ToBoolean(row["Approved_Status"]) : false;
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    register.ETA = Convert.ToString(row["ETA"]);
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
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseQuoteId") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.DetailsID = rowItem["pqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["pqd_Id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.RequestDetailId = rowItem["Prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Prd_Id"]) : 0;
                        item.Name = rowItem["ItemName"].ToString();
                        item.Unit = rowItem["unit"].ToString();
                        item.Status = Convert.ToInt32(rowItem["P_Status"]);
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                        item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                        item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                        item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                        item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                        item.ItemCode = Convert.ToString(rowItem["ItemCode"]);
                        item.Description = Convert.ToString(rowItem["Description"]);
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
                Application.Helper.LogException(ex, "PurchaseQuote | GetDetails(int Id,int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// check for the purchase quote is refered in another transactions
        /// </summary>
        /// <param name="purchaseQuoteid (id)"></param>
        /// <returns>bool which either have refernce or not</returns>
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
                    string query = @"  select count(*) from [TBL_PURCHASE_ENTRY_DETAILS] ped with(nolock) join
                                        TBL_PURCHASE_QUOTE_DETAILS q with(nolock) on q.Pqd_Id=ped.Pqd_Id
                                        join  TBL_PURCHASE_QUOTE_REGISTER pqr with(nolock) on q.Pq_Id=pqr.Pq_Id where
                                        pqr.Pq_Id=@id";
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
                    Application.Helper.LogException(ex, "PurchaseQuote |  HasReference(int id)");
                    throw;
                }
            }
        }


        //public async Task<OutputMessage> SendPoAsync(int PoId, string url, int CreatedBy)
        //{
        //     OutputMessage result= await Task.Run(() => {
        //        if (!Entities.Security.Permissions.AuthorizeUser(CreatedBy, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
        //        {
        //            return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);
        //        }
        //        try
        //        {
        //            DBManager db = new DBManager();
        //            db.Open();
        //            db.CreateParameters(1);
        //            db.AddParameters(0, "@PoId", PoId);
        //            string ChekQuery = @"select isnull(Approved_Status,0) Approved_Status from TBL_PURCHASE_QUOTE_REGISTER where Pq_Id=@PoId";

        //            if (!Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, ChekQuery)))
        //            {
        //                return new OutputMessage("Please confirm the quote for send mail", false, Type.Others, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);

        //            }
        //            string toAddress;
        //            string query = @"select s.Email from TBL_PURCHASE_QUOTE_REGISTER p
        //                      left join TBL_SUPPLIER_MST s on s.Supplier_Id=p.Supplier_Id
        //                       where p.Pq_Id=@PoId";

        //            toAddress = Convert.ToString(db.ExecuteScalar(CommandType.Text, query));

        //            //getting the email content from print page
        //            WebClient client = new WebClient();
        //            UTF8Encoding utf8 = new UTF8Encoding();
        //            string htmlMarkup = utf8.GetString(client.DownloadData(url));

        //            //creating PDF 
        //            HtmlToPdf converter = new HtmlToPdf();
        //            PdfDocument doc = converter.ConvertUrl(url);
        //             byte[] bytes;
        //             using (MemoryStream ms = new MemoryStream())
        //             {
        //                 doc.Save(ms);
        //                 bytes = ms.ToArray();
        //                 ms.Close();
        //                 doc.DetachStream();
        //             }

        //             //sending the mail
        //             string query1 = @"SELECT * from 
        //                               (SELECT keyValue AS Email_Id FROM TBL_SETTINGS where KeyID=106) AS Email_Id,
        //                               (SELECT keyValue AS Email_Password FROM TBL_SETTINGS where KeyID=107) AS Email_Password,
        //                               (SELECT keyValue AS Email_Host FROM TBL_SETTINGS where KeyID=108) AS Email_Host,
        //                               (SELECT keyValue AS Email_Port FROM TBL_SETTINGS where KeyID=109) AS Email_Port";
        //             DataTable dt = new DataTable();
        //             dt = db.ExecuteQuery(CommandType.Text, query1);
        //             MailMessage mail = new MailMessage(Convert.ToString(dt.Rows[0]["Email_Id"]), toAddress);
        //            mail.IsBodyHtml = false;
        //            mail.Subject = "Purchase Quote";
        //            mail.Body = "Please Find the attached copy of Purchase Quote";
        //            mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "PurchaseQuote.pdf"));
        //            SmtpClient smtp = new SmtpClient()
        //            {
        //                Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
        //                EnableSsl = true,
        //                UseDefaultCredentials = false,
        //                Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
        //                Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

        //            };
        //            smtp.Send(mail);
        //            query = @"update tbl_purchase_quote_register set Mail=1 where Pq_Id=@Pq_Id";
        //            db.CreateParameters(1);
        //            db.AddParameters(0, "@Pq_Id", PoId);
        //            db.ExecuteNonQuery(CommandType.Text, query);
        //            return new OutputMessage("Mail send successfully", true, Type.NoError, "PurchaseQuote | SendPo", System.Net.HttpStatusCode.OK);
        //        }
        //        catch (Exception ex)
        //        {
        //            return new OutputMessage("Mail sending failed", false, Type.RequiredFields, "PurchaseQuote | SendPo", System.Net.HttpStatusCode.InternalServerError,ex);
        //        }
        //    });

        //    return result;
        //}

        public static OutputMessage SendMail(int PurchaseId, string toAddress, int userId, string url)
        {


            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email address is not valid. Please revert and try again", false, Type.InsufficientPrivilege, "Purchase Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError);
            }
            try
            {
                DBManager db = new DBManager();
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@PoId", PurchaseId);
                string ChekQuery = @"select isnull(Approved_Status,0) Approved_Status from TBL_PURCHASE_QUOTE_REGISTER where Pq_Id=@PoId";

                if (!Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, ChekQuery)))
                {
                    return new OutputMessage("Please confirm the quote for send mail", false, Type.Others, "PurchaseQuote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
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
                mail.Subject = "Purchase Order";
                mail.Body = "Please Find the attached copy of Purchase Order";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Purchase Order.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                query = @"update tbl_purchase_quote_register set Mail=1 where Pq_Id=@Pq_Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Pq_Id", PurchaseId);
                db.ExecuteNonQuery(CommandType.Text, query);
                return new OutputMessage("Mail send successfully", true, Type.NoError, "Purchase Quote | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail sending failed", false, Type.RequiredFields, "Purchase Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}

