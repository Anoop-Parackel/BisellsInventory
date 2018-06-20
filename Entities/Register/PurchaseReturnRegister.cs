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
   
   public class PurchaseReturnRegister : Register, IRegister
    {
        #region properties
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        
        public string BillNo { get; set; }
        public string FinancialYear { get; set; }
        /// <summary>
        /// return type 0 damage
        /// return type 1 Wrong Supply
        /// return type 2 Shortage
        /// </summary>
        public int ReturnType { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CargoId { get; set; }
        public int Carton { get; set; }
        public decimal Weight { get; set; }
        public int Status { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyRegId { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public int SupStateId { get; set; }
        public List<Entities.Master.Address> BillingAddress { get; set; }
        public string SupCountry { get; set; }
        public string SupState { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyEmail { get; set; }
        public string LocationPhone { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string LocationRegId { get; set; }
        public string SupplierAddress1 { get; set; }
        public string SupplierAddress2 { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierTaxNo { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        public List<Item> Products { get; set; }
        public override decimal NetAmount { get; set; }
        #endregion properties
        public PurchaseReturnRegister(int ID)
        {
            this.ID = ID;
        }

        public PurchaseReturnRegister()
        {
        }

        /// <summary>
        /// save details of purchase return register and purchase return details
        /// to save an entry make sure that the id must be zero
        /// </summary>
        /// <returns>return success alert when details saved successfully otherwise return error alert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseReturn, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseReturn | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year<1900)
                {
                    return new OutputMessage("Select a valid date to make a Return.", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Return.", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError);
                }
               else
                {
                    db.Open();
                    string query = @"insert into TBL_PURCHASE_RETURN_REGISTER(Return_Date,Location_Id,Supplier_Id,Bill_No,Return_Type,Tax_Amount,
                                   Gross_Amount,Net_Amount,Round_Off,Other_Charges,Narration,Cargo_Id,Cartons,Weight,Status,Created_By,Created_Date,Cost_Center_Id,Job_Id,TandC,Payment_Terms,Salutation,Contact_Name,Contact_Address1,Contact_Address2,Contact_City,State_ID,Country_ID,Contact_Zipcode,Contact_Phone1,Contact_Phone2,Contact_Email)
                                   values(@Return_Date,@Location_Id,@Supplier_Id,
                                   [dbo].UDF_Generate_Sales_Bill(" + this.CompanyId+",'"+ this.FinancialYear + "','PRT'),@Return_Type,@Tax_Amount, @Gross_Amount,@Net_Amount, @Round_Off,@Other_Charges,@Narration,@Cargo_Id, @Cartons,@Weight,@Status, @Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@TandC,@Payment_Terms,@Salutation,@Contact_Name,@Contact_Address1,@Contact_Address2,@Contact_City,@State_ID,@Country_ID,@Contact_Zipcode,@Contact_Phone1,@Contact_Phone2,@Contact_Email); select @@identity";
                    db.CreateParameters(31);
                    db.AddParameters(0, "@Return_date", this.EntryDateString);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(3, "@Bill_No", this.BillNo);
                    db.AddParameters(4, "@Return_Type", this.ReturnType);
                    db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(6, "@Gross_Amount", this.Gross);
                    db.AddParameters(7, "@Net_Amount", this.NetAmount);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(10, "@Narration", this.Narration);
                    db.AddParameters(11, "@Cargo_Id", this.CargoId);
                    db.AddParameters(12, "@Cartons", this.Carton);
                    db.AddParameters(13, "@Weight", this.Weight);
                    db.AddParameters(14, "@Status",0);
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
                    dynamic setting = Application.Settings.GetFeaturedSettings(); //Gets All Settings from settings table
                    decimal net=0;
                    foreach (Item item in Products)
                    {

                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        bool exist;
                        query = @"select count(*) from TBL_PURCHASE_ENTRY_DETAILS ped left join TBL_PURCHASE_ENTRY_REGISTER pe on ped.Pe_Id=pe.Pe_Id where pe.Supplier_Id="+SupplierId+" and ped.item_id="+item.ItemID+ "and ped.instance_id="+ item.InstanceId;
                        exist = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query)) > 0 ? true : false;
                        if (item.Quantity <= 0)

                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase return | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else if(!exist)
                        {
                            return new OutputMessage("The selected Item is not exist in the current supplier", false, Type.Others, "Purchase return | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        
                       else
                        {
                            Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId,this.LocationId);
                            if (setting.AllowPriceEditingInPurchaseReturn)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();

                            //Retrun from stock
                            if (item.ReturnFrom == 0)
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds than Stock", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            //Retrun from Damage
                            else if (item.ReturnFrom == 1)
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.DamageStock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds than Damage Stock", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                              
                            }
                                prod.TaxAmount = item.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                                prod.Gross = item.Quantity * item.CostPrice;
                                prod.NetAmount = prod.Gross + prod.TaxAmount;
                                net += prod.NetAmount;
                                db.CleanupParameters();
                                query = @"insert into TBL_PURCHASE_RETURN_DETAILS(Pr_Id,item_id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,
                                     Net_Amount,Remarks,Location_Id,Status,Return_from,instance_id)
                                     values(@Pr_Id,@item_id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                     @Remarks,@Location_Id,@Status,@Return_from,@instance_id)";
                                db.CreateParameters(14);
                                db.AddParameters(0, "@Pr_Id", identity);
                                db.AddParameters(1, "@item_id", item.ItemID);
                                db.AddParameters(2, "@Quantity", item.Quantity);
                                db.AddParameters(3, "@MRP", prod.MRP);
                                db.AddParameters(4, "@Rate", item.CostPrice);
                                db.AddParameters(5, "@Tax_Id", prod.TaxId);
                                db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                                db.AddParameters(7, "@Gross_Amount", prod.Gross);
                                db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                                db.AddParameters(9, "@Remarks", prod.Remarks);
                                db.AddParameters(10, "@Location_Id", this.LocationId);
                                db.AddParameters(11, "@Status", 0);
                                db.AddParameters(12, "@Return_from", item.ReturnFrom);
                                db.AddParameters(13, "@instance_id", item.InstanceId);
                                db.ExecuteProcedure(System.Data.CommandType.Text, query);
                                this.TaxAmount += prod.TaxAmount;
                                this.Gross += prod.Gross;
                           
                        }
           
                         }

                    decimal _NetAmount = 0;
                    if (Application.Settings.IsAutoRoundOff())
                    {
                        _NetAmount = Math.Round(net);
                        this.RoundOff = _NetAmount - net;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        this.NetAmount = Math.Round(net, 2);
                        _NetAmount = net + this.RoundOff;
                    }
                    else
                    {
                        _NetAmount = Math.Round(net);
                        this.RoundOff = _NetAmount - net;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_RETURN_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRT')[New_Order] from TBL_PURCHASE_RETURN_REGISTER where Pr_Id=" + identity);
                return new OutputMessage("Purchase return has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Return | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });

            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Purchase return could not be saved", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of purchase return register and purchase return details
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return success alert when details updated successfully otherwise return error alert</returns>
        public OutputMessage Update()
        {
           if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseReturn, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseReturn | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update a Return", false, Type.Others, "Purchase Return | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a return", false, Type.Others, "Purchase Return | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                //Product wise Validations. Use ladder-if after this "if" for more validations
                

                else
                {
                    db.Open();
                    string query = @"delete from tbl_purchase_return_details where Pr_Id=@Id;
                                     update TBL_PURCHASE_RETURN_REGISTER set Return_Date=@Return_Date,Location_Id=@Location_Id,
                                     Supplier_Id=@Supplier_Id,Return_Type=@Return_Type,
                                     Tax_Amount=@Tax_Amount,Gross_Amount=@Gross_Amount,Net_Amount=@Net_Amount,Round_Off=@Round_Off,
                                     Other_Charges=@Other_Charges,Narration=@Narration,Cargo_Id=@Cargo_Id,Cartons=@Cartons,Weight=@Weight,
                                     Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,
                                     TandC=@TandC,Payment_Terms=@Payment_Terms,Salutation=@Salutation,Contact_Name=@Contact_Name,Contact_Address1=@Contact_Address1,
                                     Contact_Address2=@Contact_Address2,Contact_City=@Contact_City,State_ID=@State_ID,Country_ID=@Country_ID,
                                     Contact_Zipcode=@Contact_Zipcode,Contact_Phone1=@Contact_Phone1,
                                     Contact_Phone2=@Contact_Phone2,Contact_Email=@Contact_Email
                                     where Pr_Id=@id";
                    db.BeginTransaction();
                    db.CreateParameters(30);
                    db.AddParameters(0, "@Return_Date", this.EntryDateString);
                    db.AddParameters(1, "@Location_Id", this.LocationId);
                    db.AddParameters(2, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(3, "@Return_Type", this.ReturnType);
                    db.AddParameters(4, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(5, "@Gross_Amount", this.Gross);
                    db.AddParameters(6, "@Net_Amount", this.NetAmount);
                    db.AddParameters(7, "@Round_Off", this.RoundOff);
                    db.AddParameters(8, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Cargo_Id", this.CargoId);
                    db.AddParameters(11, "@Cartons", this.Carton);
                    db.AddParameters(12, "@Weight", this.Weight);
                    db.AddParameters(13, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(14, "@id", this.ID);
                    db.AddParameters(15, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(16 ,"@Job_Id", this.JobId);
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
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    dynamic setting = Application.Settings.GetFeaturedSettings(); //Gets All Settings from settings table
                    decimal net = 0;
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                    
                        bool exist;
                        query = @"select count(*) from TBL_PURCHASE_ENTRY_DETAILS ped left join TBL_PURCHASE_ENTRY_REGISTER pe on ped.Pe_Id=pe.Pe_Id where pe.Supplier_Id=" + SupplierId + " and ped.item_id=" + item.ItemID + "and ped.instance_id=" + item.InstanceId;

                        exist = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query)) > 0 ? true : false;
                         if (item.Quantity <= 0)

                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase return | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else if (!exist)
                        {
                            return new OutputMessage("The selected Item is not exist in the current supplier", false, Type.Others, "Purchase return | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId, this.LocationId);
                            if (setting.AllowPriceEditingInPurchaseReturn)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            dynamic IsNegBillingAllowed = Application.Settings.GetFeaturedSettings();
                            //Retrun from stock
                            if (item.ReturnFrom == 0)
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.Stock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Purchase Return | Save", System.Net.HttpStatusCode.InternalServerError);
                                }
                            }
                            //Retrun from Damage
                            else if (item.ReturnFrom == 1)
                            {
                                if (!IsNegBillingAllowed.AllowNegativeBilling && prod.DamageStock < item.Quantity)
                                {
                                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Purchase Return | Update", System.Net.HttpStatusCode.InternalServerError);
                                }
                               
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            net += prod.NetAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_RETURN_DETAILS(Pr_Id,item_id,Quantity,MRP,Rate,Tax_Id,Tax_Amount,Gross_Amount,
                                     Net_Amount,Remarks,Location_Id,Status,Return_from,instance_id)
                                     values(@Pr_Id,@item_id,@Quantity,@MRP,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                     @Remarks,@Location_Id,@Status,@Return_from,@instance_id)";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Pr_Id", this.ID);
                            db.AddParameters(1, "@item_id", item.ItemID);
                            db.AddParameters(2, "@Quantity", item.Quantity);
                            db.AddParameters(3, "@MRP", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", prod.Gross);
                            db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(9, "@Remarks", prod.Remarks);
                            db.AddParameters(10, "@Location_Id", this.LocationId);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Return_from", item.ReturnFrom);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                        }
                    }
                    decimal _NetAmount = 0;
                    if (Application.Settings.IsAutoRoundOff())
                    {
                        _NetAmount = Math.Round(net);
                        this.RoundOff = _NetAmount - net;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        this.NetAmount = Math.Round(net, 2);
                        _NetAmount = net + this.RoundOff;
                    }
                    else
                    {
                        _NetAmount = Math.Round(net);
                        this.RoundOff = _NetAmount - net;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_RETURN_REGISTER] set [Tax_amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + ID);
                }

                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", this.ID, this.ModifiedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Bill_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRT')[New_Order] from TBL_PURCHASE_RETURN_REGISTER where Pr_Id=" + ID);
                return new OutputMessage("Purchase return  " + dt.Rows[0]["Saved_No"].ToString()+ " has been Updated", true, Type.NoError, "Purchase Return | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
               
            }
            catch (Exception ex)
            {

                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase Return| Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Purchase Return could not be updated", false, Type.Others, "Purchase Return | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong.Purchase Return could not be updated", false, Type.Others, "Purchase Return | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// delete details of purchase return register and purchase return details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseReturn, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseReturn | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("Select an Entry for deletion", false, Type.RequiredFields, "PurchaseReturn| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_PURCHASE_RETURN_DETAILS where Pr_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_PURCHASE_RETURN_REGISTER where pr_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    Entities.Application.Helper.PostFinancials("TBL_PURCHASE_RETURN_REGISTER", this.ID, this.ModifiedBy);
                    return new OutputMessage("Purchase return deleted successfully", true, Type.NoError, "PurchaseReturn | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                       return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "PurchaseReturn | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseReturn | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    }
                    else
                    {
                        db.RollBackTransaction();
                       return new OutputMessage("Something went wrong. Purchase return could not be deleted", false, Type.Others, "Purchase Return | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
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


            if (!Entities.Security.Permissions.AuthorizeUser(userId, Security.BusinessModules.PurchaseQuote, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseReturn | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!toAddress.IsValidEmail())
            {
                return new OutputMessage("Email Address is not valid. Please Revert and try again", false, Type.InsufficientPrivilege, "Purchase Return | Send Mail", System.Net.HttpStatusCode.InternalServerError);
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
                mail.Subject = "Debit Note";
                mail.Body = "Please Find the attached copy of Debit Note";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Debit Note.pdf"));
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
                return new OutputMessage("Mail Sending Failed", false, Type.RequiredFields, "Purchase Return | Send Mail", System.Net.HttpStatusCode.InternalServerError,ex);
            }

        }

        /// <summary>
        /// Gets all the Purchase Return Register with list of Products
        /// </summary>
        /// <param name="LocationId">Location Id from where the return is  generated</param>
        /// <returns>list purchase return details</returns>  
        public static List<PurchaseReturnRegister> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pr.Pr_Id[PurchaseReturnId],pr.Location_Id,pr.Supplier_Id,isnull(pr.Return_Date,0) Return_Date,isnull(prd.Return_From,0) Return_From,pr.Return_Type,
                                    isnull(pr.Bill_No,0)[Bill_No],pr.Cargo_Id,pr.Cartons,pr.Gross_Amount,pr.Net_Amount,pr.Tax_Amount,isnull(pr.Narration,0)[Narration],
                                    isnull(pr.[Status],0)[Status],pr.Other_Charges,pr.Round_Off,pr.[Weight],isnull(prd.item_id,0) item_id,prd.Prd_Id,prd.Quantity,
                                    prd.MRP,prd.Rate[CostPrice],prd.Net_Amount[P_Net_Amount],prd.Gross_Amount[P_Gross_Amount],prd.Tax_Amount[P_Tax_Amount]
                                    ,isnull(l.Name,0)[Location],isnull(cmp.Name,0)[Company],tx.Percentage[TaxPercentage] ,isnull(sup.Name,0)[Supplier],
                                    isnull(it.Name,0)[Item],isnull(it.Item_Code,0)[Item_Code],it.Item_Id,prd.instance_id, 
                                    l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],
                                    cmp.Address1[Company_Address1],cmp.Address2[Company_Address2],cmp.Mobile_No1[Company_Phone],cmp.Reg_Id1[Company_RegId],
                                    sup.Address1[Supplier_Address1],sup.Address2[Supplier_Address2],sup.Phone1[Supplier_Phone],sup.Taxno1[Supplier_TaxNo],
									cmp.Logo[Comp_Logo],cmp.Email[Comp_Email],pr.TandC,pr.Payment_Terms
                                    from TBL_PURCHASE_RETURN_REGISTER pr with(nolock)
                                    left join TBL_PURCHASE_RETURN_DETAILS prd with(nolock) on prd.Pr_Id=pr.Pr_Id
                                    left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pr.Location_Id
                                    left join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                                    left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=prd.Tax_Id
                                    left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pr.Supplier_Id                                   
                                    left join TBL_ITEM_MST it with(nolock) on it.Item_Id=prd.Item_Id
                                    where pr.Location_Id=@Location_Id order by pr.Created_Date desc";
                #endregion query

                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseReturnRegister> result = new List<PurchaseReturnRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseReturnRegister register = new PurchaseReturnRegister();
                        register.ID = row["PurchaseReturnId"]!=DBNull.Value? Convert.ToInt32(row["PurchaseReturnId"]):0;
                        register.LocationId = row["Location_Id"]!=DBNull.Value? Convert.ToInt32(row["Location_Id"]):0;
                        register.SupplierId = row["supplier_id"]!=DBNull.Value? Convert.ToInt32(row["supplier_id"]):0;
                        register.EntryDateString = row["Return_Date"]!=DBNull.Value? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;                       
                        register.ReturnType = row["Return_Type"]!=DBNull.Value? Convert.ToInt32(row["Return_Type"]):0;
                        register.BillNo = Convert.ToString(row["Bill_No"]);
                        register.CargoId = row["Cargo_Id"]!=DBNull.Value?Convert.ToInt32(row["Cargo_Id"]):0;
                        register.Carton = row["Cartons"]!=DBNull.Value? Convert.ToInt32(row["Cartons"]):0;
                        register.TaxAmount = row["Tax_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Tax_Amount"]):0;
                        register.Gross = row["Gross_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Gross_Amount"]):0;
                        register.NetAmount = row["Net_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Net_Amount"]):0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.SupplierAddress1 = Convert.ToString(row["Supplier_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Supplier_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Supplier_Phone"]);
                        register.SupplierTaxNo = Convert.ToString(row["Supplier_TaxNo"]);
                        register.CompanyAddress1 = Convert.ToString(row["Company_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Company_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Company_Phone"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegId"]);
                        register.OtherCharges = row["Other_Charges"]!=DBNull.Value? Convert.ToDecimal(row["Other_Charges"]):0;
                        register.RoundOff = row["Round_Off"]!=DBNull.Value? Convert.ToDecimal(row["Round_Off"]):0;
                        register.Weight = row["Weight"]!=DBNull.Value? Convert.ToDecimal(row["Weight"]):0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseReturnId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.ItemID= rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.DetailsID = rowItem["prd_Id"]!=DBNull.Value? Convert.ToInt32(rowItem["prd_Id"]):0;
                            item.InstanceId = rowItem["instance_id"] !=DBNull.Value? Convert.ToInt32(rowItem["instance_id"]):0;
                            item.ReturnFrom = rowItem["return_from"] != DBNull.Value ? Convert.ToInt32(rowItem["return_from"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"]!=DBNull.Value? Convert.ToDecimal(rowItem["Mrp"]):0;
                            item.CostPrice = rowItem["CostPrice"]!=DBNull.Value? Convert.ToDecimal(rowItem["CostPrice"]):0;
                            item.TaxPercentage = rowItem["TaxPercentage"]!=DBNull.Value? Convert.ToDecimal(rowItem["TaxPercentage"]):0;
                            item.Gross = rowItem["P_Gross_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_Gross_Amount"]):0;
                            item.NetAmount = rowItem["P_Net_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_Net_Amount"]):0;
                            item.TaxAmount = rowItem["P_Tax_Amount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_Tax_Amount"]):0;
                            item.Quantity = rowItem["Quantity"]!=DBNull.Value? Convert.ToDecimal(rowItem["Quantity"]):0;
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
                Application.Helper.LogException(ex, "PurchaseReturn | GetDetails(int LocationId)");
                return null;
            }
            finally {
                db.Close();
}
        }

        public static List<PurchaseReturnRegister> GetDetails(int LocationId, int? SupplierId, DateTime? from, DateTime? to)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pr.Pr_Id[PurchaseReturnId],pr.Location_Id,pr.Supplier_Id,isnull(pr.Return_Date,0) Return_Date,isnull(prd.Return_From,0) Return_From,pr.Return_Type,
                                    isnull(pr.Bill_No,0)[Bill_No],pr.Cargo_Id,pr.Cartons,pr.Gross_Amount,pr.Net_Amount,pr.Tax_Amount,isnull(pr.Narration,0)[Narration],
                                    isnull(pr.[Status],0)[Status],pr.Other_Charges,pr.Round_Off,pr.[Weight],isnull(prd.item_id,0) item_id,prd.Prd_Id,prd.Quantity,
                                    prd.MRP,prd.Rate[CostPrice],prd.Net_Amount[P_Net_Amount],prd.Gross_Amount[P_Gross_Amount],prd.Tax_Amount[P_Tax_Amount]
                                    ,isnull(l.Name,0)[Location],isnull(cmp.Name,0)[Company],tx.Percentage[TaxPercentage] ,isnull(sup.Name,0)[Supplier],
                                    isnull(it.Name,0)[Item],isnull(it.Item_Code,0)[Item_Code],it.Item_Id,prd.instance_id, 
                                    l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],
                                    cmp.Address1[Company_Address1],cmp.Address2[Company_Address2],cmp.Mobile_No1[Company_Phone],cmp.Reg_Id1[Company_RegId],
                                    sup.Address1[Supplier_Address1],sup.Address2[Supplier_Address2],sup.Phone1[Supplier_Phone],sup.Taxno1[Supplier_TaxNo],
									cmp.Logo[Comp_Logo],cmp.Email[Comp_Email],isnull(pr.Cost_Center_Id,0)[Cost_Center_Id],isnull(pr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job]
                                     ,sup.email[Sup_Email],pr.TandC,pr.Payment_Terms from TBL_PURCHASE_RETURN_REGISTER pr with(nolock)
                                    left join TBL_PURCHASE_RETURN_DETAILS prd with(nolock) on prd.Pr_Id=pr.Pr_Id
                                    inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pr.Location_Id
                                    inner join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                                    inner join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=prd.Tax_Id
                                    inner join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pr.Supplier_Id                                   
                                    inner join TBL_ITEM_MST it with(nolock) on it.Item_Id=prd.Item_Id
									left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pr.Cost_Center_Id
									left join TBL_JOB_MST j on j.Job_Id=pr.Job_Id
                                    where pr.Location_Id=@Location_Id {#supplierfilter#} {#daterangefilter#} order by pr.Bill_No desc";
                #endregion query
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
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", from);
                db.AddParameters(2, "@todate", to);
                db.AddParameters(3, "@Supplier_Id", SupplierId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseReturnRegister> result = new List<PurchaseReturnRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseReturnRegister register = new PurchaseReturnRegister();
                        register.ID = row["PurchaseReturnId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseReturnId"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.EntryDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.ReturnType = row["Return_Type"] != DBNull.Value ? Convert.ToInt32(row["Return_Type"]) : 0;
                        register.BillNo = Convert.ToString(row["Bill_No"]);
                        register.CargoId = row["Cargo_Id"] != DBNull.Value ? Convert.ToInt32(row["Cargo_Id"]) : 0;
                        register.Carton = row["Cartons"] != DBNull.Value ? Convert.ToInt32(row["Cartons"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CompanyEmail = Convert.ToString(row["Comp_Email"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.SupplierAddress1 = Convert.ToString(row["Supplier_Address1"]);
                        register.SupplierAddress2 = Convert.ToString(row["Supplier_Address2"]);
                        register.SupplierPhone = Convert.ToString(row["Supplier_Phone"]);
                        register.SupplierTaxNo = Convert.ToString(row["Supplier_TaxNo"]);
                        register.CompanyAddress1 = Convert.ToString(row["Company_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Company_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Company_Phone"]);
                        register.SupplierEmail = Convert.ToString(row["Sup_Email"]);
                        register.CompanyRegId = Convert.ToString(row["Company_RegId"]);
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.Weight = row["Weight"] != DBNull.Value ? Convert.ToDecimal(row["Weight"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseReturnId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.DetailsID = rowItem["prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["prd_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.ReturnFrom = rowItem["return_from"] != DBNull.Value ? Convert.ToInt32(rowItem["return_from"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseReturn | GetDetails(int LocationId, int? SupplierId, DateTime? from, DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static PurchaseReturnRegister GetDetails(int Id,int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pr.Pr_Id[PurchaseReturnId],pr.Location_Id,pr.Supplier_Id,isnull(pr.Return_Date,0) Return_Date,isnull(prd.Return_From,0) Return_From,pr.Return_Type,
                                isnull(pr.Bill_No,0)[Bill_No],pr.Cargo_Id,pr.Cartons,pr.Gross_Amount,pr.Net_Amount,pr.Tax_Amount,isnull(pr.Narration,0)[Narration],
                                isnull(pr.[Status],0)[Status],pr.Other_Charges,pr.Round_Off,pr.[Weight],isnull(prd.item_id,0) item_id,prd.Prd_Id,prd.Quantity,
                                prd.MRP,prd.Rate[CostPrice],prd.Net_Amount[P_Net_Amount],prd.Gross_Amount[P_Gross_Amount],prd.Tax_Amount[P_Tax_Amount]
                                ,isnull(l.Name,0)[Location],isnull(cmp.Name,0)[Company],tx.Percentage[TaxPercentage] ,isnull(sup.Name,0)[Supplier],
                                isnull(it.Name,0)[Item],isnull(it.Item_Code,0)[Item_Code],it.Item_Id,prd.instance_id, 
                                l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],
                                sup.Taxno1[Supplier_TaxNo]
								,cmp.Email[Comp_Email],coun.Name[Sup_Country],st.Name[Sup_State],
                                isnull(pr.Cost_Center_Id,0)[Cost_Center_Id],isnull(pr.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job],pr.TandC,pr.Payment_Terms,
                                pr.Salutation,pr.Contact_Name,pr.Contact_Address1,pr.Contact_Address2,pr.Contact_City,pr.State_ID,pr.Country_ID,
                                pr.Contact_Zipcode,pr.Contact_Phone1,pr.Contact_Phone2,pr.Contact_Email
								from TBL_PURCHASE_RETURN_REGISTER pr with(nolock)
                                left join TBL_PURCHASE_RETURN_DETAILS prd with(nolock) on prd.Pr_Id=pr.Pr_Id
                                inner join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pr.Location_Id
                                inner join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                                inner join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=prd.Tax_Id
								left join TBL_COUNTRY_MST coun with(nolock) on coun.Country_Id=pr.Country_Id
                                left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pr.Supplier_Id 
								left join TBL_STATE_MST st with(nolock) on st.State_Id=pr.State_Id                                  
                                left join TBL_ITEM_MST it with(nolock) on it.Item_Id=prd.Item_Id
                                left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pr.Cost_Center_Id
							    left join TBL_JOB_MST j on j.Job_Id=pr.Job_Id
                                where pr.Location_Id=@Location_Id and pr.Pr_Id=@Pr_Id order by pr.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Pr_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    
                        DataRow row = dt.Rows[0];
                        PurchaseReturnRegister register = new PurchaseReturnRegister();
                        register.ID = row["PurchaseReturnId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseReturnId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
                        register.EntryDateString = row["Return_Date"] != DBNull.Value ? Convert.ToDateTime(row["Return_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.ReturnType = row["Return_Type"] != DBNull.Value ? Convert.ToInt32(row["Return_Type"]) : 0;
                        register.BillNo = Convert.ToString(row["Bill_No"]);
                        register.CargoId = row["Cargo_Id"] != DBNull.Value ? Convert.ToInt32(row["Cargo_Id"]) : 0;
                        register.Carton = row["Cartons"] != DBNull.Value ? Convert.ToInt32(row["Cartons"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Status = Convert.ToInt32(row["Status"]);
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.Weight = row["Weight"] != DBNull.Value ? Convert.ToDecimal(row["Weight"]) : 0;
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.SupplierTaxNo = Convert.ToString(row["Supplier_TaxNo"]);
                        register.Supplier = Convert.ToString(row["Supplier"]);
                        register.TermsandConditon = Convert.ToString(row["TandC"]);
                        register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
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
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseReturnId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.DetailsID = rowItem["prd_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["prd_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.ReturnFrom = rowItem["return_from"] != DBNull.Value ? Convert.ToInt32(rowItem["return_from"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["P_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["P_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_Tax_Amount"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseReturn | GetDetails(int Id,int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}

