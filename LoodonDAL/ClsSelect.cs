using System;
using System.Data;
using System.Data.SqlClient;

namespace LoodonDAL
{
        public class ClsSelect : ClsAbstractSelectFunctions
    {
        SqlConnection _sqlCon = new SqlConnection();


        public DataSet GetTableData(LoodonDAL.ClsDataTypes.Credentials databaseCredentials)
        {
            var sqlConnectionString = "Data Source=" + databaseCredentials.Server + ";User Id=" + databaseCredentials.Username + ";Password=" + databaseCredentials.Password + ";Initial Catalog=" + databaseCredentials.DbName + ";";
            _sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();

            //Check for the invalid table name. eg. 'dbo'
            if (databaseCredentials.TableName == "dbo") return dst;
            var dad = new SqlDataAdapter("Select * from " + databaseCredentials.TableName, _sqlCon);
            dad.Fill(dst);
            return dst;
        }


        public string GetServerCurrentTimeStamp(string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";

            _sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();
            var dad = new SqlDataAdapter(ClsQueries.SqlGetservertimestamp, _sqlCon);
            dad.Fill(dst);

            var response = Convert.ToString(dst.Tables[0].Rows[0][0]);
            return response;
        }

        /// <summary>
        /// Retruns the dataset contains the results table names
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public DataSet GetTablefromColumn(string columnName, string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var dst = new ClsCommon().GetDatasetFromSqlQuery(ClsQueries.SqlGettablefromcolumn.Replace("{COLUMNAME}", columnName), _sqlCon);
            return dst;
        }


        /// <summary>
        /// Gets Records count
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public long GetRecordsCount(string tableName, string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            long response = 0;

            _sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();
            var dad = new SqlDataAdapter(ClsQueries.SqlCountrecords.Replace("{TABLENAME}", tableName), _sqlCon);
            dad.Fill(dst);

            response = Convert.ToInt64(dst.Tables[0].Rows[0][0]);
            return (long)response;
        }


        public virtual string GetTableScript(string tableName, string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var common = new ClsCommon();
            var dst = common.GetDatasetFromSqlQuery(ClsQueries.SqlGettablescript.Replace("{TABLENAME}", tableName), _sqlCon);
            var response = common.ConvertDatasettoString(dst);

            return response;
        }

        public string GetPrimaryKey(string tableName, string server, string username, string password, string database)
        {
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            _sqlCon.ConnectionString = sqlConnectionString;

            var common = new ClsCommon();
            var dst = common.GetDatasetFromSqlQuery(ClsQueries.SqlGetprimarykey.Replace("{TABLENAME}", tableName), _sqlCon);
            var response = dst.Tables[0].Rows[0][1].ToString();
            return response;
        }
    }
}
