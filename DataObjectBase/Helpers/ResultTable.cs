

namespace DataObjectBaseLibrary.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;

    public class ResultTable : IResultTable
    {
        private ResultRow[] rows;

        public ResultTable(ColumnInfo[] columnInfo)
        {
            this.rows = new ResultRow[0];
            this.ColumnInfo = columnInfo;
        }

        public ColumnInfo[] ColumnInfo { get; }

        public int Count => rows.Length;

        public bool IsReadOnly => false;

        public ResultRow this[int index]
        {
            get 
            {
                return (ResultRow)this.rows[index];
            }
            set
            {
                if (index < this.rows.Length)
                {
                    this.rows[index] = value;
                }
                else if (index == this.rows.Length)
                {
                    this.Add(value);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public string GetColumnName(int i)
        {
            if (i >= this.ColumnInfo.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return this.ColumnInfo[i].Name;
        }

        public Type GetColumnType(int i)
        {
            if (i >= this.ColumnInfo.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return this.ColumnInfo[i].Type;
        }

        public IEnumerator<ResultRow> GetEnumerator()
        {
            return new RowEnum(this.rows);
        }
        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        public void Add(ResultRow item)
        {
            var temp = new ResultRow[this.rows.Length + 1];
            int i = 0;
            for (; i < this.rows.Length; i++)
            {
                temp[i] = this.rows[i];
            }
            temp[i] = item;
            this.rows = temp;
        }

        public void Clear()
        {
            this.rows = null;
        }

        public bool Contains(ResultRow item)
        {
            if (this.rows.Contains(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CopyTo(ResultRow[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("The array cannot be null.");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            if (Count > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            for (int i = 0; i < this.rows.Length; i++)
            {
                array[i + arrayIndex] = this.rows[i];
            }
        }

        public bool Remove(ResultRow item)
        {
            bool result = false;

            if (this.rows.Contains(item))
            {
                int j = 0;
                var temp = new ResultRow[this.rows.Length - 1];
                for (int i = 0; i < this.rows.Length; i++)
                {
                    
                    if (this.rows[i].Equals(item))
                    {
                        continue;
                    }

                    temp[j++] = rows[i];
                }
                this.rows = temp;
            }

            return result;

        }
    }
}
