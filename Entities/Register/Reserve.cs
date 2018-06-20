using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Register
{
    public class Reserve
    {
        #region Properties
        public int InstanceId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion Properties

        /// <summary>
        /// Save or Update Products
        /// </summary>
        /// <param name="InstanceId">Instance Id of that particular product</param>
        /// <param name="ItemId">ItemId of that particular item </param>
        /// <param name="LocationId">Location</param>
        /// <param name="Qty">Number of items to reserve</param>
        /// <returns>Return success message when product insert or update successfully otherwise return an error message</returns>
        public static OutputMessage ReserveQty(int InstanceId, int ItemId, int LocationId, decimal Qty, int UserId)
        {
            DBManager db = new DBManager();

            try
            {
                db.Open();
                string query = @"if exists(select * from TBL_RESERVE where item_id = @item_id and instance_id = @instance)
                               begin
                               select isnull(stock, 0)[Stock],isnull((select qty from TBL_RESERVE where Item_Id = @item_id and Instance_Id = @instance),0)[current_reserve]
                               from VW_STOCK where Item_Id = @item_id and Instance_Id = @instance
                               end
                               else
                               begin
                               select isnull(stock,0)[stock],0 [current_reserve]
                               from VW_STOCK where Item_Id = @item_id and Instance_Id = @instance
                               end";
                db.CreateParameters(2);
                db.AddParameters(0, "@item_id", ItemId);
                db.AddParameters(1, "@instance", InstanceId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return new OutputMessage("No Stock Found", false, Type.Others, "Reserve | ReserveQty", System.Net.HttpStatusCode.InternalServerError);
                }
                decimal UpdatedQty = Qty + Convert.ToDecimal(dt.Rows[0]["current_reserve"]);
                if (Convert.ToDecimal(dt.Rows[0]["stock"]) < UpdatedQty)
                {
                    return new OutputMessage("Quantity Exceeds", false, Type.Others, "Reserve | ReserveQty", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    string query1 = @"if exists(select * from TBL_RESERVE where item_id = @Item_Id and instance_id=@Instance_Id)
                             begin 
                             update TBL_RESERVE set Qty=@UpdateQty where Item_Id=@Item_Id and Instance_Id=@Instance_Id
                             end
                             else
                             begin
                             insert into TBL_RESERVE (Item_Id,Instance_Id,Qty,Created_By,Created_Date,Location_Id)values(@Item_Id,@Instance_Id,@Qty,@Created_By,GETUTCDATE(),@Location_Id)
                             end";
                    db.CreateParameters(7);
                    db.AddParameters(0, "@Item_Id", ItemId);
                    db.AddParameters(1, "@Instance_Id", InstanceId);
                    db.AddParameters(2, "@Qty", Qty);
                    db.AddParameters(3, "@UpdateQty", UpdatedQty);
                    db.AddParameters(4, "@Created_By", UserId);
                    db.AddParameters(5, "@ModifiedBy", UserId);
                    db.AddParameters(6, "@Location_Id", LocationId);
                    db.ExecuteNonQuery(CommandType.Text, query1);
                    return new OutputMessage("Reserved Quantity is successfully added", true, Type.NoError, "Reserve | ReserveQty", System.Net.HttpStatusCode.OK);

                }

            }
            catch (Exception ex)
            {
                return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Reserve | ReserveQty", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve details of reserved products
        /// </summary>
        /// <param name="LocationId">Location</param>
        /// <returns></returns>
        public static List<Item> GetReservedProducts(int LocationId)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select r.Reserve_Id,r.Item_Id,r.Instance_Id,isnull(r.Qty,0)[Reserved_Qty],it.Name[Item],
                               it.mrp,it.selling_Price from TBL_RESERVE r
                               left join TBL_ITEM_INSTANCES i on i.Instance_Id=r.Instance_Id
                               left join TBL_ITEM_MST it on it.Item_Id=r.Item_Id
	                           where isnull(r.Qty,0)<>0 and r.Location_Id=@Location_Id";
                db.Open();
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Item> result = new List<Item>();
                if (dt != null)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DataRow rowItem = dt.Rows[i];
                        Item item = new Item();
                        item.ID = rowItem["Reserve_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Reserve_Id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.InstanceId = rowItem["Instance_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Instance_Id"]) : 0;
                        item.Name = Convert.ToString(rowItem["Item"]);
                        item.SellingPrice = rowItem["Selling_Price"] != DBNull.Value ? Convert.ToInt32(rowItem["Selling_Price"]) : 0;
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToInt32(rowItem["Mrp"]) : 0;
                        item.ReservedQuantity = rowItem["Reserved_Qty"] != DBNull.Value ? Convert.ToInt32(rowItem["Reserved_Qty"]) : 0;
                        result.Add(item);

                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Reserve | GetReservedProducts(int LocationId)");
                return null;
            }
            finally
            {
                db.Close();

            }
        }
        /// <summary>
        /// Delete reserved products
        /// </summary>
        /// <param name="id">ItemId of that particular Item</param>
        /// <param name="InstanceId">InstanceId of that particular item</param>
        /// <returns>Return success message when product deleted successfully otherwise return error alert</returns>

        public static OutputMessage ReserveDelete(int id, int InstanceId)
        {
            DBManager db = new DBManager();
            try
            {
                if (InstanceId < 0)
                {
                    string query = @"update tbl_reserve set Qty=0 where Item_Id=@Item_Id and Instance_Id=@Instance_Id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@Item_Id", id);
                    db.AddParameters(1, "@Instance_Id", InstanceId);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Reserved Quantity is successfully deleted", true, Type.NoError, "Reserve |ReserveDelete", System.Net.HttpStatusCode.OK);
                }
                else
                {
                    string query = @"update tbl_reserve set Qty=0 where Instance_Id=@Instance_Id and Item_Id=@Item_Id";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@Instance_Id", InstanceId);
                    db.AddParameters(1, "@Item_Id", id);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Reserved Quantity is successfully deleted", true, Type.NoError, "Reserve |ReserveDelete", System.Net.HttpStatusCode.OK);
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
                        return new OutputMessage("You cannot delete this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Reserve | ReserveDelete", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Reserve | ReserveDelete", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong.could not save delete Entry", false, Type.Others, "Reserve| ReserveDelete", System.Net.HttpStatusCode.InternalServerError,ex);
                }

            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Update details of reserved products
        /// </summary>
        /// <param name="InstanceId">InstanceId of that particular product</param>
        /// <param name="Id">ItemId of that particular product</param>
        /// <param name="locationId">LocationId</param>
        /// <param name="ModifiedBy">User Id </param>
        /// <param name="Quantity">Number of items to reserve</param>
        /// <returns>Return success message when details updated successfully otherwise return an error alert</returns>
        public static OutputMessage UpdateReserve(int InstanceId, int Id, int locationId, int ModifiedBy, decimal Quantity)
        {
            DBManager db = new DBManager();

            try
            {
                db.Open();
               
                    string query = @" select isnull(stock, 0)[Stock],isnull((select qty from TBL_RESERVE where Item_Id = @item_id and Instance_Id = @instance),0)[current_reserve]
                                   from VW_STOCK where Item_Id = @item_id and Instance_Id = @instance and Location_Id=@Location_Id";
                    db.CreateParameters(3);
                    db.AddParameters(0, "@item_id", Id);
                    db.AddParameters(1, "@instance", InstanceId);
                    db.AddParameters(2, "@Location_Id", locationId);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        return new OutputMessage("No Stock Found", false, Type.Others, "Reserve | UpdateReserve", System.Net.HttpStatusCode.InternalServerError);
                    }

                    else if (Convert.ToDecimal(dt.Rows[0]["stock"]) < Quantity)
                    {
                        return new OutputMessage("Quantity Exceeds", false, Type.Others, "Reserve | UpdateReserve", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else
                    {
                        query = @"update tbl_reserve set Qty=@Qty,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where Item_Id=@id and Instance_Id=@Instance_Id";
                        db.CreateParameters(4);
                        db.AddParameters(0, "@Qty", Quantity);
                        db.AddParameters(1, "@id", Id);
                        db.AddParameters(2, "@Modified_By", ModifiedBy);
                        db.AddParameters(3, "@Instance_Id", InstanceId);
                        db.ExecuteNonQuery(CommandType.Text, query);
                        return new OutputMessage("Reserved Quantity is successfully updated", true, Type.NoError, "Reserve | UpdateReserve", System.Net.HttpStatusCode.OK);
                    }
                }
                
            catch(Exception ex)
            {
                return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Reserve | UpdateReserve", System.Net.HttpStatusCode.InternalServerError,ex);
            }
            finally
            {
                db.Close();
            }
        }
     }
}
