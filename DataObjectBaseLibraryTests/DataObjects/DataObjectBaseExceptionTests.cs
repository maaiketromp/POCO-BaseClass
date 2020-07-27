namespace DataObjectBaseLibraryTests.DataObjects
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibrary.Interfaces;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.DataObjects;

    [TestClass]
    class DataObjectBaseExceptionTests
    {
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
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
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
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

            if (!caught)
            {
                Assert.Fail("Test is expected to throw exception but does not.");
            }
        }

        [TestMethod()]
        public void TryPopulateByIdWithoutPK()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            int testVal = 1;
            bool caught = false;

            try
            {
                MockClassWithoutIdProperty objectWithoutPK = new MockClassWithoutIdProperty(mockWrapper, testVal);
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
        public void TryPopulateByIdWithMultiplePKs()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            int testVal = 1;
            bool caught = false;

            try
            {
                MockClassWithTwoIdProperties objectWithCompoundId = new MockClassWithTwoIdProperties(mockWrapper, testVal);
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