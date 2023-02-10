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
        public void GetSaleListTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetSaleList(fromDate, toDate);

            Assert.IsNotNull(result.Count >= 0);
        }

        [TestMethod]
        public void GetTotalSalesTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetTotalSales(fromDate, toDate);

            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetTotalSalesReturnTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetTotalSalesReturn(fromDate, toDate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalSalesOverviewTest()
        {
            var result = DashboardService.GetTotalSalesOverview();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalSalesOverviewChartTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetTotalSalesOverviewChart(fromDate, toDate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAvgOrderOverviewTest()
        {
            var result = DashboardService.GetAvgOrderOverview();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAvgOrderOverviewChartTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetAvgOrderOverviewChart(fromDate, toDate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalReturnOverviewTest()
        {
            var result = DashboardService.GetTotalReturnOverview();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalReturnOverviewChartTest()
        {
            String fromDate = "2023-01-01";
            String toDate = "2023-12-31";
            var result = DashboardService.GetTotalReturnOverviewChart(fromDate, toDate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSaleOnChannelChartTest()
        {
            var result = DashboardService.GetSaleOnChannelChart();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetSaleByLocationOverviewTest()
        {
            var result = DashboardService.GetSaleByLocationOverview();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void InsertNewUserTest()
        {
            User user = new User();
            var result = UserService.InsertNewUser(user);

            Assert.IsNotNull(result);
        }
    }
}
