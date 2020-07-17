using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseLibrary.Helpers
{
    class RowEnum : IEnumerator<ResultRow>
    {
        private readonly ResultRow[] rows;
        private int position;

        public RowEnum(ResultRow[] rows)
        {
            this.rows = rows;
            this.position = -1;
        }

        public ResultRow Current
        {
            get
            {
                try
                {
                    return this.rows[position];
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            this.position++;
            return (this.position < this.rows.Length);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
