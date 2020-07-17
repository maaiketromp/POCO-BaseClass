using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjectBaseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataObjectBaseLibrary.Data.Tests
{
    [TestClass()]
    public class QueryResultWrapperTests
    {
        const string connString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MaaikeTrompLocal; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

        [TestMethod()]
        public void GetResultTest()
        {
            DatabaseConnector db = new DatabaseConnector(connString);
            QueryResultWrapper wrapper = new QueryResultWrapper(db);
            string sql = "SELECT * FROM TestObjectClass WHERE Id < 12";
            var result = wrapper.GetResult(sql);
        }
    }
}