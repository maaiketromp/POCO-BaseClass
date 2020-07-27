namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MockClassWithTwoIdProperties : DataObjectBase
    {
        public MockClassWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base(wrapper, activeUpdate)
        {
        }

        public MockClassWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, int id, bool activeUpdate = false)
            : base(wrapper, id, activeUpdate)
        {
        }

        [IdProperty]
        public int Id { get; set; }

        [IdProperty]
        public string Name { get; set; }
    }
}
