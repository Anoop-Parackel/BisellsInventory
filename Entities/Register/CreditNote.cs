using Core.DBManager;
using Entities.Application;
using Entities.Register;
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
    public class CreditNote : Register
    {
        #region Properties
        public DateTime ReturnDate { get; set; }
        public String ReturnDateString { get; set; }
        public int EntryId { get; set; }
        public int CustomerId { get; set; }
        public int ReturnFrom { get; set; }
        public string BillNo { get; set; }
        /// <summary>
        /// 0 Damage
        /// 1 Wrong Supply
        /// </summary>
        public int ReturnType { get; set; }
        public string Customer { get; set; }
        public string FinancialYear { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTaxNo { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerState { get; set; }
        public int CustStateId { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerEmail { get; set; }
        public string ContactNo { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyRegId { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public static object GetDetailsForConfirm(int locationId)
        {
            throw new NotImplementedException();
        }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string LocationRegId { get; set; }
        public string SupplierAddress1 { get; set; }
        public string SupplierAddress2 { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierTaxNo { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyEmail { get; set; }
        public int Status { get; set; }
        public List<Item> Products { get; set; }
        public override decimal NetAmount { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public string InstanceId { get; set; }
        public string ItemID { get; set; }
        public string Quantity { get; set; }
        public string SellingPrice { get; set; }
        public string TaxId { get; set; }
        public string MRP { get; set; }
        public string DamageId { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        #endregion Properties

        #region Constructors
        public CreditNote(int ID)
        {
            this.ID = ID;
        }

        public CreditNote()
        {
        }
        #endregion Constructors

        #region Functions
        /// <summary>
        /// save sales return register and sales return details
        /// for save an entry id of that entry must be zero
        /// </summary>
        /// <returns>return success alert when details saved successfully otherwise return error alert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.SalesCreditNote, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "CreditNote | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    int identity = 0;
                    {
                        db.Open();
                        string query = @"insert into tbl_sales_return_register( Return_Date,Location_Id,Customer_Id,Return_From,Bill_No,Return_Type,Tax_Amount,Gross_Amount,Net_Amount,Round_Off,
	                                   Other_Charges,Narration,[Status],Created_By,Created_Date,Cost_Center_Id,Job_Id,TandC,Payment_Terms,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
                                       values(@Return_Date,@Location_Id,@Customer_Id,@Return_From,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SRT') ,@Return_Type,@Tax_Amount,@Gross_Amount,@Net_Amount,@Round_Off,@Other_Charges,@Narration,@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity";
                        db.CreateParameters(28);
                        db.AddParameters(0, "@Return_Date", this.ReturnDate);
                        db.AddParameters(1, "@Location_Id", this.LocationId);
                        db.AddParameters(2, "@Customer_Id", this.CustomerId);
                        db.AddParameters(3, "@Return_From", this.ReturnFrom);
                        db.AddParameters(4, "@Return_Type", this.ReturnType);
                        db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                        db.AddParameters(6, "@Gross_Amount", this.Gross);
                        db.AddParameters(7, "@Net_Amount", this.NetAmount);
                        db.AddParameters(8, "@Round_Off", this.RoundOff);
                        db.AddParameters(9, "@Other_Charges", this.OtherCharges);
                        db.AddParameters(10, "@Narration", this.Narration);
                        db.AddParameters(11, "@Status", this.Status);
                        db.AddParameters(12, "@Created_By", this.CreatedBy);
                        db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                        db.AddParameters(14, "@Job_Id", this.JobId);
                        db.AddParameters(15, "@TandC", this.TermsandConditon);
                        db.AddParameters(16, "@Payment_Terms", this.Payment_Terms);
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
                        UpdateOrderNumber(this.CompanyId, this.FinancialYear, "SRT", db);
                        foreach (Item item in Products)
                        {
                            if (item.Quantity <= 0)
                            {
                                return new OutputMessage("Qty must be grater than zero", true, Type.Others, "CreditNote | Save", System.Net.HttpStatusCode.InternalServerError);
                            }
                            else
                            {
                                Item product = Item.GetPrices(item.ItemID, item.InstanceId);
                                product.TaxAmount = product.SellingPrice * (product.TaxPercentage / 100) * item.Quantity;
                                product.Gross = item.Quantity * product.SellingPrice;
                                product.NetAmount = product.Gross + product.TaxAmount;
                                db.CleanupParameters();
                                query = @"insert into tbl_sales_return_details (Sret_Id,item_id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,
                                        Net_Amount, Remarks,[Status],instance_id,Sed_Id)
                                        values (@Sret_Id,@item_id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                        @Remarks,@Status,@instance_id,@Sed_Id)";
                                db.CreateParameters(14);
                                db.AddParameters(0, "@Sret_Id", identity);
                                db.AddParameters(2, "@item_id", product.ItemID);
                                db.AddParameters(3, "@Quantity", item.Quantity);
                                db.AddParameters(4, "@MRP", product.MRP);
                                db.AddParameters(5, "@Rate", product.SellingPrice);
                                db.AddParameters(6, "@Tax_Id", product.TaxId);
                                db.AddParameters(7, "@Tax_Amount", product.TaxAmount);
                                db.AddParameters(8, "@Gross_Amount", product.Gross);
                                db.AddParameters(9, "@Net_Amount", product.NetAmount);
                                db.AddParameters(10, "@Remarks", DBNull.Value);
                                db.AddParameters(11, "@Status", 0);
                                db.AddParameters(12, "@instance_id", product.InstanceId);
                                db.AddParameters(13, "@Sed_Id", item.SedId);
                                db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                                this.TaxAmount += product.TaxAmount;
                                this.Gross += product.Gross;
                                this.NetAmount += product.NetAmount;
                            }
                        }
                        db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_RETURN_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sret_Id=" + identity);
                    }
                    db.CommitTransaction();
                    Entities.Application.Helper.PostFinancials("TBL_SALES_RETURN_REGISTER", identity, this.CreatedBy);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SRT')[New_Order] from TBL_SALES_RETURN_REGISTER where Sret_Id=" + identity);
                    return new OutputMessage("Sales return registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "CreditNote | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
                }
                catch (Exception ex)
                {
                    return new OutputMessage("something went wrong", false, Type.Others, "CreditNote | Save", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// update details of sales return register and sales return details 
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesCreditNote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "CreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.ReturnDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update a Return.", false, Type.Others, "CreditNote| Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a return.", false, Type.Others, "CreditNote  | Update", System.Net.HttpStatusCode.InternalServerError);
                }

                else
                {
                    db.Open();
                    string query = @"delete from tbl_sales_return_details where Sret_Id=@id;
                                   update TBL_SALES_RETURN_REGISTER set Return_Date=@Return_Date,Location_Id=@Location_Id,Customer_Id=@Customer_Id,
                                   Return_From=@Return_From,Return_Type=@Return_Type,Tax_Amount=@Tax_Amount,Gross_Amount=@Gross_Amount,Net_Amount=@Net_Amount,
                                   Round_Off=@Round_Off,Other_Charges=@Other_Charges,Narration=@Narration,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),
                                   Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,
                                   TandC=@TandC,Payment_Terms=@Payment_Terms,Salutation=@Salutation,Contact_Name=@Contact_Name,Contact_Address1=@Contact_Address1,
                                   Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                   Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                   Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email where Sret_Id=@id";
                    db.BeginTransaction();
                    db.CreateParameters(27);
                    db.AddParameters(0, "@Return_Date", this.ReturnDate);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Customer_Id", this.CustomerId);
                    db.AddParameters(3, "@Return_From", this.ReturnFrom);
                    db.AddParameters(4, "@Return_Type", this.ReturnType);
                    db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(6, "@Gross_Amount", this.Gross);
                    db.AddParameters(7, "@Net_Amount", this.NetAmount);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(10, "@Narration", this.Narration);
                    db.AddParameters(11, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(12, "@id", this.ID);
                    db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(14, "@Job_Id", this.JobId);
                    db.AddParameters(15, "@TandC", this.TermsandConditon);
                    db.AddParameters(16, "@Payment_Terms", this.Payment_Terms);
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
                    }
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "CreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            dynamic setting = Application.Settings.GetFeaturedSettings();
                            Item product = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInSalesReturn)//check for is rate editable in setting
                            {
                                product.SellingPrice = item.SellingPrice;
                            }
                            if (item.Quantity < product.Quantity)
                            {
                                return new OutputMessage("Quantity sold is less than the quantity you are trying to retur. Hence not allowed", false, Type.Others, "CreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
                            }
                            product.TaxAmount = product.SellingPrice * (product.TaxPercentage / 100) * item.Quantity;
                            product.Gross = item.Quantity * product.SellingPrice;
                            product.NetAmount = product.Gross + product.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into tbl_sales_return_details (Sret_Id,Sed_Id,item_id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,
                                    Net_Amount, Remarks,[Status],instance_id)
                                    values (@Sret_Id,@Sed_Id,@item_id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                    @Remarks,@Status,@instance_id)";
                            db.CreateParameters(13);
                            db.AddParameters(0, "@Sret_Id", this.ID);
                            db.AddParameters(1, "@Sed_Id", item.SedId);
                            db.AddParameters(2, "@item_id", product.ItemID);
                            db.AddParameters(3, "@Quantity", item.Quantity);
                            db.AddParameters(4, "@MRP", product.MRP);
                            db.AddParameters(5, "@Rate", product.SellingPrice);
                            db.AddParameters(6, "@Tax_Id", product.TaxId);
                            db.AddParameters(7, "@Tax_Amount", product.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", product.Gross);
                            db.AddParameters(9, "@Net_Amount", product.NetAmount);
                            db.AddParameters(10, "@Remarks", DBNull.Value);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@instance_id", product.InstanceId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += product.TaxAmount;
                            this.Gross += product.Gross;
                            this.NetAmount += product.NetAmount;
                        }
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_SALES_RETURN_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Sret_Id=" + ID);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_SALES_RETURN_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','SRT')[New_Order] from tbl_sales_return_register where Sret_Id=" + ID);
                return new OutputMessage("Credit Note has been updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "CreditNote | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Sales Return| Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Credit note updated successfully", true, Type.Others, "Sales Return | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Credit note updated successfully", true, Type.Others, "Sales Return | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// delete details of the sales return register and sales return details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.SalesCreditNote, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesCreditNote | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Select an entry for deletion", false, Type.RequiredFields, "SalesCreditNote| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    db.Open();
                    db.BeginTransaction();
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    string query = "select isnull( Damage_Id,0) from TBL_DAMAGE_REGISTER where Sret_id=@id";
                    int damage_id = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    db.CreateParameters(1);
                    db.AddParameters(0, "@damageid", damage_id);
                    if (damage_id > 0)//return type is damage
                    {
                        query = "delete from TBL_DAMAGE_details where damage_id=@damageid";
                        Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        query = "delete from TBL_DAMAGE_register where damage_id=@damageid";
                        Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                    }
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    query = "delete from TBL_SALES_RETURN_DETAILS where Sret_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_SALES_RETURN_REGISTER where Sret_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    Entities.Application.Helper.PostFinancials("TBL_SALES_RETURN_REGISTER", this.ID, this.ModifiedBy);
                    return new OutputMessage("Credit note request has been deleted", true, Type.NoError, "SalesReturn | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "SalesReturn | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Credit note could not be deleted", false, Type.RequiredFields, "SalesReturn | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Credit note could not be deleted", false, Type.Others, "SalesReturn | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
        }

        ////////end delete//////////////
        public static List<SalesReturnRegister> GetDetails(int LocationId, int? CustomerId, DateTime? From, DateTime? To)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select sr.Sret_Id[SalesReturnId],isnull(sr.Return_Date,0) Return_Date,isnull(sed.Se_Id,0) sales_entryId,sr.Location_Id,sr.Customer_Id,sr.Bill_No,sr.Return_Type,sr.Tax_Amount
                               ,sr.Gross_Amount,sr.Round_Off,sr.Net_Amount,sr.Other_Charges,sr.Narration,sr.[Status],srd.Sretd_Id,srd.Sed_Id,srd.item_id,srd.Quantity
                               ,srd.MRP,srd.Rate[Selling_Price],sr.Tax_Amount[P_Tax_Amount],srd.Instance_Id,srd.Gross_Amount[P_Gross_Amount],srd.Net_Amount[P_Net_Amount],l.Name[Location],
                               cm.Name[Company],tx.Percentage[Tax_Percentage],
							   it.Name[Item],it.Item_Code,cus.Name[Customer], 
							   l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],
                               cm.Address1[Company_Address1],cm.Address2[Company_Address2],cm.Mobile_No1[Company_Phone],cm.Reg_Id1[Company_RegId],
                               cus.Address1[Cus_Address1],cus.Address2[Cus_Address2],cus.Phone1[Cus_Phone],cus.Taxno1[Cus_taxNo],cus.Name[Customer]
							   ,cm.Logo[Company_Logo],cm.Email[Company_Email],isnull(sr.Cost_Center_Id,0)[Cost_Center_Id],isnull(sr.Job_Id,0)[Job_Id],cost.fcc_Name[Cost_Center],j.job_name[Job]
							   ,cus.Email[Cust_Email],sr.TandC,sr.Payment_Terms from TBL_SALES_RETURN_REGISTER sr with(nolock)
                               left join TBL_SALES_RETURN_DETAILS srd with(nolock) on srd.Sret_Id=sr.Sret_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=sr.Location_Id
							   left join TBL_CUSTOMER_MST cus on cus.Customer_Id=sr.Customer_Id
                               left join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=srd.Tax_Id                              
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=srd.Item_Id
							   left join TBL_SALES_ENTRY_DETAILS sed with(nolock) on sed.Sed_Id=srd.Sed_Id
							   left join tbl_fin_CostCenter cost on cost.Fcc_ID=sr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.job_id=sr.job_id
                               where sr.Location_Id=@Location_Id {#customerfilter#} {#daterangefilter#} order by sr.Return_Date desc";
                #endregion query
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and sr.Return_Date>=@fromdate and sr.Return_Date<=@todate ");
                }
                else
                {
                    To = DateTime.UtcNow;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and sr.Return_Date>=@fromdate and sr.Return_Date<=@todate ");
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
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.AddParameters(3, "@CustomerId", CustomerId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<SalesReturnRegister> result = new List<SalesReturnRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        SalesReturnRegister register = new SalesReturnRegister();
                        register.ID = row["SalesReturnId"] != DBNull.Value ? Convert.ToInt32(row["SalesReturnId"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.EntryId = row["sales_entryId"] != DBNull.Value ? Convert.ToInt32(row["sales_entryId"]) : 0;
                        register.ReturnDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                        register.BillNo = Convert.ToString(row["Bill_No"]);
                        register.Customer = Convert.ToString(row["Customer"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.CompanyAddress1 = Convert.ToString(row["Company_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Company_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Company_Phone"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegId"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CustomerEmail = Convert.ToString(row["Cust_Email"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.CustomerAddress = Convert.ToString(row["Cus_Address1"]);
                        register.ContactNo = Convert.ToString(row["Cus_Phone"]);
                        register.CustomerAddress2 = Convert.ToString(row["Cus_Address2"]);
                        register.Customer = Convert.ToString(row["Customer"]);
                        register.CustomerTaxNo = Convert.ToString(row["Cus_taxNo"]);
                        register.ReturnType = row["Return_Type"] != DBNull.Value ? Convert.ToInt32(row["Return_Type"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.Company = Convert.ToString(row["Company"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("SalesReturnId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Sretd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sretd_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.SedId = rowItem["sed_id"] != DBNull.Value ? Convert.ToInt32(rowItem["sed_id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["MRP"] != DBNull.Value ? Convert.ToDecimal(rowItem["MRP"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
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
        Application.Helper.LogException(ex, "Credit Note | GetDetails(int LocationId, int? CustomerId, DateTime? From, DateTime? To)");
        return null;
    }
    finally
    {
        db.Close();
    }
}

        public static CreditNote GetDetails(int Id, int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select sr.Sret_Id[SalesReturnId],isnull(sr.Return_Date,0) Return_Date,isnull(sed.Se_Id,0) sales_entryId,
                               sr.Location_Id,sr.Customer_Id,sr.Bill_No,sr.Return_Type,sr.Tax_Amount,sr.Gross_Amount,sr.Round_Off,sr.Net_Amount,
                               sr.Other_Charges,sr.Narration,sr.[Status],srd.Sretd_Id,srd.Sed_Id,srd.item_id,srd.Quantity,srd.MRP,srd.Rate[Selling_Price],
                               sr.Tax_Amount[P_Tax_Amount],srd.Instance_Id,srd.Gross_Amount[P_Gross_Amount],srd.Net_Amount[P_Net_Amount],l.Name[Location],
                               cm.Name[Company],tx.Percentage[Tax_Percentage],it.Name[Item],it.Item_Code,l.Address1[Loc_Address1],l.Address2[Loc_Address2],
                               l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],ut.Name[Unit],
                               cus.Taxno1[Cus_taxNo],cus.Name[Customer],cm.Email[Company_Email],coun.Name[Customer_Country],
                               st.Name[Customer_State],isnull(sr.Cost_Center_Id,0)[Cost_Center_Id],
                               isnull(sr.Job_Id,0)[Job_Id],cost.fcc_Name[Cost_Center],j.job_name[Job],sr.TandC,sr.Payment_Terms,
                               sr.Contact_Address1,sr.Salutation,sr.Contact_Address2,sr.Contact_City,sr.Contact_Email,sr.Contact_Name,
                               sr.Contact_Phone1,sr.Contact_Phone2,sr.Contact_Zipcode,sr.Country_ID,sr.State_ID
                               from TBL_SALES_RETURN_REGISTER sr with(nolock)
                               left join TBL_SALES_RETURN_DETAILS srd with(nolock) on srd.Sret_Id=sr.Sret_Id
                               inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=sr.Location_Id
                               inner join TBL_CUSTOMER_MST cus on cus.Customer_Id=sr.Customer_Id
                               inner join TBL_COMPANY_MST cm with(nolock) on cm.Company_Id=l.Company_Id
                               left join tbl_country_mst coun with(nolock) on coun.Country_Id=sr.Country_Id
                               left join tbl_state_mst st with(nolock) on st.State_Id=sr.State_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=srd.Tax_Id                              
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=srd.Item_Id
                               left join TBL_SALES_ENTRY_DETAILS sed with(nolock) on sed.Sed_Id=srd.Sed_Id
                               left join tbl_fin_CostCenter cost on cost.Fcc_ID=sr.Cost_Center_Id
                               left join TBL_JOB_MST j on j.job_id=sr.job_id
                               left join tbl_unit_mst ut on ut.Unit_Id=it.Unit_Id
                               where sr.Location_Id=@Location_Id and sr.Sret_Id=@Sret_Id order by sr.Return_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Sret_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    CreditNote register = new CreditNote();
                    register.ID = row["SalesReturnId"] != DBNull.Value ? Convert.ToInt32(row["SalesReturnId"]) : 0;
                    register.EntryId = row["sales_entryId"] != DBNull.Value ? Convert.ToInt32(row["sales_entryId"]) : 0;
                    register.ReturnDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                    register.CustomerId = row["customer_id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                    register.BillNo = Convert.ToString(row["Bill_No"]);
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.Location = Convert.ToString(row["Location"]);
                    register.CustomerTaxNo = Convert.ToString(row["Cus_taxNo"]);
                    register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                    register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                    register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                    register.Customer = Convert.ToString(row["Customer"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.ReturnType = row["Return_Type"] != DBNull.Value ? Convert.ToInt32(row["Return_Type"]) : 0;
                    register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                    register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                    register.Company = Convert.ToString(row["Company"]);
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    List<Entities.Master.Address> addresslist = new List<Master.Address>();
                    Entities.Master.Address address = new Master.Address();
                    address.ContactName = Convert.ToString(row["Contact_Name"]);
                    address.Address1 = Convert.ToString(row["Contact_Address1"]);
                    address.Address2 = Convert.ToString(row["Contact_Address2"]);
                    address.City = Convert.ToString(row["Contact_City"]);
                    address.Phone1 = Convert.ToString(row["Contact_Phone1"]);
                    address.Phone2 = Convert.ToString(row["Contact_Phone2"]);
                    address.Zipcode = Convert.ToString(row["Contact_Zipcode"]);
                    address.Salutation = Convert.ToString(row["Salutation"]);
                    address.State = Convert.ToString(row["Customer_State"]);
                    address.Country = Convert.ToString(row["Customer_Country"]);
                    address.CountryID = row["Country_ID"] != DBNull.Value ? Convert.ToInt32(row["Country_ID"]) : 0;
                    address.StateID = row["State_ID"] != DBNull.Value ? Convert.ToInt32(row["State_ID"]) : 0;
                    addresslist.Add(address);
                    register.BillingAddress = addresslist;
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("SalesReturnId") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.DetailsID = rowItem["Sretd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Sretd_Id"]) : 0;
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.SedId = rowItem["sed_id"] != DBNull.Value ? Convert.ToInt32(rowItem["sed_id"]) : 0;
                        item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                        item.Name = rowItem["Item"].ToString();
                        item.MRP = rowItem["MRP"] != DBNull.Value ? Convert.ToDecimal(rowItem["MRP"]) : 0;
                        item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                        item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                        item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                        item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                        item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
                        item.Quantity = rowItem["Quantity"] != DBNull.Value ? Convert.ToDecimal(rowItem["Quantity"]) : 0;
                        item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                        item.Unit = Convert.ToString(rowItem["Unit"]);
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
                Application.Helper.LogException(ex, "SalesReturn | GetDetails(int Id,int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        ///////////////////////// Mail//////////////////////////
        public static OutputMessage SendMail(int SalesId, string toAddress, int userId, string url)
        {
            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.SalesCreditNote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Credit Note | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Credit Note | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                string CreditNoteNo = "";
                string query = "select bill_no from TBL_SALES_RETURN_REGISTER where Sret_Id=@Id";
                db.CreateParameters(1);
                db.AddParameters(0, "@Id", SalesId);
                db.Open();
                CreditNoteNo = Convert.ToString(db.ExecuteScalar(CommandType.Text, query));
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
                mail.Subject = "Credit Note";
                mail.Body = "Please Find the attached copy of Credit Note";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Credit Note "+CreditNoteNo+".pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Convert.ToString(dt.Rows[0]["Email_Host"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Convert.ToString(dt.Rows[0]["Email_id"]), Convert.ToString(dt.Rows[0]["Email_Password"])),
                    Port = Convert.ToInt32(dt.Rows[0]["Email_Port"])

                };
                smtp.Send(mail);
                return new OutputMessage("Mail Send successfully", true, Type.NoError, "Sales Return | Send Mail", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Sales Return | Send Mail", System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }

        #endregion Function
    }
}
