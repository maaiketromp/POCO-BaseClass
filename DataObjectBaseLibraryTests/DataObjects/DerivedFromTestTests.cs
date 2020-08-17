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
            try
            {
                DerivedFromTest testObject = new DerivedFromTest(mockWrapper);
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }
        }

        [TestMethod()]
        public void DerivedFromTestTest1()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();

            try
            {
                DerivedFromTest testObject = new DerivedFromTest(mockWrapper, 1);
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
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

            try
            {
                testObject.UpdateObject();
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }
        }
    }
}