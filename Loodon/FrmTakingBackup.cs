using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.OleDb;
using System.Data.SqlClient;   
using System.IO;
using ADOX;
using Loodon;

namespace SQLBackup
{
    public partial class frmTakingBackup : Form
    {
        private string  m_sConn1 = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\ExcelData1.xls; Extended Properties=""Excel 8.0;HDR=YES""";

        public frmTakingBackup()
        {
            InitializeComponent();
        }

        private void frmTakingBackup_Load(object sender, EventArgs e)
        {
            //Lists the tables first
            DataTable tbl = new DataTable(); //Creating Table object

            //Adding the Columns
            //----------------------------------------------------------------
            DataColumn colObjSchema = new DataColumn();
            colObjSchema.DataType = System.Type.GetType("System.String");
            colObjSchema.ColumnName = "Object Schema";

            DataColumn colObjName = new DataColumn();
            colObjName.DataType = System.Type.GetType("System.String");
            colObjName.ColumnName = "Object Name";

            DataColumn colObjType = new DataColumn();
            colObjType.DataType = System.Type.GetType("System.String");
            colObjType.ColumnName = "Object Type";

            DataColumn colStatus = new DataColumn();
            colStatus.DataType = System.Type.GetType("System.String");
            colStatus.ColumnName = "Status";

            tbl.Columns.Add(colObjSchema);
            tbl.Columns.Add(colObjName);
            tbl.Columns.Add(colObjType);
            tbl.Columns.Add(colStatus);
            //----------------------------------------------------------------     
            dgrdDatabase.DataSource = tbl;

            ClsCommon common = new ClsCommon();
            DataSet dstTables = new DataSet();
            dstTables = common.GetTables();

            for (int i = 0; i <= dstTables.Tables[0].Rows.Count - 1; i++)
            {
                DataRow dtr = tbl.NewRow();
                dtr["Object Schema"] = dstTables.Tables[0].Rows[i]["SchemaName"];
                dtr["Object Name"] = dstTables.Tables[0].Rows[i]["TableName"];
                dtr["Object Type"] = "Table";
                dtr["Status"] = "";

                tbl.Rows.Add(dtr);
            }            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStartBackup_Click(object sender, EventArgs e)
        {
            btnStartBackup.Enabled = false;
            btnStartBackup.Refresh();

            ClsCommon common = new ClsCommon ();
            string backupPath = string.Empty; 
           
            if (ClsCommon.BackupDestination == BackupType.Excel)
            {        
                if (ClsCommon.ExcelMode == ExcelBackupMode.SeparateSheet)
                {
                    folderBrowserDialog1.ShowDialog();
                    backupPath = folderBrowserDialog1.SelectedPath;  
                    BackupExcelSeparateSheet(backupPath);
                }
                else if (ClsCommon.ExcelMode == ExcelBackupMode.SeparateWorkbook)
                {
                    folderBrowserDialog1.ShowDialog();
                    backupPath = folderBrowserDialog1.SelectedPath;
                    BackupExcelSeparateWorkBook(backupPath);
                }
            }
            else if (ClsCommon.BackupDestination == BackupType.Access)
            {
                folderBrowserDialog1.ShowDialog();
                backupPath = folderBrowserDialog1.SelectedPath;
                BackupAccess(backupPath);
            }
            else if (ClsCommon.BackupDestination == BackupType.Xml)
            {
                folderBrowserDialog1.ShowDialog();
                backupPath = folderBrowserDialog1.SelectedPath;
                BackupXML(backupPath);
            }
            else if (ClsCommon.BackupDestination == BackupType.Sql )
            {
                btnStartBackup.Enabled = false;
                btnStartBackup.Refresh();

                Thread thread = new Thread(BackupSQL);
                thread.Start();

                //BackupSQL();
            }
            btnStartBackup.Enabled = true;
            btnStartBackup.Refresh();
        }

        private void BackupExcelSeparateWorkBook(string destination)
        {
            ClsCommon common = new ClsCommon();
            System.IO.DirectoryInfo dir = new DirectoryInfo(destination + "\\" + ClsCommon.DatabaseName);

            //vérifier si le dossier existe ou pas
            if (Directory.Exists(destination + "\\" + ClsCommon.DatabaseName))
            {
                if (MessageBox.Show("Delete existing Directory " + destination +  ClsCommon.DatabaseName, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {                                        
                    foreach (System.IO.FileInfo file in dir.GetFiles()) 
                        file.Delete();
                    Directory.Delete(destination + "\\" + ClsCommon.DatabaseName);                 
                }                
            }                    
            
            //crèer un nouveau dossier
            Directory.CreateDirectory(destination + "\\" + ClsCommon.DatabaseName);  
            
            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            string tableName = string.Empty;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = m_sConn1;
            conn.Open();

            for (int tblCount = 0; tblCount <= dgrdDatabase.Rows.Count - 1; tblCount++)
            {
                //Récupérer les données de la table
                //créer une chaîne de colonnes
                tableName = Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[0].Value);
                if (tableName.Length > 0)
                {
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Copying Data...";
                    dgrdDatabase.Refresh();

                    if (tblCount > 5)
                    {
                        dgrdDatabase.FirstDisplayedScrollingRowIndex = tblCount - 5;
                    }

                    dgrdDatabase.Rows[tblCount].Selected = true;

                    DataSet dstTemp = common.GetTableData(tableName);

                    //extraire les colonnes de la table de données
                    string cols = string.Empty;
                    for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                    {
                        cols = cols + "[" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "] varchar(255), ";
                    }
                    cols = cols.Substring(0, cols.Length - 2);
                    //cols = "Invalid varchar(250)";

                    OleDbCommand cmd = new OleDbCommand();
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close(); 
                    }
                    conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\\" + ClsCommon.DatabaseName  + "\\" + tableName + ".xls; Extended Properties='Excel 8.0;HDR=YES'";
                    cmd.Connection = conn;

                    conn.Open(); 
                    cmd.CommandText = "CREATE TABLE [" + tableName + "] (" + cols + ")";
                    cmd.ExecuteNonQuery();

                    //extraire les colonnes de la table source
                    string colsDest = string.Empty;
                    string colsValues = string.Empty;
                    string fieldValue = string.Empty;

                    for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                    {
                        colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "],255) , ";
                    }
                    colsDest = colsDest.Substring(0, colsDest.Length - 2);

                    dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgrdDatabase.Refresh();
                    //Insérez l'enregistrement de la table
                    for (int rowData = 0; rowData <= dstTemp.Tables[0].Rows.Count - 1; rowData++)
                    {
                        //dgrdDatabase.Rows[tblCount].Cells[2].Value = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred." ;
                        lblStatus.Text = "Totla: " + dstTemp.Tables[0].Rows.Count + " Current: " + (rowData + 1) + " rows transferred.";
                        lblStatus.Refresh();

                        //dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        //dgrdDatabase.Refresh(); 

                        for (int colData = 0; colData <= dstTemp.Tables[0].Columns.Count - 1; colData++)
                        {
                            fieldValue = dstTemp.Tables[0].Rows[rowData][colData].ToString();
                            if (fieldValue.Contains(""))
                            {
                                fieldValue = string.Empty;
                            }

                            if (fieldValue.Length >= 250)
                            {
                                colsValues = colsValues + "'" + fieldValue.Substring(1, 250).Replace("'", "''") + "',";
                            }
                            else
                            {
                                colsValues = colsValues + "'" + fieldValue.Replace("'", "''") + "',";
                            }
                        }
                        colsValues = colsValues.Substring(0, colsValues.Length - 1);

                        cmd.CommandText = "INSERT INTO [" + tableName + "] values (" + colsValues + ")";
                        cmd.ExecuteNonQuery();
                        colsValues = string.Empty;
                    }

                    //cmd.CommandText = "insert into [" + tableName + "] select " + colsDest + " from [" + clsCommon.databaseName + "].[dbo].[" + tableName + "]";
                    //cmd.ExecuteNonQuery();
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Completed.";
                    dgrdDatabase.Refresh();
                    dgrdDatabase.Rows[tblCount].Selected = false;
                }
            }
            conn.Close();

            string backupCompletedMessage = "Backup Completed successfully. Do you want to view the Folder?";
            if (MessageBox.Show(backupCompletedMessage, "Backup Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string argument = @"/select, " + destination + ClsCommon.DatabaseName ;

                System.Diagnostics.Process.Start("explorer.exe", argument); 
                //using (System.Diagnostics.Process prc = new System.Diagnostics.Process())
                //{
                //    prc. = "c:\\";
                //    prc.Start();
                //}
            }
        }

        private void BackupExcelSeparateSheet(string destination)        
        {
            bool dataError = false;

            ClsCommon common = new ClsCommon();
            if (File.Exists(destination + "\\ExcelData1.xls"))
            {
                if (MessageBox.Show("Delete existing workbooks (" + destination + "\\ExcelData1.xls", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(destination + "\\ExcelData1.xls");
                }
            }

            //si Excel, 
            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            string tableName = string.Empty;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+ destination +"\\ExcelData1.xls; Extended Properties='Excel 8.0;HDR=YES'"; ;
            conn.Open();

            for (int tblCount = 0; tblCount <= dgrdDatabase.Rows.Count - 1; tblCount++)
            {
                //Récupérer les données de la table
                //créer une chaîne de colonnes
                tableName = Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[0].Value) + "."  + Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[1].Value);
                if (tableName.Length > 0)
                {
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Copying Data...";
                    dgrdDatabase.Refresh();

                    if (tblCount > 5)
                    {
                        dgrdDatabase.FirstDisplayedScrollingRowIndex = tblCount - 5;
                    }

                    dgrdDatabase.Rows[tblCount].Selected = true;

                    DataSet dstTemp = new DataSet();

                    try
                    {
                        dstTemp = common.GetTableData(tableName);
                    }
                    catch (Exception)
                    {
                        dataError = true;
                    }

                    if (dataError == false)
                    {
                        if (dstTemp.Tables.Count > 0)
                        {
                            //extraire les colonnes de la table de données
                            string cols = string.Empty;
                            for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                cols = cols + "[" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "] varchar(255), ";
                            }
                            cols = cols.Substring(0, cols.Length - 2);
                            //cols = "Invalid varchar(250)";

                            OleDbCommand cmd = new OleDbCommand();
                            cmd.Connection = conn;
                            cmd.CommandText = "CREATE TABLE [" + tableName + "] (" + cols + ")";
                            cmd.ExecuteNonQuery();

                            //extraire les colonnes de la table source
                            string colsDest = string.Empty;
                            string colsValues = string.Empty;
                            string fieldValue = string.Empty;

                            for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "],255) , ";
                            }
                            colsDest = colsDest.Substring(0, colsDest.Length - 2);

                            dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            dgrdDatabase.Refresh();
                            //Insérez l'enregistrement de la table
                            for (int rowData = 0; rowData <= dstTemp.Tables[0].Rows.Count - 1; rowData++)
                            {
                                //dgrdDatabase.Rows[tblCount].Cells[2].Value = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred." ;
                                lblStatus.Text = dstTemp.Tables[0].Rows.Count + " of " + (rowData + 1) + " rows transferred.";
                                lblStatus.Refresh();

                                //dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                                //dgrdDatabase.Refresh(); 

                                for (int colData = 0; colData <= dstTemp.Tables[0].Columns.Count - 1; colData++)
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

                                        }
                                    }
                                    else
                                    {
                                        colsValues = colsValues + "'" + fieldValue.Replace("'", "''") + "',";
                                    }

                                }
                                colsValues = colsValues.Substring(0, colsValues.Length - 1);

                                cmd.CommandText = "INSERT INTO [" + tableName + "] values (" + colsValues + ")";
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (Exception)
                                {

                                }

                                colsValues = string.Empty;
                            }

                            //cmd.CommandText = "insert into [" + tableName + "] select " + colsDest + " from [" + clsCommon.databaseName + "].[dbo].[" + tableName + "]";
                            //cmd.ExecuteNonQuery();
                            dgrdDatabase.Rows[tblCount].Cells[2].Value = "Completed.";
                            dgrdDatabase.Refresh();
                            dgrdDatabase.Rows[tblCount].Selected = false;
                        }
                    }
                    else
                    {
                        dgrdDatabase.Rows[tblCount].Cells[2].Value = "Data Error.";
                        dgrdDatabase.Refresh();
                        dgrdDatabase.Rows[tblCount].Selected = false;
                    }
                }
            }
            conn.Close();

            string backupCompletedMessage = "Backup Completed successfully. Do you want to view the Backup Excel Sheet?";
            if (MessageBox.Show(backupCompletedMessage, "Backup Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (System.Diagnostics.Process prc = new System.Diagnostics.Process())
                {
                    prc.StartInfo.FileName = destination + "\\Exceldata1.xls";
                    prc.Start();
                }
            } 
        }

        private void BackupAccess(string destination)
        {
            //pas supporte dbo.

            ClsCommon common = new ClsCommon();
            if (File.Exists(destination + ClsCommon.DatabaseName + ".mdb"))
            {
                if (MessageBox.Show("Delete existing workbooks (" + destination + ClsCommon.DatabaseName + ".mdb", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(destination + ClsCommon.DatabaseName + ".mdb");
                }
            }

            Catalog cat = new Catalog();
            cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ destination + "\\" + ClsCommon.DatabaseName + ".mdb;User Id=;Password=;");                        

            //si Excel, 
            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            string tableName = string.Empty;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destination + "\\" + ClsCommon.DatabaseName + ".mdb;User Id=;Password=;"; ;
            conn.Open();

            for (int tblCount = 0; tblCount <= dgrdDatabase.Rows.Count - 1; tblCount++)
            {
                //Récupérer les données de la table
                //créer une chaîne de colonnes
                tableName = Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[0].Value);
                if (tableName.Length > 0)
                {
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Copying Data...";
                    dgrdDatabase.Refresh();

                    if (tblCount > 5)
                    {
                        dgrdDatabase.FirstDisplayedScrollingRowIndex = tblCount - 5;
                    }

                    dgrdDatabase.Rows[tblCount].Selected = true;

                    DataSet dstTemp = common.GetTableData(tableName);

                    //extraire les colonnes de la table de données
                    string cols = string.Empty;
                    for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                    {
                        cols = cols + "[" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "] varchar(255), ";
                    }
                    cols = cols.Substring(0, cols.Length - 2);
                    //cols = "Invalid varchar(250)";

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;

                    if (!common.DoesTableExists(tableName, conn.ConnectionString))
                    {
                        cmd.CommandText = "CREATE TABLE [" + tableName.Replace("dbo.", "") + "] (" + cols + ")";
                        cmd.ExecuteNonQuery();
                    }

                    //extraire les colonnes de la table source
                    string colsDest = string.Empty;
                    string colsValues = string.Empty;
                    string fieldValue = string.Empty;

                    for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                    {
                        colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "],255) , ";
                    }
                    colsDest = colsDest.Substring(0, colsDest.Length - 2);

                    dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgrdDatabase.Refresh();
                    //Insérez l'enregistrement de la table
                    for (int rowData = 0; rowData <= dstTemp.Tables[0].Rows.Count - 1; rowData++)
                    {
                        //dgrdDatabase.Rows[tblCount].Cells[2].Value = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred." ;
                        lblStatus.Text = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred.";
                        lblStatus.Refresh();

                        //dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        //dgrdDatabase.Refresh(); 

                        for (int colData = 0; colData <= dstTemp.Tables[0].Columns.Count - 1; colData++)
                        {
                            fieldValue = dstTemp.Tables[0].Rows[rowData][colData].ToString();
                            if (fieldValue.Contains(""))
                            {
                                fieldValue = string.Empty;
                            }

                            if (fieldValue.Length >= 250)
                            {
                                colsValues = colsValues + "'" + fieldValue.Substring(1, 250).Replace("'", "''") + "',";
                            }
                            else
                            {
                                colsValues = colsValues + "'" + fieldValue.Replace("'", "''") + "',";
                            }

                        }
                        colsValues = colsValues.Substring(0, colsValues.Length - 1);

                        cmd.CommandText = "INSERT INTO [" + tableName + "] values (" + colsValues + ")";
                        cmd.ExecuteNonQuery();
                        colsValues = string.Empty;
                    }

                    //cmd.CommandText = "insert into [" + tableName + "] select " + colsDest + " from [" + clsCommon.databaseName + "].[dbo].[" + tableName + "]";
                    //cmd.ExecuteNonQuery();
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Completed.";
                    dgrdDatabase.Refresh();
                    dgrdDatabase.Rows[tblCount].Selected = false;
                }
            }
            conn.Close();

            string backupCompletedMessage = "Backup Completed successfully. Do you want to view the Backup Excel Sheet?";
            if (MessageBox.Show(backupCompletedMessage, "Backup Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (System.Diagnostics.Process prc = new System.Diagnostics.Process())
                {
                    prc.StartInfo.FileName = destination + ClsCommon.DatabaseName + ".mdb";
                    prc.Start();
                }
            }
        }

        private void BackupXML(string destination)
        {
            ClsCommon common = new ClsCommon();
            System.IO.DirectoryInfo dir = new DirectoryInfo(destination + "\\" + ClsCommon.DatabaseName + "XML");

            //vérifier si le dossier existe ou pas
            if (Directory.Exists(destination + "\\" + ClsCommon.DatabaseName + "XML"))
            {
                if (MessageBox.Show("Delete existing Directory " + destination + ClsCommon.DatabaseName + "XML", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (System.IO.FileInfo file in dir.GetFiles())
                        file.Delete();
                    Directory.Delete(destination + "\\" + ClsCommon.DatabaseName + "XML");
                }
            }

            //crèer un nouveau dossier
            Directory.CreateDirectory(destination + "\\" + ClsCommon.DatabaseName + "XML");

            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            string tableName = string.Empty;
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = m_sConn1;
            conn.Open();

            for (int tblCount = 0; tblCount <= dgrdDatabase.Rows.Count - 1; tblCount++)
            {
                //Récupérer les données de la table
                //créer une chaîne de colonnes
                tableName = Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[0].Value);
                if (tableName.Length > 0)
                {
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Copying Data...";
                    dgrdDatabase.Refresh();

                    if (tblCount > 5)
                    {
                        dgrdDatabase.FirstDisplayedScrollingRowIndex = tblCount - 5;
                    }

                    dgrdDatabase.Rows[tblCount].Selected = true;

                    DataSet dstTemp = common.GetTableData(tableName);
                    dstTemp.WriteXml(destination + "\\" + ClsCommon.DatabaseName + "XML\\" + tableName + ".xml"); 
                    
                    dgrdDatabase.Rows[tblCount].Cells[2].Value = "Completed.";
                    dgrdDatabase.Refresh();
                    dgrdDatabase.Rows[tblCount].Selected = false;
                }
            }
            conn.Close();

            string backupCompletedMessage = "Backup Completed successfully. Do you want to view the Folder?";
            if (MessageBox.Show(backupCompletedMessage, "Backup Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string argument = @"/select, " + destination + ClsCommon.DatabaseName + "XML";

                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
        }

        private void BackupSQL()
        {
            //////pas supporte dbo.
            ////clsCommon common = new clsCommon();
            ////if (File.Exists(destination + clsCommon.databaseName + ".mdb"))
            ////{
            ////    if (MessageBox.Show("Delete existing workbooks (" + destination + clsCommon.databaseName + ".mdb", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            ////    {
            ////        File.Delete(destination + clsCommon.databaseName + ".mdb");
            ////    }
            ////}

            //crèer la base de donnès
            ClsCommon common = new ClsCommon();
            common.CreateSQLDatabase(ClsCommon.ServerNameDestination, ClsCommon.LoginNameDestination, ClsCommon.PasswordNameDestination, ClsCommon.DatabaseName);

            //Catalog cat = new Catalog();
            //cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destination + "\\" + clsCommon.databaseName + ".mdb;User Id=;Password=;");

            //si Excel, 
            //créer un classeur avec un nom de table EmployeeData. La table a different champs
            string tableName = string.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=" + ClsCommon.ServerNameDestination + ";Initial Catalog=" + ClsCommon.DatabaseName + ";User Id=" + ClsCommon.LoginNameDestination + ";Password=" + ClsCommon.PasswordNameDestination + ";"; 
            conn.Open();

            for (int tblCount = 0; tblCount <= dgrdDatabase.Rows.Count - 1; tblCount++)
            {
                //Récupérer les données de la table
                //créer une chaîne de colonnes
                tableName = Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[0].Value) + "." + Convert.ToString(dgrdDatabase.Rows[tblCount].Cells[1].Value);
                if (tableName.Length > 0)
                {
                    if (InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            dgrdDatabase.Rows[tblCount].Cells[2].Value = "Copying Data...";
                            dgrdDatabase.Refresh();

                            if (tblCount > 5)
                            {
                                dgrdDatabase.FirstDisplayedScrollingRowIndex = tblCount - 5;
                            }

                            dgrdDatabase.Rows[tblCount].Selected = true;

                            DataSet dstTemp = common.GetTableData(tableName);

                            //extraire les colonnes de la table de données
                            string cols = string.Empty;
                            for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                cols = cols + "[" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "] varchar(255), ";
                            }
                            cols = cols.Substring(0, cols.Length - 2);
                            //cols = "Invalid varchar(250)";

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;

                            //if (!common.DoesTableExists(tableName, conn.ConnectionString))
                            //{
                            cmd.CommandText = "CREATE TABLE [" + tableName + "] (" + cols + ")";
                            cmd.ExecuteNonQuery();
                            //}

                            //extraire les colonnes de la table source
                            string colsDest = string.Empty;
                            string colsValues = string.Empty;
                            string fieldValue = string.Empty;

                            for (int i = 0; i <= dstTemp.Tables[0].Columns.Count - 1; i++)
                            {
                                colsDest = colsDest + "left([" + dstTemp.Tables[0].Columns[i].ColumnName.ToString() + "],255) , ";
                            }
                            colsDest = colsDest.Substring(0, colsDest.Length - 2);

                            dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                            dgrdDatabase.Refresh();
                            //Insérez l'enregistrement de la table
                            for (int rowData = 0; rowData <= dstTemp.Tables[0].Rows.Count - 1; rowData++)
                            {
                                //dgrdDatabase.Rows[tblCount].Cells[2].Value = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred." ;
                                lblStatus.Text = dstTemp.Tables[0].Rows.Count + " of " + rowData + " rows transferred.";
                                lblStatus.Refresh();

                                //dgrdDatabase.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                                //dgrdDatabase.Refresh(); 

                                for (int colData = 0; colData <= dstTemp.Tables[0].Columns.Count - 1; colData++)
                                {
                                    fieldValue = dstTemp.Tables[0].Rows[rowData][colData].ToString();
                                    if (fieldValue.Contains(""))
                                    {
                                        fieldValue = string.Empty;
                                    }

                                    if (fieldValue.Length >= 250)
                                    {
                                        colsValues = colsValues + "'" + fieldValue.Substring(1, 250).Replace("'", "''") + "',";
                                    }
                                    else
                                    {
                                        colsValues = colsValues + "'" + fieldValue.Replace("'", "''") + "',";
                                    }

                                }
                                colsValues = colsValues.Substring(0, colsValues.Length - 1);

                                cmd.CommandText = "INSERT INTO [" + tableName + "] values (" + colsValues + ")";
                                cmd.ExecuteNonQuery();
                                colsValues = string.Empty;
                            }

                            //cmd.CommandText = "insert into [" + tableName + "] select " + colsDest + " from [" + clsCommon.databaseName + "].[dbo].[" + tableName + "]";
                            //cmd.ExecuteNonQuery();
                            dgrdDatabase.Rows[tblCount].Cells[2].Value = "Completed.";
                            dgrdDatabase.Refresh();
                            dgrdDatabase.Rows[tblCount].Selected = false;
                        });
                    }
                }
            }
            conn.Close();
            conn.Dispose();

            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    btnStartBackup.Enabled = false;
                }));
            }

            //string backupCompletedMessage = "Backup Completed successfully.";
            //MessageBox.Show(backupCompletedMessage);
            //this.Close();
            ////////if (MessageBox.Show(backupCompletedMessage, "Backup Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            ////////{
            ////////    using (System.Diagnostics.Process prc = new System.Diagnostics.Process())
            ////////    {
            ////////        prc.StartInfo.FileName = destination + clsCommon.databaseName + ".mdb";
            ////////        prc.Start();
            ////////    }
            ////////}
        }


    }
}