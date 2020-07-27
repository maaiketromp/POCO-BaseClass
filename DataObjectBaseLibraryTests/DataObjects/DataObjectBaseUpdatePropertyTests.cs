namespace DataObjectBaseLibraryTests.DataObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibrary.Interfaces;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.DataObjects;

    [TestClass]
    public class DataObjectBaseUpdatePropertyTests
    {
        [TestMethod()]
        public void UpdatePropertyMethodTest()
        {
            // arrange
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            bool activeUpdate = true;
            TestClass test = new TestClass(mockWrapper, activeUpdate);

            try
            {
                test.IntVal = 1;
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }
        }

        [TestMethod()]
        public void UpdateIdPropertyTest()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            bool activeUpdate = true;
            var test = new MockClassUpdatePropertyWithDefaultAndIdProps(mockWrapper, activeUpdate);
            bool caught = false;

            try
            {
                test.Id = 1;
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
        public void UpdateDefaultPropertyTest()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            bool activeUpdate = true;
            var test = new MockClassUpdatePropertyWithDefaultAndIdProps(mockWrapper, activeUpdate);
            bool caught = false;

            try
            {
                test.Name = "test";
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
        public void UpdatePropertyWithoutPropertyNameTest()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            bool activeUpdate = true;
            var test = new MockClassUpdatePropertyWithDefaultAndIdProps(mockWrapper, activeUpdate);
            bool caught = false;

            try
            {
                test.Address = "test";
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
