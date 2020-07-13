using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestDataObjectBase.InvalidObject
{
    public class TestDefaultAndIdColumns : DataObjectBase
    {
        private int id;
        private DateTime date;
        private int secondId;
        private string anotherTest;

        public TestDefaultAndIdColumns(IDatabaseConnector db)
            : base(db)
        {
        }

        public TestDefaultAndIdColumns(IDatabaseConnector db, int id)
            : base(db, id)
        {
        }

        [IdProperty]
        public int Id 
        { 
            get => this.id;
            set
            {
                this.id = value;
                this.UpdateProperty(value);
            }
        }

        [IdProperty]
        public int SecondId
        {
            get => this.secondId;
            set
            {
                this.secondId = value;
                this.UpdateProperty(value);
            }
        }

        [DefaultColumn]
        public DateTime Date
        {
            get => this.date;
            set
            {
                this.date = value;
                this.UpdateProperty(value);
            }
        }

        public string AnotherTest
        {
            get => this.anotherTest;
            set
            {
                this.UpdateProperty(value, "");
            }
        }
    }
}
