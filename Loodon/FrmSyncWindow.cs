using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Data.OleDb;
using ADOX;
using Loodon;

namespace SQLBackup
{
    public partial class frmSyncWindow : Form, IDisposable
    {
        public enum SyncDirection
        {
            SourceToDestination,
            DestinationToSource
        }


        public string sourceServer = string.Empty;
        public string sourceUsername = string.Empty;
        public string sourcePassword = string.Empty;
        public string sourceDatabase = string.Empty;

        public string destinationServer = string.Empty;
        public string destinationUsername = string.Empty;
        public string destinationPassword = string.Empty;
        public string destinationDatabase = string.Empty;
        public bool destinationIsLocal = false;
        public bool sourceIsLocal = false;

        public string LabelMessage = string.Empty;

        DataSet dstListBox = new DataSet();

        //DataSet dst = new DataSet();
        //string primaryKeySourceTable = string.Empty;
        //string destinationTable = string.Empty;

        public SyncDirection direction = SyncDirection.DestinationToSource;

        public frmSyncWindow(string SourceServer, string SourceUsername, string SourcePassword, string SourceDatabase,
            string DestinationServer, string DestinationUsername, string DestinationPassword, string DestinationDatabase, SyncDirection Direction, bool DestinationIsLocal, bool SourceIsLocal)
        {
            sourceServer = SourceServer;
            sourceUsername = SourceUsername;
            sourcePassword = SourcePassword;
            sourceDatabase = SourceDatabase;

            destinationServer = DestinationServer;
            destinationUsername = DestinationUsername;
            destinationPassword = DestinationPassword;
            destinationDatabase = DestinationDatabase;
            destinationIsLocal = DestinationIsLocal;
            sourceIsLocal = sourceIsLocal;

            direction = Direction;

            InitializeComponent();
            this.Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSyncWindow_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Note: Data Synchronization depends on the Primary key inside the Table, if any of the Table not contains" +
                "the Primary key, then that table will be skipped.", "Loodon: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (direction == SyncDirection.DestinationToSource)
            {
                UpdateMessageLabel("Destination to Source Data Synchronization");
                //lstStatus.Items.Add("Destination to Source Data Synchronization");
                PopulateDestinationDatabase();
            }
            else if (direction == SyncDirection.SourceToDestination)
            {
                UpdateMessageLabel("Source to Destination Data Synchronization");
                //lstStatus.Items.Add("Source to Destination Data Synchronization");
                PopulateSourceDatabase();
            }
        }

