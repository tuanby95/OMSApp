using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class SqlQueryHelper
    {
        private static string _query = "default";


        #region DashboardService Query
        internal static string GetTotalNotSellingProductsByDateQuery(DateTime fromDate, DateTime toDate)
        {
            _query = String.Format(@"
                                    SELECT 
                                      COUNT(pdt.Id) AS Total 
                                    FROM 
                                      Product pdt 
                                    WHERE 
                                      pdt.Id NOT IN (
                                        SELECT 
                                          DISTINCT ordl.ProductId 
                                        FROM 
                                          OrderList ord 
                                          JOIN OrderDetail ordl ON ord.Id = ordl.OrderId 
                                        WHERE 
                                          ord.OrderedAt BETWEEN '2023-01-24' 
                                          AND DATEADD(SECOND, -1, '2023-03-30')
                                      )");
            return _query;
        }


        #endregion

        #region ProductService Query
        #endregion

        #region ChannelService Query
        #endregion

        #region CustomerService Query
        #endregion

        #region InventoryService Query
        #endregion

        #region InventoryService Query
        #endregion

        #region PaymentService Query
        #endregion

        #region UserService Query
        internal static string GetUserInformationByIdQuery(int id)
        {
            _query = string.Format(@"
                SELECT *
                FROM UserList
                WHERE Id = {0};
                ", id);
            return _query;
        }
        #endregion

        #region OrderService Query
        internal static string GetDateToDateAllOrderQuery(string fromDate, string toDate, int? channelId, string orderStatus = "")
        {
            string byChannel = channelId > 0 ? string.Format("AND ChannelId = {0}", channelId) : string.Empty;

            string byOrderStatus = orderStatus?.Length > 0 ? $"AND OrderStatus = '{orderStatus}'" : string.Empty;


            _query = string.Format(@"
            SELECT odl.Id,
                   CONVERT(DATE, odl.OrderedAt) AS Date,
                   chn.ChannelName,
                   COUNT(odt.ProductId) AS ProductUnit,
                   odl.TotalPrice,
                   odl.ShipmentProvider,
                   odl.OrderStatus
            FROM ORDERLIST AS odl
            JOIN Channel AS chn ON odl.ChannelId = chn.Id
            JOIN OrderDetail AS odt ON odt.OrderId = odl.Id
            WHERE CAST(odl.OrderedAt AS Date) BETWEEN '{0}' AND '{1}'
                  {2} {3}
            GROUP BY odl.Id,
                     CONVERT(DATE, odl.OrderedAt),
                     ChannelName,
                     TotalPrice,
                     ShipmentProvider,
                     OrderStatus
                        ", fromDate, toDate, byChannel, byOrderStatus);
            return _query;
        }

        #endregion
    }
}
