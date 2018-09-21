using System;
using System.Data;

namespace Loodon
{
    interface ICommonFunctions
    {
        bool IsUserAuthenticated(string server, string login, string password);
        void LogLoginAttempt(string server, string login, string password, string logingStatus);
        
        string ValidateInput(string inputText, string controlLabelText, ValidationType validateType);
        DataSet GetDatabases();

        DataSet GetTables();
        DataSet GetColumns(int objectID);
        DataSet GetViews();
        DataSet GetStoredProcedures();
        DataSet GetTableValuedFunctions();
        DataSet GetScalarValuedFunctions();
        DataSet GetTableData(string tableName);
        Boolean DoesTableExists(string tableName, string connectionString);
        void CreateSQLDatabase(string serverName, string userName, string passWord, string dataBase);
    }
}