        private void btnStartSync_Click(object sender, EventArgs e)
        {
            if (dstListBox.Tables.Count < 1)
            {
                dstListBox.Tables.Add("ListBoxDataset");
                dstListBox.Tables["ListBoxDataset"].Columns.Add("Tables");
            }
            else
            {
                dstListBox.Clear();
            }

            for (int i = 0; i <= lstSource.Items.Count - 1; i++)
            {
                lstSource.SelectedIndex = i;
                lstSource.Refresh();

                dstListBox.Tables["ListBoxDataset"].Rows.Add(lstSource.Text);
            }          

            SyncTable_SQL();
            MessageBox.Show("Synchronization process Completed.", "Loodon: Data Synchronization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Bulkcopy(string SourceSQLQuery, string DestinationTableName)
        {
            // Establishing connection
            SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
            cb.DataSource = sourceServer;
            cb.InitialCatalog = sourceDatabase;
            cb.Password = sourcePassword;
            cb.UserID = sourceUsername;

            cb.IntegratedSecurity = true;
            SqlConnection cnn = new SqlConnection(cb.ConnectionString);

            // Getting source data
            SqlCommand cmd = new SqlCommand(SourceSQLQuery, cnn);
            cnn.Open();
            SqlDataReader rdr = cmd.ExecuteReader(); 

            // Initializing an SqlBulkCopy object for Destination
            SqlBulkCopy sbc = new SqlBulkCopy("server=" + destinationServer + ";database=" + destinationDatabase + ";Integrated Security=SSPI");

            // Copying data to destination
            sbc.DestinationTableName = DestinationTableName;
            sbc.WriteToServer(rdr);

            // Closing connection and the others
            sbc.Close();
            rdr.Close();
            cnn.Close(); 
        }

        private void SyncTable_SQL()
        {
            //Steps of Function
            //1. Start a loop that will take the Table one by one from the List box
            //2. Take the Primary keys from the Source and Destination Tables
            //3. It first takes out all the Primary keys from the Destination Table and then matches those primary keys
            //   with the source table and then it Inserts the Difference Records in the Destination table
            //-------------------------------------------------------------------------------------------------------------------

            string primaryKeySourceTable = string.Empty;
            string sourceTable = string.Empty;
            string destinationTable = string.Empty;
            string sqlQuery = string.Empty;
            string directoryPath = "C:\\LoodonTemp";
            string insertStatement = string.Empty;
            string insertColumns = string.Empty;
            string insertColumnValues = string.Empty;
            DataSet dst = new DataSet();
            string sourceTablePath = string.Empty;
            string destinationTablePath = string.Empty;
            string tempQuery = string.Empty;
            bool identityColumnExists = false;

            ClsCommon common = new ClsCommon();
            for (int i = 0; i <= lstSource.Items.Count - 1; i++)
            {
                //Selects the first TableName from the Source List of Tables
                //Selects the list box i number item and updates the Status Label message
                lstSource.SelectedIndex = i;
                lstSource.Refresh();

                sourceTable = lstSource.Text;
                UpdateMessageLabel("Table Processing: " + sourceTable);
                //lstStatus.Items.Add("Table Processing: " + sourceTable);
                //lstStatus.Refresh();
                destinationTable = sourceTable;
                //LogActions("Table: " + sourceTable + " | " + DateTime.Now); //Log Entry

                //Collect the table information: Check the Primary key field in the Source Table in the Source Database
                //------------------------------------------------------------------------------           
                try
                {
                    UpdateMessageLabel("Fetching Primary key: " + sourceTable);
                    //lstStatus.Items.Add("Fetching Primary key: " + sourceTable);
                    //lstStatus.Refresh();
                    //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                    //lstStatus.Refresh();

                    primaryKeySourceTable = common.GetPrimaryKeyofDatabaseTable(sourceServer, sourceUsername, sourcePassword, sourceDatabase,
                    sourceTable);

                    UpdateMessageLabel("Primary key fetched successfully.");
                    //lstStatus.Items.Add("Primary key fetched successfully.");
                    //lstStatus.Refresh();
                    //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                    //lstStatus.Refresh();

                }
                catch (Exception ex)
                {
                    UpdateMessageLabel("Primary key fetching Error: " + sourceTable);
                    //lstStatus.Items.Add("Primary key fetching Error: " + sourceTable);
                    //lstStatus.Items.Add("----------------------------------------------------------------");
                    //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                    //lstStatus.Refresh();
                }

                //Get Columns of the Table
                //dst = common.GetColumnsofTable(sourceServer, sourceUsername, sourcePassword, sourceDatabase, sourceTable);

                //If there is no primary key, skip the current table
                //---------------------------------------------------------------------------------
                if (primaryKeySourceTable.Length > 0)
                {
                    //--------------------------------------------------------------------------------------------------
                    //Insert the Unmatched data in the Destination Table.
                    //Create Insert statements for the Unmatched data and saves the Text statements in a Text File
                    //--------------------------------------------------------------------------------------------------
                    //Fetch the Columns for the Insert Statement
                    //---------------------------------------------------------------------------------------------------
                    ////for (int dstColCount = 0; dstColCount <= dst.Tables[0].Rows.Count -1; dstColCount++)
                    ////{
                    ////    insertColumns = insertColumns + dst.Tables[0].Rows[dstColCount][0].ToString() + ",";
                    ////}

                    //A little housekeeping with the last comma
                    ////insertColumns = insertColumns.Substring(0, insertColumns.Length - 1);
                    //---------------------------------------------------------------------------------------------------
                    //Get the Values for Insert Statement
                    //---------------------------------------------------------------------------------------------------                  
                    try
                    {
                        UpdateMessageLabel("Starting Sync | Table: " + destinationTable);
                        //lstStatus.Items.Add("Starting Sync | Table: " + destinationTable);
                        //lstStatus.Refresh();

                        if (sourceIsLocal == true && destinationIsLocal == true)
                        {
                            sourceTablePath = sourceTable;
                            destinationTablePath= destinationTable;
                        }
                        else if (sourceIsLocal == false && destinationIsLocal == false)
                        {
                            sourceTablePath = "[" + sourceServer + "]." + sourceDatabase + "." + sourceTable;
                            destinationTablePath = "[" + destinationServer + "]." + destinationDatabase + "." + destinationTable; ;
                        }
                        else if (sourceIsLocal == true && destinationIsLocal == false)
                        {
                            sourceTablePath = sourceTable;
                            destinationTablePath = "[" + destinationServer + "]." + destinationDatabase + "." + destinationTable; ;                            
                        }
                        else if (sourceIsLocal == false && destinationIsLocal == true)
                        {
                            sourceTablePath = "[" + sourceServer + "]." + sourceDatabase + "." + sourceTable;
                            destinationTablePath = destinationTable;
                        }

                        string [] strTableArray = destinationTable.Split('.');                                               
                        
                        //Check whether Identity insert is enabled or not
                        tempQuery = "SELECT IDENT_SEED(TABLE_NAME) AS Seed, IDENT_INCR(TABLE_NAME) AS Increment, IDENT_CURRENT(TABLE_NAME) AS Current_Identity, " +
                                            "TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'TableHasIdentity') = 1" +
                                            "AND TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = '" + strTableArray[1].Replace("[","").Replace("]","") + "'";

                        if (ClsCommon.IdentityColumnExists(destinationServer, destinationUsername, destinationPassword, destinationDatabase, tempQuery))
                        {
                            identityColumnExists = true;
                        }                       

                        //if (identityColumnExists)
                        //{
                        //    insertStatement = insertStatement + "SET IDENTITY_INSERT " + destinationTablePath + " ON;";
                        //}

                        //Get the Max id of the Destination Table
                        string queryMaxID = "SELECT MAX(" + primaryKeySourceTable + ") FROM " + destinationTablePath;
                        long maxID = ClsCommon.GetMaxId(destinationServer, destinationUsername, destinationPassword, destinationDatabase, queryMaxID);

                        //insertStatement = insertStatement + "Insert into " + destinationTablePath +
                        //"(" + insertColumns + ") SELECT " + insertColumns + " FROM " + sourceTablePath + " WHERE " + primaryKeySourceTable +
                        //" NOT IN (SELECT " + primaryKeySourceTable + " FROM " + destinationTablePath + ");";

                        //if (rdbDirect.Checked)
                        //{
                        //    insertStatement = insertStatement + "Insert into " + destinationTablePath +
                        //    "(" + insertColumns + ") SELECT " + insertColumns + " FROM " + sourceTablePath + " WHERE " + primaryKeySourceTable +
                        //    " > " + maxID;
                        //}
                        //else if (rdbLoop.Checked)
                        //{
                            insertStatement = "SELECT * FROM " + sourceTablePath + " WHERE " + primaryKeySourceTable +
                            " > " + maxID;
                        //}

                        //if (identityColumnExists)
                        //{
                        //    insertStatement = insertStatement + "SET IDENTITY_INSERT " + destinationTablePath + " OFF;";
                        //}

                        tempQuery = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        UpdateMessageLabel("Error: " + ex.Message);
                        //lstStatus.Items.Add("Error: " + ex.Message);
                        //lstStatus.Refresh();
                        //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                        //lstStatus.Refresh();
                    }                    
                    

                    //Write the Insert statements in the Text file and executes them finally
                    //Followiing this logic: Write 100 Insert statements and execute it, and delete the text file content
                    //----------------------------------------------------------------------
                    //sw.WriteLine(insertStatement);
                    try
                    {
                        UpdateMessageLabel("Current Table: " + destinationTable);
                        //lstStatus.Items.Add("Current Table: " + destinationTable);
                        //lstSource.SelectedIndex = lstStatus.Items.Count - 1;
                        //lstStatus.Refresh();
                    }
                    catch (Exception)
                    {
                        
                    }

                    try
                    {
                        Bulkcopy(insertStatement, destinationTable);

                        //Check the type of Synchronization and proceed
                        //if (rdbDirect.Checked)
                        //{
                        //    ////common.ExecuteInsertStatements(destinationServer, destinationUsername, destinationPassword, destinationDatabase, insertStatement);
                        //}
                        //else if (rdbLoop.Checked)
                        //{                           

                        //    //Take the values in a Dataset                           

                        //    ////DataSet dstData = new DataSet();
                        //    ////dstData = common.ExecuteInsertStatementsForDataset(destinationServer, destinationUsername, destinationPassword, destinationDatabase, insertStatement);

                        //    ////BackupExcelSeparateSheet("C:", dstData, destinationTable);

                        //    //////Initialize the insertStatement variable for fresh use
                        //    ////insertStatement = string.Empty;

                        //    ////string columnsInsert = string.Empty;

                        //    //for (int iRow = 0; iRow <= dstData.Tables[0].Rows.Count - 1; iRow++)
                        //    //{
                        //    //    if (identityColumnExists)
                        //    //    {
                        //    //        insertStatement = insertStatement + "SET IDENTITY_INSERT " + destinationTablePath + " ON;";
                        //    //    }

                        //    //    for (int j = 0; j <= dstData.Tables[0].Columns.Count - 1; j++)
                        //    //    {    
                        //    //        if(dstData.Tables[0].Columns[j].DataType == typeof(string))
                        //    //        {
                        //    //            columnsInsert = columnsInsert + ",'" + dstData.Tables[0].Rows[iRow][j].ToString() + "'";                                    
                        //    //        }
                        //    //        else if (dstData.Tables[0].Columns[j].DataType == typeof(string))
                        //    //        {
                        //    //            columnsInsert = columnsInsert + "," + dstData.Tables[0].Rows[iRow][j].ToString();                                    
                        //    //        }
                        //    //        else
                        //    //        {
                        //    //            columnsInsert = columnsInsert + ",'" + dstData.Tables[0].Rows[iRow][j].ToString() + "'";
                        //    //        }    
                        //    //    }                                

                        //    //    //A lil homework with the columns values list to remove the first comma
                        //    //    columnsInsert = columnsInsert.Substring(1,columnsInsert.Length -1);

                        //    //    //Override the InsertStatement variable
                        //    //    insertStatement = insertStatement + "Insert into " + destinationTablePath + "(" + columnsInsert + ")";
                        //    //    common.ExecuteInsertStatements(destinationServer, destinationUsername, destinationPassword, destinationDatabase, insertStatement);
                                
                        //    //    if (identityColumnExists)
                        //    //    {
                        //    //        insertStatement = insertStatement + "SET IDENTITY_INSERT " + destinationTablePath + " OFF;";
                        //    //    }
                        //    //}                  
                                                       
                        //}
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Source  + "\n" + ex.Message + "\n\nResolution: \n1. Select Local Server [Destination], if the Destination is on the Local Server." +
                            "Table: " + destinationTablePath + "\n\nSynchronization is now aborted.";

                        //MessageBox.Show(error, "Loodon: Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        //break;
                        //this.Close();
                    }

                    try
                    {
                        UpdateMessageLabel("Sync Finished for Table: " + destinationTable);
                        UpdateMessageLabel("----------------------------------------------------------------");
                        //lstStatus.Items.Add("Sync Finished for Table: " + destinationTable);
                        //lstStatus.Items.Add("----------------------------------------------------------------");
                        //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                        //lstStatus.Refresh();
                    }
                    catch (Exception)
                    {                        
                        
                    }                    
                }
                insertColumns = string.Empty;
                insertColumnValues = string.Empty;
                insertStatement = string.Empty;
                primaryKeySourceTable = string.Empty;
                identityColumnExists = false;
            }
        }            
        
        private void SyncTable()
        {
            //Steps of Function
            //1. Start a loop that will take the Table one by one from the List box
            //2. Take the Primary keys from the Source and Destination Tables
            //3. It first takes out all the Primary keys from the Destination Table and then matches those primary keys
            //   with the source table and then it Inserts the Difference Records in the Destination table
            //-------------------------------------------------------------------------------------------------------------------

            string primaryKeySourceTable = string.Empty;
            string sourceTable = string.Empty;
            string destinationTable = string.Empty;
            string sqlQuery = string.Empty;
            string directoryPath = "C:\\LoodonTemp";
            string insertStatement = string.Empty;
            string insertColumns = string.Empty;
            string insertColumnValues = string.Empty;
            DataSet dst = new DataSet();

            ClsCommon common = new ClsCommon();

            //Source to Destination
            //Manual Process: To collect the Primay keys ids in a text file and compare them with the Destination Database table
            //===================================================================================================================
            //Create Temporary Folder for these tasks on C:
            //--------------------------------------------------------------------------------------------------
            //if (!Directory.Exists(directoryPath))
            //{
            //    Directory.CreateDirectory(directoryPath); //Creates Temporary directory
            //}

            //File.CreateText(directoryPath + "\\" + "LoodonInsertStatements.txt").Dispose();
            //StreamWriter sw = File.AppendText(directoryPath + "\\" + "LoodonInsertStatements.txt");

            //if (InvokeRequired)
            //{
            //    lstStatus.Invoke((MethodInvoker)delegate
            //    {
                    //lstStatus.Items.Add("ddfdf");
                    //for (int i = 0; i <= dstListBox.Tables["ListBoxDataset"].Rows.Count - 1; i++)
                    for (int i = 0; i <= lstSource.Items.Count - 1; i++)
                    {
                        //Selects the first TableName from the Source List of Tables
                        //Selects the list box i number item and updates the Status Label message
                        lstSource.SelectedIndex = i;
                        lstSource.Refresh();

                        sourceTable = lstSource.Text;
                        //lstStatus.Items.Add("Table Processing: " + sourceTable);
                        //lblStatus.Refresh();

                        LogActions("Table: " + sourceTable + " | " + DateTime.Now);

                        //Collect the table information: Check the Primary key field in the Source Table in the Source Database
                        //------------------------------------------------------------------------------           
                        try
                        {
                            primaryKeySourceTable = common.GetPrimaryKeyofDatabaseTable(sourceServer, sourceUsername, sourcePassword, sourceDatabase,
                            sourceTable);

                            //lstStatus.Items.Add("Primary key fetched successfully.");

                        }
                        catch (Exception ex)
                        {
                            //lstStatus.Items.Add("Primary key fetching Error: " + ex.Message);

                        }

                        //If there is no primary key, skip the current table
                        //---------------------------------------------------------------------------------
                        if (primaryKeySourceTable.Length > 0)
                        {

                            //Collect the Primary key field values in a temporary file
                            //--------------------------------------------------------------------------------
                            string conString = "server=" + destinationServer + ";uid=" + destinationUsername + ";pwd=" + destinationPassword + "; database=" + destinationDatabase;
                            SqlConnection con = new SqlConnection(conString);
                            con.Open();

                            StringBuilder builder = new StringBuilder();
                            try
                            {
                                //Select the primary key of the Destination Table that will match with the Source table and the rest data in the 
                                //source table will be updated in the Destination table
                                destinationTable = sourceTable; //For ease of convinience: sourceTable and destinationTable name are same but
                                //as the connectionstring is for destinationServer, we are assumint the same name for the destinationTable

                                string CommandText = "Select " + primaryKeySourceTable + " from " + destinationTable + " order by " + primaryKeySourceTable;
                                SqlCommand cmd = new SqlCommand(CommandText);
                                cmd.Connection = con;

                                SqlDataReader rdr = cmd.ExecuteReader();


                                while (rdr.Read())
                                {
                                    builder.Append(rdr[0].ToString() + ",");
                                }

                                //Match the data in the Source Table based on the Primary key of the Destination Database Table
                                //Create Query to get the Unmatch Data
                                //---------------------------------------------------------------------------------------------
                                if (builder.ToString().Length < 1)
                                {
                                    sqlQuery = "Select * from " + sourceTable + " order by " + primaryKeySourceTable ; //If no values in builder variable, then take the whole data as nothing to match.
                                }
                                else
                                {
                                    sqlQuery = "Select * from " + sourceTable + " where " + primaryKeySourceTable + " not in (" + builder.ToString().Substring(0, builder.ToString().Length - 1) + ") order by " + primaryKeySourceTable ;
                                }
                                //DataSet dst = new DataSet();

                                dst = common.GetUnMatchData(sourceServer, sourceUsername, sourcePassword, sourceDatabase, sqlQuery);

                                //lstStatus.Items.Add("Unmatch Data Collected: Rows Count: " + dst.Tables[0].Rows.Count.ToString());
                                //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                                //lstStatus.Refresh();
                                //Thread td = new Thread(new ThreadStart(SyncTable));
                                //td.Start();
                            }
                            catch (Exception)
                            {

                            }

                            //--------------------------------------------------------------------------------------------------
                            //Insert the Unmatched data in the Destination Table.
                            //Create Insert statements for the Unmatched data and saves the Text statements in a Text File
                            //--------------------------------------------------------------------------------------------------
                            //Fetch the Columns for the Insert Statement
                            //---------------------------------------------------------------------------------------------------
                            for (int dstColCount = 0; dstColCount <= dst.Tables[0].Columns.Count - 1; dstColCount++)
                            {
                                //Igonre the Primary key
                                if (dst.Tables[0].Columns[dstColCount].ColumnName != primaryKeySourceTable)
                                {
                                    insertColumns = insertColumns + dst.Tables[0].Columns[dstColCount].ColumnName + ",";
                                }
                            }
                            //A little housekeeping with the last comma
                            insertColumns = "(" + insertColumns.Substring(0, insertColumns.Length - 1) + ")";
                            //---------------------------------------------------------------------------------------------------

                            //Get the Values for Insert Statement
                            //---------------------------------------------------------------------------------------------------
                            for (int dstRowCount = 0; dstRowCount <= dst.Tables[0].Rows.Count - 1; dstRowCount++)
                            {
                                insertColumnValues = string.Empty;

                                for (int dstColCount = 0; dstColCount <= dst.Tables[0].Columns.Count - 1; dstColCount++)
                                {
                                    //Igonre the Primary key
                                    if (dst.Tables[0].Columns[dstColCount].ColumnName != primaryKeySourceTable)
                                    {

                                        //Check the type of the column and creates the string based on that
                                        //Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 SByte Single String TimeSpan UInt16 UInt32 UInt64
                                        if (dst.Tables[0].Columns[dstColCount].DataType == System.Type.GetType("System.DateTime"))
                                        {
                                            //insertColumnValues = insertColumnValues + "'" + Convert.ToDateTime(dst.Tables[0].Rows[dstRowCount][dstColCount]) + "'" + ",";
                                            insertColumnValues = insertColumnValues + "'" + DateTime.ParseExact(dst.Tables[0].Rows[dstRowCount][dstColCount].ToString(), "mm/dd/yyyy", null) +  "'" + ",";

                                        }
                                        else if (dst.Tables[0].Columns[dstColCount].DataType == System.Type.GetType("System.String"))
                                        {
                                            insertColumnValues = insertColumnValues + "'" + dst.Tables[0].Rows[dstRowCount][dstColCount].ToString().Replace("'", "''") + "'" + ",";
                                        }
                                        else if (dst.Tables[0].Columns[dstColCount].DataType == System.Type.GetType("System.Int32"))
                                        {
                                            if (dst.Tables[0].Rows[dstRowCount][dstColCount].ToString().Length < 1)
                                            {
                                                insertColumnValues = insertColumnValues + "NULL,";
                                            }
                                            else
                                            {
                                                insertColumnValues = insertColumnValues + dst.Tables[0].Rows[dstRowCount][dstColCount].ToString() + ",";
                                            }
                                        }
                                        else if (dst.Tables[0].Columns[dstColCount].DataType == System.Type.GetType("System.Decimal"))
                                        {
                                            insertColumnValues = insertColumnValues + dst.Tables[0].Rows[dstRowCount][dstColCount].ToString() + ",";
                                        }
                                        else if (dst.Tables[0].Columns[dstColCount].DataType == System.Type.GetType("System.Boolean"))
                                        {
                                            if (dst.Tables[0].Rows[dstRowCount][dstColCount].ToString().Length < 1)
                                            {
                                                insertColumnValues = insertColumnValues + "NULL,";
                                            }
                                            else
                                            {
                                                insertColumnValues = insertColumnValues + "'" + dst.Tables[0].Rows[dstRowCount][dstColCount].ToString() + "',";
                                            }
                                        }
                                        else
                                            insertColumnValues = insertColumnValues + "'" + dst.Tables[0].Rows[dstRowCount][dstColCount].ToString() + "'" + ",";
                                    }
                                }

                                //A little housekeeping with the last comma
                                insertColumnValues = "(" + insertColumnValues.Substring(0, insertColumnValues.Length - 1) + ")";

                                //Create the Insert Statement
                                insertStatement = "INSERT INTO " + destinationTable + insertColumns + " VALUES " + insertColumnValues;

                                //Write the Insert statements in the Text file and executes them finally
                                //Followiing this logic: Write 100 Insert statements and execute it, and delete the text file content
                                //----------------------------------------------------------------------
                                //sw.WriteLine(insertStatement);
                                ClsCommon.ExecuteInsertStatements(destinationServer, destinationUsername, destinationPassword, destinationDatabase, insertStatement);
                                //lstStatus.Items.Add("tests");
                                                              
                                //LabelMessage = "Sync Count: " + dstRowCount.ToString();
                                //lstStatus.Items.Add("tests");
                                //lstStatus.SelectedIndex = lstStatus.Items.Count - 1;
                                //lstStatus.Refresh();

                                
                            }
                            //---------------------------------------------------------------------------------------------------
                            //File.Delete(directoryPath + "\\" + "LoodonInsertStatements.txt");//Deletes the Temp Insert Statement file
                            insertColumns = string.Empty;
                            insertColumnValues = string.Empty;
                            insertStatement = string.Empty;
                            primaryKeySourceTable = string.Empty;
                        }
                //    }                    
                //});

                
                dst.Clear();
            }            
       }

        private void UpdateMessageLabel(string Message)
        {
            txtLog.Text = txtLog.Text + "\n" + Message;
            txtLog.Refresh();
        }

        private void LogActions(string LogMessage)
        {
            string filePath = "C:\\LoodonTemp\\LoodonLog.txt";

            FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(aFile);
            sw.WriteLine(LogMessage);
            
            sw.Close();
            aFile.Close();
        }

        private void PopulateSourceDatabase()
        {
            ClsCommon common = new ClsCommon();

            lstSource.DisplayMember = "TableName";
            lstSource.DataSource = common.GetTablesfromDbDetails(sourceServer, sourceUsername, sourcePassword, sourceDatabase).Tables[0];
            
            //Dispose(true);
        }

        private void PopulateDestinationDatabase()
        {
            ClsCommon common = new ClsCommon();

            lstDestination.DisplayMember = "TableName";
            lstDestination.DataSource = common.GetTablesfromDbDetails(destinationServer, destinationUsername, destinationPassword, destinationDatabase).Tables[0];
            

            //Dispose(true);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
