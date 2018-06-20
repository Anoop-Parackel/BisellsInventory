using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
    public class ItemInstance
    {
        #region Properties
        public int ID { get; set; }
        public int ItemId { get; set; }
        public decimal Mrp { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public string Barcode { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion Properties

        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Item, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "ItemInstance | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into TBL_ITEM_INSTANCES (Item_Id,Mrp,Selling_Price,Cost_Price,Barcode,Status,Created_By,Created_Date)
                                      values(@Item_Id,@Mrp,@Selling_Price,@Cost_Price,@Barcode,@Status,@Created_By,GETUTCDATE());select @@identity;";
                        db.CreateParameters(7);
                        db.AddParameters(0, "@Item_Id", this.ItemId);
                        db.AddParameters(1, "@Mrp", this.Mrp);
                        db.AddParameters(2, "@Selling_Price", this.SellingPrice);
                        db.AddParameters(3, "@Cost_Price", this.CostPrice);
                        db.AddParameters(4, "@Barcode", this.Barcode);
                        db.AddParameters(5, "@Status", this.Status);
                        db.AddParameters(6, "@Created_By", this.CreatedBy);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                        return new OutputMessage("Instance saved successfully", true, Entities.Type.NoError, "ItemInstance | Save", System.Net.HttpStatusCode.OK, identity);



                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Somthing went wrong. Instance could not be saved", false, Entities.Type.Others, "ItemInstance | Save", System.Net.HttpStatusCode.InternalServerError,ex);

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

        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Item, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "ItemInstance | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"update TBL_ITEM_INSTANCES set Item_Id=@Item_Id,Mrp=@Mrp,Selling_Price=@Selling_Price,Cost_Price=@Cost_Price,
                                       Barcode=@Barcode,Status=@Status,Modified_By=@Modified_By,Modified_Date=GETUTCDATE() where instance_id=@id";
                        db.CreateParameters(8);
                        db.AddParameters(0, "@Item_Id", this.ItemId);
                        db.AddParameters(1, "@Mrp", this.Mrp);
                        db.AddParameters(2, "@Selling_Price", this.SellingPrice);
                        db.AddParameters(3, "@Cost_Price", this.CostPrice);
                        db.AddParameters(4, "@Barcode", this.Barcode);
                        db.AddParameters(5, "@Status", this.Status);
                        db.AddParameters(6, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(7, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Instance Updated Successfully", true, Entities.Type.NoError, "ItemInstance | Update", System.Net.HttpStatusCode.OK, this.ID);

                        }
                        else
                        {
                            return new OutputMessage("Somthing went wrong. Instance could not be updated", false, Entities.Type.Others, "ItemInstance | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Somthing went wrong. Instance could not be updated", false, Entities.Type.Others, "ItemInstance | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();
                    }

                }
            }
        }

        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Item, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "ItemInstance | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_ITEM_INSTANCES where Instance_Id=@id";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("Instance deleted Successfully", true, Entities.Type.NoError, "ItemInstance | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Instance could not be deleted", false, Entities.Type.Others, "ItemInstance | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID Cannot be zero for deletion", false, Entities.Type.Others, "ItemInstance | Delete", System.Net.HttpStatusCode.InternalServerError);

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

                            return new OutputMessage("You cannot delete this iteminstance because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "iteminstance | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "iteminstance | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Instance could not be deleted", false, Type.Others, "iteminstance | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }


                }
                finally
                {

                    db.Close();

                }

            }
        }

    }
}
