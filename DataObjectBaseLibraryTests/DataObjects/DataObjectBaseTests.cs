namespace DataObjectBaseLibrary.DataObjects.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.Interfaces;
    using System.Net.NetworkInformation;

    [TestClass()]
    public class DataObjectBaseTests
    {

        [TestMethod()]
        public void DataObjectBaseTest()
        {
            // Test BaseClass constructor without populating.
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            
            try
            {
                TestClass test = new TestClass(mockWrapper);
                OrderDetails details = new OrderDetails(mockWrapper);
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
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
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapper();
            int id = 1;
            try
            {
                TestClass test = new TestClass(mockWrapper, id);
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }
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

                if (test2.IntVal != 0 || test2.NIntVal != null || test4.Quantity != 0)
                {
                    Assert.Fail("Object did not populate.");
                }
                if (test.Id != 1 || test3.ProductId != 1)
                {
                    Assert.Fail("Object did not populate.");
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
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

        [TestMethod()]
        public void UpdateObjectTest()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapperColumnResult();
            Order testOrder = new Order(mockWrapper)
            {
                Id = 1,
                CustomerId = 2,
                EmployeeId = 3,
                ShippingDate = DateTime.Now,
                TimeOfOrder = DateTime.UtcNow,
            };

            try
            {
                testOrder.UpdateObject();
            }
            catch (Exception e)
            {
                Assert.Fail($"Exception {e} \nMessage: {e.Message}\nStacktrace: {e.StackTrace}");
            }

        }

        /*TestClass testClass = new TestClass(mockWrapper)
            {
                Id = 1,
                BoolVal = false,
                NBoolVal = false,
                IntVal = 1,
                NIntVal = 1,
                FloatVal = 1,
                NFloatVal = 1,
                DoubleVal = 1,
                NDoubleVal = 1,
                DecVal = 1,
                NDecVal = 1,
                CharVal = '1',
                NCharVal = '1',
                StringVal = "1",
                DateTimeVal = DateTime.Now,
                NDateTimeVal = DateTime.Now,
            };

            OrderDetails testOrderDetails = new OrderDetails(mockWrapper)
            {
                OrderId = 1,
                ProductId = 1,
                Quantity = 1,
            };*/
    }
}

