// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="OrderDetails.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.DataObjects
{
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;

    /// <summary>
    /// Represents an record with order details and product ids.
    /// </summary>
    public class OrderDetails : DataObjectBase
    {
        private int orderId;
        private int productId;
        private int quantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetails"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public OrderDetails(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class 
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="objectData">Data to populate the object.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public OrderDetails(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        [IdProperty]
        public int OrderId
        {
            get => this.orderId;
            set
            {
                if (value != this.orderId)
                {
                    this.orderId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the product id.
        /// </summary>
        [IdProperty]
        public int ProductId
        {
            get => this.productId;
            set
            {
                if (value != this.productId)
                {
                    this.productId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the quantity of the products of this order.
        /// </summary>
        public int Quantity
        {
            get => this.quantity;
            set
            {
                if (this.quantity != value)
                {
                    this.quantity = value;
                    this.UpdateProperty(value);
                }
            }
        }
    }
}
