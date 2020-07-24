// <copyright file="IdPropertyAttribute.cs" company="Maaike Tromp">
// Copyright (c) Maaike Tromp. All rights reserved.
// </copyright>

namespace DataObjectBaseLibrary.Attributes
{
    using System;

    /// <summary>
    /// Identifies an Id Property of a POCO.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdPropertyAttribute : Attribute
    {
    }
}
