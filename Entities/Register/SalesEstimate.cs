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
     public class SalesEstimate: Register, IRegister
    {
        #region Properties
        public int RequestNo { get; set; }
        public int SchemeId { get; set; }
        public int CustomerId { get; set; }
        public string EstimateNumber { get; set; }
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
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public int Status { get; set; }
        #endregion Properties
        public SalesEstimate(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public SalesEstimate()
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
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Estimate | SaveOrUpdate", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a customer to make a request.", false, Type.Others, "Sales Estimate | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (string.IsNullOrWhiteSpace((this.CustomerName)))
                {
                    return new OutputMessage("Please enter customer name to make a request", false, Type.Others, "Sales Estimate | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a request.", false, Type.Others, "Sales Estimate | Save", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {

                    db.Open();
                    string query = @"INSERT INTO [dbo].[TBL_SALES_ESTIMATE_REGISTER]([Location_Id],[Customer_Id],[Estimate_No],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Narration],[Round_Off],
                                   [Approved_Status],[Status],[Created_By],[Created_Date],entry_date,[customer_name],[customer_Address],[customer_Phone],Cost_Center_Id,Job_Id,TandC,Payment_Terms,Validity,ETA
                                   ,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email,Discount)
                                   VALUES(@Location_Id,@Customer_Id,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','EST'),@Tax_Amount,@Gross_Amount, @Net_Amount,@Narration,@Round_Off,@Approved_Status,@Status,@Created_By,GETUTCDATE(),@entry_date,@CustomerName,@CustomerAddress,@ContactNo,@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@Validity,@ETA,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email,@Discount);select @@identity";
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
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "EST", db);

                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError);
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
                            if (setting.AllowPriceEditingInSalesOrder)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            //if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock<item.Quantity)
                            //{
                            //    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Estimate|Save", System.Net.HttpStatusCode.InternalServerError);

                            //}
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"INSERT INTO TBL_SALES_ESTIMATE_DETAILS([Se_Id],[item_id],[Scheme_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],
                                    [Gross_Amount],[Net_Amount],[Priority],[Status],[instance_id],Description)
                                    values (@Se_Id,@item_id,@Scheme_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Priority,@Status,@instance_id,@Desc)";
                            db.CleanupParameters();
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Se_Id", identity);
                            //db.AddParameters(1, "@Srd_Id", item.RequestDetailId);
                            db.AddParameters(1, "@item_id", item.ItemID);
                            db.AddParameters(2, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.SellingPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Priority", this.Priority);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@instance_id", item.InstanceId);
                            db.AddParameters(13, "@Desc", item.Description);
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_ESTIMATE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Estimate_Id=" + identity);
                }

                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Estimate_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','EST')[New_Order],Estimate_Id from tbl_sales_Estimate_register where Estimate_Id=" + identity);
                return new OutputMessage("Sales estimate saved successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Estimate | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Estimate_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong.Sales estimate could not be saved", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of sales estimate register and sales estimate details
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesEstimate | SaveOrUpdate", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a customer to update a request.", false, Type.Others, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (string.IsNullOrWhiteSpace((this.CustomerName)))
                {
                    return new OutputMessage("Please Enter Customer name to update a request.", false, Type.Others, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a request.", false, Type.Others, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                //else if (HasReference(this.ID))
                //{
                //    db.RollBackTransaction();

                //    return new OutputMessage("Cannot Update. This request is alredy in other transaction", false, Type.Others, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError);

                //}
                else
                {

                    db.Open();
                    string query = @"DELETE FROM TBL_SALES_ESTIMATE_DETAILS where Se_Id=@Se_Id
                                   update [dbo].[TBL_SALES_ESTIMATE_REGISTER] set [Location_Id]=@Location_Id,[Customer_Id]=@Customer_Id,[Tax_Amount]=@Tax_amount,[Gross_Amount]=@Gross_Amount,
                                   [Net_Amount]=@Net_Amount,[Narration]=@Narration,[Round_Off]=@Round_Off,
                                   [modified_by]=@modified_by,[modified_date]=GETUTCDATE(),entry_date=@entry_date,
                                   [Customer_Name]=@CustomerName,[Customer_Address]=@CustomerAddress,[customer_Phone]=@ContactNo,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id   
                                   ,TandC=@TandC,Payment_Terms=@Payment_Terms,Validity=@Validity,ETA=@ETA
                                   ,Contact_Name=@Contact_Name,contact_address1=@contact_address1,Contact_Address2=@Contact_Address2,
                                    Salutation=@Salutation,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                    Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email,Discount=@Discount
                                    where Estimate_Id=@Estimate_Id";
                    db.CreateParameters(32);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@modified_by", this.ModifiedBy);
                    db.AddParameters(8, "@entry_date", this.EntryDate);
                    db.AddParameters(9, "@Estimate_Id", this.ID);
                    db.AddParameters(10, "@CustomerName", this.CustomerName);
                    db.AddParameters(11, "@CustomerAddress", this.CustomerAddress);
                    db.AddParameters(12, "@ContactNo", this.ContactNo);
                    db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(14, "@Job_Id", this.JobId);
                    db.AddParameters(15, "@Se_Id", this.ID);
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
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            if (prod.TrackInventory == true) //get the track inventory in Getpricewithscheme function
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Entry|Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            if (setting.AllowPriceEditingInSalesOrder)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            //if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                            //{
                            //    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Sales Estimate|Update", System.Net.HttpStatusCode.InternalServerError);
                            //}
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"INSERT INTO TBL_SALES_ESTIMATE_DETAILS([Se_Id],[item_id],[Scheme_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],
                                    [Gross_Amount],[Net_Amount],[Priority],[Status],[instance_id],Description)
                                    values (@Se_Id,@item_id,@Scheme_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Priority,@Status,@instance_id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(14);
                            //db.AddParameters(0, "@Srd_Id", item.DetailsID);
                            db.AddParameters(0, "@Se_Id", this.ID);
                            db.AddParameters(1, "@item_id", item.ItemID);
                            db.AddParameters(2, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.SellingPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Priority", this.Priority);
                            db.AddParameters(11, "@Status", this.Status);
                            db.AddParameters(12, "@instance_id", item.InstanceId);
                            db.AddParameters(13, "@Description", item.Description);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                            //query = @"update TBL_SALES_REQUEST_DETAILS set Status=1 where Srd_Id=@srd_id;
                            //        declare @registerId int,@total int,@totalQouted int; 
                            //        select @registerId= sr_id from TBL_SALES_REQUEST_DETAILS where Srd_Id=@srd_id;
                            //        select @total= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId;
                            //        select @totalQouted= count(*) from TBL_SALES_REQUEST_DETAILS where Sr_Id=@registerId and Status=1;
                            //        if(@total=@totalQouted) 
                            //        begin update TBL_SALES_REQUEST_DETAILS set Status=1 where Sr_Id=@registerId end";
                            //db.CreateParameters(1);
                            //db.AddParameters(0, "@srd_id", item.RequestDetailId);

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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_ESTIMATE_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Estimate_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Estimate_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','EST')[New_Order],Estimate_Id from tbl_sales_estimate_register where Estimate_Id=" + ID);
                return new OutputMessage("Sales estimate has been Updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Estimate | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Estimate_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Sales estimate couldnot be updated", false, Type.Others, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }

            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// delete details of sales estimate register and sales estimate details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return false</returns>
        public OutputMessage Delete()
        {

            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            //if (HasReference(this.ID))
            //{


            //    return new OutputMessage("Cannot delete. This request is alredy in other transaction", false, Type.Others, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError);

            //}
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_SALES_ESTIMATE_DETAILS where Se_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_SALES_ESTIMATE_REGISTER where Estimate_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Sales estimate deleted successfully", true, Type.NoError, "Sales Estimate | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Sales estimate could not be deleted", false, Type.Others, "Sales Estimate | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();

                }
            }

        }
        /// <summary>
        /// confirm the saved sales estimates
        /// </summary>
        /// <returns></returns>
        public OutputMessage Confirm()
        {


            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Estimate | Confirm", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Select an entry for confirming", false, Type.RequiredFields, "Sales Estimate| Confirm", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "update TBL_SALES_ESTIMATE_REGISTER set Approved_Status=1,ApprovedBy_Id=@Modified_By,Approved_Time=getutcdate() where Estimate_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    db.ExecuteNonQuery(CommandType.Text, query);

                    return new OutputMessage("Sales estimate has been Confirmed", true, Type.NoError, "Sales Estimate | Confirm", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Estimate | Confirm", System.Net.HttpStatusCode.InternalServerError, ex);

                }
            }

        }
        public OutputMessage ToggleConfirm()
        {


            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Estimate | Confirm", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for Confirming", false, Type.RequiredFields, "Sales Estimate| Confirm", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = " update TBL_SALES_ESTIMATE_REGISTER set Approved_Status=isnull(Approved_Status,0)^1,ApprovedBy_Id=@Modified_By,Approved_Time=GETUTCDATE() where Estimate_Id=@id;select Approved_Status from tbl_sales_estimate_register where Estimate_Id=@id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.ID);
                    db.AddParameters(1, "@Modified_By", this.ModifiedBy);
                    db.Open();

                    bool approvedstatus = Convert.ToBoolean(db.ExecuteScalar(CommandType.Text, query));
                    if (approvedstatus)
                    {
                        return new OutputMessage("Estimate Confirmed", true, Type.NoError, "Sales Estimate | Confirm", System.Net.HttpStatusCode.OK, true);
                    }
                    else
                    {
                        return new OutputMessage("Approval Reverted", true, Type.NoError, "Sales Estimate | Confirm", System.Net.HttpStatusCode.OK, false);
                    }
                }
                catch (Exception ex)
                {


                    db.Close();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Estimate | Confirm", System.Net.HttpStatusCode.InternalServerError, ex);

                }
            }

        }
        /// <summary>
        /// Gets all the Sales estimate Register with list of Products
        /// </summary>
        /// <param name="LocationID">Location Id from where the estimate is generated</param>
        /// <returns></returns>
        public static List<SalesEstimate> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select ser.Estimate_Id [Sales_Estimate_Id],sed.Srd_Id [sales_request_detail_Id],Sed.Sq_Id,isnull(itm.Name ,0)[item_name],
                                isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,ser.Customer_Id,ser.Location_Id,ser.Estimate_No,ser.Tax_Amount,isnull(ser.[Status],0)[Status],
                                ser.Gross_Amount,ser.Net_Amount,isnull(ser.Narration,0)[Narration],ser.Round_Off,tax.Percentage [tax_percentage],ser.ApprovedBy_Id,
                                ser.Approved_Time,ser.Approved_Status,ser.Entry_Date,sed.Scheme_Id,sed.Qty,sed.Mrp,
                                isnull(sed.[Status],0)[P_Status],sed.rate,sed.tax_amount [p_tax_amount],sed.item_id,sed.instance_id,sed.Gross_Amount [p_gross_Amount],sed.Net_Amount [P_net_amount],
                                sed.[Priority],isnull(loc.Name,0)[location_name],isnull(cus.Address1,0)[Cus_Address1],isnull(cus.Address2,0)[Cus_Address2],cus.Phone1[Cus_Phone],
                                loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone]
                                ,isnull(cm.Name,0)[company],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone]
                                ,sed.Scheme_Id,cus.Email[Customer_Email],cus.Taxno1[Customer_TaxNo],loc.Reg_Id1[Location_RegistrationId],cm.Reg_Id1[Company_Registration]
								,cm.Logo[Company_Logo],cm.Email[Company_Email]
								from TBL_SALES_ESTIMATE_REGISTER ser with(nolock)
                                left join TBL_SALES_ESTIMATE_DETAILS sed with(nolock) on sed.Sq_Id=ser.Estimate_Id
                                left join TBL_TAX_MST tax with(nolock) on sed.Tax_Id=tax.Tax_Id
                                left join TBL_LOCATION_MST loc with(nolock) on ser.Location_Id=loc.Location_Id
                                left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=ser.Customer_Id                                                               
                                left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
                                left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sed.Item_Id
                                where ser.Location_Id=@Location_Id order by ser.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEstimate> result = new List<SalesEstimate>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEstimate register = new SalesEstimate();
                        register.ID = row["Sales_Estimate_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Estimate_Id"]) : 0;
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
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        register.EstimateNumber = Convert.ToString(row["Estimate_No"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Estimate_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEstimate | GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<SalesEstimate> GetDetails(int LocationID, int? CustomerId, DateTime? from, DateTime? to)
        {


            DBManager db = new DBManager();
            try
            {

                #region query
                db.Open();
          //      string query = @"select ser.Se_Id [Sales_Estimate_Id],sed.Srd_Id [sales_request_detail_Id],sed.Sed_Id,isnull(itm.Name ,0)[item_name],
          //                     isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,ser.Customer_Id,ser.Location_Id,ser.Estimate_No,ser.Tax_Amount,isnull(ser.[Status],0)[Status],
          //                     ser.Gross_Amount,ser.Net_Amount,isnull(ser.Narration,0)[Narration],ser.Round_Off,tax.Percentage [tax_percentage],ser.ApprovedBy_Id,
          //                     ser.Approved_Time,ser.Approved_Status,ser.Entry_Date,isnull(sed.Scheme_Id,0) Scheme_Id,sed.Qty,sed.Mrp,
          //                     isnull(sed.[Status],0)[P_Status],sed.rate,sed.tax_amount [p_tax_amount],sed.item_id,sed.instance_id,sed.Gross_Amount [p_gross_Amount],sed.Net_Amount [P_net_amount],
          //                     sed.[Priority],isnull(loc.Name,0)[location_name],isnull(cus.Address1,0)[Cus_Address1],isnull(cus.Address2,0)[Cus_Address2],cus.Phone1[Cus_Phone],
          //                     loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone]
          //                     ,isnull(cm.Name,0)[company],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],isnull(cm.Mobile_No1,0)[Comp_Phone]
          //                     ,isnull(sed.Scheme_Id,0) Scheme_Id,isnull(ser.Cost_Center_Id,0)[Cost_Center_Id],isnull(ser.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job] 
							   // ,cus.Email[Cus_Email] from TBL_SALES_ESTIMATE_REGISTER ser with(nolock)
          //                     left join TBL_SALES_ESTIMATE_DETAILS sed with(nolock) on sed.se_id=ser.se_id
          //                     left join TBL_TAX_MST tax with(nolock) on sed.Tax_Id=tax.Tax_Id
          //                     left join TBL_LOCATION_MST loc with(nolock) on ser.Location_Id=loc.Location_Id
          //                     left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=ser.Customer_Id                                                               
          //                     left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
          //                     left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sed.Item_Id
							   //left join tbl_Fin_CostCenter cost on cost.Fcc_ID=ser.Cost_Center_Id
							   //left join TBL_JOB_MST j on j.job_id=ser.Job_Id
          //                     where ser.Location_Id= @Location_Id {#customerfilter#} {#daterangefilter#} order by ser.Created_Date desc";


                string query = @" select ser.Estimate_Id[Sales_Estimate_Id],sed.Sed_Id[sales_request_detail_Id],sed.Sed_Id,isnull(itm.Name, 0)[item_name],
                               isnull(cus.Name, 0)[Customer],isnull(itm.Item_Code, 0) Item_Code,ser.Customer_Id,ser.Location_Id,ser.Estimate_No,ser.Tax_Amount,isnull(ser.[Status], 0)[Status],
                               ser.Gross_Amount,ser.Net_Amount,isnull(ser.Narration, 0)[Narration],ser.Round_Off,tax.Percentage[tax_percentage],ser.ApprovedBy_Id,
                               ser.Approved_Time,ser.Approved_Status,ser.Entry_Date,isnull(sed.Scheme_Id, 0) Scheme_Id,sed.Qty,sed.Mrp,
                               isnull(sed.[Status], 0)[P_Status],sed.rate,sed.tax_amount[p_tax_amount],sed.item_id,sed.instance_id,sed.Gross_Amount[p_gross_Amount],sed.Net_Amount[P_net_amount],
                               sed.[Priority],isnull(loc.Name, 0)[location_name],isnull(cus.Address1, 0)[Cus_Address1],isnull(cus.Address2, 0)[Cus_Address2],cus.Phone1[Cus_Phone],
                               loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone],isnull(ser.Discount,0)[Discount]
                               ,isnull(cm.Name, 0)[company],isnull(cm.Address1, 0)[Comp_Address1],isnull(cm.Address2, 0)[Comp_Address2],isnull(cm.Mobile_No1, 0)[Comp_Phone]
                               ,isnull(sed.Scheme_Id, 0) Scheme_Id,isnull(ser.Cost_Center_Id, 0)[Cost_Center_Id],isnull(ser.Job_Id, 0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job]
							    ,cus.Email[Cus_Email],sed.Description,ser.TandC,ser.Validity,ser.Payment_Terms,ser.ETA 
                                from TBL_SALES_ESTIMATE_REGISTER ser with(nolock)
                               left join TBL_SALES_ESTIMATE_DETAILS sed with(nolock) on sed.se_id = ser.Estimate_Id
                               left join TBL_TAX_MST tax with(nolock) on sed.Tax_Id = tax.Tax_Id
                               left join TBL_LOCATION_MST loc with(nolock) on ser.Location_Id = loc.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id = ser.Customer_Id
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id = loc.Company_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id = sed.Item_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID = ser.Cost_Center_Id
                               left join TBL_JOB_MST j on j.job_id = ser.Job_Id
                               where ser.Location_Id = @Location_Id  {#customerfilter#} {#daterangefilter#} order by ser.Created_Date desc";
                #endregion query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and ser.Entry_Date>=@fromdate and ser.Entry_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and ser.Entry_Date>=@fromdate and ser.Entry_Date<=@todate ");
                }
                if (CustomerId != null && CustomerId > 0)
                {
                    query = query.Replace("{#customerfilter#}", " and ser.customer_id=@CustomerId ");
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
                    List<SalesEstimate> result = new List<SalesEstimate>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEstimate register = new SalesEstimate();
                        register.ID = row["Sales_Estimate_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Estimate_Id"]) : 0;
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
                        register.CustomerName = Convert.ToString(row["Customer"]);
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
                        register.EstimateNumber = Convert.ToString(row["Estimate_No"]);
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Payment_Terms= Convert.ToString(row["Payment_Terms"]);
                        register.Validity= Convert.ToString(row["Validity"]);
                        register.ETA= Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Estimate_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEstimate | GetDetails(int LocationID , int? CustomerId, DateTime? from,DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static SalesEstimate GetDetails(int Id, int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select ser.Estimate_Id [Sales_Estimate_Id],sed.Sed_Id,isnull(itm.Name ,0)[item_name],
                               isnull(cus.Name,0)[Customer],isnull(itm.Item_Code,0) Item_Code,ser.Customer_Id,ser.Location_Id,ser.Estimate_No,ser.Tax_Amount,isnull(ser.[Status],0)[Status],
                               ser.Gross_Amount,ser.Net_Amount,isnull(ser.Narration,0)[Narration],ser.Round_Off,tax.Percentage [tax_percentage],ser.ApprovedBy_Id,
                               ser.Approved_Time,ser.Approved_Status,ser.Entry_Date,sed.Scheme_Id,sed.Qty,sed.Mrp,
                               isnull(sed.[Status],0)[P_Status],sed.rate,sed.tax_amount [p_tax_amount],sed.item_id,sed.instance_id,sed.Gross_Amount [p_gross_Amount],sed.Net_Amount [P_net_amount],
                               sed.[Priority],isnull(loc.Name,0)[location_name],isnull(cus.Address1,0)[Cus_Address1],isnull(cus.Address2,0)[Cus_Address2],cus.Phone1[Cus_Phone],
                               loc.Address1[Loc_Address1],loc.Address2[Loc_Address2],loc.Contact[Loc_Phone]
                               ,isnull(cm.Name,0)[company],sed.Scheme_Id,cus.Email[Customer_Email],loc.Reg_Id1[Loc_RegId],
							   cus.Taxno1[Cust_TaxNo],cm.Email[Comp_Email],ser.State_Id[Cust_StateId],ser.Country_ID,
							   coun.Name[Customer_Country],st.Name[Customer_State],
                               isnull(ser.Cost_Center_Id,0)[Cost_Center_Id],isnull(ser.Job_Id,0)[Job_Id],
							   cost.Fcc_Name[Cost_Center],j.Job_Name[Job],sed.Description,isnull(ser.Discount,0)[Discount],
							   ser.TandC,ser.Payment_Terms,ut.Name[Unit],ser.Validity,ser.ETA,ser.Customer_Address[Est_Cust_Addr],ser.Customer_Name[Est_Cust_Name],ser.Customer_Phone[Est_Cust_No],
							   ser.Contact_Name,ser.Contact_Address1,ser.Contact_Address2,ser.Contact_City,ser.Contact_Email,ser.Contact_Phone1,ser.Contact_Phone2,ser.Contact_Zipcode,ser.Salutation
                               from TBL_SALES_ESTIMATE_REGISTER ser with(nolock)
                               left join TBL_SALES_ESTIMATE_DETAILS sed with(nolock) on sed.se_id=ser.Estimate_Id
                               left join TBL_TAX_MST tax with(nolock) on sed.Tax_Id=tax.Tax_Id
                               left join TBL_LOCATION_MST loc with(nolock) on ser.Location_Id=loc.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=ser.Customer_Id                                                               
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=loc.Company_Id
							   left join TBL_COUNTRY_MST coun with(nolock) on coun.Country_Id=ser.Country_Id
							   left join TBL_STATE_MST st with(nolock) on st.State_Id=ser.State_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sed.Item_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=ser.Cost_Center_Id
	                           left join TBL_JOB_MST j on j.job_id=ser.Job_Id
                               left join TBL_UNIT_MST ut on ut.Unit_id=itm.Unit_id
                               where ser.Location_Id=@Location_Id and ser.Estimate_Id=@Estimate_Id order by ser.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@Estimate_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    SalesEstimate register = new SalesEstimate();
                    register.ID = row["Sales_Estimate_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Estimate_Id"]) : 0;
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
                    register.Status = row["Status"]!=DBNull.Value? Convert.ToInt32(row["Status"]):0;
                    register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                    register.CustomerName = Convert.ToString(row["Est_Cust_Name"]);
                    register.CustomerTaxNo = Convert.ToString(row["Cust_TaxNo"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.CustomerState = Convert.ToString(row["Customer_State"]);
                    register.CustomerCountry = Convert.ToString(row["Customer_Country"]);
                    register.CustStateId = row["Cust_StateId"]!=DBNull.Value? Convert.ToInt32(row["Cust_StateId"]):0;
                    register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                    register.CustomerEmail = Convert.ToString(row["Customer_Email"]);
                    register.CustomerAddress = Convert.ToString(row["Est_Cust_Addr"]);
                    register.ContactNo = Convert.ToString(row["Est_Cust_No"]);
                    register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                    register.Company = Convert.ToString(row["Company"]);
                    register.Location = Convert.ToString(row["Location_Name"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                    register.EstimateNumber = Convert.ToString(row["Estimate_No"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    register.Validity = Convert.ToString(row["Validity"]);
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
                    address.State = Convert.ToString(row["Customer_State"]);
                    address.Country = Convert.ToString(row["Customer_Country"]);
                    address.CountryID = row["Country_ID"] != DBNull.Value ? Convert.ToInt32(row["Country_ID"]) : 0;
                    address.StateID = row["Cust_StateId"] != DBNull.Value ? Convert.ToInt32(row["Cust_StateId"]) : 0;
                    addresslist.Add(address);
                    register.BillingAddress = addresslist;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Estimate_Id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
                        //item.RequestDetailId = rowItem["sales_request_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["sales_request_detail_Id"]) : 0;
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
                Application.Helper.LogException(ex, "Sales Estimate |  GetDetails(int Id,int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// check for the sales estimate is refered in another transactions
        /// </summary>
        /// <param name="salesEstimateid (id)"></param>
        /// <returns>bool which either have refernce or not</returns>
        public static List<SalesEstimate> GetDetailsForConfirm(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @" select se.Estimate_Id,se.Location_Id,se.Customer_Id,se.Estimate_No,se.Tax_Amount,se.Gross_Amount,sed.item_id,isnull(sed.instance_id,0) instance_id,se.Net_Amount,isnull(se.Approved_Status,0)[Approved_Status],
                               se.Entry_Date,sed.Sed_Id,sed.Qty,sed.Mrp,sed.Rate,sed.Tax_Amount [p_tax_amount],sed.Gross_Amount [p_gross_amount],sed.Net_Amount [p_Net_Amount],
                               l.Name[Location],c.Name[Customer],isnull(se.Discount,0)[Discount],it.Name[Item],it.Item_Id,it.Item_Code,tx.Percentage[Tax_Percentage] 
                               from tbl_sales_estimate_register se with(nolock)
                               left join tbl_sales_estimate_details sed with(nolock) on sed.Se_Id=se.Estimate_Id
                               left join tbl_location_mst l with(nolock) on l.Location_Id=se.Location_Id
                               left join tbl_customer_mst c with(nolock) on c.Customer_Id=se.Customer_Id                              
                               left join tbl_item_mst it with(nolock) on it.Item_Id=sed.Item_Id
                               left join tbl_tax_mst tx with(nolock) on tx.Tax_Id=sed.Tax_Id
                              where se.Location_Id=@Location_Id  order by se.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesEstimate> result = new List<SalesEstimate>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesEstimate register = new SalesEstimate();
                        register.ID = row["Se_Id"] != DBNull.Value ? Convert.ToInt32(row["Se_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.EstimateNumber = Convert.ToString(row["Estimate_No"]);
                        register.EntryDate = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]) : DateTime.MinValue;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Discount = row["Discount"] != DBNull.Value ? Convert.ToDecimal(row["Discount"]) : 0;
                        register.ApprovedStatus = row["Approved_Status"] != DBNull.Value ? Convert.ToInt32(row["Approved_Status"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Location = Convert.ToString(row["Location"]);
                        register.CustomerName = Convert.ToString(row["Customer"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Se_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Sed_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sed_Id"]) : 0;
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
                Application.Helper.LogException(ex, "SalesEstimate |  GetDetailsForConfirm(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public static OutputMessage SendMail(int SalesId, string toAddress, int userId, string url)
        {


            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.SalesEstimate, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Sales Estimate | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Sales Estimate | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                mail.Subject = "Estimate";
                mail.Body = "Please Find the attached copy of Estimate";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Estimate.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "Sales Estimate | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Sales Estimate | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

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
        //            string query = @"select count(*) from TBL_SALES_ENTRY_DETAILS sed with(nolock) join
        //                             TBL_SALES_quote_DETAILS q with(nolock) on q.Sqd_Id=sed.Sqd_Id
        //                             join  TBL_sales_ESTIMATE_REGISTER sqr with(nolock) on q.Sq_Id=sqr.Sq_Id where
        //                             sqr.sq_id=@id ";
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
        //        catch (Exception ex)
        //        {
        //            Application.Helper.LogException(ex, "SalesQuote |  HasReference(int id)");
        //            throw;
        //        }
        //    }
        //}
        //public static OutputMessage SendMail(int SalesId, string toAddress, int userId, string url)
        //{


        //    if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.SalesQuote, Security.PermissionTypes.Update))
        //    {
        //        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesQuote | Update", System.Net.HttpStatusCode.InternalServerError);
        //    }
        //    else if (!toAddress.IsValidEmail())
        //    {
        //        return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Sales Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError);
        //    }
        //    try
        //    {
        //        DBManager db = new DBManager();

        //        //getting the email content from print page
        //        WebClient client = new WebClient();
        //        UTF8Encoding utf8 = new UTF8Encoding();
        //        string htmlMarkup = utf8.GetString(client.DownloadData(url));

        //        //creating PDF 
        //        HtmlToPdf converter = new HtmlToPdf();
        //        PdfDocument doc = converter.ConvertUrl(url);
        //        byte[] bytes;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            doc.Save(ms);
        //            bytes = ms.ToArray();
        //            ms.Close();
        //            doc.DetachStream();
        //        }

        //        //sending the mail
        //        string query = @"SELECT * from 
        //                               (SELECT keyValue AS Email_Id FROM TBL_SETTINGS where KeyID=106) AS Email_Id,
        //                               (SELECT keyValue AS Email_Password FROM TBL_SETTINGS where KeyID=107) AS Email_Password,
        //                               (SELECT keyValue AS Email_Host FROM TBL_SETTINGS where KeyID=108) AS Email_Host,
        //                               (SELECT keyValue AS Email_Port FROM TBL_SETTINGS where KeyID=109) AS Email_Port";
        //        DataTable dt = new DataTable();
        //        db.Open();
        //        dt = db.ExecuteQuery(CommandType.Text, query);
        //        MailMessage mail = new MailMessage(Convert.ToString(dt.Rows[0]["Email_Id"]), toAddress);
        //        mail.IsBodyHtml = false;
        //        mail.Subject = "Invoice";
        //        mail.Body = "Please Find the attached copy of Invoice";
        //        mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));
        //        SmtpClient smtp = new SmtpClient()
        //        {
        //            Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
        //            EnableSsl = true,
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
        //            Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

        //        };
        //        smtp.Send(mail);
        //        return new OutputMessage("Mail Send successfully", true, Type.NoError, "Sales Quote | Send Mail", System.Net.HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Sales Quote | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
        //    }

        //}

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
