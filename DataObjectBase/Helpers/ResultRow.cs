// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="ResultRow.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.Helpers
{
    using DataObjectBaseLibrary.Data;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ResultRow : IEnumerable<DatabaseObject>
    {
        private DatabaseObject[] cells;

        public ResultRow(DatabaseObject[] cells)
        {
            // deep copy input array.
            this.cells = new DatabaseObject[cells.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                this.cells[i] = cells[i];
            }
        }

        public DatabaseObject this[int index]
        {
            get
            {
                return (DatabaseObject)this.cells[index];
            }
        }

        public IEnumerator<DatabaseObject> GetEnumerator()
        {
            return new CellEnum(this.cells);
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator1();
        }
    }
}
