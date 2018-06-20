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
    public class TransferIn : Register.Register, IRegister
    {
        #region Properties
        public int FromLocation { get; set; }
        public string FinancialYear { get; set; }
        public int Status { get; set; }
        public string EntryNo { get; set; }
        public int CostCenterId { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public string LocationRegId { get; set; }
        public int JobId { get; set; }
        public string TermsAndConditions { get; set; }
        public List<Item> Products { get; set; }
        #endregion Properties

        #region Functions
        /// <summary>
        /// Function for saving transfer in details
        /// </summary>
        /// <returns>Return successalert when details inserted successfully otherwise return an erroralert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.TransferIn, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TranferIn | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a TranferIn.", false, Type.Others, "TranferIn | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a TranferIn.", false, Type.Others, "TranferIn | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"insert into TBL_TRANSFER_IN_REGISTER(From_Location, Entry_Date, Total_Tax_Amount, Total_Gross_Amount, Total_Net_Amount, Discount_Amount, Other_Charges, Round_Off,      
                                  Narration, Entry_No,[Status], Created_By ,Created_Date,Cost_Center_Id,Job_Id,Terms_And_Conditions )
                                  values(@From_Location,@Entry_Date,@Total_Tax_Amount,@Total_Gross_Amount,@Total_Net_Amount,@Discount_Amount,@Other_Charges,@Round_Off,      
                                  @Narration,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TIN'),@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id,@Terms_And_Conditions); select @@identity";
                    db.CreateParameters(14);
                    db.AddParameters(0, "@From_Location", this.FromLocation);
                    db.AddParameters(1, "@Entry_Date", this.EntryDate);
                    db.AddParameters(2, "@Total_Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Total_Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Total_Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Discount_Amount", this.Discount);
                    db.AddParameters(6, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(7, "@Round_Off", this.RoundOff);
                    db.AddParameters(8, "@Narration", this.Narration);
                    db.AddParameters(9, "@Status", this.Status);
                    db.AddParameters(10, "@Created_By", this.CreatedBy);
                    db.AddParameters(11, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(12, "@Job_Id", this.JobId);
                    db.AddParameters(13, "@Terms_And_Conditions", this.TermsAndConditions);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "TIN", db);
                
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations

                        Item prod = TransferOut.GetTransferOutQuantity(item.ItemID, item.InstanceId, item.TodId);
                        item.TaxPercentage = prod.TaxPercentage;
                        item.TaxId = prod.TaxId;
                        item.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * prod.Quantity;
                        item.Gross = prod.Quantity * prod.CostPrice;
                        item.NetAmount = item.Gross + item.TaxAmount;
                        db.CleanupParameters();
                        query = @"insert into TBL_TRANSFER_IN_DETAILS (Transfer_In_Id,Tod_Id,item_id,Quantity,Mrp,Rate,Selling_Price,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Status,instance_id)
                                 values(@Transfer_In_Id,@Tod_Id,@item_id,@Quantity,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@instance_id)";
                        db.CreateParameters(13);
                        db.AddParameters(0, "@Transfer_In_Id", identity);
                        db.AddParameters(1, "@Tod_Id",item.TodId);
                        db.AddParameters(2, "@item_id", item.ItemID);
                        db.AddParameters(3, "@Quantity", prod.Quantity);
                        db.AddParameters(4, "@Mrp", prod.MRP);
                        db.AddParameters(5, "@Rate", prod.CostPrice);
                        db.AddParameters(6, "@Selling_Price", prod.SellingPrice);
                        db.AddParameters(7, "@Tax_Id", prod.TaxId);
                        db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                        db.AddParameters(9, "@Gross_Amount", item.Gross);
                        db.AddParameters(10, "@Net_Amount", item.NetAmount);
                        db.AddParameters(11, "@Status",0);
                        db.AddParameters(12, "@instance_id", item.InstanceId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        this.TaxAmount += item.TaxAmount;
                        this.Gross += item.Gross;
                        query = @"update TBL_TRANSFER_OUT_DETAILS set status=1 where tod_id=@tod_Id;
                              declare @registerId int,@total int,@totaltransfer int;
                              select @registerId=Transfer_Out_Id from TBL_TRANSFER_OUT_DETAILS where Tod_Id=@tod_Id;
                              select @total=count(*) from TBL_TRANSFER_OUT_DETAILS where Transfer_Out_Id=@registerId;
                              select @totaltransfer=count(*) from TBL_TRANSFER_OUT_DETAILS where Transfer_Out_Id=@registerId and Status=1;
                              if(@total=@totaltransfer)
                              begin update TBL_TRANSFER_OUT_REGISTER set Status=1 where Transfer_Out_Id=@registerId end";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@tod_Id", item.TodId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                    }
                    decimal _NetAmount = Math.Round(this.NetAmount);
                    this.RoundOff = this.NetAmount - _NetAmount;
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_TRANSFER_IN_REGISTER] set [Total_Tax_Amount]=" + this.TaxAmount + ", [Total_Gross_Amount]=" + this.Gross + " ,[Total_Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Transfer_In_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TOT')[New_Order] from TBL_TRANSFER_IN_REGISTER where Transfer_In_Id=" + identity);
                return new OutputMessage("Transfer In registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "TransferIn | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Transfer In could not be saved ", false, Type.Others, "TransferIn | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for updating trnsfer in details
        /// For updating an entry id must not be zero
        /// </summary>
        /// <returns>Return successalert when details updated successfully otherwise returns an error alert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.TransferIn, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a TransferIn.", false, Type.Others, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"update TBL_TRANSFER_IN_REGISTER set From_Location=@From_Location,Entry_Date=@Entry_Date,Total_Tax_Amount=@Total_Tax_Amount,
                                  Total_Gross_Amount=@Total_Gross_Amount,Total_Net_Amount=@Total_Net_Amount,Discount_Amount=@Discount_Amount,Other_Charges=@Other_Charges,
                                  Round_Off=@Round_Off,Narration=@Narration,[Status]=@Status,Modified_By=@Modified_By,
                                  Modified_Date=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,Terms_And_Conditions=@Terms_And_Conditions where Transfer_In_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(15);
                    db.AddParameters(0, "@From_Location", this.FromLocation);
                    db.AddParameters(1, "@Entry_Date", this.EntryDate);
                    db.AddParameters(2, "@Total_Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Total_Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Total_Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Discount_Amount", this.Discount);
                    db.AddParameters(6, "@Other_Charges", this.OtherCharges);
                    db.AddParameters(7, "@Round_Off", this.RoundOff);
                    db.AddParameters(8, "@Narration", this.Narration);
                    db.AddParameters(9, "@Status", this.Status);
                    db.AddParameters(10, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(11, "@Id", this.ID);
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.AddParameters(14, "@Terms_And_Conditions", this.TermsAndConditions);
                    db.ExecuteScalar(System.Data.CommandType.Text, query);
                 
                    foreach (Item item in Products)
                    {
                     
                        Item prod = TransferOut.GetTransferOutQuantity(item.ItemID, item.InstanceId,item.TodId);
                        item.TaxPercentage = prod.TaxPercentage;
                        item.TaxId = prod.TaxId;
                        item.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * prod.Quantity;
                        item.Gross = prod.Quantity * prod.CostPrice;
                        item.NetAmount = item.Gross + item.TaxAmount;
                        db.CleanupParameters();
                        query = @"insert into TBL_TRANSFER_IN_DETAILS (Transfer_In_Id,Tod_Id,item_id,Quantity,Mrp,Rate,Selling_Price,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Status,instance_id)
                                 values(@Transfer_In_Id,@Tod_Id,@item_id,@Quantity,@Mrp,@Rate,@Selling_Price,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status,@instance_id)";
                        db.CreateParameters(13);
                        db.AddParameters(0, "@Transfer_In_Id", ID);
                        db.AddParameters(1, "@Tod_Id",prod.TodId);
                        db.AddParameters(2, "@item_id", item.ItemID);
                        db.AddParameters(3, "@Quantity", prod.Quantity);
                        db.AddParameters(4, "@Mrp", prod.MRP);
                        db.AddParameters(5, "@Rate", prod.CostPrice);
                        db.AddParameters(6, "@Selling_Price", prod.SellingPrice);
                        db.AddParameters(7, "@Tax_Id", prod.TaxId);
                        db.AddParameters(8, "@Tax_Amount", item.TaxAmount);
                        db.AddParameters(9, "@Gross_Amount", item.Gross);
                        db.AddParameters(10, "@Net_Amount", item.NetAmount);
                        db.AddParameters(11, "@Status", 0);
                        db.AddParameters(12, "@instance_id", item.InstanceId);
                        this.TaxAmount += item.TaxAmount;
                        this.Gross += item.Gross;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_TRANSFER_IN_REGISTER] set [Total_Tax_Amount]=" + this.TaxAmount + ", [Total_Gross_Amount]=" + this.Gross + " ,[Total_Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Transfer_In_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Entry_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','TOT')[New_Order] from TBL_TRANSFER_IN_REGISTER where Transfer_In_Id=" + this.ID);
                return new OutputMessage("Transfer In " + dt.Rows[0]["Saved_No"].ToString()+ " has been Updated", true, Type.NoError, "TransferIn | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;

                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Transfer In could not be updated", false, Type.Others, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Transfer In could not be updated", false, Type.Others, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }
            finally
            {
                db.Close();
            }
        }

        public static List<TransferIn> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_In_Id,isnull(tr.From_Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Tot_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Tot_Gross_Amount],
                               isnull(tr.Total_Net_Amount,0)[Tot_Net_Amount],isnull(tr.Entry_No,0)[Entry_No],
                               isnull(tr.Status,0)[Status],td.Tid_Id,td.Tod_Id,td.item_id,td.Instance_Id,isnull(td.Quantity,0)[Quantity],isnull(td.Mrp,0)[Mrp],
                               isnull(td.Rate,0)[Rate],isnull(td.Selling_Price,0)[Selling_Price],isnull(td.Tax_Amount,0)[Tax_Amount],td.Tax_Id,
                               isnull(td.Gross_Amount,0)[Gross_Amount],
                               isnull(td.Status,0)[Status],td.Net_Amount,isnull(t.Percentage,0)[Tax_Percentage],l.Name[Location],isnull(i.Name,0)[Item],
                               isnull(i.Item_Code,0)[Item_Code],tr.Cost_Center_Id,j.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name
							   from TBL_TRANSFER_IN_REGISTER tr
                               left join TBL_TRANSFER_IN_DETAILS td on td.Transfer_In_Id=tr.Transfer_In_Id
                               left join TBL_TAX_MST t on t.Tax_Id=td.Tax_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.From_Location                              
                               left join tbl_item_mst i on i.item_Id=td.Item_Id
							   left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
                               where tr.From_Location = @Location_Id  order by tr.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<TransferIn> result = new List<TransferIn>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        TransferIn register = new TransferIn();
                        register.ID = row["Transfer_In_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_In_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = row["From_Location"] != DBNull.Value ? Convert.ToInt32(row["From_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tot_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Tax_Amount"]) : 0;
                        register.Gross = row["Tot_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Gross_Amount"]) : 0;
                        register.NetAmount = row["Tot_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Net_Amount"]) : 0;
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_In_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Tid_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tid_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.TodId = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "TransferIn | GetDetails(int LocationId)");
                return null;
            }

        }
        /// <summary>
        /// Retrieve a list of transfer in details
        /// </summary>
        /// <param name="LocationId">Location id of the entries</param>
        /// <param name="From">From date for filter details</param>
        /// <param name="To">To date for filter details </param>
        /// <returns>list of transfer in details</returns>
        public static List<TransferIn> GetDetails(int LocationId, DateTime? From, DateTime? To)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_In_Id,isnull(tr.From_Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Tot_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Tot_Gross_Amount],
                               isnull(tr.Total_Net_Amount,0)[Tot_Net_Amount],isnull(tr.Entry_No,0)[Entry_No],
                               isnull(tr.Status,0)[Status],td.Tid_Id,td.Tod_Id,td.item_id,td.Instance_Id,isnull(td.Quantity,0)[Quantity],isnull(td.Mrp,0)[Mrp],
                               isnull(td.Rate,0)[Rate],isnull(td.Selling_Price,0)[Selling_Price],isnull(td.Tax_Amount,0)[Tax_Amount],td.Tax_Id,
                               isnull(td.Gross_Amount,0)[Gross_Amount],
                               isnull(td.Status,0)[Status],td.Net_Amount,isnull(t.Percentage,0)[Tax_Percentage],l.Name[Location],isnull(i.Name,0)[Item],
                               isnull(i.Item_Code,0)[Item_Code],tr.Cost_Center_Id,j.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name,
                               l.Address1[Loc_address1],l.Address2[Loc_address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],tr.Terms_And_Conditions
                               from TBL_TRANSFER_IN_REGISTER tr
                               left join TBL_TRANSFER_IN_DETAILS td on td.Transfer_In_Id=tr.Transfer_In_Id
                               left join TBL_TAX_MST t on t.Tax_Id=td.Tax_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.From_Location                              
                               left join tbl_item_mst i on i.item_Id=td.Item_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
                               where tr.From_Location = @Location_Id {#daterangefilter#} order by tr.Created_Date desc";
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
                    List<TransferIn> result = new List<TransferIn>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        TransferIn register = new TransferIn();
                        register.ID = row["Transfer_In_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_In_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = row["From_Location"] != DBNull.Value ? Convert.ToInt32(row["From_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tot_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Tax_Amount"]) : 0;
                        register.Gross = row["Tot_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Gross_Amount"]) : 0;
                        register.NetAmount = row["Tot_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Net_Amount"]) : 0;
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.TermsAndConditions = Convert.ToString(row["Terms_And_Conditions"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_In_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Tid_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tid_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.TodId = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "TransferIn | GetDetails(int LocationId, DateTime? From, DateTime? To)");
                return null;
            }

        }
        /// <summary>
        /// Retrieve single entry from the list
        /// </summary>
        /// <param name="Id">Id of that particular entry you want retrieve</param>
        /// <param name="LocationId">Location id of that particular entry</param>
        /// <returns>single transfer in details</returns>
        public static TransferIn GetDetails(int Id,int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_In_Id,isnull(tr.From_Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Tot_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Tot_Gross_Amount],
                               isnull(tr.Total_Net_Amount,0)[Tot_Net_Amount],isnull(tr.Entry_No,0)[Entry_No],
                               isnull(tr.Status,0)[Status],td.Tid_Id,td.Tod_Id,td.item_id,td.Instance_Id,isnull(td.Quantity,0)[Quantity],isnull(td.Mrp,0)[Mrp],
                               isnull(td.Rate,0)[Rate],isnull(td.Selling_Price,0)[Selling_Price],isnull(td.Tax_Amount,0)[Tax_Amount],td.Tax_Id,
                               isnull(td.Gross_Amount,0)[Gross_Amount],
                               isnull(td.Status,0)[Status],td.Net_Amount,isnull(t.Percentage,0)[Tax_Percentage],l.Name[Location],isnull(i.Name,0)[Item],
                               isnull(i.Item_Code,0)[Item_Code],tr.Cost_Center_Id,j.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name,
							   l.name[Location],l.Address1[Loc_Address1],l.Address2[Loc_Address2],l.Contact[Loc_Phone],l.Reg_Id1[Loc_RegId],tr.Terms_And_Conditions 
							   from TBL_TRANSFER_IN_REGISTER tr
                               left join TBL_TRANSFER_IN_DETAILS td on td.Transfer_In_Id=tr.Transfer_In_Id
                               left join TBL_TAX_MST t on t.Tax_Id=td.Tax_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.From_Location                              
                               left join tbl_item_mst i on i.item_Id=td.Item_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=tr.Cost_Center_Id
							   left join TBL_JOB_MST j on j.Job_Id=tr.Job_Id
                               where tr.From_Location=@Location_Id and tr.Transfer_In_Id=@Transfer_In_Id order by tr.Created_Date desc";
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Transfer_In_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                  
                        DataRow row = dt.Rows[0];
                        TransferIn register = new TransferIn();
                        register.ID = row["Transfer_In_Id"] != DBNull.Value ? Convert.ToInt32(row["Transfer_In_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.FromLocation = row["From_Location"] != DBNull.Value ? Convert.ToInt32(row["From_Location"]) : 0;
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Tot_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Tax_Amount"]) : 0;
                        register.Gross = row["Tot_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Gross_Amount"]) : 0;
                        register.NetAmount = row["Tot_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tot_Net_Amount"]) : 0;
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.TermsAndConditions = Convert.ToString(row["Terms_And_Conditions"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                      
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_In_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Tid_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tid_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.TodId = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["Item"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "TransferIn |GetDetails(int Id,int LocationId)");
                return null;
            }

        }

        public static List<TransferOut> GetDetailsFromTransferOut(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select tr.Transfer_Out_Id,isnull(tr.To_Location,0)[To_Location],isnull(tr.Location,0)[From_Location],isnull(tr.Entry_Date,0)[Entry_Date],
                               isnull(tr.Total_Tax_Amount,0)[Total_Tax_Amount],isnull(tr.Total_Gross_Amount,0)[Total_Gross_Amount],isnull(tr.Total_Net_Amount,0)[Total_Net_Amount],
                               isnull(tr.Discount_Amount,0)[Discount_Amount],isnull(tr.Other_Charges,0)[Other_Charges],isnull(tr.Round_Off,0)[Round_Off],isnull(tr.Narration,0)[Narration],
                               isnull(tr.Entry_No,0)[Entry_No],isnull(tr.Status,0)[Status],trd.Tod_Id,trd.item_id,trd.Instance_Id,trd.Quantity,
                               isnull(trd.Mrp,0)[Mrp],isnull(trd.Rate,0)[Cost_Price],isnull(trd.Selling_Price,0)[Selling_Price],isnull(trd.Tax_Amount,0)[Tax_Amount],
                               isnull(trd.Gross_Amount,0)[Gross_Amount],isnull(trd.Net_Amount,0)[Net_Amount],l.Name[Location],isnull(t.Percentage,0)[Tax_Percentage],i.Name[Item],i.Item_Code 
							   from TBL_TRANSFER_OUT_REGISTER tr 
                               join TBL_TRANSFER_OUT_DETAILS trd on trd.Transfer_Out_Id=tr.Transfer_Out_Id
                               left join TBL_LOCATION_MST l on l.Location_Id=tr.To_Location
                               left join TBL_TAX_MST t on t.Tax_Id=trd.Tax_Id			   
							   left join TBL_ITEM_MST i on i.Item_Id=trd.Item_Id
                               where  tr.To_Location = @To_Location  order by tr.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@To_Location", LocationId);
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
                        register.EntryNo = Convert.ToString(row["Entry_No"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Transfer_Out_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Tod_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Tod_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
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
                Application.Helper.LogException(ex, "TransferIn |GetDetailsFromTransferOut(int LocationId)");
                return null;
            }
        }
        /// <summary>
        /// Function for deleting an entry from database
        /// For deleting an entry id must not be zero
        /// </summary>
        /// <returns>Return successalert when details deleted successfully otherwise return an error message</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.TransferIn, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "TransferIn | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "TransferIn| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_TRANSFER_IN_DETAILS where Transfer_In_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_TRANSFER_IN_REGISTER where Transfer_In_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Transfer In deleted succesfully", true, Type.NoError, "TransferIn | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "TransferIn | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "TransferIn | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Transfer In could not be saved ", false, Type.Others, "TransferIn | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
        }

        #endregion Functions
    }
}
