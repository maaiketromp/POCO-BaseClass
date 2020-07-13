using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestDataObjectBase
{
    public class PageElement : DataObjectBase
    {
        private int pageId;
        private int elementId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageElement"/> class.
        /// </summary>
        /// <param name="db">DatabaseConnector.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        public PageElement(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageElement"/> class and populates it.
        /// </summary>
        /// <param name="db">DatabaseConnector.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        /// <param name=""
        public PageElement(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, objectData, activeLoading)
        {
        }

        /// <summary>
        /// Gets or sets the Id property of this object. When Id is set, this object will be populated automatically.
        /// </summary>
        [IdProperty]
        public int PageId
        {
            get => this.pageId;
            set
            {
                if (value != this.pageId)
                {
                    this.pageId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Id property of this object. When Id is set, this object will be populated automatically.
        /// </summary>
        [IdProperty]
        public int ElementId
        {
            get => this.elementId;
            set
            {
                if (value != this.elementId)
                {
                    this.elementId = value;
                }
            }
        }

        public bool NotUsed { get; set; }
    }
}
