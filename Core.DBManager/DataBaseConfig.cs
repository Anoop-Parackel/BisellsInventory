/*
******************************************************************************************
Module/Class       : DataBaseConfig
Purpose            : Fetching the DB configuration from Webconfiguration
Company            : ENVESTNET
Developed by       : Johnson
Last Modifided by  : Johnson
Last Modified Date :
Usage              : 
*****************************************************************************************
*/
  
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
//using System.Data.OracleClient;
using System.Web.Configuration;
using System.Configuration;

namespace Core.DBManager
{
    public class DataBaseConfig    
    {
        public string ConnectionString = "";
        public string DBProvider = "";
        System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        //System.Configuration.ConnectionStringSettingsCollection WinConfig = System.Configuration.ConfigurationManager.ConnectionStrings;
        public DataBaseConfig()
        {
            ConnectionString = rootWebConfig.ConnectionStrings.ConnectionStrings["Connection"].ToString();
            DBProvider = rootWebConfig.ConnectionStrings.ConnectionStrings["Connection"].ProviderName.ToString();

            //ConnectionString = WinConfig["Default"].ConnectionString.ToString();
            //DBProvider = WinConfig["Default"].ProviderName.ToString();

        }

        public DataBaseConfig(string ConStringName)
        {
            ConnectionString = rootWebConfig.ConnectionStrings.ConnectionStrings[ConStringName].ToString();
            DBProvider = rootWebConfig.ConnectionStrings.ConnectionStrings[ConStringName].ProviderName.ToString();
            //ConnectionString = WinConfig[ConStringName].ConnectionString.ToString();
            //DBProvider = WinConfig[ConStringName].ProviderName.ToString();

        }
    }
    
}