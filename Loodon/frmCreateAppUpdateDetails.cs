using System;
using System.Data;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmCreateAppUpdateDetails : Form
    {
        public FrmCreateAppUpdateDetails()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var timestamp = txtTimestamp.Text;
            var appName = txtAppName.Text;
            var appSize = txtAppSize.Text;
            var updatedBy = txtUpdatedBy.Text;
            var updatedSource = txtUpdatedSource.Text;
            var details = txtDetails.Text;

            DataSet dst = new DataSet();
            dst.Tables.Add("Release");
            dst.Tables["Release"].Columns.Add("TimeStamp");
            dst.Tables["Release"].Columns.Add("AppName");
            dst.Tables["Release"].Columns.Add("AppSize");
            dst.Tables["Release"].Columns.Add("UpdatedBy");
            dst.Tables["Release"].Columns.Add("Updatedsource");
            dst.Tables["Release"].Columns.Add("Details");

            dst.Tables["Release"].Rows.Add(timestamp, appName, appSize, updatedBy, updatedSource, details);

            dst.WriteXml("UpdateInfo.xml");
            MessageBox.Show("Release updated info has created successfully.");            
        }

        private void frmCreateAppUpdateDetails_Load(object sender, EventArgs e)
        {
            txtTimestamp.Text = DateTime.Now.ToString();
            txtAppName.Text = "Loodon";
            txtUpdatedBy.Text = "Krayknot Team";
            txtUpdatedSource.Text = "www.krayknot.com";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
