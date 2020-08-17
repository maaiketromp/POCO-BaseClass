namespace DataObjectBaseLibraryTests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;

    internal class DataObjectWithoutIdProperty : DataObjectBase
    {
        internal DataObjectWithoutIdProperty(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base (wrapper, activeUpdate)
        {
        }

        internal DataObjectWithoutIdProperty(IDatabaseConnectorWrapper wrapper, int id, bool activeUpdate = false)
            : base(wrapper, id, activeUpdate)
        {
        }

        internal int Id { get; set; }

        internal string Name { get; set; }

    }
}
