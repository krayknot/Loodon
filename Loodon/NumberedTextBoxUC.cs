using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SQLBackup;
using LoodonDAL;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Loodon;
using LoodonDAL;
using ClsCommon = Loodon.ClsCommon;


namespace NumberedTextBox
{
    
    public partial class NumberedTextBoxUC : UserControl
    {
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int LockWindowUpdate(int hWnd);

        private int selectedRow = 0;

        public string _SELECTEDTEXT = string.Empty; // contains the selected text

        //public Regex keyWordsBlue = new Regex("int | if |then |else |fi |true |while |do |done |set |export |bool |break |case |class |const |for |foreach |goto |in |void |if\n|then\n|else\n|fi\n|true\n|while\n|do\n|done\n|set\n|export\n|bool\n|break\n|case\n|class\n|const\n|for\n|foreach\n|goto\n|in\n|void\n");
        public Regex keyWordsBlue = new Regex("OPENDATASOURCE |OPENROWSET |OPENQUERY |OPENXML  |ANSI_NULLS |ANSI_NULL_DFLT_ON |ANSI_PADDING |ANSI_WARNINGS |CONCAT_NULL_YIELDS_NULL |QUOTED_IDENTIFIER |ADD |" +
                                            "EXTERNAL |PROCEDURE |FETCH |PUBLIC |ALTER |FILE |RAISERROR |FILLFACTOR |READ |FOR |READTEXT |AS |FOREIGN |RECONFIGURE |ASC |FREETEXT |REFERENCES |AUTHORIZATION |" +
                                            "FREETEXTTABLE |REPLICATION |BACKUP |FROM |RESTORE |BEGIN |FULL |RESTRICT |FUNCTION |RETURN |BREAK |GOTO |REVERT |BROWSE |GRANT |REVOKE |BULK |GROUP |BY |" +
                                            "HAVING |ROLLBACK |CASCADE |HOLDLOCK |ROWCOUNT |CASE |IDENTITY |ROWGUIDCOL |CHECK |IDENTITY_INSERT |RULE |CHECKPOINT |IDENTITYCOL | SAVE |CLOSE |IF |SCHEMA |CLUSTERED |" +
                                            "VALUE FOR |GET |FULLTEXTTABLE |XMLNAMESPACES |FLOAT |REAL |RANGE |XMLBINARY |END-EXEC |PATH |PARTITION |VARCHAR |OUTPUT |VALUE |UNNEST |DECIMAL |NUMERIC |NO |" +
                                            "TIMESTAMP |TIME |NCHAR |SYMMETRIC |CHARACTER |CHAR |CATALOG |LEVEL |SMALLINT |SIZE |BIT |BINARY |LANGUAGE |ISOLATION |ASYMMETRIC |INT |ROLE |RETURNS |AGGREGATE |" +
                                            "ACTION |EXCEPT |OUTPUT |ESCAPE |END-EXEC |ORDER |DROP |OPEN |WHERE |ELSE |OPTION |WITH |END |ON |WHEN |DOUBLE |VARYING |DISTINCT |OF |VIEW |NUMERIC |VARCHAR |" +
                                            "VALUES |VALUE |DESC |DELETE |NO |DEFAULT |NCHAR |UPDATE |UNION |DECIMAL |NATIONAL |UNIQUE |DECLARE |DEALLOCATE |TRANSACTION |CURSOR |TO |CURRENT_TIME |CURRENT_DATE |" +
                                            "LEVEL |TIMESTAMP |CURRENT |TIME |THEN |CREATE |LANGUAGE |TABLE |CONTINUE |KEY |CONSTRAINT |ISOLATION |INTO |COMMIT |COLUMN |INTERSECT |COLLATE |INT |INSERT |" +
                                            "SOME |SMALLINT |CLOSE |SIZE |CHECK |SET |CHARACTER |CATALOG |INCLUDE |SELECT |CHAR |INDEX |CASE |IDENTITY |SCHEMA |CASCADE |GROUP |ROLLBACK |BY |HAVING |GRANT |" +
                                            "RESTRICT |BIT |GOTO |REVOKE |GET |REFERENCES |BEGIN |FROM |READ |AUTHORIZATION |FULL |REAL |PUBLIC |PROCEDURE |ASC |AS |FOREIGN |FOR |FLOAT |PRIMARY |PRECISION |" +
                                            "ALTER |FETCH |ADD |EXTERNAL |ACTION |EXECUTE |EXEC |PRINT |WRITETEXT |EXIT |PROC |GROUP |WHERE |EXCEPT |PLAN |WHILE |EXEC |PRECISION |WITH |EXECUTE |PRIMARY |" +
                                            "VIEW |END |OVER |WAITFOR |ERRLVL |PERCENT |WHEN |ESCAPE |VALUES |DUMP |ORDER |VARYING |ELSE |DROP |DISK |OPENQUERY | UPDATE |DISTINCT |OPENROWSET |UPDATETEXT |" +
                                            "DISTRIBUTED |OPENXML |USE |DOUBLE |OPTION |DEFAULT |OFFSETS |TSEQUAL |DELETE |ON |UNION |DENY |OPEN |UNIQUE |DESC |OPENDATASOURCE |TRIGGER |DEALLOCATE |OF |" +
                                            "TRUNCATE |DECLARE |OFF |TRANSACTION |DBCC |TRAN |DATABASE |NONCLUSTERED |TOP |CURSOR |NOCHECK |TO |TEXTSIZE |CURRENT_TIME |NATIONAL |THEN |LINENO |TABLE |CURRENT |" +
                                            "LOAD |TABLESAMPLE |CURRENT_DATE |STATISTICS |CREATE |SETUSER |CONTAINSTABLE |KEY |SHUTDOWN |CONTINUE |KILL |SOME |CONTAINS |CONSTRAINT |COMPUTE |INTO |COMMIT |" +
                                            "INTERSECT |COLUMN |INSERT |INDEX |SELECT |COLLATE |OPENDATASOURCE\n|OPENROWSET\n|OPENQUERY\n|OPENXML \n|ANSI_NULLS\n|ANSI_NULL_DFLT_ON\n|ANSI_PADDING\n|ANSI_WARNINGS\n|CONCAT_NULL_YIELDS_NULL\n|QUOTED_IDENTIFIER\n|ADD\n|" +
                                            "EXTERNAL\n|PROCEDURE\n|FETCH\n|PUBLIC\n|ALTER\n|FILE\n|RAISERROR\n|FILLFACTOR\n|READ\n|FOR\n|READTEXT\n|AS\n|FOREIGN\n|RECONFIGURE\n|ASC\n|FREETEXT\n|REFERENCES\n|AUTHORIZATION\n|" +
                                            "FREETEXTTABLE\n|REPLICATION\n|BACKUP\n|FROM\n|RESTORE\n|BEGIN\n|FULL\n|RESTRICT\n|FUNCTION\n|RETURN\n|BREAK\n|GOTO\n|REVERT\n|BROWSE\n|GRANT\n|REVOKE\n|BULK\n|GROUP\n|BY\n|" +
                                            "HAVING\n|ROLLBACK\n|CASCADE\n|HOLDLOCK\n|ROWCOUNT\n|CASE\n|IDENTITY\n|ROWGUIDCOL\n|CHECK\n|IDENTITY_INSERT\n|RULE\n|CHECKPOINT\n|IDENTITYCOL\n|SAVE\n|CLOSE\n|IF\n|SCHEMA\n|CLUSTERED\n|" +
                                            "VALUE FOR\n|GET\n|FULLTEXTTABLE\n|XMLNAMESPACES\n|FLOAT\n|REAL\n|RANGE\n|XMLBINARY\n|END-EXEC\n|PATH\n|PARTITION\n|VARCHAR\n|OUTPUT\n|VALUE\n|UNNEST\n|DECIMAL\n|NUMERIC\n|NO\n|" +
                                            "TIMESTAMP\n|TIME\n|NCHAR\n|SYMMETRIC\n|CHARACTER\n|CHAR\n|CATALOG\n|LEVEL\n|SMALLINT\n|SIZE\n|BIT\n|BINARY\n|LANGUAGE\n|ISOLATION\n|ASYMMETRIC\n|INT\n|ROLE\n|RETURNS\n|AGGREGATE\n|" +
                                            "ACTION\n|EXCEPT\n|OUTPUT\n|ESCAPE\n|END-EXEC\n|ORDER\n|DROP\n|OPEN\n|WHERE\n|ELSE\n|OPTION\n|WITH\n|END\n|ON\n|WHEN\n|DOUBLE\n|VARYING\n|DISTINCT\n|OF\n|VIEW\n|NUMERIC\n|VARCHAR\n|" +
                                            "VALUES\n|VALUE\n|DESC\n|DELETE\n|NO\n|DEFAULT\n|NCHAR\n|UPDATE\n|UNION\n|DECIMAL\n|NATIONAL\n|UNIQUE\n|DECLARE\n|DEALLOCATE\n|TRANSACTION\n|CURSOR\n|TO\n|CURRENT_TIME\n|CURRENT_DATE\n|" +
                                            "LEVEL\n|TIMESTAMP\n|CURRENT\n|TIME\n|THEN\n|CREATE\n|LANGUAGE\n|TABLE\n|CONTINUE\n|KEY\n|CONSTRAINT\n|ISOLATION\n|INTO\n|COMMIT\n|COLUMN\n|INTERSECT\n|COLLATE\n|INT\n|INSERT\n|" +
                                            "SOME\n|SMALLINT\n|CLOSE\n|SIZE\n|CHECK\n|SET\n|CHARACTER\n|CATALOG\n|INCLUDE\n|SELECT\n|CHAR\n|INDEX\n|CASE\n|IDENTITY\n|SCHEMA\n|CASCADE\n|GROUP\n|ROLLBACK\n|BY\n|HAVING\n|GRANT\n|" +
                                            "RESTRICT\n|BIT\n|GOTO\n|REVOKE\n|GET\n|REFERENCES\n|BEGIN\n|FROM\n|READ\n|AUTHORIZATION\n|FULL\n|REAL\n|PUBLIC\n|PROCEDURE\n|ASC\n|AS\n|FOREIGN\n|FOR\n|FLOAT\n|PRIMARY\n|PRECISION\n|" +
                                            "ALTER\n|FETCH\n|ADD\n|EXTERNAL\n|ACTION\n|EXECUTE\n|EXEC\n|PRINT\n|WRITETEXT\n|EXIT\n|PROC\n|GROUP\n|WHERE\n|EXCEPT\n|PLAN\n|WHILE\n|EXEC\n|PRECISION\n|WITH\n|EXECUTE\n|PRIMARY\n|" +
                                            "VIEW\n|END\n|OVER\n|WAITFOR\n|ERRLVL\n|PERCENT\n|WHEN\n|ESCAPE\n|VALUES\n|DUMP\n|ORDER\n|VARYING\n|ELSE\n|DROP\n|DISK\n|OPENQUERY\n|UPDATE\n|DISTINCT\n|OPENROWSET\n|UPDATETEXT\n|" +
                                            "DISTRIBUTED\n|OPENXML\n|USE\n|DOUBLE\n|OPTION\n|DEFAULT\n|OFFSETS\n|TSEQUAL\n|DELETE\n|ON\n|UNION\n|DENY\n|OPEN\n|UNIQUE\n|DESC\n|OPENDATASOURCE\n|TRIGGER\n|DEALLOCATE\n|OF\n|" +
                                            "TRUNCATE\n|DECLARE\n|OFF\n|TRANSACTION\n|DBCC\n|TRAN\n|DATABASE\n|NONCLUSTERED\n|TOP\n|CURSOR\n|NOCHECK\n|TO\n|TEXTSIZE\n|CURRENT_TIME\n|NATIONAL\n|THEN\n|LINENO\n|TABLE\n|CURRENT\n|" +
                                            "LOAD\n|TABLESAMPLE\n|CURRENT_DATE\n|STATISTICS\n|CREATE\n|SETUSER\n|CONTAINSTABLE\n|KEY\n|SHUTDOWN\n|CONTINUE\n|KILL\n|SOME\n|SET\n|CONTAINS\n|CONSTRAINT\n|COMPUTE\n|INTO\n|COMMIT\n|" +
                                            "INTERSECT\n|COLUMN\n|INSERT\n|INDEX\n|SELECT\n|COLLATE\n");

