using System;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmAddColumn : Form
    {
        readonly string tablename = string.Empty;

        public FrmAddColumn(string tablename)
        {
            InitializeComponent();
            this.tablename = tablename;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAddColumn_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var query = "ALTER TABLE " + tablename + " ADD ";

            var columnName = txtColumn.Text;
            var columnType = cmbColumnType.Text;

            query = query + columnName + " " + columnType;

            var serverName = ClsCommon.ServerName;
            var databaseName =ClsCommon.DatabaseName;
            var userName = ClsCommon.LoginName;
            var passWord = ClsCommon.PasswordName;

            try
            {
                ClsCommon.ExecuteInsertStatements(serverName, userName, passWord, databaseName, query);
                MessageBox.Show("New column has added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
    }
}
