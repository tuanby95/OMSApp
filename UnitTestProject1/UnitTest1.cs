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
    }
}
