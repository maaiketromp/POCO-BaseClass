// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="IPopulatable.cs" company="">
// Copyright (C) 2020 Maaike Tromp

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
        public bool ActiveUpdate { get; set; }

        /// <summary>
        /// Updates the populated object in the database.
        /// </summary>
        /// <returns>Number of rows (1) affected. returns zero on failure. </returns>
        public int UpdateObject();
    }
}
