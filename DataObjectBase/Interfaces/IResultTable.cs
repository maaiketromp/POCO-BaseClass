// <copyright file="IResultTable.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Helpers;

    /// <summary>
    /// Defines functionality for resultset classes.
    /// </summary>
    public interface IResultTable : ICollection<ResultRow>
    {
        /// <summary>
        /// Gets or sets the an element in the row enumeration.
        /// </summary>
        /// <param name="index">Index value of the row to be retrieved.</param>
        /// <returns>ResultRow Object.</returns>
        public ResultRow this[int index] { get; set; }

        /// <summary>
        /// Gets the column Name.
        /// </summary>
        /// <param name="i">Index of the column.</param>
        /// <returns>Column name.</returns>
        public string GetColumnName(int i);

        /// <summary>
        /// Gets the Type of the column.
        /// </summary>
        /// <param name="i">Index of the column (zero-based).</param>
        /// <returns>Type of column.</returns>
        public Type GetColumnType(int i);
    }
}
