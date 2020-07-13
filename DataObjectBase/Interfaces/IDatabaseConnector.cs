// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="IDatabaseConnector.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.Interfaces
{
    using System.Data;
    using Microsoft.Data.SqlClient;
    
    /// <summary>
    /// Interface for the DatabaseConnector.
    /// </summary>
    public interface IDatabaseConnector
    {
        /// <summary>
        /// Executes a SQL query, defined in the application.
        /// </summary>
        /// <param name="commandText">Sql String.</param>
        /// <param name="commandType">CommandType.</param>
        /// <param name="parameters">An array of SqlParameters.</param>
        /// <returns>A SqlDataReader.</returns>
        public SqlDataReader PrepareAndExecuteQuery(
            string commandText,
            CommandType commandType = CommandType.Text,
            SqlParameter[] parameters = null);

        /// <summary>
        /// Executes a userdefined SQL query.
        /// </summary>
        /// <param name="commandText">Sql string.</param>
        /// <param name="type">CommandType.</param>
        /// <param name="parameters">Array of SqlParameters.</param>
        /// <returns>Number of rows affected.</returns>
        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null);
    }
}
