namespace DataObjectBaseLibraryTests.Data
{
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.DataObjects;
    using DataObjectBaseLibrary.Interfaces;

    internal class DataObjectIdAndDefaultPropertyWithUpdate : DataObjectBase
    {
        private int id;
        private string name;
        private string address;

        internal DataObjectIdAndDefaultPropertyWithUpdate(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base (wrapper, activeUpdate)
        {
        }

        [IdProperty]
        internal int Id 
        { 
            get => this.id;
            set 
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.UpdateProperty(value);
                }
            } 
        }

        [DefaultColumn]
        internal string Name
        {
            get => this.name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.UpdateProperty(value);
                }
            }
        }

        internal string Address
        {
            get => this.address;
            set
            {
                if (this.address != value)
                {
                    this.address = value;
                    this.UpdateProperty(value, null);
                }
            }
        }

    }
}
