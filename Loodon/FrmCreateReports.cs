using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmCreateReports : Form
    {
        private Rectangle _dragBoxFromMouseDown;
        private int _rowIndexFromMouseDown;
        private int _rowIndexOfItemUnderMouseToDrop;

        private int _selectedRow;
        private int _selectedCol;

        private int _selectedRowTable;
        private int _selectedColTable;

        private readonly DataSet _dstSelectedColumns = new DataSet();

        public FrmCreateReports()
        {
            InitializeComponent();
        }

        private void frmCreateReports_Load(object sender, EventArgs e)
        {
            //Fill the Databases
            //-----------------------------------------------------
            var common = new ClsCommon();
            dgrdTables.DataSource = common.GetTables().Tables[0];
            dgrdTables.Columns[0].Visible = false;
            dgrdTables.Columns[2].Visible = false;
            dgrdTables.Columns[3].Visible = false;

            //For SelectedColumns datagridview
            _dstSelectedColumns.Tables.Add("Cols");
            _dstSelectedColumns.Tables["Cols"].Columns.Add("Columns");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgrdTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var tableName = dgrdTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            var common = new ClsCommon();
            var dst = new DataSet();
            dst = common.GetColumnsofTable(ClsCommon.ServerName, ClsCommon.LoginName,
                ClsCommon.PasswordName, ClsCommon.DatabaseName, tableName);

            dgrdColumns.DataSource = dst.Tables[0];
        }

        private void dgrdColumns_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dgrdColumns, new Point(e.X, e.Y));
            }
        }

        private void dgrdColumns_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left) return;
            // If the mouse moves outside the rectangle, start the drag.
            if (_dragBoxFromMouseDown != Rectangle.Empty &&
                !_dragBoxFromMouseDown.Contains(e.X, e.Y))
            {

                // Proceed with the drag and drop, passing in the list item.                    
                //var dropEffect = dgrdColumns.DoDragDrop(dgrdColumns.Rows[_rowIndexFromMouseDown], DragDropEffects.Move);
            }
        }

        private void dgrdColumns_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            _rowIndexFromMouseDown = dgrdColumns.HitTest(e.X, e.Y).RowIndex;
            if (_rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                var dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                _dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dgrdColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgrdColumns_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            var clientPoint = dgrdSelectedColumns.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            _rowIndexOfItemUnderMouseToDrop =
                dgrdSelectedColumns.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect != DragDropEffects.Move) return;
            var rowToMove = e.Data.GetData(
                typeof(DataGridViewRow)) as DataGridViewRow;
            dgrdColumns.Rows.RemoveAt(_rowIndexFromMouseDown);
            dgrdColumns.Rows.Insert(_rowIndexOfItemUnderMouseToDrop, rowToMove);
        }

        private void dgrdSelectedColumns_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            var clientPoint = dgrdSelectedColumns.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            _rowIndexOfItemUnderMouseToDrop =
                dgrdSelectedColumns.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect != DragDropEffects.Move) return;
            var rowToMove = e.Data.GetData(
                typeof(DataGridViewRow)) as DataGridViewRow;
            dgrdColumns.Rows.RemoveAt(_rowIndexFromMouseDown);
            dgrdColumns.Rows.Insert(_rowIndexOfItemUnderMouseToDrop, rowToMove);
        }

        private void dgrdSelectedColumns_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgrdSelectedColumns_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("receiving");
        }

        private void dgrdColumns_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show("receiving");
        }

        private void selectColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Selects the column from the column list and puts that in the Selected Column table
            var selectedColumn = dgrdColumns.Rows[_selectedRow].Cells[_selectedCol].Value.ToString();

            //Paste in the Selected Column datagridview
            _dstSelectedColumns.Tables["Cols"].Rows.Add(selectedColumn);
            dgrdSelectedColumns.DataSource = _dstSelectedColumns.Tables[0];
        }

        private void dgrdColumns_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void dgrdColumns_Click(object sender, EventArgs e)
        {
            
        }

        private void dgrdColumns_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _selectedRow = e.RowIndex;
            _selectedCol = e.ColumnIndex;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnPreviewandExport_Click(object sender, EventArgs e)
        {
            var table = dgrdTables.Rows[_selectedRowTable].Cells[_selectedColTable].Value.ToString();

            var columns = string.Empty;

            for (var i = 0; i <= dgrdSelectedColumns.Rows.Count - 1; i++)
            {
                columns = columns + dgrdSelectedColumns.Rows[i].Cells[0].Value.ToString() + ", ";
            }

            columns = columns.Substring(0, columns.Length - 2);

            var query = "Select " + columns + " from " + table;

            var expRep = new FrmExportReport(query);
            expRep.ShowDialog();
        }

        private void dgrdTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _selectedRowTable = e.RowIndex;
            _selectedColTable = e.ColumnIndex;

            var tableName = dgrdTables.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            var common = new ClsCommon();
            var dst = new DataSet();
            dst = common.GetColumnsofTable(ClsCommon.ServerName, ClsCommon.LoginName,
                ClsCommon.PasswordName, ClsCommon.DatabaseName, tableName);

            dgrdColumns.DataSource = dst.Tables[0];
        }

        private void dgrdColumns_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectColumnToolStripMenuItem_Click(sender, e);
        }

    }
}
