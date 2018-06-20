using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
using System.Data;

namespace Entities.Register
{
    public class Scheme
    {
        #region Properties
        public string SchemeName { get; set; }
        public int SchemeId { get; set; }
        public int LocationId { get; set; }
     
        public decimal Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public decimal AmountOrPercentage { get; set; }
        public string Types { get; set; }
        public string Modes { get; set; }
        public bool IsPercentageBased { get; set; }
        public int Status { get; set; }
        public int SchemeType { get; set; }
        public string Location { get; set; }
        public int Mode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<Entities.Customer> Customers { get; set; }
        public List<Entities.Register.Item> Items { get; set; }
        #endregion Properties
        /// <summary>
        /// Save details of each schemes,scheme customer relation and scheme product relation
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Scheme, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesCreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }

            DBManager db = new DBManager();
            try
            {
                if(!string.IsNullOrWhiteSpace(this.StartDateString))
                 {
                    return new OutputMessage("Select date for  Scheme.", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                }
              else if (this.StartDate.Year < 1900)

                {
                    return new OutputMessage("Select a valid date to make a Scheme.", false, Type.Others, "Scheme  | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.EndDate.Year < 1990)
                {
                    return new OutputMessage("Select a valid End date to make a Scheme.", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (string.IsNullOrWhiteSpace((this.SchemeName)))
                {
                    return new OutputMessage("Please enter scheme name to make a Scheme.", false, Type.Others, " Scheme | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if(this.AmountOrPercentage==0)
                {
                    return new OutputMessage("Please enter amount or percentage to make a scheme", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    string query = @"insert into TBL_SCHEMES (Name,Quantity,Scheme_Start_Date,Scheme_End_Date,Amount_Or_Percentage,Ispercentage,Scheme_Type,Scheme_Status,Mode,Created_By,Created_Date,Location_Id) 
                 values (@Name,@Quantity,@Scheme_Start_Date,@Scheme_End_Date,@Amount_Or_Percentage,@Ispercentage,@Scheme_Type,@Scheme_Status,@Mode,@Created_By,GETUTCDATE(),@Location_Id);select @@IDENTITY";
                    db.CreateParameters(11);
                    db.AddParameters(0, "@Name", this.SchemeName);
                    db.AddParameters(1, "@Quantity", this.Quantity);
                    db.AddParameters(2, "@Scheme_Start_Date", this.StartDate);
                    db.AddParameters(3, "@Scheme_End_Date", this.EndDate);
                    db.AddParameters(4, "@Amount_Or_Percentage", this.AmountOrPercentage);
                    db.AddParameters(5, "@Scheme_Type", this.SchemeType);
                    db.AddParameters(6, "@Scheme_Status", this.Status);
                    db.AddParameters(7, "@Mode", this.Mode);
                    db.AddParameters(8, "@Created_By", this.CreatedBy);
                    db.AddParameters(9, "@Ispercentage", this.IsPercentageBased);
                    db.AddParameters(10, "@Location_Id", this.LocationId);
                    db.Open();
                    db.BeginTransaction();
                    int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    if (this.Items == null || this.Items.Count==0)
                    {
                        return new OutputMessage("You must select some item to create this scheme", false, Type.RequiredFields, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        foreach (Item item in this.Items)
                        {
                            db.CleanupParameters();
                            query = @"insert into TBL_SCHEME_PRODUCT_RELATION (Scheme_Id,Item_Id) values (@Scheme_Id,@Item_Id)";
                            db.CreateParameters(2);
                            db.AddParameters(0, "@Scheme_Id", identity);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.ExecuteNonQuery(CommandType.Text, query);                           
                        }

                    }
                    if(this.Customers==null || this.Items.Count==0)
                    {
                        return new OutputMessage("You must select some customer to create this scheme", false, Type.RequiredFields, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else
                    {
                        foreach (Customer customer in this.Customers)
                        {
                            db.CleanupParameters();
                            query = @"insert into TBL_SCHEME_CUSTOMER_RELATION (Scheme_Id,Customer_Id) values (@Scheme_Id,@Customer_Id)";
                            db.CreateParameters(2);
                            db.AddParameters(0, "@Scheme_Id", identity);
                            db.AddParameters(1, "@Customer_Id", customer.ID);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                    }

                    db.CommitTransaction();
                    return new OutputMessage("Scheme saved successfully", true, Type.NoError, "Scheme | Save", System.Net.HttpStatusCode.OK, identity.ToString());

                }
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Scheme could not be saved", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Scheme, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesCreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                if (this.StartDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update a scheme.", false, Type.Others, "Scheme  | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.EndDate.Year < 1990)
                {
                    return new OutputMessage("Select a valid End date to update a scheme.", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else if (string.IsNullOrWhiteSpace((this.SchemeName)))
                {
                    return new OutputMessage("Please enter scheme name to update a scheme.", false, Type.Others, " Scheme | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.AmountOrPercentage == 0)
                {
                    return new OutputMessage("Please enter amount or percentage to update a scheme", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    string query = @"delete from TBL_SCHEME_PRODUCT_RELATION where Scheme_Id=@Id;
                                     delete from TBL_SCHEME_CUSTOMER_RELATION where Scheme_Id=@Id;
                                     update TBL_SCHEMES set Name=@Name,Quantity=@Quantity,Scheme_Start_Date=@Scheme_Start_Date,Scheme_End_Date=@Scheme_End_Date,Amount_Or_Percentage=@Amount_Or_Percentage
                                   ,Ispercentage=@Ispercentage,Scheme_Type=@Scheme_Type,Scheme_Status=@Scheme_Status,Mode=@Mode,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Scheme_Id=@Id";
                    db.CreateParameters(11);
                    db.AddParameters(0, "@Name", this.SchemeName);
                    db.AddParameters(1, "@Quantity", this.Quantity);
                    db.AddParameters(2, "@Scheme_Start_Date", this.StartDate);
                    db.AddParameters(3, "@Scheme_End_Date", this.EndDate);
                    db.AddParameters(4, "@Amount_Or_Percentage", this.AmountOrPercentage);
                    db.AddParameters(5, "@Scheme_Type", this.SchemeType);
                    db.AddParameters(6, "@Scheme_Status", this.Status);
                    db.AddParameters(7, "@Mode", this.Mode);
                    db.AddParameters(8, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(9, "@Ispercentage", this.IsPercentageBased);
                    db.AddParameters(10, "@Id", this.SchemeId);
                    db.Open();
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                  
                    if (this.Items == null || this.Items.Count == 0)
                    {
                        return new OutputMessage("You must select some item to create this scheme", false, Type.RequiredFields, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        foreach (Item item in this.Items)
                        {
                            db.CleanupParameters();
                            query = @"insert into TBL_SCHEME_PRODUCT_RELATION (Scheme_Id,Item_Id) values (@Scheme_Id,@Item_Id)";
                            db.CreateParameters(2);
                            db.AddParameters(0, "@Scheme_Id", this.SchemeId);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }

                    }

                    if (this.Customers == null || this.Items.Count == 0)
                    {
                        return new OutputMessage("You must select some customer to create scheme", false, Type.RequiredFields, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else
                    {
                        foreach (Customer customer in this.Customers)
                        {
                            db.CleanupParameters();
                            query = @"insert into TBL_SCHEME_CUSTOMER_RELATION (Scheme_Id,Customer_Id) values (@Scheme_Id,@Customer_Id)";
                            db.CreateParameters(2);
                            db.AddParameters(0, "@Scheme_Id", this.SchemeId);
                            db.AddParameters(1, "@Customer_Id", customer.ID);
                            db.ExecuteNonQuery(CommandType.Text, query);
                        }
                    }

                    db.CommitTransaction();
                    return new OutputMessage("Scheme updated successfully ", true, Type.NoError, "Scheme | Save", System.Net.HttpStatusCode.OK, this.SchemeId.ToString());

                }
            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Scheme | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Scheme | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Scheme could not be saved", false, Type.Others, "Scheme | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }
        }
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Scheme, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "SalesCreditNote | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (this.SchemeId != 0)
                    {
                        db.Open();

                        string query = @"delete from TBL_SCHEMES where Scheme_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.SchemeId);
                        string query1 = "DELETE from TBL_SCHEME_CUSTOMER_RELATION WHERE Scheme_Id=@ID";
                        string query2 = "DELETE from TBL_SCHEME_PRODUCT_RELATION WHERE Scheme_Id=@ID";

                        db.BeginTransaction();
                      int q1=  Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text,query1));
                        int q2 =Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text,query2));
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false && q1 >= 1 && q2 >= 1)
                        {
                            db.CommitTransaction();
                            return new OutputMessage("Scheme Deleted Successfully", true, Entities.Type.NoError, "Scheme | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            db.RollBackTransaction();
                            return new OutputMessage("Something went wrong. Scheme could not be deleted", false, Entities.Type.Others, "Scheme | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                     
                        return new OutputMessage("ID must not be zero for deletion", false, Entities.Type.Others, "Scheme | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this Scheme because it is referenced in other transactions", false, Entities.Type.Others, "Scheme | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Scheme could not be deleted", false, Entities.Type.Others, "Scheme | Delete", System.Net.HttpStatusCode.InternalServerError,ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Scheme could not be deleted", false, Type.Others, "Scheme | Save", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                }
                finally
                {
                   db.Close();
               }

            }
        }
        public static List<Scheme> GetDetails(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                List<Scheme> result = new List<Scheme>();
                string query = @"select  s.Scheme_Id,s.Name,s.Formula,s.Quantity,s.Scheme_Start_Date,s.Scheme_End_Date,s.Amount_Or_Percentage,
                               s.IsPercentage,s.Scheme_Type,s.Scheme_Status,s.Mode,s.Created_By,s.Created_date,s.Modified_By, s.Modified_date,
                               s.Location_Id,case s.Scheme_Type when 0 then 'Primary' when 1 then 'Secondary' end[Type],case s.mode when 0 then 
                               'Mrp Based' when 1 then 'SP Based'  when 2 then 'Lc Based' end[Modes] from TBL_SCHEMES s where Location_Id=@Location_Id";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {                        
                        Scheme sch = new Scheme();
                        sch.SchemeId = item["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(item["Scheme_Id"]) : 0;
                        sch.SchemeName = Convert.ToString(item["Name"]);
                        sch.Quantity = item["Quantity"] != DBNull.Value ? Convert.ToDecimal(item["Quantity"]) : 0;
                        sch.StartDateString = item["Scheme_Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Scheme_Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        sch.EndDateString = item["Scheme_End_Date"] != DBNull.Value ? Convert.ToDateTime(item["Scheme_End_Date"]).ToString("dd/MMM/yyy") : string.Empty;
                        sch.AmountOrPercentage = item["Amount_Or_Percentage"] != DBNull.Value ? Convert.ToDecimal(item["Amount_Or_Percentage"]) : 0;
                        sch.IsPercentageBased = Convert.ToBoolean(item["IsPercentage"]);
                        sch.Types = Convert.ToString(item["Type"]);
                        sch.Status = item["Scheme_Status"] != DBNull.Value ? Convert.ToInt32(item["Scheme_Status"]) : 0;
                        sch.Modes = Convert.ToString(item["Modes"]);
                        result.Add(sch);
                    }
                return result;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Scheme |  GetDetails(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public static Scheme GetDetails(int id, int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select top 1 s.Scheme_Id,s.Name,s.Formula,s.Quantity,s.Scheme_Start_Date,s.Scheme_End_Date,s.Amount_Or_Percentage,s.IsPercentage
                             ,s.Scheme_Type,s.Scheme_Status,s.Mode,s.Created_By,s.Created_Date,s.Modified_By,s.Modified_Date,l.Location_Id,
                              l.Name[Location] from TBL_SCHEMES s left join TBL_LOCATION_MST l on l.Location_Id=s.Location_Id where s.Location_Id=@Location_Id and Scheme_Id=@id
                              ;select Item_Id from TBL_SCHEME_PRODUCT_RELATION where Scheme_Id =@id select Customer_Id from TBL_SCHEME_CUSTOMER_RELATION where SCHEME_ID=@id";
                #endregion query
                db.CreateParameters(2);
                db.AddParameters(0, "@Location_Id", LocationId);
                db.AddParameters(1, "@id", id);
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                if (ds != null)
                {                    
                    Scheme sch = new Scheme();
                    DataRow item = ds.Tables[0].Rows[0];
                    sch.SchemeId = item["Scheme_Id"] != DBNull.Value ? Convert.ToInt32(item["Scheme_Id"]) : 0;
                    sch.SchemeName = Convert.ToString(item["Name"]);
                    sch.Quantity = item["Quantity"] != DBNull.Value ? Convert.ToDecimal(item["Quantity"]) : 0;
                    sch.StartDateString = item["Scheme_Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Scheme_Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    sch.EndDateString = item["Scheme_End_Date"] != DBNull.Value ? Convert.ToDateTime(item["Scheme_End_Date"]).ToString("dd/MMM/yyy") : string.Empty;
                    sch.AmountOrPercentage = item["Amount_Or_Percentage"] != DBNull.Value ? Convert.ToDecimal(item["Amount_Or_Percentage"]) : 0;
                    sch.IsPercentageBased = Convert.ToBoolean(item["IsPercentage"]);
                    sch.SchemeType = item["Scheme_Type"] != DBNull.Value ? Convert.ToInt32(item["Scheme_Type"]) : 0;
                    sch.Status = item["Scheme_Status"] != DBNull.Value ? Convert.ToInt32(item["Scheme_Status"]) : 0;
                    sch.Mode = item["Mode"] != DBNull.Value ? Convert.ToInt32(item["Mode"]) : 0;
                    sch.Location = Convert.ToString(item["Location"]);
                    List<Item> items = new List<Item>();
                    foreach (DataRow product in ds.Tables[1].Rows)
                    {
                        items.Add(new Item() { ItemID = Convert.ToInt32(product["Item_Id"]) });
                    }
                    List<Customer> customers = new List<Customer>();
                    foreach (DataRow customer in ds.Tables[2].Rows)
                    {
                        customers.Add(new Customer() { ID = Convert.ToInt32(customer["Customer_Id"]) });
                    }
                    sch.Items = items;
                    sch.Customers = customers;
                    return sch;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Scheme | GetDetails(int id, int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static DataTable GetItems()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"SELECT [Item_Id],[Name] ,[Item_Code],[HS_Code],[OEM_Code] FROM [dbo].[TBL_ITEM_MST] where [Status]=1";
                db.Open();
                return db.ExecuteQuery(CommandType.Text,query);
            }
            finally
            {
                db.Close();
            }

        }
        public static DataTable GetItems(List<int> brandlist,List<int> Categorylist,List<int> Grouplist,List<int> ProductTypelist,string key)
        {
            DBManager db = new DBManager();
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append("select [Item_Id],[Name] ,[Item_Code],[HS_Code],[OEM_Code] FROM [dbo].[TBL_ITEM_MST] where [Status]=1 ");
                int flag = 0;
                int post = 0;
                if (brandlist.Count>0)
                {
                    for (int i = 0; i < brandlist.Count; i++)
                    {
                        if (flag > 0)
                        {
                            query.Append(" or ");
                            post = 1;
                        }

                        if (i == brandlist.Count - 1 && post == 1)
                        {
                            query.Append(" brand_id=" + brandlist[i]);
                            flag = 0;
                        }
                        else
                        {
                            if (post == 1)
                            {
                                query.Append("  brand_id=" + brandlist[i] + "  ");
                                flag = 1;
                            }
                            else
                            {
                                query.Append(" AND (brand_id=" + brandlist[i] + "  ");
                                post = 1;
                                flag = 1;
                            }

                        }


                    }
                    query.Append(" )");
                }
                 if (Categorylist.Count>0)
                {
                    flag = 0;
                    post = 0;
                    for (int i = 0; i < Categorylist.Count; i++)
                    {
                        if (flag > 0)
                        {
                            query.Append(" or ");
                            post = 1;
                        }

                        if (i == Categorylist.Count - 1 && post == 1)
                        {
                            query.Append(" category_id=" + Categorylist[i]);
                            flag = 0;
                        }
                        else
                        {
                            if (post == 1)
                            {
                                query.Append("  category_id=" + Categorylist[i] + "  ");
                                flag = 1;
                            }
                            else
                            {
                                query.Append(" AND (category_id=" + Categorylist[i] + "  ");
                                post = 1;
                                flag = 1;
                            }

                        }


                    }
                    query.Append(" )");
                }
              if (Grouplist.Count>0)
                {
                    flag = 0;
                    post = 0;
                    for (int i = 0; i < Grouplist.Count; i++)
                    {
                        if (flag > 0)
                        {
                            query.Append(" or ");
                            post = 1;
                        }

                        if (i == Grouplist.Count - 1 && post == 1)
                        {
                            query.Append(" group_id=" + Grouplist[i]);
                            flag = 0;
                        }
                        else
                        {
                            if (post == 1)
                            {
                                query.Append("  group_id=" + Grouplist[i] + "  ");
                                flag = 1;
                            }
                            else
                            {
                                query.Append(" AND (group_id=" + Grouplist[i] + "  ");
                                post = 1;
                                flag = 1;
                            }

                        }


                    }
                    query.Append(" )");
                }

               if (ProductTypelist.Count>0)
                {
                    flag = 0;
                    post = 0;
                    for (int i = 0; i < ProductTypelist.Count; i++)
                    {
                        if (flag > 0)
                        {
                            query.Append(" or ");
                            post = 1;
                        }

                        if (i == ProductTypelist.Count - 1 && post == 1)
                        {
                            query.Append(" type_id=" + ProductTypelist[i]);
                            flag = 0;
                        }
                        else
                        {
                            if (post == 1)
                            {
                                query.Append("  type_id=" + ProductTypelist[i] + "  ");
                                flag = 1;
                            }
                            else
                            {
                                query.Append(" AND (type_id=" + ProductTypelist[i] + "  ");
                                post = 1;
                                flag = 1;
                            }

                        }


                    }
                    query.Append(" )");
                }
                if (!string.IsNullOrWhiteSpace(key))
                {
                    query.Append(" AND Name LIKE '%" + key+"%'");
                }

                    db.Open();
            
             return    db.ExecuteQuery(CommandType.Text, query.ToString());
            }
            finally
            {
                db.Close();
            }
        }
        public static DataTable GetCustomers()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"SELECT   [Customer_Id] ,[Name],[Credit_Amount] ,[Credit_Period] FROM [dbo].[TBL_CUSTOMER_MST] where [Status]=1";
                db.Open();
                return db.ExecuteQuery(CommandType.Text, query);
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// function returns a data table for finding maximum credit amount and days from database
        /// </summary>
        public static DataTable GetCreditMax()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"SELECT   MAX(Credit_Period) [credit days],max(Credit_Amount) [credit_amount]
                                                                from [dbo].[TBL_CUSTOMER_MST] where [Status]=1";
                db.Open();
                return db.ExecuteQuery(CommandType.Text, query);
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Function for filter the customers basis of credit amount and days
        /// </summary>
        /// <param creditamountmin="minAmt"></param>
        /// <param creditamountmax="maxAmt"></param>
        /// <param creditdaysmin="minPeriod"></param>
        /// <param creditdaysmax="maxPeriod"></param>
        /// <returns></returns>
        public static DataTable GetFilteredCustomers(int minAmt,int maxAmt,int minPeriod,int maxPeriod,string keyword)
        {
            string query = "";
            DBManager db = new DBManager();
            try
            {
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                     query = @"SELECT [Customer_Id] ,[Name],[Credit_Amount] ,[Credit_Period]
                                     FROM [dbo].[TBL_CUSTOMER_MST] where [Status]=1 AND 
                                     ( (Credit_Amount Between @minAmt AND @maxAmt) AND
                                    ([Credit_Period] Between @minPeriod AND @maxPeriod) AND Name like '%'+@keyword+'%')";
                }
                else
                {
                    query = @"SELECT [Customer_Id] ,[Name],[Credit_Amount] ,[Credit_Period]
                                     FROM [dbo].[TBL_CUSTOMER_MST] where [Status]=1 AND 
                                     ( (Credit_Amount Between @minAmt AND @maxAmt) AND
                                    ([Credit_Period] Between @minPeriod AND @maxPeriod) )";
                }
             
                db.Open();
                db.CreateParameters(5);
                db.AddParameters(0,"@minAmt", minAmt);
                db.AddParameters(1, "@maxAmt", maxAmt);
                db.AddParameters(2, "@minPeriod", minPeriod);
                db.AddParameters(3, "@maxPeriod", maxPeriod);
                db.AddParameters(4, "@keyword", keyword);
           
                return db.ExecuteQuery(CommandType.Text, query);
            }
            finally
            {
                db.Close();
            }

        }
    

    }
}


