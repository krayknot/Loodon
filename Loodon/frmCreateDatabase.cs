using System;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmCreateDatabase : Form
    {
        public FrmCreateDatabase()
        {
            InitializeComponent();
        }

        private void txtDBName_TextChanged(object sender, EventArgs e)
        {
            txtDFDataFilename.Text = txtDBName.Text + "_Data";
            txtDFMDFFilename.Text = "C:\\" + txtDBName.Text + "_Data.mdf";

            txtLFFilename.Text = txtDBName.Text + "_Log";
            txtLFLDFFilename.Text = "C:\\" +  txtDBName.Text + "_Log.ldf";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var dbParam = new ClsCommon.DatabaseParam();
            dbParam.DatabaseName = txtDBName.Text;
            //Assign Data file parameters
            if (rdbDFinmegabytes.Checked == true)
            {
                dbParam.DataFileGrowth = txtDFinmegabytes.Text;
            }
            else
            {
                dbParam.DataFileGrowth = txtDFbypercent.Text + "%";
            }

            dbParam.DataFileName = txtDFDataFilename.Text;
            dbParam.DataFileSize = "2";//2MB at the init state
            dbParam.DataPathName = txtDFMDFFilename.Text;
            //Assign Log file parameters
            if (rdbLFinmegabytes.Checked == true)
            {
                dbParam.LogFileGrowth = txtLFinimegabytes.Text;
            }
            else
            {
                dbParam.LogFileGrowth = txtLFbypercent.Text + "%";
            }

            dbParam.LogFileName = txtLFFilename.Text;
            dbParam.LogFileSize = "1";//1MB at the init state
            dbParam.LogPathName = txtLFLDFFilename.Text;

            ClsCommon.CreateDatabase(dbParam);

            MessageBox.Show("Database created successfully.\nTo view the database, you need to re-login.", "Loodon: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }
    }
}
