// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="DatabaseConnector.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.Data
{
    using System;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <inheritdoc/>
    public class DatabaseConnector : IDatabaseConnector, IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnector"/> class using the connection string.
        /// (For development and testing purposes only)
        /// </summary>
        /// <param name="connString">Database connectionString.</param>
        public DatabaseConnector(string connString)
        {
            this.connectionString = connString;
            this.Initialize();
        }

        /// <inheritdoc/>
        public SqlDataReader PrepareAndExecuteQuery(
            string commandText,
            CommandType commandType = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using SqlCommand cmd = new SqlCommand(commandText, this.connection);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd.ExecuteReader();
        }

        /// <inheritdoc/>
        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using SqlCommand cmd = new SqlCommand(commandText, this.connection);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd.ExecuteNonQuery();
        }

        /// <inheritdoc/>
        public List<Dictionary<string, DatabaseObject>> ExecuteQueryGetResult(
            string commandText,
            CommandType type  = CommandType.Text,
            SqlParameter[] parameters = null)
        {
            using var rdr = this.PrepareAndExecuteQuery(commandText, type, parameters);
            var outputList = new List<Dictionary<string, DatabaseObject>>();

            if (!rdr.HasRows)
            {
                return outputList;
            }

            var colTypes = GetColumnDictionary(rdr);

            while (rdr.Read())
            {
                var row = new Dictionary<string, DatabaseObject>();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string colName = rdr.GetName(i);
                    if ((rdr[i] is DBNull))
                    {
                        if (row.ContainsKey(colName))
                        {
                            // do nothing.
                        }
                        else
                        {
                            // replace DBNull object with a null value.
                            row.Add(colName, new DatabaseObject(colTypes[colName], null));

                        }
                    }
                    else
                    {
                        if (row.ContainsKey(colName))
                        {
                            // override previous entry.
                            row[colName] = new DatabaseObject(colTypes[colName], rdr[i]);
                        }
                        else
                        {
                            row.Add(colName, new DatabaseObject(colTypes[colName], rdr[i]));
                        }
                    }
                }

                outputList.Add(row);
            }

            rdr.Close();
            return outputList;
        }

        private Dictionary<string, Type> GetColumnDictionary(SqlDataReader rdr)
        {
            var columns = from col in rdr.GetColumnSchema()
                          select new { Name = col.ColumnName, Type = col.DataType };

            var outputDict = new Dictionary<string, Type>();
            foreach (var col in columns)
            {
                // check for duplicate column names.
                if (!outputDict.ContainsKey(col.Name))
                {
                    outputDict.Add(col.Name, col.Type);
                }
            }

            return outputDict;
        }

        /// <summary>
        /// Closes closes the SqlConnection.
        /// </summary>
        public void Dispose()
        {
            if (this.connection.State != 0)
            {
                this.connection.Close();
            }
        }

        private void Initialize()
        {
            this.connection = new SqlConnection(this.connectionString);
            this.connection.Open();
        }
    }
}
