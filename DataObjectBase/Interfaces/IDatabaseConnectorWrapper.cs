// <copyright file="IDatabaseConnectorWrapper.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using DataObjectBaseLibrary.Data;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Defines an interface for a database wrapper.
    /// </summary>
    public interface IDatabaseConnectorWrapper
    {
        /// <summary>
        /// Gets an Ienumerable as result.
        /// </summary>
        /// <param name="commandText">Sql commandtext.</param>
        /// <param name="parameters">SqlParameter array.</param>
        /// <returns>A IResultTable object, which can be cast to IEnumerable.</returns>
        public IResultTable GetResult(string commandText, SqlParameter[] parameters = null);

        /// <summary>
        /// Prepares and executes a non query.
        /// </summary>
        /// <param name="commandText">Sql Commandtext.</param>
        /// <param name="type">Command type.</param>
        /// <param name="parameters">SqlParameter array.</param>
        /// <returns>Number of rows affected.</returns>
        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null);

        /// <summary>
        /// Prepares and executes a query and returns a datareader.
        /// </summary>
        /// <param name="commandText">Sql command text.</param>
        /// <param name="commandType">Command type.</param>
        /// <param name="parameters">Sql Parameter array.</param>
        /// <returns>A sql datareader object.</returns>
        public SqlDataReader PrepareAndExecuteQuery(
            string commandText,
            CommandType commandType = CommandType.Text,
            SqlParameter[] parameters = null);
    }
}
