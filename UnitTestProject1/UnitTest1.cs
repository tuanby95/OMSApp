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
        public void GetAllOrderTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 2;
            string orderStatus = string.Format("COMPLETED");

            var result = OrderService.GeteAllOrder(fromDate.ToString("yyyy'-'MM'-'dd"), toDate.ToString("yyyy'-'MM'-'dd"), channelId, orderStatus);

            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetAllOrderDapperTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 2;
            string orderStatus = string.Format("COMPLETED");

            var result = OrderService.GetAllOrderDapper(fromDate.ToString("yyyy'-'MM'-'dd"), toDate.ToString("yyyy'-'MM'-'dd"), channelId, orderStatus);

            Assert.IsTrue(result.Count > 0);

        }

        [TestMethod]
        public void GetTotalProductTest()
        {
            string filterFormat = "MONTH";
            string productName = "Avo";
            var result = DashboardService.GetTotalProduct(filterFormat, productName);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetTotalProductDapperTest()
        {
            string filterFormat = "MONTH";
            string productName = "Avo";
            var result = DashboardService.GetTotalProductDapper(filterFormat, productName);

            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetCancelledDeliveriedReturnTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24, 0, 0, 0);
            DateTime toDate = new DateTime(2023, 1, 24, 0, 0, 0);
            var result = DashboardService.GetCancelledDeliveriedReturn(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetCancelledDeliveriedReturnDapperTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24, 0, 0, 0);
            DateTime toDate = new DateTime(2023, 1, 24, 0, 0, 0);
            var result = DashboardService.GetCancelledDeliveriedReturnDapper(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }
        [TestMethod]
        public void GetSalesAnalyticsTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            var result = DashboardService.GetSalesAnalytics(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetsAnalyticsDapperTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            var result = DashboardService.GetSalesAnalyticsDapper(fromDate, toDate);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void UpdatePasswordTest()
        {
            string oldPassword = "123456";
            string newPassword = "12345678";
            User user = UserService.GetUserInformation(3);
            var result = UserService.UpdatePassword(user, oldPassword, newPassword);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void UpdatePasswordDapperTest()
        {
            string oldPassword = "123456";
            string newPassword = "12345678";
            User user = UserService.GetUserInformation(1);
            var result = UserService.UpdatePasswordDapper(user, oldPassword, newPassword);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void GetOrderDetailTest()
        {
            int id = 1;
            var result = OrderService.GetOrderDetail(id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOrderDetailDapperTest()
        {
            int id = 1;
            var result = OrderService.GetOrderDetailDapper(id);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void UpdateReturnOrderTest()
        {
            string updateStatus = "CANCEL";
            User user = UserService.GetUserInformation(1);
            int returnId = 17;

            var result = OrderService.UpdateReturnOrder(returnId, updateStatus, user);

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void UpdateReturnOrderDapperTest()
        {
            string updateStatus = "APPROVE";
            User user = UserService.GetUserInformation(2);
            int returnId = 16;

            var result = OrderService.UpdateReturnOrderDapper(returnId, updateStatus, user);

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void GetProductsTest()
        {
            User user = UserService.GetUserInformation(2);
            if (user.Role == "MANAGER")
            {
                int channelId = 3;
                //string productStatus = "ACTIVE";
                var result = ProductService.GetProducts(channelId);
                Assert.IsTrue(result.Count > 0);
            }
        }
        [TestMethod]
        public void GetProductsDapperTest()
        {
            User user = UserService.GetUserInformation(2);
            if (user.Role == "MANAGER")
            {
                int channelId = 3;
                //string productStatus = "ACTIVE";
                var result = ProductService.GetProductsDapper(channelId);
                Assert.IsTrue(result.Count > 0);
            }
        }
        [TestMethod]
        public void UpdateProductStockTest()
        {
            int channelId = 3;
            int productVariantId = 3;
            int warehouseId = 3;
            int updateStockQuantity = 800;

            var result = ProductService.UpdateProductStock(channelId, productVariantId, warehouseId, updateStockQuantity);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void UpdateProductStockDapperTest()
        {
            int channelId = 3;
            int productVariantId = 3;
            int warehouseId = 3;
            int updateStockQuantity = 800;

            var result = ProductService.UpdateProductStockDapper(channelId, productVariantId, warehouseId, updateStockQuantity);

            Assert.AreEqual(result, 1);
        }
        [TestMethod]
        public void InsertProductTest()
        {
            User user = UserService.GetUserInformation(20);
            if (user.Role == "MANAGER")
            {
                //Product product = new Product
                //{
                //    ProductMainImg = "https://source.unsplash.com/random",
                //    ProductGallery = "https://source.unsplash.com/random",
                //    ProductName = "Banana 3kg Box",// not null
                //    SKU = "B-BN3B-297599",// not null
                //    Barcode = "#bnn3kbo",// not null
                //    Price = 10, // not null
                //    Cost = 5, // not null
                //    Height = 0, // not null
                //    Width = 0, // not null
                //    Length = 10,// not null
                //    Weight = 10, // not null
                //    ProductDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                //    CreatedAt = DateTime.Now,
                //    ProviderId = 1,
                //};
                //var result = ProductService.InsertNewProduct(product);
                //Assert.AreEqual(result, 1);
            }
        }

        [TestMethod]
        public void InsertProductChannelStockTest()
        {
            User user = UserService.GetUserInformation(2);
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
        public void GetRatingTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 1;

            var result = CustomerService.GetRating(fromDate, toDate, channelId);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetRatingDapperTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 24);
            DateTime toDate = new DateTime(2023, 2, 24);
            int channelId = 1;

            var result = CustomerService.GetRatingDapper(fromDate, toDate, channelId);
            
            Assert.IsTrue(result.Count > 0);
        }
    }
}
