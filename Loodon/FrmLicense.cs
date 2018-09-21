using System;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmLicense : Form
    {
        public FrmLicense()
        {
            InitializeComponent();
        }

        private void btnDontAgree_Click(object sender, EventArgs e)
        {
            Environment.Exit(-1);
        }

        private void btnNotnow_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (new RegisterApplication.frmUserInformation().ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Close();
            }
        }
    }
}
