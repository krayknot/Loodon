using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Win32;
using SQLBackup;

namespace Loodon
{
    public enum ValidationType
    {
        EmptyCheck,
        EmailCheck
    }

    public enum BackupType
    {
        Excel,
        Access,
        Xml,
        Sql
    }

    public enum ExcelBackupMode
    {
        SeparateSheet,
        SeparateWorkbook
    }

    class ClsCommon : ICommonFunctions
    {        
        public static string ServerName = string.Empty;
        public static string LoginName = string.Empty;
        public static string PasswordName = string.Empty;
        public static string DatabaseName = string.Empty;
        public static BackupType BackupDestination;
        public static ExcelBackupMode ExcelMode;
        public static readonly string ServerNameDestination = string.Empty;
        public static readonly string LoginNameDestination = string.Empty;
        public static readonly string PasswordNameDestination = string.Empty;

        private static SqlConnection _sqlCon = new SqlConnection();

        //Registry related
        readonly string subKey = "SOFTWARE\\" + "Loodon";
        readonly RegistryKey _baseRegistryKey = Registry.LocalMachine;

        public static DataSet PublicDatasetServers;


        public struct DatabaseParam
        {
            private string _serverName;

            //Data file parameters
            public string DataFileName;
            public string DataPathName;
            public string DataFileSize;
            public string DataFileGrowth;
            //Log file parameters
            public string LogFileName;
            public string LogPathName;
            public string LogFileSize;
            public string LogFileGrowth;

            public string DatabaseName { get; set; }

            //public DatabaseParam(string serverName)
            //{
            //    _serverName = serverName;
            //}
        }

        public int ExecuteUpdateDeleteStatement(string server, string username, string password, string database, string query)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + "; ";
            _sqlCon.ConnectionString = sqlConnectionString;
            var cmd = new SqlCommand(query, _sqlCon);
            _sqlCon.Open();
            var response = cmd.ExecuteNonQuery();
            _sqlCon.Close();
            cmd.Dispose();          

            return response;
        }

        public string Read(string keyName)
        {
            // Opening the registry key
            var rk = _baseRegistryKey;
            // Open a subKey as read-only
            var sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }

