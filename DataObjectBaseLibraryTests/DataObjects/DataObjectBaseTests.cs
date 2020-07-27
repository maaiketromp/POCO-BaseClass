namespace DataObjectBaseLibrary.DataObjects.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.Interfaces;

    [TestClass()]
    public class DataObjectBaseTests
    {
        public object MockDatabaseConnectorWrapper { get; private set; }

        [TestMethod()]
        public void DataObjectBaseTest()
        {
            // Test BaseClass constructor without populating.
            // Arrange
            MockDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            
            try
            {
                TestClass test = new TestClass(mockWrapper);
                OrderDetails details = new OrderDetails(mockWrapper);
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}\n Stacktrace: {e.StackTrace}");
            }
        }

        [TestMethod()]
        public void DataObjectBaseTest1()
        {
            // to be depricated: List<Dictionary<string, DatabaseObject>> 
            Assert.Fail();
        }

        [TestMethod()]
        public void DataObjectBaseTest2()
        {
            // TODO: Fix Populate by id.
            Assert.Fail();
        }

        [TestMethod()]
        public void DataObjectBasePopulateTest()
        {
            // Arrange
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();

            IResultRow mockTestClassData = new MockTestClassData();
            IResultRow mockResultNulls = new MockTestClassDataNulls();
            IResultRow mockOrderDetailsData = new MockOrderDetailsData();
            IResultRow mockOrderDetailsDataNulls = new MockOrderDetailsDataNulls();

            try
            {
                TestClass test = new TestClass(mockWrapper, mockTestClassData);
                TestClass test2 = new TestClass(mockWrapper, mockResultNulls);
                OrderDetails test3 = new OrderDetails(mockWrapper, mockOrderDetailsData);
                OrderDetails test4 = new OrderDetails(mockWrapper, mockOrderDetailsDataNulls);
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}\n Stacktrace: {e.StackTrace}");
            }
        }

        [TestMethod()]
        public void TryPopulateNonExistingProperty()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            IResultRow mockData = new MockTestClassDataExtraProp();
            bool caught = false;

            try
            {
                TestClass test = new TestClass(mockWrapper, mockData);
            }
            catch (InvalidOperationException)
            {
                caught = true;
                 // expected behaviour, test runs correctly.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}\n Stacktrace: {e.StackTrace}");
            }
            
            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        public void TryPopulateWithWrongDataType()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            IResultRow mockData = new MockPropertyWithWrongDataType();

            bool caught = false;

            try
            {
                TestClass test = new TestClass(mockWrapper, mockData);
            }
            catch (ArgumentException)
            {
                caught = true;
                // expected behaviour, test runs correctly.
            }
            catch (Exception e)
            {
                Assert.Fail($"Message: {e.Message}\n Stacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        public void UpdateObjectTest()
        {
            Assert.Fail();
        }
    }
}