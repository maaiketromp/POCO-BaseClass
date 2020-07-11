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

        public Order(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Order(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
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

        public int CustomerId
        {
            get => this.customerId;
            set
            {
                if (this.customerId != value)
                {
                    this.customerId = value;

                    if (this.Populating)
                    {
                        return;
                    }

                    if (this.ActiveLoading)
                    {
                        this.UpdateProperty(value);
                    }
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

                    if (this.ActiveLoading)
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

                    if (this.ActiveLoading)
                    {
                        this.UpdateProperty(value);
                    }
                }
            }
        }
    }
}
