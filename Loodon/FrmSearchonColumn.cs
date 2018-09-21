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
    public partial class FrmSearchonColumn : Form
    {
        string ServerName = string.Empty;
        string Username = string.Empty;
        string Password = string.Empty;
        string DBName = string.Empty;
        string ColumnName = string.Empty;
        string TableName = string.Empty;
        int windHeight = 0;
        int windWidth = 0;

        public FrmSearchonColumn(string Server, string User, string Pass, string Database, string Col, string Table, int WindHeight, int WindWidth)
        {
            InitializeComponent();

            ServerName = Server;
            Username = User;
            Password = Pass;
            DBName = Database;
            ColumnName = Col;
            TableName = Table;
            windHeight = WindHeight;
            windWidth = WindWidth;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtSearch.Text;

            if(rdbFreeSearch.Checked)
            {
                searchString = ColumnName + " like '%" + searchString +"%'";
            }
            else if(rdbMatchCase.Checked)
            {
                searchString = ColumnName + " = '" + searchString + "'";
            }

            string query = "Select * from " + TableName + " Where " + searchString;

            DataSet dst = new DataSet();
            dst = LoodonDAL.ClsCommon.GetDatasetFromQuery(query, ServerName, Username, Password, DBName);

            new FrmSeparateResults(dst.Tables[0], windHeight, windWidth).ShowDialog();
        }

        private void frmSearchonColumn_Load(object sender, EventArgs e)
        {
            lblColumn.Text = ColumnName;
        }
    }
}
