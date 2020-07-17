using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseLibrary.Data
{
    public struct ColumnInfo
    {
        public ColumnInfo(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name { get; }

        public Type Type { get; }
    }
}
