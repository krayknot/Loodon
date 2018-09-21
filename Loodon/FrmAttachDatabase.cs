using System;
using System.Data;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmAttachDatabase : Form
    {
        readonly string _selectedServer;
        
        public FrmAttachDatabase(string selectedTabText)
        {
            InitializeComponent();
            _selectedServer = selectedTabText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            //Validations
            var databaseName = txtDatabaseName.Text;
            var dbMDF = txtDatabasemdf.Text;
            var dbLDF = txtDatabaseldf.Text;
            var query = string.Empty;

            var flag = true;

            if (databaseName.Trim().Length < 1)
            {
                MessageBox.Show("Database name is mandatory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
            }

            if (dbMDF.Trim().Length < 1)
            {
                MessageBox.Show("Database .mdf is mandatory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag = false;
            }

            try
            {
                if (flag != true) return;
                if (dbMDF.Trim().Length > 0 && dbLDF.Trim().Length < 1)
                {
                    query = "CREATE DATABASE " + databaseName + " ON " + "PRIMARY ( FILENAME =  '" + dbMDF + "' ) " + " FOR ATTACH";
                }
                else if (dbMDF.Trim().Length > 0 && dbLDF.Trim().Length > 0)
                {
                    query = "CREATE DATABASE " + databaseName + " ON " + "PRIMARY ( FILENAME =  '" + dbMDF + "' ), " + "FILEGROUP MyDatabase_Log ( FILENAME =  '" + dbLDF + "')" + "FOR ATTACH";
                }

                ServerExistsinPervasiveDataset(_selectedServer); //To arrange the connection string elements

                var common = new ClsCommon();
                ClsCommon.AttachSqlDatabase(ClsCommon.ServerName, ClsCommon.LoginName, ClsCommon.PasswordName, query);

                MessageBox.Show("Database has attached successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Database Attach", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }         
        }

        private bool ServerExistsinPervasiveDataset(string serverName)
        {
            var response = false;

            // Presuming the DataTable has a column named Date. 
            var expression = "ServerDatabase = '" + serverName + "'";

            // Sort descending by column named CompanyName. 
            var sortOrder = "Server ASC";

            // Use the Select method to find all rows matching the filter.
            var foundRows = ClsCommon.PublicDatasetServers.Tables[0].Select(expression, sortOrder);

            foreach (DataRow dr in foundRows)
            {
                ClsCommon.ServerName = dr["Server"].ToString();
                ClsCommon.LoginName = dr["Username"].ToString();
                ClsCommon.PasswordName = dr["Password"].ToString();
                ClsCommon.DatabaseName = dr["Database"].ToString();
            }

            return false;
        }

        private void btnBrowsemdf_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Attach Database|*.mdf";
                openFileDialog1.Title = "Database Backup";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtDatabasemdf.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error: Database Attach", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnBrowseldf_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Attach Database|*.ldf";
                openFileDialog1.Title = "Database Backup";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    txtDatabaseldf.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Database Attach", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
