// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="SqlDataReaderExtensions.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseExample.Data;

    /// <summary>
    /// Extensions for the SqlDataReader.
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Parses resultset of SqlDataReader.
        /// </summary>
        /// <param name="rdr">A SqlDataReader.</param>
        /// <returns>A list of Dictionary objects.</returns>
        public static List<Dictionary<string, DatabaseObject>> ToList(this SqlDataReader rdr)
        {
            var outputList = new List<Dictionary<string, DatabaseObject>>();
            
            if (!rdr.HasRows)
            {
                return outputList;
            }

            var colTypes = GetColumnDictionary(rdr);

            while (rdr.Read())
            {
                var row = new Dictionary<string, DatabaseObject>();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string colName = rdr.GetName(i);
                    if ((rdr[i] is DBNull))
                    {
                        if (row.ContainsKey(colName))
                        {
                            // do nothing.
                        }
                        else
                        {
                            // replace DBNull object with a null value.
                            row.Add(colName, new DatabaseObject(colTypes[colName], null));

                        }
                    }
                    else
                    {
                        if (row.ContainsKey(colName))
                        {
                            // override previous entry.
                            row[colName] = new DatabaseObject(colTypes[colName], rdr[i]);
                        }
                        else
                        {
                            row.Add(colName, new DatabaseObject(colTypes[colName], rdr[i]));
                        }
                    }
                }

                outputList.Add(row);
            }

            rdr.Close();
            return outputList;
            
        }

        private static Dictionary<string, Type> GetColumnDictionary(SqlDataReader rdr)
        {
            var columns = from col in rdr.GetColumnSchema()
                          select new { Name = col.ColumnName, Type = col.DataType };

            var outputDict = new Dictionary<string, Type>();
            foreach (var col in columns)
            {
                if (!outputDict.ContainsKey(col.Name))
                {
                    outputDict.Add(col.Name, col.Type);
                }
            }

            return outputDict;
        }
    }
}
