using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Loodon;

namespace SQLBackup
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Enabled = false;

            FrmLogin login= new FrmLogin("", "", false);
            login.ShowDialog();

        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            //Initialize the pervasive dataset 
            ClsCommon.PublicDatasetServers = new DataSet();
            ClsCommon.PublicDatasetServers.Tables.Add("ServersList");
            ClsCommon.PublicDatasetServers.Tables["ServersList"].Columns.Add("Server");
            ClsCommon.PublicDatasetServers.Tables["ServersList"].Columns.Add("Username");
            ClsCommon.PublicDatasetServers.Tables["ServersList"].Columns.Add("Password");
            ClsCommon.PublicDatasetServers.Tables["ServersList"].Columns.Add("Database");
            ClsCommon.PublicDatasetServers.Tables["ServersList"].Columns.Add("ServerDatabase");            
            
            

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}