using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using OMSTest;

namespace UnitTestProject1
{
    public static class OrderService
    {
        internal static List<Order> GeteAllOrder(string fromDate, string toDate, int? channelId = null, string orderStatus = "")
        {
            var reader = SqlHelper.ExecuteReader(SqlQueryHelper.GetAllOrderQuery(fromDate, toDate, channelId, orderStatus), CommandType.Text);
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
                    ShippingProvider = Convert.ToString(reader[5]),
                    OrderStatus = Convert.ToString(reader[6]),
                });
            }
            return result;
        }



        internal static OrderDetail GetOrderDetail(int? id)
        {

            if (id != null && id > 0)
            {
                var sql = SqlQueryHelper.GetOrderDetailQuery(id);

                var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
                var result = new OrderDetail();
                while (reader.Read())
                {
                    result = new OrderDetail()
                    {
                        ProductId = Convert.ToInt32(reader[0]),
                        ProductName = Convert.ToString(reader[1]),
                        ProductVariantImage = (byte[])reader[2],
                        ProductVariantValue = Convert.ToString(reader[3]),
                        ProductDescription = Convert.ToString(reader[4]),
                        CSKU = Convert.ToString(reader[5]),
                        Quantity = Convert.ToInt32(reader[6]),
                        PerProductPrice = Convert.ToInt32(reader[7]),
                        TotalPerProductPrice = Convert.ToInt32(reader[8]),
                        RecipientName = Convert.ToString(reader[9]),
                        RecipientPhoneNumber = Convert.ToString(reader[10]),
                        ShippingAddress = Convert.ToString(reader[11])
                    };
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

        internal static List<OrderDetail> GetOrderDetailDapper(int id)
        {
            var sql = SqlQueryHelper.GetOrderDetailQuery(id);
            return SqlHelper.Query<OrderDetail>(sql);
        }

        internal static List<Order> GetAllOrderDapper(string fromDate, string toDate, int? channelId = null, string orderStatus = "")
        {
            var sql = SqlQueryHelper.GetAllOrderQuery(fromDate, toDate, channelId, orderStatus);

            return SqlHelper.Query<Order>(sql);
        }

        internal static int UpdateReturnOrderDapper(int returnId, string updateStatus, User user)
        {
            if (returnId != 0 && (updateStatus == "APPROVE" || updateStatus == "CANCEL"))
            {
                string updatedStatus = updateStatus == "APPROVE" ? "APPROVED" : "CANCELLED";

                var sql = SqlQueryHelper.UpdateReturnOrderQuery(returnId, user, updatedStatus);
                return SqlHelper.Excecute(sql);
            }
            else
            {
                return 0;
            }

        }
    }
}
