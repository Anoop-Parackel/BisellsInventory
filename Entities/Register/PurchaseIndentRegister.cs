using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities.Register;
using System.Net;
using System.Net.Mail;
using SelectPdf;
using System.IO;
using System.Threading;
using Entities.Application;

namespace Entities.Register
{
  public  class PurchaseIndentRegister:Register,IRegister
    {
        #region properties
        public int SupplierId { get; set; }
        public string IndentNo { get; set; }
        public string FinancialYear { get; set; }
        public int RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Priority { get; set; }
        public List<Item> Products { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public string CompanyAddress1 { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyRegId { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyState { get; set; }
        public string LocationRegId { get; set; }
        public override decimal NetAmount { get; set; }
        public int CostCenterId { get; set; }
        public string CostCenter { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string SupplierMail { get; set; }
        public string SupplierMailCC { get; set; }
        public string SupplierMailBCC { get; set; }
        public string LocationAddress1 { get; set; }
        public string LocationAddress2 { get; set; }
        public string LocationPhone { get; set; }
        public string TermsandConditon { get; set; }
        public string Payment_Terms { get; set; }
        #endregion properties

        /// <summary>
        /// Function to Manipulate the Purchase Request Register
        /// Inserts new Entry if ID is 0
        /// Update the existing Entry with the given ID
        /// Make sure the instance properties are set
        /// </summary>
        /// <returns></returns>

        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.PurchaseIndent, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseIndent | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {
                int identity;
                //Main Validations. Use ladder-if after this "if" for more validations

                if (this.RequestDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to make a request.", false, Type.Others, "PurchaseIndent  | Save", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.DueDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to make a request.", false, Type.Others, "PurchaseIndent | Save", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to make a request.", false, Type.Others, "PurchaseIndent | Save", System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    db.Open();
                    string query = @"INSERT INTO [dbo].[TBL_PURCHASE_INDENT_REGISTER]
([Indent_No],[Request_Date],[Tax_amount],[Gross_Amount],[Net_Amount],[Narration],[Round_Off],[Request_Status],
[Due_Date],[Priorities],[Created_By],[Created_Date],Cost_Center_Id,Job_Id,Company_Id,TandC,Payment_Terms) VALUES([dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PND'),@Request_Date, @Tax_amount, @Gross_Amount, @Net_Amount, @Narration, @Round_Off, @Request_Status, @Due_Date, @Priorities, @Created_By, GETUTCDATE(),@Cost_Center_Id,@Job_Id,@Company_Id,@TandC,@Payment_Terms); select @@IDENTITY";
                    db.CreateParameters(15);
                    db.AddParameters(0, "@Request_Date", this.RequestDate);
                    db.AddParameters(1, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(2, "@Gross_Amount", this.Gross);
                    db.AddParameters(3, "@Net_Amount", this.NetAmount);
                    db.AddParameters(4, "@Narration", this.Narration);
                    db.AddParameters(5, "@Round_Off", this.RoundOff);
                    db.AddParameters(6, "@Request_Status", this.RequestStatus);
                    db.AddParameters(7, "@Created_By", this.CreatedBy);
                    db.AddParameters(8, "@Due_Date", this.DueDate);
                    db.AddParameters(9, "@Priorities", this.Priority);
                    db.AddParameters(10, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(11, "@Job_Id", this.JobId);
                    db.AddParameters(12, "@Company_Id", this.CompanyId);
                    db.AddParameters(13, "@TandC", this.TermsandConditon);
                    db.AddParameters(14, "@Payment_Terms", this.Payment_Terms);
                    db.BeginTransaction();
                    identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    UpdateOrderNumber(this.CompanyId, this.FinancialYear, "PND", db);

                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase Indent | Save", System.Net.HttpStatusCode.InternalServerError);
                        }
                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            query = @"INSERT INTO [dbo].[TBL_PURCHASE_INDENT_DETAILS]([Pi_Id],[Item_Id],[Instance_Id],[Qty],[Mrp],[Rate],[Tax_Id]
                                      ,[Tax_Amount],[Gross_Amount],[Net_Amount],[Priority],[Request_Status],Description)
                                      VALUES(@Pi_Id,@Item_Id,@Instance_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,
                                      @Gross_Amount,@Net_Amount,@Priority,@Request_Status,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(13);
                            db.AddParameters(0, "@Pi_Id", identity);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.AddParameters(2, "@Qty", item.Quantity);
                            db.AddParameters(3, "@Mrp", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", prod.Gross);
                            db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(9, "@Priority", this.Priority);
                            db.AddParameters(10, "@Request_Status", this.RequestStatus);
                            db.AddParameters(11, "@instance_id", prod.InstanceId);
                            db.AddParameters(12, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
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
                        this.RoundOff = _NetAmount-this.NetAmount;
                    }
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_INDENT_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where PI_Id=" + identity);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select indent_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PND')[New_Order],Pi_Id from TBL_PURCHASE_INDENT_REGISTER where PI_Id=" + identity);
                return new OutputMessage("Purchase indent has been Registered as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase indent | Save", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pi_ID"] });
            }
            catch (Exception ex)
            {
                db.RollBackTransaction();
                return new OutputMessage("Something went wrong. Request could not be saved", false, Type.Others, "Purchase indent | Save", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }

        }
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseIndent, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseINDENT | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            DBManager db = new DBManager();
            try
            {

                //Main Validations. Use ladder-if after this "if" for more validations
                if (this.RequestDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update an Indent.", false, Type.Others, "Purchase Indent | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                else if (this.DueDate.Year < 1900)
                {
                    return new OutputMessage("Select a valid date to update an Indent.", false, Type.Others, "Purchase Indent | Update", System.Net.HttpStatusCode.InternalServerError);

                }

                else if (this.Products.Count <= 0)
                {
                    return new OutputMessage("Select some products to update an Indent.", false, Type.Others, "Purchase Indent | Update", System.Net.HttpStatusCode.InternalServerError);
                }
            
                else
                {
                    db.Open();
                    string query = @"delete from TBL_PURCHASE_INDENT_DETAILS where Pi_Id=@Pi_Id;
                                  update [TBL_PURCHASE_INDENT_register] set [Request_Date]=@Request_Date,[Tax_amount]=@Tax_amount,[Gross_Amount]=@Gross_Amount
                                  ,[Net_Amount]=@Net_Amount,[Narration]=@Narration,[Round_Off]=@Round_Off,Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),
                                   [Due_Date]=@Due_Date,[Priorities]=@Priorities,Cost_Center_Id=@Cost_Center_Id,Job_Id=@Job_Id,Company_Id=@Company_Id,
                                    TandC=@TandC,Payment_Terms=@Payment_Terms
                                    where Pi_Id=@Pi_Id";
                    db.CreateParameters(16);
                    db.AddParameters(0, "@Request_Date", this.RequestDate);
                    db.AddParameters(1, "@Tax_amount", this.TaxAmount);
                    db.AddParameters(2, "@Gross_Amount", this.Gross);
                    db.AddParameters(3, "@Net_Amount", this.NetAmount);
                    db.AddParameters(4, "@Narration", this.Narration);
                    db.AddParameters(5, "@Round_Off", this.RoundOff);
                    db.AddParameters(6, "@Request_Status", this.RequestStatus);
                    db.AddParameters(7, "@Modified_By", this.ModifiedBy);
                    db.AddParameters(8, "@Due_Date", this.DueDate);
                    db.AddParameters(9, "@Priorities", this.Priority);
                    db.AddParameters(10, "@Pi_Id", this.ID);
                    db.AddParameters(11, "@Cost_Center_Id", this.CostCenterId);
                    db.AddParameters(12, "@Job_Id", this.JobId);
                    db.AddParameters(13, "@Company_Id", this.CompanyId);
                    db.AddParameters(14, "@TandC", this.TermsandConditon);
                    db.AddParameters(15, "@Payment_Terms", this.Payment_Terms);
                    db.BeginTransaction();
                    Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));

                    foreach (Item item in Products)
                    {
                        //Product wise Validations. Use ladder-if after this "if" for more validations
                        if (item.Quantity <= 0)
                        {
                            return new OutputMessage("Some of the selected Products have a quantity less than or equal to zero. Please revert and try again", false, Type.Others, "Purchase INDENT | Update", System.Net.HttpStatusCode.InternalServerError);
                        }

                        else
                        {
                            Item prod = Item.GetPrices(item.ItemID, item.InstanceId);
                            prod.TaxAmount = prod.CostPrice * (prod.TaxPercentage / 100) * item.Quantity;
                            prod.Gross = item.Quantity * prod.CostPrice;
                            prod.NetAmount = prod.Gross + prod.TaxAmount;
                            db.CleanupParameters();
                            query = @"insert into [TBL_PURCHASE_INDENT_DETAILS]([Pi_Id],[Item_Id],[Qty],[Mrp],[Rate],[Tax_Id],[Tax_Amount],[Gross_Amount],
                                   [Net_Amount],[Priority],[Request_Status],[instance_id],Description)
	                               values(@Pi_Id,@Item_Id,@Qty,@Mrp,@Rate,@Tax_Id,@Tax_Amount,@Gross_Amount,@Net_Amount,
                                   @Priority,@Request_Status,@instance_id,@Description)";
                            db.CleanupParameters();
                            db.CreateParameters(13);
                            db.AddParameters(0, "@Pi_Id", this.ID);
                            db.AddParameters(1, "@Item_Id", item.ItemID);
                            db.AddParameters(2, "@Qty", item.Quantity);
                            db.AddParameters(3, "@Mrp", prod.MRP);
                            db.AddParameters(4, "@Rate", prod.CostPrice);
                            db.AddParameters(5, "@Tax_Id", prod.TaxId);
                            db.AddParameters(6, "@Tax_Amount", prod.TaxAmount);
                            db.AddParameters(7, "@Gross_Amount", prod.Gross);
                            db.AddParameters(8, "@Net_Amount", prod.NetAmount);
                            db.AddParameters(9, "@Priority", this.Priority);
                            db.AddParameters(10, "@Request_Status", this.RequestStatus);
                            db.AddParameters(11, "@instance_id", prod.InstanceId);
                            db.AddParameters(12, "@Description", item.Description);
                            db.ExecuteProcedure(System.Data.CommandType.Text, query);
                            this.TaxAmount += prod.TaxAmount;
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
                    db.ExecuteNonQuery(CommandType.Text, @"update [TBL_PURCHASE_INDENT_REGISTER] set [Tax_Amount]=" + this.TaxAmount + ", [Gross_Amount]=" + this.Gross + " ,[Net_Amount]=" + _NetAmount + " ,[Round_Off]=" + this.RoundOff + " where Pi_Id=" + ID);
                }
                db.CommitTransaction();
                DataTable dt = db.ExecuteQuery(CommandType.Text, "select INDENT_No[Saved_No],[dbo].UDF_Generate_Sales_Bill(" + this.CompanyId + ",'" + this.FinancialYear + "','PND')[New_Order],Pi_Id from tbl_purchase_indent_register where Pi_Id=" + ID);
                return new OutputMessage("Purchase indent has been updated as " + dt.Rows[0]["Saved_No"].ToString(), true, Type.NoError, "Purchase indent | Update", System.Net.HttpStatusCode.OK, new { OrderNo = dt.Rows[0]["New_Order"].ToString(), Id = dt.Rows[0]["Pi_Id"] });

            }
            catch (Exception ex)
            {
                dynamic Exception = ex;
                if (ex.GetType().GetProperty("Number") != null)
                {
                    if (Exception.Number == 547)
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("You cannot Update this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase INDENT | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                    else
                    {
                        db.RollBackTransaction();

                        return new OutputMessage("Something went wrong. Purchase Indent could not be updated", false, Type.Others, "Purchase Indent | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                else
                {
                    db.RollBackTransaction();

                    return new OutputMessage("Something went wrong. Purchase Indent could not be updated", false, Type.Others, "PurchaseIndent  | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }

            }
            finally
            {
                db.Close();
            }

        }
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.PurchaseIndent, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "PurchaseIndent | Delete", System.Net.HttpStatusCode.InternalServerError);

            }
            if (this.ID == 0)
            {
                return new OutputMessage("No entry is Selected for deletion", false, Type.RequiredFields, "PurchaseIndent | Delete", System.Net.HttpStatusCode.InternalServerError);
            }
       
            else
            {
                DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_PURCHASE_Indent_DETAILS where Pi_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.BeginTransaction();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    query = "delete from TBL_PURCHASE_Indent_REGISTER where Pi_Id=@id";
                    db.AddParameters(0, "@id", this.ID);
                    db.ExecuteNonQuery(CommandType.Text, query);
                    db.CommitTransaction();
                    return new OutputMessage("Purchase Indent has been deleted", true, Type.NoError, "PurchaseIndent | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("You cannot deleted this Entry because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Purchase Indent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            db.RollBackTransaction();

                            return new OutputMessage("Something went wrong. Try again later", false, Type.RequiredFields, "PurchaseIndent| Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                    }
                    else
                    {
                        db.RollBackTransaction();
                        return new OutputMessage("Something went wrong. Purchase Indent could not be deleted", false, Type.Others, "PurchaseIndent | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }
                }
                finally
                {
                    db.Close();

                }
            }

        }
        public OutputMessage SendMail(int indentId, string url)
        {

            DBManager db = new DBManager();

            try
            {
                //getting the email content from print page
                WebClient client = new WebClient();
                UTF8Encoding utf8 = new UTF8Encoding();
                string htmlMarkup = utf8.GetString(client.DownloadData(url));

                //creating PDF 
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertUrl(url);
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    bytes = ms.ToArray();
                    ms.Close();
                    doc.DetachStream();
                }

                string query = @"select TagName,KeyID,KeyValue from TBL_SETTINGS where KeyID=106
                                 select TagName,KeyID,KeyValue from TBL_SETTINGS where KeyID=107
                                 select TagName,KeyID,KeyValue from TBL_SETTINGS where KeyID=108
                                 select TagName,KeyID,KeyValue from TBL_SETTINGS where KeyID=109";
                db.Open();
                DataSet ds = db.ExecuteDataSet(CommandType.Text, query);
                string fromMail = Convert.ToString(ds.Tables[0].Rows[0]["KeyValue"]);
                string MailPassword = Convert.ToString(ds.Tables[1].Rows[0]["KeyValue"]);
                string Host = Convert.ToString(ds.Tables[2].Rows[0]["KeyValue"]);
                string Port = Convert.ToString(ds.Tables[3].Rows[0]["KeyValue"]);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromMail);

                if (!string.IsNullOrWhiteSpace(SupplierMail))
                {
                    mail.To.Add(this.SupplierMail);


                }
                if (!string.IsNullOrWhiteSpace(this.SupplierMailBCC))
                {
                    mail.Bcc.Add(this.SupplierMailBCC);


                }
                if (!string.IsNullOrWhiteSpace(SupplierMailCC))
                {
                    mail.CC.Add(SupplierMailCC);

                }
                mail.Body = "Purchase Indent";
                mail.IsBodyHtml = false;
                mail.Subject = "Invoice";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Indent.pdf"));
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Host,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMail, MailPassword),
                    Port = Convert.ToInt32(Port)
                };
                smtp.Send(mail);
                query = "update TBL_PURCHASE_INDENT_REGISTER set supplier_mail=@mail,SupplierCC_Mail=@SupplierCC_Mail,SupplierBCC_Mail=@SupplierBCC_Mail where pi_id=@indentId";
                db.CreateParameters(4);
                db.AddParameters(0, "@mail", this.SupplierMail);
                db.AddParameters(1, "@indentId", indentId);
                db.AddParameters(2, "@SupplierCC_Mail", this.SupplierMailCC);
                db.AddParameters(3, "@SupplierBCC_Mail", this.SupplierMailBCC);
                db.ExecuteNonQuery(CommandType.Text, query);
                return new OutputMessage("Mail Send Successfully", true, Type.Others, "Purchase Indent | SendMail", System.Net.HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return new OutputMessage(ex.Message, false, Type.Others, "Purchase Indent | SendMail", System.Net.HttpStatusCode.InternalServerError, ex);

            }
            finally
            {
                db.Close();
            }
        }
        public static List<PurchaseIndentRegister> GetDetails(DateTime? from, DateTime? to)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select pir.Pi_Id,pir.Indent_No,pir.Request_Date,pir.Tax_amount,pir.Gross_Amount,
                                pir.Net_Amount,pir.Narration,pir.Round_Off
                                ,pir.Due_Date,pir.Priorities,pir.Supplier_Mail,pir.SupplierBCC_Mail,pir.SupplierCC_Mail,isnull(pir.Cost_Center_Id,0)[Cost_Center_Id],isnull(pir.Job_Id,0)[Job_Id],
                                cost.Fcc_Name[Cost_Center],j.Job_Name[Job],pir.TandC,pir.Payment_Terms from TBL_PURCHASE_INDENT_REGISTER pir with(nolock)
                                 left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pir.Cost_Center_Id
                                 left join TBL_JOB_MST j on j.Job_Id=pir.Job_Id
                                  where {#daterangefilter#}  order by pir.Request_Date desc";
                #endregion query
                if (from != null && to != null)
                {
                    query = query.Replace("{#daterangefilter#}", "  pir.Request_Date>=@fromdate and pir.Request_Date<=@todate ");
                }
                else
                {
                    to = DateTime.UtcNow;
                    from = new DateTime(to.Value.Year, to.Value.Month, 01);
                    query = query.Replace("{#daterangefilter#}", "  pir.Request_Date>=@fromdate and pir.Request_Date<=@todate ");
                }            
                db.CreateParameters(2);

                db.AddParameters(0, "@fromdate", from);
                db.AddParameters(1, "@todate", to);

                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseIndentRegister> result = new List<PurchaseIndentRegister>();
                    for (int i = 0; i < dt.Rows.Count;i++)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseIndentRegister register = new PurchaseIndentRegister();
                        register.ID = row["Pi_Id"] != DBNull.Value ? Convert.ToInt32(row["Pi_Id"]) : 0;
                        register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                        register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        register.IndentNo = Convert.ToString(row["Indent_No"]);
                        register.CostCenter = Convert.ToString(row["Cost_Center"]);
                        register.JobName = Convert.ToString(row["Job"]);
                        register.SupplierMail = Convert.ToString(row["Supplier_Mail"]);
                        register.SupplierMailBCC = Convert.ToString(row["SupplierBCC_Mail"]);
                        register.SupplierMailCC = Convert.ToString(row["SupplierCC_Mail"]);
                        register.EntryDate = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]) : DateTime.MinValue;
                        register.EntryDateString = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Priority = row["Priorities"] != DBNull.Value ? Convert.ToBoolean(row["Priorities"]) : false;
                        register.DueDateString = row["Due_Date"] != DBNull.Value ? Convert.ToDateTime(row["Due_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                        register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                        register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                        register.Narration = Convert.ToString(row["Narration"]);
                        register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                        register.TermsandConditon= Convert.ToString(row["TandC"]);
                        register.Payment_Terms= Convert.ToString(row["Payment_Terms"]);
                        result.Add(register);
                    }
                    return result;

                }
                return null;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "PurchaseIndent | GetDetails( DateTime? from, DateTime? to)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        public static PurchaseIndentRegister GetDetails(int Id)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select pir.Pi_Id,pir.Indent_No,pir.Request_Date,pir.Tax_amount,pir.Gross_Amount,
         pir.Net_Amount,pir.Narration,pir.Round_Off
         ,pir.Due_Date,pir.Priorities,pir.Supplier_Mail,pid.Instance_Id,pid.Item_Id,pid.Gross_Amount [P_GrossAmount],pid.Net_Amount [P_NetAmount],
         pid.Tax_Amount [P_TaxAmount],pid.Rate [CostPrice],pid.Qty,tax.Percentage [taxPercentage],pid.Mrp,itm.Name [itemName],itm.Item_Code 
         ,isnull(pir.Cost_Center_Id,0)[Cost_Center_Id],isnull(pir.Job_Id,0)[Job_Id],cost.Fcc_Name[Cost_Center],j.Job_Name[Job]
         ,cm.name[Company],cm.Country_Id,cm.State_Id,cm.Address1[Comp_Address1],cm.Address2[Comp_Address2],cm.Reg_Id1[Comp_RegId],cm.Mobile_No1[Comp_Phone]
                               ,coun.Name[Country],st.Name[State],cm.Logo,cm.Email,pid.Description,pir.TandC,pir.Payment_Terms					  
                              from TBL_PURCHASE_INDENT_REGISTER pir with(nolock)
         left join TBL_PURCHASE_INDENT_DETAILS pid on pid.pi_id=pir.pi_id
         left join TBL_TAX_MST tax on tax.Tax_Id=pid.Tax_Id
         left join TBL_ITEM_MST itm on itm.Item_Id=pid.Item_Id
         left join tbl_Fin_CostCenter cost on cost.Fcc_ID=pir.Cost_Center_Id
                              left join TBL_JOB_MST j on j.Job_Id=pir.Job_Id
         inner join tbl_company_mst cm on cm.Company_Id=pir.Company_Id
                              left join TBL_COUNTRY_MST coun on coun.Country_Id=cm.Country_Id
         left join TBL_STATE_MST st on st.state_Id=cm.State_Id
         where pir.Pi_Id=@Pi_Id";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Pi_Id", Id);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    PurchaseIndentRegister register = new PurchaseIndentRegister();
                    register.ID = row["Pi_Id"] != DBNull.Value ? Convert.ToInt32(row["Pi_Id"]) : 0;
                    register.IndentNo = Convert.ToString(row["Indent_No"]);
                    register.CostCenter = Convert.ToString(row["Cost_Center"]);
                    register.JobName = Convert.ToString(row["Job"]);
                    register.CostCenterId = row["Cost_Center_Id"] != DBNull.Value ? Convert.ToInt32(row["Cost_Center_Id"]) : 0;
                    register.JobId = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    register.EntryDate = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]) : DateTime.MinValue;
                    register.EntryDateString = row["Request_Date"] != DBNull.Value ? Convert.ToDateTime(row["Request_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.Priority = row["Priorities"] != DBNull.Value ? Convert.ToBoolean(row["Priorities"]) : false;
                    register.DueDateString = row["Due_Date"] != DBNull.Value ? Convert.ToDateTime(row["Due_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    register.Gross = row["Gross_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Gross_Amount"]) : 0;
                    register.TaxAmount = row["Tax_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Tax_Amount"]) : 0;
                    register.NetAmount = row["Net_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Net_Amount"]) : 0;
                    register.Narration = Convert.ToString(row["Narration"]);
                    register.Company = Convert.ToString(row["Company"]);
                    register.CompanyAddress1 = Convert.ToString(row["Comp_Address1"]);
                    register.CompanyAddress2 = Convert.ToString(row["Comp_Address2"]);
                    register.CompanyPhone = Convert.ToString(row["Comp_Phone"]);
                    register.CompanyRegId = Convert.ToString(row["Comp_RegId"]);
                    register.CompanyCountry = Convert.ToString(row["Country"]);
                    register.CompanyState = Convert.ToString(row["State"]);
                    register.CompanyEmail = Convert.ToString(row["Email"]);
                    register.CompanyLogo = Convert.ToBase64String(row["logo"] as byte[]);
                    register.RoundOff = row["Round_Off"] != DBNull.Value ? Convert.ToDecimal(row["Round_Off"]) : 0;
                    register.TermsandConditon = Convert.ToString(row["TandC"]);
                    register.Payment_Terms = Convert.ToString(row["Payment_Terms"]);
                    DataTable inProducts = dt.AsEnumerable().Where(x => x.Field<int>("Pi_Id") == register.ID).CopyToDataTable();
                    List<Item> products = new List<Item>();
                    for (int j = 0; j < inProducts.Rows.Count; j++)
                    {
                        DataRow rowItem = inProducts.Rows[j];
                        Item item = new Item();
                        item.InstanceId = rowItem["instance_id"] != DBNull.Value ? Convert.ToInt32(rowItem["instance_id"]) : 0;
                        item.ItemID = rowItem["Item_Id"] != DBNull.Value ? Convert.ToInt32(rowItem["Item_Id"]) : 0;
                        item.Name = rowItem["ItemName"].ToString();
                        item.Description = Convert.ToString(rowItem["Description"]);
                        item.MRP = rowItem["Mrp"] != DBNull.Value ? Convert.ToDecimal(rowItem["Mrp"]) : 0;
                        item.CostPrice = rowItem["CostPrice"] != DBNull.Value ? Convert.ToDecimal(rowItem["CostPrice"]) : 0;
                        item.TaxPercentage = rowItem["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(rowItem["TaxPercentage"]) : 0;
                        item.Gross = rowItem["P_GrossAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_GrossAmount"]) : 0;
                        item.NetAmount = rowItem["P_NetAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_NetAmount"]) : 0;
                        item.TaxAmount = rowItem["P_TaxAmount"] != DBNull.Value ? Convert.ToDecimal(rowItem["P_TaxAmount"]) : 0;
                        item.Quantity = rowItem["Qty"] != DBNull.Value ? Convert.ToDecimal(rowItem["Qty"]) : 0;
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
                Application.Helper.LogException(ex, "PurchaseIdent | GetDetails(int Id)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


        public static List<PurchaseIndentRegister> GetDetailsIndent(int LocationID)
        {
            DBManager db = new DBManager();
            try
            {
                #region query
                db.Open();
                string query = @"select m.Pi_Id [PurchaseRequestId],m.Due_Date,m.Request_Status,m.Round_Off,isnull(m.Indent_No,0)[Request_No],t.Percentage[TaxPercentage],
                              m.Priorities,isnull(l.Name,0)[location],L.Company_Id,isnull(c.Name,0)[company],it.Item_Code,m.Gross_Amount,m.Tax_amount,
                              isnull(m.Narration,0)[Narration],m.Net_Amount,us.Location_Id,m.Request_Date,d.Pid_Id [DetailsId],it.Item_Id,
                              it.Name[ItemName],d.Mrp,d.Rate[CostPrice],d.Qty,d.Tax_Amount[P_TaxAmount],d.Gross_Amount[P_GrossAmount],
                              d.Net_Amount[P_NetAmount],d.Request_Status[p_RequestStatus],d.instance_id,m.Created_Date,isnull(l.Address1,0)[Loc_Address1],
                              isnull(l.Address2,0)[Loc_Address2],isnull(l.Contact,0)[Loc_Phone],m.Round_Off,isnull(c.Address1,0)[Comp_Address1],isnull(c.Address2,0)[Comp_Address2],isnull(c.Mobile_No1,0)[Comp_Phone]
                              ,l.Reg_Id1[Location_RegistrationId],c.Reg_Id1[Company_registrationId],c.Email[Company_Email],c.Logo[Comp_Logo]  
							  from TBL_PURCHASE_INDENT_REGISTER m with(nolock)
                              join TBL_PURCHASE_INDENT_DETAILS d with(nolock) on m.Pi_Id = d.Pi_Id
                              left outer join TBL_ITEM_MST it with(nolock) on it.Item_Id = d.Item_Id
							  inner join TBL_USER_MST us on us.User_Id=m.Created_By
                              inner join TBL_LOCATION_MST L with(nolock) ON L.Location_Id=us.Location_Id
                              inner join TBL_COMPANY_MST C with(nolock) on C.Company_Id=L.Company_Id
                              inner join TBL_TAX_MST t with(nolock) on t.Tax_Id=d.Tax_Id
                              where us.Location_Id=@Location_Id order by m.Created_Date desc";
                #endregion query
                db.CreateParameters(1);
                db.AddParameters(0, "@Location_Id", LocationID);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<PurchaseIndentRegister> result = new List<PurchaseIndentRegister>();
                    for (int i = 0; i < dt.Rows.Count;)
                    {
                        DataRow row = dt.Rows[i];
                        PurchaseIndentRegister register = new PurchaseIndentRegister();
                        register.ID = row["PurchaseRequestId"] != DBNull.Value ? Convert.ToInt32(row["PurchaseRequestId"]) : 0;
                        register.LocationId = row["Location_Id"] != DBNull.Value ? Convert.ToInt32(row["Location_Id"]) : 0;
                        register.IndentNo = Convert.ToString(row["Request_No"]);
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
                Application.Helper.LogException(ex, "PurchaseIndent |  GetDetails(int LocationID)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }


    }
}
