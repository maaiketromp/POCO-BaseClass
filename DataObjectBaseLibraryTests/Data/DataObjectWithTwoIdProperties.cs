namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;

    internal class DataObjectWithTwoIdProperties : DataObjectBase
    {
        internal DataObjectWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base(wrapper, activeUpdate)
        {
        }

        internal DataObjectWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, int id, bool activeUpdate = false)
            : base(wrapper, id, activeUpdate)
        {
        }

        [IdProperty]
        internal int Id { get; set; }

        [IdProperty]
        internal string Name { get; set; }
    }
}
