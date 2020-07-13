// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="DefaultColumnAttribute .cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.Attributes
{
    using System;

    /// <summary>
    /// Identifies a column with a default value that needs to be updated when another column is updated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultColumnAttribute : Attribute
    {
    }
}
