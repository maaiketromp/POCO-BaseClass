
namespace DataObjectBaseLibrary.Interfaces
{
    using System;
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Helpers;
    
    public interface IResultTable : ICollection<ResultRow>
    {
        public string GetColumnName(int i);

        public Type GetColumnType(int i);

        public ResultRow this[int index] { get; set; }


    }
}
