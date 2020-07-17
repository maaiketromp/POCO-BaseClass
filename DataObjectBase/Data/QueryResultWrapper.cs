namespace DataObjectBaseLibrary.Data
{
    using System;
    using System.Linq;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using System.Data;

    public class QueryResultWrapper : IQueryResultWrapper
    {
        private readonly IDatabaseConnector db;

        public QueryResultWrapper(IDatabaseConnector db)
        {
            this.db = db;
        }

        public IResultTable GetResult(string commandText, SqlParameter[] parameters = null)
        {
            using var rdr = this.db.PrepareAndExecuteQuery(commandText, parameters: parameters);

            var columns = from col in rdr.GetColumnSchema()
                          select new ColumnInfo(col.ColumnName, col.DataType);
            ResultTable result = new ResultTable(columns.ToArray());

            while (rdr.Read())
            {
                DatabaseObject[] cells = new DatabaseObject[rdr.FieldCount];
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    DatabaseObject cell;
                    Type t = columns
                        .Where(c => c.Name == rdr.GetName(i))
                        .Select(c => c.Type)
                        .FirstOrDefault();
                    cell = new DatabaseObject(t, rdr[i]);
                }

                var row = new ResultRow(cells);
                result.Add(row);
            }

            return result;
        }

        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            return this.db.PrepareAndExecuteNonQuery(commandText, type, parameters);
        }
    }
}
