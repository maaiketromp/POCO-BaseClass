using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjectBaseLibrary.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using DataObjectBaseLibraryTests.Data;

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
            // populate by id. TODO.
            Assert.Fail();
        }

        [TestMethod()]
        public void DerivedFromTestTest2()
        {
            MockDatabaseConnectorWrapperDerivedClass mockWrapper = new MockDatabaseConnectorWrapperDerivedClass();
            MockDerivedFromTestData mockData = new MockDerivedFromTestData();
            DerivedFromTest testObject = new DerivedFromTest(mockWrapper, mockData.GetMockData());

            testObject.AnotherStringVal = "A new TestString!";
            testObject.UpdateObject();
        }
    }
}