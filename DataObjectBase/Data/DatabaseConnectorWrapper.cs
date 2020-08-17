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

            ColumnInfo[] columns = (from col in rdr.GetColumnSchema()
                          select new ColumnInfo(col.ColumnName, col.DataType)).ToArray();
            ResultTable result = new ResultTable(columns);

            while (rdr.Read())
            {
                object[] cells = new object[rdr.FieldCount];
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    object cell = rdr[i];
                }

                var row = new ResultRow(cells, columns);
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

        public List<int> PrepareAndExecuteTransaction(
            string[] commandText,
            CommandType[] commandTypes,
            SqlParameter[][] parameters,
            string transactionName)
        {
            return this.db.PrepareAndExecuteTransaction(
                commandText,
                commandTypes,
                parameters,
                transactionName);
        }
    }
}
