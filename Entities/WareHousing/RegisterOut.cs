using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Core.DBManager;
using System.Data;

namespace Entities.Register
{
    public class RegisterOut : Register, IRegister
    {
        #region Properties

        public string FinancialYear { get; set; }
        public int CustomerId { get; set; }
        public string Others { get; set; }
        public int EmployeeId { get; set; }
        public int CostCenterId { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public string OrderNo { get; set; }
        public List<Item> Products { get; set; }
        #endregion Properties
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.RegisterOut, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a RegisterOut.", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.CustomerId <= 0 && this.EmployeeId <= 0 && string.IsNullOrWhiteSpace(this.Others))
                {
                    return new OutputMessage("Select Whom to make a RegisterOut.", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a RegisterOut.", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"INSERT INTO [dbo].[TBL_REGISTER_OUT_REGISTER]([Location_Id],[Customer_Id],[Employee_Id],[Entry_Date],
                                   [Others],[Order_No],[Tax_Amount],[Gross_Amount],[Net_Amount],[Round_Off],[Narration],[Status],[Created_By],[Created_Date],Cost_Center_Id,Job_Id)
                                   VALUES(@Location_Id,@Customer_Id,@Employee_Id,@Entry_Date,@Others,
                                   [dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','ROT'),@Tax_Amount,@Gross_Amount, @Net_Amount, @Round_Off, @Narration, @Status, @Created_By, GETUTCDATE(),@Cost_Center_Id,@Job_Id); select @@identity;";
                    db.CreateParameters(14);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Entry_Date", this.EntryDate);
                    db.AddParameters(3, "@Others", this.Others);
                    db.AddParameters(4, "@Employee_Id", this.EmployeeId);
                    db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(6, "@Gross_Amount", this.Gross);
                    db.AddParameters(7, "@Net_Amount", this.NetAmount);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Status", this.Status);
                    db.AddParameters(11, "@Created_By", this.CreatedBy);
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "ROT", db);

                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations

                        Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                        item.TaxPercentage = prod.TaxPercentage;
                        item.TaxId = prod.TaxId;
                        item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                        item.Gross = item.Quantity * prod.CostPrice;
                        item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                        db.CleanupParameters();
                        query = @"INSERT INTO [dbo].[TBL_REGISTER_OUT_DETAILS]([Reg_Out_Id],[item_id],[Quantity],
                                [Rate],[Mrp],[Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Status],[instance_Id])
                                VALUES(@Reg_Out_Id,@item_id,@Qty,@Rate,@Mrp,@Tax_Id,@Tax_Amount,@Gross_Amount,
                                @Net_Amount,@Status,@instanceId)";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Reg_Out_Id", identity);
                        db.AddParameters(1, "@item_id", item.ItemID);
                        db.AddParameters(2, "@Qty", item.Quantity);
                        db.AddParameters(3, "@Mrp", prod.MRP);
                        db.AddParameters(4, "@Rate", prod.CostPrice);
                        db.AddParameters(5, "@Tax_Id", item.TaxId);
                        db.AddParameters(6, "@Tax_Amount", item.TaxAmount);
                        db.AddParameters(7, "@Gross_Amount", item.Gross);
                        db.AddParameters(8, "@Net_Amount", item.NetAmount);
                        db.AddParameters(9, "@Status", 0);
                        db.AddParameters(10, "@instanceId", item.InstanceId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        this.TaxAmount += item.TaxAmount * item.Quantity;
                        this.Gross += item.Gross;
                }
                    decimal _NetAmount = Math.Round(this.NetAmount);
                    this.RoundOff = this.NetAmount - _NetAmount;
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_REGISTER_OUT_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Reg_Out_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Order_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','ROT')[New_Order] from TBL_REGISTER_OUT_REGISTER where Reg_Out_Id=" + identity);
                return new OutputMessage("Register Out registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "RegisterOut | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Register Out could not be saved ", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.RegisterOut, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterOut | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a Register Out", false, Type.Others, "RegisterOut | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "RegisterOut Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"delete from [TBL_REGISTER_OUT_DETAILS] where Reg_Out_Id=@Id;
                                   Update TBL_REGISTER_OUT_REGISTER set  [Location_Id]= @Location_Id,[Customer_Id]= @Customer_Id,[Employee_Id]= @Employee_Id ,[Entry_Date]= @Entry_Date ,[Others]=@Others,
                                  [Tax_Amount]= @Tax_Amount ,[Gross_Amount]=@Gross_Amount ,[Net_Amount]= @Net_Amount ,[Round_Off]=@Round_Off,
                                  [Narration]= @Narration ,[Status]=@Status ,[Modified_By]= @Modified_By,Modified_Date=GETUTCDATE(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id where Reg_Out_Id=@Id";
                    db.BeginTransaction();
                    db.CreateParameters(15);
                    db.AddParameters(0, "@Location_Id", LocationId);
                    db.AddParameters(1, "@Customer_Id", this.CustomerId);
                    db.AddParameters(2, "@Employee_Id", this.EmployeeId);
                    db.AddParameters(3, "@Entry_Date", this.EntryDate);
                    db.AddParameters(4, "@Others", this.Others);
                    db.AddParameters(5, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(6, "@Gross_Amount", this.Gross);
                    db.AddParameters(7, "@Net_Amount", this.NetAmount);
                    db.AddParameters(8, "@Round_Off", this.RoundOff);
                    db.AddParameters(9, "@Narration", this.Narration);
                    db.AddParameters(10, "@Status", this.Status);
                    db.AddParameters(11, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(12, "@Id", this.ID);
                    db.AddParameters(13, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(14, "@Job_Id", this.JobId);
                    db.ExecuteScalar(System.Data.CommandType.Text, query);

                    foreach (Item item in Products)
                    {

                        Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId,this.LocationId);

                        if (prod.Stock < item.Quantity)
                        {
                            return new OutputMessage("Quantity Exceeds", false, Type.Others, "Damage |Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        item.TaxPercentage = prod.TaxPercentage;
                        item.TaxId = prod.TaxId;
                        item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                        item.Gross = item.Quantity * prod.CostPrice;
                        item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                        db.CleanupParameters();
                        query = @"INSERT INTO [dbo].[TBL_REGISTER_OUT_DETAILS]([Reg_Out_Id],[item_id],[Quantity],[Rate],[Mrp],
                                [Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Status],[instance_id])
                                VALUES(@Reg_Out_Id,@item_Id,@Qty,@Rate,@Mrp,@Tax_Id,@Tax_Amount,@Gross_Amount,
                                @Net_Amount,@Status,@instance_id)";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Reg_Out_Id", this.ID);
                        db.AddParameters(1, "@item_Id", item.ItemID);
                        db.AddParameters(2, "@Qty", item.Quantity);
                        db.AddParameters(3, "@Mrp", prod.MRP);
                        db.AddParameters(4, "@Rate", prod.CostPrice);
                        db.AddParameters(5, "@Tax_Id", prod.TaxId);
                        db.AddParameters(6, "@Tax_Amount", item.TaxAmount);
                        db.AddParameters(7, "@Gross_Amount", item.Gross);
                        db.AddParameters(8, "@Net_Amount", item.NetAmount);
                        db.AddParameters(9, "@Status", 0);
                        db.AddParameters(10, "@instance_id", item.InstanceId);
                        db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        this.TaxAmount += item.TaxAmount * item.Quantity;
                        this.Gross += item.Gross;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_REGISTER_OUT_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Reg_Out_Id=" + this.ID);
                }
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Order_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','ROT')[New_Order] from TBL_REGISTER_OUT_REGISTER where Reg_Out_Id=" + this.ID);
                db.CommitTransaction();
                return new OutputMessage("Register Out " + dt.Rows[0]["Saved_No"].ToString() + " has been Updated  ", true, Type.NoError, "RegisterOut | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;

                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "RegisterOut | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Register Out could not be saved", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Register Out could not be saved", false, Type.Others, "RegisterOut | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }
            finally
            {
                db.Close();
            }
        }
        public static List<RegisterOut> GetDetails(int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],
                               outR.Customer_Id Customer_Id,outR.Employee_Id Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,outR.Others Others,
                               outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,
                               isnull(outR.Net_Amount,0)Total_Net_Amount ,isnull(outR.round_off,0)Round_Off,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,
                               isnull(outd.Item_Id,0) Item_Id,outd.instance_id,isnull(outd.quantity,0) quantity,ISNULL(outd.Rate,0) rate ,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,
                               ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent], 
                               outR.Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration from TBL_REGISTER_OUT_REGISTER outR
                               join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                               left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                               left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                               left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id                                
                               left join TBL_ITEM_MST itm on itm.Item_Id=outD.Item_Id
                               left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
                               where outR.Location_Id=@location_id order by outR.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<RegisterOut> result = new List<RegisterOut>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        RegisterOut register = new RegisterOut();
                        register.ID = row["RegisterOut_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterOut_Id"]) : 0;
                        register.LocationId = Convert.ToInt32(row["Location_Id"]);
                        register.CustomerId = Convert.ToInt32(row["customer_id"]);
                        register.EmployeeId = Convert.ToInt32(row["employee_id"]);
                        register.Location = Convert.ToString(row["location"]);
                        register.Others = Convert.ToString(row["others"]);
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.OrderNo = Convert.ToString(row["order_no"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterOut_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_out_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_out_detail_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["item_name"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percent"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percent"]) : 0;
                            item.Gross = rowItem["Gross"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross"]) : 0;
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
                Application.Helper.LogException(ex, "registerout | GetDetails(int LocationId)");
                return null;
            }
        }

        public static List<RegisterOut> GetDetails(int LocationId, DateTime? From, DateTime? To)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],
                               outR.Customer_Id Customer_Id,outR.Employee_Id Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,outR.Others Others,
                               outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,
                               isnull(outR.Net_Amount,0)Total_Net_Amount ,isnull(outR.round_off,0)Round_Off,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,
                               isnull(outd.Item_Id,0) Item_Id,outd.instance_id,isnull(outd.quantity,0) quantity,ISNULL(outd.Rate,0) rate ,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,
                               ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent], 
                               outR.Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration,cost.Fcc_Name[Cost_Center],j.Job_Name,outR.Cost_Center_Id,outR.Job_Id
							   from TBL_REGISTER_OUT_REGISTER outR
                               join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                               left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                               left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                               left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id                                
                               left join TBL_ITEM_MST itm on itm.Item_Id=outD.Item_Id
                               left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
							   left join tbl_fin_costCenter cost on cost.Fcc_ID=outR.Cost_Center_Id
							   left join TBL_JOB_MST j on j.Job_Id=outR.Job_Id
                               where outR.Location_Id=@Location_Id {#daterangefilter#}  order by outR.Created_Date desc";
                #endregion query
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and outR.Entry_Date>=@fromdate and outR.Entry_Date<=@todate ");
                }
                else
                {
                    To = DateTime.Now;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and outR.Entry_Date>=@fromdate and outR.Entry_Date<=@todate ");
                }
                db.CreateParameters(3);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<RegisterOut> result = new List<RegisterOut>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        RegisterOut register = new RegisterOut();
                        register.ID = row["RegisterOut_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterOut_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = Convert.ToInt32(row["Location_Id"]);
                        register.CustomerId = Convert.ToInt32(row["customer_id"]);
                        register.EmployeeId = Convert.ToInt32(row["employee_id"]);
                        register.Location = Convert.ToString(row["location"]);
                        register.Others = Convert.ToString(row["others"]);
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.OrderNo = Convert.ToString(row["order_no"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;

                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterOut_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_out_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_out_detail_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["item_name"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percent"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percent"]) : 0;
                            item.Gross = rowItem["Gross"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross"]) : 0;
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
                Application.Helper.LogException(ex, "registerout |  GetDetails(int LocationId, DateTime? From, DateTime? To)");
                return null;
            }
        }
        public static RegisterOut GetDetails(int Id,int LocationId)
        {

            DBManager db = new DBManager();
            try
            {
                #region query
                string query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],
                               outR.Customer_Id Customer_Id,outR.Employee_Id Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,outR.Others Others,
                               outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,
                               isnull(outR.Net_Amount,0)Total_Net_Amount ,isnull(outR.round_off,0)Round_Off,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,
                               isnull(outd.Item_Id,0) Item_Id,outd.instance_id,isnull(outd.quantity,0) quantity,ISNULL(outd.Rate,0) rate ,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,
                               ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent], 
                               outR.Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration,cost.Fcc_Name[Cost_Center],j.Job_Name,outR.Cost_Center_Id,outR.Job_Id
                               from TBL_REGISTER_OUT_REGISTER outR
                               join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                               left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                               left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                               left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id                                
                               left join TBL_ITEM_MST itm on itm.Item_Id=outD.Item_Id
                               left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
                               left join tbl_fin_costCenter cost on cost.Fcc_ID=outR.Cost_Center_Id
							   left join TBL_JOB_MST j on j.Job_Id=outR.Job_Id
                               where outR.Location_Id=@Location_Id and outR.Reg_Out_Id=@Reg_Out_Id order by outR.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@Reg_Out_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    
                        DataRow row = dt.Rows[0];
                        RegisterOut register = new RegisterOut();
                        register.ID = row["RegisterOut_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterOut_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.LocationId = Convert.ToInt32(row["Location_Id"]);
                        register.CustomerId = Convert.ToInt32(row["customer_id"]);
                        register.EmployeeId = Convert.ToInt32(row["employee_id"]);
                        register.Location = Convert.ToString(row["location"]);
                        register.Others = Convert.ToString(row["others"]);
                        register.EntryDateString = row["Entry_Date"] != DBNull.Value ? Convert.ToDateTime(row["Entry_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.TaxAmount = row["Total_Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Tax_Amount"]) : 0;
                        register.Gross = row["Total_Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Gross_Amount"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.NetAmount = row["Total_Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.OrderNo = Convert.ToString(row["order_no"]);
                        register.Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0;
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterOut_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_out_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_out_detail_Id"]) : 0;
                            item.ItemID = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
                            item.Name = rowItem["item_name"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["rate"]) : 0;
                            item.TaxPercentage = rowItem["Tax_Percent"] != DBNull.Value ? Convert.ToDecimal(rowItem["Tax_Percent"]) : 0;
                            item.Gross = rowItem["Gross"] != DBNull.Value ? Convert.ToDecimal(rowItem["Gross"]) : 0;
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
                Application.Helper.LogException(ex, "registerout |  GetDetails(int Id,int LocationId)");
                return null;
            }
        }
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.RegisterOut, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterOut | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "RegisterOut| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (HasReference(this.ID))
            {


                return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "RegisterOut Request | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_REGISTER_OUT_DETAILS where Reg_Out_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_REGISTER_OUT_REGISTER where Reg_Out_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Register out has been deleted", true, Type.NoError, "RegisterOut | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "RegisterOut | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "RegisterOut | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    return null;
                }
                finally
                {
                    db.Close();

                }
            }
        }
        /// <summary>
        /// check for the Register out  is refered in another transactions
        /// </summary>
        /// <param name="RegisteroutId (id)"></param>
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
                    string query = @"select count(*) from [TBL_REGISTER_OUT_REGISTER] ror join 
                                   [TBL_REGISTER_OUT_DETAILS] rod on rod.Reg_Out_Id=ror.Reg_Out_Id 
                                   join [TBL_REGISTER_IN_DETAILS] rir  on rod.Reg_Out_Detail_Id=rir.Reg_Out_Detail_Id where rod.Reg_Out_Id=@Id";
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
        /// return price and quantity from register out for inserting in register in details
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="InstanceId"></param>
        /// <param name="RegoutdId"></param>
        /// <returns></returns>
        public static Item GetRegisterOutQuantity(int ItemID, int InstanceId, int RegoutdId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select Quantity,Mrp,Rate,tax.Tax_Id,rate,tax.Percentage,Tax_Amount,Gross_Amount,Net_Amount,Instance_Id,Item_Id
   from TBL_register_OUT_DETAILS rod with(nolock) left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=rod.Tax_Id 
   where Instance_Id=@InstanceId and item_id=@ItemID and Reg_Out_Detail_Id=@RegoutdId";
                db.CreateParameters(3);
                db.AddParameters(0, "@ItemID", ItemID);
                db.AddParameters(1, "@InstanceId", InstanceId);
                db.AddParameters(2, "@RegoutdId", RegoutdId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["Item_Id"] != DBNull.Value ? Convert.ToInt32(prod["Item_Id"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.CostPrice = prod["rate"] != DBNull.Value ? Convert.ToDecimal(prod["rate"]) : 0;
                item.TaxPercentage = prod["percentage"] != DBNull.Value ? Convert.ToDecimal(prod["percentage"]) : 0;
                item.Quantity = prod["Quantity"] != DBNull.Value ? Convert.ToDecimal(prod["Quantity"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "registerout |  GetRegisterOutQuantity(int ItemID, int InstanceId, int RegoutdId)");
                return null;
            }
        }
    }
}
