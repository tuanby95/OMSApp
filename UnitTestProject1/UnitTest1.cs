using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using UnitTestProject1;
//using GraphQL;
//using GraphQL.Types;
//using GraphQL.SystemTextJson; // First add PackageReference to GraphQL.SystemTextJson

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

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalSalesTest()
        {
            String fromDate = "GETDATE()";
            String toDate = "GETDATE()";
            int backNumber = -7;
            var result = DashboardService.GetTotalSales(fromDate, toDate, backNumber);

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
            UserInfo user = new UserInfo()
            {
                FullName = "Phạm Lê Nhật Tiến Test",
                PhoneNumber = "0932350000",
                DOB = new DateTime(1997, 03, 07),
                Gender = "Male",
                Email = "tienplnps15456@fpt.edu.vn",
                UserRole = "MANAGER",
                FullAddress = "???",
                UserStatus = "ACTIVE",
                Facebook = String.Empty,
                Instagram = String.Empty,
                UserName = "tienbotay1997",
                UserPassword = "123456789"
            };
            var result = UserService.InsertNewUser(user);

            Assert.IsNotNull(result);
        }
    }
}
