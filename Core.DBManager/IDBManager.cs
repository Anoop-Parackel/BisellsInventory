/*
******************************************************************************************
Module/Class       : DBManager Ineterface
Purpose            : Interface for access from another application
Company            : ENVESTNET
Developed by       : Johnson
Last Modifided by  : Johnson
Last Modified Date :
Usage              : 
*****************************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using System.Data.OleDb;
//using Oracle.DataAccess.Client;
  
 namespace Core.DBManager
 {
     public enum DataProvider
     {
         Oracle, SqlServer, OleDb, Odbc
     }

     public enum StoredProcedureParameterDirection
     {
         Input, InputOutput, Output, ReturnValue
     }

     interface IDBManager
     {

         DataProvider ProviderType
         {
             get;
             set;
         }

         string ConnectionString
         {
             get;
             set;
         }

         IDbConnection Connection
         {
             get;
         }
         IDbTransaction Transaction
         {
             get;
         }

         IDataReader DataReader
         {
             get;
         }
         IDbCommand Command
         {
             get;
         }

         IDbDataParameter[] Parameters
         {
             get;
         }

         void Open();
         void BeginTransaction();
         void CommitTransaction();
         void RollBackTransaction();
         void CreateParameters(int paramsCount);
         void AddParameters(int index, string paramName, object objValue);
         void AddParameters(int index, string paramName, object objValue, StoredProcedureParameterDirection parameterDirection);
         void AddParameters(int index, string paramName, StoredProcedureParameterDirection parameterDirection);
         void AddParameters(int index, string paramName, StoredProcedureParameterDirection parameterDirection, DbType type,int size);
         void AddParameters(int index, string paramName, object objValue, StoredProcedureParameterDirection parameterDirection, DbType dbType, int size);
         void BulkCopyToSQLServer(DataTable dt, string DestinationTable);
         //void AddRefParameter(string paramName);
         void CleanupParameters();

         IDataReader ExecuteReader(CommandType commandType, string commandText);
         DataSet ExecuteDataSet(CommandType commandType, string commandText);
         object ExecuteScalar(CommandType commandType, string commandText);
         int ExecuteNonQuery(CommandType commandType, string commandText);
         DataTable ExecuteQuery(CommandType commandType, string commandText);
         DataTable ExecuteProcedure(CommandType commandType, string commandText);
         void CloseReader();
         void Close();
         void Dispose();

     }
 }