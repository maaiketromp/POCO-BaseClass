namespace DataObjectBaseLibrary.DataObjects.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DataObjectBaseLibraryTests.Data;
    using DataObjectBaseLibrary.Interfaces;

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

            IResultRow mockTestClassData = MockData.GetTestClassMockData();
            IResultRow mockResultNulls = MockData.GetTestClassNullsMockData();
            IResultRow mockOrderDetailsData = MockData.GetOrderDetailsMockData();
            IResultRow mockOrderDetailsDataNulls = MockData.GetOrderDetailsNullsMockData();

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
                if (test.Id == 0 || test3.ProductId == 0)
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
        public void UpdateObjectTest()
        {
            IDatabaseConnectorWrapper mockWrapper = new MockDatabaseConnectorWrapperColumnData();
            Order testOrder = new Order(mockWrapper)
            {
                Id = new Random().Next(),
                CustomerId = new Random().Next(),
                EmployeeId = new Random().Next(),
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
    }
}

