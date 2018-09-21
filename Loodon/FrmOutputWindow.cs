using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmOutputWindow : Form
    {
        DataSet dst = new DataSet();
        bool tblAlternateColor = true;

        public FrmOutputWindow(DataSet OutputDataset)
        {
            InitializeComponent();
            dst = OutputDataset;
        }

        private void frmOutputWindow_Load(object sender, EventArgs e)
        {
            dgrdDataOutput.DataSource = dst.Tables[0];
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tblAlternateColor == false)
            {
                dgrdDataOutput.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.White;
                dgrdDataOutput.Refresh();

                tblAlternateColor = true;
            }
            else
            {
                dgrdDataOutput.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgrdDataOutput.Refresh();

                tblAlternateColor = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