        public Regex keyWordsMagenta = new Regex("$PARTITION |@@CONNECTIONS |@@CPU_BUSY |@@CURSOR_ROWS |@@DATEFIRST |@@DBTS |@@ERROR |@@FETCH_STATUS |@@IDENTITY |@@IDLE |@@IO_BUSY |@@LANGID |@@LANGUAGE |@@LOCK_TIMEOUT |" +
            "@@MAX_CONNECTIONS |@@MAX_PRECISION |@@NESTLEVEL |@@OPTIONS |@@PACK_RECEIVED |@@PACK_RECEIVED |@@PACK_SENT |@@PACKET_ERRORS |@@PROCID |@@REMSERVER |@@ROWCOUNT |@@SERVERNAME |@@SERVICENAME |@@SPID |" +
            "@@TEXTSIZE |@@TIMETICKS |@@TOTAL_ERRORS |@@TOTAL_READ |@@TOTAL_WRITE |@@TRANCOUNT |@@VERSION |APP_NAME |AVG |AVG |BINARY_CHECKSUM |CAST |CAST |CHECKSUM |CHECKSUM_AGG |COALESCE |COALESCE |COL_LENGTH |" +
            "COL_NAME |COLUMNPROPERTY |CONVERT |CONVERT |COUNT |COUNT |COUNT_BIG |CURRENT_TIMESTAMP |CURRENT_TIMESTAMP |CURRENT_USER |CURRENT_USER |CURRENT_USER |CURSOR_STATUS |DATABASEPROPERTYEX |DAY |DAY |DB_ID |" +
            "DB_NAME |FILE_ID |FILE_NAME |FILEGROUP_ID |FILEGROUP_NAME |FILEGROUPPROPERTY |FILEPROPERTY |FORMATMESSAGE |FULLTEXTCATALOGPROPERTY |FULLTEXTSERVICEPROPERTY |GETANSINULL |GROUPING |GROUPING |HOST_ID |" +
            "HOST_NAME |INDEX_COL |INDEXKEY_PROPERTY |INDEXPROPERTY |IS_MEMBER |IS_SRVROLEMEMBER |ISNULL |ISNUMERIC |LOWER |MAX |MAX |MIN |MIN |MONTH |MONTH |NEWID |NEWSEQUENTIALID |NULLIF |NULLIF |OBJECT_ID |" +
            "OBJECT_NAME |OBJECTPROPERTY |OBJECTPROPERTYEX |PARSENAME |PERMISSIONS |SCOPE_IDENTITY |SERVERPROPERTY |SESSION_USER |SESSION_USER |SESSION_USER |SPACE |SPACE |STATS_DATE |STDEV |STDEVP |SUBSTRING |" +
            "SUM |SUM |SUSER_ID |SUSER_NAME |SUSER_SID |SUSER_SNAME |SYSTEM_USER |SYSTEM_USER |SYSTEM_USER |TYPEPROPERTY |UPPER |USER |USER |USER_ID |USER_NAME |VAR |VARP |YEAR |YEAR" +
            "$PARTITION\n|@@CONNECTIONS\n|@@CPU_BUSY\n|@@CURSOR_ROWS\n|@@DATEFIRST\n|@@DBTS\n|@@ERROR\n|@@FETCH_STATUS\n|@@IDENTITY\n|@@IDLE\n|@@IO_BUSY\n|@@LANGID\n|@@LANGUAGE\n|@@LOCK_TIMEOUT\n|" +
            "@@MAX_CONNECTIONS\n|@@MAX_PRECISION\n|@@NESTLEVEL\n|@@OPTIONS\n|@@PACK_RECEIVED\n|@@PACK_RECEIVED\n|@@PACK_SENT\n|@@PACKET_ERRORS\n|@@PROCID\n|@@REMSERVER\n|@@ROWCOUNT\n|@@SERVERNAME\n|@@SERVICENAME\n|@@SPID\n|" +
            "@@TEXTSIZE\n|@@TIMETICKS\n|@@TOTAL_ERRORS\n|@@TOTAL_READ\n|@@TOTAL_WRITE\n|@@TRANCOUNT\n|@@VERSION\n|APP_NAME\n|AVG\n|AVG\n|BINARY_CHECKSUM\n|CAST\n|CAST\n|CHECKSUM\n|CHECKSUM_AGG\n|COALESCE\n|COALESCE\n|COL_LENGTH\n|" +
            "COL_NAME\n|COLUMNPROPERTY\n|CONVERT\n|CONVERT\n|COUNT\n|COUNT\n|COUNT_BIG\n|CURRENT_TIMESTAMP\n|CURRENT_TIMESTAMP\n|CURRENT_USER\n|CURRENT_USER\n|CURRENT_USER\n|CURSOR_STATUS\n|DATABASEPROPERTYEX\n|DAY\n|DAY\n|DB_ID\n|" +
            "DB_NAME\n|FILE_ID\n|FILE_NAME\n|FILEGROUP_ID\n|FILEGROUP_NAME\n|FILEGROUPPROPERTY\n|FILEPROPERTY\n|FORMATMESSAGE\n|FULLTEXTCATALOGPROPERTY\n|FULLTEXTSERVICEPROPERTY\n|GETANSINULL\n|GROUPING\n|GROUPING\n|HOST_ID\n|" +
            "HOST_NAME\n|INDEX_COL\n|INDEXKEY_PROPERTY\n|INDEXPROPERTY\n|IS_MEMBER\n|IS_SRVROLEMEMBER\n|ISNULL\n|ISNUMERIC\n|LOWER\n|MAX\n|MAX\n|MIN\n|MIN\n|MONTH\n|MONTH\n|NEWID\n|NEWSEQUENTIALID\n|NULLIF\n|NULLIF\n|OBJECT_ID\n|" +
            "OBJECT_NAME\n|OBJECTPROPERTY\n|OBJECTPROPERTYEX\n|PARSENAME\n|PERMISSIONS\n|SCOPE_IDENTITY\n|SERVERPROPERTY\n|SESSION_USER\n|SESSION_USER\n|SESSION_USER\n|SPACE\n|SPACE\n|STATS_DATE\n|STDEV\n|STDEVP\n|SUBSTRING\n|" +
            "SUM\n|SUM\n|SUSER_ID\n|SUSER_NAME\n|SUSER_SID\n|SUSER_SNAME\n|SYSTEM_USER\n|SYSTEM_USER\n|SYSTEM_USER\n|TYPEPROPERTY\n|UPPER\n|USER\n|USER\n|USER_ID\n|USER_NAME\n|VAR\n|VARP\n|YEAR\n|YEAR\n");

