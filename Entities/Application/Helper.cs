using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DBManager;
using System.IO;

namespace Entities.Application
{
    public static class Helper
    {
        public static bool IsValidDate(this string DateString)
        {
            try
            {
                DateTime date;
                return DateTime.TryParse(DateString, out date);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool HasReference(string table, string column, string value)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"declare @dependancies nvarchar(max)='';
                                 SELECT @dependancies = @dependancies + ' select count(*) DataCount from ' +
                                 OBJECT_NAME(f.parent_object_id) + '  where ' + COL_NAME(fc.parent_object_id, fc.parent_column_id) + ' =' + @value + ' union all' FROM sys.foreign_keys AS f INNER JOIN sys.foreign_key_columns AS fc    ON f.object_id = fc.constraint_object_id   where OBJECT_NAME(f.referenced_object_id) = @table and COL_NAME(fc.parent_object_id, fc.parent_column_id) = @column set @dependancies = 'select sum(DataCount) DataCount from (' + substring(@dependancies, 1, len(@dependancies) - 9) + ')t1' exec(@dependancies)";
                db.Open();
                return Convert.ToInt32(db.ExecuteScalar(System.Data.CommandType.Text, query)) > 0;
            }
            finally
            {
                db.Close();
            }
        }

        public static bool PostFinancials(string entity, int identity, int userId)
        {
            DBManager db = new DBManager();
            try
            {
                string query2 = @"[DBO].[USP_FIN_POSTING] @Flagvalue,@Flagid,@Userid";
                db.CleanupParameters();
                db.CreateParameters(3);
                db.AddParameters(0, "@Flagvalue", entity);
                db.AddParameters(1, "@Flagid", identity);
                db.AddParameters(2, "@Userid", userId);
                db.Open();
                db.BeginTransaction();
                db.ExecuteProcedure(System.Data.CommandType.Text, query2);
                db.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Helper | PostFinancials(string entity,int identity, int userId)");
                db.RollBackTransaction();
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Helper | IsValidEmail(this string email)");
                return false;
            }
        }
        /// <summary>
        /// Get string value between [first] a and [last] b.
        /// </summary>
        public static string Between(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        public static string Before(this string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        public static void LogException(Exception Ex, string Operation)
        {
            try
            {
                if (!File.Exists(@"C:\Users\Public\Exception.log"))
                {
                    File.Create(@"C:\Users\Public\Exception.log");
                }
                FileStream readStream = new FileStream(@"C:\Users\Public\Exception.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using (StreamReader reader = new StreamReader(readStream))
                {
                    string Exceptions = reader.ReadToEnd();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("****************************************");
                    sb.AppendLine("UTC DATE:-");
                    sb.AppendLine(DateTime.UtcNow.ToString());
                    sb.AppendLine("MESSAGE:-");
                    sb.AppendLine(Ex.Message);
                    sb.AppendLine("INNER EXCEPTION:-");
                    sb.AppendLine(Ex.InnerException == null ? "No Inner Exception Found" : Ex.InnerException.Message);
                    sb.AppendLine("OPERATION:-");
                    sb.AppendLine(Operation);
                    sb.AppendLine("****************************************");
                    FileStream writeStream = new FileStream(@"C:\Users\Public\Exception.log", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    using (StreamWriter writer = new StreamWriter(writeStream))
                    {
                        writer.WriteLine(sb.ToString() + Exceptions);
                    }
                    writeStream.Dispose();
                    readStream.Dispose();
                }


            }
            catch (Exception ex)
            {
            }
            finally
            {

            }
        }

        public static string FindFinancialYear()
        {
            string query = "select [dbo].[UDF_GetFinYear]()";
            DBManager db = new DBManager();
            try
            {
                db.Open();
                return Convert.ToString(db.ExecuteScalar(System.Data.CommandType.Text, query));
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

        public static string TimeLeft(DateTime UTCDate)
        {
            TimeSpan timeLeft = (DateTime.UtcNow - UTCDate);
            string timeLeftString = "";
            if (timeLeft.Days > 0)
            {
                timeLeftString+= timeLeft.Days == 1 ? timeLeft.Days+" day" : timeLeft.Days+" days";
            }
            else if (timeLeft.Hours > 0)
            {
                timeLeftString += timeLeft.Hours == 1 ? timeLeft.Hours + " hour" : timeLeft.Hours+" hours";
            }
            else
            {
                timeLeftString += timeLeft.Minutes == 1 ? timeLeft.Minutes+" minute" : timeLeft.Minutes+" minutes";
            }
            return timeLeftString;
        }
    }
}
