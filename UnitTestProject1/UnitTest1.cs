using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1;

namespace OMSTest
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void GetUserInformationTest()
        {
            int id = 2;
            var result = UserService.GetUserInformation(id);

            Assert.IsTrue(result.FullName != null);
        }

        [TestMethod]
        public void GetUserInformationDapperTest()
        {
            int id = 2;
            var result = UserService.GetUserInformationDapper(id);

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

        [TestMethod]
        public void GetTotalProductByCatalogTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 1, 25);
            string productName = "Avocado 10kg Box";
            var result = DashboardService.GetTotalProductByCatalog(fromDate, toDate, productName);

            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetMonthlyCancelledDeliveriedReturnTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24, 0, 0, 0);
            DateTime toDate = new DateTime(2023, 1, 24, 0, 0, 0);
            var result = DashboardService.GetMonthlyCancelledDeliveriedReturn(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetDateToDateSalesAnalyticsByCountryTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            var result = DashboardService.GetDateToDateSalesAnalyticsByCountry(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            string oldPassword = "123456";
            string newPassword = "12345678";
            User user = UserService.GetUserInformationById(3);
            var result = UserService.UpdatePassword(user, oldPassword, newPassword);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void GetOrderDetailByIdTest()
        {
            int id = 1;
            var result = OrderService.GetOrderDetailById(id);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateReturnOrderTest()
        {
            string updateStatus = "CANCEL";
            User user = UserService.GetUserInformationById(2);
            int returnId = 17;

            var result = OrderService.UpdateReturnOrder(returnId, updateStatus, user);

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetProductsByChannelTest()
        {
            User user = UserService.GetUserInformationById(2);
            if (user.Role == "MANAGER")
            {
                int channelId = 3;
                //string productStatus = "ACTIVE";
                var result = ProductService.GetProductsByChannel(channelId);
                Assert.IsTrue(result.Count > 0);
            }
        }
        [TestMethod]
        public void UpdateProductStockByChannelTest()
        {
            int channelId = 3;
            int productId = 3;
            int updateStockQuantity = 800;

            var result = ProductService.UpdateProductStockByChannel(channelId, productId, updateStockQuantity);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void InsertProductTest()
        {
            User user = UserService.GetUserInformationById(20);
            if (user.Role == "MANAGER")
            {
                Product product = new Product
                {
                    ProductMainImg = "https://source.unsplash.com/random",
                    ProductGallery = "https://source.unsplash.com/random",
                    ProductName = "Banana 3kg Box",// not null
                    SKU = "B-BN3B-297599",// not null
                    Barcode = "#bnn3kbo",// not null
                    Price = 10, // not null
                    Cost = 5, // not null
                    Height = 0, // not null
                    Width = 0, // not null
                    Length = 10,// not null
                    Weight = 10, // not null
                    ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    CreatedAt = DateTime.Now,
                    ProviderId = 1,
                };
                var result = ProductService.InsertNewProduct(product);
                Assert.AreEqual(result, 1);
            }
        }

        [TestMethod]
        public void InsertProductChannelStockTest()
        {
            User user = UserService.GetUserInformationById(2);
            if (user.Role == "MANAGER")
            {
                int productId = 10;
                int channelId = 1;
                int updateStock = 500;
                var result = ProductService.InsertProductChannelStock(productId, channelId, updateStock);

                Assert.AreEqual(result, 1);
            }

        }

        [TestMethod]
        public void GetDateToDateRatingByChannelTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 1;

            var result = CustomerService.GetDateToDateRatingByChannel(fromDate, toDate, channelId);
        }
    }
}