        public Regex keyWordsGray = new Regex(@"ALL |AND |ANY |BETWEEN |CROSS |EXISTS |IN |INNER |IS |JOIN |LEFT |LIKE |NOT |NULL |OR |OUTER |PIVOT |RIGHT |UNPIVOT |[+] |[-] |[*] |[/] |[%] |ALL\n|AND\n|ANY\n|[+]\n|[-]\n|[*]\n|[/]\n|[%]\n|ANY\n|BETWEEN\n|CROSS\n|EXISTS\n|IN\n|INNER\n|IS\n|JOIN\n|LEFT\n|LIKE\n|NOT\n|NULL\n|OR\n|OUTER\n|PIVOT\n|RIGHT\n|UNPIVOT\n");

        public Regex keyWordsBlackonSilver = new Regex("SQLCMD |SQLCMD\n");

        public Regex keyWordRed = new Regex("' |'|'\n|' \n");
                

        int selectionStartCursor = 0;  
        //--------------------------------------------------------------------------------------------------

        int start = 0;
        int indexOfSearchText = 0;

        
        DataSet dstOutputDataset = new DataSet();

        //-----------------------
        int findStartIndex = 0;
        int findEndIndex = 0;
        //-----------------------

        public NumberedTextBoxUC()
        {
            InitializeComponent();

    //        numberLabel.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size);

            //Drag and Drop is not easy in case if the target control is in a usercontrol, In this case
            //set the AllowDrop property of the UserControl to false in the UserControl project. 
            //Then set the AllowDrop properties of its child controls to true. Once these changes are made,
            //the UserControl won't intercept drag-and-drop events intended for its child controls.
            //Note that changing the UserControl's AllowDrop property in a client form has no effect. 
            //When the UserControl is added to a form, its child controls will receive drag-and-drop events 
            //regardless of whether the UserControl's AllowDrop property is set to true or false.
            richTextBox1.DragEnter += new DragEventHandler(richTextBox1_DragEnter);
            richTextBox1.DragDrop += new DragEventHandler(richTextBox1_DragDrop);
            richTextBox1.AllowDrop = true;
        }


