
namespace DataObjectBaseLibrary.Interfaces
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseLibrary.Data;

    public interface IDatabaseConnectorWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters);

        public List<Dictionary<string, DatabaseObject>> GetResultAsDictionary(
            string commandText, 
            SqlParameter[] parameters = null);

        public int PrepareAndExecuteNonQuery(
            string commandText,
            CommandType type = CommandType.Text,
            SqlParameter[] parameters = null);

        public SqlDataReader PrepareAndExecuteQuery(
            string commandText,
            CommandType commandType = CommandType.Text,
            SqlParameter[] parameters = null);
    }
}
