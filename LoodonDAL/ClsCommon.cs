using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace LoodonDAL
{
    public class ClsCommon : ClsAbstractCommonFunctions
    {
        public DataSet GetColumnProperties(string server, string username, string password, string columnName, string tableName, string database)
        {
            var query = ClsQueries.SqlGetcolumnproperties.Replace("{COLUMNNAME}", columnName).Replace("{TABLENAME}", tableName);

            var sqlConnectionString = "Data Source=" + server + ";User Id=" + username + ";Password=" + password + ";Initial Catalog=" + database + ";";
            var con = new SqlConnection(sqlConnectionString);

            var dstResponse = GetDatasetFromSqlQuery(query, con);
            return dstResponse;
        }

        /// <summary>
        /// Checks whether DELETE is present in Query or not
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool QueryhasDelete(string query)
        {
            var response = query.ToLower().Contains("delete");
            return response;
        }

        /// <summary>
        /// Checks whether UPDATE is present in Query or not
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool QueryhasUpdate(string query)
        {
            var response = query.ToLower().Contains("update");
            return response;
        }

        public void BackupExcelSeparateSheet(string destination, string SQLQuery, string Server, string Username, string Password, string Database, string TableName)
        {
            var dataError = false;

            var sqlConnectionString = "Data Source=" + Server + ";User Id=" + Username + ";Password=" + Password + ";Initial Catalog=" + Database + ";";
            var con = new SqlConnection (sqlConnectionString);

            var dstTemp = new DataSet();

            try
            {
                dstTemp = GetDatasetFromSqlQuery(SQLQuery, con);
            }
            catch (Exception)
            {
                dataError = true;
            }       
            
            var common = new ClsCommon();
            if (File.Exists(destination + "\\" + Database + ".xls"))
                File.Delete(destination + "\\" + Database + ".xls");
            
            //si Excel, 
            var conn = new OleDbConnection
            {
                ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + destination + "\\" + Database +
                                   ".xls; Extended Properties='Excel 8.0;HDR=YES'"
            };
            ;
            conn.Open();            

                    if (dataError == false)
                    {
                        if (dstTemp.Tables.Count > 0)
                        {
                            string cols = string.Empty;
                            for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                cols = cols + "[" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "] varchar(255), ";
                            }
                            cols = cols.Substring(0, cols.Length - 2);
                        
                            var cmd = new OleDbCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "CREATE TABLE [" + TableName + "] (" + cols + ")";
                            cmd.ExecuteNonQuery();

                            //extraire les colonnes de la table source
                            var colsDest = string.Empty;
                            var colsValues = string.Empty;
                            var fieldValue = string.Empty;

                            for (var i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "],255) , ";
                            }
                            colsDest = colsDest.Substring(0, colsDest.Length - 2);
                                                    
                            for (var rowData = 0; rowData <= dstTemp.Tables[0].Rows.Count - 1; rowData++)
                            {          
                                for (var colData = 0; colData <= dstTemp.Tables[0].Columns.Count - 1; colData++)
                                {
                                    fieldValue = dstTemp.Tables[0].Rows[rowData][colData].ToString();
                                    if (fieldValue.Contains(""))
                                    {
                                        fieldValue = string.Empty;
                                    }

                                    if (fieldValue.Length >= 250)
                                    {
                                        try
                                        {
                                            colsValues = colsValues + "'" + fieldValue.Substring(1, 250).Replace("'", "''") + "',";
                                        }
                                        catch (Exception)
                                        {
                                            // ignored
                                        }
                                    }
                                    else
                                    {
                                        colsValues = colsValues + "'" + fieldValue.Replace("'", "''") + "',";
                                    }
                                }
                                colsValues = colsValues.Substring(0, colsValues.Length - 1);

                                cmd.CommandText = "INSERT INTO [" + TableName + "] values (" + colsValues + ")";
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {

                                }

                                colsValues = string.Empty;
                            }                            
                        }
                    }
            conn.Close();
        }

        public override DataSet GetDatasetFromSqlQuery(string sqlQuery, SqlConnection connection)
        {
            var dad = new SqlDataAdapter(sqlQuery, connection);
            var dst = new DataSet();
            dad.Fill(dst);
            return dst;
        }

        public static DataSet GetDatasetFromQuery(string sqlQuery, string server, string user, string pass, string database)
        {
            var sqlCon = new SqlConnection();
            var sqlConnectionString = "Data Source=" + server + ";User Id=" + user + ";Password=" + pass + ";Initial Catalog=" + database + ";";
            sqlCon.ConnectionString = sqlConnectionString;
            var dst = new DataSet();

            var dad = new SqlDataAdapter(sqlQuery, sqlCon);
            dad.Fill(dst);
            return dst;
        }

        public override string ConvertDatasettoString(DataSet dataSetName)
        {
            var response = string.Empty;

            for (var i = 0; i <= dataSetName.Tables[0].Rows.Count - 1; i++)
            {
                response = response + dataSetName.Tables[0].Rows[i][0] + "\n";
            }
            return response;
        }
    }
}
