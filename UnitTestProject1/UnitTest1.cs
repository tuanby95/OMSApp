using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1;

namespace OMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetNotSellingProductByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 03, 24);
            var result = DashboardService.GetTotalNotSellingProductsByDate(fromDate, toDate);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetUserInformationByIdTest()
        {
            int id = 2;
            var result = UserService.GetUserInformationById(id);

            Assert.IsTrue(result.FullName != null);
        }

        [TestMethod]
        public void GetDateToDateAllOrderTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 2;
            string orderStatus = string.Format("COMPLETED");

            var result = OrderService.GetDateToDateAllOrder(fromDate.ToString("yyyy'-'MM'-'dd"), toDate.ToString("yyyy'-'MM'-'dd"), channelId, orderStatus);

            Assert.IsTrue(result.Count > 0);

            var resultWithoutChannelId = OrderService.GetDateToDateAllOrder(fromDate.ToString("yyyy'-'MM'-'dd"), toDate.ToString("yyyy'-'MM'-'dd"), null, orderStatus);

            Assert.IsTrue(resultWithoutChannelId.Count > 0);

            var resultWithoutOrderStatus = OrderService.GetDateToDateAllOrder(fromDate.ToString("yyyy'-'MM'-'dd"), toDate.ToString("yyyy'-'MM'-'dd"), channelId, null);

            Assert.IsTrue(resultWithoutOrderStatus.Count > 0);
        }
    }
}
