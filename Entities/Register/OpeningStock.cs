using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
 public class OpeningStock : Register, IRegister
    {
        #region Properties
        public int SupplierId { get; set; }
        public string Supplier { get; set; }
        public string EntryNo { get; set; }
        public string FinancialYear { get; set; }
        public bool IsMigrated { get; set; }
        public string InvoiceDateString { get; set; }
        public int Status { get; set; }
        public List<Item> Products { get; set; }
        public override decimal NetAmount { get; set; }
        #endregion Properties
        public OpeningStock(int ID, int UserId)
        {
            this.ID = ID;
            this.ModifiedBy = UserId;
        }
        public OpeningStock(int ID)
        {
            this.ID = ID;
        }
        public OpeningStock()
        {

        }
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
                if (this.EntryDate.Year < 1900)
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
                    string query = @"insert into TBL_PURCHASE_ENTRY_REGISTER (Location_Id, Supplier_Id,Entry_Date,Total_TaxAmount,Total_GrossAmount,Discount_Amount,  
                                   Other_Charges,Round_Off,Net_Amount,Narration,[Status],Created_By,Created_Date,Entry_No,Is_Migrated) 
                                   values(@Location_Id, @Supplier_Id,@Entry_Date,@Total_TaxAmount,@Total_GrossAmount,@Discount_Amount,@Other_Charges,  
                                   @Round_Off,@Net_Amount,@Narration,@Status,@Created_By,GETUTCDATE(),[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PEN'),@Is_Migrated);select @@identity;";

                    db.CreateParameters(14);
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
                    db.AddParameters(13, "@Is_Migrated", this.IsMigrated);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PEN", db);

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
                            item.TaxAmount = item.CostPrice * (prod.TaxPercentage / 100);
                            item.Gross = item.Quantity * item.CostPrice;
                            item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_ENTRY_DETAILS(Pe_Id,Item_Id,Qty,Mrp,Rate,Selling_Price,Tax_Id,
                                    Tax_Amount,Gross_Amount,Net_Amount,Status,Pqd_Id,instance_id)
                                    values(@Pe_Id,@Item_Id,@Qty,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Status,@Pqd_Id,@instance_id)";
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Ped_Id", item.DetailsID);//***
                            db.AddParameters(1, "@Pe_Id", identity);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", item.MRP);
                            db.AddParameters(5, "@Rate", item.CostPrice);
                            db.AddParameters(6, "@Selling_Price", prod.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", prod.TaxId);
                            db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", item.Gross);
                            db.AddParameters(10, "@Net_Amount", item.NetAmount);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Pqd_Id", item.QuoteDetailId);
                            db.AddParameters(13, "@instance_id", item.InstanceId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount * item.Quantity;
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
                    if (Application.Settings.IsAutoRoundOff())
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        _NetAmount = this.NetAmount + this.RoundOff;
                    }
                    else
                    {
                        this.RoundOff = this.NetAmount - _NetAmount;
                        _NetAmount = Math.Round(this.NetAmount);
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_ENTRY_REGISTER] set [Total_TaxAmount]=" + this.TaxAmount + ", [Total_GrossAmount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pe_Id=" + identity);
                }
                db.CommitTransaction();
                Entities.Application.Helper.PostFinancials("TBL_PURCHASE_ENTRY_REGISTER", identity, this.CreatedBy);
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PEN')[New_Order] from tbl_purchase_entry_register where Pe_Id=" + identity);
                return new OutputMessage("Purchase entry has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Entry | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong.could not save Entry", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError,ex);
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
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SetOpeningMenu | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make an Opening Menu.", false, Type.Others, "SetOpeningMenu | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"delete from TBL_PURCHASE_ENTRY_DETAILS where Pe_Id=@Id;
                                   update TBL_PURCHASE_ENTRY_REGISTER set Location_Id=@Location_Id,Supplier_Id=@Supplier_Id,Entry_Date=@Entry_Date,Total_TaxAmount=@Total_TaxAmount,
                                   Total_GrossAmount=@Total_GrossAmount,Discount_Amount=@Discount_Amount,Other_Charges=@Other_Charges,Round_Off=@Round_Off,Net_Amount=@Net_Amount,Narration=@Narration,[Status]=@Status,
                                   Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Pe_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(13);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Supplier_Id", this.SupplierId);
                    db.AddParameters(2, "@Entry_Date", this.EntryDate);
                    db.AddParameters(3, "@Total_TaxAmount", this.TaxAmount);
                    db.AddParameters(4, "@Total_GrossAmount", this.Gross);
                    db.AddParameters(5, "@Discount_Amount", this.Discount);
                    db.AddParameters(6, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(7, "@Round_Off", this.RoundOff);
                    db.AddParameters(8, "@Net_Amount", this.NetAmount);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Status", this.Status);
                    db.AddParameters(11, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(12, "@Id", this.ID);
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                   
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "SetOpeningMenu | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {

                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            item.TaxAmount = item.CostPrice * (prod.TaxPercentage / 100);
                            item.Gross = item.Quantity * item.CostPrice;
                            item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                            db.CleanupParameters();
                            query = @"insert into TBL_PURCHASE_ENTRY_DETAILS(Pe_Id,Item_Id,Qty,Mrp,Rate,Selling_Price,Tax_Id,
                                    Tax_Amount,Gross_Amount,Net_Amount,Status,Pqd_Id)
                                    values(@Pe_Id,@Item_Id,@Qty,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,
                                    @Gross_Amount,@Net_Amount,@Status,@Pqd_Id)";
                            db.CreateParameters(13);
                            db.AddParameters(0, "@Ped_Id", item.DetailsID);
                            db.AddParameters(1, "@Pe_Id", ID);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", item.MRP);
                            db.AddParameters(5, "@Rate", item.CostPrice);
                            db.AddParameters(6, "@Selling_Price", item.SellingPrice);
                            db.AddParameters(7, "@Tax_Id", item.TaxId);
                            db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(9, "@Gross_Amount", item.Gross);
                            db.AddParameters(10, "@Net_Amount", item.NetAmount);
                            db.AddParameters(11, "@Status", 0);
                            db.AddParameters(12, "@Pqd_Id", item.QuoteDetailId);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount * item.Quantity;
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
                    if (Application.Settings.IsAutoRoundOff())
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }

                    else if (this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        _NetAmount = this.NetAmount + this.RoundOff;
                    }
                    else
                    {
                        this.RoundOff = this.NetAmount - _NetAmount;
                        _NetAmount = Math.Round(this.NetAmount);
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_ENTRY_REGISTER] set [Total_TaxAmount]=" + this.TaxAmount + ", [Total_GrossAmount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pe_Id=" + ID);
                }
                db.CommitTransaction();
                return new OutputMessage("Opening Menu has been Updated", true, Type.NoError, "SetOpeningMenu | Update", System.Net.HttpStatusCode.OK, ID.ToString());
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this menu because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "SetOpeningMenu | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.could not update menu", false, Type.Others, "SetOpeningMenu | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();
                return new OutputMessage("Something went wrong.could not update Opening Menu", false, Type.Others, "SetOpeningMenu | Update", System.Net.HttpStatusCode.InternalServerError,ex);
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
                #region query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],
                               isnull(pe.Entry_Date,0)Entry_Date,isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount],
                               isnull(pe.Discount_Amount,0)[Discount_Amount],pe.Entry_No,isnull(pe.Other_Charges,0)[Other_Charges],isnull(pe.Round_Off,0)[Round_Off],
                               isnull(pe.Net_Amount,0)[Net_Amount],pe.Narration,isnull(pe.[Status],0)[Status],isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],
                               isnull(ped.Item_Id,0)[Item_Id],isnull(ped.Pqd_Id,0)[Pqd_Id],isnull(ped.Qty,0)[Qty],isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],
                               isnull(ped.Selling_Price,0)[Selling_Price],isnull(ped.Tax_Amount,0)[P_Tax_Amount],isnull(ped.Gross_Amount,0)[P_Gross_Amount],
                               isnull(ped.Net_Amount,0)[P_Net_Amount],l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],sup.Name[Supplier],
                               it.Item_Id,it.Item_Code,it.Name[Item]
                               from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               left join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               left join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               where pe.Location_Id=@Location_Id and pe.Is_Migrated=1 order by pe.Created_Date desc";
                #endregion query
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
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
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
                Application.Helper.LogException(ex, "OpeningStock | GetDetails(int LocationId)");
                return null;
            }
        }

        public static List<PurchaseEntryRegister> GetDetails(int LocationId, int? supplierId, DateTime? From, DateTime? To)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],
                               isnull(pe.Entry_Date,0)Entry_Date,isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount],
                               isnull(pe.Discount_Amount,0)[Discount_Amount],pe.Entry_No,isnull(pe.Other_Charges,0)[Other_Charges],isnull(pe.Round_Off,0)[Round_Off],
                               isnull(pe.Net_Amount,0)[Net_Amount],pe.Narration,isnull(pe.[Status],0)[Status],isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],
                               isnull(ped.Item_Id,0)[Item_Id],isnull(ped.Pqd_Id,0)[Pqd_Id],isnull(ped.Qty,0)[Qty],isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],
                               isnull(ped.Selling_Price,0)[Selling_Price],isnull(ped.Tax_Amount,0)[P_Tax_Amount],isnull(ped.Gross_Amount,0)[P_Gross_Amount],
                               isnull(ped.Net_Amount,0)[P_Net_Amount],l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],sup.Name[Supplier],
                               it.Item_Id,it.Item_Code,it.Name[Item]
                               from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               left join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               left join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               where pe.Location_Id=@Location_Id and pe.Is_Migrated=1 {#supplierfilter#} {#daterangefilter#} order by pe.Created_Date desc";
                #endregion query
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and pe.Entry_Date>=@fromdate and pe.Entry_Date<=@todate ");
                }
                else
                {
                    To = DateTime.UtcNow;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and pe.Entry_Date>=@fromdate and pe.Entry_Date<=@todate ");
                }
                if (supplierId != null && supplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and pe.supplier_id=@supplierId ");
                }
                else
                {

                    query = query.Replace("{#supplierfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.AddParameters(3, "@supplierId", supplierId);
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
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;

                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
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
                Application.Helper.LogException(ex, "OpeningStock | GetDetails(int LocationId, int? supplierId, DateTime? From, DateTime? To)");
                return null;
            }
        }

        public static PurchaseEntryRegister GetDetails(int Id,int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select pe.Pe_Id[PurchaseEntryId],isnull(pe.Location_Id,0)[Location_Id],isnull(pe.Supplier_Id,0)[Supplier_Id],isnull(pe.Entry_Date,0)Entry_Date,
                               isnull(pe.Total_GrossAmount,0)[Total_GrossAmount],isnull(pe.Total_TaxAmount,0)[Total_TaxAmount],isnull(pe.Discount_Amount,0)[Discount_Amount],
                               pe.Entry_No,isnull(pe.Other_Charges,0)[Other_Charges],isnull(pe.Round_Off,0)[Round_Off],isnull(pe.Net_Amount,0)[Net_Amount],
                               pe.Narration,isnull(pe.[Status],0)[Status],isnull(ped.Ped_Id,0)[Ped_Id],isnull(ped.Pe_Id,0)[PurchaseEntryId],isnull(ped.Item_Id,0)[Item_Id],
                               isnull(ped.Pqd_Id,0)[Pqd_Id],isnull(ped.Qty,0)[Qty],isnull(ped.Mrp,0)[Mrp],isnull(ped.Rate,0)[Cost_Price],isnull(ped.Selling_Price,0)[Selling_Price],
                               isnull(ped.Tax_Amount,0)[P_Tax_Amount],isnull(ped.Gross_Amount,0)[P_Gross_Amount],isnull(ped.Net_Amount,0)[P_Net_Amount],
                               l.Name[Location],cmp.Name[Company],isnull(tx.Percentage,0)[Tax_Percentage],sup.Name[Supplier],
                               it.Item_Id,it.Item_Code,it.Name[Item]
                               from TBL_PURCHASE_ENTRY_REGISTER pe with(nolock)
                               left join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Pe_Id=pe.Pe_Id
                               left join TBL_LOCATION_MST l with(nolock) on l.Location_Id=pe.Location_Id
                               left join TBL_COMPANY_MST cmp with(nolock) on cmp.Company_Id=l.Company_Id
                               left join TBL_TAX_MST tx with(nolock) on tx.Tax_Id=ped.Tax_Id
                               left join TBL_SUPPLIER_MST sup with(nolock) on sup.Supplier_Id=pe.Supplier_Id
                               left join TBL_ITEM_MST it with(nolock) on it.Item_Id=ped.Item_Id
                               where pe.Location_Id=@Location_Id and pe.Pe_Id=@Pe_Id and pe.Is_Migrated=1 order by pe.Created_Date desc";
                #endregion query

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
                        register.SupplierId = row["supplier_id"] != DBNull.Value ? Convert.ToInt32(row["supplier_id"]) : 0;
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
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseEntryId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
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
                    return register;
                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "OpeningStock | GetDetails(int Id,int LocationId)");
                return null;
            }
        }
        /// <summary>
        /// delete details of purchase entry register and purchase entry details
        /// </summary>
        /// <returns></returns>

        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseEntry, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SetOpeningMenu | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "SetOpeningMenu| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_PURCHASE_ENTRY_DETAILS where Pe_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_PURCHASE_ENTRY_REGISTER where pe_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Opening menu has been deleted", true, Type.NoError, "SetOpeningMenu | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "SetOpeningMenu | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "SetOpeningMenu | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.could not delete Opening menu", false, Type.Others, "SetOpeningMenu | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
        }

       

    }
}
