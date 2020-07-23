
namespace DataObjectBaseLibrary.DataObjects
{
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;
    using System;
    using System.Collections.Generic;

    public class TestClass : DataObjectBase
    {
        public TestClass(IDatabaseConnectorWrapper db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        public TestClass(IDatabaseConnectorWrapper db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        public TestClass(IDatabaseConnectorWrapper db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate)
            : base(db, objectData, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="db">Database wrapper instance.</param>
        /// <param name="data">Data to populate object.</param>
        /// <param name="activeUpdate">Value indicating whether the object should update any changes immeadiately to the database.</param>
        public TestClass(IDatabaseConnectorWrapper db, IResultTable data, bool activeUpdate)
            : base(db, data, activeUpdate)
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
