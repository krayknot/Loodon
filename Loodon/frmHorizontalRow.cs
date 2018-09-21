using System;
using System.Data;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmHorizontalRow : Form
    {
        private DataSet _dstRow = new DataSet();

        public FrmHorizontalRow(DataSet rowDataset)
        {
            InitializeComponent();
            _dstRow = rowDataset;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmHorizontalRow_Load(object sender, EventArgs e)
        {
            dgrdRow.DataSource = _dstRow.Tables[0];
            dgrdRow.Columns[0].ReadOnly = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMakeUpdatable_Click(object sender, EventArgs e)
        {
            //dgrdRow.EditMode = DataGridViewEditMode.EditOnEnter;
        }
  
    }
}
