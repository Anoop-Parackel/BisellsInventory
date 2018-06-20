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
    public class Damage : Register.Register, IRegister
    {
        #region Properties
        public string DamageNo { get; set; }
        public string FinancialYear { get; set; }
        public List<Item> Products { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public string LocationRegNo { get; set; }
        public int Status { get; set; }
        #endregion Properties

        #region Functions
        /// <summary>
        /// Function for saving damage details
        /// </summary>
        /// <returns>Return successalert when details inserted successfully otherwise returns an erroralert</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Damage, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Damage | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a damage entry.", false, Type.Others, "Damage | Save", System.Net.HttpStatusCode.InternalServerError);
                }
               else if(string.IsNullOrWhiteSpace(this.Narration))
                {
                    return new OutputMessage("Narration must not be empty", false, Type.Others, "Damage | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"insert into TBL_DAMAGE_REGISTER (Location_Id,Damage_No,Damage_Date,Tax_Amount,
                                  Gross_Amount,Net_Amount,Narration,Round_Off,[Status],Created_By,Created_Date)
                                  values(@Location_Id,[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','DMG'),@Damage_Date,@Tax_Amount,@Gross_Amount,@Net_Amount,@Narration,@Round_Off,@Status,@Created_By,GETUTCDATE());select @@identity;";
                    db.CreateParameters(9);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Damage_Date", this.EntryDate);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Status", this.Status);
                    db.AddParameters(8, "@Created_By", this.CreatedBy);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "DMG", db);
                  
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Damage | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId,this.LocationId);
                            if (prod.Stock < item.Quantity)
                            {
                                return new OutputMessage("Quantity Exceeds", false, Type.Others, "Damage |Save", System.Net.HttpStatusCode.InternalServerError);
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"insert into TBL_DAMAGE_DETAILS (Damage_Id,instance_id,Item_Id,Quantity,Mrp,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Status)
                                    values(@Damage_Id,@instance_id,@Item_Id,@Quantity,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status)";
                            db.CleanupParameters();
                            db.CreateParameters(11);
                            db.AddParameters(0, "@Damage_Id", identity);
                            db.AddParameters(1, "@instance_id", item.InstanceId);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Quantity", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Status", prod.Status);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                        }
                    }
                    decimal _NetAmount = Math.Round(this.NetAmount);
                    this.RoundOff = this.NetAmount - _NetAmount;
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_DAMAGE_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Damage_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Damage_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','DMG')[New_Order],Damage_Id from TBL_DAMAGE_REGISTER where Damage_Id=" + identity);
                return new OutputMessage("Damage entry registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Damage | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Damage_Id"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Damage entry could not be saved", false, Type.Others, "Damage | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Function for updating the damage details
        /// </summary>
        /// <returns>Return successalert when details updates successfully otherwise returns an erroralert</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Damage, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Damage | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a damage entry.", false, Type.Others, "Damage | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if(string.IsNullOrWhiteSpace(this.Narration))
                {
                    return new OutputMessage("Narration must not be empty", false, Type.Others, "Damage | Update", System.Net.HttpStatusCode.InternalServerError);
                }
              
                else
                {
                    db.Open();
                    string query = @"delete from TBL_DAMAGE_DETAILS where Damage_Id=@Damage_Id;
                                   update TBL_DAMAGE_REGISTER set Location_Id=@Location_Id,Damage_Date=@Damage_Date,Tax_Amount=@Tax_Amount,Gross_Amount=@Gross_Amount,
                                   Net_Amount=@Net_Amount,Narration=@Narration,Round_Off=@Round_Off,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Damage_Id=@Damage_Id";
                    db.CreateParameters(9);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Damage_Date", this.EntryDate);
                    db.AddParameters(2, "@Tax_Amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(8, "@Damage_Id", this.ID);
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                  
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Damage | Update", System.Net.HttpStatusCode.InternalServerError);
                        }

                        else
                        {
                            Item prod = Item.GetProductFromStock(item.ItemID, item.InstanceId,this.LocationId);
                              if (prod.Stock < item.Quantity)
                            {
                                return new OutputMessage("Quantity Exceeds", false, Type.Others, "Damage |Update", System.Net.HttpStatusCode.InternalServerError);
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into TBL_DAMAGE_DETAILS (Damage_Id,instance_id,Item_Id,Quantity,Mrp,Rate,Tax_Id,Tax_Amount,Gross_Amount,Net_Amount,Status)
                                    values(@Damage_Id,@instance_id,@Item_Id,@Quantity,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,@Status)";
                            db.CleanupParameters();
                            db.CreateParameters(11);
                            db.AddParameters(0, "@Damage_Id", this.ID);
                            db.AddParameters(1, "instance_id", item.InstanceId);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Quantity", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Status", prod.Status);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                        }

                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_DAMAGE_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + this.NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Damage_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Damage_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','DMG')[New_Order],Damage_Id from TBL_DAMAGE_REGISTER where Damage_Id=" + ID);
                return new OutputMessage("Damage entry updated successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Damage | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Damage_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Damage | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Damage entry could not be updated", false, Type.Others, "Damage | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Damage entry could not be updated", false, Type.Others, "Damage | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// Function for deleting damage register and its details
        /// for deleting an entry id must not be empty
        /// </summary>
        /// <returns>return successalert when details deleted successfully otherwise returns an error alert</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Damage, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Damage | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("No damage entry is Selected for deletion", false, Type.RequiredFields, "Damage | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_DAMAGE_DETAILS where Damage_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_DAMAGE_REGISTER where Damage_Id=@id";
                    db.AddParameters(0, "@id", this.ID);
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Damage entry deleted successfully", true, Type.NoError, "Damage | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("You cannot deleted this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Damage | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Damage | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Damage entry could not be deleted", false, Type.Others, "Damage | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                finally
                {
                    db.Close();

                }
            }
        }

        public static List<Damage> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select dr.Damage_Id,dr.Location_Id,dr.Damage_No,dr.Damage_Date,dr.Tax_Amount,dr.Net_Amount,dr.Gross_Amount,dr.Round_Off,
                              d.Damage_Detail_Id,d.Item_Id,d.Instance_Id,d.Item_Id,d.Quantity,d.Mrp,d.Rate,d.Tax_Amount,d.Gross_Amount,d.Net_Amount,l.Name[Location],
                              it.Item_Code,it.Name[Item],tx.Percentage[Tax_Percentage],dr.Narration
                              from TBL_DAMAGE_REGISTER dr
                              left join TBL_DAMAGE_DETAILS d on d.Damage_Id=dr.Damage_Id
                              left join TBL_LOCATION_MST l on l.Location_Id=dr.Location_Id
                              left join TBL_ITEM_MST it on it.Item_Id=d.Item_Id
                              left join TBL_TAX_MST tx on tx.Tax_Id=d.Tax_Id
                              where dr.Location_Id=@Location_Id order by dr.Damage_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<Damage> result = new List<Damage>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        Damage register = new Damage();
                        register.ID = row["Damage_Id"] != DBNull.Value ? Convert.ToInt32(row["Damage_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.DamageNo = Convert.ToString(row["Damage_No"]);
                        register.EntryDate = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Damage_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Damage_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Damage_Detail_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "damage | GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for retrieving the list of damage details
        /// </summary>
        /// <param name="LocationID">Location Id of damage entry</param>
        /// <param name="From">From date for filter details</param>
        /// <param name="To">To date for filter details</param>
        /// <returns>list of damage entries</returns>
        public static List<Damage> GetDetails(int LocationID, DateTime? From, DateTime? To)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @" select dr.Damage_Id,dr.Location_Id,dr.Damage_No,dr.Damage_Date,dr.Tax_Amount,dr.Net_Amount,dr.Gross_Amount,dr.Round_Off,
                              d.Damage_Detail_Id,d.Item_Id,d.Instance_Id,d.Item_Id,d.Quantity,d.Mrp,d.Rate,d.Tax_Amount,d.Gross_Amount,d.Net_Amount,l.Name[Location],l.Address1[LocationAddress],l.Contact[LocationContact],l.Reg_Id1[LocationReg], 
                              it.Item_Code,it.Name[Item],tx.Percentage[Tax_Percentage],dr.Narration
							  from TBL_DAMAGE_REGISTER dr
                              left join TBL_DAMAGE_DETAILS d on d.Damage_Id=dr.Damage_Id
                              left join TBL_LOCATION_MST l on l.Location_Id=dr.Location_Id
                              left join TBL_ITEM_MST it on it.Item_Id=d.Item_Id
                              left join TBL_TAX_MST tx on tx.Tax_Id=d.Tax_Id
							  where dr.Location_Id=@Location_Id  {#daterangefilter#} order by dr.Damage_Date desc
";

                if (From != null && To != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and dr.Damage_Date>=@fromdate and dr.Damage_Date<=@todate ");
                }
                else
                {
                    To = DateTime.Now;
                    From = new DateTime(To.Value.Year, To.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and dr.Damage_Date>=@fromdate and dr.Damage_Date<=@todate ");
                }
               
                db.CreateParameters(3);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", From);
                db.AddParameters(2, "@todate", To);
              
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<Damage> result = new List<Damage>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        Damage register = new Damage();
                        register.ID = row["Damage_Id"] != DBNull.Value ? Convert.ToInt32(row["Damage_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.DamageNo = Convert.ToString(row["Damage_No"]);
                        register.EntryDate = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Damage_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Damage_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Damage_Detail_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "damage | GetDetails(int LocationID, DateTime? From, DateTime? To)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for retrieving a single entry from list
        /// </summary>
        /// <param name="Id">Id of that particular entry you want to retrieve</param>
        /// <param name="LocationID">Location id of that entry</param>
        /// <returns>single damage entry details</returns>
        public static Damage GetDetails(int Id,int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select dr.Damage_Id,dr.Location_Id,dr.Damage_No,dr.Damage_Date,dr.Tax_Amount,dr.Net_Amount,dr.Gross_Amount,dr.Round_Off,
                              d.Damage_Detail_Id,d.Item_Id,d.Instance_Id,d.Item_Id,d.Quantity,d.Mrp,d.Rate,d.Tax_Amount,d.Gross_Amount,d.Net_Amount,l.Name[Location],
                              it.Item_Code,it.Name[Item],tx.Percentage[Tax_Percentage],dr.Narration,l.Address1[Loc_address1],l.Contact[Loc_Phone],
							  l.Reg_Id1[Loc_RegNo],l.Address2[Loc_Address2]
                              from TBL_DAMAGE_REGISTER dr
                              left join TBL_DAMAGE_DETAILS d on d.Damage_Id=dr.Damage_Id
                              left join TBL_LOCATION_MST l on l.Location_Id=dr.Location_Id
                              left join TBL_ITEM_MST it on it.Item_Id=d.Item_Id
                              left join TBL_TAX_MST tx on tx.Tax_Id=d.Tax_Id
                              where dr.Location_Id=@Location_Id and dr.Damage_Id=@Damage_Id order by dr.Damage_Date desc";
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@Damage_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                   
                        DataRow row = dt.Rows[0];
                        Damage register = new Damage();
                        register.ID = row["Damage_Id"] != DBNull.Value ? Convert.ToInt32(row["Damage_Id"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.DamageNo = Convert.ToString(row["Damage_No"]);
                        register.EntryDate = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Damage_Date"] != DBNull.Value ? Convert.ToDateTime(row["Damage_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegNo = Convert.ToString(row["Loc_RegNo"]);
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Damage_Id") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.DetailsID = rowItem["Damage_Detail_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Damage_Detail_Id"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.Name = Convert.ToString(rowItem["Item"]);
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["Rate"] != DBNull.Value ? Convert.ToDecimal(rowItem["Rate"]) : 0;
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
                Application.Helper.LogException(ex, "damage | GetDetails(int Id,int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        #endregion Functions
    }
}

