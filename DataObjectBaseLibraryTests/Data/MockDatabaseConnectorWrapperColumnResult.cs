namespace DataObjectBaseLibraryTests.Data
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;

    class MockDatabaseConnectorWrapperColumnResult : IDatabaseConnectorWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters)
        {
            string[] columnNames = new string[]
            {
                "Id", "CustomerId", "EmployeeId", "ShippingDate", "TimeOfOrder",
            };
            string[] defaultVals = new string[]
            {
                null, null, null, null, "(getdate())",
            };

            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };
            ResultTable result = new ResultTable(colInfo);

            for (int i = 0; i < columnNames.Length; i++)
            {
                object[] cells = new object[]
                {
                    columnNames[i],
                    defaultVals[i],
                };

                result.Add(new ResultRow(cells, colInfo));
            }

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

        public List<int> PrepareAndExecuteTransaction(string[] commandText, CommandType[] commandTypes, SqlParameter[][] parameters, string transactionName)
        {
            return new List<int>();
        }
    }
}
