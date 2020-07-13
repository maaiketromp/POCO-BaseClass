using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestDataObjectBase.InvalidObject
{
    public class Status : DataObjectBase
    {
        private int id;
        private string name;
        private bool isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class with a databaseConnection and object loading state.
        /// </summary>
        /// <param name="db">The databaseconnection.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        public Status(IDatabaseConnector db, bool activeLoading)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class and populates it using a SqlDataReader.
        /// Populates it.
        /// </summary>
        /// <param name="db">databaseconnection.</param>
        /// <param name="rdr">A SqlDataReader for populating.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        public Status(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        public Status(IDatabaseConnector db, int id, bool activeUpdate = false)
            : base(db, id, activeUpdate)
        {
        }

        /// <summary>
        /// Gets or sets the primary key of the object.
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

        // Property Name is missing.
    }
}
