using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Application
{
    public class Task
    {
        public int TaskId { get; set; }
        public Job Job { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ImagesAsB64 { get; set; }
        public List<string> ImagesPath { get; set; }
        public string ImagePath { get; set; }
        public User CreatedBy { get; set; }
        public string Participants { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public string TimeLeft { get; set; }
        public List<Entities.Payroll.Employee> EmployeeList { get; set; }


        public OutputMessage Save()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    List<string> paths = new List<string>();
                    try
                    {
                        string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                        string fullPath = Path.Combine(folderPath, "Resources\\Tasks\\Images");
                        if (!Directory.Exists(fullPath))
                        {
                            Directory.CreateDirectory(fullPath);
                        }
                        foreach (string file in this.ImagesAsB64)
                        {
                            string guid = Guid.NewGuid().ToString();
                            string filePath = Path.Combine(fullPath, guid + ".jpeg");
                            paths.Add("/Resources/Tasks/Images/"+ guid + ".jpeg");
                            byte[] fileAsBinary = Convert.FromBase64String(file);
                            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                            {
                                fs.Write(fileAsBinary, 0, fileAsBinary.Length);
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    string query = @"insert into TBL_TASKS (Task_Title,Task_Descripton,Job_Id,Created_By,Created_Date,Start_Date,End_Date,Participants)
                                     values(@title, @description, @jobid, @userid, GETUTCDATE(),@startdate,@enddate,@Participants);select @@identity";
                    db.CreateParameters(7);
                    db.AddParameters(0, "@title", this.Title);
                    db.AddParameters(1, "@description", this.Description);
                    db.AddParameters(2, "@jobid", this.Job.ID);
                    db.AddParameters(3, "@userid", this.CreatedBy.ID);
                    if (this.StartDate == null)
                    {
                        db.AddParameters(4, "@startdate", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameters(4, "@startdate", this.StartDate);
                    }
                    if (this.EndDate == null)
                    {
                        db.AddParameters(5, "@enddate", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameters(5, "@enddate", this.EndDate);
                    }
                    db.AddParameters(6, "@Participants", this.Participants);
                    db.Open();
                    db.BeginTransaction();
                    int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    //Saving images
                    query = @"insert into TBL_TASK_ATTACHMENTS (Mime_Type,File_Path,Task_Id) values (@mimetype, @path, @taskid)";
                    foreach (string path in paths)
                    {
                        db.CleanupParameters();
                        db.CreateParameters(3);
                        db.AddParameters(0, "@mimetype", "image/jpeg");
                        db.AddParameters(1, "@path",path );
                        db.AddParameters(2, "@taskid",identity);
                        db.ExecuteNonQuery(System.Data.CommandType.Text,query);
                    }

                    db.CommitTransaction();
                    db.Close();
                    return new OutputMessage("Task added successfully", true, Type.NoError, "Task | Save", System.Net.HttpStatusCode.OK, new { TaskId = identity });
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Task | Save", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public OutputMessage AddImage()
        {
            using (DBManager db = new DBManager())
            {

                List<string> paths = new List<string>();
                try
                {
                    string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["RootAppFolder"].ToString();
                    string fullPath = Path.Combine(folderPath, "Resources\\Tasks\\Images");
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    foreach (string file in this.ImagesAsB64)
                    {
                        string guid = Guid.NewGuid().ToString();
                        string filePath = Path.Combine(fullPath, guid + ".jpeg");
                        paths.Add("/Resources/Tasks/Images/" + guid + ".jpeg");
                        byte[] fileAsBinary = Convert.FromBase64String(file);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            fs.Write(fileAsBinary, 0, fileAsBinary.Length);
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                    }
                  string  query = @"insert into TBL_TASK_ATTACHMENTS (Mime_Type,File_Path,Task_Id) values (@mimetype, @path, @taskid)";
                    foreach (string path in paths)
                    {
                        db.CleanupParameters();
                        db.CreateParameters(3);
                        db.AddParameters(0, "@mimetype", "image/jpeg");
                        db.AddParameters(1, "@path", path);
                        db.AddParameters(2, "@taskid", this.TaskId);
                        db.Open();
                        db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                    }
                    return new OutputMessage("Image added successfully", true, Type.NoError, "Task | AddImage", System.Net.HttpStatusCode.OK);

                }
                catch (Exception ex)
                {
                    return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Task | AddImage", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

                
            
        

        public OutputMessage Update()
        {
            using (DBManager db = new DBManager())
            {
                try
                {
                    
                    string query = @"update TBL_TASKS set Task_Title=@Task_Title,Task_Descripton=@Task_Descripton,Job_Id=@Job_Id,
                                   Modified_By=@Modified_By,Modified_Date=GETUTCDATE(),Start_Date=@Start_Date,End_Date=@End_Date,
                                   Participants=@Participants where Task_Id=@Id";
                    db.CreateParameters(8);
                    db.AddParameters(0, "@Task_Title", this.Title);
                    db.AddParameters(1, "@Task_Descripton", this.Description);
                    db.AddParameters(2, "@Job_Id", this.Job.ID);
                    db.AddParameters(3, "@Modified_By", this.CreatedBy.ID);
                    db.AddParameters(4, "@Id", this.TaskId);
                    if (this.StartDate == null)
                    {
                        db.AddParameters(5, "@Start_Date", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameters(5, "@Start_Date", this.StartDate);
                    }
                    if (this.EndDate == null)
                    {
                        db.AddParameters(6, "@End_Date", DBNull.Value);
                    }
                    else
                    {
                        db.AddParameters(6, "@End_Date", this.EndDate);
                    }
                    db.AddParameters(7, "@Participants", this.Participants);
                    db.Open();
                    db.BeginTransaction();
                    int identity = Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query));
                    db.CommitTransaction();
                    db.Close();
                    return new OutputMessage("Task Updated successfully", true, Type.NoError, "Task | Update", System.Net.HttpStatusCode.OK, new { TaskId = identity });
                }
                catch (Exception ex)
                {
                    db.RollBackTransaction();
                    return new OutputMessage("Something went wrong. Try again later", false, Type.Others, "Task | Update", System.Net.HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public OutputMessage UpdateStatus()
        {
            
            DBManager db = new DBManager();
            try
            {
                    
               string query = @"update TBL_TASKS set Status=@Status
								   where Task_Id=@Id";

                    db.CreateParameters(2);
                    db.AddParameters(0, "@Status", this.Status);
                    db.AddParameters(1, "@Id", this.TaskId);
                db.Open();
                db.ExecuteNonQuery(System.Data.CommandType.Text, query);
                return new OutputMessage("Status updated successfully", true, Type.NoError, "Task| UpdateStatus", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new OutputMessage("Something went wrong. Status could not be updated", false, Type.Others, "Task | UpdateStatus", System.Net.HttpStatusCode.InternalServerError, ex);
            }
            finally
            {
                db.Close();
            }
        }

        public OutputMessage Delete()
        {
             if (this.TaskId == 0)
            {
                return new OutputMessage("Select a task for deletion", false, Type.RequiredFields, "Task| Delete", System.Net.HttpStatusCode.InternalServerError);
            }
            else
            {
                DBManager db = new DBManager();
                try
                {

                    string query = "delete from TBL_TASKS where Task_Id=@id";
                    db.CreateParameters(1);
                    db.AddParameters(0, "@id", this.TaskId);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Task has been deleted", true, Type.NoError, "Task | Delete", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    dynamic Exception = ex;
                    if (ex.GetType().GetProperty("Number") != null)
                    {
                        if (Exception.Number == 547)
                        {
                            return new OutputMessage("You cannot delete this job because it is referenced in other transactions", false, Entities.Type.ForeignKeyViolation, "Task | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                        }
                        else
                        {
                            return new OutputMessage("Something went wrong. Task could not be deleted", false, Type.RequiredFields, "Task | Delete", System.Net.HttpStatusCode.InternalServerError, ex);

                        }
                    }
                    else
                    {
                        return new OutputMessage("Something went wrong. Task could not be deleted", false, Type.Others, "Task | Delete", System.Net.HttpStatusCode.InternalServerError, ex);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
        }


        public OutputMessage DeleteImage()
        {
             DBManager db = new DBManager();
                try
                {
                    string query = "delete from TBL_TASK_ATTACHMENTS where Task_Id=@id and File_Path=@Path";
                    db.CreateParameters(2);
                    db.AddParameters(0, "@id", this.TaskId);
                    db.AddParameters(1, "@Path", this.ImagePath);
                    db.Open();
                    db.ExecuteNonQuery(CommandType.Text, query);
                    return new OutputMessage("Image has been deleted", true, Type.NoError, "Task | DeleteImage", System.Net.HttpStatusCode.OK);
                }
                catch (Exception ex)
                {

                Application.Helper.LogException(ex, "Task | DeleteImage()");
                return null;
               }
                finally
                {
                    db.Close();
                }
            }

        public static Task GetTask(int TaskId)
        {

            DBManager db = new DBManager();
            try
            {
                #region Query
                string query = @"select t.Task_Id,t.Task_Title,t.Task_Descripton,t.Participants,
                               t.Start_Date,t.End_Date,t.Job_Id,t.Status
                               from TBL_TASKS t
                               where t.Task_Id=@Id";
                #endregion Query
                db.CreateParameters(1);
                db.AddParameters(0, "@Id", TaskId);
                db.Open();
                DataTable dt = db.ExecuteQuery(CommandType.Text, query);
                if (dt != null)
                {

                    DataRow row = dt.Rows[0];
                    Task task = new Task();
                    task.TaskId = Convert.ToInt32(row["Task_Id"]);
                    task.Status =row["Status"]!=DBNull.Value? Convert.ToInt32(row["Status"]):0;
                    task.Title = Convert.ToString(row["Task_Title"]);
                    task.Description = Convert.ToString(row["Task_Descripton"]);
                    task.StartDate = row["Start_Date"] != DBNull.Value ? Convert.ToDateTime(row["Start_Date"]) : (DateTime?)null;
                    task.EndDate = row["End_Date"] != DBNull.Value ? Convert.ToDateTime(row["End_Date"]) : (DateTime?)null;
                    task.StartDateString = row["Start_Date"] != DBNull.Value ? Convert.ToDateTime(row["Start_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    task.EndDateString = row["End_Date"] != DBNull.Value ? Convert.ToDateTime(row["End_Date"]).ToString("dd/MMM/yyyy") : string.Empty;
                    return task;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Task | GetTask(int TaskId)");
                return null;
            }
        }
    }
}
