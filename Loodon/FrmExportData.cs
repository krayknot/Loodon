using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

namespace Loodon
{
    public partial class FrmExportData : Form
    {
        private string _server = string.Empty;
        private string _database = string.Empty;
        private string _table = string.Empty;
        private string _destination = string.Empty;

        private string _query = string.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;

        public FrmExportData(string server, string database, string table, string destination, string query, string username, string password)
        {
            InitializeComponent();
            _server = server;
            _database = database;
            _table = table;
            _destination = destination;
            _query = query;
            _username = username;
            _password = password;
        }

        private void frmExportData_Load(object sender, EventArgs e)
        {
            Refresh();
            lblServer.Text = "Server: " + _server;
            lblDatabase.Text = "Database: " + _database;
            lblTable.Text = "Table: " + _table;
            lblDestinationPath.Text = _destination;

            Refresh();
            //new LoodonDAL.clsCommon().BackupExcelSeparateSheet(destination, query, server, username, password, database, table);
            BackupExcelSeparateSheet(_destination, _query, _server, _username, _password, _database, _table);

            Close();
        }

        private void BackupExcelSeparateSheet(string destination, string SQLQuery, string Server, string Username, string Password, string Database, string TableName)
        {
            var dataError = false;

            var sqlConnectionString = "Data Source=" + Server + ";User Id=" + Username + ";Password=" + Password + ";Initial Catalog=" + Database + ";";
            var con = new SqlConnection(sqlConnectionString);

            var dstTemp = new DataSet();

            try
            {
                dstTemp = new LoodonDAL.ClsCommon().GetDatasetFromSqlQuery(SQLQuery, con);
            }
            catch (Exception)
            {
                dataError = true;
            }

            var common = new ClsCommon();
            if (File.Exists(destination + "\\" + TableName + ".xls"))
                File.Delete(destination + "\\" + TableName + ".xls");

            //si Excel, 
            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            //string tableName = string.Empty;
            var conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + destination + "\\" + TableName + ".xls; Extended Properties='Excel 8.0;HDR=YES'"; ;
            conn.Open();

            if (dataError == false)
            {
                if (dstTemp.Tables.Count > 0)
                {
                    //extraire les colonnes de la table de données
                    var cols = string.Empty;
                    for (var i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
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
                        colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName + "],255) , ";
                    }
                    //colsDest = colsDest.Substring(0, colsDest.Length - 2);

                    //Insérez l'enregistrement de la table
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
                            // ignored
                        }

                        colsValues = string.Empty;
                    }
                }
            }
            conn.Close();
        }
    }
}