        //private void updateNumberLabel()
        //{
        //    //we get index of first visible char and number of first visible line
        //    Point pos = new Point(0, 0);
        //    int firstIndex = richTextBox1.GetCharIndexFromPosition(pos);
        //    int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

        //    //now we get index of last visible char and number of last visible line
        //    pos.X = ClientRectangle.Width;
        //    pos.Y = ClientRectangle.Height;
        //    int lastIndex = richTextBox1.GetCharIndexFromPosition(pos);
        //    int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);

        //    //this is point position of last visible char, we'll use its Y value for calculating numberLabel size
        //    pos = richTextBox1.GetPositionFromCharIndex(lastIndex);

           
        //    //finally, renumber label
        //    numberLabel.Text = "";
        //    for (int i = firstLine; i <= lastLine + 1; i++)
        //    {
        //        numberLabel.Text += i + 1 + "\n";
        //    }

        //}


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Commenting this section as we find it of no use
            //It slows down the speed sometimes as it locks the window
            //Date : 25 April 2014
            //By: Kshitij
            //--------------------------------------------------------------------------------------------------
            //indexOfSearchText = 0;
            //updateNumberLabel();
            //base.OnTextChanged(e);
            //LockWindowUpdate(this.Handle.ToInt32());
            //selectionStartCursor = richTextBox1.SelectionStart;
            //ApplySyntaxHighlighting();
            //richTextBox1.SelectionStart = selectionStartCursor;
            //LockWindowUpdate(0);
            //---------------------------------------------------------------------------------------------------
        }

