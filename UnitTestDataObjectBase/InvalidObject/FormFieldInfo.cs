using DataObjectBaseExample.Attributes;
using DataObjectBaseExample.Data;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestDataObjectBase.InvalidObject
{
    public class FormFieldInfo : DataObjectBase
    {
        private string id;
        private int formId;
        private string tag;
        private string name;
        private string type;
        private string placeholder;
        private bool isHidden;
        private string labelText;
        private bool readOnly;
        private int editLevel;
        private int viewLevel;
        private int displayOrder;
        private int status;
        private string fieldType;
        private string value;
        private bool isSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldInfo"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        public FormFieldInfo(IDatabaseConnector db, bool activeLoading = false)
            : base(db, activeLoading)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFieldInfo"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="activeLoading">A value indicating the object's loading state.</param>
        /// <param name="rdr">A reader containing data to populate the object. </param>
        public FormFieldInfo(IDatabaseConnector db, Dictionary<string, DatabaseObject> objectData, bool activeLoading = false)
            : base(db, objectData, activeLoading)
        {
        }

        public FormFieldInfo(IDatabaseConnector db, int id, bool activeLoading = false)
            : base(db, id, activeLoading)
        {
        }

        /// <summary>
        /// Gets or sets the Id property of this object. When Id is set, this object will be populated automatically.
        /// </summary>
        [IdProperty]
        public string Id
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

        /// <summary>
        /// Gets or sets foreign key to Form table.
        /// </summary>
        public int FormId
        {
            get => this.formId;
            set
            {
                if (this.formId != value)
                {
                    this.formId = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the html tag of this formfield.
        /// </summary>
        public string Tag
        {
            get => this.tag;
            set
            {
                if (this.tag != value)
                {
                    this.tag = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the name attribute of the html tag of this formfield.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the type attribute of the htmltag of this formfield.
        /// </summary>
        public string Type
        {
            get => this.type;
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the placeholder attribute in the html-tag of this formfield.
        /// </summary>
        public string Placeholder
        {
            get => this.placeholder;
            set
            {
                if (this.placeholder != value)
                {
                    this.placeholder = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the hidden attribute is included in the htmltag of the formfield.
        /// </summary>
        public bool IsHidden
        {
            get => this.isHidden;
            set
            {
                if (this.isHidden != value)
                {
                    this.isHidden = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the text for the label tag.
        /// </summary>
        public string LabelText
        {
            get => this.labelText;
            set
            {
                if (this.labelText != value)
                {
                    this.labelText = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this formField is readonly.
        /// </summary>
        public bool ReadOnly
        {
            get => this.readOnly;
            set
            {
                if (this.readOnly != value)
                {
                    this.readOnly = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the editlevel, indicating which user can modify this field.
        /// </summary>
        public int EditLevel
        {
            get => this.editLevel;
            set
            {
                if (this.editLevel != value)
                {
                    this.editLevel = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the viewlevel, indicating which user can view this field.
        /// </summary>
        public int ViewLevel
        {
            get => this.viewLevel;
            set
            {
                if (this.viewLevel != value)
                {
                    this.viewLevel = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the DisplayOrder, which determines the order in which the fields are shown.
        /// </summary>
        public int DisplayOrder
        {
            get => this.displayOrder;
            set
            {
                if (this.displayOrder != value)
                {
                    this.displayOrder = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the status id of the formfield.
        /// </summary>
        public int Status
        {
            get => this.status;
            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the last time this record was updated.
        /// </summary>
        [DefaultColumn]
        public DateTime LastUpdated { get; private set; }

        /// <summary>
        /// Gets or sets the value attribute of the formfield.
        /// </summary>
        public string Value
        {
            get => this.value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.UpdateProperty(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the FieldType of this field.
        /// </summary>
        public string FieldType
        {
            get => this.fieldType;
            set
            {
                if (this.fieldType != value)
                {
                    this.fieldType = value;
                    this.UpdateProperty(value);
                }
            }
        }
    }
}
