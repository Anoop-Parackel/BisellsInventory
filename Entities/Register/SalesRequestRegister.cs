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
    public class SalesRequestRegister : Register, IRegister
    {
        #region Properties
        public int SupplierId { get; set; }
        public string RequestNo { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string FinancialYear { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddress2 { get; set; }
        public string ContactNo { get; set; }
        public int Status { get; set; }
        public string CompanyRegId { get; set; }
        public string LocationRegId { get; set; }
        public string CustomerTaxNo { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CustomerState { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerEmail { get; set; }
        public int CustStateId { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public int RequestStatus { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string LocationPhone { get; set; }
        public bool Priority { get; set; }
        public List<Item> Products { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public string TermsandConditon { get; set; }
        public string PaymentTerms { get; set; }
        public string Customer { get; set; }
        public string Validity { get; set; }
        public string ETA { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public override decimal NetAmount { get; set; }
        #endregion Properties
        public SalesRequestRegister(int ID)
        {
            this.ID = ID;
        }
        public SalesRequestRegister()
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
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesRequest, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesRequest | SaveOrUpdate", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.CustomerId == 0)
                {
                    return new OutputMessage("Select a customer to make a request.", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (string.IsNullOrWhiteSpace((this.CustomerName)))
                {
                    return new OutputMessage("Please Enter Customer name to make a request.", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a request.", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError);
                }
              
                else
                {

                    db.Open();
                    string query = @"insert into [TBL_SALES_REQUEST_REGISTER]([Request_Date],[Request_No],[Customer_Id],[Customer_Name],[Customer_Address],
	                              [Contact_No],[Tax_Amount],[Gross_Amount],[Net_Amount],[Other_Charges],[Round_Off],[Discount],[Narration],[Location_Id],
	                              [Status],[Created_By],[Created_Date],Cost_Center_Id,Job_Id,Validity,ETA,Terms_And_Condition,Payment_Terms,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
	                              values(@Request_Date,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId+",'"+this.FinancialYear+ "','SRQ'),@Customer_Id,@Customer_Name,@Customer_Address,@Contact_No,@Tax_Amount,@Gross_Amount,@Net_Amount,@Other_Charges,@Round_Off,@Discount,@Narration,@Location_Id,@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@Validity,@ETA,@Terms_And_Condition,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity";
                    db.CreateParameters(32);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Request_Date", this.EntryDate);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Status", 0);
                    db.AddParameters(8, "@Created_By", this.CreatedBy);
                    db.AddParameters(9, "@Customer_Id", this.CustomerId);
                    db.AddParameters(10, "@Customer_Name", this.CustomerName);
                    db.AddParameters(11, "@Customer_Address", this.CustomerAddress);
                    db.AddParameters(12, "@Contact_No", this.ContactNo);
                    db.AddParameters(13, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(14, "@Discount", this.Discount);
                    db.AddParameters(15, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(16, "@Job_Id", this.JobId);
                    db.AddParameters(17, "@Validity", this.Validity);
                    db.AddParameters(18, "@ETA", this.ETA);
                    db.AddParameters(19, "@Terms_And_Condition", this.TermsandConditon);
                    db.AddParameters(20, "@Payment_Terms", this.PaymentTerms);
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
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "SRQ", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in this.Products)
                    {
                        //Product wise Validations.Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                           
                            Item prod = Item.GetPricesWithScheme(item.ItemID, item.InstanceId, this.CustomerId, this.LocationId, item.SchemeId);
                            if (setting.AllowPriceEditingInSalesRequest)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            item.Gross = item.Quantity * prod.SellingPrice;
                            item.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            item.NetAmount = item.Gross + item.TaxAmount;
                            query = @"insert into [TBL_SALES_REQUEST_DETAILS]([Sr_Id],[Item_Id],Scheme_Id,[Qty],[Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Rate],[Priority],[Status],[mrp],[instance_Id],Description)
	                               values(@Sr_Id,@Item_Id,@Scheme_Id,@Qty,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Rate,@Priority,@Request_Status,@mrp,@instance_Id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(15);
                            db.AddParameters(0, "@Srd_Id", item.DetailsID);//**
                            db.AddParameters(1, "@Sr_Id", identity);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Mrp", prod.MRP);
                            db.AddParameters(6, "@Rate", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", item.Gross);
                            db.AddParameters(10, "@Net_Amount", item.NetAmount);
                            db.AddParameters(11, "@Priority", this.Priority);
                            db.AddParameters(12, "@Request_Status", this.RequestStatus);
                            db.AddParameters(13, "@instance_Id", item.InstanceId);
                            db.AddParameters(14, "@Description", item.Description);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount;
                            this.Gross += item.Gross;
                            this.NetAmount += item.NetAmount;
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_REQUEST_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sr_Id=" + identity);
              }

                db.CommitTransaction();
                DataTable dt=db.ExecuteQuery(CommandType.Text, "select Request_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SRQ')[New_Order],Sr_Id from TBL_SALES_REQUEST_REGISTER where Sr_Id=" + identity);
                return new OutputMessage("Sales request registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Request | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(),Id=dt.Rows[0]["Sr_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Sales request could not be saved", false, Type.Others, "Sales Request | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of sales request register and sales request details
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesRequest, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesRequest | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a request.", false, Type.Others, "Sales Request  | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                
                else if (this.LocationId<=0)
                {
                    return new OutputMessage("Select a location", false, Type.Others, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is already in other transaction", false, Type.Others, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"update [TBL_SALES_REQUEST_REGISTER] set [Request_Date]=@Request_Date,[Customer_Id]=@Customer_Id,[Customer_Name]=@Customer_Name,[Customer_Address]=@Customer_Address,
                                  [Contact_No]=@Contact_No,[Tax_Amount]=@Tax_Amount,[Gross_Amount]=@Gross_Amount,[Net_Amount]=@Net_Amount,[Other_Charges]=@Other_Charges,[Round_Off]=@Round_Off,[Discount]=@Discount,[Narration]=@Narration,[Location_Id]=@Location_Id,
                                  [Modified_By]=@Modified_By,[Modified_Date]=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,Validity=@Validity,ETA=@ETA,Terms_And_Condition=@Terms_And_Condition,Payment_Terms=@Payment_Terms,
                                  Salutation=@Salutation,Contact_Name=@Contact_Name,Contact_Address1=@Contact_Address1,
                                  Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                  Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                  Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email
                                  where [sr_Id]=@Sr_Id; delete from [TBL_SALES_REQUEST_DETAILS] where [sr_Id]=@Sr_Id;";
                    db.CreateParameters(32);
                    db.AddParameters(0, "@Sr_Id", this.ID);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Request_Date", this.EntryDate);
                    db.AddParameters(3, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(4, "@Gross_Amount", this.Gross);
                    db.AddParameters(5, "@Net_Amount", this.NetAmount);
                    db.AddParameters(6, "@Narration", this.Narration);
                    db.AddParameters(7, "@Round_Off", this.RoundOff);
                    db.AddParameters(8, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(9, "@Customer_Id", this.CustomerId);
                    db.AddParameters(10, "@Customer_Name", this.CustomerName);
                    db.AddParameters(11, "@Customer_Address", this.CustomerAddress);
                    db.AddParameters(12, "@Contact_No", this.ContactNo);
                    db.AddParameters(13, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(14, "@Discount", this.Discount);
                    db.AddParameters(15, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(16, "@Job_Id", this.JobId);
                    db.AddParameters(17, "@Validity", this.Validity);
                    db.AddParameters(18, "@ETA", this.ETA);
                    db.AddParameters(19, "@Terms_And_Condition", this.TermsandConditon);
                    db.AddParameters(20, "@Payment_Terms", this.PaymentTerms);
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
                            if (setting.AllowPriceEditingInSalesRequest)//check for is rate editable in setting
                            {
                                prod.SellingPrice = item.SellingPrice;
                            }
                            prod.TaxAmount = prod.SellingPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.SellingPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"insert into [TBL_SALES_REQUEST_DETAILS]([Sr_Id],[Item_Id],Scheme_Id,[Qty],[Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Rate],[Priority],[Status],[mrp],[instance_Id],Description)
	                               values(@Sr_Id,@Item_Id,@Scheme_Id,@Qty,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Rate,@Priority,@Request_Status,@mrp,@instance_Id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(15);
                            db.AddParameters(0, "@Srd_Id", item.DetailsID);
                            db.AddParameters(1, "@Sr_Id", this.ID);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Scheme_Id", item.SchemeId);
                            db.AddParameters(4, "@Qty", item.Quantity);
                            db.AddParameters(5, "@Mrp", prod.MRP);
                            db.AddParameters(6, "@Rate", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", prod.Gross);
                            db.AddParameters(10, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(11, "@Priority", this.Priority);
                            db.AddParameters(12, "@Request_Status", this.RequestStatus);
                            db.AddParameters(13, "@instance_Id", item.InstanceId);
                            db.AddParameters(14, "@Description", item.Description);
                            db.BeginTransaction();
                            db.ExecuteScalar(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount ;
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_REQUEST_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sr_Id=" + this.ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Request_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SRQ')[New_Order],Sr_Id from tbl_sales_request_register where Sr_Id=" + ID);
                return new OutputMessage("Sales request updated successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Sales Request | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(),Id=dt.Rows[0]["Sr_Id"] });
        }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Sales request could not be updated", false, Type.Others, "Sales Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// delete details of sales request register and sales request details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesRequest, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesRequest | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "SalesRequest | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            if (HasReference(this.ID))
            {

                return new OutputMessage("Cannot Delete. This request is already in other transaction", false, Type.Others, "SalesRequest | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_SALES_REQUEST_DETAILS where Sr_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_SALES_REQUEST_REGISTER where Sr_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Sales request deleted successfully", true, Type.NoError, "SalesRequest | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Request | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Sales Request | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Sales request could not be deleted", false, Type.Others, "Sales Request | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }

                }
            }

        }
        /// <summary>
        /// Gets all the Sales Request Register with list of Products
        /// </summary>
        /// <param name="LocationID">Location Id from where the request generated</param>
        /// <returns></returns>
        public static List<SalesRequestRegister> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select sr.Sr_Id [Sales_Request_Id],sr.Request_No,sr.Request_Date,sr.Customer_Name,sr.Customer_Id,sr.Customer_Address,sr.Contact_No,sr.Narration,
                               sr.location_Id,sd.Item_Id,sd.instance_id,sr.Tax_Amount,tax.Percentage [TaxPercentage],isnull(sr.[Status],0)[Status],isnull(sd.Scheme_Id,0)[Scheme_Id]
                               ,sr.Round_Off,sr.Gross_Amount,sr.Net_Amount,sd.Rate [Rate],sd.Mrp,sd.Priority,sd.Status,l.Name [location]
                               ,sd.Srd_Id,sd.Qty,sr.Created_Date,isnull(sd.[Status],0)[P_Status],sd.Tax_Amount [P_Tax_Amount],sd.Gross_Amount [P_Gross_Amount],
                               sd.Net_Amount [P_Net_Amount],itm.Name,itm.Item_Code,l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],
                               cus.Address1[Cus_Address2],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],
							   isnull(cm.Name,0)[Company],isnull(cm.Mobile_No1,0)[Comp_Phone],l.Reg_Id1[Location_RegistrationId],cus.Taxno1[Customer_TaxNo],cm.Reg_Id1[Company_RegistrationId]
							   ,cm.Logo[Company_Logo],cm.Email[Company_Email]
							   from[TBL_SALES_REQUEST_REGISTER] sr with(nolock)
                               left join [TBL_SALES_REQUEST_DETAILS] sd with(nolock) on sr.Sr_Id=sd.Sr_Id
                               left join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=sr.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sr.Customer_Id
                               left join TBL_TAX_MST tax with(nolock) on Tax.Tax_Id=sd.Tax_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sd.Item_Id 		
                               left join TBL_COMPANY_MST cm with(nolock) on cm.company_id=l.Company_Id
                               where sr.Location_Id=@Location_Id order by sr.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesRequestRegister> result = new List<SalesRequestRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesRequestRegister register = new SalesRequestRegister();
                        register.ID =row["Sales_Request_Id"]!=DBNull.Value? Convert.ToInt32(row["Sales_Request_Id"]):0;
                        register.LocationId =row["Location_Id"]!=DBNull.Value? Convert.ToInt32(row["Location_Id"]):0;
                        register.EntryDateString = Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy");
                        register.Priority =Convert.ToBoolean(row["Priority"]);
                        register.RequestNo = Convert.ToString(row["Request_No"]);
                        register.Gross =row["Gross_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Gross_Amount"]):0;
                        register.TaxAmount =row["Tax_Amount"]!=DBNull.Value?Convert.ToDecimal(row["Tax_amount"]):0;
                        register.NetAmount =row["Net_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Net_Amount"]):0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status =row["Status"]!=DBNull.Value? Convert.ToInt32(row["Status"]):0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegistrationId"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CustomerTaxNo = Convert.ToString(row["Customer_TaxNo"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.CustomerId =row["Customer_Id"]!=DBNull.Value? Convert.ToInt32(row["customer_id"]):0;
                        register.CustomerName = Convert.ToString(row["Customer_Name"]);
                        register.CustomerAddress = Convert.ToString(row["Customer_Address"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ContactNo = Convert.ToString(row["Contact_No"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Request_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID =rowItem["Srd_Id"]!=DBNull.Value? Convert.ToInt32(rowItem["Srd_Id"]):0;
                            item.ItemID =rowItem["Item_Id"]!=DBNull.Value? Convert.ToInt32(rowItem["Item_Id"]):0;
                            item.Name = Convert.ToString(rowItem["name"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.SellingPrice = rowItem["Rate"]!=DBNull.Value? Convert.ToDecimal(rowItem["rate"]):0;
                            item.TaxPercentage =rowItem["TaxPercentage"]!=DBNull.Value? Convert.ToDecimal(rowItem["TaxPercentage"]):0;
                            item.Gross =rowItem["P_Gross_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["p_Gross_Amount"]):0;
                            item.NetAmount =rowItem["P_Net_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["p_Net_Amount"]):0;
                            item.TaxAmount=rowItem["P_Tax_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["p_Tax_Amount"]):0;
                            item.MRP =rowItem["Mrp"]!=DBNull.Value? Convert.ToDecimal(rowItem["mrp"]):0;
                            item.Status =rowItem["P_Status"]!=DBNull.Value? Convert.ToInt32(rowItem["P_Status"]):0;
                            item.Quantity =rowItem["Qty"]!=DBNull.Value? Convert.ToDecimal(rowItem["Qty"]):0;
                           
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
                Application.Helper.LogException(ex, "SalesRequest | GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<SalesRequestRegister> GetDetails(int LocationID, int? CustomerId, DateTime? From, DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select sr.Sr_Id [Sales_Request_Id],sr.Request_No,sr.Request_Date,sr.Customer_Name,sr.Customer_Id,sr.Customer_Address,sr.Contact_No,sr.Narration,
                              sr.location_Id,sd.Item_Id,sd.instance_id,sr.Tax_Amount,tax.Percentage [TaxPercentage],isnull(sr.[Status],0)[Status],isnull(sd.Scheme_Id,0)[Scheme_Id]
                              ,sr.Round_Off,sr.Gross_Amount,sr.Net_Amount,sd.Rate [Rate],sd.Mrp,sd.Priority,sd.Status,l.Name [location]
                              ,sd.Srd_Id,sd.Qty,sr.Created_Date,isnull(sd.[Status],0)[P_Status],sd.Tax_Amount [P_Tax_Amount],sd.Gross_Amount [P_Gross_Amount],
                              sd.Net_Amount [P_Net_Amount],itm.Name,itm.Item_Code,l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],
                              cus.Address1[Cus_Address2],isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],
                              isnull(cm.Name,0)[Company],isnull(cm.Mobile_No1,0)[Comp_Phone],l.Reg_Id1[Location_RegistrationId],
                              cus.Taxno1[Customer_TaxNo],cm.Reg_Id1[Company_RegistrationId],cus.Name[Customer]
                              ,cm.Logo[Company_Logo],cm.Email[Company_Email],isnull(sr.Cost_Center_Id,0)[Cost_Center_Id],isnull(sr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job]
                              ,cus.Email[Cus_Email],sr.Validity,sr.ETA,sr.Terms_And_Condition,sr.Payment_Terms,sd.Description
                              from[TBL_SALES_REQUEST_REGISTER] sr with(nolock)
                              left join [TBL_SALES_REQUEST_DETAILS] sd with(nolock) on sr.Sr_Id=sd.Sr_Id
                              left join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=sr.Location_Id
                              left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sr.Customer_Id
                              left join TBL_TAX_MST tax with(nolock) on Tax.Tax_Id=sd.Tax_Id
                              left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sd.Item_Id 		
                              left join TBL_COMPANY_MST cm with(nolock) on cm.company_id=l.Company_Id
                              left join tbl_Fin_CostCenter cost on cost.Fcc_ID=sr.Cost_Center_Id
                              left join TBL_JOB_MST j on j.job_Id=sr.Job_Id
                              where sr.Location_Id=@Location_Id {#customerfilter#} {#daterangefilter#} order by sr.Created_Date desc";
                #endregion query
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and sr.Request_Date>=@fromdate and sr.Request_Date<=@todate ");
                }
                else
                {
                    To = DateTime.UtcNow;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and sr.Request_Date>=@fromdate and sr.Request_Date<=@todate ");
                }
                if (CustomerId != null && CustomerId > 0)
                {
                    query = query.Replace("{#customerfilter#}", " and sr.customer_id=@CustomerId ");
                }
                else
                {

                    query = query.Replace("{#customerfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@CustomerId", CustomerId);
                db.AddParameters(3, "@todate", To);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesRequestRegister> result = new List<SalesRequestRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesRequestRegister register = new SalesRequestRegister();
                        register.ID = row["Sales_Request_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Request_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.EntryDateString = Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy");
                        register.Priority = Convert.ToBoolean(row["Priority"]);
                        register.RequestNo = Convert.ToString(row["Request_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.Customer = Convert.ToString(row["Customer"]);
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegistrationId"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CustomerTaxNo = Convert.ToString(row["Customer_TaxNo"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CustomerEmail = Convert.ToString(row["Cus_Email"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                        register.CustomerName = Convert.ToString(row["Customer_Name"]);
                        register.CustomerAddress = Convert.ToString(row["Customer_Address"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ContactNo = Convert.ToString(row["Contact_No"]);
                        register.TermsandConditon = Convert.ToString(row["Terms_And_Condition"]);
                        register.PaymentTerms = Convert.ToString(row["Payment_Terms"]);
                        register.Validity = Convert.ToString(row["Validity"]);
                        register.ETA = Convert.ToString(row["ETA"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Request_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Srd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Srd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = Convert.ToString(rowItem["name"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.SellingPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["mrp"]) : 0;
                            item.Status = rowItem["P_Status"] != DBNull.Value ? Convert.ToInt32(rowItem["P_Status"]) : 0;
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
                Application.Helper.LogException(ex, "SalesRequest | GetDetails(int LocationID, int? CustomerId, DateTime? From, DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static SalesRequestRegister GetDetails(int Id,int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select sr.Sr_Id [Sales_Request_Id],sr.Request_No,sr.Request_Date,sr.Customer_Name,sr.Customer_Id,sr.Customer_Address,sr.Contact_No,sr.Narration,
                               sr.location_Id,sd.Item_Id,sd.instance_id,sr.Tax_Amount,tax.Percentage [TaxPercentage],isnull(sr.[Status],0)[Status],isnull(sd.Scheme_Id,0)[Scheme_Id]
                               ,sr.Round_Off,sr.Gross_Amount,sr.Net_Amount,sd.Rate [Rate],sd.Mrp,sd.Priority,sd.Status,l.Name [location]
                               ,sd.Srd_Id,sd.Qty,sr.Created_Date,isnull(sd.[Status],0)[P_Status],sd.Tax_Amount [P_Tax_Amount],sd.Gross_Amount [P_Gross_Amount],
                               sd.Net_Amount [P_Net_Amount],itm.Name,itm.Item_Code,l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],
                               isnull(cm.Address1,0)[Comp_Address1],isnull(cm.Address2,0)[Comp_Address2],
							   isnull(cm.Name,0)[Company],ut.Name[Unit],isnull(cm.Mobile_No1,0)[Comp_Phone],l.Reg_Id1[Location_RegistrationId],cus.Taxno1[Customer_TaxNo],cm.Reg_Id1[Company_RegistrationId]
							   ,cm.Logo[Company_Logo],cm.Email[Company_Email],coun.Name[Customer_Country],st.Name[Customer_State],
                               isnull(sr.Cost_Center_Id,0)[Cost_Center_Id],isnull(sr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job],
                               sr.Validity,sr.ETA,sr.Terms_And_Condition,sr.Payment_Terms,sd.Description,
							   sr.Salutation,sr.Contact_Name,sr.Contact_Address1,sr.Contact_Address2,sr.Contact_City,sr.State_ID,sr.Country_ID,
                               sr.Contact_Zipcode,sr.Contact_Phone1,sr.Contact_Phone2,sr.Contact_Email
							   from[TBL_SALES_REQUEST_REGISTER] sr with(nolock)
                               left join [TBL_SALES_REQUEST_DETAILS] sd with(nolock) on sr.Sr_Id=sd.Sr_Id
                               left join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=sr.Location_Id
                               left join TBL_CUSTOMER_MST cus with(nolock) on cus.Customer_Id=sr.Customer_Id
                               left join TBL_TAX_MST tax with(nolock) on Tax.Tax_Id=sd.Tax_Id
							   left join tbl_country_mst coun with(nolock) on coun.Country_Id=sr.Country_Id
							   left join TBL_STATE_MST st with(nolock) on st.State_Id=sr.State_Id
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=sd.Item_Id 	
                               left join TBL_UNIT_MST ut on ut.Unit_id=itm.Unit_id	
                               left join TBL_COMPANY_MST cm with(nolock) on cm.company_id=l.Company_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=sr.Cost_Center_Id
                               left join TBL_JOB_MST j on j.job_Id=sr.Job_Id
                               where sr.Location_Id=@Location_Id and sr.Sr_Id=@Sr_Id order by sr.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@Sr_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                        DataRow row = dt.Rows[0];
                        SalesRequestRegister register = new SalesRequestRegister();
                        register.ID = row["Sales_Request_Id"] != DBNull.Value ? Convert.ToInt32(row["Sales_Request_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.EntryDateString = Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy");
                        register.Priority = Convert.ToBoolean(row["Priority"]);
                        register.RequestNo = Convert.ToString(row["Request_No"]);
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CustomerTaxNo = Convert.ToString(row["Customer_TaxNo"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                        register.CustomerName = Convert.ToString(row["Customer_Name"]);
                        register.ContactNo = Convert.ToString(row["Contact_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["customer_id"]) : 0;
                        register.CustomerName = Convert.ToString(row["Customer_Name"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.ContactNo = Convert.ToString(row["Contact_No"]);
                        register.Validity = Convert.ToString(row["Validity"]);
                        register.ETA = Convert.ToString(row["ETA"]);
                        register.PaymentTerms = Convert.ToString(row["Payment_Terms"]);
                        register.TermsandConditon = Convert.ToString(row["Terms_And_Condition"]);
                        List<Entities.Master.Address> addresslist = new List<Master.Address>();
                        Entities.Master.Address address = new Master.Address();
                        address.ContactName = Convert.ToString(row["Contact_Name"]);
                        address.Address1 = Convert.ToString(row["Contact_Address1"]);
                        address.Address2 = Convert.ToString(row["Contact_Address2"]);
                        address.City = Convert.ToString(row["Contact_City"]);
                        address.Phone1 = Convert.ToString(row["Contact_Phone1"]);
                        address.Phone2 = Convert.ToString(row["Contact_Phone2"]);
                        address.Email = Convert.ToString(row["Contact_Email"]);
                        address.Zipcode = Convert.ToString(row["Contact_Zipcode"]);
                        address.Salutation = Convert.ToString(row["Salutation"]);
                        address.State = Convert.ToString(row["Customer_State"]);
                        address.Country = Convert.ToString(row["Customer_Country"]);
                        address.CountryID = row["Country_ID"] != DBNull.Value ? Convert.ToInt32(row["Country_ID"]) : 0;
                        address.StateID = row["State_ID"] != DBNull.Value ? Convert.ToInt32(row["State_ID"]) : 0;
                        addresslist.Add(address);
                        register.BillingAddress = addresslist;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Sales_Request_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Srd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Srd_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = Convert.ToString(rowItem["name"]);
                            item.Unit = Convert.ToString(rowItem["Unit"]);
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.ItemCode = rowItem["item_Code"].ToString();
                            item.SchemeId = rowItem["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Scheme_Id"]) : 0;
                            item.SellingPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["p_Tax_Amount"]) : 0;
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["mrp"]) : 0;
                            item.Status = rowItem["P_Status"] != DBNull.Value ? Convert.ToInt32(rowItem["P_Status"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                           
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
                Application.Helper.LogException(ex, "SalesRequest | GetDetails(int Id,int LocationID)");
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
                    string query = @"select count(*) from [TBL_sales_request_DETAILS] srd with(nolock) join
                                    TBL_sales_QUOTE_DETAILS q with(nolock) on q.Srd_Id=srd.Srd_Id
                                    join  TBL_sales_QUOTE_REGISTER qr with(nolock) on q.Sq_Id=qr.Sq_Id where
                                    srd.sr_id=@id";
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
                    Application.Helper.LogException(ex, "SalesRequest | HasReference(int id)");
                    throw;
                }
            }
       }

        public static OutputMessage SendMail(int RequestId, string toAddress, int userId, string url)
        {


            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesRequest | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "SalesRequest | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                mail.Subject = "Sales Request";
                mail.Body = "Please Find the attached copy of Sales Request";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Sales Request.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "SalesRequest | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "SalesRequest | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }

        }

        //public static Item GetItemsFromPurchaseWithScheme(int CustomerId, int LocationId,int ItemId,int InstanceId)
        //{
        //    DBManager db = new DBManager();
        //    try
        //    {
        //        db.Open();
        //        db.CreateParameters(4);
        //        db.AddParameters(0, "@CustomerId", CustomerId);
        //        db.AddParameters(1, "@LocationId", LocationId);
        //        db.AddParameters(2, "@ItemId", ItemId);
        //        db.AddParameters(3, "@InstanceId", InstanceId);
        //        string query = @"[dbo].[USP_SEARCH_PRODUCTS_FROMSCHEME] @CustomerId @LocationId";
        //        DataTable dt = db.ExecuteQuery(CommandType.Text, query);

        //        Item item = new Item();
        //        DataRow prod = dt.Rows[0];
        //        item.ItemID = prod["ItemId"] != DBNull.Value ? Convert.ToInt32(prod["ItemId"]) : 0;
        //        item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
        //        item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
        //        item.TaxPercentage = prod["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(prod["Taxpercentage"]) : 0;
        //        item.SellingPrice = prod["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Selling_Price"]) : 0;
        //        item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
        //        item.Stock = prod["Stock"] != DBNull.Value ? Convert.ToDecimal(prod["Stock"]) : 0;
        //        item.SchemeId = prod["scheme_id"] != DBNull.Value ? Convert.ToInt32(prod["scheme_id"]) : 0;
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }


        //}

    }
    }

