// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="DataObjectBase.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseLibrary.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Attributes;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Interfaces;

    /// <summary>
    /// A base class for a POCO that interacts with the database.
    /// </summary>
    public abstract class DataObjectBase : IPopulatable
    {
        private bool populating;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectBase"/> class.
        /// </summary>
        /// <param name="db">Databaseconnection/ </param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public DataObjectBase(IDatabaseConnectorWrapper db, bool activeUpdate)
        {
            this.Db = db;
            this.ActiveUpdate = activeUpdate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectBase"/> class.
        /// </summary>
        /// <param name="db">The databaseconnection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public DataObjectBase(IDatabaseConnectorWrapper db, int id, bool activeUpdate)
        {
            this.Db = db;
            this.ActiveUpdate = activeUpdate;
            this.PopulateById(id);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataObjectBase"/> class.
        /// </summary>
        /// <param name="db">The databaseconnection.</param>
        /// <param name="activeUpdate">A value indicating if the object should update any changes immeadiately to the database.</param>
        public DataObjectBase(IDatabaseConnectorWrapper db, Dictionary<string, DatabaseObject> objectData, bool activeUpdate)
        {
            this.Db = db;
            this.ActiveUpdate = activeUpdate;
            this.Populate(objectData);
        }

        public DataObjectBase(IDatabaseConnectorWrapper db, IResultTable data, bool activeUpdate)
        {
            this.Db = db;
            this.ActiveUpdate = activeUpdate;
            this.PopulateWithIResultTable(data);
        }


        /// <inheritdoc/>
        public bool ActiveUpdate { get; set; }

        /// <summary>
        /// Gets Database connection object.
        /// </summary>
        protected IDatabaseConnectorWrapper Db { get; }

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
                    if (string.IsNullOrEmpty(defaultVal))
                    {
                        if (colName != "Id")
                        {
                            var prop = t.GetProperty(colName);
                            sql += $" {colName} = '{prop.GetValue(this)}', ";
                        }
                    }
                    else
                    {
                        sql += $" {colName} = DEFAULT , ";
                    }
                }
            }

            sql = sql.TrimEnd(new char[] { ',', ' ' }) + condition;
            return this.Db.PrepareAndExecuteNonQuery(commandText: sql);
        }

        public void PopulateWithIResultTable(IResultTable queryResult)
        {
            if (queryResult.Count() > 1)
            {
                throw new InvalidOperationException("Multiple records of inputdata.");
            }

            this.populating = true;
            Type t = this.GetType();
            var data = queryResult.First();
            int colNum = queryResult[0].Count();

            for (int i = 0; i < colNum; i++)
            {
                PropertyInfo prop = t.GetProperty(queryResult.GetColumnName(i));

                if (prop == null)
                {
                    if (data[i].ValueObject == null)
                    {
                        continue;
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"Object does not contains a property {queryResult.GetColumnName(i)}, but resultset contains a non-null value.");
                    }
                }
                else if (!this.ValidateType(prop, data[i].ValueType))
                {
                    throw new ArgumentException($"Unsafe Conversion not permitted. Type validation for {prop.Name} failed.");
                }
                else
                {
                    if (data[i].ValueObject != null)
                    {
                        Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        prop.SetValue(this, Convert.ChangeType(data[i].ValueObject, type));
                    }
                    else if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    {
                        prop.SetValue(this, null);
                    }
                    else
                    {
                        // allows database entries to be null, while the property is not nullable.
                    }
                }
            }

            this.populating = false;
        }


        /// <summary>
        /// Populates this object with a provided SqlDataReader.
        /// </summary>
        /// <param name="objectData">A Datareader containing data for this object.</param>
        private void Populate(Dictionary<string, DatabaseObject> objectData)
        {
            this.populating = true;
            Type t = this.GetType();
            foreach (var unit in objectData)
            {
                PropertyInfo prop = t.GetProperty(unit.Key);

                if (prop == null)
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
                else if (!this.ValidateType(prop, unit.Value.ValueType))
                {
                    throw new ArgumentException($"Unsafe Conversion not permitted. Type validation for {prop.Name} failed.");
                }
                else
                {
                    if (unit.Value.ValueObject != null)
                    {
                        Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        prop.SetValue(this, Convert.ChangeType(unit.Value.ValueObject, type));
                    }
                    else if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                    {
                        prop.SetValue(this, null);
                    }
                    else
                    {
                        // allows database entries to be null, while the property is not nullable.
                    }
                }
            }

            this.populating = false;
        }

        /// <summary>
        /// Set an id and populate the object.
        /// </summary>
        /// <param name="id">Integer Id of object.</param>
        private void PopulateById(int id)
        {
            Type t = this.GetType();

            var idCheck = (from p in t.GetProperties()
                          where Attribute.IsDefined(p, typeof(IdPropertyAttribute))
                          select p).Count();
            if (idCheck > 1)
            {
                throw new InvalidOperationException("Populating by Id not supported for compound Primary Key.");
            }

            string sql = $"SELECT * FROM {t.Name} WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
            };

            
            var objectData = this.Db.GetResultAsDictionary(commandText: sql, parameters: parameters).First();
                        
            this.Populate(objectData);
        }

        /// <summary>
        /// Updates a single property in the database.
        /// </summary>
        /// <param name="value">The value from the set method of the property.</param>
        /// <param name="propName">Name of the caller.</param>
        /// <typeparam name="T">Type of the value parameter.</typeparam>
        protected void UpdateProperty<T>(T value, [CallerMemberName] string propName = null)
        {
            if (this.populating || !this.ActiveUpdate)
            {
                return;
            }

            if (propName == null)
            {
                throw new InvalidOperationException("Cannot update without a propertyName!");
            }

            Type t = this.GetType();
            var prop = t.GetProperty(propName);

            // Do not update properties with Id or Default Attribute.
            if (Attribute.IsDefined(prop, typeof(DefaultColumnAttribute)) ||
                Attribute.IsDefined(prop, typeof(IdPropertyAttribute)))
            {
                throw new InvalidOperationException("Updating Id or Default column not allowed.");
            }

            this.UpdatePropertyInternal(value, propName, t);
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
                      where Attribute.IsDefined(p, typeof(IdPropertyAttribute))
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
                              where Attribute.IsDefined(p, typeof(DefaultColumnAttribute))
                              select p.Name;


            if (defaultVals.Count() > 0)
            {
                output += ", " + string.Join(" , ", defaultVals.Select(d => $" {d} = DEFAULT "));
            }

            return output;
        }

        private void UpdatePropertyInternal<T>(T value, string propName, Type t)
        {
            var props = t.GetProperties();
            string condition = this.GetConditionForUpdate(props);
            string defValues = this.GetDefaultColumns(props);
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Value", value),
            };

            string sql = $"UPDATE {t.Name} SET {propName} = @Value " +
                defValues + condition;

            if (this.Db.PrepareAndExecuteNonQuery(commandText: sql, parameters: parameters) != 1)
            {
                throw new InvalidOperationException("Can't update an non-existing record.");
            }
        }





        private bool ValidateType(PropertyInfo prop, Type dataType)
        {
            // if selected property is a char, check if type returned by the databasereader is a string.
            if (prop.PropertyType.IsEquivalentTo(typeof(char)) || prop.PropertyType.IsEquivalentTo(typeof(char?)))
            {
                if (dataType.IsEquivalentTo(typeof(string)))
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
                if (dataType.IsEquivalentTo(typeof(float)))
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
            if (propType.IsEquivalentTo(dataType))
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
