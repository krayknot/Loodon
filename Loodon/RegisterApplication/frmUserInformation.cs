using System;
using System.Windows.Forms;
using LoodonDAL;
using LoodonDAL.com.krayknot.wcfservices;

namespace Loodon.RegisterApplication
{
    public partial class frmUserInformation : Form
    {
        public frmUserInformation()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var userName = txtName.Text.Trim();
            var email = txtEmailAddress.Text.Trim();

            var details = new UserDetails();
            details.Users_Email = email;
            details.Users_FullName = userName;

            ClsInsert.InsertUser(details);

            MessageBox.Show("Details has been saved successfully.", "User Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
