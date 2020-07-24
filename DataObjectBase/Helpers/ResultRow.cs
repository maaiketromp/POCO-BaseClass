// <copyright file="ResultRow.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DataObjectBaseLibrary.Data;

    /// <summary>
    /// Represents a row in a result from a database-query.
    /// </summary>
    public class ResultRow : IEnumerable<DatabaseObject>
    {
        private readonly DatabaseObject[] cells;
        private readonly ColumnInfo[] columnInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultRow"/> class.
        /// </summary>
        /// <param name="cells">row of database objects.</param>
        /// <param name="columnInfos">array of column info structs.</param>
        public ResultRow(DatabaseObject[] cells, ColumnInfo[] columnInfos)
        {
            // deep copy input array.
            this.cells = new DatabaseObject[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                this.cells[i] = cells[i];
            }

            this.columnInfos = columnInfos;
        }

        /// <summary>
        /// Retrieves databaseObject from row by specifying column name.
        /// </summary>
        /// <param name="colName">Column's name.</param>
        /// <returns>DatabaseObject.</returns>
        public DatabaseObject this[string colName]
        {
            get
            {
                ColumnInfo col = (from colInfo in this.columnInfos
                            where colInfo.Name == colName
                            select colInfo).First();

                if (col.Equals(default(ColumnInfo)))
                {
                    throw new InvalidOperationException($"Could not find a column with column name {colName}");
                }

                int index = Array.IndexOf(this.columnInfos, col);
                return (DatabaseObject)this.cells[index];
            }
        }

        /// <summary>
        /// Gets the databaseObject at zerobased index.
        /// </summary>
        /// <param name="index">index of column.</param>
        /// <returns>Database Object.</returns>
        public DatabaseObject this[int index]
        {
            get
            {
                return (DatabaseObject)this.cells[index];
            }
        }

        /// <summary>
        /// Gets a column's name.
        /// </summary>
        /// <param name="i">zero-based index of column.</param>
        /// <returns>String column name.</returns>
        public string GetColumnName(int i)
        {
            if (i < 0 || i >= this.columnInfos.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return this.columnInfos[i].Name;
        }

        /// <summary>
        /// Gets the datatype of a column.
        /// </summary>
        /// <param name="i">zero-based column index.</param>
        /// <returns>A Type instance.</returns>
        public Type GetColumnType(int i)
        {
            if (i < 0 || i >= this.columnInfos.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return this.columnInfos[i].Type;
        }

        /// <inheritdoc/>
        public IEnumerator<DatabaseObject> GetEnumerator()
        {
            return new CellEnum(this.cells);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator1();
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

    }
}
