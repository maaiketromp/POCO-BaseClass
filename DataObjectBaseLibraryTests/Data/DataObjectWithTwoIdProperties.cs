namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;

    public class DataObjectWithTwoIdProperties : DataObjectBase
    {
        public DataObjectWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base(wrapper, activeUpdate)
        {
        }

        public DataObjectWithTwoIdProperties(IDatabaseConnectorWrapper wrapper, int id, bool activeUpdate = false)
            : base(wrapper, id, activeUpdate)
        {
        }

        [IdProperty]
        public int Id { get; set; }

        [IdProperty]
        public string Name { get; set; }
    }
}
