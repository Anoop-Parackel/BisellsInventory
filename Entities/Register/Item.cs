using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
    public class Item : Product
    {
        #region properties
        public int DetailsID { get; set; }
        public int QuoteDetailId { get; set; }
        public int ID { get; set; }
        public int PedId { get; set; }      
        public int TodId { get; set; }
        public int RoId { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public decimal TaxAmount { get; set; }
        public int RequestDetailId { get; set; }
        public int ReturnFrom { get; set; }
        public int SedId { get; set; }
        public int SrdId { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Gross { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Stock { get; set; }
        public decimal SalesDamageStock { get; set; }
        public decimal PurchaseDamageStock { get; set; }
        public decimal DamageStock { get; set; }
        public int? SchemeId { get; set; }
        public int LastUsedSchemeId { get; set; }
        public decimal ModifiedQuantity { get; set; }
        /// <summary>
        /// This Property is used to identify the previous table.
        /// </summary>
        public string ConvertedFrom { get; set; }
        public bool AffectedinStock { get; set; }
        /// <summary>
        /// For Purchase entry Details Table
        /// </summary>
        public int IsGRN { get; set; }
        public enum DetailsType { ItemMaster, PurchaseMaster, SalesMaster }
        #endregion properties



        #region Lookup Functions
        /// <summary>
        /// Search a particular product from a list of products
        /// </summary>
        /// <param name="Keyword">the letters in the product name or item code of that particular item</param>
        /// <returns>product</returns>
        public static List<Item> GetDetails(string Keyword, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();

                DataTable dt = db.ExecuteDataSet(CommandType.Text, "USP_SEARCH_PRODUCTS '" + Keyword + "'," + CompanyId + "").Tables[0];
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow pro in dt.Rows)
                    {
                        Item item = new Item();
                        item.InstanceId = pro["instance_id"] != DBNull.Value ? Convert.ToInt32(pro["instance_id"]) : 0;
                        item.ItemID = pro["Item_Id"] != DBNull.Value ? Convert.ToInt32(pro["Item_Id"]) : 0;
                        item.ItemCode = Convert.ToString(pro["Item_code"]);
                        item.Name = Convert.ToString(pro["Name"]);
                        item.MRP = pro["Mrp"] != DBNull.Value ? Convert.ToDecimal(pro["Mrp"]) : 0;
                        item.SellingPrice = pro["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(pro["Selling_Price"]) : 0;
                        item.CostPrice = pro["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(pro["cost_price"]) : 0;
                        item.TaxPercentage = pro["TaxPer"] != DBNull.Value ? Convert.ToDecimal(pro["TaxPer"]) : 0;
                        item.Description= Convert.ToString(pro["Description"]);
                        result.Add(item);
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

                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a particular list of products from entire product list
        /// </summary>
        /// <param name="keyword">the condition specified for retrieving details of the products.</param>
        /// <returns>list of items</returns>

        public static List<Item> GetDetailsFromPurchaseWithStock(string keyword, int LocationId)
        {
            DBManager db = new DBManager();
            try 
            {
                db.Open();

                DataTable dt = db.ExecuteDataSet(CommandType.Text, "USP_SEARCH_PRODUCTS_FROMSTOCKONLY '" + keyword + "'," + LocationId + "").Tables[0];
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow pro in dt.Rows)
                    {
                        Item item = new Item();
                        item.ItemID = pro["Item_Id"] != DBNull.Value ? Convert.ToInt32(pro["Item_Id"]) : 0;
                        item.InstanceId = pro["instance_id"] != DBNull.Value ? Convert.ToInt32(pro["instance_id"]) : 0;
                        item.Name = Convert.ToString(pro["name"]);
                        item.ItemCode = Convert.ToString(pro["Item_code"]);
                        item.MRP = pro["Mrp"] != DBNull.Value ? Convert.ToDecimal(pro["Mrp"]) : 0;
                        item.CostPrice = pro["Cost_price"] != DBNull.Value ? Convert.ToDecimal(pro["Cost_price"]) : 0;
                        item.SellingPrice = pro["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(pro["Selling_Price"]) : 0;
                        item.TaxPercentage = pro["Percentage"] != DBNull.Value ? Convert.ToDecimal(pro["Percentage"]) : 0;                       
                        item.Stock = pro["Stock"] != DBNull.Value ? Convert.ToDecimal(pro["Stock"]) : 0;
                        result.Add(item);
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
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// retrieve particular list of product details under a specified supplier id
        /// </summary>
        /// <param name="keyword">the first letter or item code of the particular item</param>
        /// <param name="SupplierId">supplier id of the particular product</param>
        /// <returns></returns>
        public static List<Item> GetDetailsFromPurchaseSupplierWise(string keyword, int SupplierId, int LocationId, int ReturnType)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();

                DataTable dt = db.ExecuteDataSet(CommandType.Text, "USP_SEARCH_PRODUCTS_FROMPURCHASE_SUPPLIERWISE '" + keyword + "'," + SupplierId + "," + LocationId + "," + ReturnType).Tables[0];
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow pro in dt.Rows)
                    {
                        Item item = new Item();

                        //item.PedId = pro["ped_Id"] != DBNull.Value ? Convert.ToInt32(pro["ped_Id"]) : 0;
                        item.ItemID = pro["Item_Id"] != DBNull.Value ? Convert.ToInt32(pro["Item_Id"]) : 0;
                        item.Name = Convert.ToString(pro["Item"]);
                        item.ItemCode = Convert.ToString(pro["Item_code"]);
                        item.MRP = pro["Mrp"] != DBNull.Value ? Convert.ToDecimal(pro["Mrp"]) : 0;
                        item.SellingPrice = pro["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(pro["Selling_Price"]) : 0;
                        item.TaxPercentage = pro["TaxPer"] != DBNull.Value ? Convert.ToDecimal(pro["TaxPer"]) : 0;
                        item.TaxAmount = pro["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(pro["Tax_Amount"]) : 0;
                        item.CostPrice = pro["cost_price"] != DBNull.Value ? Convert.ToDecimal(pro["cost_price"]) : 0;

                        result.Add(item);
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
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        /// <summary>
        /// return product details under a specified sales bill number
        /// </summary>
        /// <param name="keyword">the condition or item code of particular item</param>
        /// <param name="SalesEntryId">id of sales entry register of a particular product</param>
        /// <returns></returns>
        public static List<Item> GetDetailsFromSalesCustomerWise(string keyword, int CustomerId, int companyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();

                DataTable dt = db.ExecuteDataSet(CommandType.Text, "USP_SEARCH_PRODUCTS_FROMSALES '" + keyword + "'," + CustomerId + "," + companyId + " ").Tables[0];
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow pro in dt.Rows)
                    {
                        Item item = new Item();
                        item.ItemID = Convert.ToInt32(pro["item_id"]);
                        item.InstanceId = Convert.ToInt32(pro["Instance_Id"]);
                        item.Name = Convert.ToString(pro["Name"]);
                        item.ItemCode = Convert.ToString(pro["Item_code"]);
                        item.MRP = pro["Mrp"] != DBNull.Value ? Convert.ToDecimal(pro["Mrp"]) : 0;
                        item.SellingPrice = pro["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(pro["Selling_Price"]) : 0;
                        item.TaxPercentage = pro["Percentage"] != DBNull.Value ? Convert.ToDecimal(pro["Percentage"]) : 0;
                        result.Add(item);
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
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        public static List<Item> GetDetailsFromPurchaseWithScheme(string keyword, int CustomerId, int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();

                DataTable dt = db.ExecuteDataSet(CommandType.Text, "[USP_SEARCH_PRODUCTS_FROMSCHEME] '" + keyword + "'," + CustomerId + "," + LocationId + "").Tables[0];
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow pro in dt.Rows)
                    {
                        Item item = new Item();
                        item.InstanceId = pro["instance_id"] != DBNull.Value ? Convert.ToInt32(pro["instance_id"]) : 0;
                        item.ItemID = pro["Item_Id"] != DBNull.Value ? Convert.ToInt32(pro["Item_Id"]) : 0;
                        item.Name = Convert.ToString(pro["Item"]);
                        item.ItemCode = Convert.ToString(pro["Item_code"]);
                        item.MRP = pro["Mrp"] != DBNull.Value ? Convert.ToDecimal(pro["Mrp"]) : 0;
                        item.SellingPrice = pro["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(pro["Selling_Price"]) : 0;
                        item.TaxPercentage = pro["Percentage"] != DBNull.Value ? Convert.ToDecimal(pro["Percentage"]) : 0;
                        item.SchemeId = pro["scheme_id"] != DBNull.Value ? Convert.ToInt32(pro["scheme_id"]) : 0;
                        item.LastUsedSchemeId = pro["last_used"] != DBNull.Value ? Convert.ToInt32(pro["last_used"]) : 0;
                        item.Stock = pro["stock"] != DBNull.Value ? Convert.ToDecimal(pro["stock"]) : 0;
                        item.IsService= pro["IsService"] != DBNull.Value ? Convert.ToBoolean(pro["IsService"]) : false;
                        item.TrackInventory= pro["Track_Inventory"] != DBNull.Value ? Convert.ToBoolean(pro["Track_Inventory"]) : false;
                        item.Description= Convert.ToString(pro["Description"]);
                        result.Add(item);
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
                Application.Helper.LogException(ex, "Item | GetDetailsFromPurchaseWithScheme(string keyword, int CustomerId, int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }

        }
        #endregion

        /// <summary>
        /// Retrieve details of items 
        /// </summary>
        /// <param name="type">specified condition for retrieving data</param>
        /// <returns></returns>
        //[Obsolete]
        //public static List<Item> GetDetails(DetailsType type)
        //{
        //    try
        //    {
        //        if (type == DetailsType.ItemMaster)
        //        {
        //            DBManager db = new DBManager();
        //            db.Open();

        //            DataTable dt = db.ExecuteDataSet(System.Data.CommandType.Text, @"select instance_id,Item_Id,mrp,Rate,Tax_Id,Tax_Amount,Tax_percent,Gross_Amount,Net_Amount,Narration,Entry_Date,isnull(Stock,0)[Stock]
        //                                                                          from (
        //                                                                          select row_number() over (partition by itm.Item_Id order by per.Pe_Id desc) Slno,isnull(inst.instance_id,0) instance_id, itm.Item_Id, isnull(ped.[mrp],0) [mrp],isnull(ped.Rate,0)
        //                                                                          Rate,itm.Tax_Id,isnull(ped.Tax_Amount,0) Tax_Amount,isnull(tx.Percentage,0) Tax_percent,isnull(Gross_Amount,0) Gross_Amount
        //                                                                          ,isnull(ped.Net_Amount,0) Net_Amount,per.Narration,vs.Stock,per.Entry_Date from [TBL_ITEM_MST] itm with(nolock)
        //                                                                          left join [TBL_PURCHASE_ENTRY_DETAILS] ped  with(nolock) on itm.Item_Id=ped.Item_Id 
        //                                                                          left join [TBL_PURCHASE_ENTRY_REGISTER] per  with(nolock) on per.Pe_Id=ped.Pe_Id
        //                                                                          left join VW_Stock vs with(nolock) on vs.Ped_Id=ped.Ped_Id
        //                                                                          left join [TBL_TAX_MST] tx  with(nolock) on tx.Tax_Id=itm.Tax_id
								//												  left join tbl_item_instances inst on inst.item_id=itm.item_id
        //                                                                          group by  itm.Item_Id, isnull(ped.[mrp],0) ,isnull(ped.Rate,0),inst.instance_id
        //                                                                          ,itm.Tax_Id,isnull(ped.Tax_Amount,0) ,isnull(tx.Percentage,0) ,isnull(Gross_Amount,0) 
        //                                                                          ,isnull(ped.Net_Amount,0) ,per.Narration,per.Entry_Date,per.Pe_Id,vs.Stock
        //                                                                          )t1 where t1.Slno = 1").Tables[0];
        //            List<Item> result = new List<Item>();
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                Item prod = new Item();
        //                prod.InstanceId = item["instance_id"] != DBNull.Value ? Convert.ToInt32(item["instance_id"]) : 0;
        //                prod.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
        //                prod.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
        //                prod.TaxPercentage = item["Tax_Percent"] != DBNull.Value ? Convert.ToDecimal(item["Tax_percent"]) : 0;
        //                prod.CostPrice = item["Rate"] != DBNull.Value ? Convert.ToDecimal(item["Rate"]) : 0;
        //                prod.MRP = item["mrp"] != DBNull.Value ? Convert.ToDecimal(item["mrp"]) : 0;
        //                prod.Stock = item["Stock"] != DBNull.Value ? Convert.ToDecimal(item["Stock"]) : 0;
        //                result.Add(prod);
        //            }
        //            return result;
        //        }
        //        else
        //        if (type == DetailsType.PurchaseMaster)
        //        {
        //            DBManager db = new DBManager();
        //            db.Open();
        //            DataTable dt = db.ExecuteDataSet(System.Data.CommandType.Text, @"select pe.Ped_Id,pe.Pe_Id,pe.Pqd_Id,pe.Item_Id,pe.Qty,pe.Mrp,pe.Rate,pe.Selling_Price,pe.Tax_Id,pe.Tax_Amount,pe.Gross_Amount,
        //                                                                     pe.Net_Amount,pe.Deleted,pe.[Status],it.Name[Item],tx.Percentage[TaxPercentage],isnull(vs.Stock,0)[Stock],isnull(vs.salesDamage,0) [Damage_Stock],pr.Created_Date 
        //                                                                     from TBL_PURCHASE_ENTRY_DETAILS pe with(nolock)
        //                                                                     left join TBL_PURCHASE_ENTRY_REGISTER pr with(nolock) on pe.Pe_Id=pr.Pe_Id
        //                                                                     left join TBL_ITEM_MST it with(nolock) on it.Item_Id=pe.Item_Id
        //                                                                     left join TBL_TAX_MST tx with(nolock)  on tx.Tax_Id=pe.Tax_Id
        //                                                                     left join VW_Stock vs with(nolock) on vs.Ped_Id=pe.Ped_Id
        //                                                                     order by Created_Date desc").Tables[0];
        //            List<Item> result = new List<Item>();
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                Item prod = new Item();
        //                prod.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
        //                prod.PedId = item["Ped_Id"] != DBNull.Value ? Convert.ToInt32(item["Ped_Id"]) : 0;
        //                prod.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
        //                prod.TaxPercentage = item["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(item["Taxpercentage"]) : 0;
        //                prod.SellingPrice = item["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(item["Selling_Price"]) : 0;
        //                prod.MRP = item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
        //                prod.CostPrice = item["Rate"] != DBNull.Value ? Convert.ToDecimal(item["Rate"]) : 0;
        //                prod.Quantity = item["Qty"] != DBNull.Value ? Convert.ToDecimal(item["Qty"]) : 0;
        //                prod.SalesDamageStock = item["Damage_Stock"] != DBNull.Value ? Convert.ToDecimal(item["Damage_Stock"]) : 0;
        //                prod.Stock = item["Stock"] != DBNull.Value ? Convert.ToDecimal(item["Stock"]) : 0;
        //                result.Add(prod);
        //            }
        //            return result;
        //        }
        //        else if (type == DetailsType.SalesMaster)
        //        {
        //            DBManager db = new DBManager();
        //            db.Open();

        //            DataTable dt = db.ExecuteDataSet(System.Data.CommandType.Text, @"select sed.Sed_Id,sed.Se_Id,sed.Sqd_Id,sed.Ped_Id,sed.Scheme_Id,sed.Qty,sed.Rate,sed.Discount,sed.Tax_Id,sed.Tax_Amount,
        //                                                                           sed.Net_Amount,sed.[Status],sed.Deleted,sed.Mrp,itm.Item_Id,tax.Percentage,ser.Created_Date,
        //                                                                           isnull(v.Stock,0)[Stock] from TBL_SALES_ENTRY_DETAILS sed with(nolock)
        //                                                                           left join TBL_SALES_ENTRY_REGISTER  ser with(nolock) on sed.Se_Id=ser.Se_Id  
        //                                                                           left join TBL_PURCHASE_ENTRY_DETAILS ped with(nolock) on ped.Ped_Id=sed.Ped_Id
        //                                                                           left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=ped.Item_Id
        //                                                                           left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=sed.Tax_Id
        //                                                                           left join VW_Stock v with(nolock) on v.Ped_Id=ped.Ped_Id
        //                                                                           order by ser.Created_Date DESC").Tables[0];
        //            List<Item> result = new List<Item>();
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                Item prod = new Item();
        //                prod.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
        //                prod.PedId = item["Ped_Id"] != DBNull.Value ? Convert.ToInt32(item["Ped_Id"]) : 0;
        //                prod.SedId = item["Sed_Id"] != DBNull.Value ? Convert.ToInt32(item["Sed_Id"]) : 0;
        //                prod.TaxId = item["Tax_Id"] != DBNull.Value ? Convert.ToInt32(item["Tax_Id"]) : 0;
        //                prod.Quantity = item["Qty"] != DBNull.Value ? Convert.ToInt32(item["Qty"]) : 0;
        //                prod.TaxPercentage = item["Percentage"] != DBNull.Value ? Convert.ToDecimal(item["Percentage"]) : 0;
        //                prod.SellingPrice = item["Rate"] != DBNull.Value ? Convert.ToDecimal(item["Rate"]) : 0;
        //                prod.TaxAmount = item["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Tax_Amount"]) : 0;
        //                prod.NetAmount = item["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Net_Amount"]) : 0;
        //                prod.MRP = item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
        //                prod.Stock = item["Stock"] != DBNull.Value ? Convert.ToDecimal(item["Stock"]) : 0;
        //                result.Add(prod);
        //            }
        //            return result;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;

        //    }

        //}
        /// <summary>
        /// return price of specific instance of an item for purchase
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="InstanceId"></param>
        /// <returns></returns>
        public static Item GetPrices(int ItemId, int InstanceId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query;
                if (InstanceId == 0)
                {
                    query = @"select itm.Item_Id,itm.barcode,isnull(itm.mrp,0) mrp,isnull(itm.cost_price,0) cost_price
                        ,isnull(itm.selling_price, 0)  selling_price,tax.Percentage [tax_percentage],itm.tax_id  from TBL_ITEM_MST itm with(nolock)
                    left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=itm.Tax_id where itm.Item_Id =@item_id";
                }
                else
                {
                    query = @"select inst.instance_id,inst.item_id,isnull(inst.mrp,0) mrp,isnull(inst.selling_price,0) selling_price,
                               isnull(inst.cost_price,0) cost_price,inst.barcode,tax.Percentage [tax_percentage],itm.tax_id from tbl_item_instances inst with(nolock)
                               left join TBL_ITEM_MST itm with(nolock) on itm.Item_Id=inst.item_id
                               left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=itm.Tax_id
                               where inst.instance_id=@instance_id";
                }
                db.CreateParameters(2);
                db.AddParameters(0, "@item_id", ItemId);
                db.AddParameters(1, "@instance_id", InstanceId);
                DataTable dt = db.ExecuteDataSet(CommandType.Text, query).Tables[0];
                if (dt != null)
                {
                    Item item = new Item();
                    DataRow pro = dt.Rows[0];
                    item.ItemID = Convert.ToInt32(pro["Item_Id"]);
                    item.TaxPercentage = Convert.ToDecimal(pro["tax_percentage"]);
                    item.InstanceId = InstanceId != 0 ? Convert.ToInt32(pro["Instance_Id"]) : 0;
                    item.Barcode = Convert.ToString(pro["barcode"]);
                    item.MRP = Convert.ToDecimal(pro["mrp"]);
                    item.CostPrice = Convert.ToDecimal(pro["cost_price"]);
                    item.SellingPrice = Convert.ToDecimal(pro["selling_price"]);
                    item.TaxId = Convert.ToInt32(pro["tax_id"]);
                    return item;


                }

                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Item  | GetPrices(int ItemId, int InstanceId)");
                return null;
            }


        }
        /// <summary>
        /// return price of specific instant of item for sales
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="InstanceID"></param>
        /// <param name="CustomerId"></param>
        /// <param name="LocationId"></param>
        /// <param name="SchemeID"></param>
        /// <returns></returns>
        public static Item GetPricesWithScheme(int ItemId, int InstanceID, int CustomerId, int LocationId, int? SchemeID)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                db.CreateParameters(5);
                db.AddParameters(0, "@CustomerId", CustomerId);
                db.AddParameters(1, "@LocationId", LocationId);
                db.AddParameters(2, "@ItemId", ItemId);
                db.AddParameters(3, "@InstanceId", InstanceID);
                db.AddParameters(4, "@SchemeID", SchemeID);
                #region query
                string query = @";with cte as (
                                  	select stk.Item_Id [itemId],stk.instance_Id,isnull(sp.Selling_Price,0)Selling_Price,isnull(sp.Mrp,0)Mrp,stk.Stock,sp.Scheme_Id,tax.Tax_id,tax.Percentage [TaxPercentage],itmin.Track_Inventory from VW_SCHEME_PRICE sp join 
                                  VW_STOCK stk on stk.Item_Id=sp.Item_Id AND stk.instance_id=sp.instance_id
                                   join VW_ITEM_INSTANCE itmin on itmin.Item_Id=sp.Item_Id AND itmin.Instance_Id=sp.Instance_Id 
                                   join tbl_tax_mst tax with(nolock) on tax.Tax_Id=itmin.Tax_id
                                   where sp.Customer_Id=@CustomerId and stk.Location_Id=@LocationId and sp.Item_Id=@ItemId and sp.instance_id=@InstanceId 
                                   union
                                  select itmin.Item_Id [itemId],itmin.instance_Id,isnull(itmin.Selling_Price,0)Selling_Price,isnull(itmin.Mrp,0)Mrp,stk.Stock, 0 [Scheme_Id],tax.Tax_id,tax.Percentage [TaxPercentage],itmin.Track_Inventory from  
                                  VW_STOCK stk join VW_ITEM_INSTANCE itmin on itmin.Item_Id=stk.Item_Id AND itmin.Instance_Id=stk.Instance_Id 
                                   join tbl_tax_mst tax with(nolock) on tax.Tax_Id=itmin.Tax_id
                                   where  stk.Location_Id=@LocationId and itmin.Item_Id=@ItemId and itmin.instance_id=@InstanceId
                                  )
                                  select * from cte where Scheme_Id = @SchemeID order by Stock desc";
                #endregion
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);

                Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["ItemId"] != DBNull.Value ? Convert.ToInt32(prod["ItemId"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.TaxPercentage = prod["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(prod["Taxpercentage"]) : 0;
                item.SellingPrice = prod["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Selling_Price"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
                item.Stock = prod["Stock"] != DBNull.Value ? Convert.ToDecimal(prod["Stock"]) : 0;
                item.SchemeId = prod["scheme_id"] != DBNull.Value ? Convert.ToInt32(prod["scheme_id"]) : 0;
                item.TrackInventory = prod["Track_Inventory"] != DBNull.Value ? Convert.ToBoolean(prod["Track_Inventory"]) : false;
                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Item  |  GetPricesWithScheme(int ItemId, int InstanceID, int CustomerId, int LocationId, int? SchemeID)");
                return null;
            }
        }

        /// <summary>
        /// Function returns details of products from purchase;Used for get price in details
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="InstanceId"></param>
        /// <returns></returns>
        public static Item GetProductFromStock(int ItemID, int InstanceId,int locationId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select s.Item_Id,s.Instance_Id,tax.Tax_Id,i.Cost_Price,i.Selling_Price,i.Mrp,s.Stock,tax.Percentage,s.DamageStock  
                                                  from VW_ITEM_INSTANCE i with(nolock)
                                                 join VW_STOCK s on s.Instance_Id=i.Instance_Id and s.item_id=i.item_id
                                                 left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=i.tax_id
                                                 where s.Item_Id=@ItemID and s.Instance_Id=@InstanceId and s.location_id=@locationId";
                db.CreateParameters(3);
                db.AddParameters(0, "@ItemID", ItemID);
                db.AddParameters(1, "@InstanceId", InstanceId);
                db.AddParameters(2, "@locationId", locationId);

                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["Item_Id"] != DBNull.Value ? Convert.ToInt32(prod["Item_Id"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.CostPrice = prod["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Cost_Price"]) : 0;
                item.Stock = prod["stock"] != DBNull.Value ? Convert.ToDecimal(prod["stock"]) : 0;
                item.TaxPercentage = prod["percentage"] != DBNull.Value ? Convert.ToDecimal(prod["percentage"]) : 0;
                item.SellingPrice = prod["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Selling_Price"]) : 0;
                item.DamageStock = prod["Damagestock"] != DBNull.Value ? Convert.ToDecimal(prod["Damagestock"]) : 0;
                //item.SalesDamageStock = prod["SalesDamage"] != DBNull.Value ? Convert.ToDecimal(prod["SalesDamage"]) : 0;
                //item.PurchaseDamageStock = prod["PurchaseDamage"] != DBNull.Value ? Convert.ToDecimal(prod["PurchaseDamage"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Item  | GetProductFromStock(int ItemID, int InstanceId)");
                return null;
            }
        }

        /// <summary>
        /// Function returns details of products from purchase;Used for get price in details
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="InstanceId"></param>
        /// <returns></returns>
        public static Item GetProductFromPurchase(int ItemID, int InstanceId)
        {

            DBManager db = new DBManager();
            try
            {
                string query = @"select s.Item_Id,s.Instance_Id,tax.Tax_Id,i.Cost_Price,i.Selling_Price,i.Mrp,s.Stock,tax.Percentage,s.DamageStock  
                                                  from VW_ITEM_INSTANCE i with(nolock)
                                                 join VW_STOCK s on s.Instance_Id=i.Instance_Id and s.item_id=i.item_id
                                                 left join TBL_TAX_MST tax with(nolock) on tax.Tax_Id=i.tax_id
                                                 where s.Item_Id=@ItemID and s.Instance_Id=@InstanceId";
                db.CreateParameters(2);
                db.AddParameters(0, "@ItemID", ItemID);
                db.AddParameters(1, "@InstanceId", InstanceId);

                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                Item item = new Item();
                DataRow prod = dt.Rows[0];
                item.ItemID = prod["Item_Id"] != DBNull.Value ? Convert.ToInt32(prod["Item_Id"]) : 0;
                item.InstanceId = prod["instance_id"] != DBNull.Value ? Convert.ToInt32(prod["instance_id"]) : 0;
                item.TaxId = prod["Tax_Id"] != DBNull.Value ? Convert.ToInt32(prod["Tax_Id"]) : 0;
                item.CostPrice = prod["Cost_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Cost_Price"]) : 0;
                item.Stock = prod["stock"] != DBNull.Value ? Convert.ToDecimal(prod["stock"]) : 0;
                item.TaxPercentage = prod["percentage"] != DBNull.Value ? Convert.ToDecimal(prod["percentage"]) : 0;
                item.SellingPrice = prod["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(prod["Selling_Price"]) : 0;
                item.DamageStock = prod["Damagestock"] != DBNull.Value ? Convert.ToDecimal(prod["Damagestock"]) : 0;
                //item.SalesDamageStock = prod["SalesDamage"] != DBNull.Value ? Convert.ToDecimal(prod["SalesDamage"]) : 0;
                //item.PurchaseDamageStock = prod["PurchaseDamage"] != DBNull.Value ? Convert.ToDecimal(prod["PurchaseDamage"]) : 0;
                item.MRP = prod["Mrp"] != DBNull.Value ? Convert.ToDecimal(prod["Mrp"]) : 0;
                return item;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Item | GetProductFromPurchase(int ItemID, int InstanceId)");
                return null;
            }
        }
    }
}
