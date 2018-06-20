using System;
using System.Collections.Generic;
using System.Linq;
using Core.DBManager;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Entities.Register
{
   public class StockAdjustment:Register
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstanceId { get; set; }
        public decimal MRP { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Stock { get; set; }
        public decimal SystemStock { get; set; }
        public decimal AdjustedQuantity { get { return CurrentStock - SystemStock; } }
        public decimal CurrentStock { get; set; }
        #endregion Properties

        public static OutputMessage Save(List<StockAdjustment> Stock)
        {
            DBManager db = new DBManager();
            try
            {
               db.Open();
                foreach (StockAdjustment st in Stock)
                {

                    string query = @"insert into tbl_stock_adjustment (Item_Id,Instance_Id,Mrp,Selling_Price,System_Stock,
                                   Adjust_Quantity,Current_Stock,Location_Id,Created_By,Created_Date)
                                   values(@Item_Id,@Instance_Id,@Mrp,@Selling_Price,@System_Stock,@Adjust_Quantity,
                                   @Current_Stock,@Location_Id,@Created_By,GETUTCDATE())";
                    db.CreateParameters(9);
                    db.AddParameters(0, "@Item_Id", st.ID);
                    db.AddParameters(1, "@Instance_Id", st.InstanceId);
                    db.AddParameters(2, "@Mrp", st.MRP);
                    db.AddParameters(3, "@Selling_Price", st.SellingPrice);
                    db.AddParameters(4, "@System_Stock", st.SystemStock);
                    db.AddParameters(5, "@Adjust_Quantity", st.AdjustedQuantity);
                    db.AddParameters(6, "@Current_Stock", st.CurrentStock);
                    db.AddParameters(7, "@Location_Id", st.LocationId);
                    db.AddParameters(8, "@Created_By", st.CreatedBy);
                   db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                }
           return new OutputMessage("Stock saved successfully", true, Type.NoError, "StockAdjustment | Save", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new OutputMessage("Something went wrong. Stock could not be saved", false, Type.Others, "StockAdjustment | Save", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }

       public static List<Item> StockAdjustments(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select  isnull(i.Item_Id,0)[Item_Id],i.Name[Item],isnull(i.Item_Code,0)[Item_Code],
                               isnull(i.Instance_Id,0)[Instance_Id], isnull(i.Mrp,0)[Mrp],isnull(i.Selling_Price,0)[Selling_Price],
                               isnull(s.TotalStock,0)[TotalStock] from VW_ITEM_INSTANCE i inner join 
                               VW_STOCK s on i.Instance_Id=s.Instance_Id and i.Item_Id=s.Item_Id where s.Location_Id=@Location_Id ";
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Item> result = new List<Item>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Item stock = new Item();
                        stock.ItemID = item["Item_Id"] != DBNull.Value ? Convert.ToInt32(item["Item_Id"]) : 0;
                        stock.Name = Convert.ToString(item["Item"]);
                        stock.ItemCode = Convert.ToString(item["Item_Code"]);
                        stock.InstanceId = item["Instance_Id"] != DBNull.Value ? Convert.ToInt32(item["Instance_Id"]) : 0;
                        stock.MRP = item["Mrp"] != DBNull.Value ? Convert.ToDecimal(item["Mrp"]) : 0;
                        stock.SellingPrice = item["Selling_Price"] != DBNull.Value ? Convert.ToDecimal(item["Selling_Price"]) : 0;
                        stock.Stock = item["TotalStock"] != DBNull.Value ? Convert.ToDecimal(item["TotalStock"]) : 0;
                        result.Add(stock);
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
                Application.Helper.LogException(ex, "StockAdjustment | StockAdjustments(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
