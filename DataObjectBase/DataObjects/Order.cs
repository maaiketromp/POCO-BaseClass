// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="Order.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.DataObjects
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.Interfaces;
  
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

        public Order(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Order(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, objectData, activeLoading)
        {
        }

        public Order(IDatabaseConnector db, int id, bool activeLoading = false)
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

        public int EmployeeId
        {
            set
            {
                if (this.employeeId != value)
                {
                    this.employeeId= value;

                    if (this.Populating)
                    {
                        return;
                    }

                    if (this.ActiveUpdate)
                    {
                        this.UpdateProperty(value);
                    }
                }
            }
        }

        public DateTime ShippingDate
        {
            set
            {
                if (this.shippingDate != value)
                {
                    this.shippingDate = value;

                    if (this.Populating)
                    {
                        return;
                    }

                    if (this.ActiveUpdate)
                    {
                        this.UpdateProperty(value);
                    }
                }
            }
        }
    }
}
