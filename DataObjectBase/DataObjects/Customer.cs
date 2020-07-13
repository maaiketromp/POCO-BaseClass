// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="Customer.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.DataObjects
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.Interfaces;

    /// <summary>
    /// Represents a record of a customer.
    /// </summary>
    public class Customer : DataObjectBase
    {
        private int id;
        private string name;
        private DateTime lastUpdated;
        private bool isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Customer(IDatabaseConnector db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="objectData">Data to populate the object.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Customer(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="id">Id of the record to be loaded from the database.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Customer(IDatabaseConnector db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        /// <summary>
        /// Gets or sets the Id of the object.
        /// </summary>
        [IdProperty]
        public int Id
        {
            get => this.id;
            set
            {
                if (this.isSet == false)
                {
                    this.id = value;
                    this.isSet = true;
                }
                else
                {
                    throw new Exception("Id settable only once.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name
        {
            get => this.name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.UpdateProperty((string)value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the last time this record was updated.
        /// </summary>
        [DefaultColumn]
        public DateTime LastUpdated 
        { 
            get => this.lastUpdated;
            set
            {
                if (this.lastUpdated != value)
                {
                    this.lastUpdated = value;
                }
            }
        }
    }
}