        private void ApplySyntaxHighlighting()
        {

            // Select all and set to black so that it's 'clean'
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;

            // Then unselect and scroll to the end of the file
            richTextBox1.ScrollToCaret();
            richTextBox1.Select(richTextBox1.Text.Length, 1);

            // Start applying the highlighting... Set a value to selPos
            int selPos = richTextBox1.SelectionStart;

            foreach (Match keyWordMatch in keyWordsBlue.Matches(richTextBox1.Text.ToUpper()))
            {
                // Select the word..
                richTextBox1.Select(keyWordMatch.Index, keyWordMatch.Length);
                // Change it to blue
                richTextBox1.SelectionColor = Color.Blue;
                // Set it to bold for this example
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
                // Move cursor back to where it was
                richTextBox1.SelectionStart = selPos;
                // Change the default font color back to black.
                richTextBox1.SelectionColor = Color.Black;
            }

            foreach (Match keyWordMatch in keyWordsBlackonSilver.Matches(richTextBox1.Text.ToUpper()))
            {
                // Select the word..
                richTextBox1.Select(keyWordMatch.Index, keyWordMatch.Length);
                // Change it to blue
                richTextBox1.SelectionColor = Color.Silver;
                // Set it to bold for this example
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
                // Move cursor back to where it was
                richTextBox1.SelectionStart = selPos;
                // Change the default font color back to black.
                richTextBox1.SelectionColor = Color.Black;
            }

            foreach (Match keyWordMatch in keyWordsMagenta.Matches(richTextBox1.Text.ToUpper()))
            {
                // Select the word..
                richTextBox1.Select(keyWordMatch.Index, keyWordMatch.Length);
                // Change it to blue
                richTextBox1.SelectionColor = Color.Magenta;
                // Set it to bold for this example
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
                // Move cursor back to where it was
                richTextBox1.SelectionStart = selPos;
                // Change the default font color back to black.
                richTextBox1.SelectionColor = Color.Black;
            }

            foreach (Match keyWordMatch in keyWordsGray.Matches(richTextBox1.Text.ToUpper()))
            {
                // Select the word..
                richTextBox1.Select(keyWordMatch.Index, keyWordMatch.Length);
                // Change it to blue
                richTextBox1.SelectionColor = Color.Gray;
                // Set it to bold for this example
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
                // Move cursor back to where it was
                richTextBox1.SelectionStart = selPos;
                // Change the default font color back to black.
                richTextBox1.SelectionColor = Color.Black;
            }

            foreach (Match keyWordMatch in keyWordRed.Matches(richTextBox1.Text.ToUpper()))
            {
                // Select the word..
                richTextBox1.Select(keyWordMatch.Index, keyWordMatch.Length);
                // Change it to blue
                richTextBox1.SelectionColor = Color.Red;
                // Set it to bold for this example
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
                // Move cursor back to where it was
                richTextBox1.SelectionStart = selPos;
                // Change the default font color back to black.
                richTextBox1.SelectionColor = Color.Black;
            }

        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            int d = richTextBox1.GetPositionFromCharIndex(0).Y % (richTextBox1.Font.Height + 1);
            //numberLabel.Location = new Point(0, d);

            //updateNumberLabel();
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            //updateNumberLabel();
            richTextBox1_VScroll(null, null);
        }

        public string GetText()
        {
            return richTextBox1.Text;
        }

        public void SetText(string Text)
        {
            richTextBox1.Text = Text;
        }

        private void NumberedTextBoxUC_Load(object sender, EventArgs e)
        {
            
        }

        private void NumberedTextBoxUC_Resize(object sender, EventArgs e)
        {
            
        }

        private void NumberedTextBoxUC_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void NumberedTextBoxUC_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {           
            if (e.KeyCode == Keys.F5)
            {
                ExecuteSelectedQuery();
            }

            //if (e.Control && e.KeyCode == Keys.R)
            //{
            //    if (splitContainer2.SplitterDistance != splitContainer2.Height)
            //    {
            //        splitContainer2.SplitterDistance = splitContainer2.Height;
            //    }
            //    else if (splitContainer2.SplitterDistance == splitContainer2.Height)
            //    {
            //        splitContainer2.SplitterDistance = splitContainer2.Height - 300;
            //    }
            //}
        }

