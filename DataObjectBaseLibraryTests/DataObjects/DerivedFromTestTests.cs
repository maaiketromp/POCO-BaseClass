using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjectBaseLibraryTests.Data;
using DataObjectBaseLibrary.Interfaces;

namespace DataObjectBaseLibrary.DataObjects.Tests
{
    [TestClass()]
    public class DerivedFromTestTests
    {
        [TestMethod()]
        public void DerivedFromTestTest()
        {
            MockDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            DerivedFromTest testObject = new DerivedFromTest(mockWrapper);
        }

        [TestMethod()]
        public void DerivedFromTestTest1()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();

            try
            {
                DerivedFromTest testObject = new DerivedFromTest(mockWrapper, 1);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void DerivedFromTestTest2()
        {
            MockDatabaseConnectorWrapperColumnData mockWrapper = new MockDatabaseConnectorWrapperColumnData();
            var mockData = MockData.GetDerivedFromTestClassMockData();
            DerivedFromTest testObject = new DerivedFromTest(mockWrapper, mockData)
            {
                AnotherStringVal = "A new TestString!"
            };
            testObject.UpdateObject();
        }
    }
}