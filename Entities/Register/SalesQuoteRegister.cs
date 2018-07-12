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

    public class SalesQuoteRegister : Register, IRegister
    {
        #region Properties
        public int RequestNo { get; set; }
        public int SchemeId { get; set; }
        public int CustomerId { get; set; }
        public string QuoteNumber { get; set; }
        public string CustomerName { get; set; }
        public string FinancialYear { get; set; }
        public string CustomerAddress { get; set; }
        public string ContactNo { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerEmail { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyRegId { get; set; }
        public string LocationRegId { get; set; }
        public string CustomerTaxNo { get; set; }
        public string LocationPhone { get; set; }
        public string CustomerState { get; set; }
        public string CustomerCountry { get; set; }
        public int CustStateId { get; set; }
        public int ApprovedStatus { get; set; }
        public override decimal NetAmount { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public bool Priority { get; set; }
        public List<Item> Products { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedDateString { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public string Validity { get; set; }
        public string ETA { get; set; }
        public string Customer_Name { get; set; }
        public int Status { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        #endregion Properties
        public SalesQuoteRegister(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public SalesQuoteRegister()
        {

        }


        /// <summary>
        /// Function to Manipulate the Sales Request Register
        /// Inserts new Entry if ID is 0
        /// Update the existing Entry with the given ID
        /// Make sure the instance properties are set
        /// </summary>
        /// <returns></returns>

        public OutputMessage Save()
        {
            int identity;
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesQuote | SaveOrUpdate", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a customer to make a request.", false, Type.Others, "Sales Quote | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (string.IsNullOrWhiteSpace((this.CustomerName)))
                {
                    return new OutputMessage("Please enter customer name to make a request", false, Type.Others, "Sales Quote | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a request.", false, Type.Others, "Sales Quote | Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {

                    db.Open();
                    string query = @"INSERT INTO [dbo].[TBL_SALES_QUOTE_REGISTER]([Location_Id],[Customer_Id],[Quote_No],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Narration],[Round_Off],
                                   [Approved_Status],[Status],[Created_By],[Created_Date],entry_date,[customer_name],[customer_Address],[customer_Phone],Cost_Center_Id,Job_Id,TandC,Payment_Terms,Validity,ETA,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email,Discount)
                                   VALUES(@Location_Id,@Customer_Id,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SQT'),@Tax_Amount,@Gross_Amount, @Net_Amount,@Narration,@Round_Off,@Approved_Status,@Status,@Created_By,GETUTCDATE(),@entry_date,@CustomerName,@CustomerAddress,@ContactNo,@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@Validity,@ETA,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email,@Discount);select @@identity";
                    db.CreateParameters(32);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Approved_Status", false);
                    db.AddParameters(8, "@Status", 0);
                    db.AddParameters(9, "@Created_By", this.CreatedBy);
                    db.AddParameters(10, "@entry_date", this.EntryDate);
                    db.AddParameters(11, "@CustomerName", this.CustomerName);
                    db.AddParameters(12, "@CustomerAddress", this.CustomerAddress);
                    db.AddParameters(13, "@ContactNo", this.ContactNo);
                    db.AddParameters(14, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(15, "@Job_Id", this.JobId);
                    db.AddParameters(16, "@TandC", this.TermsandConditon);
                    db.AddParameters(17, "@Payment_Terms", this.Payment_Terms);
                    db.AddParameters(18, "@Validity", this.Validity);
                    db.AddParameters(19, "@ETA", this.ETA);
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
                    db.AddParameters(31, "@Discount", this.Discount);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "SQT", db);

                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Quote | Save", System.Net.HttpStatusCode.InternalServerError);
                        }

                        else
                        {
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            if (prod.TrackInventory == true) //get thetrack inventory in Getpricewithscheme function
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Quote|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            if (setting.AllowPriceEditingInSalesOrder)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            //if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock<item.Quantity)
                            //{
                            //    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Quote|Save", System.Net.HttpStatusCode.InternalServerError);

                            //}
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"INSERT INTO TBL_SALES_QUOTE_DETAILS([Sq_Id],[item_id],[Scheme_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],
                                    [Gross_Amount],[Net_Amount],[Priority],[Status],[instance_id],Description,Converted_From,Converted_Id)
                                    values (@Sq_Id,@item_id,@Scheme_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Priority,@Status,@instance_id,@Desc,@Converted_From,@Srd_Id)";
                            db.CleanupParameters();
                            db.CreateParameters(16);
                            db.AddParameters(0, "@Sq_Id", identity);
                            db.AddParameters(1, "@Srd_Id", item.RequestDetailId);
                            db.AddParameters(2, "@item_id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Mrp", prod.MRP);
                            db.AddParameters(6, "@Rate", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", prod.Gross);
                            db.AddParameters(10, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(11, "@Priority", this.Priority);
                            db.AddParameters(12, "@Status", 0);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.AddParameters(14, "@Desc", item.Description);
                            db.AddParameters(15, "@Converted_From", item.ConvertedFrom);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                            //query = @"update TBL_SALES_REQUEST_DETAILS set Status=1 where Srd_Id=@srd_id;
                            //        declare @registerId int,@total int,@totalQouted int; 
                            //        select @registerId= Sr_id from TBL_SALES_REQUEST_DETAILS where Srd_Id=@srd_id;
                            //        select @total= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId;
                            //        select @totalQouted= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId and Status=1;
                            //        if(@total=@totalQouted) 
                            //        begin update TBL_SALES_REQUEST_REGISTER set Status=1 where Sr_Id=@registerId end";
                            //db.CreateParameters(1);
                            //db.AddParameters(0, "@srd_id", item.RequestDetailId);

                            //db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }
                    }

                    decimal _NetAmount = 0;
                    dynamic Settings = Application.Settings.GetFeaturedSettings();

                    if (Settings.IsDiscountEnabled)
                    {

                        this.NetAmount = this.NetAmount - this.Discount;
                    }
                    else
                    {
                        this.Discount = 0;
                    }
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_QUOTE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sq_Id=" + identity);
                }

                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Quote_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SQT')[New_Order],Sq_Id from tbl_sales_quote_register where Sq_Id=" + identity);
                return new OutputMessage("Sales order saved successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Quote | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Sq_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong.Sales order could not be saved", false, Type.Others, "Sales Quote | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of sales quote register and sales quote details
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesQuote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a customer to update a request.", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (string.IsNullOrWhiteSpace((this.CustomerName)))
                {
                    return new OutputMessage("Please Enter Customer name to update a request.", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a request.", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in other transaction", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {

                    db.Open();
                    string query = @"DELETE FROM TBL_SALES_QUOTE_DETAILS where Sq_Id=@Sq_Id
                                   update [dbo].[TBL_SALES_QUOTE_REGISTER] set [Location_Id]=@Location_Id,[Customer_Id]=@Customer_Id,[Tax_Amount]=@Tax_amount,[Gross_Amount]=@Gross_Amount,
                                   [Net_Amount]=@Net_Amount,[Narration]=@Narration,[Round_Off]=@Round_Off,
                                   [modified_by]=@modified_by,[modified_date]=GETUTCDATE(),entry_date=@entry_date,
                                   [Customer_Name]=@CustomerName,[Customer_Address]=@CustomerAddress,[customer_Phone]=@ContactNo,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,  
                                   TandC=@TandC,Payment_Terms=@Payment_Terms,Validity=@Validity,ETA=@ETA,
                                   Contact_Name=@Contact_Name,contact_address1=@contact_address1,Contact_Address2=@Contact_Address2,
                                    Salutation=@Salutation,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                    Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email,Discount=@Discount
                                    where Sq_Id=@Sq_Id";
                    db.CreateParameters(31);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@modified_by", this.ModifiedBy);
                    db.AddParameters(8, "@entry_date", this.EntryDate);
                    db.AddParameters(9, "@Sq_Id", this.ID);
                    db.AddParameters(10, "@CustomerName", this.CustomerName);
                    db.AddParameters(11, "@CustomerAddress", this.CustomerAddress);
                    db.AddParameters(12, "@ContactNo", this.ContactNo);
                    db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(14, "@Job_Id", this.JobId);
                    db.AddParameters(15, "@TandC", this.TermsandConditon);
                    db.AddParameters(16, "@Payment_Terms", this.Payment_Terms);
                    db.AddParameters(17, "@Validity", this.Validity);
                    db.AddParameters(18, "@ETA", this.ETA);
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
                    db.AddParameters(30, "@Discount", this.Discount);
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            if (prod.TrackInventory == true) //get the track inventory in Getpricewithscheme function
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Quote|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            if (setting.AllowPriceEditingInSalesOrder)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            //if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                            //{
                            //    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Quote|Update", System.Net.HttpStatusCode.InternalServerError);
                            //}
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"INSERT INTO TBL_SALES_QUOTE_DETAILS([Sq_Id],[Srd_Id],[item_id],[Scheme_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],
                                    [Gross_Amount],[Net_Amount],[Priority],[Status],[instance_id],Description)
                                    values (@Sq_Id,@Srd_Id,@item_id,@Scheme_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Priority,@Status,@instance_id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(15);
                            db.AddParameters(0, "@Srd_Id", item.DetailsID);
                            db.AddParameters(1, "@Sq_Id", this.ID);
                            db.AddParameters(2, "@item_id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Mrp", prod.MRP);
                            db.AddParameters(6, "@Rate", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", prod.Gross);
                            db.AddParameters(10, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(11, "@Priority", this.Priority);
                            db.AddParameters(12, "@Status", this.Status);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.AddParameters(14, "@Description", item.Description);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                            query = @"update TBL_SALES_REQUEST_DETAILS set Status=1 where Srd_Id=@srd_id;
                                    declare @registerId int,@total int,@totalQouted int; 
                                    select @registerId= sr_id from TBL_SALES_REQUEST_DETAILS where Srd_Id=@srd_id;
                                    select @total= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId;
                                    select @totalQouted= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId and Status=1;
                                    if(@total=@totalQouted) 
                                    begin update TBL_SALES_REQUEST_DETAILS set Status=1 where Sr_Id=@registerId end";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@srd_id", item.RequestDetailId);

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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_QUOTE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sq_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Quote_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SQT')[New_Order],Sq_Id from tbl_sales_quote_register where Sq_Id=" + ID);
                return new OutputMessage("Sales order has been Updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Quote | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Sq_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Sales quote couldnot be updated", false, Type.Others, "Sales Quote | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }

            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// delete details of sales quote register and sales quote details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return false</returns>
        public OutputMessage Delete()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            if (HasReference(this.ID))
            {


                return new OutputMessage("Cannot delete. This request is alredy in other transaction", false, Type.Others, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_SALES_QUOTE_DETAILS where Sq_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_SALES_QUOTE_REGISTER where Sq_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Sales order deleted successfully", true, Type.NoError, "Sales Quote | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Sales order could not be deleted", false, Type.Others, "Sales Quote | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();

                }
            }

        }
        /// <summary>
        /// confirm the saved sales quotes
        /// </summary>
        /// <returns></returns>
        public OutputMessage Confirm()
        {


            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesQuote | Confirm", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Select an entry for confirming", false, Type.RequiredFields, "SalesQuote| Confirm", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "update TBL_Sales_QUOTE_REGISTER set Approved_Status=1,ApprovedBy_Id=@Modified_By,Approved_Time=getutcdate() where sq_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    db.ExecuteNonQuery(CommandType.Text, query);

                    return new OutputMessage("Sales quote request has been Confirmed", true, Type.NoError, "SalesQuote | Confirm", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "SalesQuote | Confirm", System.Net.HttpStatusCode.InternalServerError, ex);

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
                    string query = " update TBL_SALES_QUOTE_REGISTER set Approved_Status=isnull(Approved_Status,0)^1,ApprovedBy_Id=@Modified_By,Approved_Time=GETUTCDATE() where Sq_Id=@id;select Approved_Status from tbl_sales_quote_register where Sq_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    bool approvedstatus = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                    if (approvedstatus)
                    {
                        return new OutputMessage("Quote Confirmed", true, Type.NoError, "SalesQuote | ToggleConfirm", System.Net.HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return new OutputMessage("Approval Reverted", true, Type.NoError, "SalesQuote | ToggleConfirm", System.Net.HttpStatusCode.OK, false);
                    }
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "SalesQuote | ToggleConfirm", System.Net.HttpStatusCode.InternalServerError, ex);

                }
            }

        }
        /// <summary>
        /// Gets all the Sales Quote Register with list of Products
        /// </summary>
        /// <param name="LocationID">Location Id from where the quote is generated</param>
        /// <returns></returns>
        public static List<SalesQuoteRegister> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select sqr.Sq_Id [Sales_Quote_Id],sqd.Srd_Id [sales_request_detail_Id],isnull(sqr.job_id,0)[job_id],sqd.Sqd_Id,isnull(itm.Name ,0)[item_name],
                                isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,sqr.Customer_Id,sqr.Location_Id,sqr.Quote_No,sqr.Tax_Amount,isnull(sqr.[Status],0)[Status],
                                sqr.Gross_Amount,sqr.Net_Amount,isnull(sqr.Narration,0)[Narration],sqr.Round_Off,tax.Percentage [tax_percentage],sqr.ApprovedBy_Id,
                                sqr.Approved_Time,sqr.Approved_Status,sqr.Entry_Date,sqd.Scheme_Id,sqd.Qty,sqd.Mrp,
                                isnull(sqd.[Status],0)[P_Status],sqd.rate,sqd.tax_amount [p_tax_amount],sqd.item_id,sqd.instance_id,sqd.Gross_Amount [p_gross_Amount],sqd.Net_Amount [P_net_amount],
                                sqd.[Priority],isnull(loc.Name,0)[location_name],isnull(cus.Address1,0)[Cus_Address1],isnull(cus.Address2,0)[Cus_Address2],cus.Phone1[Cus_Phone],
                                loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone]
                                ,isnull(cm.Name,0)[company],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone]
                                ,sqd.Scheme_Id,cus.Email[Customer_Email],cus.Taxno1[Customer_TaxNo],loc.Reg_Id1[Location_RegistrationId],cm.Reg_Id1[Company_Registration]
								,cm.Logo[Company_Logo],cm.Email[Company_Email],sqd.description,sqr.TandC,sqr.Validity,sqr.Payment_Terms,sqr.ETA
								from TBL_SALES_QUOTE_REGISTER sqr with(nolock)
                                left join TBL_SALES_QUOTE_DETAILS sqd with(nolock) on sqd.sq_id=sqr.sq_id
                                left join TBL_TAX_MST tax with(nolock) on sqd.Tax_Id=tax.Tax_Id
                                left join TBL_LOCATION_MST loc with(nolock) on sqr.Location_Id=loc.Location_Id
                                left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sqr.Customer_Id                                                               
                                left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
                                left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sqd.Item_Id
                                where sqr.Location_Id=@Location_Id order by sqr.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesQuoteRegister> result = new List<SalesQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesQuoteRegister register = new SalesQuoteRegister();
                        register.ID = row["Sales_Quote_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Quote_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Priority = row["Priority"] != DBNull.Value ? Convert.ToBoolean(row["Priority"]) : false;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                        register.CustomerName = Convert.ToString(row["Customer"]);
                        register.CustomerTaxNo = Convert.ToString(row["Customer_TaxNo"]);
                        register.CompanyRegId = Convert.ToString(row["Company_Registration"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CustomerAddress = Convert.ToString(row["Cus_Address1"]);
                        register.CustomerEmail = Convert.ToString(row["Customer_Email"]);
                        register.ContactNo = Convert.ToString(row["Cus_Phone"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.Location = Convert.ToString(row["Location_Name"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.JobId = Convert.ToInt32(row["job_id"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Payment_Terms= Convert.ToString(row["Payment_Terms"]);
                        register.Validity= Convert.ToString(row["Validity"]);
                        register.ETA= Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Quote_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.RequestDetailId = rowItem["sales_request_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["sales_request_detail_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["item_name"]);
                            item.ItemCode = Convert.ToString(rowItem["item_code"]);
                            item.Status = Convert.ToInt32(rowItem["P_Status"]);
                            item.SellingPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["p_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["p_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["p_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["mrp"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.Description= Convert.ToString(rowItem["description"]);
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
                Application.Helper.LogException(ex, "SalesQuote | GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<SalesQuoteRegister> GetDetails(int LocationID, int? CustomerId, DateTime? from, DateTime? to)
        {


            DBManager db = new DBManager();
            try
            {

                #region query
                db.Open();
                string query = @"select sqr.Sq_Id [Sales_Quote_Id],sqd.Srd_Id [sales_request_detail_Id],sqd.Sqd_Id,isnull(itm.Name ,0)[item_name],
                               isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,sqr.Customer_Id,sqr.Location_Id,sqr.Quote_No,sqr.Tax_Amount,isnull(sqr.[Status],0)[Status],
                               sqr.Gross_Amount,sqr.Net_Amount,isnull(sqr.Narration,0)[Narration],sqr.Round_Off,tax.Percentage [tax_percentage],sqr.ApprovedBy_Id,
                               sqr.Approved_Time,sqr.Approved_Status,sqr.Entry_Date,isnull(sqd.Scheme_Id,0) Scheme_Id,sqd.Qty,sqd.Mrp,
                               isnull(sqd.[Status],0)[P_Status],sqd.rate,sqd.tax_amount [p_tax_amount],sqd.item_id,sqd.instance_id,sqd.Gross_Amount [p_gross_Amount],sqd.Net_Amount [P_net_amount],
                               sqd.[Priority],isnull(loc.Name,0)[location_name],isnull(cus.Address1,0)[Cus_Address1],isnull(cus.Address2,0)[Cus_Address2],cus.Phone1[Cus_Phone],
                               loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone],isnull(sqr.Discount,0)[Discount],
                               isnull(cm.Name,0)[company],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone]
                               ,isnull(sqd.Scheme_Id,0) Scheme_Id,isnull(sqr.Cost_Center_Id,0)[Cost_Center_Id],isnull(sqr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job] 
							    ,cus.Email[Cus_Email],isnull(sqd.Description,'') Description,sqr.Customer_Name,sqr.TandC,sqr.Validity,sqr.payment_Terms,sqr.ETA 
                               from TBL_SALES_QUOTE_REGISTER sqr with(nolock)
                               left join TBL_SALES_QUOTE_DETAILS sqd with(nolock) on sqd.sq_id=sqr.sq_id
                               left join TBL_TAX_MST tax with(nolock) on sqd.Tax_Id=tax.Tax_Id
                               left join TBL_LOCATION_MST loc with(nolock) on sqr.Location_Id=loc.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sqr.Customer_Id                                                               
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sqd.Item_Id
							   left join tbl_Fin_CostCenter cost on cost.Fcc_ID=sqr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.job_id=sqr.Job_Id
                               where sqr.Location_Id= @Location_Id {#customerfilter#} {#daterangefilter#} order by sqr.Created_Date desc";
                #endregion query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and sqr.Entry_Date>=@fromdate and sqr.Entry_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and sqr.Entry_Date>=@fromdate and sqr.Entry_Date<=@todate ");
                }
                if (CustomerId != null && CustomerId > 0)
                {
                    query = query.Replace("{#customerfilter#}", " and sqr.customer_id=@CustomerId ");
                }
                else
                {

                    query = query.Replace("{#customerfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", from);
                db.AddParameters(2, "@todate", to);
                db.AddParameters(3, "@CustomerId", CustomerId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesQuoteRegister> result = new List<SalesQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesQuoteRegister register = new SalesQuoteRegister();
                        register.ID = row["Sales_Quote_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Quote_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Priority = row["Priority"] != DBNull.Value ? Convert.ToBoolean(row["Priority"]) : false;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                        register.CustomerName = Convert.ToString(row["Customer"]);
                        register.Customer_Name = Convert.ToString(row["Customer_Name"]);
                        register.CustomerEmail = Convert.ToString(row["Cus_Email"]);
                        register.ContactNo = Convert.ToString(row["Cus_Phone"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.Location = Convert.ToString(row["Location_Name"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Validity= Convert.ToString(row["Validity"]);
                        register.Payment_Terms= Convert.ToString(row["payment_Terms"]);
                        register.ETA= Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Quote_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.RequestDetailId = rowItem["sales_request_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["sales_request_detail_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["item_name"]);
                            item.ItemCode = Convert.ToString(rowItem["item_code"]);
                            item.Status = Convert.ToInt32(rowItem["P_Status"]);
                            item.SellingPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["p_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["p_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["p_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["mrp"]) : 0;
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
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
                Application.Helper.LogException(ex, "SalesQuote | GetDetails(int LocationID , int? CustomerId, DateTime? from,DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static SalesQuoteRegister GetDetails(int Id, int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select sqr.Sq_Id [Sales_Quote_Id],sqd.Srd_Id [sales_request_detail_Id],sqd.Sqd_Id,isnull(itm.Name ,0)[item_name],
                               isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,sqr.Customer_Id,sqr.Location_Id,sqr.Quote_No,sqr.Tax_Amount,isnull(sqr.[Status],0)[Status],
                               sqr.Gross_Amount,sqr.Net_Amount,isnull(sqr.Narration,0)[Narration],sqr.Round_Off,tax.Percentage [tax_percentage],sqr.ApprovedBy_Id,
                               sqr.Approved_Time,sqr.Approved_Status,sqr.Entry_Date,sqd.Scheme_Id,sqd.Qty,sqd.Mrp,
                               isnull(sqd.[Status],0)[P_Status],sqd.rate,sqd.tax_amount [p_tax_amount],sqd.item_id,sqd.instance_id,sqd.Gross_Amount [p_gross_Amount],sqd.Net_Amount [P_net_amount],
                               sqd.[Priority],isnull(loc.Name,0)[location_name],
                               loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone]
                               ,isnull(cm.Name,0)[company],sqd.Scheme_Id,cus.Email[Customer_Email],loc.Reg_Id1[Loc_RegId],
							   cus.Taxno1[Cust_TaxNo],cm.Email[Comp_Email],sqr.State_Id[Cust_StateId],isnull(sqr.Discount,0)[Discount],
							   coun.Name[Customer_Country],st.Name[Customer_State],J.Job_Name,sqr.Country_ID,
                               isnull(sqr.Cost_Center_Id,0)[Cost_Center_Id],isnull(sqr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job],isnull(sqd.Description,'') Description 
							   ,sqr.TandC,sqr.payment_Terms,sqr.Validity,sqr.ETA,sqr.Customer_Name customerName,sqr.Customer_Address,sqr.Customer_Phone
                               ,sqr.Contact_Name,sqr.Contact_Address1,ut.Name[Unit],sqr.Contact_Address2,sqr.Contact_City,sqr.Contact_Email,sqr.Contact_Phone1,sqr.Contact_Phone2,sqr.Contact_Zipcode,sqr.Salutation
                                from TBL_SALES_QUOTE_REGISTER sqr with(nolock)
                               left join TBL_SALES_QUOTE_DETAILS sqd with(nolock) on sqd.sq_id=sqr.sq_id
                               left join TBL_TAX_MST tax with(nolock) on sqd.Tax_Id=tax.Tax_Id
                               left join TBL_LOCATION_MST loc with(nolock) on sqr.Location_Id=loc.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sqr.Customer_Id                                                               
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
							   left join TBL_COUNTRY_MST coun with(nolock) on coun.Country_Id=cus.Country_Id
							   left join TBL_STATE_MST st with(nolock) on st.State_Id=cus.State_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sqd.Item_Id
                               left join TBL_UNIT_MST ut on ut.Unit_id=itm.Unit_id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=sqr.Cost_Center_Id
	                           left join TBL_JOB_MST j on j.job_id=sqr.Job_Id
                               where sqr.Location_Id=@Location_Id and sqr.Sq_Id=@Sq_Id order by sqr.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@Sq_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    SalesQuoteRegister register = new SalesQuoteRegister();
                    register.ID = row["Sales_Quote_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Quote_Id"]) : 0;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.Priority = row["Priority"] != DBNull.Value ? Convert.ToBoolean(row["Priority"]) : false;
                    register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                    register.TaxAmount = row["Tax_amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0;
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                    register.Status = Convert.ToInt32(row["Status"]);
                    register.CustomerEmail= Convert.ToString(row["Customer_Email"]);
                    register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                    register.CustomerTaxNo = Convert.ToString(row["Cust_TaxNo"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    //register.CustStateId = Convert.ToInt32(row["Cust_StateId"]);
                    register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                    register.Company = Convert.ToString(row["Company"]);
                    register.Location = Convert.ToString(row["Location_Name"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                    register.JobName = row["Job_Name"] != DBNull.Value ? Convert.ToString(row["Job_Name"]) : "";
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                    register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                    register.TermsandConditon= Convert.ToString(row["TandC"]);
                    register.Payment_Terms= Convert.ToString(row["payment_Terms"]);
                    register.Validity= Convert.ToString(row["Validity"]);
                    register.ETA= Convert.ToString(row["ETA"]);
                    register.Customer_Name= Convert.ToString(row["customerName"]);
                    List<Entities.Master.Address> addresslist = new List<Master.Address>();
                    Entities.Master.Address address = new Master.Address();
                    address.ContactName = Convert.ToString(row["Contact_Name"]);
                    address.Address1 = Convert.ToString(row["Contact_Address1"]);
                    address.Address2 = Convert.ToString(row["Contact_Address2"]);
                    address.Email = Convert.ToString(row["Contact_Email"]);
                    address.City = Convert.ToString(row["Contact_City"]);
                    address.Phone1 = Convert.ToString(row["Contact_Phone1"]);
                    address.Phone2 = Convert.ToString(row["Contact_Phone2"]);
                    address.Zipcode = Convert.ToString(row["Contact_Zipcode"]);
                    address.Salutation = Convert.ToString(row["Salutation"]);
                    address.State = Convert.ToString(row["Customer_State"]);
                    address.Country = Convert.ToString(row["Customer_Country"]);
                    address.CountryID = row["Country_ID"] != DBNull.Value ? Convert.ToInt32(row["Country_ID"]) : 0;
                    address.StateID = row["Cust_StateId"] != DBNull.Value ? Convert.ToInt32(row["Cust_StateId"]) : 0;
                    addresslist.Add(address);
                    register.BillingAddress = addresslist;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Quote_Id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.DetailsID = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                        item.RequestDetailId = rowItem["sales_request_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["sales_request_detail_Id"]) : 0;
                        item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                        item.Name = Convert.ToString(rowItem["item_name"]);
                        item.ItemCode = Convert.ToString(rowItem["item_code"]);
                        item.Unit = Convert.ToString(rowItem["Unit"]);
                        item.Status = Convert.ToInt32(rowItem["P_Status"]);
                        item.SellingPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Gross = rowItem["p_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["p_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["p_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                        item.MRP = rowItem["mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["mrp"]) : 0;
                        item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                        item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                        item.Description= Convert.ToString(rowItem["Description"]);
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
                Application.Helper.LogException(ex, "SalesQuote |  GetDetails(int Id,int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// check for the sales quote is refered in another transactions
        /// </summary>
        /// <param name="salesQuoteid (id)"></param>
        /// <returns>bool which either have refernce or not</returns>
        public static List<SalesQuoteRegister> GetDetailsForConfirm(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select sq.Sq_Id,sq.Location_Id,sq.Customer_Id,sq.Quote_No,sq.Tax_Amount,sq.Gross_Amount,sqd.item_id,isnull(sqd.instance_id,0) instance_id,sq.Net_Amount,isnull(sq.Approved_Status,0)[Approved_Status],
                               sq.Entry_Date,sqd.Sqd_Id,sqd.Srd_Id,sqd.Qty,sqd.Mrp,sqd.Rate,sqd.Tax_Amount [p_tax_amount],sqd.Gross_Amount [p_gross_amount],sqd.Net_Amount [p_Net_Amount],
                               l.Name[Location],c.Name[Customer],isnull(sq.Discount,0)[Discount],it.Name[Item],it.Item_Id,it.Item_Code,tx.Percentage[Tax_Percentage] 
                               from tbl_sales_quote_register sq with(nolock)
                               left join tbl_sales_quote_details sqd with(nolock) on sqd.Sq_Id=sq.Sq_Id
                               left join tbl_location_mst l with(nolock) on l.Location_Id=sq.Location_Id
                               left join tbl_customer_mst c with(nolock) on c.Customer_Id=sq.Customer_Id                              
                               left join tbl_item_mst it with(nolock) on it.Item_Id=sqd.Item_Id
                               left join tbl_tax_mst tx with(nolock) on tx.Tax_Id=sqd.Tax_Id
                              where sq.Location_Id=@Location_Id  order by sq.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesQuoteRegister> result = new List<SalesQuoteRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesQuoteRegister register = new SalesQuoteRegister();
                        register.ID = row["Sq_Id"] != DBNull.Value ? Convert.ToInt32(row["Sq_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.QuoteNumber = Convert.ToString(row["Quote_No"]);
                        register.EntryDate = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]) : DateTime.MinValue;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0;
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Location = Convert.ToString(row["Location"]);
                        register.CustomerName = Convert.ToString(row["Customer"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sq_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sqd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sqd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.SellingPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
                            item.TaxAmount = rowItem["p_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.Gross = rowItem["p_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["p_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
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
                Application.Helper.LogException(ex, "SalesQuote |  GetDetailsForConfirm(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


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
                    string query = @"select count(*) from TBL_SALES_ENTRY_DETAILS sed with(nolock) join
                                     TBL_SALES_quote_DETAILS q with(nolock) on q.Sqd_Id=sed.Sqd_Id
                                     join  TBL_sales_QUOTE_REGISTER sqr with(nolock) on q.Sq_Id=sqr.Sq_Id where
                                     sqr.sq_id=@id ";
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
                    Application.Helper.LogException(ex, "SalesQuote |  HasReference(int id)");
                    throw;
                }
            }
        }


        public static OutputMessage SendMail(int SalesId, string toAddress, int userId, string url)
        {


            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesQuote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Sales Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                string QuotationNo = "";
                string query = "select Quote_No from TBL_SALES_QUOTE_REGISTER where Sq_Id=@Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Id", SalesId);
                db.Open();
                QuotationNo = Convert.ToString(db.ExecuteScalar(CommandType.Text, query));
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
                mail.Subject = "Quotation";
                mail.Body = "Please Find the attached copy of Quotation";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Quotation "+QuotationNo+".pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "Sales Quote | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Sales Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        //no use
        //public static List<Item> GetItemsFromPurchaseWithScheme(int CustomerId, int LocationId)
        //{
        //    DBManager db = new DBManager();
        //    db.Open();
        //    DataTable dt = db.ExecuteDataSet(System.Data.CommandType.Text, @"[dbo].[USP_SEARCH_PRODUCTS_FROMSCHEME] ''," + CustomerId + "," + LocationId).Tables[0];
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
    }
}
