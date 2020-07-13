using DataObjectBaseExample.Data;
using DataObjectBaseExample.DataObjects;
using DataObjectBaseExample.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestDataObjectBase.InvalidObject;

namespace UnitTestDataObjectBase
{
    [TestClass]
    public class DataObjectBaseTests
    {
        public const string connString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MaaikeTrompLocal; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

        [TestMethod]
        public void TestPopulate()
        {
            // Arrange data.
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);

            // compound primary key.
            string sql = "SELECT pe.* FROM PageElement AS pe " +
                "LEFT JOIN Element AS e " +
                "ON e.Id=pe.ElementId " +
                "LEFT JOIN PAGE AS p " +
                "ON p.Id=pe.PageId " +
                "WHERE p.Name = 'TestPage' AND e.Name = 'TestElement'"; // test element and testpage.
            var objectData = db.PrepareAndExecuteQuery(commandText: sql).ToList().FirstOrDefault();

            sql = "SELECT * FROM TestObjectClass";
            var testData = db.PrepareAndExecuteQuery(commandText: sql).ToList();

            try
            {
                // act
                PageElement element = new PageElement(db, objectData);
                var testObjects = new List<TestObjectClass>();
                foreach (var item in testData)
                {
                    testObjects.Add(new TestObjectClass(db, item));
                }

            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestLeftJoinResults()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            string sql = @"SELECT * 
                        FROM TestObjectClass AS toc
                        LEFT JOIN Status AS s
                        ON s.Id = toc.Id
                        LEFT JOIN FormFieldInfo as ffi
                        ON ffi.FormId = toc.Id; ";

            try
            {
                db.PrepareAndExecuteQuery(commandText: sql).ToList();
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestPopulateById()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);

            try
            {
                TestObjectClass testClass = new TestObjectClass(db, 1);
                testClass = new TestObjectClass(db, 2);
                testClass = new TestObjectClass(db, 3);
                testClass = new TestObjectClass(db, 4);
                testClass = new TestObjectClass(db, 5);
                testClass = new TestObjectClass(db, 6);
                testClass = new TestObjectClass(db, 7);
                testClass = new TestObjectClass(db, 8);
                testClass = new TestObjectClass(db, 9);
                testClass = new TestObjectClass(db, 10);
                testClass = new TestObjectClass(db, 11);
                testClass = new TestObjectClass(db, 12);
                testClass = new TestObjectClass(db, 13);
                testClass = new TestObjectClass(db, 14);
                testClass = new TestObjectClass(db, 15);
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestUpdateObject()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            string sql = "SELECT Id FROM Element WHERE Name='TestElement'";
            var elementIds = new List<int>();
            
            using (var rdr = db.PrepareAndExecuteQuery(commandText: sql))
            {
                int i = 0;
                while (rdr.Read())
                {
                    elementIds.Add(rdr.GetInt32(i));
                }
            }

            sql = "SELECT Id FROM Page WHERE Name='TestPage'";
            var pageIds = new List<int>();
            using (var rdr2 = db.PrepareAndExecuteQuery(commandText: sql))
            {
                int i = 0;
                while (rdr2.Read())
                {   
                    pageIds.Add(rdr2.GetInt32(i));
                }
            }

            sql = $"SELECT * FROM PageElement WHERE PageId={pageIds.First()} AND ElementId={elementIds.First()}";
            var objectData = db.PrepareAndExecuteQuery(commandText: sql).ToList().FirstOrDefault();

            try
            {
                // toggle not used column in pageElement.
                PageElement pageElement = new PageElement(db, objectData);
                pageElement.NotUsed = !pageElement.NotUsed;
                pageElement.UpdateObject();
                pageElement.NotUsed = !pageElement.NotUsed;
                pageElement.UpdateObject();

                // update a char variable in testobject. 
                TestObjectClass testClass = new TestObjectClass(db, 1);
                var temp = testClass.CharVal;
                testClass.CharVal = 't';
                testClass.UpdateObject();
                testClass.CharVal = temp;
                testClass.UpdateObject();
            }   
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestUpdateProperty()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            var pageIds = new List<int>();
            var elementIds = new List<int>();
            
            string sql = "SELECT Id FROM Element WHERE Name='TestElement'";
            using (var rdr = db.PrepareAndExecuteQuery(commandText: sql))
            {
                int i = 0;
                while (rdr.Read())
                {
                    elementIds.Add(rdr.GetInt32(i));
                }
            }
            sql = "SELECT Id FROM Page WHERE Name='TestPage'";
            using (var rdr2 = db.PrepareAndExecuteQuery(commandText: sql))
            {
                int i = 0;
                while (rdr2.Read())
                {
                    pageIds.Add(rdr2.GetInt32(i));
                }
            }

            sql = $"SELECT * FROM PageElement WHERE PageId={pageIds.First()} AND ElementId={elementIds.First()}";
            var objectData = db.PrepareAndExecuteQuery(commandText: sql).ToList().FirstOrDefault();

            try
            {
                // toggle not used column in pageElement.
                PageElement pageElement = new PageElement(db, objectData, true);
                pageElement.NotUsed = !pageElement.NotUsed;
                pageElement.NotUsed = !pageElement.NotUsed;

                // update a char variable in testobject. 
                TestObjectClass testClass = new TestObjectClass(db, 1, true);
                var temp = testClass.CharVal;
                testClass.CharVal = 't';
                testClass.CharVal = temp;
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestUnsafeConversion()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            // formfield info testobject, validation fails.

            try
            {
                FormFieldInfo field = new FormFieldInfo(db, 1);
            }
            catch (ArgumentException e)
            {
                // expected behaviour
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestMissingProperty()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            try
            {
                Status status = new Status(db, 5);
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }


        [TestMethod] 
        public void TestPrimaryKeyAttributeMissing()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            Dictionary<string, DatabaseObject> objectData = new Dictionary<string, DatabaseObject>();
            objectData.Add("Id", new DatabaseObject(typeof(int), 1));
            objectData.Add("Name", new DatabaseObject(typeof(string), "some value"));
            ClassWithoutPrimaryKey testClass1 = new ClassWithoutPrimaryKey(db, objectData, true);

            try
            {
                testClass1.UpdateObject();

            }
            catch(InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }

            try
            {
                testClass1.Name = "hello world!";
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestDefaultOrIdActiveUpdate()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);

            try
            {
                var testObject = new TestDefaultAndIdColumns(db);
                testObject.Id = 1;
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }

            try
            {
                var testObject2 = new TestDefaultAndIdColumns(db);
                testObject2.Date = DateTime.Now;
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }

        }

        [TestMethod]
        public void TestPopulateByIdMultiplePrimaryKeys()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            try
            {
                var testObject = new TestDefaultAndIdColumns(db, 1);
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

        [TestMethod]
        public void TestPropNameNull()
        {
            DatabaseConnector db = new DatabaseConnector(DataObjectBaseTests.connString);
            try
            {
                var testObject = new TestDefaultAndIdColumns(db);
                testObject.AnotherTest = "hello world";
            }
            catch (InvalidOperationException e)
            {
                // expected behaviour.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}. \nException: {e}\nStackTrace: {e.StackTrace}");
            }
        }

            // try populate by id with multiple primary keys. (no db needed)
            // propname is null or empty (no db needed).
    }
}
