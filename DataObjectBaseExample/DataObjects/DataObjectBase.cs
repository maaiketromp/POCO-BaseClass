// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="DataObjectBase.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseExample.Attributes;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.Interfaces;
    using DataObjectBaseExample.Extensions;

    /// <summary>
    /// A base object enabling POCO's to populate an object, to populate by id, to update an object and to update a single property.
    /// </summary>
    public abstract class DataObjectBase : IPopulatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectBase"/> class.
        /// </summary>
        /// <param name="db">The databaseconnection.</param>
        /// <param name="activeLoading">A value indicating this object's loading state.</param>
        public DataObjectBase(IDatabaseConnector db, bool activeLoading)
        {
            this.Db = db;
            this.ActiveLoading = activeLoading;
        }

        /// <inheritdoc/>
        public bool ActiveLoading { get; set; }

        /// <summary>
        /// Gets Database crudclass instance.
        /// </summary>
        protected IDatabaseConnector Db { get; }

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
        protected void Populate(Dictionary<string, DatabaseObject> propertyData)
        {
            this.PopulateInternal(propertyData);
        }

        /// <summary>
        /// Set an id and populate the object.
        /// </summary>
        /// <param name="id">Integer Id of object.</param>
        protected void PopulateById(int id)
        {
            Type t = this.GetType();
            string sql = $"SELECT * FROM {t.Name} WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int, id),
            };

            var objectData = this.Db.PrepareAndExecuteQuery(commandText: sql, parameters: parameters)
                .ToList()
                .FirstOrDefault();
            
            this.PopulateInternal(objectData);
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

        private SqlDataReader GetColumnInfo(string tableName)
        {
            string sql = $"SELECT COLUMN_NAME, COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE TABLE_NAME = '{tableName}'";
            return this.Db.PrepareAndExecuteQuery(commandText: sql);
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

        private void PopulateInternal(Dictionary<string, DatabaseObject> objectData)
        {
            this.Populating = true;
            foreach (var unit in objectData)
            {
                Type t = this.GetType();
                var prop = t.GetProperty(unit.Key);

                if (!this.ValidateType(prop, unit))
                {
                    throw new ArgumentException($"Unsafe Conversion not permitted. Type validation for {prop.Name} failed.");
                }

                if (prop == null && unit.Value.ValueObject == null)
                {
                    // allows joins and larger queries.
                    if (unit.Value.ValueObject == null)
                    {
                        continue;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Object does not contains a property {unit.Key}, but resultset contains a non-null value.");
                    }
                }
                else
                {
                    if (unit.Value.ValueObject != null)
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        prop.SetValue(this, Convert.ChangeType(unit.Value.ValueObject, type));
                    }
                    else if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    {
                        prop.SetValue(this, null);
                    }
                    else
                    {
                        // allows databasecolumns to be nullable, while the property is not nullable.
                    }
                }
            }

            this.Populating = false;
        }

        private bool ValidateType(PropertyInfo prop, KeyValuePair<string, DatabaseObject> unit)
        {
            // if selected property is a char, check if type returned by the databasereader is a string.
            if (prop.PropertyType.IsEquivalentTo(typeof(char)) || prop.PropertyType.IsEquivalentTo(typeof(char?)))
            {
                if (unit.Value.ValueType.IsEquivalentTo(typeof(string)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // selected property is a double, check if type returned by databasereader type is a float.
            if (prop.PropertyType.IsEquivalentTo(typeof(double)) || prop.PropertyType.IsEquivalentTo(typeof(double?)))
            {
                if (unit.Value.ValueType.IsEquivalentTo(typeof(float)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // database reader does not return nullables, get underlying type in case of nullable property.
            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (propType.IsEquivalentTo(unit.Value.ValueType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
