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

        public Customer(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Customer(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, activeLoading)
        {
            this.Populate(objectData);
        }

        [IdProperty]
        public int Id
        {
            get => this.id;
            set
            {
                if (value != this.Id)
                {
                    this.id = value;
                    if (!this.Populating)
                    {
                        this.PopulateById(this.id);
                    }
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

                    if (this.Populating)
                    {
                        return;
                    }

                    if (this.ActiveLoading)
                    {
                        this.UpdateProperty((string)value);
                    }
                }
            }
        }

        [DefaultColumn]
        public DateTime LastUpdated 
        { 
            get => this.lastUpdated;
            set
            {
                if (this.Populating)
                {
                    this.lastUpdated = value;
                }
            }
        }
    }
}