            // If the RegistryKey exists I get its value
            // or null is returned.
            return (string)sk1.GetValue(keyName.ToUpper());
        }

        public bool Write(string keyName, object value)
        {
            
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                var sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1?.SetValue(keyName.ToUpper(), value);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataSet GetStoredProcedureText(string server, string username, string password, string database, string storedProcedureName)
        {
            var query = "SELECT OBJECT_DEFINITION (OBJECT_ID(N'" + storedProcedureName + "'));";
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dst = GetDatasetFromSQLQuery(query);
            return dst;
        }

        public static long GetMaxId(string server, string username, string password, string database, string query)
        {
            long response = 0;
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter(query, _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);

            if (dst.Tables[0].Rows.Count <= 0) return response;
            //MAX ID exception handling. Will be set to zero on any Exception
            try
            {
                response = Convert.ToInt32(dst.Tables[0].Rows[0][0]);
            }
            catch (Exception)
            {
                response = 0;
            }

            return response;
        }

        public static bool IdentityColumnExists(string server, string username, string password, string database, string query)
        {
            var response = false;
            
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter(query, _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);

            if (dst.Tables[0].Rows.Count > 0)
            {
                response = true;
            }
            return response;    
        }

        public static void CreateDatabase(DatabaseParam dbParam)
        {
            
            //System.Data.SqlClient.SqlConnection tmpConn;
            _sqlCon = new SqlConnection
            {
                ConnectionString = "SERVER = " + ServerName + "; DATABASE = master; User ID = " + LoginName +
                                   "; Pwd = " + PasswordName
            };

            var sqlCreateDbQuery = " CREATE DATABASE " + dbParam.DatabaseName + " ON PRIMARY "
                                      + " (NAME = " + dbParam.DataFileName + ", "
                                      + " FILENAME = '" + dbParam.DataPathName + "', "
                                      + " SIZE = 5MB,"
                                      + "	FILEGROWTH =" + dbParam.DataFileGrowth + ") "
                                      + " LOG ON (NAME =" + dbParam.LogFileName + ", "
                                      + " FILENAME = '" + dbParam.LogPathName + "', "
                                      + " SIZE = 5MB, "
                                      + "	FILEGROWTH =" + dbParam.LogFileGrowth + ") ";

            var myCommand = new SqlCommand(sqlCreateDbQuery, _sqlCon);
            try
            {
                _sqlCon.Open();
                myCommand.ExecuteNonQuery();
            }
            finally
            {
                _sqlCon.Close();
            }
        }


        public DataSet GetColumnsofTable(string server, string username, string password, string database, string tableName)
        {
            if (tableName.Contains("[") || tableName.Contains("."))
            {
                tableName = TableNamewithoutSchema(tableName);
            }

            var query = "select column_name, Data_Type, Character_maximum_Length from information_schema.columns  where table_name = '" + tableName + "' order by ordinal_position";
            return GetDatasetFromSQLQuery(query);
        }

        public static void ExecuteInsertStatements(string server, string username, string password, string database, string query)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + "; ";
            _sqlCon.ConnectionString = sqlConnectionString;            
            _sqlCon.Open();

            var cmd = new SqlCommand(query, _sqlCon) {CommandTimeout = 500};
            cmd.ExecuteNonQuery();
            _sqlCon.Close();
        }

        public static DataSet ExecuteInsertStatementsForDataset(string server, string username, string password, string database, string query)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + "; ";
            _sqlCon.ConnectionString = sqlConnectionString;
            var dad = new SqlDataAdapter(query, _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public DataSet GetUnMatchData(string server, string username, string password, string database, string query)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";Connection Timeout=200";
            _sqlCon.ConnectionString = sqlConnectionString;

            return GetDatasetFromSQLQuery(query);
        }

        private string TableNamewithoutSchema(string tableNamewithSchema)
        {
            //Remove the schema from the Table variable
            var strArray = tableNamewithSchema.Split('.');
            var table = strArray[1].Replace("[", "").Replace("]", "");

            return table;
        }

        public string GetPrimaryKeyofDatabaseTable(string server, string Username, string Password, string Database, string table)
        {
            table = TableNamewithoutSchema(table);
            var sqlQuery = "SELECT KU.table_name as tablename,column_name as primarykeycolumn FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC " +
                "INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' AND " +
                "TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME and ku.table_name='" + table + "' ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION";

            var dst = GetDatasetFromSQLQuery(sqlQuery);;
            var response = dst.Tables[0].Rows[0]["primarykeycolumn"].ToString();

            return response;
        }

        public DataSet GetTablesfromDbDetails(string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            if (_sqlCon.State == ConnectionState.Open)
            {
                _sqlCon.Dispose();    
            }
            _sqlCon.ConnectionString = sqlConnectionString;

            var sqlQuery = "select '[' + SCHEMA_NAME(schema_id) + '].[' + name + ']'as TableName from sys.tables where type = 'U' order by name";
            return GetDatasetFromSQLQuery(sqlQuery);
        }

        public DataSet GetDatasetFromSQLQuery(string sqlQuery)
        {
            
            var dad = new SqlDataAdapter(sqlQuery, _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst; 
        }

        

        public DataSet GetTableData(string tableName)
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();

            //Check for the invalid table name. eg. 'dbo'
            if (tableName == "dbo") return dst;
            var dad = new SqlDataAdapter("Select * from " + tableName, _sqlCon);                
            dad.Fill(dst);
            return dst;
        }

        public static DataSet GetTableData(string tableName, string serverName, string loginName, string password, string databaseName)
        {
            var sqlConnectionString = "Data Source=" + serverName + ";User Id=" + loginName + ";Password=" + password + ";Initial Catalog=" + databaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();

            //Check for the invalid table name. eg. 'dbo'
            if (tableName == "dbo") return dst;
            var dad = new SqlDataAdapter("Select * from " + tableName, _sqlCon);
            dad.Fill(dst);
            return dst;
        }   
        
        public DataSet GetColumns(int objectId)
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter("select * from sys.columns where object_id = " + objectId, _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public DataSet GetStoredProcedures()
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter("Select * from sysobjects where type = 'P' and category = 0", _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        /// <summary>
        /// Retrieves the Table name from the sys object - sys.tables
        /// </summary>
        /// <returns>Data-set</returns>
        public  DataSet GetTables()
        {
            var dst = new DataSet();
            try
            {
                var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
                _sqlCon.ConnectionString = sqlConnectionString;

                var dad = new SqlDataAdapter("SELECT (s.name + '.' + t.name) AS SchemaTable,  s.name AS SchemaName, t.name AS TableName, s.schema_id, t.OBJECT_ID FROM sys.Tables t INNER JOIN sys.schemas s ON s.schema_id = t.schema_id ORDER BY t.name", _sqlCon);
                
                dad.Fill(dst);
            }
            finally
            {
                _sqlCon.Close();
            }            
            return dst;                     
        }

        public DataSet GetViews()
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter("select * from sys.views", _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public DataSet GetTableValuedFunctions()
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter("Select * from sysobjects where type = 'TF'", _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public DataSet GetScalarValuedFunctions()
        {
            var sqlConnectionString = "Data Source=" + ClsCommon.ServerName + ";User Id=" + ClsCommon.LoginName + ";Password=" + ClsCommon.PasswordName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dad = new SqlDataAdapter("Select * from sysobjects where type = 'FN'", _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="inputText"></param>
     /// <param name="controlLabelText"></param>
     /// <param name="validateType"></param>
     /// <returns></returns>
        public string ValidateInput(string inputText, string controlLabelText, ValidationType validateType)
        {
            var error = string.Empty;

            if (validateType != 0) return error;
            if (inputText.Length < 1)
            {
                error = controlLabelText + ": Empty value is not allowed." + "\n";
            }
            return error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsUserAuthenticated(string server, string login, string password)
        {
            bool flag;
            try
            {
                //Sets the Connection
                var sqlConnectionString = "Data Source=" + server + ";User Id=" + login + ";Password=" + password + ";";
                _sqlCon.ConnectionString = sqlConnectionString;

                //var cmd = new SqlCommand("Select Count(*) from sys.objects", sqlCon);
                _sqlCon.Open();
                ServerName = server;
                LoginName = login;
                PasswordName = password; 
                flag = true;
                _sqlCon.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public void LogLoginAttempt(string server, string login, string password, string logingStatus)
        {
            var acsCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database\\sqlbackup.mdb;User Id=admin;Password=;";
            var con = new OleDbConnection(acsCon);
            var cmd = new OleDbCommand("Insert into Logging(Logging_Server, Logging_Login, Logging_Password, Logging_Date, Logging_Status) values('" + server + "', '" + login + "', '" + password + "', '" + DateTime.Now + "', '" + logingStatus + "')", con);
            con.Open();  
            cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public DataSet GetDatabases()
        {
            var dad = new SqlDataAdapter("Select name from sys.databases order by name", _sqlCon);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public bool DoesTableExists(string tableName, string connectionString)
        {
            var dbConn = new OleDbConnection(connectionString);
            dbConn.Open();

            var restrictions = new string[3];
            restrictions[2] = tableName;

            var dbTbl = dbConn.GetSchema("Tables", restrictions);

            var flag = dbTbl.Rows.Count >= 1;
            dbTbl.Dispose();
            dbConn.Close();
            dbConn.Dispose();

            return flag; 
        }

        public void CreateSQLDatabase(string serverName, string userName, string passWord, string databaseName)
        {
            var conDb = new SqlConnection("Data Source=" + serverName + ";Initial Catalog=master;User Id=" + userName + ";Password=" + passWord + ";");
            var cmd = new SqlCommand("Create Database " + databaseName ,conDb);
            conDb.Open();
            cmd.ExecuteNonQuery();
            conDb.Close();
            conDb.Dispose();  
        }

        public static void AttachSqlDatabase(string serverName, string userName, string passWord, string query)
        {
            var conDb = new SqlConnection("Data Source=" + serverName + ";Initial Catalog=master;User Id=" + userName + ";Password=" + passWord + ";");
            var cmd = new SqlCommand(query, conDb);
            conDb.Open();
            cmd.ExecuteNonQuery();
            conDb.Close();
            conDb.Dispose();
        }

        public static void BackupDatabase(string databaseBackupPath)
        {

            var conDb = new SqlConnection("Data Source=" + ServerName + ";Initial Catalog=master;User Id=" +LoginName + ";Password=" + PasswordName + ";");
            var cmd = new SqlCommand("BACKUP DATABASE " + DatabaseName + " TO DISK='" + databaseBackupPath + "'", conDb);
            conDb.Open();
            cmd.ExecuteNonQuery();
            conDb.Close();
            conDb.Dispose();
        }

        public static void RestoreDatabase(string databaseBackupPath, string databaseName)
        {

            var conDb = new SqlConnection("Data Source=" + ServerName + ";Initial Catalog=master;User Id=" + LoginName + ";Password=" + PasswordName + ";");
            var cmd = new SqlCommand("RESTORE DATABASE " + databaseName + " FROM DISK='" + databaseBackupPath + "'", conDb);
            conDb.Open();
            cmd.ExecuteNonQuery();
            conDb.Close();
            conDb.Dispose();
        }
    }
}
