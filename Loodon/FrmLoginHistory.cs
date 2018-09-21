using System;
using System.Data;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmLoginHistory : Form
    {
        public string ServerName { get; set; }
        public string UserName { get; set; }

        private int _columnIndex = 0;
        private int _rowIndex = 0;
        readonly string xmlPath = "XML\\LoginLogs.xml";

        private readonly bool _selectButtonVisible;

        public FrmLoginHistory(bool showSelectButton)
        {
            InitializeComponent();
            _selectButtonVisible = showSelectButton;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //ServerName = dgrdLoginLogs.Rows[rowIndex].Cells[0].ToString();
            //UserName = dgrdLoginLogs.Rows[rowIndex].Cells[1].ToString();
            Close();
        }

        private void frmLoginHistory_Load(object sender, EventArgs e)
        {
            //string xmlPath = "XML\\LoginLogs.xml";

            LoadLoginLog();

            //If frmloginhistory is calling from the frmmain to show the previous connections list then
            //there is no need to provide the Select and Clear button to connect or delete any database.
            if (_selectButtonVisible) return;
            btnSelect.Visible = false;
            btnClear.Visible = false;
        }

        private void LoadLoginLog()
        {
            var dst = new DataSet();

            if (System.IO.File.Exists(xmlPath))
            {
                dst.ReadXml(xmlPath);

                dgrdLoginLogs.DataSource = dst.Tables[0];
            }
            else
            {
                dgrdLoginLogs.Rows.Clear();
            }
        }

        private void dgrdLoginLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _columnIndex = e.ColumnIndex;
            _rowIndex = e.RowIndex;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ServerName = dgrdLoginLogs.Rows[_rowIndex].Cells[0].Value.ToString();
            UserName = dgrdLoginLogs.Rows[_rowIndex].Cells[1].Value.ToString();
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(xmlPath);
                LoadLoginLog();
                MessageBox.Show("Login logs has deleted successfully.", "Loodon: Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
