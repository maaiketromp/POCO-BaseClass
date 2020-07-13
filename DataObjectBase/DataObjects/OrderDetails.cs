// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="OrderDetails.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.DataObjects
{
    using System.Collections.Generic;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.Interfaces;

    /// <summary>
    /// Represents an record with order details and product ids.
    /// </summary>
    public class OrderDetails : DataObjectBase
    {
        private int orderId;
        private int productId;
        private int quantity;

        public OrderDetails(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public OrderDetails(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        [IdProperty]
        public int OrderId
        {
            get => this.orderId;
            set
            {
                if (value != this.orderId)
                {
                    // No populating by id with compound primary keys.
                    this.orderId = value;
                }
            }
        }

        [IdProperty]
        public int ProductId
        {
            get => this.productId;
            set
            {
                if (value != this.productId)
                {
                    // No populating by id with compound primary keys.
                    this.productId = value;
                }
            }
        }

        public int Quantity
        {
            get => this.quantity;
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;

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
