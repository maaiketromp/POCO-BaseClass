namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class MockOrderDetailsDataNulls : IResultRow
    {
        private int? orderId = null;
        private int? productId = null;
        private int? quantity = null;


        private readonly object[] cells;
        private readonly ColumnInfo[] columnInfos;

        public MockOrderDetailsDataNulls()
        {
            this.cells = new object[]
            {
                this.orderId,
                this.productId,
                this.quantity,
            };
            this.columnInfos = new ColumnInfo[]
            {
            new ColumnInfo("OrderId", typeof(int)),
            new ColumnInfo("ProductId", typeof(int)),
            new ColumnInfo("Quantity", typeof(int)),
            };
        }

        public object this[string colName]
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
                return this.cells[index];
            }
        }

        public object this[int index]
        {
            get
            {
                return this.cells[index];
            }
        }

        public string GetColumnName(int i)
        {
            if (i < 0 || i >= this.columnInfos.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return this.columnInfos[i].Name;
        }

        public Type GetColumnType(int i)
        {
            if (i < 0 || i >= this.columnInfos.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return this.columnInfos[i].Type;
        }

        /// <inheritdoc/>
        public IEnumerator<object> GetEnumerator()
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
