namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class MockTestClassData : IResultRow
    {
        private readonly int id = 1;
        private readonly bool boolVal = true;
        private readonly bool? nBoolVal = true;
        private readonly int intval = 1;
        private readonly int? nIntval = 1;
        private readonly float floatVal = 1;
        private readonly float? nFloatVal = 1;
        private readonly double doubleVal = 1;
        private readonly double? nDoubleVal = 1;
        private readonly decimal decVal = 1;
        private readonly decimal? nDecVal = 1;
        private readonly char charVal = '1';
        private readonly char nCharVal = '1';
        private readonly string stringVal = "a";
        private readonly DateTime dateTimeVal = DateTime.Now;
        private readonly DateTime? nDateTimeVal = DateTime.Now;


        private readonly object[] cells;
        private readonly ColumnInfo[] columnInfos;

        public MockTestClassData()
        {
            this.cells = new object[]
            {   this.id,
                this.boolVal,
                this.nBoolVal,
                this.intval,
                this.nIntval,
                this.floatVal,
                this.nFloatVal,
                this.doubleVal,
                this.nDoubleVal,
                this.decVal,
                this.nDecVal,
                this.charVal,
                this.nCharVal,
                this.stringVal,
                this.dateTimeVal,
                this.nDateTimeVal
            };
            this.columnInfos = new ColumnInfo[]
            {
                new ColumnInfo("Id", typeof(int)),
                new ColumnInfo("BoolVal", typeof(bool)),
                new ColumnInfo("NBoolVal", typeof(bool)),
                new ColumnInfo("IntVal", typeof(int)),
                new ColumnInfo("NIntVal", typeof(int)),
                new ColumnInfo("FloatVal", typeof(float)),
                new ColumnInfo("NFloatVal", typeof(float)),
                new ColumnInfo("DoubleVal", typeof(float)),
                new ColumnInfo("NDoubleVal", typeof(float)),
                new ColumnInfo("DecVal", typeof(decimal)),
                new ColumnInfo("NDecVal", typeof(decimal)),
                new ColumnInfo("CharVal", typeof(string)),
                new ColumnInfo("NCharVal", typeof(string)),
                new ColumnInfo("StringVal", typeof(string)),
                new ColumnInfo("DateTimeVal", typeof(DateTime)),
                new ColumnInfo("NDateTimeVal", typeof(DateTime)),
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
