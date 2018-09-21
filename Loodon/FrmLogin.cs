using System;
using System.Data;
using System.Windows.Forms;
using SQLBackup;

namespace Loodon
{
    public partial class FrmLogin : Form
    {
        bool _flagTabIncrement;
        public bool IncrementTab { get; set; }
        public string TabServername { get; set; }
        //public string TabDatabaseName { get; set; }

        /// <summary>
        /// Loads the frmLogin based on the Servername, Usernamd and OpenNewConnection information
        /// </summary>
        /// <param name="servername">Servername if available else provide the String.Empty</param>
        /// <param name="username">Username if available else provide the String.Empty</param>
        /// <param name="openNewConnection">Whether it is a new connection not exists in the previously loaded connections xml</param>
        public FrmLogin(string servername, string username, bool openNewConnection)
        {
            InitializeComponent();

            if ((servername.Length <= 0 || username.Length <= 0) && !openNewConnection) return;
            txtServerName.Text = servername;
            txtLogin.Text = username;
            _flagTabIncrement = true;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var common = new ClsCommon();
                var message = string.Empty;

                //Validations
                message = message + common.ValidateInput(txtServerName.Text, lblServerName.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtLogin.Text, lblLogin.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtPassword.Text, lblPassword.Text, ValidationType.EmptyCheck);

                if (message.Length > 1)
                {
                    MessageBox.Show("Following information is mandatory to provide:\n\n\n" + message, "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    //Authenticate the user
                    if (common.IsUserAuthenticated(txtServerName.Text, txtLogin.Text, txtPassword.Text))
                    {
                        //Fills the Combobox with the permitted database list
                        //======================================================================================
                        cmbDatabase.Enabled = true;
                        cmbDatabase.DataSource = common.GetDatabases().Tables[0];
                        cmbDatabase.DisplayMember = "name";
                        cmbDatabase.ValueMember = "name";
                        cmbDatabase.SelectedIndex = 0;
                        //======================================================================================
                        btnProceed.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials provided.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {                
                var error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_flagTabIncrement)
            {
                Close();
            }
            else
            {
                Environment.Exit(-1); 
            }
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            var server = txtServerName.Text.Trim();
            var username = txtLogin.Text.Trim();

            try
            {
                ClsCommon.DatabaseName = cmbDatabase.Text;
                ClsCommon.LoginName = txtLogin.Text;
                ClsCommon.PasswordName = txtPassword.Text;
                ClsCommon.ServerName = txtServerName.Text;

                //System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                //t.Start();

                //Enter the entry in the Login Log XML
                //Create the xml if not exists
                //------------------------------------------------------------------------------------------------------

                try
                {
                    var dstLoginLog = new DataSet();

                    if (System.IO.File.Exists("XML\\LoginLogs.xml"))
                    {
                        dstLoginLog.ReadXml("XML\\LoginLogs.xml");
                    }
                    else
                    {
                        dstLoginLog.Tables.Add("Logs");
                        dstLoginLog.Tables["Logs"].Columns.Add("Server");
                        dstLoginLog.Tables["Logs"].Columns.Add("Username");
                    }

                    if (!LoginLogsIsDuplicate(server, username))
                    {
                        dstLoginLog.Tables["Logs"].Rows.Add(server, username);
                        dstLoginLog.WriteXml("XML\\LoginLogs.xml");
                    }

                    }
                catch (Exception ex)
                {
                    //string errorMessage = "\n" +  ex.Data + "\n" + ex.Message + "\n" + ex.Source;

                    //MessageBox.Show("There is an Error to check on your system.\nThis can affect the functionality. Here are the details: " + errorMessage);


                }

                //Maintain the pervasive dataset for Active Servers
                ClsCommon.PublicDatasetServers.Tables["ServersList"].Rows.Add(server, username, txtPassword.Text, cmbDatabase.Text, server + "." + cmbDatabase.Text); //Column definition added in frmSplash
               

                //------------------------------------------------------------------------------------------------------

                //The form frmLogin calls from two places
                //1. After the splash screen
                //2. Inside the software from the Connection toolbar, when user clicks the Open Connection button.
                //Clicking on Proceed on frmLogin opens the frmmain but from the Open Connection it is not as it opens an existed connection
                //and increments the tab. The variable: flagTabIncrement decided whether to increment the tab or open the frmmain           

                if (_flagTabIncrement == true)
                {
                    IncrementTab = true;
                    TabServername = ClsCommon.ServerName + "#" + ClsCommon.DatabaseName;
                }
                else
                {
                    var frmmain = new FrmMain();
                    frmmain.Show();
                }                

                Hide();
            }
            catch (Exception ex)
            {
                var error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                     
        }

        public static void ThreadProc()
        {
            Application.Run(new FrmMain() );
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private bool LoginLogsIsDuplicate(string servername, string username)
        {
            var response = false;

            var dstLoginLog = new DataSet();
            if (!System.IO.File.Exists("XML\\LoginLogs.xml")) return response;
            dstLoginLog.ReadXml("XML\\LoginLogs.xml");

            for (var i = 0; i <= dstLoginLog.Tables["Logs"].Rows.Count - 1; i++)
            {
                if (dstLoginLog.Tables["Logs"].Rows[i]["Server"].ToString() == servername && dstLoginLog.Tables["Logs"].Rows[i]["Username"].ToString() == username)
                {
                    response = true;
                }                    
            }

            return response;
        }

        private void btnBrowseLogin_Click(object sender, EventArgs e)
        {
            //Loodon.frmLoginHistory loginHistory = new Loodon.frmLoginHistory();
            //loginHistory.ShowDialog();

            using (var loginHistory = new Loodon.FrmLoginHistory(true))
            {
                //var result = loginHistory.ShowDialog();
              
                    var serverName = loginHistory.ServerName;       
                    //values preserved after close
                    var userName = loginHistory.UserName;
                    //Do something here with these values

                    txtServerName.Text = serverName;
                    txtLogin.Text = userName;
                
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConnect_Click(sender,e);
            }
        }
    }


}