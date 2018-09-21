using System;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace Loodon
{
    class ExportToExcel
    {

        private Excel.Application _app;

        private Excel.Workbook _workbook;
        private Excel.Worksheet _previousWorksheet;

        private Excel.Range _workSheetRange;

        private readonly string _folder;

        private static string _connectionStr = "Data Source=(local);Database=DATABASE_NAME;"
                                                         + "Integrated Security=SSPI;";


        public ExportToExcel(string folder)
        {
            _folder = folder;
            _app = null;
            _workbook = null;
            _previousWorksheet = null;
            _workSheetRange = null;
            CreateDoc();
        }

        private void CreateDoc()
        {
            try
            {
                _app = new Excel.Application();

                _app.Visible = false;
                _workbook = _app.Workbooks.Add(1);
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }

        //public void ShutDown()
        //{
        //    try
        //    {
        //        _workbook = null;
        //        _app.Quit();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e.ToString());
        //    }
        //}

        public void ExportTable(string query, string sheetName)
        {
            var CONNECTION_STR = "Data Source=krayknot-pc;User Id=sa;Password=11111;Initial Catalog=saatpheredestination;";
            SqlConnection myConnection = new SqlConnection(CONNECTION_STR);

            SqlDataReader myReader = null;

            try
            {

                var worksheet = (Excel.Worksheet)_workbook.Sheets.Add(Missing.Value, Missing.Value, 1, Excel.XlSheetType.xlWorksheet);
                worksheet.Name = sheetName;
                _previousWorksheet = worksheet;
                myConnection.Open();

                var myCommand = new SqlCommand(query, myConnection);

                myReader = myCommand.ExecuteReader();

                var columnCount = myReader.FieldCount;

                for (var n = 0; n < columnCount; n++)
                {
                    Console.Write(myReader.GetName(n) + "\t");
                    CreateHeaders(worksheet, 1, n + 1, myReader.GetName(n));
                }

                var rowCounter = 2;
                while (myReader.Read())
                {
                    for (int n = 0; n < columnCount; n++)
                    {
                        Console.WriteLine();
                        Console.Write(myReader[myReader.GetName(n)].ToString() + "\t");

                        addData(worksheet, rowCounter, n + 1, myReader[myReader.GetName(n)].ToString());
                    }
                    rowCounter++;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (myReader != null && !myReader.IsClosed)
                {
                    myReader.Close();
                }

                myConnection.Close();
            }
        }

        public static void CreateHeaders(Excel.Worksheet worksheet, int row, int col, string htext)
        {
            worksheet.Cells[row, col] = htext;
        }

        public void addData(Excel.Worksheet worksheet, int row, int col, string data)
        {
            worksheet.Cells[row, col] = data;
        }


        public void SaveWorkbook()
        {

            String folderPath = "C:\\My Files\\" + this._folder;

            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            var fileNameBase = "db";
            var fileName = fileNameBase;
            var ext = ".xlsx";
            var counter = 1;

            while (System.IO.File.Exists(folderPath + fileName + ext))
            {
                fileName = fileNameBase + counter;
                counter++;
            }

            fileName = fileName + ext;
            var filePath = folderPath + fileName;

            try
            {
                _workbook.SaveAs(filePath, Excel.XlFileFormat.xlWorkbookDefault, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}

