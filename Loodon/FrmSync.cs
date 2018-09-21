using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Loodon;

namespace SQLBackup
{
    public partial class frmSync : Form
    {
        public frmSync()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSConnect_Click(object sender, EventArgs e)
        {
            //Date: 27th October 2012
            //By: Kshitij [krayknot@gmail.com]            
            //Connects to the Database based on the Server, Username and Password.
            //Grows the List of the Databases found on the credentials provided.
            //-----------------------------------------------------------------------------------------------------------------------------
            try
            {
                ClsCommon common = new ClsCommon();
                string message = string.Empty;

                //Validations
                //Checks the blank input and provides the message based on the Label Text, like, if Server is Empty,
                //it provides the message by pointing the Label Text ['Server']. 
                //For more: Check the Method ValidateInput in Common class
                //----------------------------------------------------------------------------------------------------
                message = message + common.ValidateInput(txtSServer.Text, label1.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtSUsername.Text, label2.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtSPassword.Text, label3.Text, ValidationType.EmptyCheck);

                if (message.Length > 1)
                {
                    MessageBox.Show("Following information is mandatory to provide:\n\n\n" + message, "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    message = string.Empty;
                }
                else
                {
                    //Authenticates the user
                    //Gets the Databases based on the Credentials provided and grows the list.
                    //-----------------------------------------------------------------------------------------------
                    if (common.IsUserAuthenticated(txtSServer.Text, txtSUsername.Text, txtSPassword.Text))
                    {
                        //Fills the Combobox with the permitted database list
                        //======================================================================================
                        cmbSDatabase.Enabled = true;
                        cmbSDatabase.DataSource = common.GetDatabases().Tables[0];
                        cmbSDatabase.DisplayMember = "name";
                        cmbSDatabase.ValueMember = "name";
                        cmbSDatabase.SelectedIndex = 0;
                        //======================================================================================
                        //btnProceed.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials provided.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestSConnection_Click(object sender, EventArgs e)
        {
            //Date: 27th October 2012
            //By: Kshitij [krayknot@gmail.com]
            //Tests the Source Database Connection
            //Creates the connection to the Database and try to open it
            //Provides true if succeeds else false and provides the Exception
            //---------------------------------------------------------------
            try
            {
                string conString = "Data Source=" + txtSServer.Text + ";Initial Catalog=" + cmbSDatabase.Text + ";User ID="+ txtSUsername.Text +";Password=" + txtSPassword.Text + ";";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    MessageBox.Show("Text Connection Succeeds.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Text Connection Failed.\n\nReason: " + ex.Message, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDConnect_Click(object sender, EventArgs e)
        {
            //Date: 27th October 2012
            //By: Kshitij [krayknot@gmail.com]            
            //Connects to the Database based on the Server, Username and Password.
            //Grows the List of the Databases found on the credentials provided.
            //-----------------------------------------------------------------------------------------------------------------------------
            try
            {
                ClsCommon common = new ClsCommon();
                string message = string.Empty;

                //Validations
                //Checks the blank input and provides the message based on the Label Text, like, if Server is Empty,
                //it provides the message by pointing the Label Text ['Server']. 
                //For more: Check the Method ValidateInput in Common class
                //----------------------------------------------------------------------------------------------------
                message = message + common.ValidateInput(txtDServer.Text, label8.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtDUsername.Text, label7.Text, ValidationType.EmptyCheck);
                message = message + common.ValidateInput(txtDPassword.Text, label6.Text, ValidationType.EmptyCheck);

                if (message.Length > 1)
                {
                    MessageBox.Show("Following information is mandatory to provide:\n\n\n" + message, "Information", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    message = string.Empty;
                }
                else
                {
                    //Authenticates the user
                    //Gets the Databases based on the Credentials provided and grows the list.
                    //-----------------------------------------------------------------------------------------------
                    if (common.IsUserAuthenticated(txtDServer.Text, txtDUsername.Text, txtDPassword.Text))
                    {
                        //Fills the Combobox with the permitted database list
                        //======================================================================================
                        cmbDDatabase.Enabled = true;
                        cmbDDatabase.DataSource = common.GetDatabases().Tables[0];
                        cmbDDatabase.DisplayMember = "name";
                        cmbDDatabase.ValueMember = "name";
                        cmbDDatabase.SelectedIndex = 0;
                        //======================================================================================
                        //btnProceed.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credentials provided.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "Cannot Proceed or not functional due to the following Exception: \n\n" + ex.Message + "\n\nFor further information please contact to vendor or visit www.krayknot.com";
                MessageBox.Show(error, "LooDon: Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestDConnection_Click(object sender, EventArgs e)
        {
            //Date: 27th October 2012
            //By: Kshitij [krayknot@gmail.com]
            //Tests the Source Database Connection
            //Creates the connection to the Database and try to open it
            //Provides true if succeeds else false and provides the Exception
            //---------------------------------------------------------------
            try
            {
                string conString = "Data Source=" + txtDServer.Text + ";Initial Catalog=" + cmbDDatabase.Text + ";User ID=" + txtDUsername.Text + ";Password=" + txtDPassword.Text + ";";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    MessageBox.Show("Text Connection Succeeds.", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Text Connection Failed.\n\nReason: " + ex.Message, "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSourcetoDest_Click(object sender, EventArgs e)
        {
            bool isLocalDestination = false;
            if (chkLocal.CheckState == CheckState.Checked)
                isLocalDestination = true;
            else
                isLocalDestination = false;

            bool isLocalSource = false;
            if (chkLocalSource.CheckState == CheckState.Checked)
                isLocalSource = true;
            else
                isLocalSource = false;


            frmSyncWindow syncWindow = new frmSyncWindow(txtSServer.Text, txtSUsername.Text, txtSPassword.Text, cmbSDatabase.Text,
                    txtDServer.Text, txtDUsername.Text, txtDPassword.Text, cmbDDatabase.Text, frmSyncWindow.SyncDirection.SourceToDestination, isLocalDestination, isLocalSource);
            syncWindow.ShowDialog();
        }

        private void btnDesttoSource_Click(object sender, EventArgs e)
        {
            //bool isLocalDestination = false;
            //if (chkLocal.CheckState == CheckState.Checked)
            //    isLocalDestination = true;
            //else
            //    isLocalDestination = false;
            //frmSyncWindow syncWindow = new frmSyncWindow(txtSServer.Text, txtSUsername.Text, txtSPassword.Text, cmbSDatabase.Text,
            //    txtDServer.Text, txtDUsername.Text, txtDPassword.Text, cmbDDatabase.Text, frmSyncWindow.SyncDirection.DestinationToSource, isLocalDestination);
            //syncWindow.ShowDialog();
        }

        private void frmSync_Load(object sender, EventArgs e)
        {
            string message = "A Link Server entry is required in the Destnation Database for the Source Database.\nYou are required to " +
                "create this on manual basis.";
            MessageBox.Show(message,"Loodon: Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
