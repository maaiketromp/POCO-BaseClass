// <copyright file="DatabaseConnectorWrapper.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Wraps the database connection class to enable result parsing.
    /// </summary>
    public class DatabaseConnectorWrapper : IDatabaseConnectorWrapper
    {
        private readonly IDatabaseConnector db;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectorWrapper"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        public DatabaseConnectorWrapper(IDatabaseConnector db)
        {
            this.db = db;
        }

        /// <inheritdoc/>
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

                var row = new ResultRow(cells, columns.ToArray());
                result.Add(row);
            }

            return result;
        }

        /// <inheritdoc/>
        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            return this.db.PrepareAndExecuteNonQuery(commandText, type, parameters);
        }

        /// <inheritdoc/>
        public SqlDataReader PrepareAndExecuteQuery(
            string commandText,
            CommandType commandType = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            return this.db.PrepareAndExecuteQuery(commandText, commandType, parameters);
        }

        /// <inheritdoc/>
        public List<Dictionary<string, DatabaseObject>> GetResultAsDictionary(string commandText, SqlParameter[] parameters = null)
        {
            var rowData = new Dictionary<string, DatabaseObject>();
            var output = new List<Dictionary<string, DatabaseObject>>();

            var result = this.GetResult(commandText, parameters);

            for (int i = 0; i < result.Count(); i++)
            {
                for (int j = 0; j < result[i].Count(); j++)
                {
                    rowData.Add(result.GetColumnName(j), result[i].ElementAt(j));
                }

                output.Add(rowData);
            }

            return output;
        }
    }
}
