using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseExample.Interfaces
{
    /// <summary>
    /// Interface for Populatable objects.
    /// </summary>
    public interface IPopulatable
    {
        /// <summary>
        /// Gets or sets a value indicating whether properties shall be updated immeadiately to the database.
        /// </summary>
        public bool ActiveLoading { get; set; }

        /// <summary>
        /// Updates the populated object in the database.
        /// </summary>
        /// <returns>Number of rows (1) affected. returns zero on failure. </returns>
        public int UpdateObject();
    }
}
