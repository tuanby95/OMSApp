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
            DateTime fromDate = new DateTime(2023, 01, 25);
            DateTime toDate = new DateTime(2023, 01, 26);
            var result = DashboardService.GetTotalNotSellingProductsByDate(fromDate, toDate);
            Assert.IsTrue(result > 3);
        }
    }
}
