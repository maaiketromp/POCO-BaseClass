using DataObjectBaseLibrary.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseClassTest.Mocks
{
    class QueryResultTestClassMock : IQueryResultWrapper
    {
        public IResultTable GetResult(string commandText, SqlParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
