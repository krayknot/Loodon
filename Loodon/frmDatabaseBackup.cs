using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Loodon
{
    public partial class FrmDatabaseBackup : Form
    {
        public FrmDatabaseBackup()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTakeBackup_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "SQL Server database backup files|*.bak";
            saveFileDialog1.Title = "Database Backup";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            var s = new SqlConnection("Data Source=" + ClsCommon.ServerName + ";Initial Catalog=" + ClsCommon.DatabaseName + ";Integrated Security=True;Pooling=False; User ID = " + ClsCommon.LoginName + "; Pwd = " + ClsCommon.PasswordName);
            var bu2 = new SqlCommand("BACKUP DATABASE " + ClsCommon.DatabaseName + " TO DISK='" + saveFileDialog1.FileName + "'", s);

            s.Open();
            bu2.ExecuteNonQuery();
            s.Close();

            MessageBox.Show("Database Backup successfull.", "Loodon: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
