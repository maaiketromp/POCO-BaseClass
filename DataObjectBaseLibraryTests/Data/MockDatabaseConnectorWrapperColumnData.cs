using DataObjectBaseLibrary.Data;
using DataObjectBaseLibrary.Helpers;
using DataObjectBaseLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace DataObjectBaseLibraryTests.Data
{
    class MockDatabaseConnectorWrapperColumnData
    : IDatabaseConnectorWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters)
        {
            switch (commandText)
            {
                case "SELECT COLUMN_NAME, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DerivedFromTest'":
                    return MockData.GetQueryResultForDerivedClassColumns();
                case "SELECT COLUMN_NAME, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestClass'":
                    return MockData.GetQueryResultForTestClassColumns();
                case "SELECT COLUMN_NAME, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Order'":
                    return MockData.GetQueryResultForOrderColumns();
                default:
                    return new ResultTable(new ColumnInfo[0])
                    {
                        new ResultRow(new object[0], new ColumnInfo[0]),
                    };
            }
        }

        public int PrepareAndExecuteNonQuery(string commandText, CommandType type = CommandType.Text, SqlParameter[] parameters = null)
        {
            return 1;
        }

        public SqlDataReader PrepareAndExecuteQuery(string commandText, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            return null;
        }

        public List<int> PrepareAndExecuteTransaction(string[] commandText, CommandType[] commandTypes, SqlParameter[][] parameters, string transactionName)
        {
            return new List<int>();
        }
    }
}
