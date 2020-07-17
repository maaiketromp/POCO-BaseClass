
namespace DataObjectBaseLibrary.Interfaces
{
    using Microsoft.Data.SqlClient;

    public interface IQueryResultWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters);
    }
}
