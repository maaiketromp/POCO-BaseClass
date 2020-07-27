namespace DataObjectBaseLibraryTests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;

    public class MockClassWithoutIdProperty : DataObjectBase
    {
        public MockClassWithoutIdProperty(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base (wrapper, activeUpdate)
        {
        }

        public MockClassWithoutIdProperty(IDatabaseConnectorWrapper wrapper, int id, bool activeUpdate = false)
            : base(wrapper, id, activeUpdate)
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
