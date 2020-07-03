using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseExample.DataObjects
{
    class Customer : DataObjectBase
    {
        private int id;
        private string name;
        private DateTime lastUpdated;

        public Customer(DatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        public Customer(DatabaseConnector db, SqlDataReader rdr, bool activeLoading = false)
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
