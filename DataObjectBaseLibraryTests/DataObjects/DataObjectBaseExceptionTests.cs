namespace DataObjectBaseLibraryTests.DataObjects.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibrary.Interfaces;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.DataObjects;

    [TestClass()]
    class DataObjectBaseExceptionTests
    {
        [TestMethod()]
        internal void TryPopulateNonExistingProperty()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            IResultRow mockData = MockData.GetExtraPropMockData();
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
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        internal void TryPopulateWithWrongDataType()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();

            IResultRow mockData = MockData.GetPropertyWithWrongDataTypeMockData();

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
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        internal void TryPopulateByIdWithoutPK()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            int testVal = 1;
            bool caught = false;

            try
            {
                DataObjectWithoutIdProperty objectWithoutPK = new DataObjectWithoutIdProperty(mockWrapper, testVal);
            }
            catch (InvalidOperationException)
            {
                caught = true;
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        internal void TryPopulateByIdWithMultiplePKs()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            int testVal = 1;
            bool caught = false;

            try
            {
                DataObjectWithTwoIdProperties objectWithCompoundId = new DataObjectWithTwoIdProperties(mockWrapper, testVal);
            }
            catch (InvalidOperationException)
            {
                caught = true;
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }
    }
}