namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;
    using System;


    /// <summary>
    /// Containing mockdata for all testcases.
    /// </summary>
    internal static class MockData
    {
        internal static IResultTable GetQueryResultForDerivedClassColumns()
        {
            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };

            IResultTable result = new ResultTable(colInfo)
            {
                new ResultRow(new object[] { "Id", null }, colInfo),
                new ResultRow(new object[] { "AnotherStringVal", null }, colInfo)
            };

            return result;
        }

        internal static IResultTable GetQueryResultForTestClassColumns()
        {
            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };

            IResultTable result = new ResultTable(colInfo)
            {
                new ResultRow(new object[] { "Id", null }, colInfo),
                new ResultRow(new object[] { "BoolVal", null }, colInfo),
                new ResultRow(new object[] { "NBoolVal", null }, colInfo),
                new ResultRow(new object[] { "IntVal", null }, colInfo),
                new ResultRow(new object[] { "NIntVal", null }, colInfo),
                new ResultRow(new object[] { "FloatVal", null }, colInfo),
                new ResultRow(new object[] { "NFloatVal", null }, colInfo),
                new ResultRow(new object[] { "DoubleVal", null }, colInfo),
                new ResultRow(new object[] { "NDoubleVal", null }, colInfo),
                new ResultRow(new object[] { "DecVal", null }, colInfo),
                new ResultRow(new object[] { "NDecVal", null }, colInfo),
                new ResultRow(new object[] { "CharVal", null }, colInfo),
                new ResultRow(new object[] { "NCharVal", null }, colInfo),
                new ResultRow(new object[] { "StringVal", null }, colInfo),
                new ResultRow(new object[] { "DateTimeVal", null }, colInfo),
                new ResultRow(new object[] { "NDateTimeVal", null }, colInfo)
            };

            return result;
        }

        internal static IResultTable GetQueryResultForOrderColumns()
        {
            string[] columnNames = new string[]
            {
                "Id", "CustomerId", "EmployeeId", "ShippingDate", "TimeOfOrder",
            };
            string[] defaultVals = new string[]
            {
                null, null, null, null, "(getdate())",
            };

            ColumnInfo[] colInfo = new ColumnInfo[]
            {
                new ColumnInfo("COLUMN_NAME", typeof(string)),
                new ColumnInfo("COLUMN_DEFAULT", typeof(string)),
            };
            ResultTable result = new ResultTable(colInfo);

            for (int i = 0; i < columnNames.Length; i++)
            {
                object[] cells = new object[]
                {
                    columnNames[i],
                    defaultVals[i],
                };

                result.Add(new ResultRow(cells, colInfo));
            }

            return result;
        }

        internal static IResultRow GetDerivedFromTestClassMockData()
        {
            int id = 1;
            bool boolVal = true;
            bool? nBoolVal = true;
            int intVal = 1;
            int? nIntVal = 1;
            float floatVal = 1;
            float? nFloatVal = 1;
            double doubleVal = 1;
            double? nDoubleVal = 1;
            decimal decVal = 1;
            decimal? nDecVal = 1;
            char charVal = '1';
            char nCharVal = '1';
            string stringVal = "a";
            string anotherStringVal = "b";
            DateTime dateTimeVal = DateTime.Now;
            DateTime? nDateTimeVal = DateTime.Now;

            var cells = new object[]
            {
                id,
                boolVal,
                nBoolVal,
                intVal,
                nIntVal,
                floatVal,
                nFloatVal,
                doubleVal,
                nDoubleVal,
                decVal,
                nDecVal,
                charVal,
                nCharVal,
                stringVal,
                dateTimeVal,
                nDateTimeVal,
                anotherStringVal,
            };
            var columnInfos = new ColumnInfo[]
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
                new ColumnInfo("AnotherStringVal", typeof(string)),
            };

            return new ResultRow(cells, columnInfos);
        }

        internal static IResultRow GetOrderDetailsMockData()
        {
            int orderId = new Random().Next();
            int productId = new Random().Next();
            int quantity = new Random().Next();

            var cells = new object[]
            {
                orderId,
                productId,
                quantity,
            };
            var columnInfos = new ColumnInfo[]
            {
            new ColumnInfo("OrderId", typeof(int)),
            new ColumnInfo("ProductId", typeof(int)),
            new ColumnInfo("Quantity", typeof(int)),
            };

            return new ResultRow(cells, columnInfos);
        }

        internal static IResultRow GetOrderDetailsNullsMockData()
        {
            int? orderId = null;
            int? productId = null;
            int? quantity = null;

            var columnInfos = new ColumnInfo[]
            {
            new ColumnInfo("OrderId", typeof(int)),
            new ColumnInfo("ProductId", typeof(int)),
            new ColumnInfo("Quantity", typeof(int)),
            };

            var cells = new object[]
            {
                orderId,
                productId,
                quantity,
            };

            return new ResultRow(cells, columnInfos);
        }

        internal static IResultRow GetPropertyWithWrongDataTypeMockData()
        {
            int? id = null;
            bool? boolVal = null;
            bool? nBoolVal = null;
            int? intval = null;
            int? nIntval = null;
            float? floatVal = null;
            float? nFloatVal = null;
            double? doubleVal = null;
            double? nDoubleVal = null;
            decimal? decVal = null;
            decimal? nDecVal = null;
            char? charVal = null;
            int? nCharVal = null;
            string stringVal = null;
            DateTime? dateTimeVal = null;
            DateTime? nDateTimeVal = null;

            var cells = new object[]
            {
                id,
                boolVal,
                nBoolVal,
                intval,
                nIntval,
                floatVal,
                nFloatVal,
                doubleVal,
                nDoubleVal,
                decVal,
                nDecVal,
                charVal,
                nCharVal,
                stringVal,
                dateTimeVal,
                nDateTimeVal,
            };

            var columnInfos = new ColumnInfo[]
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
                new ColumnInfo("NCharVal", typeof(int)),    // FOUTE CHAR VALUE
                new ColumnInfo("StringVal", typeof(string)),
                new ColumnInfo("DateTimeVal", typeof(DateTime)),
                new ColumnInfo("NDateTimeVal", typeof(DateTime)),
            };

            return new ResultRow(cells, columnInfos);
        }

        internal static IResultRow GetExtraPropMockData()
        {
            int? id = null;
            bool? boolVal = null;
            bool? nBoolVal = null;
            int? intval = null;
            int? nIntval = null;
            float? floatVal = null;
            float? nFloatVal = null;
            double? doubleVal = null;
            double? nDoubleVal = null;
            decimal? decVal = null;
            decimal? nDecVal = null;
            char? charVal = null;
            char? nCharVal = null;
            string stringVal = null;
            DateTime? dateTimeVal = null;
            DateTime? nDateTimeVal = null;
            int? someNonExistingProperty = 1;

            var cells = new object[]
            {
                id, 
                boolVal, 
                nBoolVal, 
                intval, 
                nIntval, 
                floatVal, 
                nFloatVal, 
                doubleVal, 
                nDoubleVal, 
                decVal, 
                nDecVal, 
                charVal,
                nCharVal, 
                stringVal, 
                dateTimeVal, 
                nDateTimeVal, 
                someNonExistingProperty,
            };

            var columnInfos = new ColumnInfo[]
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
                new ColumnInfo("SomeNonExistendProperty", typeof(int)),
            };

            return new ResultRow(cells, columnInfos);
        }

        internal static IResultRow GetTestClassMockData()
        {
            int id = 1;
            bool boolVal = true;
            bool? nBoolVal = true;
            int intval = 1;
            int? nIntval = 1;
            float floatVal = 1;
            float? nFloatVal = 1;
            double doubleVal = 1;
            double? nDoubleVal = 1;
            decimal decVal = 1;
            decimal? nDecVal = 1;
            char charVal = '1';
            char nCharVal = '1';
            string stringVal = "a";
            DateTime dateTimeVal = DateTime.Now;
            DateTime? nDateTimeVal = DateTime.Now;

            var cells = new object[]
            {   id,
                boolVal,
                nBoolVal,
                intval,
                nIntval,
                floatVal,
                nFloatVal,
                doubleVal,
                nDoubleVal,
                decVal,
                nDecVal,
                charVal,
                nCharVal,
                stringVal,
                dateTimeVal,
                nDateTimeVal,
            };

            var columnInfos = new ColumnInfo[]
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

            return new ResultRow(cells, columnInfos);

        }
        
        internal static IResultRow GetTestClassNullsMockData()
        {
            int? id = 1;
            bool? boolVal = null;
            bool? nBoolVal = null;
            int? intval = null;
            int? nIntval = null;
            float? floatVal = null;
            float? nFloatVal = null;
            double? doubleVal = null;
            double? nDoubleVal = null;
            decimal? decVal = null;
            decimal? nDecVal = null;
            char? charVal = null;
            char? nCharVal = null;
            string stringVal = null;
            DateTime? dateTimeVal = null;
            DateTime? nDateTimeVal = null;

            var cells = new object[]
            {   id,
                boolVal,
                nBoolVal,
                intval,
                nIntval,
                floatVal,
                nFloatVal,
                doubleVal,
                nDoubleVal,
                decVal,
                nDecVal,
                charVal,
                nCharVal,
                stringVal,
                dateTimeVal,
                nDateTimeVal,
            };

            var columnInfos = new ColumnInfo[]
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

            return new ResultRow(cells, columnInfos);
        }


    }
}
