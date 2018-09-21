using System;
using System.Data;
using System.Windows.Forms;

namespace Loodon
{
    public partial class FrmColumnProperties : Form
    {
        private string _tableName = string.Empty;
        private string _serverName = string.Empty;
        private string _columnName = string.Empty;
        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        private string _database = string.Empty;


        public FrmColumnProperties(string tableName, string server, string columnName, string username, string password, string database)
        {
            InitializeComponent();

            _tableName = tableName;
            _serverName = server;
            _columnName = columnName;
            _userName = username;
            _passWord = password;
            _database = database;
        }

        private void frmColumnProperties_Load(object sender, EventArgs e)
        {
            var dstColProperties = new LoodonDAL.ClsCommon().GetColumnProperties(_serverName, _userName, _passWord, _columnName, _tableName, _database);

            var dst = new DataSet();
            dst.Tables.Add("ROW");
            dst.Tables["ROW"].Columns.Add("Column");
            dst.Tables["ROW"].Columns.Add("Value");

            for (var i = 0; i <= dstColProperties.Tables[0].Columns.Count - 1; i++)
            {
                dst.Tables["ROW"].Rows.Add(dstColProperties.Tables[0].Columns[i].ColumnName, dstColProperties.Tables[0].Rows[0][i].ToString());
            }

            dgrdColumnProperties.DataSource = dst.Tables[0];

            groupBox1.Text = "Column: " + _columnName + " properties"; ;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
