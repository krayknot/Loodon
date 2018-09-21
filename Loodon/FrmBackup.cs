using System;
using System.Windows.Forms;
using SQLBackup;

namespace Loodon
{
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text.Trim() == "Backup")
            {
                if (txtDatabasemdf.Text.Length < 1)
                {
                    MessageBox.Show("No Database Backup file path found!!", "Information: Database Backup",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        var common = new ClsCommon();
                        ClsCommon.BackupDatabase(txtDatabasemdf.Text);

                        MessageBox.Show("Database Backup completed successfully.", "Information: Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error: Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
            else if (tabControl1.SelectedTab.Text.Trim() == "Restore" )
            {
                if (txtRestoreDatabase.Text.Length < 1 || txtDbName.Text.Length < 1)
                {
                    MessageBox.Show("No Database Restore file path | Database Name found!!", "Information: Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        var common = new ClsCommon();
                        ClsCommon.RestoreDatabase(txtRestoreDatabase.Text, txtDbName.Text);

                        MessageBox.Show("Database Restore completed successfully.\nYou can use it by login again.", "Information: Database Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error: Database Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
            }
            else if (tabControl1.SelectedTab.Text.Trim() == "Backup-External")
            {
                ClsCommon.ExcelMode = ExcelBackupMode.SeparateSheet;

                var frmtakingbackup = new frmTakingBackup();
                frmtakingbackup.ShowDialog();
            }
        }

        private void btnBrowsemdf_Click(object sender, EventArgs e)
        {            
            saveFileDialog1.Title = "Database Backup";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDatabasemdf.Text = saveFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Restore Database|*.bak";
            openFileDialog1.Title = "Database Restore";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRestoreDatabase.Text = openFileDialog1.FileName;
            }
        }
    }
}