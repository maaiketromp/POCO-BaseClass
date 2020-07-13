// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="IdPropertyAttribute.cs" company="">
// Copyright (C) 2020 Maaike Tromp

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
