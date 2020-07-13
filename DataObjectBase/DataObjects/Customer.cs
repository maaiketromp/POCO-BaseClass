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

        public Customer(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Customer(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, objectData, activeLoading)
        {
        }

        public Customer(IDatabaseConnector db, int id, bool activeLoading = false)
            : base(db, id, activeLoading)
        {
        }

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
