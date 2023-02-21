using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1;
using Dapper;
using Dapper.Contrib;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GreenDonut;

namespace OMSTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetSellingProductTopThreeTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 1, 30);

            var result = DashboardService.GetSellingProductTopThree(fromDate, toDate);
            Assert.AreEqual(result = 3, 3);
        }

        #region Dapper

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

        [TestMethod]
        public void GetTotalOrdersByDateTest()
        {
            string fromDate = "2023-01-24";
            string toDate = "2023-02-20";
            var result = DashboardService.GetTotalOrderByDate(fromDate, toDate);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalOrdersOnChannelsTest()
        {
            var fromDate = new DateTime(2023, 01, 24);
            var toDate = new DateTime(2023, 02, 20);
            var result = ChannelService.GetTotalOrdersOnChannel(fromDate, toDate);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalNotSellingProductsTest()
        {
            var toDate = DateTime.Today;
            var checkingChannel = "2";
            var result = DashboardService.GetTotalNotSellingProducts(toDate, checkingChannel);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalReturnOrdersTest()
        {
            var filterFormat = "DAY";
            var filterRange = 7;
            var toDate = DateTime.Today;
        }
        #endregion
    }
}
