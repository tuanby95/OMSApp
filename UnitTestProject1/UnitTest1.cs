using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1;
using Dapper;
using Dapper.Contrib;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace OMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetNotSellingProductByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 25);
            DateTime toDate = new DateTime(2023, 01, 26);
            var result = DashboardService.GetTotalNotSellingProductsByDate(fromDate, toDate);
            Assert.IsTrue(result > 3);
        }

        [TestMethod]
        public void GetTotalSalesByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 04, 24);

            var result = DashboardService.GetTotalSalesByDate(fromDate, toDate);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetOutOfStockProductsTest()
        {
            var result = DashboardService.GetTotalOutOfStockProducts();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalNearlyOutOfStockProductTest()
        {
            var result = DashboardService.GetTotalNearlyOutOfStockProdudcts();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalOrdersOnChannelByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 01, 30);

            var result = DashboardService.GetTotalOrdersOnChannelByDate(fromDate, toDate);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalIssueOrdersByByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 01, 24);

            long result = DashboardService.GetTotalIssueOrdersByDate(fromDate, toDate);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetSellingProductTopThreeTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 1, 30);

            var result = DashboardService.GetSellingProductTopThree(fromDate, toDate);
            Assert.AreEqual(result = 3, 3);
        }

        [TestMethod]
        public void GetTotalProductByAmountTest()
        {
            var checkingChannel = "2";
            var result = DashboardService.GetTotalProductByAmount(checkingChannel);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalChannelsByStatusTest()
        {
            var result = DashboardService.GetTotalChannelsByStatus();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalInactiveProductOnChannelTest()
        {
            string checkingChannel = "2";
            var result = DashboardService.GetTotalInactiveProductOnChannel(checkingChannel);
            Assert.IsNotNull(result);
        }
    }
}
