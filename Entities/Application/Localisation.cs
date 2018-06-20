using Core.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Application
{
    class Localisation
    {
        public DateTime UTCDateTime { get; set; }
        public decimal DayLightSaving { get; set; }

        public static DateTime GetLocalDate(DateTime Date)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select KeyValue from TBL_SETTINGS where KeyID=101";
                db.Open();
                double DaylightSaving = Convert.ToDouble(db.ExecuteScalar(System.Data.CommandType.Text, query));
                return Date.AddMinutes(DaylightSaving * 60);
            }
            catch(Exception ex)
            {
                Application.Helper.LogException(ex, "Localisation | GetLocalDate(DateTime Date)");
                 return Date;
            }
            finally
            {
                db.Close();
            }
        }
        public string GetLocalDateString(DateTime Date)
        {
            DBManager db = new DBManager();
            try
            {
                string query = @"select KeyValue from TBL_SETTINGS where KeyID=101";
                double DaylightSaving = (double)db.ExecuteScalar(System.Data.CommandType.Text, query);
                return Date.AddMinutes(DaylightSaving * 60).ToString();
            }
            catch (Exception ex)
            {
                Application.Helper.LogException(ex, "Localisation | GetLocalDateString(DateTime Date)");
                return Date.ToString();
            }
        }
    }
}
