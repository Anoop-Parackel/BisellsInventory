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
    public class RegisterIn : Register, IRegister
    {
        #region Properties
   
        public string FinancialYear { get; set; }
        public int CustomerId { get; set; }
        public string Others { get; set; }
        public int EmployeeId { get; set; }
        public int Status { get; set; }
        public int CostCenterId { get; set; }
        public int JobId { get; set; }
        public string OrderNo{ get; set; }
        public List<Item> Products { get; set; }
        #endregion Properties
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.RegisterIn, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid Entry Date to make a RegisterIn.", false, Type.Others, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a RegisterIn.", false, Type.Others, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"INSERT INTO [dbo].[TBL_REGISTER_IN_REGISTER]
                                      ([Location_Id],[Customer_Id],[Employee_Id],[Entry_Date]
                                      ,[Others],[Order_No],[Tax_Amount],[Gross_Amount],[Net_Amount],[Round_Off],[Narration]
                                      ,[Status],[Created_By],[Created_Date],Cost_Center_Id,Job_Id)
                         VALUES(@Location_Id,@Customer_Id,@Employee_Id,@Entry_Date,@Others,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','RIN') , @Tax_Amount,@Gross_Amount,@Net_Amount,@Round_Off,@Narration,@Status,@Created_By,GETUTCDATE(),@Cost_Center_Id,@Job_Id); select @@identity;";
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
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "RIN", db);                   
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Entry | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = RegisterOut.GetRegisterOutQuantity(item.ItemID, item.InstanceId,item.RoId);
                            item.TaxPercentage = prod.TaxPercentage;
                            item.TaxId = prod.TaxId;
                            item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                            item.Gross = item.Quantity * prod.CostPrice;
                            item.NetAmount = item.Gross + (item.TaxAmount * prod.Quantity);
                            db.CleanupParameters();
                            query = @"INSERT INTO [dbo].[TBL_REGISTER_IN_DETAILS]([Reg_In_Id],[Reg_Out_Detail_Id],[item_id],[Quantity]
                                                ,[Rate],[Mrp],[Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Status],[instance_id])
                                    VALUES(@Reg_In_Id,@Reg_Out_Detail_Id,@item_id,@Quantity,@Rate,@Mrp,@Tax_Id,@Tax_Amount,@Gross_Amount,
                                        @Net_Amount,@Status,@instance_id)";
                            db.CreateParameters(12);
                            db.AddParameters(0, "@Reg_In_Id", identity);
                            db.AddParameters(1, "@Reg_Out_Detail_Id", item.RoId);
                            db.AddParameters(2, "@item_id", item.ItemID );
                            db.AddParameters(3, "@Quantity", prod.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", item.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", item.Gross);
                            db.AddParameters(9, "@Net_Amount", item.NetAmount);
                            db.AddParameters(10, "@Status", 0);
                            db.AddParameters(11, "@instance_id", item.InstanceId );
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += item.TaxAmount * item.Quantity;
                            this.Gross += item.Gross;
                            query = @"update TBL_register_out_DETAILS set Status=1 where Reg_Out_Detail_Id=@regd_id;
                                    declare @registerId int,@total int,@totalRegisterOut int; 
                                    select @registerId= reg_out_Id from TBL_register_out_DETAILS where Reg_Out_Detail_Id=@regd_id;
                                    select @total= count(*) from TBL_register_out_DETAILS where Reg_Out_Id=@registerId;
                                    select @totalRegisterOut= count(*) from TBL_register_out_DETAILS where Reg_Out_Id=@registerId and Status=1;
                                    if(@total=@totalRegisterOut) 
                                    begin update TBL_register_out_register set Status=1 where Reg_Out_Id=@registerId end";
                            db.CreateParameters(1);
                            db.AddParameters(0, "@regd_id", item.RoId);

                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                        }
                    }
                    decimal _NetAmount = Math.Round(this.NetAmount);
                    this.RoundOff = this.NetAmount - _NetAmount;
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_REGISTER_IN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Reg_IN_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Order_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','RIN')[New_Order] from TBL_REGISTER_IN_REGISTER where Reg_IN_Id=" + identity);
                return new OutputMessage("Register In Registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "RegisterIn | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Register In could not be saved", false, Type.Others, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.RegisterIn, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterIn | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a RegisterIn.", false, Type.Others, "RegisterIn | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"delete from [TBL_REGISTER_IN_DETAILS] where Reg_IN_Id=@Id;
                                  update [TBL_REGISTER_IN_REGISTER] set
                        [Location_Id]=@Location_Id,[Customer_Id]=@Customer_Id,[Employee_Id]=@Employee_Id,[Entry_Date]=@Entry_Date
                ,[Others]=@Others,[Tax_Amount]=@Tax_Amount,[Gross_Amount]=@Gross_Amount,[Net_Amount]=@Net_Amount,[Round_Off]=@Round_Off,[Narration]=@Narration
                    ,[Status]=@Status,[modified_By]=@Modified_By,[modified_Date]=getutcdate(),Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id where reg_in_id=@Id";
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

                        Item prod = RegisterOut.GetRegisterOutQuantity(item.ItemID, item.InstanceId, item.RoId);
                        item.TaxPercentage = prod.TaxPercentage;
                        item.TaxId = prod.TaxId;
                        item.TaxAmount = prod.CostPrice * (item.TaxPercentage / 100);
                        item.Gross = prod.Quantity * prod.CostPrice;
                        item.NetAmount = item.Gross + (item.TaxAmount * item.Quantity);
                        db.CleanupParameters();
                        query = @"INSERT INTO [dbo].[TBL_REGISTER_IN_DETAILS]([Reg_In_Id],[Reg_Out_Detail_Id],[item_id],[Quantity]
                                                ,[Rate],[Mrp],[Tax_Id],[Tax_Amount],[Gross_Amount],[Net_Amount],[Status],[instance_id])
                                    VALUES(@Reg_In_Id,@Reg_Out_Detail_Id,@ItemID,@Quantity,@Rate,@Mrp,@Tax_Id,@Tax_Amount,@Gross_Amount,
                                        @Net_Amount,@Status,@instance_id)";
                        db.CreateParameters(12);
                        db.AddParameters(0, "@Reg_In_Id", this.ID);
                        db.AddParameters(1, "@Reg_Out_Detail_Id", item.RoId);
                        db.AddParameters(2, "@ItemID", item.ItemID);
                        db.AddParameters(3, "@Quantity", prod.Quantity);
                        db.AddParameters(4, "@Mrp", prod.MRP);
                        db.AddParameters(5, "@Rate", prod.CostPrice);
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_REGISTER_IN_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Reg_IN_Id=" + this.ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Order_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','RIN')[New_Order] from TBL_REGISTER_IN_REGISTER where Reg_IN_Id=" + this.ID);
                return new OutputMessage("Register IN  " + dt.Rows[0]["Saved_No"].ToString()+ " has been Updated", true, Type.NoError, "RegisterIn | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString() });
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;

                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "RegisterOut | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. RegisterIn could not be saved", false, Type.Others, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. RegisterIn could not be saved", false, Type.Others, "RegisterIn | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                }
            }
            finally
            {
                db.Close();
            }
        }
        public static List<RegisterOut> GetDetailsUserWise(int LocationId,string id,int type)
        {
            DBManager db = new DBManager();
            try
            {
                string query = "";
                if (type == 1)
                {
                    query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],isnull(outR.Customer_Id,0) Customer_Id,isnull(outR.Employee_Id,0) Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,isnull(outR.Others,0) Others
                                 ,outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,isnull(outR.Net_Amount,0)  Total_Net_Amount ,isnull(outR.round_off,0)  Round_Off
                                  ,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,isnull(outd.item_id,0) item_id,isnull(outd.instance_id,0) instance_id,isnull(outd.quantity,0) quantity
                                  ,ISNULL(outd.Rate,0) rate,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount 
                                  ,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent],isnull(outR.Status,0) Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration  from TBL_REGISTER_OUT_REGISTER outR
                                  join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                                  left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                                  left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                                  left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id
                                  left join TBL_ITEM_MST itm on itm.Item_Id=outd.Item_Id
                                  left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
                                  where outR.Location_Id=@Location_Id and outR.Customer_Id=@id order by outR.Created_Date desc";
                }
                else if (type == 2)
                {
                    query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],isnull(outR.Customer_Id,0) Customer_Id,isnull(outR.Employee_Id,0) Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,isnull(outR.Others,0) Others
                                 ,outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,isnull(outR.Net_Amount,0)  Total_Net_Amount ,isnull(outR.round_off,0)  Round_Off
                                  ,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,isnull(outd.item_id,0) item_id,isnull(outd.instance_id,0) instance_id,isnull(outd.quantity,0) quantity
                                  ,ISNULL(outd.Rate,0) rate,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount 
                                  ,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent],isnull(outR.Status,0) Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration from TBL_REGISTER_OUT_REGISTER outR
                                  join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                                  left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                                  left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                                  left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id
                                  left join TBL_ITEM_MST itm on itm.Item_Id=outd.Item_Id
                                  left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
                                  where outR.Location_Id=@Location_Id and outR.Employee_Id=@id order by outR.Created_Date desc";
                }
                else if (type == 3)
                {
                    query = @"select isnull(outR.Reg_Out_Id,0) RegisterOut_Id,isnull(outR.Location_Id,0) Location_Id,loc.Name [location],isnull(outR.Customer_Id,0) Customer_Id,isnull(outR.Employee_Id,0) Employee_Id,isnull(outR.Entry_Date,0) Entry_Date,isnull(outR.Others,0) Others
                                 ,outR.Order_No,isnull(outR.Tax_Amount,0)  Total_Tax_Amount,isnull(outR.Gross_Amount,0)  Total_Gross_Amount,isnull(outR.Net_Amount,0)  Total_Net_Amount ,isnull(outR.round_off,0)  Round_Off
                                  ,isnull(outD.Reg_Out_Detail_Id,0) Reg_out_detail_Id,isnull(outd.item_id,0) item_id,isnull(outd.instance_id,0) instance_id,isnull(outd.quantity,0) quantity
                                  ,ISNULL(outd.Rate,0) rate,ISNULL(outd.Mrp,0) mrp,ISNULL(outd.Tax_Id,0) Tax_Id,ISNULL(outd.Gross_Amount,0) gross,ISNULL(outd.Net_Amount,0) Net_amount 
                                  ,itm.Name [item_name],itm.Item_Code [item_code],isnull(tax.Percentage,0) [tax_percent],isnull(outR.Status,0) Status,isnull(outD.Tax_Amount,0)  Tax_Amount,outr.Narration from TBL_REGISTER_OUT_REGISTER outR
                                  join TBL_REGISTER_OUT_DETAILS outD on outD.Reg_Out_Id=outR.Reg_Out_Id
                                  left join TBL_LOCATION_MST loc on loc.Location_Id=outr.Location_Id
                                  left join TBL_CUSTOMER_MST cus on cus.customer_id=outr.Customer_Id
                                  left join TBL_EMPLOYEE_MST emp on emp.employee_id=outr.Employee_Id
                                  left join TBL_ITEM_MST itm on itm.Item_Id=outd.Item_Id
                                  left join TBL_TAX_MST tax on tax.Tax_Id=outd.Tax_Id
                                  where outR.Location_Id=@Location_Id AND outr.Others not like '%0%' order by outR.Created_Date desc";

                }
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@id", id);
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
                Application.Helper.LogException(ex, "registerin | GetDetailsUserWise(int LocationId,string id,int type)");
                return null;
            }
        }
        public static List<RegisterIn> GetDetails(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select rin.Reg_In_Id registerIn_Id,rin.Location_Id,loc.Name [location],isnull(rin.Customer_Id,0) Customer_Id,cus.Name [customer],isnull(rin.Employee_Id,0) Employee_Id,emp.First_Name [employee],isnull(rin.Entry_Date,0) Entry_date,
                                  	 rin.Others others,isnull(rin.Order_No,0) order_No,isnull(rin.Tax_Amount,0) Total_Tax_Amount,isnull(rin.Gross_Amount,0) Total_Gross_Amount,isnull(rin.Net_Amount,0) Total_Net_Amount,isnull(rin.Round_Off,0) round_off
                                    ,rdin.Reg_In_Detail_Id,rdin.Reg_Out_Detail_Id,rdin.item_id,rdin.instance_id,isnull(rdin.Quantity,0) quantity,isnull(rdin.Rate,0) rate,isnull(rdin.Mrp,0) mrp,rdin.Tax_Id,isnull(rdin.Tax_Amount,0) tax_Amount,isnull(rdin.Gross_Amount,0) gross
                                    ,isnull(rdin.Net_Amount,0) NET_AMOUNT,ISNULL(rdin.Status,0) [status],itm.Name [item_name],itm.Item_Code,rin.narration,tax.Percentage [tax_percent]	from TBL_REGISTER_IN_REGISTER rin
                                  	 join TBL_REGISTER_IN_DETAILS rdin on rdin.Reg_In_Id=rin.Reg_In_Id
                                  	 left join TBL_CUSTOMER_MST cus on cus.Customer_Id=rin.Customer_Id
                                  	 left join TBL_EMPLOYEE_MST emp on emp.employee_id=rin.Employee_Id
                                  	 left join TBL_LOCATION_MST loc on loc.location_id=rin.location_id
                                     left join tbl_tax_mst tax on tax.tax_id=rdin.Tax_Id 
                                  	 left join TBL_ITEM_MST itm on itm.item_id=rdin.Item_Id
                                  	  where rin.Location_Id=@locationId order by rin.Created_Date DESc";
                db.CreateParameters(1);
                db.AddParameters(0, "@locationId", LocationId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<RegisterIn> result = new List<RegisterIn>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        RegisterIn register = new RegisterIn();
                        register.ID = row["RegisterIn_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterIn_Id"]) : 0;
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
                        
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterIn_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_in_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_in_detail_Id"]) : 0;
                            item.RoId = rowItem["Reg_Out_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_Out_Detail_Id"]) : 0;
                            item.ItemID  = rowItem["item_id"] != DBNull.Value ? Convert.ToInt32(rowItem["item_id"]) : 0;
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
                Application.Helper.LogException(ex, "registerin | GetDetails(int LocationId)");
                return null;
            }
        }

        public static List<RegisterIn> GetDetails(int LocationId, DateTime? From, DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select rin.Reg_In_Id registerIn_Id,rin.Location_Id,loc.Name [location],isnull(rin.Customer_Id,0) Customer_Id,cus.Name [customer],isnull(rin.Employee_Id,0) Employee_Id,emp.First_Name [employee],isnull(rin.Entry_Date,0) Entry_date,
                                  	 rin.Others others,isnull(rin.Order_No,0) order_No,isnull(rin.Tax_Amount,0) Total_Tax_Amount,isnull(rin.Gross_Amount,0) Total_Gross_Amount,isnull(rin.Net_Amount,0) Total_Net_Amount,isnull(rin.Round_Off,0) round_off
                                    ,rdin.Reg_In_Detail_Id,rdin.Reg_Out_Detail_Id,rdin.item_id,rdin.instance_id,isnull(rdin.Quantity,0) quantity,isnull(rdin.Rate,0) rate,isnull(rdin.Mrp,0) mrp,rdin.Tax_Id,isnull(rdin.Tax_Amount,0) tax_Amount,isnull(rdin.Gross_Amount,0) gross
                                    ,isnull(rdin.Net_Amount,0) NET_AMOUNT,ISNULL(rdin.Status,0) [status],itm.Name [item_name],itm.Item_Code,rin.narration,tax.Percentage [tax_percent],
                                    rin.Cost_Center_Id,rin.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name from TBL_REGISTER_IN_REGISTER rin
                                  	 join TBL_REGISTER_IN_DETAILS rdin on rdin.Reg_In_Id=rin.Reg_In_Id
                                  	 left join TBL_CUSTOMER_MST cus on cus.Customer_Id=rin.Customer_Id
                                  	 left join TBL_EMPLOYEE_MST emp on emp.employee_id=rin.Employee_Id
                                  	 left join TBL_LOCATION_MST loc on loc.location_id=rin.location_id
                                     left join tbl_tax_mst tax on tax.tax_id=rdin.Tax_Id 
                                  	 left join TBL_ITEM_MST itm on itm.item_id=rdin.Item_Id
                                     left join tbl_Fin_CostCenter cost on cost.Fcc_ID=rin.Cost_Center_Id
									 left join TBL_JOB_MST j on j.Job_Id=rin.Job_Id
                                  	  where rin.Location_Id=@Location_Id {#daterangefilter#} order by rin.Created_Date DESc";
                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and rin.Entry_Date>=@fromdate and rin.Entry_Date<=@todate ");
                }
                else
                {
                    To = DateTime.Now;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and rin.Entry_Date>=@fromdate and rin.Entry_Date<=@todate ");
                }
                db.CreateParameters(3);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<RegisterIn> result = new List<RegisterIn>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        RegisterIn register = new RegisterIn();
                        register.ID = row["RegisterIn_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterIn_Id"]) : 0;
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

                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterIn_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_in_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_in_detail_Id"]) : 0;
                            item.RoId = rowItem["Reg_Out_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_Out_Detail_Id"]) : 0;
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
                Application.Helper.LogException(ex, "registerin | GetDetails(int LocationId, DateTime? From, DateTime? To)");
                return null;
            }
        }
        public static RegisterIn GetDetails(int Id,int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select rin.Reg_In_Id registerIn_Id,rin.Location_Id,loc.Name [location],isnull(rin.Customer_Id,0) Customer_Id,cus.Name [customer],isnull(rin.Employee_Id,0) Employee_Id,emp.First_Name [employee],isnull(rin.Entry_Date,0) Entry_date,
                                  	 rin.Others others,isnull(rin.Order_No,0) order_No,isnull(rin.Tax_Amount,0) Total_Tax_Amount,isnull(rin.Gross_Amount,0) Total_Gross_Amount,isnull(rin.Net_Amount,0) Total_Net_Amount,isnull(rin.Round_Off,0) round_off
                                    ,rdin.Reg_In_Detail_Id,rdin.Reg_Out_Detail_Id,rdin.item_id,rdin.instance_id,isnull(rdin.Quantity,0) quantity,isnull(rdin.Rate,0) rate,isnull(rdin.Mrp,0) mrp,rdin.Tax_Id,isnull(rdin.Tax_Amount,0) tax_Amount,isnull(rdin.Gross_Amount,0) gross
                                    ,isnull(rdin.Net_Amount,0) NET_AMOUNT,ISNULL(rdin.Status,0) [status],itm.Name [item_name],itm.Item_Code,rin.narration,tax.Percentage [tax_percent],	
									rin.Cost_Center_Id,rin.Job_Id,cost.Fcc_Name[Cost_Center],j.Job_Name
									from TBL_REGISTER_IN_REGISTER rin
                                  	 join TBL_REGISTER_IN_DETAILS rdin on rdin.Reg_In_Id=rin.Reg_In_Id
                                  	 left join TBL_CUSTOMER_MST cus on cus.Customer_Id=rin.Customer_Id
                                  	 left join TBL_EMPLOYEE_MST emp on emp.employee_id=rin.Employee_Id
                                  	 left join TBL_LOCATION_MST loc on loc.location_id=rin.location_id
                                     left join tbl_tax_mst tax on tax.tax_id=rdin.Tax_Id 
                                  	 left join TBL_ITEM_MST itm on itm.item_id=rdin.Item_Id
									 left join tbl_Fin_CostCenter cost on cost.Fcc_ID=rin.Cost_Center_Id
									 left join TBL_JOB_MST j on j.Job_Id=rin.Job_Id
                                  	 where rin.Location_Id=@locationId  and rin.Reg_In_Id=@Reg_In_Id order by rin.Created_Date DESC";
                db.CreateParameters(2);
                db.AddParameters(0, "@locationId", LocationId);
                db.AddParameters(1, "@Reg_In_Id", Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    
                        DataRow row = dt.Rows[0];
                        RegisterIn register = new RegisterIn();
                        register.ID = row["RegisterIn_Id"] != DBNull.Value ? Convert.ToInt32(row["RegisterIn_Id"]) : 0;
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
                       
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("RegisterIn_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["Reg_in_detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_in_detail_Id"]) : 0;
                            item.RoId = rowItem["Reg_Out_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reg_Out_Detail_Id"]) : 0;
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
                Application.Helper.LogException(ex, "registerin | GetDetails(int Id,int LocationId)");
                return null;
            }
        }
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.RegisterIn, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "RegisterIn | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            if (this.ID == 0)
            {
                return new OutputMessage("Id must not be zero for deletion", false, Type.RequiredFields, "RegisterIn| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_REGISTER_in_DETAILS where Reg_in_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_REGISTER_in_REGISTER where Reg_in_Id=@id";
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Register In has been deleted", true, Type.NoError, "RegisterIn | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "RegisterIn | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
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
    }
}
