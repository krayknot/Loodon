using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmTablefromColumn : Form
    {
        string serverName = string.Empty;
        string databaseName = string.Empty;
        string userName = string.Empty;
        string passWord = string.Empty;

        public FrmTablefromColumn(string Server, string Database, string Username, string Password)
        {
            InitializeComponent();
            serverName = Server;
            databaseName = Database;
            userName = Username;
            passWord = Password;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string columnName = txtColumntoSearch.Text.Trim();

            dgrdResults.DataSource = new LoodonDAL.ClsSelect().GetTablefromColumn(columnName, serverName, userName, passWord, databaseName).Tables[0];
            lblMessage.Text = "Total Records: " + (dgrdResults.Rows.Count).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
