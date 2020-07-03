using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using Microsoft.Data.SqlClient;
using System;

namespace DataObjectBaseExample.DataObjects
{
    class Order : DataObjectBase
    {
        private int id;
        private int customerId;
        private int employeeId;
        private DateTime shippingDate;

        public Order(DatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Order(DatabaseConnector db, SqlDataReader rdr, bool activeLoading = false)
            : base(db, activeLoading)
        {
            this.Populate(rdr);
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
