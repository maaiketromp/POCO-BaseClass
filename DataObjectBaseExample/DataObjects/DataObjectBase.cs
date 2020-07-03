namespace DataObjectBaseExample.DataObjects
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.Interfaces;
    using Microsoft.Data.SqlClient;


    /// <summary>
    /// A base object enabling POCO's to populate an object, to populate by id, to update an object and to update a single property.
    /// </summary>
    public class DataObjectBase : IPopulatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectBase"/> class.
        /// </summary>
        /// <param name="db">The databaseconnection.</param>
        /// <param name="activeLoading">A value indicating this object's loading state.</param>
        public DataObjectBase(DatabaseConnector db, bool activeLoading)
        {
            this.Db = db;
            this.ActiveLoading = activeLoading;
        }

        /// <inheritdoc/>
        public bool ActiveLoading { get; set; }

        /// <summary>
        /// Gets Database crudclass instance.
        /// </summary>
        protected DatabaseConnector Db { get; }

        /// <summary>
        /// Gets a value indicating whether the object is now populating.
        /// </summary>
        protected bool Populating { get; private set; }

        /// <inheritdoc/>
        public int UpdateObject()
        {
            var t = this.GetType();
            var props = t.GetProperties();
            string sql = $"UPDATE {t.Name} SET ";
            string condition = this.GetConditionForUpdate(props);

            using (var rdr = this.GetColumnInfo(t.Name))
            {
                while (rdr.Read())
                {
                    string colName = Convert.ToString(rdr["COLUMN_NAME"]);
                    string defaultVal = Convert.ToString(rdr["COLUMN_DEFAULT"]);
                    if (defaultVal != null)
                    {
                        sql += $" {colName} = DEFAULT , ";
                    }
                    else
                    {
                        if (colName != "Id")
                        {
                            var prop = t.GetProperty(colName);
                            sql += $" {colName} = '{prop.GetValue(this)}', ";
                        }
                    }
                }
            }

            sql = sql.TrimEnd(new char[] { ',', ' ' }) + condition;
            return this.Db.PrepareAndExecuteNonQuery(commandText: sql);
        }

        /// <summary>
        /// Populates this object with a provided SqlDataReader.
        /// </summary>
        /// <param name="rdr">A Datareader containing data for this object.</param>
        protected void Populate(SqlDataReader rdr)
        {
            this.PopulateInternal(rdr);
        }

        /// <summary>
        /// Set an id and populate the object.
        /// </summary>
        /// <param name="id">Integer Id of object.</param>
        protected void PopulateById(int id)
        {
            Type t = this.GetType();
            using SqlDataReader rdr = this.Db.PrepareAndExecuteQuery(
                    commandText: $"SELECT * FROM {t.Name} WHERE Id = {id}");
            if (rdr.Read())
            {
                this.PopulateInternal(rdr);
            }
        }

        /// <summary>
        /// Updates a single property in the database.
        /// </summary>
        /// <param name="value">The value from the set method of the property.</param>
        /// <param name="propName">Name of the caller.</param>
        /// <typeparam name="TV">Type of the value parameter.</typeparam>
        protected void UpdateProperty<TV>(TV value, [CallerMemberName] string propName = null)
        {
            var t = this.GetType();
            if (propName == null)
            {
                throw new InvalidOperationException("Cannot update without a propertyName!");
            }

            var props = t.GetProperties();
            SqlParameter[] parameters = this.GetParameter(value);
            string condition = this.GetConditionForUpdate(props);
            string defValues = this.GetDefaultColumns(props);
            string sql = $"UPDATE {t.Name} SET {propName} = @Value " +
                defValues + condition;
            if (this.Db.PrepareAndExecuteNonQuery(commandText: sql, parameters: parameters) != 1)
            {
                throw new InvalidOperationException("Can't update an non-existing record.");
            }
        }

        private string GetDefaultColumns(PropertyInfo[] props)
        {
            string output = string.Empty;

            // Check for a columns with default attribute (for timestamps).
            var defaultVals = from p in props
                              where p.GetCustomAttributes<DefaultColumnAttribute>().Count() == 1
                              select p.Name;

            if (defaultVals.Count() > 0)
            {
                output += ", " + string.Join(" , ", defaultVals.Select(d => $" {d} = DEFAULT "));
            }

            return output;
        }

        private SqlParameter[] GetParameter<TV>(TV value)
        {
            SqlParameter[] output = new SqlParameter[]
            {
                new SqlParameter("@Value", value),
            };

            return output;
        }

        private string GetConditionForUpdate(PropertyInfo[] props)
        {
            string output = " WHERE ";
            var ids = from p in props
                      where p.GetCustomAttributes<IdPropertyAttribute>().Count() == 1
                      select new { name = p.Name, value = p.GetValue(this) };

            if (ids.Count() < 1)
            {
                throw new InvalidOperationException("Cannot update property without a primary key");
            }
            else if (ids.Count() > 1)
            {
                output += string.Join(" AND ", ids.Select(a => $"{a.name} = {a.value}"));
            }
            else
            {
                output += $" {ids.First().name} = {ids.First().value} ";
            }

            return output;
        }

        private void PopulateInternal(SqlDataReader rdr)
        {
            this.Populating = true;
            try
            {
                for (int x = 0; x < rdr.FieldCount; x++)
                {
                    string name = rdr.GetName(x);
                    var prop = this.GetType().GetProperty(name);
                    if (prop == null)
                    {
                        // field is not in properties, but also null in reader (enables joinqueries).
                        if (Convert.IsDBNull(rdr.GetValue(x)))
                        {
                            continue;
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                $"No property found with name {name}");
                        }
                    }
                    else
                    {
                        if (!Convert.IsDBNull(rdr.GetValue(x)))
                        {
                            Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            prop.SetValue(this, Convert.ChangeType(rdr.GetValue(x), t));
                        }
                        else
                        {
                            prop.SetValue(this, null);
                        }
                    }
                }
            }
            finally
            {
                this.Populating = false;
            }
        }

        private SqlDataReader GetColumnInfo(string tableName)
        {
            string sql = $"SELECT COLUMN_NAME, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME = '{tableName}'";
            return this.Db.PrepareAndExecuteQuery(commandText: sql);
        }
    }
}
