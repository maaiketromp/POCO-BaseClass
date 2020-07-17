using DataObjectBaseLibrary.Data;
// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="CellEnum.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CellEnum : IEnumerator<DatabaseObject>
    {
        private DatabaseObject[] cells;
        private int position;

        public CellEnum(DatabaseObject[] cells)
        {
            this.cells = cells;
            this.position = -1;
        }

        public DatabaseObject Current
        {
            get
            {
                try
                {
                    return this.cells[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current { get => this.Current; }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.position++;
            return (this.position < this.cells.Length);
        }

        public void Reset()
        {
            this.position = -1;
        }
    }
}
