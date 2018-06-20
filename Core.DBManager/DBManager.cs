
/*
******************************************************************************************
Module/Class       : DBManager
Purpose            : Database activity manager
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

namespace Core.DBManager
{
    //public sealed class DBManager : IDBManager, IDisposable
    public class DBManager : IDBManager, IDisposable
    {
        private IDbConnection idbConnection;
        private IDataReader idataReader;
        private IDbCommand idbCommand;
        private DataProvider providerType;
        private IDataParameter idataParameter;
        private IDbTransaction idbTransaction = null;
        private IDbDataParameter[] idbParameters = null;
        //private OracleParameter[] refcurParameters = null;
        private string strConnection;

        public DBManager()
        {
            DataBaseConfig dbConfig = new DataBaseConfig();
            strConnection = dbConfig.ConnectionString;
            string strProvider = "sqlserver";

            switch (strProvider)
            {
                case "sqlserver":
                    providerType = DataProvider.SqlServer;
                    break;
                //case "oracle":
                   // providerType = DataProvider.Oracle;
                   // break;
                case "odbc":
                    providerType = DataProvider.Odbc;
                    break;
                case "oledb":
                    providerType = DataProvider.OleDb;
                    break;
            }

        }

        public DBManager(DataProvider providerType)
        {
            this.providerType = providerType;
        }

        public DBManager(DataProvider providerType, string connectionString)
        {
            this.providerType = providerType;
            this.strConnection = connectionString;
        }

        public DBManager(string ConStringName)
        {
            DataBaseConfig dbConfig = new DataBaseConfig(ConStringName);
            strConnection = dbConfig.ConnectionString;
            string strProvider = dbConfig.DBProvider.ToString().ToLower();

            switch (strProvider)
            {
                case "sqlserver":
                    providerType = DataProvider.SqlServer;
                    break;
               // case "oracle":
                   // providerType = DataProvider.Oracle;
                   // break;
                case "odbc":
                    providerType = DataProvider.Odbc;
                    break;
                case "oledb":
                    providerType = DataProvider.OleDb;
                    break;
            }


        }


        public IDbConnection Connection
        {
            get
            {
                return idbConnection;
            }
        }

        public IDataReader DataReader
        {
            get
            {
                return idataReader;
            }
            set
            {
                idataReader = value;
            }
        }

        public IDataParameter DataParameter
        {
            get
            {
                return idataParameter;
            }
            set
            {
                idataParameter = value;
            }
        }

        public DataProvider ProviderType
        {
            get
            {
                return providerType;
            }
            set
            {
                providerType = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }

        public IDbCommand Command
        {
            get
            {
                return idbCommand;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return idbTransaction;
            }
        }

        public IDbDataParameter[] Parameters
        {
            get
            {
                return idbParameters;
            }
        }

        //public OracleParameter[] RefParameters
        //{
        //    get
        //    {
        //        return refcurParameters;
        //    }
        //}

        

        public void Open()
        {
            idbConnection =  DBManagerFactory.GetConnection(this.providerType);
            idbConnection.ConnectionString = this.ConnectionString;
            if (idbConnection.State != ConnectionState.Open)
                idbConnection.Open();
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
        }

        //string a = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'; Data Source= 'D:\share\johns\extract\NonFinData.xls';";


        public void Close()
        {
            if(idbConnection!=null)
            {
                if (idbConnection.State != ConnectionState.Closed)
                    idbConnection.Close();
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
            this.idbCommand = null;
            this.idbTransaction = null;
            this.idbConnection = null;
        }
        public void CreateParameters(int paramsCount)
        {
            idbParameters = new IDbDataParameter[paramsCount];
            idbParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
        }
        public void CleanupParameters()
        {
            idbParameters = null;
           // refcurParameters = null;
        }


        public void BeginTransaction()
        {
            if (this.idbTransaction == null)
            {
                if (this.idbConnection != null)
                    idbTransaction = idbConnection.BeginTransaction();
            }
            this.idbCommand.Transaction = idbTransaction;
        }

        public void CommitTransaction()
        {
            if (this.idbTransaction != null)
                this.idbTransaction.Commit();
             idbTransaction = null;
        }

        
        public void RollBackTransaction()
        {
                if (this.idbTransaction != null)
                this.idbTransaction.Rollback();
            idbTransaction = null;
        }


        public void CloseReader()
        {
            if (this.DataReader != null)
                this.DataReader.Close();
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            int returnValue = idbCommand.ExecuteNonQuery();
            idbCommand.Parameters.Clear();
            return returnValue;
        }
        public void InsertVarbinary(byte[] image,string query)
        {
            DataBaseConfig dbConfig = new DataBaseConfig();
            using (var conn = new SqlConnection(dbConfig.ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                var param = new SqlParameter("@imagecolumn", SqlDbType.Binary)
                {
                    // here goes your binary data (make sure it's correct)
                    Value = image
                };
                cmd.Parameters.Add(param);
                int rowsAffected = cmd.ExecuteNonQuery();

                // do your other magic ...
            }
        }
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            object returnValue = idbCommand.ExecuteScalar();
            idbCommand.Parameters.Clear();
            return returnValue;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            //PrepareCommand(idbCommand,this.Connection, this.Transaction,commandType,commandText);
            this.Command.CommandText = commandText;
            this.Command.CommandType = commandType;
            this.Command.Connection = this.Connection;
            if (this.Parameters != null)
            {
                AttachParameters(this.Command, this.Parameters);
            }
            IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
            dataAdapter.SelectCommand = this.Command;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            idbCommand.Parameters.Clear();
            return dataSet;
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            idbCommand.Connection = this.Connection;
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            this.DataReader = idbCommand.ExecuteReader();
            idbCommand.Parameters.Clear();
            return this.DataReader;
        }
        public DataTable ExecuteQuery(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            IDataReader reader = idbCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Dispose();
            idbCommand.Parameters.Clear();
            return dt;
        }
        public DataTable ExecuteProcedure(CommandType commandType, string commandText)
        {
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            IDataReader reader = idbCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Dispose();
            idbCommand.Parameters.Clear();
            return dt;
        }
        private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        private void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter idbParameter in commandParameters)
            {
                if (((idbParameter.Direction == ParameterDirection.InputOutput) ||
                     (idbParameter.Direction == ParameterDirection.Input) ||
                     (idbParameter.Direction == ParameterDirection.Output) ||
                     (idbParameter.Direction == ParameterDirection.ReturnValue)) && (idbParameter.Value == null))
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }

            //if (this.providerType == DataProvider.Oracle && refcurParameters!=null )
            //{
            //    foreach (OracleParameter RefParam in refcurParameters)
            //    {
            //        command.Parameters.Add(RefParam);
            //    } 
            //}

        }


        public void AddParameters(int index, string paramName,object objValue)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Direction = System.Data.ParameterDirection.Input;
                idbParameters[index].Value = objValue;
            }
        }

        
        public void AddParameters(int index, string paramName, StoredProcedureParameterDirection parameterDirection)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                switch (parameterDirection)
                {
                    case StoredProcedureParameterDirection.Input:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case StoredProcedureParameterDirection.Output:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Output;
                        break;
                    case StoredProcedureParameterDirection.InputOutput:
                        idbParameters[index].Direction = System.Data.ParameterDirection.InputOutput;
                        break;
                    case StoredProcedureParameterDirection.ReturnValue:
                        idbParameters[index].Direction = System.Data.ParameterDirection.ReturnValue;
                        break;
                }

            }
        }

        public void AddParameters(int index, string paramName, object objValue, StoredProcedureParameterDirection parameterDirection)
        {


            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue;
                switch (parameterDirection)
                {
                    case StoredProcedureParameterDirection.Input:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case StoredProcedureParameterDirection.Output:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Output;
                        break;
                    case StoredProcedureParameterDirection.InputOutput:
                        idbParameters[index].Direction = System.Data.ParameterDirection.InputOutput;
                        break;
                    case StoredProcedureParameterDirection.ReturnValue:
                        idbParameters[index].Direction = System.Data.ParameterDirection.ReturnValue;
                        break;
                }

            }
        }

        public void AddParameters(int index, string paramName, StoredProcedureParameterDirection parameterDirection, DbType type, int size)
        {
            
            //if (this.providerType == DataProvider.Oracle)
            //{
            //    OracleParameter param = new OracleParameter(paramName, OracleType.Cursor);
            //}

            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].DbType = type;
                idbParameters[index].Size = size;
                switch (parameterDirection)
                {
                    case StoredProcedureParameterDirection.Input:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case StoredProcedureParameterDirection.Output:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Output;
                        break;
                    case StoredProcedureParameterDirection.InputOutput:
                        idbParameters[index].Direction = System.Data.ParameterDirection.InputOutput;
                        break;
                    case StoredProcedureParameterDirection.ReturnValue:
                        idbParameters[index].Direction = System.Data.ParameterDirection.ReturnValue;
                        break;
                }

            }
        }
        public void AddParameters(int index, string paramName, object objValue, StoredProcedureParameterDirection parameterDirection, DbType dbType, int size)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue;
                idbParameters[index].Size = size;
                idbParameters[index].DbType = dbType;
                switch (parameterDirection)
                {
                    case StoredProcedureParameterDirection.Input:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case StoredProcedureParameterDirection.Output:
                        idbParameters[index].Direction = System.Data.ParameterDirection.Output;
                        break;
                    case StoredProcedureParameterDirection.InputOutput:
                        idbParameters[index].Direction = System.Data.ParameterDirection.InputOutput;
                        break;
                    case StoredProcedureParameterDirection.ReturnValue:
                        idbParameters[index].Direction = System.Data.ParameterDirection.ReturnValue;
                        break;
                }

            }
        }

        //public void AddRefParameter(string paramName)
        //{
        //    if (this.providerType == DataProvider.Oracle)
        //    {
        //        refcurParameters  =  new OracleParameter[1];
        //        refcurParameters[0] = new OracleParameter();
        //        refcurParameters[0].ParameterName = paramName;
        //        refcurParameters[0].Value = DBNull.Value;
        //        refcurParameters[0].OracleType = OracleType.Cursor;
        //        refcurParameters[0].Direction = System.Data.ParameterDirection.Output;
        //    }
        //}

        public void BulkCopyToSQLServer(DataTable dt,string DestinationTable)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(this.idbConnection.ConnectionString))
            {
                bulkCopy.DestinationTableName = DestinationTable;
                bulkCopy.WriteToServer(dt);
            }
        }

        }
}