        public void ExecuteSelectedQuery()
        {
            //Housekeeping task
            //1. Asking for the current selected node to get the active table[ActiveTable] name from the parent frmmain
            //this tablename can be use by the 'Display' option in the Context menu to get the tabledetails and to edit the values
            
            long totalRecords = 0;
            ClsCommon common = new ClsCommon();
            LoodonDAL.ClsCommon lCommon = new LoodonDAL.ClsCommon ();
            bool executeFlag = true;

            string query = richTextBox1.SelectedText;

            //Check whether DELETE and UPDATE are allowed or not, If yes execute if no Dont execute
            if (LoodonDAL.ClsCommon.QueryhasDelete(query))
            {
                if (common.Read("PREVENTDELETE") == "TRUE")
                {
                    MessageBox.Show(LoodonDAL.ClsConfigMessages.Preventdeletemessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    executeFlag = false;
                }
            }

            if (LoodonDAL.ClsCommon.QueryhasUpdate(query))
            {
                if (common.Read("PREVENTUPDATE") == "TRUE")
                {
                    MessageBox.Show(LoodonDAL.ClsConfigMessages.Preventupdatemessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    executeFlag = false;
                }
            }

            if(executeFlag == true)
            {
#region Query Execution Operation starts
            
            if (query.Length < 1) //If there is no selected text then pick all the text as an Query
            {
                query = richTextBox1.Text;

            }

            DateTime dtExecutionTimeStart = DateTime.Now;

            if (query.Length > 0)
            {
                tslblStatus.Text = "Working...";
                string LogFileName = common.Read("LOGFILESAVELOCATION") + "\\" + "LooLog_" + DateTime.Now.Date.ToString().Replace("00", "").Replace(":", "") + ".txt";
                ClsFileOperations fileOperations = new ClsFileOperations();

                try
                {
                    //Take the query in array by the difference of space to check whether the first word is DELETE or UPDATE
                    string[] strArray = query.Split(' ');

                    if (strArray[0].ToLower() == "update" || strArray[0].ToLower() == "delete")
                    {

                        totalRecords = common.ExecuteUpdateDeleteStatement(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, ClsCommon.DatabaseName, query);
                        MessageBox.Show(totalRecords.ToString() + " records affected.", "Query Execution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dstOutputDataset = ClsCommon.ExecuteInsertStatementsForDataset(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, ClsCommon.DatabaseName, query);
                        if (dstOutputDataset.Tables.Count > 0)
                        {
                            totalRecords = dstOutputDataset.Tables[0].Rows.Count;
                        }

                    }

                    DateTime dtTimeStampCurrent = DateTime.Now;
                    string diffTimeQueryExecution = (dtTimeStampCurrent.Hour - dtExecutionTimeStart.Hour).ToString() + ":" + (dtTimeStampCurrent.Minute - dtExecutionTimeStart.Minute).ToString() + ":" + (dtTimeStampCurrent.Second - dtExecutionTimeStart.Second).ToString();

                    tslblExecutionTime.Text = "Execution Time: " + diffTimeQueryExecution;

                    //Calculate total row count for select statements especially
                    //if(dstOutputDataset.Tables.Count > 0 )
                    //{
                    //    totalRecords = dstOutputDataset.Tables[0].Rows.Count;
                    try
                    {
                        tslblTotalRecords.Text = "Total Records affected: " + (totalRecords).ToString();
                    }
                    catch (Exception)
                    {
                    }
                    //}

                    tslblStatus.Text = string.Empty;
                    statusStrip1.Refresh();

                    dgrdDataOutput.DataSource = dstOutputDataset.Tables[0];
                    splitContainer2.SplitterDistance = splitContainer2.Height - 300;

                    //Row numbers
                    if (common.Read("ROWNUMBERS") == "TRUE")
                    {
                        setRowNumber();
                    }

                    //Log Results
                    //1. Check whether Logging is Enabled or not
                    //2. Prepare the data and Write/Append in a file in the following format:
                    //-------------------------------------------------------------------------
                    //20th May 2013: 09:00:56
                    //-------------------------------------------------------------------------
                    //Server:
                    //Database:

                    //Statement: Select * from
                    //Results: 10 Records
                    //-------------------------------------------------------------------------
                    //3. If Log File not exists then create a new one or else use the existed one                   

                    if (common.Read("ENABLELOGGING") == "TRUE") //1. Check whether Logging is Enabled or not
                    {

                        try
                        {
                            string queryLogDetails = string.Empty;

                            //2. Prepare the data and Write/Append in a file in the following format:
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Date:" + DateTime.Now.ToString() + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Server:" + ClsCommon.ServerName + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Database:" + ClsCommon.DatabaseName + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Statement:" + query + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Results Count:" + totalRecords.ToString() + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;


                            if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                            {
                                ClsFileOperations.AppendFile(LogFileName, queryLogDetails);
                            }
                            else
                            {
                                ClsFileOperations.WriteFile(LogFileName, queryLogDetails);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                            {
                                ClsFileOperations.AppendFile(LogFileName, ex.ToString());
                            }
                            else
                            {
                                ClsFileOperations.WriteFile(LogFileName, ex.ToString());
                            }
                        }

                    }

                }
                catch (Exception ex)
                {

                    if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                    {
                        ClsFileOperations.AppendFile(LogFileName, ex.ToString());
                    }
                    else
                    {
                        ClsFileOperations.WriteFile(LogFileName, ex.ToString());
                    }

                    if (ex.Message != "Cannot find table 0.") //Delete and update queries throws this message and this should not appear
                    {
                        MessageBox.Show("Error: " + ex.Message, "Loodon: SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
#endregion
            }
            }
        }
        
        public void ExecuteSelectedQueryforEditorOutput(string Query)
        {
            ClsCommon common = new ClsCommon();
            string query = Query;

            if (query.Length < 1) //If there is no selected text then pick all the text as an Query
            {
                query = richTextBox1.Text;

            }

            DateTime dtExecutionTimeStart = DateTime.Now;

            if (query.Length > 0)
            {
                tslblStatus.Text = "Working...";
                string LogFileName = common.Read("LOGFILESAVELOCATION") + "\\" + "LooLog_" + DateTime.Now.Date.ToString().Replace("00", "").Replace(":", "") + ".txt";
                ClsFileOperations fileOperations = new ClsFileOperations();

                try
                {
                    //ExecuteInsertStatementsForDataset fetches the data also
                    dstOutputDataset = ClsCommon.ExecuteInsertStatementsForDataset(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, ClsCommon.DatabaseName, query);

                    DateTime dtTimeStampCurrent = DateTime.Now;
                    string diffTimeQueryExecution = (dtTimeStampCurrent.Hour - dtExecutionTimeStart.Hour).ToString() + ":" + (dtTimeStampCurrent.Minute - dtExecutionTimeStart.Minute).ToString() + ":" + (dtTimeStampCurrent.Second - dtExecutionTimeStart.Second).ToString();

                    tslblExecutionTime.Text = "Execution Time: " + diffTimeQueryExecution;

                    long totalRecords = dstOutputDataset.Tables[0].Rows.Count;
                    try
                    {                       
                        tslblTotalRecords.Text = "Total Records: " + (totalRecords).ToString();
                    }
                    catch (Exception)
                    {
                        tslblTotalRecords.Text = "Query execution failure.";
                    }
                    tslblStatus.Text = string.Empty;
                    statusStrip1.Refresh();
                    
                    DataSet dst = new DataSet ();
                    string editorOutput = string.Empty;

                    dst = dstOutputDataset;
                    editorOutput = new LoodonDAL.ClsCommon().ConvertDatasettoString(dst);
                    richTextBox1.Text = editorOutput;

                    splitContainer2.SplitterDistance = splitContainer2.Height - 300;
                                       
                    //Row numbers
                    if (common.Read("ROWNUMBERS") == "TRUE")
                    {
                        setRowNumber();
                    }                    

                   //Log Results
                   //1. Check whether Logging is Enabled or not
                   //2. Prepare the data and Write/Append in a file in the following format:
                   //-------------------------------------------------------------------------
                   //20th May 2013: 09:00:56
                   //-------------------------------------------------------------------------
                   //Server:
                   //Database:

                   //Statement: Select * from
                   //Results: 10 Records
                   //-------------------------------------------------------------------------
                   //3. If Log File not exists then create a new one or else use the existed one                   
                    
                    if (common.Read("ENABLELOGGING") == "TRUE") //1. Check whether Logging is Enabled or not
                    {                                          
                        try
                        {
                            string queryLogDetails = string.Empty;                            

                            //2. Prepare the data and Write/Append in a file in the following format:
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Date:" + DateTime.Now.ToString() + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Server:" + ClsCommon.ServerName + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Database:" + ClsCommon.DatabaseName + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Statement:" + query + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "Results Count:" + totalRecords.ToString() + Environment.NewLine;
                            queryLogDetails = queryLogDetails + "-------------------------------------------------------------------------" + Environment.NewLine;

                            
                            if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                            {
                                ClsFileOperations.AppendFile(LogFileName, queryLogDetails);
                            }
                            else
                            {
                                ClsFileOperations.WriteFile(LogFileName, queryLogDetails);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                            {
                                ClsFileOperations.AppendFile(LogFileName, ex.ToString());
                            }
                            else
                            {
                                ClsFileOperations.WriteFile(LogFileName, ex.ToString());
                            }
                        }
                        
                    }
                    
                    
                }
                catch (Exception ex)
                {                    

                    if (System.IO.File.Exists(LogFileName))//3. If Log File not exists then create a new one or else use the existed one       
                    {
                        ClsFileOperations.AppendFile(LogFileName, ex.ToString());
                    }
                    else
                    {
                        ClsFileOperations.WriteFile(LogFileName, ex.ToString());
                    }

                    MessageBox.Show("Error: " + ex.Message, "Loodon: SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void setRowNumber()
        {
            foreach (DataGridViewRow row in dgrdDataOutput.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBox1.SelectionFont = new Font(LoodonDAL.ClsConfiguration.FontName, LoodonDAL.ClsConfiguration.FontSize, FontStyle.Regular);
            richTextBox1.SelectionColor = System.Drawing.Color.Black;
        }

        public void Undo()
        {
            richTextBox1.Undo();
        }

        public void Paste()
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Paste();               
            }
        }

        public void Redo()
        {
            richTextBox1.Redo();
        }

        public void Cut()
        {
            richTextBox1.Cut();
        }

        public void Copy()
        {
            richTextBox1.Copy();
        }

        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        public void FindandReplace()
        {
            panel1.Visible = true;
            txtFind.Focus();
        }

        /// <summary>
        /// Highlights the text to find one by one in a Forward direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            int startindex = 0;
            DeSelectText(0, richTextBox1.Text.Length - 1);//First De-highlight the previous selected text

            if (txtFind.Text.Length > 0)
                startindex = FindMyText(txtFind.Text.Trim(), start, richTextBox1.Text.Length, RichTextBoxFinds.None);
            
            if (startindex >= 0) // If string was found in the RichTextBox, highlight it
            {                
                richTextBox1.SelectionBackColor = Color.LightBlue; //Highlighted BackColor
                richTextBox1.SelectionColor = Color.White; //Highlighted SelectionColor
                int endindex = txtFind.Text.Length;// Find the end index. End Index = number of characters in textbox
                richTextBox1.Select(startindex, endindex);// Select the search string                                
                start = startindex + endindex; // mark the start position after the position of last search string
                
                findStartIndex = startindex;//Tracks the start point to clear the selection onClick event
                findEndIndex = endindex;//Tracks the end point to clear the selection onClick event
            }

            //Reset the start index
            if (startindex <0)
            {
                start = 0;
            }
        }

        public int FindMyText(string txtToSearch, int searchStart, int searchEnd, RichTextBoxFinds FindDirection)
        {
            // Unselect the previously searched string
            if (searchStart > 0 && searchEnd > 0 && indexOfSearchText >= 0 || FindDirection == RichTextBoxFinds.Reverse)
            {
                richTextBox1.Undo();
                richTextBox1.Undo(); 
            }

            // Set the return value to -1 by default.
            int retVal = -1;

            // A valid starting index should be specified.
            // if indexOfSearchText = -1, the end of search
            if (searchStart >= 0 && indexOfSearchText >= 0)
            {
                // A valid ending index
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Find the position of search string in RichTextBox
                    indexOfSearchText = richTextBox1.Find(txtToSearch, searchStart, searchEnd, FindDirection);
                    // Determine whether the text was found in richTextBox1.
                    if (indexOfSearchText != -1)
                    {
                        // Return the index to the specified search text.
                        retVal = indexOfSearchText;
                    }
                }
            }
            return retVal;
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        /// <summary>
        /// Highlights the text to find one by one in a Backward direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int startindex = 0;
            start = 0;
            DeSelectText(0, richTextBox1.Text.Length - 1);//First De-highlight the previous selected text

            if (txtFind.Text.Length > 0)
                startindex = FindMyText(txtFind.Text.Trim(), start, richTextBox1.SelectionStart, RichTextBoxFinds.Reverse);
            
            if (startindex >= 0)// If string was found in the RichTextBox, highlight it
            {                
                richTextBox1.SelectionBackColor = Color.LightBlue;//Highlighted BackColor
                richTextBox1.SelectionColor = Color.White;//Highlighted SelectionColor
                int endindex = txtFind.Text.Length;// Find the end index. End Index = number of characters in textbox
                richTextBox1.Select(startindex, endindex);// Select the search string
                start = startindex + endindex;// mark the start position after the position of last search string

                findStartIndex = startindex;//Tracks the start point to clear the selection onClick event
                findEndIndex = endindex;//Tracks the end point to clear the selection onClick event
            }            
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            //Get the selected word and replace
            //string selectedWord = richTextBox1.Text.Substring(richTextBox1.SelectionStart, richTextBox1.SelectionLength);

            richTextBox1.SelectedText = txtReplace.Text;

        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            richTextBox1.Text= richTextBox1.Text.Replace(txtFind.Text, txtReplace.Text);
        }

        public void CollapseResults()
        {
            splitContainer2.SplitterDistance = splitContainer2.Height - 5;
        }

        public void ExpandResults()
        {
            splitContainer2.SplitterDistance = splitContainer2.Height - 300;
        }

        private void dgrdDataOutput_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Select the row on right click
                try
                {
                    
                    var hti = dgrdDataOutput.HitTest(e.X, e.Y);
                    selectedRow = hti.RowIndex;
                    dgrdDataOutput.ClearSelection();
                    //dgrdDataOutput.Rows[hti.RowIndex].Selected = true;
                    dgrdDataOutput.CurrentCell = dgrdDataOutput.Rows[hti.RowIndex].Cells[0];
                    dgrdDataOutput.Rows[hti.RowIndex].Selected = true;

                }
                catch (Exception)
                {
                   
                }               

                cmsGridMenu.Show(Cursor.Position.X, Cursor.Position.Y);

            }
        }

        public DataTable GetDataGridRecords()
        {
            DataTable dtbl = (DataTable)(dgrdDataOutput.DataSource);
            return dtbl;
        }

        private void dgrdDataOutput_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dgrdDataOutput.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
        }

        private void dgrdDataOutput_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.dgrdDataOutput.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
        }

        public void RTBUndo()
        {            
            richTextBox1.Undo();
        }

        public void ColumnResize()
        {
            dgrdDataOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {                       
            //---Change the selection color,if any, to white background and black foreground
            int CurrentPos = richTextBox1.SelectionStart;
            if (findStartIndex > 0) //If the value is greater than 0 than last operation may be Find and REplace
            {

            //Execute DeSelect only if there is anyword selected. and variable start contains the index of caret
            //as start variable is using in find operation and find operations normally selects the targetted word
            //if (start > 0) 
            //{
                //DeSelectText(0, richTextBox1.Text.Length - 1);
                DeSelectText(findStartIndex, findEndIndex);
                richTextBox1.SelectionStart = CurrentPos; //Set the current position again to the previous one
            //}

                findStartIndex = 0; //set to zero to prevent the selection again and again on every click
            }
            //---Change the selection color,if any, to white background and black foreground
        }

        /// <summary>
        /// De selectes the text to the default one with white background and Black foreground
        /// </summary>
        /// <param name="StartPos">Starting Index</param>
        /// <param name="EndPos">End Index</param>
        private void DeSelectText(int StartPos, int EndPos)
        {
            richTextBox1.Select(StartPos, EndPos);
            richTextBox1.SelectionBackColor = Color.White;
            richTextBox1.SelectionColor = Color.Black;            
        }

        private void cmsGridMenu_Opening(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// Display the selected row vertically
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editCurrentRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get the single selected row, if more than one rows, no function
            if (dgrdDataOutput.SelectedRows.Count == 1)
            {
                DataSet dst = new DataSet();
                dst.Tables.Add("ROW");
                dst.Tables["ROW"].Columns.Add("Column");
                dst.Tables["ROW"].Columns.Add("Value");

                for (int i = 0;  i <= dgrdDataOutput.ColumnCount - 1; i++)
                {
                    dst.Tables["ROW"].Rows.Add(dgrdDataOutput.Columns[i].Name, dgrdDataOutput.Rows[selectedRow].Cells[i].Value.ToString());
                }
                new Loodon.FrmHorizontalRow(dst).ShowDialog();
            }
        }

        private void dgrdDataOutput_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }

        private void separateResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Loodon.FrmSeparateResults(GetDataGridRecords(), this.Height, this.Width).ShowDialog();
        }

        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(typeof(string))) e.Effect = DragDropEffects.Copy;


            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode") || e.Data.GetDataPresent(typeof(string)))
            //if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            int i;
            String s;
            string receivedText = string.Empty;

            // Get start position to drop the text.
            i = richTextBox1.SelectionStart;
            s = richTextBox1.Text.Substring(i);
            richTextBox1.Text = richTextBox1.Text.Substring(0, i);

            // Drop the text on to the RichTextBox.
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                receivedText = e.Data.GetData("System.Windows.Forms.TreeNode").ToString().Replace("TreeNode: ","");
            }

            if (e.Data.GetDataPresent(typeof(string)))
            {
                receivedText = e.Data.GetData(typeof(string)).ToString();
            }

            richTextBox1.Text = richTextBox1.Text + receivedText;
            richTextBox1.Text = richTextBox1.Text + s;
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    RichTextBox box = (RichTextBox)sender;
            //    box.SelectionStart = box.GetCharIndexFromPosition(e.Location);
            //    box.SelectionLength = 0;

            //    //cmsPlatformRightMenu.Show(Cursor.Position.X, Cursor.Position.Y);
            //}
        }

        private void executeSelectedQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteSelectedQuery();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            ////if (e.Button == MouseButtons.Right)
            ////{
            ////    //The code below moves the caret position on right click
            ////    //When you left-click in the text the cursor jumps to where you clicked. 
            ////    RichTextBox box = (RichTextBox)sender;
            ////    box.SelectionStart = box.GetCharIndexFromPosition(e.Location);
            ////    box.SelectionLength = 0;
            ////}

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   
                richTextBox1.ContextMenuStrip = cmsPlatformRightMenu;
            }
        }

        public void CutAction(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        public void CopyAction(object sender, EventArgs e)
        {           
            Clipboard.SetData(DataFormats.Rtf, richTextBox1.SelectedRtf);           
        }

        public void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                richTextBox1.SelectedRtf = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        //if you want to copy paste with another application like notepad (text only not the styles) please replace following methods
        //---------------------------------------------------------------------------
        ////public void CopyAction(object sender, EventArgs e)
        ////{
        ////    Clipboard.SetText(richTextBox1.SelectedText);
        ////}

        ////public void PasteAction(object sender, EventArgs e)
        ////{
        ////    if (Clipboard.ContainsText())
        ////    {
        ////        int pastePosStart = richTextBox1.SelectionStart;
        ////        richTextBox1.SelectedText = Clipboard.GetText(TextDataFormat.Text).ToString();

        ////        //richTextBox1.Text
        ////        //    += Clipboard.GetText(TextDataFormat.Text).ToString();
        ////    }
        ////}
        //---------------------------------------------------------------------------

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CutAction(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyAction(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteAction(sender, e);
        } 
    }
}
