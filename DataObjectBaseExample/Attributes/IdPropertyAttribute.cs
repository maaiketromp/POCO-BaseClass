using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseExample.Attributes
{
    /// <summary>
    /// Identifies an Id Property (or properties) of a plain DataObject.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdPropertyAttribute : Attribute
    {
    }
}
