using System;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmExportReport : Form
    {
        private string _query = string.Empty;

        public FrmExportReport(string query)
        {
            InitializeComponent();
            _query = query;
        }

        private void frmExportReport_Load(object sender, EventArgs e)
        {
            var common = new ClsCommon();
            var dst = common.GetDatasetFromSQLQuery(_query);
            dgrdReport.DataSource = dst.Tables[0];

            lblCount.Text = dgrdReport.Rows.Count.ToString() + " records found.";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            // creating Excel Application
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();

            // creating new WorkBook within Excel application
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

            // creating new Excelsheet in workbook
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            // see the excel sheet behind the program
            app.Visible = true;

            // get the reference of first sheet. By default its name is Sheet1.
            // store its reference to worksheet
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;

            // changing the name of active sheet
            worksheet.Name = "Exported from gridview";

            // storing header part in Excel
            for (var i = 1; i < dgrdReport.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgrdReport.Columns[i - 1].HeaderText;
            }

            // storing Each row and column value to excel sheet
            for (var i = 0; i < dgrdReport.Rows.Count - 1; i++)
            {
                for (var j = 0; j < dgrdReport.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgrdReport.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application
            workbook.SaveAs("c:\\Report.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            // Exit from the application
            app.Quit();

            MessageBox.Show("Report Excel Export successfull.\nExport Path: C:\\Report.xls", "Report Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCSVExport_Click(object sender, EventArgs e)
        {
            var streamWriter = new System.IO.StreamWriter("C:\\Report.csv");

            var strHeader = "";
            for (var i = 0; i < dgrdReport.Columns.Count; i++)
            {
                strHeader += dgrdReport.Columns[i].HeaderText + ",";
            }
            streamWriter.WriteLine(strHeader);


            for (var m = 0; m < dgrdReport.Rows.Count; m++)
            {
                var strRowValue = "";
                for (var n = 0; n < dgrdReport.Columns.Count; n++)
                {
                    strRowValue += dgrdReport.Rows[m].Cells[n].Value + ",";
                }
                streamWriter.WriteLine(strRowValue);
            }

            streamWriter.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
