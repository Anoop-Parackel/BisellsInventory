using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
    public class Despatch
    {
        #region properties
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhone { get; set; }
        public string Narration { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        #endregion properties
        /// <summary>
        /// Retrieve Id and Name for Dropdownlist Population
        /// </summary>
        /// <param name="CompanyId">Company id of that particular despatch</param>
        /// <returns>Dropdown list of despatch names</returns>
        public static DataTable GetDespatch(int CompanyId)
        {

            using (DBManager db = new DBManager())
            {

                try
                {
                    db.Open();
                    string query = "SELECT [Despatch_Id],[Name] FROM [dbo].[TBL_DESPATCH_MST] where Status<>0 and Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Despatch | GetDespatch(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// Save details of each despatchs
        /// for save an entry the id must be zero
        /// </summary>
        /// <returns>Output message of Success when details saved successfully otherwise return error message</returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Despatch, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Despatch | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Despatch Name must not be empty", false, Type.RequiredFields, "Despatch | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"INSERT INTO [dbo].[TBL_DESPATCH_MST]([Name],[Address],[Phone_No],[Mobile_No],[Contact_Person],[Contact_Person_Phone],[Narration],[Created_By],[Created_Date],[Status],[Company_Id])
                                             values(@Name,@Address,@PhoneNo,@Mobile_No,@Contact_Person,@Contact_Person_Phone,@Narration,@Created_By,GETUTCDATE(),@Status,@Company_Id);select @@identity";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address", this.Address);
                        db.AddParameters(2, "@PhoneNo", this.PhoneNo);
                        db.AddParameters(3, "@Mobile_No", this.MobileNo);
                        db.AddParameters(4, "@Contact_Person", this.ContactPerson);
                        db.AddParameters(5, "@Contact_Person_Phone", this.ContactPersonPhone);
                        db.AddParameters(6, "@Narration", this.Narration);
                        db.AddParameters(7, "@Created_By", this.CreatedBy);
                        db.AddParameters(9, "@Status", this.Status);
                        db.AddParameters(10, "@Company_Id", this.CompanyId);
                        int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                        //bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (identity >= 1)
                        {
                            return new OutputMessage("Despatch Saved successfully", true, Type.NoError, "Despatch | Save", System.Net.HttpStatusCode.OK,identity);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Despatch could not be saved", false, Type.Others, "Despatch | Save", System.Net.HttpStatusCode.InternalServerError);
                        }

                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Despatch could not be saved", false, Type.Others, "Despatch | Save", System.Net.HttpStatusCode.InternalServerError,ex);
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
        /// Update details of each despatchs
        /// to update an entry the id must be grater than zero
        /// </summary>
        /// <returns>Output message of Success when details Updated successfully otherwise return error message</returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Despatch, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Despatch | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (this.ID == 0)
            {
                return new OutputMessage("Select An Entry for Saving", false, Type.RequiredFields, "Despatch | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (string.IsNullOrWhiteSpace(this.Name))
            {
                return new OutputMessage("Despatch Name must not be empty", false, Type.RequiredFields, "Despatch | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                using (DBManager db = new DBManager())
                {
                    try
                    {
                        db.Open();
                        string query = @"Update [dbo].[TBL_DESPATCH_MST] set [Name]=@Name,[Address]=@Address,[Phone_No]=@PhoneNo,[Mobile_No]=@Mobile_No,
                                            [Contact_Person]=@Contact_Person,[Contact_Person_Phone]=@Contact_Person_Phone,
                                             [Narration]=@Narration,[Modified_By]=@Modified_By,[Modified_Date]=GETUTCDATE(),[Status]=@Status
                                              where Despatch_Id=@id";
                        db.CreateParameters(11);
                        db.AddParameters(0, "@Name", this.Name);
                        db.AddParameters(1, "@Address", this.Address);
                        db.AddParameters(2, "@PhoneNo", this.PhoneNo);
                        db.AddParameters(3, "@Mobile_No", this.MobileNo);
                        db.AddParameters(4, "@Contact_Person", this.ContactPerson);
                        db.AddParameters(5, "@Contact_Person_Phone", this.ContactPersonPhone);
                        db.AddParameters(6, "@Narration", this.Narration);
                        db.AddParameters(7, "@Modified_By", this.ModifiedBy);
                        db.AddParameters(9, "@Status", this.Status);
                        db.AddParameters(10, "@id", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Despatch updated successfully", true, Type.NoError, "Despatch | Update", System.Net.HttpStatusCode.OK);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Despatch could not be updated", false, Type.Others, "Despatch | Update", System.Net.HttpStatusCode.InternalServerError);
                        }
                    }
                    catch (Exception ex)
                    {
                        return new OutputMessage("Something went wrong. Despatch could not be updated", false, Type.Others, "Despatch | Update", System.Net.HttpStatusCode.InternalServerError,ex);
                    }
                    finally
                    {
                        db.Close();

                    }

                }
            }
        }
        /// <summary>
        ///  Delete individual despatch from despatch master.
        /// For delete an entry, first set the particular id u want to delete
        /// </summary>
        /// <returns>return success alert when the details deleted successfully otherwise return error alert</returns>
        public OutputMessage Delete()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Despatch, Security.PermissionTypes.Delete))
                    {
                        return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError);
                    }
                    else if (this.ID != 0)
                    {
                        db.Open();
                        string query = @"delete from TBL_DESPATCH_MST where Despatch_Id=@ID";
                        db.CreateParameters(1);
                        db.AddParameters(0, "@ID", this.ID);
                        bool status = Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query)) >= 1 ? true : false;
                        if (status)
                        {
                            return new OutputMessage("Despatch Deleted Successfully", true, Type.Others, "Despatch | Delete", System.Net.HttpStatusCode.OK);

                        }
                        else
                        {
                            return new OutputMessage("Please Select a Despatch. Despatch could not be deleted", false, Type.Others, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. Despatch could not be deleted", false, Type.Others, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError);
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

                            return new OutputMessage("You cannot delete this despatch because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Despatch could not be deleted", false, Type.Others, "Despatch | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {

                    db.Close();

                }

            }
        }

        /// <summary>
        /// Retrieve a list of despatchs from despatch master under a particular company id
        /// </summary>
        /// <param name="CompanyId">Company id of despatchs list</param>
        /// <returns>list of despatchs</returns>
        public static List<Despatch> GetDetails(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select ds.Despatch_Id,ds.Name,ds.[Address],ds.Phone_No,
                               ds.Mobile_No,ds.Contact_Person,ds.Contact_Person_Phone,
                              ds.Narration,isnull(ds.Created_By,0)[Created_By],ds.Created_Date,c.Name[Company],
                               isnull( ds.Modified_By,0)[Modified_By],ds.Modified_Date,isnull(ds.[Status],0)[Status],isnull(ds.Company_Id,0)[Company_Id]
                               from TBL_DESPATCH_MST ds
                               left join TBL_COMPANY_MST c on ds.Company_Id = c.Company_Id where c.Company_Id =@Company_Id order by ds.Created_Date desc";

                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Despatch> result = new List<Despatch>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Despatch despatch = new Despatch();
                        despatch.ID = item["Despatch_Id"] != DBNull.Value ? Convert.ToInt32(item["Despatch_Id"]) : 0;
                        despatch.Name = Convert.ToString(item["Name"]);
                        despatch.Address = Convert.ToString(item["Address"]);
                        despatch.PhoneNo = Convert.ToString(item["Phone_No"]);
                        despatch.MobileNo = Convert.ToString(item["Mobile_No"]);
                        despatch.ContactPerson = Convert.ToString(item["Contact_Person"]);
                        despatch.ContactPersonPhone = Convert.ToString(item["Contact_Person_Phone"]);
                        despatch.Company = Convert.ToString(item["Company"]);
                        despatch.Narration = Convert.ToString(item["narration"]);
                        despatch.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        result.Add(despatch);
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
                Application.Helper.LogException(ex, "Despatch | GetDetails(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// Retrieve a single despatch from despatch master
        /// </summary>
        /// <param name="id">Id of the particular item you want to retrieve</param>
        /// <param name="CompanyId">Company Id of that particular item</param>
        /// <returns>Details of a single despatch</returns>
        public static Despatch GetDetails(int id, int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select top 1 ds.Despatch_Id,ds.Name,ds.[Address],ds.Phone_No,
                               ds.Mobile_No,ds.Contact_Person,ds.Contact_Person_Phone,
                               ds.Narration,isnull(ds.Created_By,0)[Created_By],ds.Created_Date, 
                               isnull(ds.Modified_By,0)[Modified_By],ds.Modified_Date,isnull(ds.[Status],0)[Status],
                               isnull(ds.Company_Id,0)[Company_Id],c.Name[Company] from TBL_DESPATCH_MST ds
                               left join TBL_COMPANY_MST c on ds.Company_Id = c.Company_Id where c.Company_Id =@Company_Id and ds.Despatch_Id= @id";
                db.CreateParameters(2);
                db.AddParameters(0, "@Company_Id", CompanyId);
                db.AddParameters(1, "@id", id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    Despatch despatch = new Despatch();
                    DataRow item = dt.Rows[0];
                    despatch.ID = item["Despatch_Id"] != DBNull.Value ? Convert.ToInt32(item["Despatch_Id"]) : 0;
                    despatch.Name = Convert.ToString(item["Name"]);
                    despatch.Address = Convert.ToString(item["Address"]);
                    despatch.PhoneNo = Convert.ToString(item["Phone_No"]);
                    despatch.MobileNo = Convert.ToString(item["Mobile_No"]);
                    despatch.ContactPerson = Convert.ToString(item["Contact_Person"]);
                    despatch.ContactPersonPhone = Convert.ToString(item["Contact_Person_Phone"]);
                    despatch.Company = Convert.ToString(item["Company"]);
                    despatch.Narration = Convert.ToString(item["narration"]);
                    despatch.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                    return despatch;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Despatch |  GetDetails(int id, int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public static dynamic GetDespatchList(int CompanyId)
        {
            using (DBManager db = new DBManager())
            {
                db.Open();
                try
                {
                    List<dynamic> result = new List<dynamic>();
                    string query = @"SELECT [Despatch_Id],[Name] FROM [dbo].[TBL_DESPATCH_MST] where Status<>0 and Company_Id=@Company_Id and  Status<>0";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    foreach (DataRow item in dt.Rows)
                    {
                        result.Add(new { Despatch = item["Name"].ToString(), DespatchId = Convert.ToInt32(item["Despatch_Id"]) });
                    }
                    return result;

                }
                catch(Exception ex)
                {
                    Application.Helper.LogException(ex, "Despatch |  GetDespatchList(int CompanyId)");
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

