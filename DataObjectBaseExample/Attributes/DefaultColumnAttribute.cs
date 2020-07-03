using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseExample.Attributes
{
    /// <summary>
    /// Identifies a column with a default value that needs to be updated when another column is updated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultColumnAttribute : Attribute
    {
    }
}
