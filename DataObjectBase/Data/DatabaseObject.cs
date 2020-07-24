// <copyright file="DatabaseObject.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Data
{
    using System;

    /// <summary>
    /// Stores a value and its type.
    /// </summary>
    public struct DatabaseObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseObject"/> struct.
        /// </summary>
        /// <param name="t">Type of the object value.</param>
        /// <param name="value">Value to be stored.</param>
        public DatabaseObject(Type t, object value)
        {
            this.ValueType = t;
            this.ValueObject = value;
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        public Type ValueType { get; }

        /// <summary>
        /// Gets a value retrieved from the database.
        /// </summary>
        public object ValueObject { get; }
    }
}
