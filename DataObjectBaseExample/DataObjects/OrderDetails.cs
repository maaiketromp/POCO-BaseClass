using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseExample.DataObjects
{
    class OrderDetails : DataObjectBase
    {
        private int orderId;
        private int productId;
        private int quantity;

        public OrderDetails(DatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public OrderDetails(DatabaseConnector db, SqlDataReader rdr, bool activeLoading = false)
            : base(db, activeLoading)
        {
            this.Populate(rdr);
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

                    if (this.ActiveLoading)
                    {
                        this.UpdateProperty(value);
                    }
                }
            }
        }

    }
}
