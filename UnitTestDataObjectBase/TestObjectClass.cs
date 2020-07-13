// <copyright file="TestFormFieldInfo.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace UnitTestDataObjectBase
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.DataObjects;
    using DataObjectBaseExample.Interfaces;

    /// <summary>
    /// Contains all different datatypes.
    /// </summary>
    public class TestObjectClass : DataObjectBase
    {
        public TestObjectClass(IDatabaseConnector db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        public TestObjectClass(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        public TestObjectClass(IDatabaseConnector db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        [IdProperty]
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
