using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Application
{
    public class Job
    {
        #region properties
        public int ID { get; set; }
        public string JobName { get; set; }
        public int CustomerId { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Status { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int TotalTasks { get; set; }
        public int TotalCompletedTasks { get; set; }
        public int TotalInvoices { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string CompletedDateString { get; set; }
        public string StartDateString { get; set; }
        public decimal EstimatedAmount { get; set; }
        public string SiteAddress { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string SiteContactName { get; set; }
        public string ContactPhone1 { get; set; }
        public string ContactPhone2 { get; set; }
        public string SiteContactPhone1 { get; set; }
        public string SiteContactPhone2 { get; set; }
        public string ContactAddress2 { get; set; }
        public string SiteContactAddress2 { get; set; }
        public string ContactCity { get; set; }
        public string SiteContactCity { get; set; }
        public string StateId { get; set; }
        public string SiteStateId { get; set; }
        public string CountryId { get; set; }
        public string SiteCountryId { get; set; }
        public string ZipCode { get; set; }
        public string SiteZipCode { get; set; }
        public string Email { get; set; }
        public string Salutation { get; set; }
        public string SiteSalutation { get; set; }
        public string SiteEmail { get; set; }
        public string Country { get; set; }
        public string SiteCountry { get; set; }
        public string State { get; set; }
        public string SiteState { get; set; }
        public decimal TotalInvoiceAmount { get; set; }
        #endregion properties

        #region Functions
        /// <summary>
        ///  saving  jobs
        /// </summary>
        /// <returns></returns>
        public OutputMessage Save()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.CreatedBy, Security.BusinessModules.Jobs, Security.PermissionTypes.Create))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Jobs | Save", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (String.IsNullOrWhiteSpace(this.JobName))
            {
                return new OutputMessage("Insert a Job Name", false, Type.Others, "Jobs | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (!Helper.IsValidDate(this.StartDateString))
            {
                return new OutputMessage("Insert job start date", false, Type.Others, "Jobs | Save", System.Net.HttpStatusCode.InternalServerError);

            }
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    db.CreateParameters(30);
                    db.AddParameters(0, "@JobName", this.JobName);
                    db.AddParameters(1, "@CustomerId", this.CustomerId);
                    db.AddParameters(2, "@Status", this.Status);
                    db.AddParameters(3, "@CompanyId", this.CompanyId);
                    db.AddParameters(4, "@CreatedBy", this.CreatedBy);
                    db.AddParameters(5, "@Contact_Name", this.ContactName);
                    db.AddParameters(6, "@Site_Contact_Name", this.SiteContactName);
                    db.AddParameters(7, "@Site_Contact_Phone", this.SiteContactPhone1);
                    db.AddParameters(8, "@Contact_Phone1", this.ContactPhone1);
                    db.AddParameters(9, "@Contact_Phone2", this.ContactPhone2);
                    db.AddParameters(10, "@Site_Contact_Phone2", this.SiteContactPhone2);
                    db.AddParameters(11, "@Contact_Address1", this.ContactAddress);
                    db.AddParameters(12, "@Contact_Address2", this.ContactAddress2);
                    db.AddParameters(13, "@Site_Contact_Address1", this.SiteAddress);
                    db.AddParameters(14, "@Site_Contact_Address2", this.SiteContactAddress2);
                    db.AddParameters(15, "@Contact_City", this.ContactCity);
                    db.AddParameters(16, "@Site_Contact_City", this.SiteContactCity);
                    db.AddParameters(17, "@State_Id", this.StateId);
                    db.AddParameters(18, "@Site_State_Id", this.SiteStateId);
                    db.AddParameters(19, "@Country_Id", this.CountryId);
                    db.AddParameters(20, "@Site_Country_Id", this.SiteCountryId);
                    db.AddParameters(21, "@ZipCode", this.ZipCode);
                    db.AddParameters(22, "@Site_ZipCode", this.SiteZipCode);
                    db.AddParameters(23, "@Email", this.Email);
                    db.AddParameters(24, "@Site_Email", this.SiteEmail);
                    db.AddParameters(25, "@Salutation", this.Salutation);
                    db.AddParameters(26, "@Start_Date", this.StartDate);
                    db.AddParameters(27, "@EstimatedAmount", this.EstimatedAmount);
                    db.AddParameters(28, "@Completed_Date", this.CompletedDate);
                    db.AddParameters(29, "@Site_Salutation", this.SiteSalutation);
                    #region query
                    string query1 = @"insert into [TBL_JOB_MST]([Job_Name],[Customer_Id],[Status],[Company_Id],[Created_By],
                                    [Created_Date],[Start_Date],[Estimated_Amount],[Completed_Date],[Contact_Name],[Site_Contact_Name],[Site_Contact_Phone],
                                    [Contact_Phone1],[Contact_Phone2],[Site_Contact_Phone2],[Contact_Address1],[Contact_Address2],
                                    [Site_Contact_Address1],[Site_Contact_Address2],[Contact_City],[Site_Contact_City],
                                    [State_Id],[Site_State_Id], [Country_Id],[Site_Country_Id],[ZipCode],[Site_ZipCode],
                                    [Email],[Site_Email],[Salutation],[Site_Salutation])  
                                    values(@JobName,@CustomerId,@Status,@CompanyId,@CreatedBy,getutcdate(),@Start_Date,@EstimatedAmount,@Completed_Date,
                                    @Contact_Name,@Site_Contact_Name,@Site_Contact_Phone,@Contact_Phone1,@Contact_Phone2,
                                    @Site_Contact_Phone2,@Contact_Address1,@Contact_Address2,@Site_Contact_Address1,@Site_Contact_Address2,
                                    @Contact_City,@Site_Contact_City,@State_Id,@Site_State_Id,@Country_Id,@Site_Country_Id,
                                    @ZipCode,@Site_ZipCode,@Email,@Site_Email,@Salutation,@Site_Salutation);select @@identity";
                    #endregion query
                    int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query1));
                    DataTable dt = db.ExecuteQuery(CommandType.Text, "select Job_Id from TBL_JOB_MST where Job_Id=" + identity);
                  return new OutputMessage("Job successfully saved", true, Type.NoError, "Jobs | Save", System.Net.HttpStatusCode.OK,new { Id = dt.Rows[0]["Job_Id"] });
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. Job could not be saved", false, Type.Others, "Jobs | Save", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        ///  saving   General  setting
        /// </summary>
        /// <returns></returns>
        public OutputMessage Update()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Jobs, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            else if (String.IsNullOrWhiteSpace(this.JobName))
            {
                return new OutputMessage("Insert a job name", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            else if (!Helper.IsValidDate(this.StartDateString))
            {
                return new OutputMessage("Insert job start date", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);

            }
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    db.CreateParameters(29);
                    db.AddParameters(0, "@JobName", this.JobName);
                    db.AddParameters(1, "@ModifiedBy", this.ModifiedBy);
                    db.AddParameters(2, "@Contact_Name", this.ContactName);
                    db.AddParameters(3, "@Site_Contact_Name", this.SiteContactName);
                    db.AddParameters(4, "@Site_Contact_Phone", this.SiteContactPhone1);
                    db.AddParameters(5, "@Contact_Phone1", this.ContactPhone1);
                    db.AddParameters(6, "@Contact_Phone2", this.ContactPhone2);
                    db.AddParameters(7, "@Site_Contact_Phone2", this.SiteContactPhone2);
                    db.AddParameters(8, "@Contact_Address1", this.ContactAddress);
                    db.AddParameters(9, "@Contact_Address2", this.ContactAddress2);
                    db.AddParameters(10, "@Site_Contact_Address1", this.SiteAddress);
                    db.AddParameters(11, "@Site_Contact_Address2", this.SiteContactAddress2);
                    db.AddParameters(12, "@Contact_City", this.ContactCity);
                    db.AddParameters(13, "@Site_Contact_City", this.SiteContactCity);
                    db.AddParameters(14, "@State_Id", this.StateId);
                    db.AddParameters(15, "@Site_State_Id", this.SiteStateId);
                    db.AddParameters(16, "@Country_Id", this.CountryId);
                    db.AddParameters(17, "@Site_Country_Id", this.SiteCountryId);
                    db.AddParameters(18, "@ZipCode", this.ZipCode);
                    db.AddParameters(19, "@Site_ZipCode", this.SiteZipCode);
                    db.AddParameters(20, "@Email", this.Email);
                    db.AddParameters(21, "@Site_Email", this.SiteEmail);
                    db.AddParameters(22, "@Salutation", this.Salutation);
                    db.AddParameters(23, "@StartDate", this.StartDate);
                    db.AddParameters(24, "@EstimatedAmount", this.EstimatedAmount);
                    db.AddParameters(25, "@ID", this.ID);
                    db.AddParameters(26, "@Completed_Date", this.CompletedDate);
                    db.AddParameters(27, "@Site_Salutation", this.SiteSalutation);
                    db.AddParameters(28, "@Customer_Id", this.CustomerId);
                    #region query
                    string query1 = @"update [TBL_JOB_MST] set [Job_Name]=@JobName,Customer_Id=@Customer_Id,[Modified_By]=@ModifiedBy,[Modified_Date]=getutcdate(),
                                    [Start_Date]=@StartDate,[Estimated_Amount]=@EstimatedAmount,Completed_Date=@Completed_Date,Contact_Name=@Contact_Name,Site_Contact_Name=@Site_Contact_Name,
                                    Site_Contact_Phone=@Site_Contact_Phone,Contact_Phone1=@Contact_Phone1,Contact_Phone2=@Contact_Phone2,
                                    Site_Contact_Phone2=@Site_Contact_Phone2,Contact_Address1=@Contact_Address1,Contact_Address2=@Contact_Address2,
                                    Site_Contact_Address1=@Site_Contact_Address1,Site_Contact_Address2=@Site_Contact_Address2,Contact_City=@Contact_City,
                                    Site_Contact_City=@Site_Contact_City,State_Id=@State_Id,Site_State_Id=@Site_State_Id,Country_Id=@Country_Id,
                                    Site_Country_Id=@Site_Country_Id,ZipCode=@ZipCode,Site_ZipCode=@Site_ZipCode,Email=@Email,
                                    Site_Email=@Site_Email,Salutation=@Salutation,Site_Salutation=@Site_Salutation
                                    where job_id=@ID";
                    #endregion query
                    if (db.ExecuteNonQuery(System.Data.CommandType.Text, query1) > 0)
                    {
                        return new OutputMessage(" Job successfully updated", true, Type.NoError, "Jobs | Update", System.Net.HttpStatusCode.OK);
                    }

                    else
                    {
                        return new OutputMessage("Something went wrong. Job could not be updated", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. Job could not be updated", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError, ex);

                }
                finally
                {
                    db.Close();
                }
            }
        }
        /// <summary>
        /// return job details customerwise
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public static List<Job> GetDetails(int Customer_Id)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select Job_Id,job_name,Status,Contact_Address1,Contact_Address2,Site_Contact_Address1,Site_Contact_Address2,Contact_Name,
                                Start_Date,Estimated_Amount,status from TBL_JOB_MST where Customer_Id=@Customer_Id order by Created_Date desc";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@Customer_Id", Customer_Id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {
                    List<Job> result = new List<Job>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];
                        Job job = new Job();
                        job.ID = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                        job.JobName = Convert.ToString(row["job_name"]);
                        job.SiteAddress = Convert.ToString(row["Site_Contact_Address1"]);
                        job.Status = Convert.ToInt32(row["status"]);
                        job.ContactName = Convert.ToString(row["Contact_Name"]);
                        job.ContactAddress = Convert.ToString(row["Contact_Address1"]);
                        job.EstimatedAmount = Convert.ToDecimal(row["Estimated_Amount"]);
                        job.StartDateString = row["Start_Date"] != DBNull.Value ? Convert.ToDateTime(row["Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        result.Add(job);
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
                Application.Helper.LogException(ex, "Jobs | GetDetails(int Customer_Id)");
                return null;
            }
        }

        public static List<Task> GetTasks(int JobId)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    string query = @"declare @i int=1
                                   declare @temtable table(id int,participants varchar(50),TaskID int)
                                   declare @count int
                                   declare @EMP_STRING VARCHAR(100)
                                   declare @EmpiDtable table(EMP_ID int,TaskID int)
                                   declare @ID int
                                   insert into @temtable select ROW_NUMBER() over(order by task_id) id,Participants,Task_Id from TBL_TASKS where Job_Id=@job_Id
                                   select @count=COUNT(*) from @temtable
                                   while @i<=@count
                                   begin
                                   set @EMP_STRING=(select Participants from @temtable where id=@i) 
                                   Set @ID=(select taskid from @temtable where id=@i) 
                                   insert into @EmpiDtable(EMP_ID,TaskID) select Convert(INT,element),@ID from Split(@EMP_STRING,',')
                                   set @i=@i+1
                                   end
                                   ;with cte(FirstName,Photopath,empID,taskID)
                                   as
                                   (
                                   select First_Name,Photo_Path,Employee_Id,TaskID from TBL_EMPLOYEE_MST a inner join @EmpiDtable b on a.Employee_Id=b.EMP_ID
                                   )
                                   select t.Task_Id,t.Task_Title,t.status,t.job_Id,t.Task_Descripton,t.Participants,t.Start_Date,
                                   t.End_Date,j.[Job_Name],u.User_Name,u.Full_Name,u.Profile_Image_Path,u.PROFILE_IMAGE_PATH[Picture],t.Created_Date,at.file_path,c.FirstName,c.PhotoPath,c.empID
                                   from tbl_job_mst j
                                   inner join TBL_TASKS t on t.Job_Id=j.Job_Id
                                   inner join tbl_user_mst u on u.User_Id=t.Created_By
                                   left join TBL_TASK_ATTACHMENTS at on at.task_id=t.task_id
								   left join cte c on c.TaskID=t.Task_Id
                                   where t.Job_Id=@job_Id order by t.Created_Date desc";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@job_Id", JobId);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    List<Task> result = new List<Task>();
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count;)
                        {
                            DataRow item = dt.Rows[i];
                            Task task = new Task();
                            task.TaskId = item["Task_Id"] != DBNull.Value ? Convert.ToInt32(item["Task_Id"]) : 0;
                            task.Status = item["Status"] != DBNull.Value ? Convert.ToInt32(item["Status"]) : 0;
                            task.Title = item["Task_Title"].ToString();
                            task.Description = item["Task_Descripton"].ToString();
                            task.CreatedDate = Convert.ToDateTime(item["Created_Date"]);
                            task.CreatedBy = new User()
                            {
                                UserName = Convert.ToString(item["User_Name"]),
                                FullName = Convert.ToString(item["Full_Name"]),
                                ProfileImagePath = Convert.ToString(item["Picture"]),

                            };
                            task.StartDate = item["Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Start_Date"]) : (DateTime?)null;
                            task.EndDate = item["End_Date"] != DBNull.Value ? Convert.ToDateTime(item["End_Date"]) : (DateTime?)null;
                            task.StartDateString = item["Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Start_Date"]).ToString("dd/MMM/yyyy") : null;
                            task.EndDateString = item["End_Date"] != DBNull.Value ? Convert.ToDateTime(item["End_Date"]).ToString("dd/MMM/yyyy") : null;
                            task.TimeLeft = Helper.TimeLeft(task.CreatedDate);
                            DataTable imagesData = dt.AsEnumerable().Where(x => x.Field<int>("task_id") == task.TaskId).CopyToDataTable();
                            if (imagesData != null && imagesData.Rows.Count != 0)
                            {
                                task.ImagesPath = new List<string>();
                                List<Entities.Payroll.Employee> EmpList = new List<Payroll.Employee>();
                                foreach (DataRow img in imagesData.Rows)
                                {
                                    Entities.Payroll.Employee Emp = new Payroll.Employee();
                                    Emp.PhotoPath = Convert.ToString(img["PhotoPath"]);
                                    Emp.FirstName = Convert.ToString(img["FirstName"]);
                                    Emp.ID = img["empID"]!= DBNull.Value ?Convert.ToInt32(img["empID"]):0;
                                    task.ImagesPath.Add(Convert.ToString(img["file_path"]));
                                    EmpList.Add(Emp);
                                    dt.Rows.RemoveAt(0);
                                }
                                task.EmployeeList = EmpList;
                            }
                            result.Add(task);
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
                    Application.Helper.LogException(ex, "Job |   GetTasks(int JobId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static Job GetCustomerwiseJob(int JobId)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    #region Query
                    string query = @"select j.Job_Id,j.Job_Name,isnull(j.Customer_Id,0)[Customer_Id],j.Status,
                                   isnull(j.Company_Id,0)[Company_Id],c.Name[Customer],
                                   c.Address1[Cust_Address],c.Phone1[Cust_Phone]
                                   from TBL_JOB_MST j
                                   left join TBL_CUSTOMER_MST c on c.Customer_Id=j.Customer_Id
                                   where j.Job_Id=@Id";
                    #endregion Query
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Id", JobId);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    if (dt != null)
                    {
                        DataRow row = dt.Rows[0];
                        Job job = new Job();
                        job.ID = Convert.ToInt32(row["Job_Id"]);
                        job.CustomerId = Convert.ToInt32(row["Customer_Id"]);
                        job.JobName = Convert.ToString(row["Job_Name"]);
                        job.Customer = Convert.ToString(row["Customer"]);
                        job.CustomerAddress = Convert.ToString(row["Cust_Address"]);
                        job.CustomerPhone = Convert.ToString(row["Cust_Phone"]);
                        return job;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Jobs | GetCustomerwiseJob(int JobId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// return single job 
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public static Job GetJob(int job_id)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select Job_Id,isnull(j.status,0)[Status],job_name,Site_Contact_Address1,Site_Contact_Address2,cus.Name[Customer],j.Start_Date,j.Completed_Date,
                                cus.Address1[Cust_Address1],cus.Phone1[Cust_phone],cus.Customer_Id,j.Contact_Name,
                                (select count(*) from TBL_SALES_ENTRY_REGISTER where Job_Id=@jobid)[NO_OF_INVOICES],
                                (select count(*) from TBL_TASKS where Job_Id=@jobid)[TOTAL_TASKS],
                                (select count(*) from TBL_TASKS where Job_Id=@jobid and status=2)[Completed_tasks],
								(select  sum(net_amount) from TBL_SALES_ENTRY_REGISTER where Job_Id=@jobid)[Total_Invoice_Amount],
                                Contact_Address1,Contact_Address2,[Start_Date],Estimated_Amount,Contact_City,
								Contact_Phone1,Contact_Phone2,Site_Contact_City,Site_Contact_Name,
								Site_Contact_Phone,Site_Contact_Phone2,j.Country_Id,Site_Country_Id,j.State_Id,Site_Email,Site_State_Id,Site_ZipCode,
								j.Email,j.ZipCode,j.Salutation,j.Site_Salutation,s.Name[State],c.Name[Country],st.Name[Site_State],co.Name[Site_Country]
							    from TBL_JOB_MST j
								inner join TBL_CUSTOMER_MST cus on cus.Customer_Id=j.Customer_Id
                                left join TBL_COUNTRY_MST c on c.Country_Id=j.Country_Id
                                left join TBL_COUNTRY_MST co on co.Country_Id=j.Site_Country_Id
								left join TBL_STATE_MST s on s.State_Id=j.State_Id
								left join TBL_STATE_MST st on st.State_Id=j.Site_State_Id
								where job_id=@jobid";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@jobid", job_id);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    Job job = new Job();
                    job.JobName = Convert.ToString(row["job_name"]);
                    job.Status = Convert.ToInt32(row["status"]);
                    job.SiteAddress = Convert.ToString(row["Site_Contact_Address1"]);
                    job.SiteContactAddress2 = Convert.ToString(row["Site_Contact_Address2"]);
                    job.ContactName = Convert.ToString(row["Contact_Name"]);
                    job.SiteSalutation = Convert.ToString(row["Site_Salutation"]);
                    job.SiteContactName = Convert.ToString(row["Site_Contact_Name"]);
                    job.ContactCity = Convert.ToString(row["Contact_City"]);
                    job.SiteContactCity = Convert.ToString(row["Site_Contact_City"]);
                    job.ContactPhone1 = Convert.ToString(row["Contact_Phone1"]);
                    job.ContactPhone2 = Convert.ToString(row["Contact_Phone2"]);
                    job.SiteContactPhone1 = Convert.ToString(row["Site_Contact_Phone"]);
                    job.SiteContactPhone2  = Convert.ToString(row["Site_Contact_Phone2"]);
                    job.SiteCountryId  = Convert.ToString(row["Site_Country_Id"]);
                    job.CountryId  = Convert.ToString(row["Country_Id"]);
                    job.SiteEmail  = Convert.ToString(row["Site_Email"]);
                    job.SiteStateId  = Convert.ToString(row["Site_State_Id"]);
                    job.StateId  = Convert.ToString(row["State_Id"]);
                    job.SiteZipCode  = Convert.ToString(row["Site_ZipCode"]);
                    job.Email  = Convert.ToString(row["Email"]);
                    job.Country = Convert.ToString(row["Country"]);
                    job.SiteCountry = Convert.ToString(row["Site_Country"]);
                    job.State = Convert.ToString(row["State"]);
                    job.SiteState = Convert.ToString(row["Site_State"]);
                    job.ZipCode = Convert.ToString(row["ZipCode"]);
                    job.Salutation = Convert.ToString(row["Salutation"]);
                    job.Customer = Convert.ToString(row["Customer"]);
                    job.CustomerAddress = Convert.ToString(row["Cust_Address1"]);
                    job.CustomerPhone = Convert.ToString(row["Cust_phone"]);
                    job.CustomerId = row["Customer_Id"] != DBNull.Value ? Convert.ToInt32(row["Customer_Id"]) : 0;
                    job.ContactAddress = Convert.ToString(row["Contact_Address1"]);
                    job.ContactAddress2 = Convert.ToString(row["Contact_Address2"]);
                    job.EstimatedAmount = row["Estimated_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Estimated_Amount"]) : 0;
                    job.StartDate = row["Start_Date"] != DBNull.Value ? Convert.ToDateTime(row["Start_Date"]) : (DateTime?)null;
                    job.CompletedDate = row["Completed_Date"] != DBNull.Value ? Convert.ToDateTime(row["Completed_Date"]) : (DateTime?)null;
                    job.StartDateString = row["Start_Date"] != DBNull.Value ? Convert.ToDateTime(row["Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    job.CompletedDateString = row["Completed_Date"] != DBNull.Value ? Convert.ToDateTime(row["Completed_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    job.ID = row["Job_Id"] != DBNull.Value ? Convert.ToInt32(row["Job_Id"]) : 0;
                    job.TotalCompletedTasks = row["Completed_tasks"] != DBNull.Value ? Convert.ToInt32(row["Completed_tasks"]) : 0;
                    job.TotalTasks = row["TOTAL_TASKS"] != DBNull.Value ? Convert.ToInt32(row["TOTAL_TASKS"]) : 0;
                    job.TotalInvoices = row["NO_OF_INVOICES"] != DBNull.Value ? Convert.ToInt32(row["NO_OF_INVOICES"]) : 0;
                    job.TotalInvoiceAmount = row["Total_Invoice_Amount"] != DBNull.Value ? Convert.ToDecimal(row["Total_Invoice_Amount"]) : 0;
                    return job;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Jobs | GetDetails(int Customer_Id)");
                return null;
            }
        }
        /// <summary>
        /// Used for delete the job
        /// </summary>
        /// <returns></returns>
        public OutputMessage Delete()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Jobs, Security.PermissionTypes.Delete))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "JOBS | Delete", System.Net.HttpStatusCode.InternalServerError);

            }

            else if (this.ID == 0)
            {
                return new OutputMessage("Select a job for deletion", false, Type.RequiredFields, "JOBS| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_JOB_MST where job_id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.ID);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Job has been deleted", true, Type.NoError, "JOBS | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this job because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "JOBS | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Job could not be deleted", false, Type.RequiredFields, "JOBS | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. Job could not be deleted", false, Type.Others, "JOBS | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// update the job status
        /// </summary>
        /// <returns></returns>
        public OutputMessage UpdateStatus()
        {
            if (!Entities.Security.Permissions.AuthorizeUser(this.ModifiedBy, Security.BusinessModules.Jobs, Security.PermissionTypes.Update))
            {
                return new OutputMessage("Limited Access. Contact Administrator", false, Type.InsufficientPrivilege, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);
            }
            using (DBManager db = new DBManager())
            {
                try
                {
                    db.Open();
                    db.CreateParameters(2);
                    db.AddParameters(0, "@ID", this.ID);
                    db.AddParameters(1, "@Status", this.Status);
                    string query = "update TBL_JOB_MST set status=@Status where job_id=@ID";
                    if (db.ExecuteNonQuery(System.Data.CommandType.Text, query) > 0)
                    {
                        return new OutputMessage("Job successfully updated", true, Type.NoError, "Jobs | Update", System.Net.HttpStatusCode.OK);

                    }

                    else
                    {
                        return new OutputMessage("Something went wrong.Job could not be updated", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);

                    }
                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong.Job could not be updated", false, Type.Others, "Jobs | Update", System.Net.HttpStatusCode.InternalServerError);

                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static DataTable GetJobs(int CompanyId)
        {

            using (DBManager db = new DBManager())
            {

                try
                {
                    db.Open();
                    string query = "SELECT Job_Id,Job_Name FROM TBL_JOB_MST where Company_Id=@Company_Id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Company_Id", CompanyId);
                    return db.ExecuteQuery(CommandType.Text, query);

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Job | GetJobs(int CompanyId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static DataTable GetCustomer(int companyID)
        {
            try
            {
                DBManager db = new DBManager();
                db.Open();
                string cmd = @"select Customer_Id,Name from TBL_CUSTOMER_MST where Company_Id=" + companyID;
                return db.ExecuteQuery(CommandType.Text, cmd);
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Job | GetCustomer(int companyID)");
                return null;
            }

        }

        public static DataTable GetJobsForVoucher(int CompanyID)
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    #region Query
                    string query = @"select j.Job_Id,j.Job_Name,isnull(j.Customer_Id,0)[Customer_Id],j.Status,
                                   isnull(j.Company_Id,0)[Company_Id],c.Name[Customer],
                                   c.Address1[Cust_Address],c.Phone1[Cust_Phone]
                                   from TBL_JOB_MST j
                                   left join TBL_CUSTOMER_MST c on c.Customer_Id=j.Customer_Id
                                   where j.Company_Id=@Id";
                    #endregion Query
                    db.CreateParameters(1);
                    db.AddParameters(0, "@Id", CompanyID);
                    db.Open();
                    DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                    return dt;

                }
                catch (Exception ex)
                {
                    Application.Helper.LogException(ex, "Jobs | GetCustomerwiseJob(int JobId)");
                    return null;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public static List<Job> GetJobList(int CompanyId)
        {
            DBManager db = new DBManager();
            try
            {
                db.Open();
                string query = @"select j.*,c.Name[Customer] from TBL_JOB_MST j
                               left join TBL_CUSTOMER_MST c on c.Customer_Id=j.Customer_Id
                               where j.Company_Id=@Company_Id order by j.Created_Date desc";
                db.CreateParameters(1);
                db.AddParameters(0, "@Company_Id", CompanyId);
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                List<Job> result = new List<Job>();
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Job job = new Job();
                        job.JobName = Convert.ToString(item["job_name"]);
                        job.Status = Convert.ToInt32(item["status"]);
                        job.SiteAddress = Convert.ToString(item["Site_Contact_Address1"]);
                        job.SiteContactAddress2 = Convert.ToString(item["Site_Contact_Address2"]);
                        job.ContactName = Convert.ToString(item["Contact_Name"]);
                        job.SiteSalutation = Convert.ToString(item["Site_Salutation"]);
                        job.SiteContactName = Convert.ToString(item["Site_Contact_Name"]);
                        job.ContactCity = Convert.ToString(item["Contact_City"]);
                        job.SiteContactCity = Convert.ToString(item["Site_Contact_City"]);
                        job.ContactPhone1 = Convert.ToString(item["Contact_Phone1"]);
                        job.ContactPhone2 = Convert.ToString(item["Contact_Phone2"]);
                        job.SiteContactPhone1 = Convert.ToString(item["Site_Contact_Phone"]);
                        job.SiteContactPhone2 = Convert.ToString(item["Site_Contact_Phone2"]);
                        job.SiteCountryId = Convert.ToString(item["Site_Country_Id"]);
                        job.CountryId = Convert.ToString(item["Country_Id"]);
                        job.SiteEmail = Convert.ToString(item["Site_Email"]);
                        job.SiteStateId = Convert.ToString(item["Site_State_Id"]);
                        job.StateId = Convert.ToString(item["State_Id"]);
                        job.SiteZipCode = Convert.ToString(item["Site_ZipCode"]);
                        job.Email = Convert.ToString(item["Email"]);
                        job.ZipCode = Convert.ToString(item["ZipCode"]);
                        job.Salutation = Convert.ToString(item["Salutation"]);
                        job.Customer = Convert.ToString(item["Customer"]);
                        //job.CustomerAddress = Convert.ToString(item["Cust_Address1"]);
                        //job.CustomerPhone = Convert.ToString(item["Cust_phone"]);
                        job.CustomerId = item["Customer_Id"] != DBNull.Value ? Convert.ToInt32(item["Customer_Id"]) : 0;
                        job.ContactAddress = Convert.ToString(item["Contact_Address1"]);
                        job.ContactAddress2 = Convert.ToString(item["Contact_Address2"]);
                        job.EstimatedAmount = item["Estimated_Amount"] != DBNull.Value ? Convert.ToDecimal(item["Estimated_Amount"]) : 0;
                        job.StartDate = item["Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Start_Date"]) : (DateTime?)null;
                        job.CompletedDate = item["Completed_Date"] != DBNull.Value ? Convert.ToDateTime(item["Completed_Date"]) : (DateTime?)null;
                        job.StartDateString = item["Start_Date"] != DBNull.Value ? Convert.ToDateTime(item["Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        job.CompletedDateString = item["Completed_Date"] != DBNull.Value ? Convert.ToDateTime(item["Completed_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                        job.ID = item["Job_Id"] != DBNull.Value ? Convert.ToInt32(item["Job_Id"]) : 0;
                        result.Add(job);
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
                Application.Helper.LogException(ex, "Lead |  GetJobList(int CompanyId)");
                return null;
            }
            finally
            {
                db.Close();
            }
        }

        #endregion Functions
    }
}
