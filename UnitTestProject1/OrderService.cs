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
            var reader = SqlHelper.ExecuteReader(SqlHelper._connectionString, SqlQueryHelper.GetDateToDateAllOrderQuery(fromDate, toDate, channelId, orderStatus), CommandType.Text);
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
    }
}
