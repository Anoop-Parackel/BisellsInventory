using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Entities.Application
{
    public class Settings

    {
        #region property
        public int KeyId { get; set; }
        public string TagName { get; set; }
        public string InvoiceTemplateType { get; set; }
        public string InvoiceTemplateNo { get; set; }
        public string EnableDescription { get; set; }
        public string Discount { get; set; }
        public string AllowNegativeBilling { get; set; }
        public string AllowPriceEditingInPurchaseRequest { get; set; }
        public string DisplayZeroQtyLookup { get; set; }
        public int DefaultCustomer { get; set; }
        public string KeyValue { get; set; }    
        public int Status { get; set; }
        public string DayLightSetting { get; set; }
        public string AutoRoundOffSetting { get; set; }
        public string CurrencySymbolSetting { get; set; }
        public string ExpeneceGroupId { get; set; }
        public int FinantialYearSetting { get; set; }
        public string EmailId { get; set; }
        public string EmailPassword { get; set; }
        public string HostSetting { get; set; }
        public string PortSetting { get; set; }
        public int ModifiedBy { get; set; }
        public string  SalesOrderTC { get; set; }
        public int SalesOrderTCStatus { get; set; }  
        public string  SalesInvoiceTC { get; set; }
        public int SalesInvoiceTCStatus { get; set; }
        public string  SalesReturnTC { get; set; }
        public int SalesReturnTCStatus { get; set; }
        public string SalesOrderDisclimer { get; set; }
        public int SalesOrderDisclimerStatus { get; set; }
        public string SalesInvoiceDisclimer { get; set; }
        public int SalesInvoiceDisclimerStatus { get; set; }
        public string SalesReturnDisclimer { get; set; }
        public int SalesReturnDisclimerStatus { get; set; }
        public string PurchaseOrderTC { get; set; }
        public int PurchaseOrderTCStatus { get; set; }
        public string PurchaseInvoiceTC { get; set; }
        public int PurchaseInvoiceTCStatus { get; set; }
        public string PurchaseReturnTC { get; set; }
        public int PurchaseReturnTCStatus { get; set; }
        public string PurchaseOrderDisclimer { get; set; }
        public int PurchaseOrderDisclimerStatus { get; set; }
        public string PurchaseInvoiceDisclimer { get; set; }
        public int PurchaseInvoiceDisclimerStatus { get; set; }
        public string PurchaseReturnDisclimer { get; set; }
        public int PurchaseReturnDisclimerStatus { get; set; }
        public int PurchaseRequestTCStatus { get; set; }
        public int SalesRequestTCStatus { get; set; }
        public string PurchaseRequestTC { get; set; }
        public string PurchaseIndentTC { get; set; }
        public string PurchaseIndentTCStatus { get; set; }
        public string GRNTC { get; set; }
        public string GRNTCStatus { get; set; }
        public string SalesRequestTC { get; set; }
        public int PurchaseRequestDisclimerStatus { get; set; }
        public int SalesRequestDisclimerStatus { get; set; }
        public string PurchaseRequestDisclimer { get; set; }
        public string SalesRequestDisclimer { get; set; }
        public string AllowPriceEditInPurchaseQuote { get; set; }
        public string AllowPriceEditInPurchaseReturn { get; set; }
        public string AllowPriceEditInPurchaseEntry { get; set; }
        public string AllowPriceEditInSalesRequest { get; set; }
        public string AllowPriceEditInSalesQuote { get; set; }
        public string AllowPriceEditInSalesEntry { get; set; }
        public string AllowPriceEditInSalesReturn { get; set; }
        public string TransferOutTC { get; set; }
        public string TransferOutTCStatus { get; set; }
        public string TransferInTC { get; set; }
        public string TransferInTCStatus { get; set; }
        public string DamageTC { get; set; }
        public string DamageTCStatus { get; set; }
        public string TaxInvoiceTC { get; set; }
        public string TaxInvoiceTCStatus { get; set; }
        public string SalesQuotationTC { get; set; }
        public string SalesQuotationTC_Status { get; set; }
        #endregion
        public static bool IsAutoRoundOff()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select keyvalue from tbl_settings where keyId=105";
                db.Open();
                return Convert.ToBoolean(db.ExecuteScalar(System.Data.CommandType.Text, query));
            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Settings | IsAutoRoundOff()");
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        //public static dynamic GetFeaturedSettings()
        //{
        //    DBManager db = new DBManager();
        //    try
        //    {
        //        string query = @"select TagName,KeyValue from TBL_SETTINGS where KeyID=105;";
        //        db.Open();
        //        DataTable dt = db.ExecuteQuery(CommandType.Text, query);
        //        dynamic settings = new ExpandoObject();
        //        if (dt != null)
        //        {
        //            settings.AutoRoundOff = Convert.ToBoolean(dt.Rows[0]["KeyValue"]);
        //        }
        //        return settings;
        //    }
        //    catch(Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        db.Close();
        //    }
        //}
        public static string GetFeaturedSettingsSerialized()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select * from (
                                 select TagName,KeyValue,KeyID from TBL_SETTINGS where KeyID=105
                                 union 
                                 select TagName,KeyValue,KeyID from TBL_SETTINGS where KeyID=110
                                 union
                                 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=111
                                 union
								 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=150
                                 union
                                 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=112
                                 union
                                 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=113
								 union
								 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=114
								 union
								 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=132
                                 union
								 select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=135
                                 )t1 order by KeyID";
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                dynamic settings;
                if (dt != null)
                {
                    settings = new {
                        AutoRoundOff = Convert.ToBoolean(dt.Rows[0]["KeyValue"]),
                        CurrencySymbol = dt.Rows[1]["KeyValue"],
                        TemplateType = dt.Rows[2]["KeyValue"],
                        TemplateNumber = dt.Rows[3]["KeyValue"],
                        CustomerInSales=dt.Rows[8]["KeyValue"],
                        IsSalesDescriptionEnabled = Convert.ToBoolean(dt.Rows[4]["KeyValue"]),
                        IsDiscountEnabled = Convert.ToBoolean(dt.Rows[5]["KeyValue"]),
                        AllowNegativeBilling = Convert.ToBoolean(dt.Rows[6]["KeyValue"]),
                    AllowPriceEditingInPurchaseRequest = Convert.ToBoolean(dt.Rows[7]["KeyValue"])
                    };
                return new JavaScriptSerializer().Serialize(settings);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Settings |  GetFeaturedSettingsSerialized()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static string GetSetting(int keyId)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = "select isnull(keyvalue,'')keyvalue from TBL_SETTINGS where KeyID=@keyid and  status=1";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@keyid", keyId);
                    db.Open();
                    return Convert.ToString(db.ExecuteScalar(CommandType.Text, query));
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Settings | GetSetting(int keyId)");
                    return string.Empty;
                }
                finally
                {
                    db.Close();
                }
            }
        }
        public static object GetFeaturedSettings()
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select * from (select TagName,KeyValue,KeyID from TBL_SETTINGS where KeyID=105
                               union 
                               select TagName,KeyValue,KeyID from TBL_SETTINGS where KeyID=110
                               union
                               select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=111
                               union
                               select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=112
                               union
                               select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=114
                               union
                               select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=113
							   union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=132
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=135
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=136
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=137
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=138
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=139
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=140
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=141
                                union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=142
							   union
							   select TagName,keyValue,KeyID from TBL_SETTINGS where KeyID=150
                                ) t1 order by KeyID;";
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                dynamic settings = new ExpandoObject();
                if (dt != null)
                {
                    settings.AutoRoundOff = Convert.ToBoolean(dt.Rows[0]["KeyValue"]);
                    settings.CurrencySymbol = dt.Rows[1]["KeyValue"];
                    settings.IsDiscountEnabled = Convert.ToBoolean(dt.Rows[5]["KeyValue"]);
                    settings.EnableDescription = Convert.ToBoolean(dt.Rows[4]["KeyValue"]);
                    settings.AllowNegativeBilling = Convert.ToBoolean(dt.Rows[6]["KeyValue"]);
                    settings.AllowPriceEditingInPurchaseRequest = Convert.ToBoolean(dt.Rows[7]["KeyValue"]);
                    settings.AllowPriceEditingInPurchaseQuote= Convert.ToBoolean(dt.Rows[8]["KeyValue"]);
                    settings.AllowPriceEditingInPurchaseEntry= Convert.ToBoolean(dt.Rows[9]["KeyValue"]);
                    settings.AllowPriceEditingInPurchaseReturn = Convert.ToBoolean(dt.Rows[10]["KeyValue"]);
                    settings.AllowPriceEditingInSalesOrder = Convert.ToBoolean(dt.Rows[11]["KeyValue"]);
                    settings.AllowPriceEditingInSalesEntry = Convert.ToBoolean(dt.Rows[12]["KeyValue"]);
                    settings.AllowPriceEditingInSalesReturn = Convert.ToBoolean(dt.Rows[13]["KeyValue"]);
                    settings.AllowPriceEditingInSalesRequest = Convert.ToBoolean(dt.Rows[14]["KeyValue"]);
                    settings.CustomerInSales = dt.Rows[15]["KeyValue"];
                }
                return settings;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Settings |  GetFeaturedSettings()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        public static List<Settings> GetDetails()
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select * from TBL_SETTINGS";
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Settings> result = new List<Settings>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Settings setting = new Settings();
                        setting.KeyId = item["keyid"] != DBNull.Value ? Convert.ToInt32(item["keyid"]) : 0;
                        setting.TagName = Convert.ToString(item["Tagname"]);
                        setting.KeyValue = Convert.ToString(item["keyvalue"]);
                        setting.Status = item["status"] != DBNull.Value ? Convert.ToInt32(item["status"]) : 0;
                        result.Add(setting);
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
                Application.Helper.LogException(ex, "Settings |  GetDetails()");
                return null;
            }
            finally
            {
                db.Close();
            }
        }
        /// <summary>
        ///  saving   General  setting
        /// </summary>
        /// <returns></returns>
        public OutputMessage SaveGeneralSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (String.IsNullOrWhiteSpace(this.DayLightSetting))
            {
                return new OutputMessage("Insert Daylight Saving", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (String.IsNullOrWhiteSpace(this.CurrencySymbolSetting))
            {
                return new OutputMessage("Insert Currency Symbol", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            using (DBManager db = new DBManager())
            {
                try
                {               
                    db.Open();
                    db.CreateParameters(3);
                    db.AddParameters(0, "@DayLightSetting", this.DayLightSetting);
                    db.AddParameters(1, "@AutoRoundOffSetting", this.AutoRoundOffSetting);
                    db.AddParameters(2, "@CurrencySymbolSetting", this.CurrencySymbolSetting);

                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=101)
                                      begin 
                                        update tbl_settings set KeyValue = @DayLightSetting where KeyID = 101 
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Daylight Saving',101,@DayLightSetting,1) 
                                    end";
                    //Saving Daylight
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);
                   
                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=105)
                                      begin 
                                update tbl_settings set KeyValue=@AutoRoundOffSetting where KeyID=105
                                    end else
                                      begin 
                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Auto RoundOff',105,@AutoRoundOffSetting,1)
                                        end";
                    //Saving Auto roundoff
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);
                 
                    

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=110)
                                      begin update tbl_settings set KeyValue=@CurrencySymbolSetting where KeyID=110 end else
                                      begin insert into tbl_settings(tagname,keyid,keyvalue,status) values('Currency_Symbol',110,@CurrencySymbolSetting,1) end";
                    //Saving Currency Symbol
                    if (db.ExecuteNonQuery(System.Data.CommandType.Text, query1)>0)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);

                    }

                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }
        /// <summary>
        ///   saving  General  Finantial setting
        /// </summary>
        /// <returns></returns>
        public OutputMessage SaveFinantialSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (String.IsNullOrWhiteSpace(this.ExpeneceGroupId))
            {
                return new OutputMessage("Enter an Expence Group Detail", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
           
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@FinantialYearSetting", this.FinantialYearSetting);
                    db.AddParameters(1, "@ExpeneceGroupId", this.ExpeneceGroupId);

                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=100)
                                      begin 
                                        update tbl_settings set KeyValue=@FinantialYearSetting where KeyID=100
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Financial Year',100,@FinantialYearSetting,1) 
                                    end";
                    //Saving Finantial Year
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=104)
                                      begin 
                                         update tbl_settings set KeyValue=@ExpeneceGroupId where KeyID=104
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Expense Group Id',104,@ExpeneceGroupId,1) 
                                    end";
                    //Saving Expence Group id
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }
        /// <summary>
        /// Testing connection
        /// </summary>
        /// <returns></returns>
        public OutputMessage TestConnection()
        {
             if (!this.EmailId.IsValidEmail())
            {
                return new OutputMessage("Enter a valid Email", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.EmailPassword))
            {
                return new OutputMessage("Enter a Password", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.HostSetting))
            {
                return new OutputMessage("Enter a Email Host", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.PortSetting))
            {
                return new OutputMessage("Enter a Email Port Id", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            try
            {
                MailMessage mail = new MailMessage(this.EmailId, this.EmailId);
                mail.Body = "Test Connection";
                SmtpClient smtp = new SmtpClient()
                {
                    Host = this.HostSetting,
                 
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(this.EmailId,this.EmailPassword),
                    Port = Convert.ToInt32(this.PortSetting)
                };
                smtp.Send(mail);
                return new OutputMessage("Connection Tested Successfully", true, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            catch (Exception ex)
            {
                return new OutputMessage(ex.Message, false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError,ex);

            }
        }
        /// <summary>
        ///   saving  General  Email setting
        /// </summary>
        /// <returns></returns>
        public OutputMessage SaveEmailSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (!this.EmailId.IsValidEmail())
            {
                return new OutputMessage("Enter a valid Email", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }      
            else if (string.IsNullOrWhiteSpace( this.EmailPassword))
            {
                return new OutputMessage("Enter a Password", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.HostSetting))
            {
                return new OutputMessage("Enter a Email Host", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (string.IsNullOrWhiteSpace(this.PortSetting))
            {
                return new OutputMessage("Enter a Email Port Id", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            using (DBManager db = new DBManager())
            {
                
                try
                {
                 
                    db.Open();
                 
                    db.CreateParameters(4);
                    db.AddParameters(0, "@EmailId", this.EmailId);
                    db.AddParameters(1, "@EmailPassword", this.EmailPassword);
                    db.AddParameters(2, "@HostSetting", this.HostSetting);
                    db.AddParameters(3, "@PortSetting", this.PortSetting);

                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=106)
                                      begin 
                                        update tbl_settings set KeyValue=@EmailId where KeyID=106
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Email_Id',106,@EmailId,1) 
                                    end";
                    //Saving Email Id
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=107)
                                      begin 
                                       update tbl_settings set KeyValue=@EmailPassword where KeyID=107
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Email_Password',107,@EmailPassword,1) 
                                    end";
                    //Saving Password
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=108)
                                      begin 
                                      update tbl_settings set KeyValue=@HostSetting where KeyID=108
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Email_Host',108,@HostSetting,1) 
                                    end";
                    //Saving Host
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=109)
                                      begin 
                                      update tbl_settings set KeyValue=@PortSetting where KeyID=109
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Email_Port',109,@PortSetting,1) 
                                    end";
                    //Saving Port
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }

        public OutputMessage SaveInvoiceTepmlateSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }

            using (DBManager db = new DBManager())
            {

                try
                {

                    db.Open();

                    db.CreateParameters(2);
                    db.AddParameters(0, "@InvoiceTemplateType", this.InvoiceTemplateType);
                    db.AddParameters(1, "@InvoiceTemplateNo", this.InvoiceTemplateNo);
              


                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=111)
                                      begin 
                                        update tbl_settings set KeyValue=@InvoiceTemplateType where KeyID=111
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Invoice_Template_Type',111,@InvoiceTemplateType,1) 
                                    end";
                    //Saving Template Type
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);


                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=112)
                                      begin 
                                       update tbl_settings set KeyValue=@InvoiceTemplateNo where KeyID=112
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Invoice_Template_Number',112,@InvoiceTemplateNo,1) 
                                    end";
                    //Saving Template Number
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError,ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }

        public OutputMessage SaveTermsSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }

            using (DBManager db = new DBManager())
            {

                try
                {

                    db.Open();

                    db.CreateParameters(26);
                    db.AddParameters(0, "@Sales_Order_TAndC", this.SalesOrderTC);
                    db.AddParameters(1, "@Sales_Invoice_TAndC", this.SalesInvoiceTC);
                    db.AddParameters(2, "@Sales_Return_TAndC", this.SalesReturnTC);
                    db.AddParameters(3, "@Purchase_Order_TAndC", this.PurchaseOrderTC);
                    db.AddParameters(4, "@Purchase_Invoice_TAndC", this.PurchaseInvoiceTC);
                    db.AddParameters(5, "@Purchase_Return_TAndC", this.PurchaseReturnTC);                  
                    db.AddParameters(6, "@Sales_Order_TAndCStatus", this.SalesOrderTCStatus);
                    db.AddParameters(7, "@Sales_Invoice_TAndCStatus", this.SalesInvoiceTCStatus);
                    db.AddParameters(8, "@Sales_Return_TAndCStatus", this.SalesReturnTCStatus);
                    db.AddParameters(9, "@Purchase_Order_TAndCStatus", this.PurchaseOrderTCStatus);
                    db.AddParameters(10, "@Purchase_Invoice_TAndCStatus", this.PurchaseInvoiceTCStatus);
                    db.AddParameters(11, "@Purchase_Return_TAndCStatus", this.PurchaseReturnTCStatus);
                    db.AddParameters(12, "@Purchase_Request_TAndCStatus", this.PurchaseRequestTCStatus);
                    db.AddParameters(13, "@Sales_Request_TAndCStatus", this.SalesRequestTCStatus);
                    db.AddParameters(14, "@Purchase_Request_TAndC", this.PurchaseRequestTC);
                    db.AddParameters(15, "@Sales_Request_TAndC", this.SalesRequestTC);
                    db.AddParameters(16, "@Purchase_Indent_TAndC", this.PurchaseIndentTC);
                    db.AddParameters(17, "@Purchase_Indent_TAndCStatus", this.PurchaseIndentTCStatus);
                    db.AddParameters(18, "@GRN_TAndC", this.GRNTC);
                    db.AddParameters(19, "@GRN_TAndCStatus", this.GRNTCStatus);
                    db.AddParameters(20, "@Transfer_Out_TC", this.TransferOutTC);
                    db.AddParameters(21, "@Transfer_Out_TCStatus", this.TransferOutTCStatus);
                    db.AddParameters(22, "@Transfer_In_TC", this.TransferInTC);
                    db.AddParameters(23, "@Transfer_In_TCStatus", this.TransferInTCStatus);
                    db.AddParameters(24, "@Damage_TC", this.DamageTC);
                    db.AddParameters(25, "@Damage_TCStatus", this.DamageTCStatus);
                    


                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=115)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Order_TAndC,status=@Sales_Order_TAndCStatus where KeyID=115
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Order_T&C',115,@Sales_Order_TAndC,@Sales_Order_TAndCStatus) 
                                    end";
                    //Saving so setting
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=128)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Request_TAndC,status=@Purchase_Request_TAndCStatus where KeyID=128
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Request_T&C',128,@Purchase_Request_TAndC,@Purchase_Request_TAndCStatus) 
                                    end";
                    //Saving purchase request setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=145)
                                      begin 
                                        update tbl_settings set KeyValue=@Transfer_Out_TC,status=@Transfer_Out_TCStatus where KeyID=145
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Transfer_Out_T&C',145,@Transfer_Out_TC,@Transfer_Out_TCStatus) 
                                    end";
                    //Saving transferout setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=146)
                                      begin 
                                        update tbl_settings set KeyValue=@Transfer_In_TC,status=@Transfer_In_TCStatus where KeyID=146
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Transfer_In_T&C',146,@Transfer_In_TC,@Transfer_In_TCStatus) 
                                    end";
                    //Saving transferin setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);
                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=147)
                                      begin 
                                        update tbl_settings set KeyValue=@Damage_TC,status=@Damage_TCStatus where KeyID=146
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Transfer_In_T&C',147,@Damage_TC,@Damage_TCStatus) 
                                    end";
                    //Saving damage setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=127)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Request_TAndC,status=@Sales_Request_TAndCStatus  where KeyID=127
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Request_T&C',127,@Sales_Request_TAndC,@Sales_Request_TAndCStatus) 
                                    end";
                    //Saving Sales request setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=116)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Invoice_TAndC,status=@Sales_Invoice_TAndCStatus where KeyID=116
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Invoice_T&C',116,@Sales_Invoice_TAndC,@Sales_Invoice_TAndCStatus) 
                                    end";
                    //Saving se setting      
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=117)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Return_TAndC,status=@Sales_Return_TAndCStatus where KeyID=117
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Return_T&C',117,@Sales_Return_TAndC,@Sales_Return_TAndCStatus) 
                                    end";
                    //Saving sr setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);


                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=118)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Order_TAndC,status=@Purchase_Order_TAndCStatus where KeyID=118
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Order_T&C',118,@Purchase_Order_TAndC,@Purchase_Order_TAndCStatus) 
                                    end";
                    //Saving po setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=119)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Invoice_TAndC,status=@Purchase_Invoice_TAndCStatus where KeyID=119
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Invoice_T&C',119,@Purchase_Invoice_TAndC,@Purchase_Invoice_TAndCStatus) 
                                    end";
                    //Saving pi setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=144)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Indent_TAndC,status=@Purchase_Indent_TAndCStatus where KeyID=144
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Indent_T&C',144,@Purchase_Indent_TAndC,@Purchase_Indent_TAndCStatus) 
                                    end";
                    //Saving grn setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=143)
                                      begin 
                                        update tbl_settings set KeyValue=@GRN_TAndC,status=@GRN_TAndCStatus where KeyID=143
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Goods Reciept Note T & C',143,@GRN_TAndC,@GRN_TAndCStatus) 
                                    end";
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=120)
                                      begin 
                                       update tbl_settings set KeyValue=@Purchase_Return_TAndC,status=@Purchase_Return_TAndCStatus where KeyID=120
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Return_T&C',120,@Purchase_Return_TAndC,@Purchase_Return_TAndCStatus) 
                                    end";
                    //Saving pr setting
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }

        public OutputMessage SaveOtherSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }

            using (DBManager db = new DBManager())
            {

                try
                {

                    db.Open();

                    db.CreateParameters(13);
                    db.AddParameters(0, "@EnableDescription", this.EnableDescription);
                    db.AddParameters(1, "@Discount", this.Discount);
                    db.AddParameters(2, "@AllowNegativeBiling", this.AllowNegativeBilling);
                    db.AddParameters(3, "@AllowPriceEditingInPurchaseRequest", this.AllowPriceEditingInPurchaseRequest);
                    db.AddParameters(4, "@AllowPriceEditInPurchaseQuote", this.AllowPriceEditInPurchaseQuote);
                    db.AddParameters(5, "@AllowPriceEditInPurchaseEntry", this.AllowPriceEditInPurchaseEntry);
                    db.AddParameters(6, "@AllowPriceEditInPurchaseReturn", this.AllowPriceEditInPurchaseReturn);
                    db.AddParameters(7, "@AllowPriceEditInSalesEntry", this.AllowPriceEditInSalesEntry);
                    db.AddParameters(8, "@AllowPriceEditInSalesQuote", this.AllowPriceEditInSalesQuote);
                    db.AddParameters(9, "@AllowPriceEditInSalesRequest", this.AllowPriceEditInSalesRequest);
                    db.AddParameters(10, "@AllowPriceEditInSalesReturn", this.AllowPriceEditInSalesReturn);
                    db.AddParameters(11, "@DisplayZeroQtyLookup", this.DisplayZeroQtyLookup);
                    db.AddParameters(12, "@DefaultCustomer", this.DefaultCustomer);
                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=135)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditingInPurchaseRequest where KeyID=135
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Purchase_Request',135,@AllowPriceEditingInPurchaseRequest,1) 
                                    end";
                    //Saving price edit in purchase request           
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=136)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInPurchaseQuote where KeyID=136
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Purchase_Quote',136,@AllowPriceEditInPurchaseQuote,1) 
                                    end";
                    //Saving price edit in purchase Quote          
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);
                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=137)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInPurchaseEntry where KeyID=137
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Purchase_Entry',137,@AllowPriceEditInPurchaseEntry,1) 
                                    end";
                    //Saving price edit in purchase Entry          
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=138)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInPurchaseReturn where KeyID=138
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Purchase_Return',138,@AllowPriceEditInPurchaseReturn,1) 
                                    end";
                    //Saving price edit in purchase return          
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=140)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInSalesEntry where KeyID=140
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Sales_Entry',140,@AllowPriceEditInSalesEntry,1) 
                                    end";
                    //Saving price edit in Sales entry  
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=139)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInSalesQuote where KeyID=139
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Sales_Order',139,@AllowPriceEditInSalesQuote,1) 
                                    end";
                    //Saving price edit in Sales Quote          
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=142)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInSalesRequest where KeyID=142
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Sales_Request',142,@AllowPriceEditInSalesRequest,1) 
                                    end";
                    //Saving price edit in Sales Request            
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=141)
                                      begin 
                                        update tbl_settings set KeyValue=@AllowPriceEditInSalesReturn where KeyID=141
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Price_Editing_In_Sales_Return',141,@AllowPriceEditInSalesReturn,1) 
                                    end";
                    //Saving price edit in Sales return           
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);
            
                     query1 = @"if exists(select * from TBL_SETTINGS where KeyID=113)
                                      begin 
                                        update tbl_settings set KeyValue=@EnableDescription where KeyID=113
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Enable_Description_In_Sales',113,@EnableDescription,1) 
                                    end";
                    //Saving Description in Sales           
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=114)
                                      begin 
                                        update tbl_settings set KeyValue=@Discount where KeyID=114
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Discount',114,@Discount,1) 
                                    end";
                    //Saving Discount in Sales           
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=132)
                                      begin 
                                       update tbl_settings set KeyValue=@AllowNegativeBiling where KeyID=132
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Allow_Negative_Billing',132,@AllowNegativeBiling,1) 
                                    end";
                    //Saving Negative billing setting
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=150)
                                      begin 
                                       update tbl_settings set KeyValue=@DefaultCustomer where KeyID=150
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Default_Customer_In_Sales',150,@DefaultCustomer,1) 
                                    end";
                    //Saving default customer setting
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=133)
                                      begin 
                                       update tbl_settings set KeyValue=@DisplayZeroQtyLookup where KeyID=133
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Show_Zero_Quantity_Item_In_Lookup',133,@DisplayZeroQtyLookup,1) 
                                    end";
                    //Saving Show_Zero_Quantity_Item_In_Lookup setting
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }
        public OutputMessage SaveDisclimerSettings()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Preferences, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);
            }

            using (DBManager db = new DBManager())
            {

                try
                {

                    db.Open();

                    db.CreateParameters(16);
                    db.AddParameters(0, "@Sales_Order_Disclimer", this.SalesOrderDisclimer);
                    db.AddParameters(1, "@Sales_Invoice_Disclimer", this.SalesInvoiceDisclimer);
                    db.AddParameters(2, "@Sales_Return_Disclimer", this.SalesReturnDisclimer);
                    db.AddParameters(3, "@Purchase_Order_Disclimer", this.PurchaseOrderDisclimer);
                    db.AddParameters(4, "@Purchase_Invoice_Disclimer", this.PurchaseInvoiceDisclimer);
                    db.AddParameters(5, "@Purchase_Return_Disclimer", this.PurchaseReturnDisclimer);
                    db.AddParameters(6, "@Sales_Order_DisclimerStatus", this.SalesOrderDisclimerStatus);
                    db.AddParameters(7, "@Sales_Invoice_DisclimerStatus", this.SalesInvoiceDisclimerStatus);
                    db.AddParameters(8, "@Sales_Return_DisclimerStatus", this.SalesReturnDisclimerStatus);
                    db.AddParameters(9, "@Purchase_Order_DisclimerStatus", this.PurchaseOrderDisclimerStatus);
                    db.AddParameters(10, "@Purchase_Invoice_DisclimerStatus", this.PurchaseInvoiceDisclimerStatus);
                    db.AddParameters(11, "@Purchase_Return_DisclimerStatus", this.PurchaseReturnDisclimerStatus);
                    db.AddParameters(12, "@Purchase_Request_DisclimerStatus", this.PurchaseRequestDisclimerStatus);
                    db.AddParameters(13, "@Purchase_Request_Disclimer", this.PurchaseRequestDisclimer);
                    db.AddParameters(14, "@Sales_Request_Disclimer", this.SalesRequestDisclimer);
                    db.AddParameters(15, "@Sales_Request_DisclimerStatus", this.SalesRequestDisclimerStatus);

                    string query1 = @"if exists(select * from TBL_SETTINGS where KeyID=121)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Order_Disclimer where KeyID=121
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Order_Disclimer',121,@Sales_Order_Disclimer,@Sales_Order_DisclimerStatus) 
                                    end";
                    //Saving so setting
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=129)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Request_Disclimer where KeyID=129
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Request_Disclimer',129,@Purchase_Request_Disclimer,@Purchase_Request_DisclimerStatus) 
                                    end";
                    //Saving purchase request setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);


                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=130)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Request_Disclimer where KeyID=130
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Request_Disclimer',130,@Sales_Request_Disclimer,@Sales_Request_DisclimerStatus) 
                                    end";
                    //Saving Sales request setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=122)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Invoice_Disclimer where KeyID=122
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Invoice_Disclimer',122,@Sales_Invoice_Disclimer,@Sales_Invoice_DisclimerStatus) 
                                    end";
                    //Saving se setting      
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=123)
                                      begin 
                                        update tbl_settings set KeyValue=@Sales_Return_Disclimer where KeyID=123
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Sales_Return_Disclimer',123,@Sales_Return_Disclimer,@Sales_Return_DisclimerStatus) 
                                    end";
                    //Saving sr setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);


                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=124)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Order_Disclimer where KeyID=124
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Order_Disclimer',124,@Purchase_Order_Disclimer,@Purchase_Order_DisclimerStatus) 
                                    end";
                    //Saving po setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=125)
                                      begin 
                                        update tbl_settings set KeyValue=@Purchase_Invoice_Disclimer where KeyID=125
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Invoice_Disclimer',125,@Purchase_Invoice_Disclimer,@Purchase_Invoice_DisclimerStatus) 
                                    end";
                    //Saving pe setting         
                    db.ExecuteNonQuery(System.Data.CommandType.Text, query1);

                    query1 = @"if exists(select * from TBL_SETTINGS where KeyID=126)
                                      begin 
                                       update tbl_settings set KeyValue=@Purchase_Return_Disclimer where KeyID=126
                                        end else
                                      begin 
                                    insert into tbl_settings(tagname,keyid,keyvalue,status) values('Purchase_Return_Disclimer',126,@Purchase_Return_Disclimer,Purchase_Return_DisclimerStatus) 
                                    end";
                    //Saving pr setting
                    if (Convert.ToInt32(db.ExecuteNonQuery(System.Data.CommandType.Text, query1)) >= 1 ? true : false)
                    {
                        return new OutputMessage("Successfully Saved", true, Type.NoError, "Setting | Update", System.Net.HttpStatusCode.OK);


                    }
                    else
                    {
                        return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong", false, Type.Others, "Setting | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {

                    db.Close();

                }

            }

        }
    }

    

}
