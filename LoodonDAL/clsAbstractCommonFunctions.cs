using System.Data;
using System.Data.SqlClient;

namespace LoodonDAL
{
    public abstract class ClsAbstractCommonFunctions : ICommonFunctions
    {      
        #region ICommonFunctions Members

        public abstract DataSet GetDatasetFromSqlQuery(string sqlQuery, SqlConnection connection);

        public abstract string ConvertDatasettoString(DataSet DatasetName);

        #endregion
    }
}
