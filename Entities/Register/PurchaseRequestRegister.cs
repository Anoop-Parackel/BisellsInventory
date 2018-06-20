using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
    public class PurchaseRequestRegister : Register, IRegister
    {
        #region properties
        public int SupplierId { get; set; }
        public string RequestNo { get; set; }
        public string FinancialYear { get; set; }
        public int RequestStatus { get; set; }
        public bool Priority { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public List<Item> Products { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyRegId { get; set; }
        public string LocationRegId { get; set; }
        public override decimal NetAmount { get; set; }
        #endregion properties
        public PurchaseRequestRegister(int ID)
        {
            this.ID = ID;
        }
        public PurchaseRequestRegister()
        {

        }
        /// <summary>
        /// Function to Manipulate the Purchase Request Register
        /// Inserts new Entry if ID is 0
        /// Update the existing Entry with the given ID
        /// Make sure the instance properties are set
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseRequest, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseRequest | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                //Main Validations. Use ladder-if after this "if" for more validations

                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to make a request.", false, Type.Others, "Purchase Request | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.DueDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to make a request.", false, Type.Others, "Purchase Request | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a request.", false, Type.Others, "Purchase Request | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"insert into [TBL_PURCHASE_REQUEST_REGISTER]([Location_Id] ,[Request_No],[Request_Date],[Tax_amount],
                                  [Gross_Amount] ,[Net_Amount],[Narration],[Round_Off],[Request_Status],[Created_By],[Created_Date],[Due_Date],[Priorities],Cost_Center_Id,Job_Id)
	                              values(@Location_Id,[dbo].UDF_Generate_Sales_Bill("+this.CompanyId+",'"+this.FinancialYear+ "','PRQ'),@Request_Date,@Tax_amount, @Gross_Amount,@Net_Amount,@Narration,@Round_Off,0,@Created_By,GETUTCDATE(),@Due_Date,@Priorities,@Cost_Center_Id,@Job_Id);select @@identity";
                    db.CreateParameters(14);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Request_Date", this.EntryDate);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Request_Status", this.RequestStatus);
                    db.AddParameters(8, "@Created_By", this.CreatedBy);
                    db.AddParameters(9, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(10, "@Due_Date", this.DueDate);
                    db.AddParameters(11, "@Priorities", this.Priority);
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PRQ", db);
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Request | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = new Item();
                            prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            if (setting.AllowPriceEditingInPurchaseRequest)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                          
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100)*item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"insert into [TBL_PURCHASE_REQUEST_DETAILS]([Pr_Id],[Item_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Priority],[Request_Status],[instance_id],Description)
	                               values(@Pr_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                   @Priority,@Request_Status,@instance_id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Prd_Id", item.DetailsID);
                            db.AddParameters(1, "@Pr_Id", identity);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Priority", this.Priority);
                            db.AddParameters(11, "@Request_Status", this.RequestStatus);
                            db.AddParameters(12, "@instance_id", prod.InstanceId);
                            db.AddParameters(13, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
                            this.Gross += prod.Gross;
                            this.NetAmount += prod.NetAmount;
                        }
                    }
                    decimal _NetAmount=0;
                
                    if (Application.Settings.IsAutoRoundOff())
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount- this.NetAmount;
                        
                    }
                   
                    else if(this.RoundOff <= 0.5M && this.RoundOff >= -0.5M)
                    {
                        this.NetAmount = Math.Round(this.NetAmount, 2);
                        _NetAmount = this.NetAmount + this.RoundOff;
                    }
                    else
                    {
                        _NetAmount = Math.Round(this.NetAmount);
                        this.RoundOff = _NetAmount - this.NetAmount;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_REQUEST_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" +  _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt=db.ExecuteQuery(CommandType.Text, "select Request_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRQ')[New_Order],Pr_Id from TBL_PURCHASE_REQUEST_REGISTER where Pr_Id=" + identity);
                return new OutputMessage("Purchase request registered successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Request | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(),Id=dt.Rows[0]["Pr_ID"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Purchase request register could not be saved", false, Type.Others, "Purchase Request | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// update details of purchase request register and purchase request details
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>return output message of success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseRequest, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseRequest | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.EntryDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update a request.", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.DueDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update a request.", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update a request.", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (HasReference(this.ID))
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Cannot Update. This request is alredy in a transaction", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else
                {
                    db.Open();
                    string query = @"delete from TBL_PURCHASE_REQUEST_DETAILS where Pr_Id=@Pr_Id;
                                  update [TBL_PURCHASE_REQUEST_REGISTER] set [Location_Id]=@Location_Id,[Request_Date]=@Request_Date,[Tax_amount]=@Tax_amount,[Gross_Amount]=@Gross_Amount
                                  ,[Net_Amount]=@Net_Amount,[Narration]=@Narration,[Round_Off]=@Round_Off,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),
                                   [Due_Date]=@Due_Date,[Priorities]=@Priorities,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id where Pr_Id=@Pr_Id";
                    db.CreateParameters(14);
                    db.AddParameters(0, "@Location_Id", this.LocationId);
                    db.AddParameters(1, "@Request_Date", this.EntryDate);
                    db.AddParameters(2, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(3, "@Gross_Amount", this.Gross);
                    db.AddParameters(4, "@Net_Amount", this.NetAmount);
                    db.AddParameters(5, "@Narration", this.Narration);
                    db.AddParameters(6, "@Round_Off", this.RoundOff);
                    db.AddParameters(7, "@Request_Status", this.RequestStatus);      
                    db.AddParameters(8, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(9, "@Due_Date", this.DueDate);
                    db.AddParameters(10, "@Priorities", this.Priority);
                    db.AddParameters(11, "@Pr_Id", this.ID);
                    db.AddParameters(12, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(13, "@Job_Id", this.JobId);
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    dynamic setting = Application.Settings.GetFeaturedSettings();
                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected products have a quantity less than or equal to zero. Please revert and try again later", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                       
                        else
                        {
                            Item prod = new Item();

                            if (setting.AllowPriceEditingInPurchaseRequest)//check for is rate editable in setting
                            {
                                prod.CostPrice = item.CostPrice;
                            }
                            else
                            {
                                prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            }
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into [TBL_PURCHASE_REQUEST_DETAILS]([Pr_Id],[Item_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Priority],[Request_Status],[instance_id],Description)
	                               values(@Pr_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                   @Priority,@Request_Status,@instance_id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(14);
                            db.AddParameters(0, "@Prd_Id", item.DetailsID);
                            db.AddParameters(1, "@Pr_Id", this.ID);
                            db.AddParameters(2, "@Item_Id", item.ItemID);
                            db.AddParameters(3, "@Qty", item.Quantity);
                            db.AddParameters(4, "@Mrp", prod.MRP);
                            db.AddParameters(5, "@Rate", prod.CostPrice);
                            db.AddParameters(6, "@Tax_Id", prod.TaxId);
                            db.AddParameters(7, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(8, "@Gross_Amount", prod.Gross);
                            db.AddParameters(9, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(10, "@Priority", this.Priority);
                            db.AddParameters(11, "@Request_Status", this.RequestStatus);
                            db.AddParameters(12, "@instance_id", prod.InstanceId);
                            db.AddParameters(13, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_REQUEST_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pr_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select Request_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PRQ')[New_Order],Pr_Id from tbl_purchase_request_register where Pr_Id=" + ID);
                return new OutputMessage("Purchase request register updated successfully as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase Request | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(),Id=dt.Rows[0]["Pr_Id"] });
               
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Purchase request register could not be updated", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Purchase request register could not be updated", false, Type.Others, "Purchase Request | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// delete details of purchase request register and purchase request details
        /// to delete an entry first you have to set id of the particular entry
        /// </summary>
        /// <returns>return success alert when details deleted successfully and otherwise return false</returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseRequest, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseRequest | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID==0)
            {
                return new OutputMessage("No entry Selected for deletion",false,Type.RequiredFields,"PurchaseRequest | Delete",System.Net.HttpStatusCode.InternalServerError);
            }
            if (HasReference(this.ID))
            {
              
                return new OutputMessage("Cannot Delete. This request is alredy in a transaction", false, Type.Others, "Purchase Request | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_PURCHASE_REQUEST_DETAILS where Pr_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text,query);
                    query = "delete from TBL_PURCHASE_REQUEST_REGISTER where Pr_Id=@id";
                    db.AddParameters(0, "@id", this.ID);
                    db.ExecuteNonQuery(CommandType.Text,query);
                    db.CommitTransaction();
                    return new OutputMessage("Purchase request register deleted successfully", true, Type.NoError, "Purchase Request | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("You cannot deleted this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase Request | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseRequest | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                       return new OutputMessage("Something went wrong. Purchase request register could not be deleted", false, Type.Others, "PurchaseRequest | Delete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                finally
                {
                    db.Close();

                }
            }
            
        }
        /// <summary>
        /// Gets all the Purchase Request Register with list of Products
        /// </summary>
        /// <param name="LocationID">Location Id from where the request generated</param>
        /// <returns></returns>
        public static List<PurchaseRequestRegister> GetDetails(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select m.Pr_Id [PurchaseRequestId],m.Due_Date,m.Request_Status,m.Round_Off,isnull(m.Request_No,0)[Request_No],t.Percentage[TaxPercentage],
                              m.Priorities,isnull(l.Name,0)[location],L.Company_Id,isnull(c.Name,0)[company],it.Item_Code,m.Gross_Amount,m.Tax_amount,
                              isnull(m.Narration,0)[Narration],m.Net_Amount,m.Location_Id,m.Request_Date,d.Prd_Id[DetailsId],it.Item_Id,
                              it.Name[ItemName],d.Mrp,d.Rate[CostPrice],d.Qty,d.Tax_Amount[P_TaxAmount],d.Gross_Amount[P_GrossAmount],
                              d.Net_Amount[P_NetAmount],d.Request_Status[p_RequestStatus],d.instance_id,m.Created_Date,isnull(l.Address1,0)[Loc_Address1],
                              isnull(l.Address2,0)[Loc_Address2],isnull(l.Contact,0)[Loc_Phone],m.Round_Off,isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone]
                              ,l.Reg_Id1[Location_RegistrationId],c.Reg_Id1[Company_registrationId],c.Email[Company_Email],c.Logo[Comp_Logo]  
							  from TBL_PURCHASE_REQUEST_REGISTER m with(nolock)
                              join TBL_PURCHASE_REQUEST_DETAILS d with(nolock) on m.Pr_Id = d.Pr_Id 
                              left outer join TBL_ITEM_MST it with(nolock) on it.Item_Id = d.Item_Id
                              left join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=m.Location_Id
                              left join TBL_COMPANY_MST C with(nolock) on C.Company_Id=L.Company_Id
                              left join TBL_TAX_MST t with(nolock) on t.Tax_Id=d.Tax_Id
                              where m.Location_Id=@Location_Id order by m.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                 if (dt != null)
                {
                    List<PurchaseRequestRegister> result = new List<PurchaseRequestRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseRequestRegister register = new PurchaseRequestRegister();
                        register.ID = row["PurchaseRequestId"]!=DBNull.Value? Convert.ToInt32(row["PurchaseRequestId"]):0;
                        register.LocationId = row["Location_Id"]!=DBNull.Value? Convert.ToInt32(row["Location_Id"]):0;
                        register.RequestNo =  Convert.ToString(row["Request_No"]);
                        register.EntryDate = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Request_Date"]!=DBNull.Value ?Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy"):string.Empty;
                        register.Priority = row["Priorities"]!=DBNull.Value? Convert.ToBoolean(row["Priorities"]):false;
                        register.DueDateString = row["Due_Date"]!=DBNull.Value? Convert.ToDateTime(row["Due_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Gross_Amount"]):0;
                        register.TaxAmount = row["Tax_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Tax_Amount"]):0;
                        register.NetAmount = row["Net_Amount"]!=DBNull.Value? Convert.ToDecimal(row["Net_Amount"]):0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CompanyId = row["Company_Id"]!=DBNull.Value? Convert.ToInt32(row["Company_Id"]):0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CompanyRegId = Convert.ToString(row["Company_registrationId"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.RequestStatus = row["Request_Status"]!=DBNull.Value? Convert.ToInt32(row["Request_Status"]):0;
                        register.RoundOff = row["Round_Off"]!=DBNull.Value? Convert.ToDecimal(row["Round_Off"]):0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseRequestId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["DetailsId"]!=DBNull.Value? Convert.ToInt32(rowItem["DetailsId"]):0;
                            item.ItemID = rowItem["Item_Id"]!=DBNull.Value? Convert.ToInt32(rowItem["Item_Id"]):0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.MRP = rowItem["Mrp"]!=DBNull.Value? Convert.ToDecimal(rowItem["Mrp"]):0;
                            item.CostPrice = rowItem["CostPrice"]!=DBNull.Value? Convert.ToDecimal(rowItem["CostPrice"]):0;
                            item.TaxPercentage = rowItem["TaxPercentage"]!=DBNull.Value? Convert.ToDecimal(rowItem["TaxPercentage"]):0;
                            item.Gross = rowItem["P_GrossAmount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_GrossAmount"]):0;
                            item.NetAmount = rowItem["P_NetAmount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_NetAmount"]):0;
                            item.TaxAmount = rowItem["P_TaxAmount"]!=DBNull.Value? Convert.ToDecimal(rowItem["P_TaxAmount"]):0;
                            item.Quantity = rowItem["Qty"]!=DBNull.Value? Convert.ToDecimal(rowItem["Qty"]):0;
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.Status = rowItem["p_RequestStatus"]!=DBNull.Value? Convert.ToInt32(rowItem["p_RequestStatus"]):0;
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
                Application.Helper.LogException(ex, "PurchaseRequest |  GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<PurchaseRequestRegister> GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select m.Pr_Id [PurchaseRequestId],m.Due_Date,m.Request_Status,m.Round_Off,isnull(m.Request_No,0)[Request_No],t.Percentage[TaxPercentage],
                              m.Priorities,isnull(l.Name,0)[location],L.Company_Id,isnull(c.Name,0)[company],it.Item_Code,m.Gross_Amount,m.Tax_amount,
                              isnull(m.Narration,0)[Narration],m.Net_Amount,m.Location_Id,m.Request_Date,d.Prd_Id[DetailsId],it.Item_Id,
                              it.Name[ItemName],d.Mrp,d.Rate[CostPrice],d.Qty,d.Tax_Amount[P_TaxAmount],d.Gross_Amount[P_GrossAmount],
                              d.Net_Amount[P_NetAmount],d.Request_Status[p_RequestStatus],d.instance_id,m.Created_Date,isnull(l.Address1,0)[Loc_Address1],
                              isnull(l.Address2,0)[Loc_Address2],isnull(l.Contact,0)[Loc_Phone],m.Round_Off,isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone]
                              ,l.Reg_Id1[Location_RegistrationId],c.Reg_Id1[Company_registrationId],c.Email[Company_Email],c.Logo[Comp_Logo],
                              cost.Fcc_Name[Cost_Center_Name],isnull(m.Cost_Center_Id,0)[Cost_Center_Id],isnull(m.Job_Id,0)[Job_Id],j.Job_Name[Job],d.Description  
                              from TBL_PURCHASE_REQUEST_REGISTER m with(nolock)
                              join TBL_PURCHASE_REQUEST_DETAILS d with(nolock) on m.Pr_Id = d.Pr_Id 
                              left outer join TBL_ITEM_MST it with(nolock) on it.Item_Id = d.Item_Id
                              inner join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=m.Location_Id
                              inner join TBL_COMPANY_MST C with(nolock) on C.Company_Id=L.Company_Id
                              inner join TBL_TAX_MST t with(nolock) on t.Tax_Id=d.Tax_Id
                              left join tbl_Fin_CostCenter cost on cost.Fcc_ID=m.Cost_Center_Id
                              left join tbl_job_mst j on j.Job_Id=m.Job_Id
                              where m.Location_Id=@Location_Id  {#supplierfilter#} {#daterangefilter#} order by m.Created_Date desc";
                #endregion query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", " and m.Request_Date>=@fromdate and m.Request_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", " and m.Request_Date>=@fromdate and m.Request_Date<=@todate ");
                }
                if (SupplierId != null && SupplierId > 0)
                {
                    query = query.Replace("{#supplierfilter#}", " and m.Supplier_Id=@Supplier_Id ");
                }
                else
                {

                    query = query.Replace("{#supplierfilter#}", string.Empty);
                }
                db.CreateParameters(4);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@fromdate", from);
                db.AddParameters(2, "@todate", to);
                db.AddParameters(3, "@Supplier_Id", SupplierId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseRequestRegister> result = new List<PurchaseRequestRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseRequestRegister register = new PurchaseRequestRegister();
                        register.ID = row["PurchaseRequestId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseRequestId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.RequestNo = Convert.ToString(row["Request_No"]);
                        register.EntryDate = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Priority = row["Priorities"] != DBNull.Value ? Convert.ToBoolean(row["Priorities"]) : false;
                        register.DueDateString = row["Due_Date"] != DBNull.Value ? Convert.ToDateTime(row["Due_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CompanyId = row["Company_Id"] != DBNull.Value ? Convert.ToInt32(row["Company_Id"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center_Name"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.LocationRegId = Convert.ToString(row["Location_RegistrationId"]);
                        register.CompanyRegId = Convert.ToString(row["Company_registrationId"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.CompanyEmail = Convert.ToString(row["Company_Email"]);
                        register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                        register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                        register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                        register.RequestStatus = row["Request_Status"] != DBNull.Value ? Convert.ToInt32(row["Request_Status"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseRequestId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["DetailsId"] != DBNull.Value ? Convert.ToInt32(rowItem["DetailsId"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Gross = rowItem["P_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_GrossAmount"]) : 0;
                            item.NetAmount = rowItem["P_NetAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_NetAmount"]) : 0;
                            item.TaxAmount = rowItem["P_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_TaxAmount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.Status = rowItem["p_RequestStatus"] != DBNull.Value ? Convert.ToInt32(rowItem["p_RequestStatus"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseRequest | GetDetails(int LocationID, int? SupplierId, DateTime? from, DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static PurchaseRequestRegister GetDetails(int Id,int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select m.Pr_Id [PurchaseRequestId],m.Due_Date,m.Request_Status,m.Round_Off,isnull(m.Request_No,0)[Request_No],t.Percentage[TaxPercentage],
                               m.Priorities,isnull(l.Name,0)[location],L.Company_Id,isnull(c.Name,0)[company],it.Item_Code,m.Gross_Amount,m.Tax_amount,
                               isnull(m.Narration,0)[Narration],m.Net_Amount,m.Location_Id,m.Request_Date,d.Prd_Id[DetailsId],it.Item_Id,
                               it.Name[ItemName],d.Mrp,d.Rate[CostPrice],d.Qty,d.Tax_Amount[P_TaxAmount],d.Gross_Amount[P_GrossAmount],
                               d.Net_Amount[P_NetAmount],d.Request_Status[p_RequestStatus],d.instance_id,m.Created_Date,isnull(l.Address1,0)[Loc_Address1],
                               isnull(l.Address2,0)[Loc_Address2],isnull(l.Contact,0)[Loc_Phone],l.Reg_Id1[Loc_RegId],m.Round_Off,
                               cost.Fcc_Name[Cost_Center_Name],isnull(m.Cost_Center_Id,0)[Cost_Center_Id],isnull(m.Job_Id,0)[Job_Id],j.Job_Name[Job],d.Description  
                               from TBL_PURCHASE_REQUEST_REGISTER m with(nolock)
                               join TBL_PURCHASE_REQUEST_DETAILS d with(nolock) on m.Pr_Id = d.Pr_Id 
                               left outer join TBL_ITEM_MST it with(nolock) on it.Item_Id = d.Item_Id
                               inner join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=m.Location_Id
                               inner join TBL_COMPANY_MST C with(nolock) on C.Company_Id=L.Company_Id
                               inner join TBL_TAX_MST t with(nolock) on t.Tax_Id=d.Tax_Id
                               left join tbl_Fin_CostCenter cost on cost.Fcc_ID=m.Cost_Center_Id
                               left join TBL_JOB_MST j on j.Job_Id=m.Job_Id
                               where m.Location_Id=@Location_Id and m.Pr_Id=@Pr_Id order by m.Created_Date desc";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationID);
                db.AddParameters(1, "@Pr_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                   
                        DataRow row = dt.Rows[0];
                        PurchaseRequestRegister register = new PurchaseRequestRegister();
                        register.ID = row["PurchaseRequestId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseRequestId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.RequestNo = Convert.ToString(row["Request_No"]);
                        register.EntryDate = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Priority = row["Priorities"] != DBNull.Value ? Convert.ToBoolean(row["Priorities"]) : false;
                        register.DueDateString = row["Due_Date"] != DBNull.Value ? Convert.ToDateTime(row["Due_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.CostCenter = Convert.ToString(row["Cost_Center_Name"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.CompanyId = row["Company_Id"] != DBNull.Value ? Convert.ToInt32(row["Company_Id"]) : 0;
                        register.Location = Convert.ToString(row["Location"]);
                        register.LocationAddress1 = Convert.ToString(row["Loc_Address1"]);
                        register.LocationAddress2 = Convert.ToString(row["Loc_Address2"]);
                        register.LocationPhone = Convert.ToString(row["Loc_Phone"]);
                        register.LocationRegId = Convert.ToString(row["Loc_RegId"]);
                        register.Company = Convert.ToString(row["Company"]);
                        register.RequestStatus = row["Request_Status"] != DBNull.Value ? Convert.ToInt32(row["Request_Status"]) : 0;
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("PurchaseRequestId") == register.ID).CopyToDataTable();
                        List<Item> products = new List<Item>();
                        for (int j = 0; j < inProducts.Rows.Count; j++)
                        {
                            DataRow rowItem = inProducts.Rows[j];
                            Item item = new Item();
                            item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                            item.DetailsID = rowItem["DetailsId"] != DBNull.Value ? Convert.ToInt32(rowItem["DetailsId"]) : 0;
                            item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                            item.Name = rowItem["ItemName"].ToString();
                            item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                            item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                            item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                            item.Description = Convert.ToString(rowItem["Description"]);
                            item.Gross = rowItem["P_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_GrossAmount"]) : 0;
                            item.NetAmount = rowItem["P_NetAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_NetAmount"]) : 0;
                            item.TaxAmount = rowItem["P_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_TaxAmount"]) : 0;
                            item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
                            item.ItemCode = Convert.ToString(rowItem["Item_Code"]);
                            item.Status = rowItem["p_RequestStatus"] != DBNull.Value ? Convert.ToInt32(rowItem["p_RequestStatus"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseRequest | GetDetails(int Id,int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// check for the purchase Request is refered in another transactions
        /// </summary>
        /// <param name="PurchaseRequestId (id)"></param>
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
                    string query = @"select count(*) from TBL_PURCHASE_REQUEST_DETAILS r with(nolock) join 
                                     TBL_PURCHASE_QUOTE_DETAILS q with(nolock) on r.Prd_Id=q.Prd_Id 
                                     join TBL_PURCHASE_QUOTE_REGISTER qr with(nolock) on 
                                     q.Pq_Id=qr.Pq_Id where r.Pr_Id=@id";
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
                    Application.Helper.LogException(ex, "PurchaseRequest | HasReference(int id)");
                    throw;
                }
            }




        }

    }
}
