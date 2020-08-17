// <copyright file="DerivedFromTest.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.DataObjects
{
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.Interfaces;

    /// <summary>
    /// Derived class from TestClass.
    /// </summary>
    public class DerivedFromTest : TestClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DerivedFromTest"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating whether the object should update any changes directly to the database.</param>
        public DerivedFromTest(IDatabaseConnectorWrapper db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DerivedFromTest"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="id">Id of the record to retrieve from database.</param>
        /// <param name="activeUpdate">A value indicating whether the object should update any changes directly to the database.</param>
        public DerivedFromTest(IDatabaseConnectorWrapper db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DerivedFromTest"/> class.
        /// </summary>
        /// <param name="db">Database wrapper instance.</param>
        /// <param name="data">Data to populate object.</param>
        /// <param name="activeUpdate">Value indicating whether the object should update any changes immeadiately to the database.</param>
        public DerivedFromTest(IDatabaseConnectorWrapper db, IResultRow data, bool activeUpdate = false)
            : base(db, data, activeUpdate)
        {
        }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [IdProperty]
        public override int Id { get; set; }

        /// <summary>
        /// Gets or sets another test string value.
        /// </summary>
        public string AnotherStringVal { get; set; }
    }
}
