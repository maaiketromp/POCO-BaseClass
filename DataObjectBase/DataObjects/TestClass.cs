
namespace DataObjectBaseLibrary.DataObjects
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;
    using System;
    using System.Collections.Generic;

    public class TestClass : DataObjectBase
    {
        public TestClass(IDatabaseConnector db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        public TestClass(IDatabaseConnector db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        public TestClass(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate)
            : base(db, objectData, activeUpdate)
        {
        }

        public int Id { get; set; }

        public bool BoolVal { get; set; }

        public bool? NBoolVal { get; set; }

        public int IntVal { get; set; }

        public int? NIntVal { get; set; }

        public float FloatVal { get; set; }

        public float? NFloatVal { get; set; }

        public double DoubleVal { get; set; }

        public double? NDoubleVal { get; set; }

        public decimal DecVal { get; set; }

        public decimal? NDecVal { get; set; }

        public char CharVal { get; set; }

        public char? NCharVal { get; set; }

        public string StringVal { get; set; }

        public DateTime DateTimeVal { get; set; }

        public DateTime? NDateTimeVal { get; set; }
    }
}
