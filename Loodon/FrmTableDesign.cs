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
    public partial class FrmTableDesign : Form
    {
        string tableName = string.Empty;
        string serverName = string.Empty;
        string databaseName = string.Empty;
        string userName = string.Empty;
        string passWord = string.Empty;



        public FrmTableDesign(string TableName, string Server, string Database, string Username, string Password)
        {
            InitializeComponent();
            tableName = TableName;
            serverName = Server;
            databaseName = Database;
            userName = Username;
            passWord = Password;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTableDesign_Load(object sender, EventArgs e)
        {
            LoadColumnDetails();
        }

        private void LoadColumnDetails()
        {
            DataSet dst = new DataSet();
            dst = new ClsCommon().GetColumnsofTable(serverName, userName, passWord, databaseName, tableName);
            dst.Tables[0].Columns[0].ColumnName = "Column Name";
            dst.Tables[0].Columns[1].ColumnName = "Column Type";
            dst.Tables[0].Columns[2].ColumnName = "Size";
            dgrdColumnsDetails.DataSource = dst.Tables[0];
        }

        private void btnAddNewColumn_Click(object sender, EventArgs e)
        {
            new Loodon.FrmAddColumn(tableName).ShowDialog();

            LoadColumnDetails();
        }
    }
}
