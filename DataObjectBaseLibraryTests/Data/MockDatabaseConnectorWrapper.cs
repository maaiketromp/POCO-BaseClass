namespace DataObjectBaseLibraryTests.Data
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;

    class MockDatabaseConnectorWrapper : IDatabaseConnectorWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters)
        {
            ColumnInfo[] colInfo = new ColumnInfo[0];
            ResultTable result = new ResultTable(colInfo);
            ResultRow row = new ResultRow(new object[0], colInfo);
            result.Add(row);
            return result;
        }

        public int PrepareAndExecuteNonQuery(string commandText, CommandType type = CommandType.Text, SqlParameter[] parameters = null)
        {
            return 1;
        }

        public SqlDataReader PrepareAndExecuteQuery(string commandText, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            return null;
        }
    }
}
