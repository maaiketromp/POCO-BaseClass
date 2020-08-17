namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using System;

    public static class MockData
    {
        public static IResultTable GetQueryResultForDerivedClassColumns()
        {
            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };

            IResultTable result = new ResultTable(colInfo);
            result.Add(new ResultRow(new object[] { "Id", null }, colInfo));
            result.Add(new ResultRow(new object[] { "AnotherStringVal", null }, colInfo));

            return result;
        }

        internal static IResultTable GetQueryResultForTestClassColumns()
        {
            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };

            IResultTable result = new ResultTable(colInfo);
            result.Add(new ResultRow(new object[] { "Id", null }, colInfo));
            result.Add(new ResultRow(new object[] { "BoolVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NBoolVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "IntVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NIntVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "FloatVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NFloatVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "DoubleVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NDoubleVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "DecVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NDecVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "CharVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NCharVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "StringVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "DateTimeVal", null }, colInfo));
            result.Add(new ResultRow(new object[] { "NDateTimeVal", null }, colInfo));
            return result;
        }
    }
}
