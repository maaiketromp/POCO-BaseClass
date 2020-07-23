using DataObjectBaseLibrary.Data;
using DataObjectBaseLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataObjectBaseClassTest.Mocks
{
    class QueryResultTestClassMock : IDatabaseConnectorWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<Dictionary<string, DatabaseObject>> GetResultAsDictionary(string commandText, SqlParameter[] parameters = null)
        {
            throw new NotImplementedException();
        }

        public int PrepareAndExecuteNonQuery(string commandText, CommandType type = CommandType.Text, SqlParameter[] parameters = null)
        {
            throw new NotImplementedException();
        }

        public SqlDataReader PrepareAndExecuteQuery(string commandText, CommandType commandType = CommandType.Text, SqlParameter[] parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
