using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using LoodonDAL;
using NumberedTextBox;
using SQLBackup;

namespace Loodon
{
    public partial class FrmMain : Form
    {
        //Configuration and Initialization variables
        //Syntax Coloring
        //============================================
        //Configuration and Initialization variables
        private string _commandTyped = string.Empty;
        private string _commandFormat = string.Empty;
        private string _commandContainer = string.Empty;
        private int _startIndex = 0;

        private int _oem7Count; //To track the Variables
        private bool _greyToBlack; //Instructs whether change the color from grey to black especially when the characters are ( )
        private bool _functionRecolor = false; //Whether to check for function to color it or not
        private int _functionSelectionStart = 0;

        string _selectedItem = string.Empty;
        string _currentSelectedNode = string.Empty;

        string _currentCommand = string.Empty;

        string _selectedStoredProcedure = string.Empty;

        private DataSet _dstConnections = new DataSet(); //Take care of all the open connections
        private DataSet _dstTablesDetails = new DataSet(); // This will be used by many other instances

        /// <summary>
        /// Reverses the string: For - Syntax Coloring
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns>Reverse String</returns>
        private string ReverseString(string sourceString)
        {
            var response = string.Empty;
            for (var i = sourceString.Length - 1; i >= 0; i--)
            {
                response = response + sourceString.Substring(i, 1);
            }
            return response;
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        //HANDLES THE LINE NUMBERS
        private static void updateNumberLabel()
        {
            ////we get index of first visible char and number of first visible line
            //Point pos = new Point(0, 0);
            //int firstIndex = txtQuery.GetCharIndexFromPosition(pos);
            //int firstLine = txtQuery.GetLineFromCharIndex(firstIndex);

            ////now we get index of last visible char and number of last visible line
            //pos.X = ClientRectangle.Width;
            //pos.Y = ClientRectangle.Height;
            //int lastIndex = txtQuery.GetCharIndexFromPosition(pos);
            //int lastLine = txtQuery.GetLineFromCharIndex(lastIndex);

            ////this is point position of last visible char, we'll use its Y value for calculating numberLabel size
            //pos = txtQuery.GetPositionFromCharIndex(lastIndex);


            ////finally, renumber label
            //numberLabel.Text = "";
            //for (int i = firstLine; i <= lastLine + 1; i++)
            //{
            //    numberLabel.Text += i + 1 + "\n";
            //}
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //===For-Syntax Coloring
                _commandFormat = "COMMAND";

                numberedTextBoxUC1.Height = tabEditor.TabPages[0].Height - 2;
                numberedTextBoxUC1.Width = tabEditor.TabPages[0].Width - 2;
                numberedTextBoxUC1.Top = 1;
                numberedTextBoxUC1.Left = 1;

                BindTables(treeDBObjects);
               
                //--Handling Tab Pages for query writing
                tabEditor.TabPages[0].Text = "Query" + (tabEditor.TabPages.Count).ToString();

                //Create the dataset definition for the Connections
                _dstConnections.Tables.Add("Connections");
                _dstConnections.Tables["Connections"].Columns.Add("Server");
                _dstConnections.Tables["Connections"].Columns.Add("User");
                _dstConnections.Tables["Connections"].Columns.Add("Password");
                _dstConnections.Tables["Connections"].Columns.Add("Database");

                _dstConnections.Tables["Connections"].Rows.Add(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, ClsCommon.DatabaseName);

                BindServerTabs(ClsCommon.ServerName + "#" + ClsCommon.DatabaseName);

                Text = ClsCommon.DatabaseName + " - " + "Loodon";
                // check for software update based on the registry settings [See options dialog box]
                var common = new ClsCommon();
                if (common.Read("UPDATES") == "TRUE")
                {
                    CheckSoftwareUpdate();
                }
            }
            catch (Exception ex)
            {
                var error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void BindServerTabs(string servername)
        {
            //Binds the Tab in the Server Tabs
            //The first tab will exist by default , it will get progress if there are more than 1 servers
            if (_dstConnections.Tables["Connections"].Rows.Count > 1)
            {

            }
            else
            {
                tabServers.TabPages[0].Text = servername;
            }
        }

        /// <summary>
        /// Binds the table structure in the Treeview control provided
        /// </summary>
        /// <param name="TabTreeView">Tree view</param>
        private void BindTables(TreeView TabTreeView)
        {
            TabTreeView.Nodes.Clear();
            var common = new ClsCommon();

            var dst = common.GetTables();
            _dstTablesDetails = dst;

            var dstViews = common.GetViews();
            var dstStoredProcedures = common.GetStoredProcedures();
            var dstTableValuedFunctions = common.GetTableValuedFunctions();
            var dstScalarValuedFunctions = common.GetScalarValuedFunctions();

            var nodeTables = new TreeNode("Tables") {ImageIndex = 0};
            for (var i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            {
                nodeTables.Nodes.Add(dst.Tables[0].Rows[i]["SchemaName"].ToString() + "." + dst.Tables[0].Rows[i]["TableName"].ToString());
                nodeTables.Nodes[i].ImageIndex = 2;
                nodeTables.Nodes[i].Nodes.Add("Columns");
                
                //Filling the Column Names
                var dstCol = common.GetColumns(Convert.ToInt32(dst.Tables[0].Rows[i]["object_Id"]));

                for (var col = 0; col <= dstCol.Tables[0].Rows.Count - 1; col++)
                {
                    nodeTables.Nodes[i].Nodes[0].ImageIndex = 0;
                    nodeTables.Nodes[i].Nodes[0].Nodes.Add(dstCol.Tables[0].Rows[col]["name"].ToString());
                    nodeTables.Nodes[i].Nodes[0].Nodes[col].ImageIndex = 1;
                }
            }

            var nodeViews = new TreeNode("Views");
            for (var i = 0; i <= dstViews.Tables[0].Rows.Count - 1; i++)
            {
                nodeViews.Nodes.Add(dstViews.Tables[0].Rows[i]["name"].ToString());
                nodeViews.Nodes[i].ImageIndex = 3;
            }

            var nodeStoredProcedure = new TreeNode("StoredProcedure");
            for (var i = 0; i <= dstStoredProcedures.Tables[0].Rows.Count - 1; i++)
            {
                nodeStoredProcedure.Nodes.Add(dstStoredProcedures.Tables[0].Rows[i]["name"].ToString());
                nodeStoredProcedure.Nodes[i].ImageIndex = 4;
            }

            var nodeTvFunctions = new TreeNode("Table-Valued Functions");
            for (var i = 0; i <= dstTableValuedFunctions.Tables[0].Rows.Count - 1; i++)
            {
                nodeTvFunctions.Nodes.Add(dstTableValuedFunctions.Tables[0].Rows[i]["name"].ToString());
                nodeTvFunctions.Nodes[i].ImageIndex = 5;
            }

            var nodeSvFunctions = new TreeNode("Scalar-Valued Functions");
            for (var i = 0; i <= dstScalarValuedFunctions.Tables[0].Rows.Count - 1; i++)
            {
                nodeSvFunctions.Nodes.Add(dstScalarValuedFunctions.Tables[0].Rows[i]["name"].ToString());
                nodeSvFunctions.Nodes[i].ImageIndex = 5;
            }

            var nodeFunctions = new TreeNode("Functions", new TreeNode[] { nodeTvFunctions, nodeSvFunctions });

            var nodeProgrammability = new TreeNode("Programmability", new TreeNode[] { nodeStoredProcedure, nodeFunctions  });

            var nodeDb = new TreeNode(ClsCommon.DatabaseName, new TreeNode[] {nodeTables, nodeViews, nodeProgrammability});

            TabTreeView.Nodes.AddRange(new TreeNode[] { nodeDb });  
            statuslblMessage.Text = LoodonDAL.ClsConfigMessages.Columnsrefreshmessage;
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void treeDBObjects_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            treeDBObjects.SelectedNode = treeDBObjects.GetNodeAt(e.X, e.Y);

            switch (e.Button)
            {
                case MouseButtons.Right when _selectedItem == ClsCommon.DatabaseName:
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Right when _selectedItem == "TableName":
                    cmsTableDef.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Right when _selectedItem == "Columns":
                    cmsColumns.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Right when _selectedItem == "ColumnChild":
                    contextMenuStrip5.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Right when _selectedItem == "Tables":
                    cmsTable.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Right when _selectedItem == "StoredProcedureChild":
                case MouseButtons.Right when _selectedItem == "StoredProcedureChild":
                    contextMenuStripStoredProcedure.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void treeDBObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedItem = e.Node.Text;
            _currentSelectedNode = e.Node.Text;

            if (e.Node.Parent == null) return;
            if (e.Node.Parent.ToString() == "TreeNode: Columns")
            {
                _selectedItem = "ColumnChild";
            }
            else if (e.Node.Parent.ToString() == "TreeNode: Tables")
            {
                _selectedItem = "Tables";
            }
            else if (e.Node.Parent.ToString() == "TreeNode: StoredProcedure")
            {
                _selectedItem = "StoredProcedureChild";
                _selectedStoredProcedure = e.Node.Text;
            }

            //We need to match only the table name, right now the Tree view grid shows the tablename with schema and hence we need to remove the 
            //schema name first from the currentSelectedNode and then it will match
            var expression = "Tablename = '" + _currentSelectedNode.Substring(_currentSelectedNode.IndexOf(".") + 1) + "'";
            var foundRows = _dstTablesDetails.Tables[0].Select(expression);

            if (foundRows.Length > 0)
            {
                _selectedItem = "TableName";
            }
        }

        private void splitContainer2_Panel1_Resize(object sender, EventArgs e)
        {
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmBackup().Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(-1);
            }
            catch (Exception)
            {
                Environment.Exit(-1);
            }
        }

        private void tsbtnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files|*.txt";
            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                CreateNewTab(System.IO.File.ReadAllText(openFileDialog1.FileName), openFileDialog1.FileName);
            }
        }

        private void SaveQueryFile(string Filename)
        {
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    TextWriter tw = new StreamWriter(Filename);
                    tw.WriteLine(numTextBox.GetText());
                    tw.Close();
                }
            }
        }

