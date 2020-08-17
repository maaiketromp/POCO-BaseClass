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
            return new ResultTable(new ColumnInfo[0]) {
                new ResultRow(new object[0], new ColumnInfo[0]),
                };
        }

        public int PrepareAndExecuteNonQuery(
            string commandText, 
            CommandType type = CommandType.Text, 
            SqlParameter[] parameters = null)
        {
            return 1;
        }

        public SqlDataReader PrepareAndExecuteQuery(
            string commandText, 
            CommandType commandType = CommandType.Text, 
            SqlParameter[] parameters = null)
        {
            return null;
        }

        public List<int> PrepareAndExecuteTransaction(
            string[] commandText, 
            CommandType[] commandTypes, 
            SqlParameter[][] parameters, 
            string transactionName)
        {
            return new List<int>();
        }
    }
}
