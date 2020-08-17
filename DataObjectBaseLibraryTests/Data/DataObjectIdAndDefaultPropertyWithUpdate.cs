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

        public DataObjectIdAndDefaultPropertyWithUpdate(IDatabaseConnectorWrapper wrapper, bool activeUpdate = false)
            : base (wrapper, activeUpdate)
        {
        }

        [IdProperty]
        public int Id 
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
        public string Name
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

        public string Address
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
