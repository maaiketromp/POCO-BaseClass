using DataObjectBaseExample.Data;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestDataObjectBase.InvalidObject
{
    public class ClassWithoutPrimaryKey : DataObjectBase
    {
        private int id;
        private bool isSet;
        private string name;
        
        public ClassWithoutPrimaryKey(IDatabaseConnector db, bool activeUpdate = false)
            : base(db, activeUpdate)
        {
        }

        public ClassWithoutPrimaryKey(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate = false)
            : base(db, objectData, activeUpdate)
        {
        }

        public int Id
        {
            get => this.id;
            set
            {
                if (this.isSet == false)
                {
                    this.id = value;
                    this.isSet = true;
                }
                else
                {
                    throw new Exception("Id settable only once.");
                }
            }
        }

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


    }
}
