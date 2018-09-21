using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Loodon
{
    public partial class FrmSQLtoAccess : Form
    {
        public FrmSQLtoAccess()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSQLtoAccess_Load(object sender, EventArgs e)
        {
            MessageBox.Show(LoodonDAL.ClsConfigMessages.AccessConversionLoadMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


            ListSQLTables();
        }

        private void ListSQLTables()
        {
            DataSet dst = new DataSet();
            dst = new ClsCommon().GetTables();

            lstSQL.DataSource = dst.Tables[0];
            lstSQL.DisplayMember = "SchemaTable";           

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string accessdBPath = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Access Database | *.mdb";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                accessdBPath = ofd.FileName;

                GenerateAccessDatabase(accessdBPath);

                PrintResponse("Process Finished. Please check the information in the list above.");
            }
            else
            {
                MessageBox.Show("Please select the Blank Access Daabase file.\nWithout it no operations are possible!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool GenerateAccessDatabase(string myPath)
        {   
            //CatalogClass cat = new CatalogClass();
            string strSQL;
            string cs;
            bool response = false;
            string tablename = string.Empty;
            string columns = string.Empty;
            string server = ClsCommon.ServerName;
            string login = ClsCommon.LoginName;
            string password = ClsCommon.PasswordName;
            string dB = ClsCommon.DatabaseName;

            PrintResponse("Access DB:" + myPath);

            //for (int i = 0; i <= lstSQL.Items.Count - 1; i++)
            //{
            foreach (DataRowView items in lstSQL.Items)
            {
                try
                {
                    cs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + myPath + ";Jet OLEDB:Engine Type=5";
                    //lstSQL.SelectedItem = i;

                    //tablename = lstSQL.SelectedItem.ToString(); //Get the tablename from sql

                    tablename = items[0].ToString();
                    tablename = tablename.Substring(tablename.IndexOf('.') + 1);

                    //get the columns in a single string variable for the sql table
                    DataSet dstCols = new DataSet();
                    dstCols = new ClsCommon().GetColumnsofTable(server, login, password, dB, tablename);

                    for (int c = 0; c <= dstCols.Tables[0].Rows.Count - 1; c++)
                    {
                        columns = columns + dstCols.Tables[0].Rows[c]["column_name"].ToString() + " TEXT(100),";
                    }

                    columns = columns.Substring(0, columns.Length - 1); //Removes the trail comman
                    //strSQL = "CREATE TABLE Issues (mID AUTOINCREMENT, mUser TEXT(100) NOT NULL " +
                    //         ", mError TEXT(100) NOT NULL, " +
                    //         "mDescription TEXT(100) NOT NULL, mDate DATETIME NOT NULL)";

                    strSQL = "CREATE TABLE " + tablename + "(" + columns + ")";
                   

                    //cat.Create(cs);

                    using (OleDbConnection cnn = new OleDbConnection(cs))
                    {
                        OleDbCommand cmd = new OleDbCommand();
                        try
                        {
                            cmd.CommandText = strSQL;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = cnn;
                            cnn.Open();
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                            response= true;
                        }
                        catch (Exception ex)
                        {
                            
                            response = false;
                            throw ex;
                        }
                        finally
                        {
                            cnn.Close();
                            cmd.Dispose();
                        }
                        lstAccess.Items.Add(tablename);
                        lstAccess.SelectedItem = lstAccess.Items.Count - 1;
                        lstAccess.Refresh();
                    }
                }
                catch (Exception ex)
                {
                    //throw ex;
                    //lstProgress.Items.Add(ex.Message);
                    PrintResponse(ex.Message + " Table: " + tablename);
                }
                columns = string.Empty; //Initialize
            }

            return response;

            //finally
            //{   
            //    //Marshal.FinalReleaseComObject(cat);
            //}
        }

        private void PrintResponse(string Message)
        {
            lstProgress.Items.Add(Message);
            lstProgress.SelectedItem = lstProgress.Items.Count - 1;
            lstProgress.Refresh();
        }
    }


    

}
