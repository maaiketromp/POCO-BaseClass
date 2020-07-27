// <copyright file="TestClass.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.DataObjects
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    using DataObjectBaseLibrary.Interfaces;

    /// <summary>
    /// A testclass with different data types.
    /// </summary>
    public class TestClass : DataObjectBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating whether the object should update any changes directly to the database.</param>
        public TestClass(IDatabaseConnectorWrapper db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="id">Id of the record to retrieve from database.</param>
        /// <param name="activeUpdate">A value indicating whether the object should update any changes directly to the database.</param>
        public TestClass(IDatabaseConnectorWrapper db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="objectData">Data to populate object.</param>
        /// <param name="activeUpdate">A value indicating whether the object should update changes immediately to the database.</param>
        public TestClass(IDatabaseConnectorWrapper db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestClass"/> class.
        /// </summary>
        /// <param name="db">Database wrapper instance.</param>
        /// <param name="data">Data to populate object.</param>
        /// <param name="activeUpdate">Value indicating whether the object should update any changes immeadiately to the database.</param>
        public TestClass(IDatabaseConnectorWrapper db, IResultRow data, bool activeUpdate = false)
            : base(db, data, activeUpdate)
        {
        }

        /// <summary>
        /// Gets or sets an Id property.
        /// </summary>
        [IdProperty]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a test value is true or false.
        /// </summary>
        public bool BoolVal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a test value is true, false or null.
        /// </summary>
        public bool? NBoolVal { get; set; }

        /// <summary>
        /// Gets or sets a test int value.
        /// </summary>
        public int IntVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable int test value.
        /// </summary>
        public int? NIntVal { get; set; }

        /// <summary>
        /// Gets or sets a float value.
        /// </summary>
        public float FloatVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable float test value.
        /// </summary>
        public float? NFloatVal { get; set; }

        /// <summary>
        /// Gets or sets a double test value.
        /// </summary>
        public double DoubleVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable double test  value.
        /// </summary>
        public double? NDoubleVal { get; set; }

        /// <summary>
        /// Gets or sets a decimal value.
        /// </summary>
        public decimal DecVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable decimal test value.
        /// </summary>
        public decimal? NDecVal { get; set; }

        /// <summary>
        /// Gets or sets a char test value.
        /// </summary>
        public char CharVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable char test value.
        /// </summary>
        public char? NCharVal { get; set; }

        /// <summary>
        /// Gets or sets a string test value.
        /// </summary>
        public string StringVal { get; set; }

        /// <summary>
        /// Gets or sets a datetime test value.
        /// </summary>
        public DateTime DateTimeVal { get; set; }

        /// <summary>
        /// Gets or sets a nullable datetime test value.
        /// </summary>
        public DateTime? NDateTimeVal { get; set; }
    }
}
