using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Loodon
{
    public partial class FrmSeparateResults : Form
    {
        //int height = 0;
        //int width = 0;

        public enum SeparateResultsMode
        {
            EDIT,
            DEFAULT
        }

        // Create an instance of a DataAdapter.
        SqlDataAdapter dad;
        DataSet dst = new DataSet("dst");
        string tableName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtbl"></param>
        /// <param name="ParentHeight">Height of the current Monitor</param>
        /// <param name="ParentWidth">Width of the Current monitor</param>
        public FrmSeparateResults(DataTable dtbl, int ParentHeight, int ParentWidth, SeparateResultsMode mode = SeparateResultsMode.DEFAULT, 
            LoodonDAL.ClsDataTypes.Credentials CurrentCredentials = null )
        {
            InitializeComponent();

            if (mode == SeparateResultsMode.EDIT)
            {
                string sqlConnectionString = "Data Source=" + CurrentCredentials.Server + ";User Id=" + CurrentCredentials.Username + ";Password=" +
                    CurrentCredentials.Password + ";Initial Catalog=" + CurrentCredentials.DbName + ";";

                // Create an instance of a DataAdapter.
                dad = new SqlDataAdapter("Select * From " + CurrentCredentials.TableName, sqlConnectionString);

                tableName = CurrentCredentials.TableName;

                // Create an instance of a DataSet, and retrieve data from the Authors table.
                dad.FillSchema(dst, SchemaType.Source, CurrentCredentials.TableName);
                dad.Fill(dst, CurrentCredentials.TableName);
                dgrdSeparateResults.DataSource = dst.Tables[0];

                dgrdSeparateResults.EditMode = DataGridViewEditMode.EditOnEnter;
                
                

            }
            else if (mode == SeparateResultsMode.DEFAULT)
            {
                dgrdSeparateResults.DataSource = dtbl;
                groupBox2.Visible = false;
                btnUpdate.Visible = false;
                this.Height = this.Height - groupBox2.Height;
            }

            this.Size = new Size(ParentWidth - 100, ParentHeight - 100);
            lblTotalRecords.Text = "Total Records: " + (dgrdSeparateResults.Rows.Count - 1).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSeparateResults_Load(object sender, EventArgs e)
        {
            //FillColumns();
        }

        private void btnExporttoExcel_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }

        private void copyAlltoClipboard()
        {
            dgrdSeparateResults.SelectAll();
            DataObject dataObj = dgrdSeparateResults.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(dad);
            dad.Update(dst, tableName);
            MessageBox.Show("Table: " + tableName + " has updated successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void FillColumns()
        //{
        //    for (int i = 0; i <= dst.Tables[0].Columns.Count - 1; i++)
        //    {
        //        cmbColumns.Items.Add(dst.Tables[0].Columns[i].ColumnName);
        //    }
        //}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;
            int recordCount = 0;

            for (int i = 0; i < dgrdSeparateResults.Rows.Count - 1; i++)
            {
                for(int j =0; j<= dgrdSeparateResults.Columns.Count -1; j++)
                {
                    if (dgrdSeparateResults.Rows[i].Cells[j].Value.ToString().ToLower() == searchValue.ToLower())
                    {
                        dgrdSeparateResults.Rows[i].Selected = true;
                        recordCount = recordCount + 1;
                    }                    
                }                
            }
            lblFound.Text = "Search results found: " + recordCount.ToString();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }       
    }
}
