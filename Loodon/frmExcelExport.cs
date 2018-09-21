using System;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmExcelExport : Form
    {
        public FrmExcelExport()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Hide();
            var backup = new SQLBackup.frmTakingBackup();
            backup.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
