using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSTest;

namespace UnitTestProject1
{
    public static class OrderService
    {
        internal static List<Order> GetDateToDateAllOrder(string fromDate, string toDate, int? channelId = null, string orderStatus = "")
        {
            var reader = SqlHelper.ExecuteReader(SqlQueryHelper.GetDateToDateAllOrderQuery(fromDate, toDate, channelId, orderStatus), CommandType.Text);
            var result = new List<Order>();
            while (reader.Read())
            {
                result.Add(new Order
                {
                    OrderId = Int32.Parse(reader[0] + ""),
                    OrderDate = DateTime.Parse(reader[1] + ""),
                    ChannelName = Convert.ToString(reader[2]),
                    ProductUnit = Int32.Parse(reader[3] + ""),
                    TotalPrice = Int32.Parse(reader[4] + ""),
                    ShipmentProvider = Convert.ToString(reader[5]),
                    OrderStatus = Convert.ToString(reader[6]),
                });
            }
            return result;
        }
        internal static List<OrderDetail> GetOrderDetailById(int? id)
        {

            if (id != null && id > 0)
            {
                var sql = SqlQueryHelper.GetOrderDetailByIdQuery(id);

                var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                var result = new List<OrderDetail>();
                while (reader.Read())
                {
                    result.Add(new OrderDetail
                    {
                        ProductMainImg = Convert.ToString(reader[0]),
                        ProductName = Convert.ToString(reader[1]),
                        SKU = Convert.ToString(reader[2]),
                        Barcode = Convert.ToString(reader[3]),
                        Quantity = Convert.ToInt32(reader[4]),
                        Price = Convert.ToInt32(reader[5]),
                        TotalPerProductPrice = Convert.ToInt32(reader[6]),
                        RecipientName = Convert.ToString(reader[7]),
                        RecipientPhoneNumber = Convert.ToString(reader[8]),
                        ShippingAddress = Convert.ToString(reader[9])
                    });
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        internal static int UpdateReturnOrder(int returnId, string updateStatus, User user)
        {

            if (returnId != 0 && (updateStatus == "APPROVE" || updateStatus == "CANCEL"))
            {
                string updatedStatus = updateStatus == "APPROVE" ? "APPROVED" : "CANCELLED";

                var sql = SqlQueryHelper.UpdateReturnOrderQuery(returnId, user, updatedStatus);

                var result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text);
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
