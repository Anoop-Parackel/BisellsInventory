using Core.DBManager;
using Entities.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        #region properties
        public int ItemID { get; set; }
        public int InstanceId { get; set; }
        public string Name { get; set; }
        public string ItemCode { get; set; }
        public int Status { get; set; }
        public int UnitID { get; set; }
        public string HSCode { get; set; }
        public string OEMCode { get; set; }
        public int TypeID { get; set; }
        public string Barcode { get; set; }
        public int CategoryID { get; set; }
        public int GroupID { get; set; }
        public int BrandID { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int TaxId { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public bool IsService { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal TaxPercentage { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
        public string Group { get; set; }
        public string Company { get; set; }
        public List<ItemInstance> Instances { get; set; }
        public bool TrackInventory { get; set; }
        #endregion properties
        /// <summary>
        ///Save details of each products 
        ///to save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>

        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Item, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Product | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            //else if (this.ItemCode.Length > 8)
            //{
            //    return new OutputMessage("itemcode length should not exceed 8 characters", false, Entities.Type.RequiredFields, "Product | Save", System.Net.HttpStatusCode.InternalServerError);
            //}
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage(" Name must not be empty", false, Entities.Type.RequiredFields, "Product | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_ITEM_MST] (Name,Item_Code,Unit_Id,Description,Remarks,HS_Code,OEM_Code,
                                       Type_Id,Category_Id,Group_ID,Brand_Id,Company_Id,Tax_id,Created_By,Created_Date,barcode,status,
                                       Mrp,Selling_Price,Cost_Price,IsService,Track_Inventory)values(@Name,@Item_Code,@Unit_Id,@Description,@Remarks,@HS_Code,
                                       @OEM_Code,@Type_Id,@Category_Id,@Group_ID,@Brand_Id,@Company_Id,@Tax_id,@Created_By,GETUTCDATE(),
                                       @barcode,@status,@Mrp,@Selling_Price,@Cost_Price,@IsService,@Track_Inventory);select @@identity";
                        db.CreateParameters(21);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Unit_Id", this.UnitID);
                        db.AddParameters(2, "@Description", this.Description);
                        db.AddParameters(3, "@Remarks", this.Remarks);
                        db.AddParameters(4, "@HS_Code", this.HSCode);
                        db.AddParameters(5, "@OEM_Code", this.OEMCode);
                        db.AddParameters(6, "@Type_Id", this.TypeID);
                        db.AddParameters(7, "@Category_Id", this.CategoryID);
                        db.AddParameters(8, "@Group_ID", this.GroupID);
                        db.AddParameters(9, "@Brand_Id", this.BrandID);
                        db.AddParameters(10, "@Company_Id", this.CompanyId);
                        db.AddParameters(11, "@Tax_id", this.TaxId);
                        db.AddParameters(12, "@Created_By", this.CreatedBy);
                        db.AddParameters(13, "@barcode", this.Barcode);
                        db.AddParameters(14, "@status", this.Status);
                        db.AddParameters(15, "@Mrp", this.MRP);
                        db.AddParameters(16, "@Selling_Price", this.SellingPrice);
                        db.AddParameters(17, "@Cost_Price", this.CostPrice);
                        db.AddParameters(18, "@IsService", this.IsService);
                        db.AddParameters(19, "@Item_Code", this.ItemCode);
                        db.AddParameters(20, "@Track_Inventory", this.TrackInventory);
                        int identity = Convert.ToInt32(db.ExecuteScalar(CommandType.Text, query));
                        if (identity>=1)
                        {
                            return new OutputMessage("Products saved successfully", true, Entities.Type.NoError, "Product | Save", System.Net.HttpStatusCode.OK,identity);

                        }
                        else
                        {
                            return new OutputMessage("Somthing went wrong. Products could not be saved", false, Entities.Type.Others, "Product | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Somthing went wrong. Products could not be saved", false, Entities.Type.Others, "Product | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        if (db.Connection.State == System.Data.ConnectionState.Open)
                        {
                            db.Close();
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Update details of each products 
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Item, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Product | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ItemID == 0)
            {
                return new OutputMessage("ID must not be empty", false, Entities.Type.Others, "Product | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {

                return new OutputMessage("Name must not be empty", false, Entities.Type.Others, "Product | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_ITEM_MST] set Name=@Name,Item_Code=@Item_Code,Unit_Id=@Unit_Id,Description=@Description,
                                       Remarks=@Remarks,HS_Code=@HS_Code,OEM_Code=@OEM_Code,Type_Id=@Type_Id,
                                       Category_Id=@Category_Id,Group_ID=@Group_ID,Brand_Id=@Brand_Id,Tax_id=@Tax_id,Modified_By=@Modified_By,
                                       Modified_Date=GETUTCDATE(),barcode=@barcode,status=@status, Mrp=@Mrp,Selling_Price=@Selling_Price,
                                       Cost_Price=@Cost_Price,IsService=@isservice,Track_Inventory=@Track_Inventory where Item_Id=@id";
                        db.CreateParameters(21);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Unit_Id", this.UnitID);
                        db.AddParameters(2, "@Description", this.Description);
                        db.AddParameters(3, "@Remarks", this.Remarks);
                        db.AddParameters(4, "@HS_Code", this.HSCode);
                        db.AddParameters(5, "@OEM_Code", this.OEMCode);
                        db.AddParameters(6, "@Type_Id", this.TypeID);
                        db.AddParameters(7, "@Category_Id", this.CategoryID);
                        db.AddParameters(8, "@Group_ID", this.GroupID);
                        db.AddParameters(9, "@Brand_Id", this.BrandID);
                        db.AddParameters(10, "@Tax_id", this.TaxId);
                        db.AddParameters(11, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(12, "@id", this.ItemID);
                        db.AddParameters(13, "@barcode", this.Barcode);
                        db.AddParameters(14, "@status", this.Status);
                        db.AddParameters(15, "@Mrp", this.MRP);
                        db.AddParameters(16, "@Selling_Price", this.SellingPrice);
                        db.AddParameters(17, "@Cost_Price", this.CostPrice);
                        db.AddParameters(18, "@isservice", this.IsService);
                        db.AddParameters(19, "@Item_Code", this.ItemCode);
                        db.AddParameters(20, "@Track_Inventory", this.TrackInventory);

                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Products updated successfully", true, Entities.Type.NoError, "Product | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Somthing went wrong. Products could not be updated", false, Entities.Type.Others, "Product | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {

                        return new OutputMessage("Somthing went wrong. Products could not be updated", false, Entities.Type.Others, "Product | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual products from product master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Item, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "Product | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ItemID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_ITEM_MST where Item_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ItemID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Product deleted Successfully", true, Entities.Type.NoError, "Product | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Products could not be deleted", false, Entities.Type.Others, "Product | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID Cannot be zero for deletion", false, Entities.Type.Others, "Product | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this Details because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "product| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Entities.Type.RequiredFields, "product | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Products could not be deleted", false, Entities.Type.Others, "product | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }


                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieve a list of product from product master under a particular company id
        /// </summary>
        /// <param name="CompanyId">Company id of group list</param>
        /// <returns>list of products</returns>
        public static List<Product> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select I.Item_Id,i.Name,i.Item_Code,isnull(i.Unit_Id,0)[Unit_Id],i.[Description],i.Remarks,i.HS_Code,
                               i.OEM_Code,isnull(i.[Type_Id],0)[Type_Id],isnull(i.Category_Id,0)[Category_Id], 
                               isnull(i.Group_ID,0)[Group_ID],isnull(i.Brand_Id,0)[Brand_Id],isnull(i.Tax_id,0)[Tax_id],isnull(i.[Status],0)[Status], 
                               isnull(i.Company_Id,0)[Company_Id],i.Created_Date,isnull(i.Created_By,0)[Created_By],
                               i.Barcode,isnull(t.Percentage,0)[TaxPercentage],c.Name[Category],B.Name[Brand],
                               U.Name[Unit],Ty.name[Type],g.name[Group],L.Name[Company] 
                               from TBL_ITEM_MST i
                               left join TBL_CATEGORY_MST C ON C.Category_Id = i.Category_Id
                               left join TBL_TAX_MST T ON T.Tax_Id = I.Tax_id
                               left join TBL_BRAND_MST B ON B.Brand_ID = i.Brand_Id
                               left join TBL_UNIT_MST U ON U.unit_id = i.unit_Id
                               left join TBL_TYPE_MST Ty ON Ty.[Type_Id] = i.[Type_Id]
                               left join TBL_GROUP_MST G ON G.Group_Id = i.Group_Id
                               left join TBL_COMPANY_MST L ON L.Company_Id = i.Company_Id  
                               where l.Company_Id=@Company_Id and isnull(i.IsService,0)=0 order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text,query);
                List<Product> result = new List<Product>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Product product = new Product();
                        product.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
                        product.Name = Convert.ToString(item["Name"]);
                        product.ItemCode = Convert.ToString(item["Item_Code"]);
                        product.UnitID = item["Unit_ID"] != DBNull.Value ? Convert.ToInt32(item["Unit_ID"]) : 0;
                        product.Description = Convert.ToString(item["Description"]);
                        product.Remarks = Convert.ToString(item["remarks"]);
                        product.HSCode = Convert.ToString(item["HS_Code"]);
                        product.OEMCode = Convert.ToString(item["OEM_Code"]);
                        product.TypeID = item["Type_ID"] != DBNull.Value ? Convert.ToInt32(item["Type_ID"]) : 0;
                        product.CategoryID = item["Category_ID"] != DBNull.Value ? Convert.ToInt32(item["Category_ID"]) : 0;
                        product.GroupID = item["Group_ID"] != DBNull.Value ? Convert.ToInt32(item["Group_ID"]) : 0;
                        product.BrandID = item["Brand_ID"] != DBNull.Value ? Convert.ToInt32(item["Brand_ID"]) : 0;
                        product.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
                        product.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        product.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        product.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["TaxPercentage"]) : 0;
                        product.Category = Convert.ToString(item["Category"]);
                        product.Barcode = Convert.ToString(item["barcode"]);
                        product.Brand = Convert.ToString(item["Brand"]);
                        product.Unit = Convert.ToString(item["Unit"]);
                        product.Type = Convert.ToString(item["Type"]);
                        product.Group = Convert.ToString(item["Group"]);
                        result.Add(product);
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
                Application.Helper.LogException(ex, "product | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


        public static List<Product> GetDetailsForService(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select I.Item_Id,i.Name,i.[Description],isnull(i.Tax_id,0)[Tax_id],isnull(i.[Status],0)[Status], 
                               isnull(i.Company_Id,0)[Company_Id],i.Created_Date,isnull(i.Created_By,0)[Created_By]
                               ,isnull(t.Percentage,0)[TaxPercentage],L.Name[Company] ,I.Selling_Price,I.Mrp
                               from TBL_ITEM_MST i
                               left join TBL_TAX_MST T ON T.Tax_Id = I.Tax_id
                               left join TBL_COMPANY_MST L ON L.Company_Id = i.Company_Id  
                               where l.Company_Id=@Company_Id and isnull(i.IsService,0)=1 order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Product> result = new List<Product>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Product product = new Product();
                        product.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
                        product.Name = Convert.ToString(item["Name"]);
                        product.Description = Convert.ToString(item["Description"]);
                        product.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
                        product.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        product.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        product.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["TaxPercentage"]) : 0;
                         product.SellingPrice= item["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(item["Selling_Price"]) : 0;
                         product.MRP= item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
                        result.Add(product);
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
                Application.Helper.LogException(ex, "product | GetDetailsForService(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


        public static List<Product> GetDetailsForService(int ItemID, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 I.Item_Id,i.Name,i.[Description],isnull(i.Tax_id,0)[Tax_id],i.Item_Code,isnull(i.[Status],0)[Status], 
                               isnull(i.Company_Id,0)[Company_Id],i.Created_Date,isnull(i.Created_By,0)[Created_By]
                               ,isnull(t.Percentage,0)[TaxPercentage],L.Name[Company],i.Selling_Price,i.Mrp,i.Track_Inventory
                               from TBL_ITEM_MST i
                               left join TBL_TAX_MST T ON T.Tax_Id = I.Tax_id
                               left join TBL_COMPANY_MST L ON L.Company_Id = i.Company_Id  
                               where l.Company_Id=@Company_Id and Item_Id=@item_id and i.IsService=1 order by Created_Date desc";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@item_id", ItemID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Product> result = new List<Product>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Product product = new Product();
                        product.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
                        product.Name = Convert.ToString(item["Name"]);
                        product.Description = Convert.ToString(item["Description"]);
                        product.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
                        product.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                        product.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                        product.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["TaxPercentage"]) : 0;
                        product.SellingPrice = item["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(item["Selling_Price"]) : 0;
                        product.MRP = item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
                        product.ItemCode = Convert.ToString(item["Item_Code"]) ;
                        product.TrackInventory = Convert.ToBoolean(item["Track_Inventory"]);
                        result.Add(product);
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
                Application.Helper.LogException(ex, "product |  GetDetailsForService(int ItemID, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single product from product master
        /// </summary>
        /// <param name="ItemID">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>details of a single product</returns>
        public static Product GetDetails(int ItemID, int CompanyId)
        {

            DBManager db = new DBManager();
            try
            {

                db.Open();
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@item_Id", ItemID);
                DataSet ds = db.ExecuteDataSet(CommandType.Text, @"select top 1 I.Item_Id,i.Name,i.Item_Code,isnull(i.Unit_Id,0)[Unit_Id],
                              i.[Description],i.Remarks,i.HS_Code,i.OEM_Code,
                              isnull(i.[Type_Id],0)[Type_Id],isnull(i.Category_Id,0)[Category_Id],isnull(i.Group_ID,0)[Group_ID],isnull(i.Brand_Id,0)[Brand_Id], 
                              isnull(i.Tax_id,0)[Tax_id],isnull(i.[Status],0)[Status],isnull(i.Company_Id,0)[Company_Id],isnull(i.Mrp,0)[Mrp],
                              isnull(i.Selling_price,0)[Selling_price],isnull(i.Cost_Price,0)[Cost_Price],i.Created_Date,
                              isnull( i.Created_By,0)[Created_By],i.Barcode,isnull(t.Percentage,0)[TaxPercentage],c.Name[Category],i.Track_Inventory,
                              B.Name[Brand],U.Name[Unit],Ty.name[Type],g.name[Group],L.Name[Company],i.IsService 
                              from TBL_ITEM_MST i
                              left join TBL_CATEGORY_MST C ON C.Category_Id = i.Category_Id
                              left join TBL_TAX_MST T ON T.Tax_Id = I.Tax_id
                              left join TBL_BRAND_MST B ON B.Brand_ID = i.Brand_Id
                              left join TBL_UNIT_MST U ON U.unit_id = i.unit_Id
                              left join TBL_TYPE_MST Ty ON Ty.[Type_Id] = i.[Type_Id]
                              left join TBL_GROUP_MST G ON G.Group_Id = i.Group_Id
                              left join TBL_COMPANY_MST L ON L.Company_Id = i.Company_Id  
                              where l.Company_Id=@Company_Id and Item_Id=@item_Id and i.IsService=0;
                              select Instance_Id,isnull(Mrp,0)[Mrp],isnull(Selling_Price,0)[Selling_Price],isnull(Cost_Price,0)[Cost_Price],Barcode from TBL_ITEM_INSTANCES where Item_Id =@item_Id;");

                DataRow item = ds.Tables[0].Rows[0];
                Product product = new Product();
                product.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
                product.Name = Convert.ToString(item["Name"]);
                product.ItemCode = Convert.ToString(item["Item_Code"]);
                product.UnitID = item["Unit_ID"] != DBNull.Value ? Convert.ToInt32(item["Unit_ID"]) : 0;
                product.Description = Convert.ToString(item["Description"]);
                product.Remarks = Convert.ToString(item["remarks"]);
                product.HSCode = Convert.ToString(item["HS_Code"]);
                product.OEMCode = Convert.ToString(item["OEM_Code"]);
                product.TypeID = item["Type_ID"] != DBNull.Value ? Convert.ToInt32(item["Type_ID"]) : 0;
                product.CategoryID = item["Category_ID"] != DBNull.Value ? Convert.ToInt32(item["Category_ID"]) : 0;
                product.GroupID = item["Group_ID"] != DBNull.Value ? Convert.ToInt32(item["Group_ID"]) : 0;
                product.BrandID = item["Brand_ID"] != DBNull.Value ? Convert.ToInt32(item["Brand_ID"]) : 0;
                product.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
                product.CreatedBy = item["Created_By"] != DBNull.Value ? Convert.ToInt32(item["Created_By"]) : 0;
                product.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                product.Barcode = Convert.ToString(item["Barcode"]);
                product.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["TaxPercentage"]) : 0;
                product.Category = Convert.ToString(item["Category"]);
                product.Brand = Convert.ToString(item["Brand"]);
                product.Unit = Convert.ToString(item["Unit"]);
                product.Type = Convert.ToString(item["Type"]);
                product.Group = Convert.ToString(item["Group"]);
                product.MRP = Convert.ToDecimal(item["Mrp"]);
                product.SellingPrice = Convert.ToDecimal(item["Selling_Price"]);
                product.CostPrice = Convert.ToDecimal(item["Cost_price"]);
                product.TrackInventory = item["Track_Inventory"] !=DBNull.Value? Convert.ToBoolean(item["Track_Inventory"]):false;
                if (ds.Tables[1] != null)
                {
                    product.Instances = new List<ItemInstance>();
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        product.Instances.Add(new ItemInstance() { ID = Convert.ToInt32(row["Instance_Id"]), Mrp = Convert.ToDecimal(row["Mrp"]), SellingPrice = Convert.ToDecimal(row["Selling_Price"]), CostPrice = Convert.ToDecimal(row["cost_price"]),Barcode= Convert.ToString(row["barcode"]) });
                    }
                }

                return product;
            }

            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "product | GetDetails(int ItemID, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        public static dynamic getAssociateData(int CompanyID)
        {
            try
            {
                object result = new
                {
                    Types = Entities.ProductType.GetProductType(CompanyID),
                    Taxes = Entities.Tax.GetTax(CompanyID),
                    Unit = Entities.UOM.GetUnits(CompanyID),
                    Groups = Entities.Group.GetGroup(CompanyID),
                    Categories = Entities.Category.GetCategory(CompanyID),
                    Brands = Entities.Brand.GetBrand(CompanyID)
                };
                return result;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "product |getAssociateData(int CompanyID)");
                return null;
            }
        }
    }
}
