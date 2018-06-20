using Core.DBManager;
using Entities.Register;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.WareHousing
{
    public class TransferOut : Register.Register, IRegister
    {
        #region Properties
        public int ToLocation { get; set; }
        public int FromLocation { get; set; }
        public string FinancialYear { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public string LocationRegId { get; set; }
        public int Status { get; set; }
        public int CostCenterId { get; set; }
        public int JobId { get; set; }
        public string EntryNo { get; set; }
        public string TermsAndConditions { get; set; }
        public List<Item> Products { get; set; }
        #endregion Properties
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.TransferOut, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TranferOut | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;

                //Main Validations. Use ladder-if after this "if" for more validations
               if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a TransferOut.", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError);

                }
               else if(this.LocationId==this.ToLocation)
                {
                    return new OutputMessage("Please select another location", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError);
                }
             else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a TransferOut.", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError);
                }
             else if(this.ToLocation==0)
                {
                    return new OutputMessage("Select Location to make a TransferOut", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError);
                }
       
                else
                {
                    db.Open();
                    string query= @"insert into TBL_TRANSFER_OUT_REGISTER(To_Location,Location, Entry_Date, Total_Tax_Amount,
	                              Total_Gross_Amount, Total_Net_Amount, Discount_Amount, Other_Charges, Round_Off, Narration, Entry_No,
	                              [Status], Created_By, Created_Date,Cost_Center_Id,Job_Id,Terms_And_Conditions)
                                  values(@To_Location,@Location,@Entry_Date,@Total_Tax_Amount,
	                              @Total_Gross_Amount,@Total_Net_Amount,@Discount_Amount,@Other_Charges,@Round_Off,@Narration,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TOT'),@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@Terms_And_Conditions); select @@identity;";
                    db.CreateParameters(15);
                    db.AddParameters(0, "@To_Location", this.ToLocation);
                    db.AddParameters(1, "@Location", this.LocationId);
                    db.AddParameters(2, "@Entry_Date", this.EntryDate);
                    db.AddParameters(3, "@Total_Tax_Amount", this.TaxAmount);
                    db.AddParameters(4, "@Total_Gross_Amount", this.Gross);
                    db.AddParameters(5, "@Total_Net_Amount", this.NetAmount);
                    db.AddParameters(6, "@Discount_Amount", this.Discount);
                    db.AddParameters(7, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Status", this.Status);
                    db.AddParameters(11, "@Created_By", this.CreatedBy);                  
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.AddParameters(14, "@Terms_And_Conditions", this.TermsAndConditions);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "TOT", db);
                   
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations


                        Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId, this.LocationId);
                        if (prod.Stock < item.Quantity)
                        {
                            return new OutputMessage("Quantity Exceeds", false, Type.Others, "TransferOut|Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        item.TaxPercentage = prod.TaxPercentage;
                            item.TaxId = prod.TaxId;
                            item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                            item.Gross = item.Quantity * prod.CostPrice;
                            item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                            db.CleanupParameters();
                            query = @"insert into TBL_TRANSFER_OUT_DETAILS (Transfer_Out_Id, item_id, Quantity, Mrp, Rate, Selling_Price, 
                                    Tax_Id, Tax_Amount, Gross_Amount, Net_Amount,[Status],[instance_id])
                                    values(@Transfer_Out_Id,@item_id,@Quantity,@Mrp,@Rate,@Selling_Price,
                                   @Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@instance_id)";
                            db.CreateParameters(12);
                            db.AddParameters(0, "@Transfer_Out_Id", identity);
                            db.AddParameters(1, "@item_id", item.ItemID);
                            db.AddParameters(2, "@Quantity", item.Quantity);
                            db.AddParameters(3, "@Mrp", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Selling_Price", prod.SellingPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", item.Gross);
                            db.AddParameters(9, "@Net_Amount", item.NetAmount);
                            db.AddParameters(10, "@Status", 0);
                            db.AddParameters(11, "@instance_id", item.InstanceId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount * item.Quantity;
                            this.Gross += item.Gross;
                        

                    }
                    decimal _NetAmount = Math.Round(this.NetAmount);
                    this.RoundOff = this.NetAmount - _NetAmount;
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_TRANSFER_OUT_REGISTER] set [Total_Tax_Amount]=" + this.TaxAmount + ", [Total_Gross_Amount]=" + this.Gross + " ,[Total_Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Transfer_Out_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TOT')[New_Order] from TBL_TRANSFER_OUT_REGISTER where Transfer_Out_Id=" + identity);
                return new OutputMessage("Transfer Out registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "TransferOut | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Transfer Out could not be saved  ", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }

        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.TransferOut, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TransferOut | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a TransferOut.", false, Type.Others, "TransferOut | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if(this.LocationId==0)
                {
                    return new OutputMessage("Select Location to make a TransferOut", false, Type.Others, "TransferOut | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if(this.LocationId==this.ToLocation)
                {
                    return new OutputMessage("Select another location for Transfer", false, Type.Others, "Transfer | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "RegisterOut Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"delete from [TBL_TRANSFER_OUT_DETAILS] where Transfer_Out_Id=@Id;
                                  Update TBL_TRANSFER_OUT_REGISTER set To_Location=@To_Location,Location=@Location,Entry_Date=@Entry_Date,
                                  Total_Tax_Amount=@Total_Tax_Amount,Total_Gross_Amount=@Total_Gross_Amount,Total_Net_Amount=@Total_Net_Amount,
                                  Discount_Amount=@Discount_Amount,Other_Charges=@Other_Charges,Round_Off=@Round_Off,Narration=@Narration,
                                  [Status]=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),
                                  Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,Terms_And_Conditions=@Terms_And_Conditions where Transfer_Out_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(16);
                    db.AddParameters(0, "@To_Location", this.ToLocation);
                    db.AddParameters(1, "@Location", this.LocationId);
                    db.AddParameters(2, "@Entry_Date", this.EntryDate);
                    db.AddParameters(3, "@Total_Tax_Amount", this.TaxAmount);
                    db.AddParameters(4, "@Total_Gross_Amount", this.Gross);
                    db.AddParameters(5, "@Total_Net_Amount", this.NetAmount);
                    db.AddParameters(6, "@Discount_Amount", this.Discount);
                    db.AddParameters(7, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Status", this.Status);
                    db.AddParameters(11, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(12, "@Id", this.ID);
                    db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(14, "@Job_Id", this.JobId);
                    db.AddParameters(15, "@Terms_And_Conditions", TermsAndConditions);
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                    
                    foreach (Item item in Products)
                    {

                        Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId,this.LocationId);

                        if (prod.Stock < item.Quantity)
                        {
                            return new OutputMessage("Quantity Exceeds", false, Type.Others, "Damage |Save", System.Net.HttpStatusCode.InternalServerError);
                        }

                            item.TaxPercentage = prod.TaxPercentage;
                            item.TaxId =prod.TaxId;
                            item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                            item.Gross = item.Quantity * prod.CostPrice;
                            item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                            db.CleanupParameters();
                            query = @"insert into TBL_TRANSFER_OUT_DETAILS (Transfer_Out_Id, item_id, Quantity, Mrp, Rate, Selling_Price, 
                                    Tax_Id, Tax_Amount, Gross_Amount, Net_Amount,[Status],instance_id)
                                    values(@Transfer_Out_Id,@itemId,@Quantity,@Mrp,@Rate,@Selling_Price,
                                   @Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@instance_id)";
                            db.CreateParameters(12);
                            db.AddParameters(0, "@Transfer_Out_Id", ID);
                            db.AddParameters(1, "@itemId", item.ItemID);
                            db.AddParameters(2, "@Quantity",item.Quantity);
                            db.AddParameters(3, "@Mrp", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Selling_Price", prod.SellingPrice);
                            db.AddParameters(6, "@Tax_Id", item.TaxId);
                            db.AddParameters(7, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", item.Gross);
                            db.AddParameters(9, "@Net_Amount", item.NetAmount);
                            db.AddParameters(10, "@Status", 0);
                            db.AddParameters(11, "@instance_id", item.InstanceId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount * item.Quantity;
                            this.Gross += item.Gross;
                       }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_TRANSFER_OUT_REGISTER] set [Total_Tax_Amount]=" + this.TaxAmount + ", [Total_Gross_Amount]=" + this.Gross + " ,[Total_Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Transfer_Out_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TOT')[New_Order] from TBL_TRANSFER_OUT_REGISTER where Transfer_Out_Id=" + this.ID);
                return new OutputMessage("Transfer Out Updated successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "TransferOut | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
              
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;

                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "TransferOut | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Transfer Out could not be updated ", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Transfer Out could not be updated", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }
            finally
            {
                db.Close();
            }
      }

        public static List<TransferOut> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_Out_Id,isnull(tr.To_Location,0)[To_Location],isnull(tr.Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Total_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Total_Gross_Amount],isnull(tr.Total_Net_Amount,0)[Total_Net_Amount],
                               isnull(tr.Discount_Amount,0)[Discount_Amount],isnull(tr.Other_Charges,0)[Other_Charges],isnull(tr.Round_Off,0)[Round_Off],isnull(tr.Narration,0)[Narration],
                               isnull(tr.Entry_No,0)[Entry_No],isnull(tr.Status,0)[Status],trd.Tod_Id,trd.item_Id,trd.instance_id,trd.Quantity,
                               isnull(trd.Mrp,0)[Mrp],isnull(trd.Rate,0)[Cost_Price],isnull(trd.Selling_Price,0)[Selling_Price],isnull(trd.Tax_Amount,0)[Tax_Amount],
                               isnull(trd.Gross_Amount,0)[Gross_Amount],isnull(trd.Net_Amount,0)[Net_Amount],l.Name[Location],isnull(t.Percentage,0)[Tax_Percentage],i.Name[Item],i.Item_Code,
							   tr.Cost_Center_Id,tr.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name 
							   from TBL_TRANSFER_OUT_REGISTER tr 
                               join TBL_TRANSFER_OUT_DETAILS trd on trd.Transfer_Out_Id=tr.Transfer_Out_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.Location
                               left join TBL_TAX_MST t on t.Tax_Id=trd.Tax_Id							  
							   left join TBL_ITEM_MST i on i.Item_Id=trd.Item_Id
							   left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
							   left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
                               where tr.Location =@Location_Id  order by tr.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<TransferOut> result = new List<TransferOut>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        TransferOut register = new TransferOut();
                        register.ID = row["Transfer_Out_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_Out_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = Convert.ToInt32(row["From_Location"]);
                        register.ToLocation = row["To_Location"] != DBNull.Value ? Convert.ToInt32(row["To_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_Out_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
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
                Application.Helper.LogException(ex, "transferout |  GetDetails(int LocationId)");
                return null;
            }
        }

        public static List<TransferOut> GetDetails(int LocationId, DateTime? From, DateTime? To)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_Out_Id,isnull(tr.To_Location,0)[To_Location],isnull(tr.Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Total_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Total_Gross_Amount],isnull(tr.Total_Net_Amount,0)[Total_Net_Amount],
                               isnull(tr.Discount_Amount,0)[Discount_Amount],isnull(tr.Other_Charges,0)[Other_Charges],isnull(tr.Round_Off,0)[Round_Off],isnull(tr.Narration,0)[Narration],
                               isnull(tr.Entry_No,0)[Entry_No],isnull(tr.Status,0)[Status],trd.Tod_Id,trd.item_Id,trd.instance_id,trd.Quantity,
                               isnull(trd.Mrp,0)[Mrp],isnull(trd.Rate,0)[Cost_Price],isnull(trd.Selling_Price,0)[Selling_Price],isnull(trd.Tax_Amount,0)[Tax_Amount],
                               isnull(trd.Gross_Amount,0)[Gross_Amount],isnull(trd.Net_Amount,0)[Net_Amount],l.Name[Location],isnull(t.Percentage,0)[Tax_Percentage],i.Name[Item],i.Item_Code, 
							   tr.Cost_Center_Id,tr.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name,
                               l.Address1[Loc_address1],l.Address2[Loc_address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],tr.Terms_And_Conditions
                               from TBL_TRANSFER_OUT_REGISTER tr 
                               join TBL_TRANSFER_OUT_DETAILS trd on trd.Transfer_Out_Id=tr.Transfer_Out_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.Location
                               left join TBL_TAX_MST t on t.Tax_Id=trd.Tax_Id							  
							   left join TBL_ITEM_MST i on i.Item_Id=trd.Item_Id
                               left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
							   left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
                               where tr.Location =@Location_Id {#daterangefilter#} order by tr.Created_Date desc";
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and tr.Entry_Date>=@fromdate and tr.Entry_Date<=@todate ");
                }
                else
                {
                    To = DateTime.Now;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and tr.Entry_Date>=@fromdate and tr.Entry_Date<=@todate ");
                }
                db.CreateParameters(3);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<TransferOut> result = new List<TransferOut>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        TransferOut register = new TransferOut();
                        register.ID = row["Transfer_Out_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_Out_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = Convert.ToInt32(row["From_Location"]);
                        register.ToLocation = row["To_Location"] != DBNull.Value ? Convert.ToInt32(row["To_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_address1"]);
                        register.TermsAndConditions = Convert.ToString(row["Terms_And_Conditions"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;

                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_Out_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
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
                Application.Helper.LogException(ex, "transferout |  GetDetails(int LocationId, DateTime? From, DateTime? To)");
                return null;
            }
        }

        public static TransferOut GetDetails(int Id,int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_Out_Id,isnull(tr.To_Location,0)[To_Location],isnull(tr.Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Total_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Total_Gross_Amount],isnull(tr.Total_Net_Amount,0)[Total_Net_Amount],
                               isnull(tr.Discount_Amount,0)[Discount_Amount],isnull(tr.Other_Charges,0)[Other_Charges],isnull(tr.Round_Off,0)[Round_Off],isnull(tr.Narration,0)[Narration],
                               isnull(tr.Entry_No,0)[Entry_No],isnull(tr.Status,0)[Status],trd.Tod_Id,trd.item_Id,trd.instance_id,trd.Quantity,
                               isnull(trd.Mrp,0)[Mrp],isnull(trd.Rate,0)[Cost_Price],isnull(trd.Selling_Price,0)[Selling_Price],isnull(trd.Tax_Amount,0)[Tax_Amount],
                               isnull(trd.Gross_Amount,0)[Gross_Amount],isnull(trd.Net_Amount,0)[Net_Amount],l.Name[Location],isnull(t.Percentage,0)[Tax_Percentage],i.Name[Item],i.Item_Code,
                               tr.Cost_Center_Id,tr.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name,l.Name[Location],l.Address1[Loc_Address1],
                               l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],tr.Terms_And_conditions
							   from TBL_TRANSFER_OUT_REGISTER tr 
                               join TBL_TRANSFER_OUT_DETAILS trd on trd.Transfer_Out_Id=tr.Transfer_Out_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.Location
                               left join TBL_TAX_MST t on t.Tax_Id=trd.Tax_Id							  
							   left join TBL_ITEM_MST i on i.Item_Id=trd.Item_Id
                               left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
							   left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
                               where tr.Location=@Location_Id and tr.Transfer_Out_Id=@Transfer_Out_Id order by tr.Created_Date desc";
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Transfer_Out_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                   
                        DataRow row = dt.Rows[0];
                        TransferOut register = new TransferOut();
                        register.ID = row["Transfer_Out_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_Out_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = Convert.ToInt32(row["From_Location"]);
                        register.ToLocation = row["To_Location"] != DBNull.Value ? Convert.ToInt32(row["To_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.Discount = row["Discount_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Amount"]) : 0;
                        register.OtherCharges = row["Other_Charges"] != DBNull.Value ? Convert.ToDecimal(row["Other_Charges"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.TermsAndConditions = Convert.ToString(row["Terms_And_Conditions"]);
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_Out_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Cost_Price"]) : 0;
                            item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(rowItem["Selling_Price"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percentage"]) : 0;
                            item.Gross = rowItem["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross_Amount"]) : 0;
                            item.NetAmount = rowItem["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Net_Amount"]) : 0;
                            item.TaxAmount = rowItem["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Amount"]) : 0;
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
                Application.Helper.LogException(ex, "transferout | GetDetails(int Id,int LocationId)");
                return null;
            }
        }

        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.TransferOut, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TransferOut | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "TransferOut| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (HasReference(this.ID))
            {
                
                return new OutputMessage("Cannot Delete. This request is alredy in a transaction", false, Type.Others, "RegisterOut Request | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_TRANSFER_OUT_DETAILS where Transfer_Out_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_TRANSFER_OUT_REGISTER where Transfer_Out_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Transfer out has been deleted", true, Type.NoError, "TransferOut | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "TransferOut | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "TransferOut | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Transfer Out could not be saved", false, Type.Others, "TransferOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
   }

        /// <summary>
        /// check for the Transfer out is refered in another transactions
        /// </summary>
        /// <param name="transferoutId (id)"></param>
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
                    string query = @"select count(*) from [TBL_TRANSFER_OUT_REGISTER] tor join 
                                   [TBL_TRANSFER_OUT_DETAILS] tod on tod.Transfer_Out_Id=tor.Transfer_Out_Id 
                                   join [TBL_TRANSFER_IN_DETAILS] tid  on tod.Tod_Id=tid.Tod_Id where tod.Transfer_Out_Id=@id";
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
                catch (Exception)
                {

                    throw;
                }
            }




        }
        /// <summary>
        /// return price and quantity from transfer out to transfer in
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="InstanceId"></param>
        /// <param name="ToutdId"></param>
        /// <returns></returns>
        public static Item GetTransferOutQuantity(int ItemID, int InstanceId,int ToutdId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select Quantity,Mrp,Rate,Selling_Price,tax.Tax_Id,rate,tax.Percentage,Tax_Amount,Gross_Amount,Net_Amount,Instance_Id,Item_Id
   from TBL_TRANSFER_OUT_DETAILS tod with(nolock) left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=tod.Tax_Id 
   where Instance_Id=@InstanceId and item_id=@ItemID and tod_id=@ToutdId";
                db.CreateParameters(3);
                db.AddParameters(0, "@ItemID", ItemID);
                db.AddParameters(1, "@InstanceId", InstanceId);
                db.AddParameters(2, "@ToutdId", ToutdId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                 Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["Item_Id"] != DBNull.Value ? Convert.ToInt32(prod["Item_Id"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.CostPrice = prod["rate"] != DBNull.Value ? Convert.ToDecimal(prod["rate"]) : 0;
                item.TaxPercentage = prod["percentage"] != DBNull.Value ? Convert.ToDecimal(prod["percentage"]) : 0;
                item.SellingPrice = prod["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Selling_Price"]) : 0;
                item.Quantity = prod["Quantity"] != DBNull.Value ? Convert.ToDecimal(prod["Quantity"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;                             
                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "transferout | GetTransferOutQuantity(int ItemID, int InstanceId,int ToutdId)");
                return null;
            }
        }
    }
}
