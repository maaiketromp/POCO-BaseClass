// <copyright file="IResultRow.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for representation of a row result.
    /// </summary>
    public interface IResultRow : IEnumerable<object>
    {
        /// <summary>
        /// Retrieves databaseObject from row by specifying column name.
        /// </summary>
        /// <param name="colName">Column's name.</param>
        /// <returns>DatabaseObject.</returns>
        public object this[string colName] { get; }

        /// <summary>
        /// Gets the databaseObject at zerobased index.
        /// </summary>
        /// <param name="index">index of column.</param>
        /// <returns>Database Object.</returns>
        public object this[int index] { get; }

        /// <summary>
        /// Gets a column's name.
        /// </summary>
        /// <param name="i">zero-based index of column.</param>
        /// <returns>String column name.</returns>
        public string GetColumnName(int i);

        /// <summary>
        /// Gets the datatype of a column.
        /// </summary>
        /// <param name="i">zero-based column index.</param>
        /// <returns>A Type instance.</returns>
        public Type GetColumnType(int i);
    }
}
