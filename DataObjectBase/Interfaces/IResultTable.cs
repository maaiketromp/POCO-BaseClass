
namespace DataObjectBaseLibrary.Interfaces
{
    using System.Collections.Generic;
    using DataObjectBaseLibrary.Data;
    using DataObjectBaseLibrary.Helpers;
    

    public interface IResultTable : ICollection<ResultRow>
    {
        public ColumnInfo[] ColumnInfo { get; }
    }
}
