// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="Order.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.DataObjects
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;
  
    /// <summary>
    /// Represents and holds the data off an order.
    /// </summary>
    public class Order : DataObjectBase
    {
        private int id;
        private int customerId;
        private int employeeId;
        private DateTime shippingDate;
        private bool isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Order(IDatabaseConnectorWrapper db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="objectData">Data to populate the object.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Order(IDatabaseConnectorWrapper db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="id">Id of the record to be loaded from the database.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public Order(IDatabaseConnectorWrapper db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="db">Database wrapper instance.</param>
        /// <param name="data">Data to populate object.</param>
        /// <param name="activeUpdate">Value indicating whether the object should update any changes immeadiately to the database.</param>
        public Order(IDatabaseConnectorWrapper db, IResultTable data, bool activeUpdate)
            : base(db, data, activeUpdate)
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
        /// Gets or sets the customer id.
        /// </summary>
        public int CustomerId
        {
            get => this.customerId;
            set
            {
                if (this.customerId != value)
                {
                    this.customerId = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the employee's id.
        /// </summary>
        public int EmployeeId
        {
            get => this.employeeId;
            set
            {
                if (this.employeeId != value)
                {
                    this.employeeId = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the shipping data of the order.
        /// </summary>
        public DateTime ShippingDate
        {
            get => this.shippingDate;
            set
            {
                if (this.shippingDate != value)
                {
                    this.shippingDate = value;
                    this.UpdateProperty(value);
                }
            }
        }
    }
}
