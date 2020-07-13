// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="IdPropertyAttribute.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.Attributes
{
    using System;

    /// <summary>
    /// Identifies an Id Property (or properties) of a plain DataObject.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdPropertyAttribute : Attribute
    {
    }
}
