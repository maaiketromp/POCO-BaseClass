// <copyright file="DatabaseConnector.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DataObjectBaseLibrary.Interfaces;
    using Microsoft.Data.SqlClient;

    /// <inheritdoc/>
    public class DatabaseConnector : IDatabaseConnector, IDisposable
    {
        private readonly string connectionString;
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnector"/> class using the connection string.
        /// (For development and testing purposes only).
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
        public List<int> PrepareAndExecuteTransaction(
            string[] commandText,
            CommandType[] commandTypes,
            SqlParameter[][] parameters,
            string transactionName)
        {
            if (commandText.Length != commandText.Length ||
                commandText.Length != parameters.First().Length)
            {
                throw new InvalidOperationException("Input arrays must have the same lenghts");
            }

            List<int> returnValues = new List<int>();
            SqlCommand command = this.connection.CreateCommand();
            SqlTransaction transaction;

            transaction = this.connection.BeginTransaction(transactionName);
            command.Connection = this.connection;
            command.Transaction = transaction;

            try
            {
                for (int i = 0; i < commandText.Length; i++)
                {
                    command.CommandType = commandTypes[i];
                    command.CommandText = commandText[i];
                    if (parameters[i] != null)
                    {
                        command.Parameters.AddRange(parameters[i]);
                    }

                    returnValues.Add(command.ExecuteNonQuery());
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                    return new List<int>() { 0 };
                }
                catch (Exception)
                {
                    // log exception.
                    return new List<int>() { -1 };
                }
            }

            return returnValues;
        }

        /// <inheritdoc/>
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