        private void SaveQueryFile(string Filename, string FileContent)
        {
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    TextWriter tw = new StreamWriter(Filename);
                    tw.WriteLine(FileContent);
                    tw.Close();
                }
            }
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            //Check whether the file is already saved or not, if yes no need to provide the savedialog, just save it directly.
            if (File.Exists(tabEditor.SelectedTab.Text))
            {
                SaveQueryFile(tabEditor.SelectedTab.Text);
            }
            else
            {
                saveFileDialog1.Filter = "Text Files|*.txt";
                var result = saveFileDialog1.ShowDialog();

                if (result != DialogResult.OK) return;
                SaveQueryFile(saveFileDialog1.FileName);
                tabEditor.TabPages[tabEditor.SelectedIndex].Text = saveFileDialog1.FileName;
            }
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://krayknot.com/contact.aspx");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutUs = new frmAboutUs();
            aboutUs.ShowDialog();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dataSynchronizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sync = new frmSync();
            sync.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var sync = new frmSync();
            sync.ShowDialog();
        }

        private void newDatabaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var message = "Database creation depends on the user permission you have logged on with.";
            MessageBox.Show(message, "Loodon: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var createDatabase = new FrmCreateDatabase();
            createDatabase.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    //Sets the database information to execute the query
                    ServerExistsinPervasiveDataset(tabServers.SelectedTab.Text); //serverNameConsolidate syntax = <server>.<database>
                                                
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.ExecuteSelectedQuery();
                }
            }            
        }

        private static void ServerExistsinPervasiveDataset(string serverName)
        {
            // Presuming the DataTable has a column named Date. 
            var expression = "ServerDatabase = '" + serverName + "'";

            // Sort descending by column named CompanyName. 
            var sortOrder = "Server ASC";

            // Use the Select method to find all rows matching the filter.
            var foundRows = ClsCommon.PublicDatasetServers.Tables[0].Select(expression, sortOrder);

            foreach (var dr in foundRows)
            {
                ClsCommon.ServerName = dr["Server"].ToString();
                ClsCommon.LoginName = dr["Username"].ToString();
                ClsCommon.PasswordName = dr["Password"].ToString();
                ClsCommon.DatabaseName = dr["Database"].ToString();
            }
        }

        private void mSExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var excelExport = new FrmExcelExport();
            excelExport.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var common = new ClsCommon();
            CreateNewTab(common.GetStoredProcedureText(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, ClsCommon.DatabaseName, _selectedStoredProcedure).Tables[0].Rows[0][0].ToString(), _selectedStoredProcedure);
                                
        }

        private string GetCommandColor(string Command)
        {
            string[] arrBlackStatements = { "fn_virtualfilestats","CONNECTIONPROPERTY","CONTEXT_INFO","CURRENT_REQUEST_ID",
                                        "ERROR_LINE","ERROR_MESSAGE","ERROR_NUMBER","ERROR_PROCEDURE","ERROR_SEVERITY","ERROR_STATE","GET_FILESTREAM_TRANSACTION_CONTEXT",
                                        "MIN_ACTIVE_ROWVERSION","ROWCOUNT_BIG","XACT_STATE",
                                        "CONCAT","FORMAT","CERTENCODED","CERTPRIVATEKEY",
                                        "DATABASE_PRINCIPAL_ID","sys.fn_builtin_permissions","sys.fn_get_audit_file","sys.fn_my_permissions",
                                        "HAS_PERMS_BY_NAME","IS_ROLEMEMBER","ORIGINAL_LOGIN","PWDCOMPARE","PWDENCRYPT","SCHEMA_ID","SCHEMA_NAME",
                                        "APPLOCK_MODE","APPLOCK_TEST","ASSEMBLYPROPERTY","DATABASE_PRINCIPAL_ID","FILE_IDEX","NEXT",
                                        "OBJECT_DEFINITION","OBJECT_SCHEMA_NAME","ORIGINAL_DB_NAME","SCHEMA_ID","SCHEMA_NAME",
                                        "TYPE_ID","TYPE_NAME","CHOOSE","IIF","SYSDATETIME","SYSDATETIMEOFFSET","SYSUTCDATETIME",
                                        "DATEFROMPARTS","DATETIME2FROMPARTS","DATETIMEFROMPARTS","DATETIMEOFFSETFROMPARTS","SMALLDATETIMEFROMPARTS",
                                        "TIMEFROMPARTS","EOMONTH","SWITCHOFFSET","TODATETIMEOFFSET","PARSE","TRY_CAST","TRY_CONVERT","TRY_PARSE",
                                        "RANK","DENSE_RANK","NTILE","ROW_NUMBER","GROUPING_ID","SUMSTDEV",
                                        "ABSOLUTE","ADA","ALLOCATE",
                                        "ARE","ASSERTION","AT",
                                        "BIT_LENGTH","BOTH","CASCADED","CHARACTER_LENGTH","COLLATION","CONNECT","CONNECTION","CONSTRAINTS",
                                        "CHAR_LENGTH","CORRESPONDING","DEFERRABLE","DEFERRED","DATE","DEC","DESCRIBE","DESCRIPTOR","DIAGNOSTICS","DISCONNECT",
                                        "DOMAIN","EXCEPTION","FIRST","EXTRACT","FALSE","FORTRAN","FOUND","GLOBAL","GO","HOUR","IMMEDIATE","INDICATOR","INITIALLY",
                                        "INPUT","INSENSITIVE","MINUTE","MODULE","LAST","LEADING","INTEGER","INTERVAL","MATCH","LOCAL",
                                        "NAMES","NATURAL","NEXT","NONE","OCTET_LENGTH"};

            var blackArrOutput = Array.IndexOf(arrBlackStatements, Command.Trim());

            string[] arrGreyStatements = {"LEFT","RIGHT","ALL","AND","ANY","BETWEEN","CROSS","EXISTS","IN","INNER","IS","JOIN","LEFT","LIKE","NOT","NULL","OR","OUTER","RIGHT","ANY","ALL","AND","BETWEEN",
                                        "CROSS","EXISTS","IN","INNER","IS","JOIN","LEFT","LIKE","NOT","NULL","OR","OUTER","RIGHT" };

            var greyArrOutput = Array.IndexOf(arrGreyStatements, Command.Trim());

            string[] arrPinkStatements = { "PERMISSIONS","SESSION_USER","SUSER_ID","SUSER_SID","SUSER_SNAME","SYSTEM_USER","SUSER_NAME","USER_ID","USER_NAME","@@PROCID","APP_NAME",
                                        "COL_LENGTH","COL_NAME","COLUMNPROPERTY","DATABASEPROPERTYEX","DB_ID","DB_NAME","FILE_ID","FILE_NAME","FILEGROUP_ID","FILEGROUP_NAME","FILEGROUPPROPERTY",
                                        "FILEPROPERTY","FULLTEXTCATALOGPROPERTY","FULLTEXTSERVICEPROPERTY","INDEX_COL","INDEXKEY_PROPERTY",
                                        "INDEXPROPERTY","OBJECT_ID","OBJECT_NAME","OBJECTPROPERTY","OBJECTPROPERTYEX","PARSENAME",
                                        "SCOPE_IDENTITY","SERVERPROPERTY","STATS_DATE","TYPEPROPERTY","ABS","ACOS","ASIN","ATAN","ATN2","CEILING","COS","COT","DEGREES","EXP","FLOOR","LOG","LOG10",
                                        "PI","POWER","RADIANS","RAND","ROUND","SIGN","SIN","SQRT","SQUARE","TAN","CURRENT_TIMESTAMP","GETDATE","GETUTCDATE",
                                        "DATENAME","DATEPART","DAY","MONTH","YEAR","DATEDIFF","DATEADD","@@DATEFIRST",
                                        "@@LANGUAGE","ISDATE","CAST","CONVERT","@@CURSOR_ROWS","@@FETCH_STATUS","@@DATEFIRST","@@DBTS",
                                        "@@LANGID","@@LANGUAGE","@@LOCK_TIMEOUT","@@MAX_CONNECTIONS","@@MAX_PRECISION","@@NESTLEVEL",
                                        "@@OPTIONS","@@REMSERVER","@@SERVERNAME","@@SERVICENAME","@@SPID","@@TEXTSIZE","@@VERSION","AVG","CHECKSUM_AGG",
                                        "COUNT","COUNT_BIG","GROUPING","MAX","MIN","STDEVP","VAR","VARP","TEXTVALID","TEXTPTR","PATINDEX","@@CONNECTIONS","@@CPU_BUSY","@@IDLE","@@IO_BUSY","@@PACKET_ERRORS",
                                        "@@PACK_RECEIVED","@@PACK_SENT","@@TIMETICKS","@@TOTAL_ERRORS","@@TOTAL_READ","@@TOTAL_WRITE","$PARTITION","@@ERROR","@@IDENTITY","@@PACK_RECEIVED","@@ROWCOUNT",
                                        "@@TRANCOUNT","BINARY_CHECKSUM","CHECKSUM","FORMATMESSAGE","GETANSINULL","HOST_ID","HOST_NAME","ISNULL","ISNUMERIC","NEWID","NEWSEQUENTIALID","ASCII","CHARINDEX","DIFFERENCE","LEN","LOWER","LTRIM",
                                        "PATINDEX","QUOTENAME","REPLACE","REPLICATE","REVERSE","RTRIM","SOUNDEX","SPACE","STR","STUFF","SUBSTRING","UNICODE","UPPER","CURRENT_USER","IS_MEMBER","IS_SRVROLEMEMBER",
                                        "COALESCE","CONVERT","CURRENT_TIMESTAMP","CURRENT_USER","NULLIF","SESSION_USER","SYSTEM_USER","USER","AVG","CAST","COALESCE","CONVERT","COUNT","CURRENT_TIMESTAMP","CURRENT_USER","DAY","LOWER","MAX","MIN","MONTH","NULLIF","SESSION_USER","SYSTEM_USER","USER"};

            var pinkArrOutput = Array.IndexOf(arrPinkStatements, Command.Trim());


            string[] arrBlueStatements = { "ADD","ALTER","AS","ASC","AUTHORIZATION","BACKUP","BEGIN","BREAK","BROWSE","BULK","BY","CASCADE",
                                        "CASE","CHECK","CHECKPOINT","CLOSE","CLUSTERED","COLLATE","COLUMN","COMMIT","COMPUTE","CONSTRAINT","CONTAINS","CONTAINSTABLE","CONTINUE","CREATE",
                                        "CURRENT","CURRENT_DATE","CURRENT_TIME","CURSOR","DATABASE","DBCC","DEALLOCATE","DECLARE","DEFAULT","DELETE","DENY","DESC","DISK",
                                        "DISTINCT","DISTRIBUTED","DOUBLE","DROP","DUMMY","DUMP","ELSE","END","ERRLVL","ESCAPE","EXCEPT","EXEC","EXECUTE","EXIT","FETCH",
                                        "FILE","FILLFACTOR","FOR","FOREIGN","FREETEXT","FREETEXTTABLE","FROM","FULL","FUNCTION","GOTO","GRANT","GROUP","HAVING","HOLDLOCK","IDENTITY","IDENTITY_INSERT",
                                        "IDENTITYCOL","IF","INDEX","INSERT","INTERSECT","INTO","KEY","KILL","LINENO","LOAD","NATIONAL","NOCHECK","NONCLUSTERED","OF","OFF",
                                        "OFFSETS","ON","OPEN","OPENDATASOURCE","OPENQUERY","OPENROWSET","OPENXML","OPTION","ORDER","OVER","PERCENT","PLAN","PRECISION","PRIMARY",
                                        "PRINT","PROC","PROCEDURE","PUBLIC","RAISERROR","READ","READTEXT","RECONFIGURE","REFERENCES","REPLICATION","RESTORE","RESTRICT","RETURN","REVOKE","ROLLBACK",
                                        "ROWCOUNT","ROWGUIDCOL","RULE","SAVE","SCHEMA","SELECT","SET","SETUSER","SHUTDOWN","SOME","STATISTICS","TABLE","TEXTSIZE","THEN",
                                        "TO","TOP","TRAN","TRANSACTION","TRIGGER","TRUNCATE","TSEQUAL","UNION","UNIQUE","UPDATE","UPDATETEXT","USE","VALUES","VARYING","VIEW",
                                        "WAITFOR","WHEN","WHERE","WHILE","WITH","WRITETEXT","ACTION","ADD","ALTER","AS","ASC","AUTHORIZATION","BEGIN",
                                        "BIT","BY","CASCADE","CASE","CATALOG","CHAR","CHARACTER","CHECK","CLOSE","COLLATE","COLUMN","COMMIT",
                                        "CONSTRAINT","CONTINUE","CREATE","CURRENT","CURRENT_DATE","CURRENT_TIME","CURSOR","DEALLOCATE","DECIMAL","DECLARE","DEFAULT","DELETE",
                                        "DESC","DISTINCT","DOUBLE","DROP","ELSE","END","END-EXEC","ESCAPE","EXCEPT",
                                        "EXEC","EXECUTE","EXTERNAL","FETCH","FLOAT","FOR","FOREIGN","FROM","FULL","GET","GOTO","GRANT","GROUP","HAVING","IDENTITY","ISOLATION","INCLUDE","INDEX","INSERT","INT","INTERSECT","INTO","LEVEL",
                                        "KEY","LANGUAGE","NATIONAL","NCHAR","NO","NUMERIC","OF","OFF","OFFSETS","ON","OPEN","OPENDATASOURCE","OPENQUERY","OPENROWSET","OPENXML","OPTION",
                                        "ORDER","OVER","PERCENT","PLAN","PRECISION","PRIMARY","PRINT","PROC","PROCEDURE","PUBLIC","RAISERROR","READ","READTEXT","RECONFIGURE","REFERENCES",
                                        "REPLICATION","RESTORE","RESTRICT","RETURN","REVOKE","ROLLBACK","ROWCOUNT","ROWGUIDCOL","RULE","SAVE","SCHEMA","SELECT","SET","SETUSER",
                                        "SHUTDOWN","SOME","STATISTICS","TABLE","TEXTSIZE","THEN","TO","TOP","TRAN","TRANSACTION","TRIGGER","TRUNCATE","TSEQUAL","UNION","UNIQUE",
                                        "UPDATE","UPDATETEXT","USE","VALUES","VARYING","VIEW","WAITFOR","WHEN","WHERE","WHILE","WITH","WRITETEXT",
                                        "CHAR","NCHAR","VALUE","FOR","SET","DATEFIRST","SET","LANGUAGE","SET","DATEFORMAT","OPENDATASOURCE","OPENROWSET","OPENQUERY","OPENXML"};

            var blueArrOutput = Array.IndexOf(arrBlueStatements, Command.Trim());

            string[] arrOperators = { "+", "-", "*", "/", "%", "=", "&", "|", "^", ">", "<", ">=", "<=", "<>", "!=", "!<", "!>",
                                    "+=","-=","*=","/=","%=","&=","^=","|=","~","","","","","","",};
            var operatorsOutput = Array.IndexOf(arrOperators, Command.Trim());



            var responseColor = string.Empty;
            if (blackArrOutput >= 0)
            {
                responseColor = "BLACK";
            }
            else if (blueArrOutput >= 0)
            {
                responseColor = "BLUE";
            }
            else if (operatorsOutput >= 0)
            {
                responseColor = "BROWN";
            }
            else if (pinkArrOutput >= 0)
            {
                responseColor = "MAGENTA";
            }
            else if (greyArrOutput >= 0)
            {
                responseColor = "GRAY";
            }

            return responseColor;
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
        }

        private void frmMain_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void CreateNewTab(string Content, string TabTitle)
        {
            string tabTitle;

            if (TabTitle.Length > 0)
            {
                tabTitle = TabTitle;
            }
            else
            {
                tabTitle = "Query" + (tabEditor.TabPages.Count + 1).ToString();
            }

            var tbPage = new TabPage(tabTitle);
            var nTextBox = new NumberedTextBoxUC
            {
                Name = "NumberedTextBoxUC" + (tabEditor.TabPages.Count + 1).ToString()
            };

            nTextBox.SetText(Content);

            nTextBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            tabEditor.TabPages.Add(tbPage);
            tbPage.Controls.Add(nTextBox);

            nTextBox.Height = tabEditor.TabPages[0].Height - 2;
            nTextBox.Width = tabEditor.TabPages[0].Width - 2;
            nTextBox.Top = 1;
            nTextBox.Left = 1;

            nTextBox.BorderStyle = BorderStyle.FixedSingle;
            tabEditor.SelectedTab = tbPage;
        }

        private void txbtnNewQuery_Click(object sender, EventArgs e)
        {
            //Get the active tab and take the server and database name from there
            CreateNewTab("", "Query" + (tabEditor.TabPages.Count + 1));
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new frmOptions();
            options.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            treeDBObjects.ExpandAll();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            treeDBObjects.CollapseAll();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() == "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString())
                    {
                        NumberedTextBoxUC numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                        numTextBox.setRowNumber();
                    }
                }
            }   
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.Undo();
                }
            }   
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.PasteAction(sender,e);
                }
            }   
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.Redo();
                }
            }   
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.CutAction(sender,e);
                }
            }   
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.CopyAction(sender,e);
                }
            }   
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.SelectAll();
                }
            }   
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version 2.0.1.6 | Undo Functionality
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.FindandReplace();
                }
            }   
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
           
        }

        private void IncrementTab(string TabHeading)
        {
            tabServers.TabPages.Add(TabHeading);
            tabServers.TabPages[tabServers.TabPages.Count - 1].Name = TabHeading;

                TreeView tr = new TreeView();
                tr.Dock = DockStyle.Fill;
                tr.Name = "aa";

                tabServers.SuspendLayout();
                tabServers.TabPages[tabServers.TabPages.Count - 1].Controls.Add(tr);
                tabServers.ResumeLayout();
                BindTables(tr);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            var login = new FrmLogin("","", true);
            login.ShowDialog();

            if (login.IncrementTab)
            {
                IncrementTab(login.TabServername);                
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            var loginHistory = new Loodon.FrmLoginHistory(false);
            loginHistory.Show();
        }

        private void openConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton7_Click(sender, e);
        }

        private void closeConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton8_Click(sender, e);
        }

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton9_Click(sender, e);
        }

        private void listConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton10_Click(sender, e);
        }

        private void tabServers_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmsDatabase.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.ExpandResults();
                }
            }
        }

        private void toolStripButton11_Click_1(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.CollapseResults();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedTab = 0;

            if (tabEditor.TabPages.Count > 0)
            {
                selectedTab =  tabEditor.SelectedIndex - 1;
            }

            tabEditor.TabPages.Remove(tabEditor.SelectedTab);

            if (tabEditor.TabPages.Count > 0)
            {
                tabEditor.SelectedIndex = selectedTab;
            }
        }

        private void tabEditor_MouseClick(object sender, MouseEventArgs e)
        {
            var tabControl = sender as TabControl;
            TabPage tabPageCurrent = null;

            switch (e.Button)
            {
                case MouseButtons.Right:
                    cmsQueryEditor.Show(Cursor.Position.X, Cursor.Position.Y);
                    break;
                case MouseButtons.Middle:
                {
                    for (var i = 0; i < tabControl.TabCount; i++)
                    {
                        if (!tabControl.GetTabRect(i).Contains(e.Location))
                            continue;
                        tabPageCurrent = tabControl.TabPages[i];
                        break;
                    }
                    if (tabPageCurrent != null)
                        tabControl.TabPages.Remove(tabPageCurrent);
                    break;
                }
            }
        }


        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            var attachdB = new FrmAttachDatabase(tabServers.SelectedTab.Text);
            attachdB.ShowDialog();
        }

        private void attachDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabServers.SelectedTab == null) return;
            var attachdB = new FrmAttachDatabase(tabServers.SelectedTab.Text);
            attachdB.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            new FrmBackup().Show();
        }

        private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txbtnNewQuery_Click(sender,e);
        }

        private void closeDatabaseConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton8_Click(sender, e);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1)) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    new FrmSeparateResults(numTextBox.GetDataGridRecords(), Height, Width).ShowDialog();
                }
            }     
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files|*.txt";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK) return;
            SaveQueryFile(saveFileDialog1.FileName);
            tabEditor.TabPages[tabEditor.SelectedIndex].Text = saveFileDialog1.FileName;
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && (e.Control))
            {
                
            }
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            //Finding Controls in TabEditor Control box and execute the query.
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1).ToString()) continue;
                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.ColumnResize();
                }
            }
        }

        private void standardToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (standardToolbarToolStripMenuItem.Checked)
            {
                tsStandard.Visible = false;
                standardToolbarToolStripMenuItem.Checked = false;
                //Manage the height of the spltcontainer lying below the toolbar
                splitContainer2.Top = splitContainer2.Top - tsStandard.Height;
                splitContainer2.Height = splitContainer2.Height + tsStandard.Height;
            }
            else if (!standardToolbarToolStripMenuItem.Checked)
            {
                tsStandard.Visible = true;
                standardToolbarToolStripMenuItem.Checked = true;
                //Manage the height of the spltcontainer lying below the toolbar
                splitContainer2.Top = splitContainer2.Top + tsStandard.Height;
                splitContainer2.Height = splitContainer2.Height - tsStandard.Height;
            }
            
            this.Refresh();
        }

        private void connectionToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectionToolbarToolStripMenuItem.Checked)
            {
                tsConnection.Visible = false;
                connectionToolbarToolStripMenuItem.Checked = false;
                //Manage the height of the spltcontainer lying below the toolbar
                splitContainer2.Top = splitContainer2.Top - tsConnection.Height;
                splitContainer2.Height = splitContainer2.Height + tsStandard.Height;
            }
            else if (!connectionToolbarToolStripMenuItem.Checked)
            {
                tsConnection.Visible = true;
                connectionToolbarToolStripMenuItem.Checked = true;
                //Manage the height of the spltcontainer lying below the toolbar
                splitContainer2.Top = splitContainer2.Top + tsConnection.Height;
                splitContainer2.Height = splitContainer2.Height - tsConnection.Height;
            }
            Refresh();
        }

        private void backupDatabaseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripMenuItem9_Click(sender, e);
        }

        private void upgradeLooDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSoftwareUpdate();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            newDatabaseToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            mSExcelToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Displays the Create Table syntax to create a new table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
            //Get the active tab and take the server and database name from there
            var content = ClsGlobalText.SqlsyntaxNewtable;
            CreateNewTab(content, "Query" + (tabEditor.TabPages.Count + 1));
        }

        private void cmsTable_Opening(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem89_Click(object sender, EventArgs e)
        {
            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var query = "SELECT TOP 100 * from " + _currentSelectedNode;
            CreateNewTab(query, "Query" + (tabEditor.TabPages.Count + 1).ToString());
            toolStripButton3_Click(sender, e);
        }




        private void RefreshDatabaseDetails()
        {
            BindTables(treeDBObjects);
        }

        /// <summary>
        /// Generates the script for the selected table in a new Query Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var query = ClsQueries.SqlGettablescript.Replace("{TABLENAME}",_currentSelectedNode);
            CreateNewTab(query, "Query" + (tabEditor.TabPages.Count + 1));
        }

        private void ExecuteSQLQueryinNewWindow(string Query)
        {
            foreach (Control ctrl in tabEditor.Controls)
            {
                for (var i = 0; i <= ctrl.Controls.Count - 1; i++)
                {
                    if (ctrl.Controls[i].Name.ToLower() !=
                        "numberedtextboxuc" + (tabEditor.SelectedIndex + 1)) continue;
                    ServerExistsinPervasiveDataset(tabServers.SelectedTab.Text); 

                    var numTextBox = (NumberedTextBoxUC)ctrl.Controls[i];
                    numTextBox.ExecuteSelectedQueryforEditorOutput(Query);
                }
            }     

        }

        /// <summary>
        /// Generates the script for the selected table in a File selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            var query = string.Empty;

            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            query = ClsQueries.SqlGettablescript.Replace("{TABLENAME}", _currentSelectedNode);

            saveFileDialog1.Filter = "Text Files|*.txt";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK) return;
            SaveQueryFile(saveFileDialog1.FileName, query);

            MessageBox.Show("Script file saved successfully at \n" + saveFileDialog1.FileName, "Save Table Script file", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Generates the Drop Table query for the selected table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var query = LoodonDAL.ClsGlobalText.SqlsyntaxDroptable.Replace("{TableName}", _currentSelectedNode);
            CreateNewTab(query, "Query" + (tabEditor.TabPages.Count + 1));
        }

        private void msExcelToolStripMenuItem1_Click(object sender, EventArgs e)
        {//Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            ServerExistsinPervasiveDataset(serverName, dbName, out var outUsername, out var outPassword);

            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowDialog();
            var backupPath = folderBrowserDialog1.SelectedPath;  
            var query = "SELECT  * from " + _currentSelectedNode;
            var tablename = _currentSelectedNode;
            new FrmExportData(serverName, dbName, tablename, backupPath, query, outUsername, outPassword).ShowDialog();

            MessageBox.Show("Data Export process completed successfully.", "Data Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Search the Credentials in Pervasive Dataset that actually maintains all the login
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void ServerExistsinPervasiveDataset(string serverName, string databaseName, out string username, out string password)
        {
            var outUsername = string.Empty;
            var outPassword = string.Empty;

            // Presuming the DataTable has a column named Date. 
            var expression = "Server = '" + serverName + "' and Database = '" + databaseName + "'";
            // string expression = "OrderQuantity = 2 and OrderID = 2";

            // Sort descending by column named CompanyName. 
            var sortOrder = "Server ASC";

            // Use the Select method to find all rows matching the filter.
            var foundRows = ClsCommon.PublicDatasetServers.Tables[0].Select(expression, sortOrder);

            foreach (var dr in foundRows)
            {
                outUsername = dr["Username"].ToString();
                outPassword = dr["Password"].ToString();
            }

            username = outUsername;
            password = outPassword;
            
        }

        private void countRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try 
	        {
	            if (_selectedItem != "TableName") return;
	            var serverDb = tabServers.SelectedTab.Text.Split('#');
	            var serverName = serverDb[0];
	            var dbName = serverDb[1];

	            ServerExistsinPervasiveDataset(serverName, dbName, out var outUsername, out var outPassword);

	            string recordCount = new ClsSelect().GetRecordsCount(_currentSelectedNode, serverName, outUsername, outPassword, dbName).ToString();
	            MessageBox.Show("Total Records: " + recordCount, "Record Count", MessageBoxButtons.OK, MessageBoxIcon.Information );
	        }
            catch (Exception ex)
            {
                var error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Process.Start("http://krayknot.com/contact.aspx");
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            txbtnNewQuery_Click(sender, e);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (_selectedItem != "TableName") return;
            var serverDB = tabServers.SelectedTab.Text.Split('#');
            var ServerName = serverDB[0];
            var DBName = serverDB[1];

            var outUsername = string.Empty;
            var outPassword = string.Empty;

            ServerExistsinPervasiveDataset(ServerName, DBName, out outUsername, out outPassword);
            new FrmTableDesign(_currentSelectedNode, ServerName, DBName, outUsername, outPassword).ShowDialog();

            BindTables(treeDBObjects); //Bind the refreshed content again
        }

        private void xmlToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Get the Server, user, password and table details based on the Active tab

            //Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            ServerExistsinPervasiveDataset(serverName, dbName, out var outUsername, out var outPassword);

            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var folderBrowserDialog1 = new FolderBrowserDialog();

            if (folderBrowserDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var backupPath = folderBrowserDialog1.SelectedPath;
            var tablename = _currentSelectedNode;

            var dst = new DataSet();
            dst = ClsCommon.GetTableData(tablename, serverName, outUsername, outPassword, dbName);
            dst.WriteXml(backupPath + "\\" + _currentSelectedNode + ".xml");

            MessageBox.Show("Data Export process completed successfully.", "Data Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmsTableDef_Opening(object sender, CancelEventArgs e)
        {
        }

        private void editRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var credentials = GetCurrentCredentials();
            credentials.TableName = _currentSelectedNode;

            new FrmSeparateResults(new DataTable(), Height, Width, FrmSeparateResults.SeparateResultsMode.EDIT, credentials).ShowDialog();
        }

        /// <summary>
        /// Checks for the new update if any from the website - krayknot.com
        /// </summary>
        private void CheckSoftwareUpdate()
        {
            lblCheckUpdate.Visible = true;
            lblCheckUpdate.Refresh();

            try
            {
                //Check the xml from theurl
                //if update found, tell the user and open the krayknot download page
                var wc = new WebClient();
                wc.DownloadFile("http://downloads.krayknot.com/loodon/updateinfo.xml", "updateinfoRecent.xml");

                //Compare the Date Fields
                var dst = new DataSet();
                dst.ReadXml("updateinfoRecent.xml");

                var dstLocal = new DataSet();
                dstLocal.ReadXml("updateinfo.xml");

                var updateInfoRecent = Convert.ToDateTime(dst.Tables["Release"].Rows[0]["TimeStamp"]);
                var updateInfo = Convert.ToDateTime(dstLocal.Tables["Release"].Rows[0]["TimeStamp"]);

                if (updateInfo >= updateInfoRecent) return;
                var appName = dst.Tables["Release"].Rows[0]["AppName"].ToString();
                var updateBy = dst.Tables["Release"].Rows[0]["UpdatedBy"].ToString();
                var updatedSource = dst.Tables["Release"].Rows[0]["UpdatedSource"].ToString();
                var details = dst.Tables["Release"].Rows[0]["Details"].ToString();

                var message = "Loodon has an update. \nPlease go to www.krayknot.com to download this new update." +
                                 "\n\nDetails are:\n" +
                                 "Application Name: " + appName +
                                 "\nUpdated By: " + updateBy +
                                 "\nSource: " + updatedSource +
                                 "\nDetails: " + details +
                                 "\n\nPlease check the Options feature to switch-off the Update check at Application startup.";

                MessageBox.Show(message, "New update found", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblCheckUpdate.Visible = false;
                lblCheckUpdate.Refresh();
            }
            catch (Exception)
            {
                //No Exception
            }
            finally
            {
                lblCheckUpdate.Visible = false;
                lblCheckUpdate.Refresh();
            }
        }

        private void sQLToAccessConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmSQLtoAccess().ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void treeDBObjects_ItemDrag(object sender, ItemDragEventArgs e)
        {
            switch (e.Button)
            {
                // Move the dragged node when the left mouse button is used. 
                // Copy the dragged node when the right mouse button is used. 
                case MouseButtons.Left:
                    DoDragDrop(e.Item, DragDropEffects.Move);
                    break;
                case MouseButtons.Right:
                    DoDragDrop(e.Item, DragDropEffects.Copy);
                    break;
            }
        }

        private void tabEditor_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        
        private void searchWithinColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get the Server, user, password and table details based on the Active tab
            var username = ClsCommon.LoginName;
            var password = ClsCommon.PasswordName;
            var columnName = _currentSelectedNode;

            //Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            var tableName = GetTableNameForSelectedColumn();

            new FrmSearchonColumn(serverName, username, password, dbName, columnName, tableName, Height, Width).ShowDialog();
        }

        /// <summary>
        /// Fetches the table for current selected Table=Column
        /// </summary>
        /// <returns></returns>
        private string GetTableNameForSelectedColumn()
        {
            //Get the Table Name
            var tableName = string.Empty;
            var tableNameFound = false;
            var node = treeDBObjects.SelectedNode;

            while (tableNameFound != true)
            {
                node = node.Parent;

                if (node.Text != "Columns") continue;
                node = node.Parent;
                tableName = node.Text;
                tableNameFound = true;
            }
            return tableName;
        }

        private void findTablesFromColumnNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get the Server, user, password and table details based on the Active tab
            var username = ClsCommon.LoginName;
            var password = ClsCommon.PasswordName;

            //Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            var tablefromcol = new FrmTablefromColumn(serverName, dbName, username, password);
            tablefromcol.ShowDialog();
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            var username = ClsCommon.LoginName;
            var password = ClsCommon.PasswordName;

            //Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            var serverTimeStamp = new ClsSelect().GetServerCurrentTimeStamp(serverName, username, password, dbName);
            MessageBox.Show("Server Timestamp :\n" + serverTimeStamp, "Server TimeStamp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void select10RowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var query = "SELECT TOP 10 * from " + _currentSelectedNode;
            CreateNewTab(query, "Query" + (tabEditor.TabPages.Count + 1).ToString());
            toolStripButton3_Click(sender, e);
        }

        private void select50RowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Prepare the table and database name
            if (_selectedItem != "TableName") return;
            var query = "SELECT TOP 50 * from " + _currentSelectedNode;
            CreateNewTab(query, "Query" + (tabEditor.TabPages.Count + 1));
            toolStripButton3_Click(sender, e);
        }

        private void cmsColumns_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get the Table Name
            var node = treeDBObjects.SelectedNode;

            var tableName = node.Parent.Text;
            new FrmAddColumn(tableName).ShowDialog();
            BindTables(treeDBObjects); //Bind the refreshed content again
        }

        private void restartWithNewConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //The command is sent to the OS, the ping pauses the script for 2-3 seconds, by which 
            //time the application has exited from Application.Exit(), then the next command after the ping starts it again.
            //Note: The \" puts quotes around the path, incase it has spaces, which cmd can't process without quotes.
            //---------------------------------------------------------------------------------------------------------
            var info = new ProcessStartInfo
            {
                Arguments = "/C ping 127.0.0.1 -n 2 && \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(info);
            Application.Exit(); 
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get the Server, user, password and table details based on the Active tab
            var username = ClsCommon.LoginName;
            var password = ClsCommon.PasswordName;
            var columnName = _currentSelectedNode;

            //Get Server name from current tab
            var serverDb = tabServers.SelectedTab.Text.Split('#');
            var serverName = serverDb[0];
            var dbName = serverDb[1];

            var tableName = GetTableNameForSelectedColumn();

            new FrmColumnProperties(tableName, serverName, columnName, username, password, dbName).ShowDialog();
        }

        private void askAQuestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmQuestions().ShowDialog();
        }

        private void registerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            new FrmLicense().ShowDialog();
        }

        private ClsDataTypes.Credentials GetCurrentCredentials()
        {
            var userCredentials = new ClsDataTypes.Credentials
            {
                DbName = ClsCommon.DatabaseName,
                Password = ClsCommon.PasswordName,
                Server = ClsCommon.ServerName,
                Username = ClsCommon.LoginName
            };

            return userCredentials;
        }
    }
}