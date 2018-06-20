using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;


namespace Entities
{
    public class UOM
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int CreatedBy { get; set; }
        public string ShortName { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name of UOM for populating dropdown list of UOM.
        /// </summary>
        /// <param name="CompanyId">company id of that particular UOM</param>
        /// <returns>dropdown list of UOM</returns>
        public static DataTable GetUnits(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    string query = @"SELECT [Unit_Id],[Short_Name] FROM [dbo].[TBL_UNIT_MST] where Company_Id=@Company_Id and Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);
                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "UOM| GetUnits(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each UOM
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Unit, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "UOM | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("UOM  must not be empty", false, Entities.Type.RequiredFields, "UOM | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"insert into [dbo].[TBL_UNIT_MST](Name,Status,Created_By,Created_Date,Short_Name,Company_Id)values(@Name,@Status,@Created_By,GETUTCDATE(),@Short_Name,@Company_Id);select @@identity";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Status", this.Status);
                        db.AddParameters(2, "@Created_By", this.CreatedBy);
                        db.AddParameters(3, "@Short_Name", this.ShortName);
                        db.AddParameters(4, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        if (identity >= 1)
                        {
                            return new OutputMessage("UOM saved successfully", true, Entities.Type.NoError, "UOM | Save", System.Net.HttpStatusCode.OK,identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. UOM could not be saved", false, Entities.Type.Others, "UOM | Save", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. UOM could not be saved", false, Entities.Type.Others, "UOM | Save", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {

                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Update details of each UOM
        /// for update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Unit, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "UOM | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (this.ID == 0)
            {

                return new OutputMessage("ID not be empty", false, Entities.Type.Others, "UOM | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {

                return new OutputMessage("Name must not be empty", false, Entities.Type.RequiredFields, "UOM | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_UNIT_MST] set Name=@Name,Status=@Status,
                        Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Short_Name=@Short_Name where Unit_Id=@id";
                        db.CreateParameters(5);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Status", this.Status);
                        db.AddParameters(2, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(3, "@Short_Name", this.ShortName);
                        db.AddParameters(4, "@id", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("UOM updated Successfully", true, Entities.Type.NoError, "UOM | Update", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. UOM could not be updated", false, Entities.Type.Others, "UOM | Update", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. UOM could not be updated", false, Entities.Type.Others, "UOM | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        /// Delete individual UOM from UOM master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Unit, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Entities.Type.InsufficientPrivilege, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_UNIT_MST where Unit_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false)
                        {
                            return new OutputMessage("UOM Deleted Successfully", true, Entities.Type.NoError, "UOM | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.UOM could not be deleted", false, Entities.Type.Others, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("ID must not be zero for deletion", false, Entities.Type.Others, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError);

                    }

                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this UOM because it is referenced in other transactions", false, Entities.Type.Others, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                        else
                        {
                            return new OutputMessage("Something went wrong.UOM could not be deleted", false, Entities.Type.Others, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong.UOM could not be deleted", false, Type.Others, "UOM | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieves all the units of measurements from the Units Master
        /// </summary>
        /// <returns>List of Units</returns>
        public static List<UOM> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select u.Unit_Id,u.Name,isnull(u.[Status],0)[Status],isnull(u.Created_By,0)[Created_By],
                               u.Created_Date,isnull(u.Modified_By,0)[Modified_By],
                               u.Modified_Date, u.Short_Name,isnull(u.Company_Id,0)[Company_Id], c.Name[Company]
                               from TBL_UNIT_MST u left join TBL_COMPANY_MST c on c.Company_Id = u.Company_Id 
                               where c.Company_Id=@Company_Id order by Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<UOM> result = new List<UOM>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows) 
                    {
                        UOM uom = new UOM();
                        uom.ID = item["unit_Id"] != DBNull.Value ? Convert.ToInt32(item["Unit_Id"]) : 0;
                        uom.Name = Convert.ToString(item["Name"]);
                        uom.ShortName = Convert.ToString(item["Short_Name"]);
                        uom.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        result.Add(uom);
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
                Application.Helper.LogException(ex, "UOM| GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        /// Retrieve a single units of measurements from list of units
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CompanyId"></param>
        /// <returns>list of unit of measurements</returns>
        public static UOM GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 u.Unit_Id,u.Name,isnull(u.[Status],0)[Status],isnull(u.Created_By,0)[Created_By],u.Created_Date,
                               isnull(u.Modified_By,0)[Modified_By],u.Modified_Date, u.Short_Name,isnull( u.Company_Id,0)[Company_Id],c.Name[Company]
                               from TBL_UNIT_MST u 
                               left join TBL_COMPANY_MST c on c.Company_Id = u.Company_Id   
                               where c.Company_Id=@Company_Id and Unit_Id=@id";

                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);

                if (dt != null)
                {
                    UOM uom = new UOM();
                    DataRow item = dt.Rows[0];
                    uom.ID = item["unit_Id"] != DBNull.Value ? Convert.ToInt32(item["Unit_Id"]) : 0;
                    uom.Name = Convert.ToString(item["Name"]);
                    uom.ShortName = Convert.ToString(item["Short_Name"]);
                    uom.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;

                    return uom;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "UOM| GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

    }
}
