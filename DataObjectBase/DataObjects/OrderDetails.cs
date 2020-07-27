// <copyright file="OrderDetails.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

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
        /// Initializes a new instance of the <see cref="OrderDetails"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public OrderDetails(IDatabaseConnectorWrapper db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetails"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="objectData">Data to populate the object.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public OrderDetails(IDatabaseConnectorWrapper db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetails"/> class.
        /// </summary>
        /// <param name="db">Database wrapper instance.</param>
        /// <param name="data">Data to populate object.</param>
        /// <param name="activeUpdate">Value indicating whether the object should update any changes immeadiately to the database.</param>
        public OrderDetails(IDatabaseConnectorWrapper db, IResultRow data, bool activeUpdate = false)
            : base(db, data, activeUpdate)
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
