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


        internal static string GetSaleListQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
                    Sum(TotalPrice), 
                    OrderedAt
                FROM OrderList
                WHERE OrderedAt BETWEEN  '{0}' 
                  AND '{1}'
                GROUP BY OrderedAt", fromDate, toDate);
        }
        internal static string GetTotalSalesQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
                    Sum(TotalPrice) 
                FROM OrderList 
                WHERE OrderedAt BETWEEN '{0}' and '{1}'", fromDate, toDate);
        }

        internal static string GetTotalSalesReturnQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
                    Sum(TotalPrice) 
                FROM OrderList 
                WHERE OrderStatus = 'Return' 
                  AND (OrderedAt BETWEEN '{0}' 
                  AND '{1}');", fromDate, toDate);
        }

        internal static string GetTotalSalesOverviewQuery()
        {
            return _query = string.Format(@"
                DECLARE @totalPreviousMonth FLOAT
                SET @totalPreviousMonth =
                  (SELECT SUM(TotalPrice)
                   FROM OrderList
                   WHERE MONTH(OrderedAt) = MONTH(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
                     AND YEAR(OrderedAt) = YEAR(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
	                 AND OrderStatus != 'RETURNED')
                SELECT SUM(od.TotalPrice) AS TotalThisMonth,
                       ROUND(100*(SUM(od.TotalPrice) - @totalPreviousMonth)/ @totalPreviousMonth, 0) AS ""Percentage""
                FROM OrderList od
                WHERE MONTH(GETDATE()) = MONTH(od.OrderedAt);");
        }

        internal static string GetTotalSalesOverviewChartQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
	                SUM(TotalPrice) AS TotalSale,
                    OrderedAt
                FROM OrderList
                WHERE OrderedAt >= CAST('{0}' AS DATETIME)
                  AND OrderedAt < DATEADD(DAY, 1, '{1}')
                  AND OrderStatus != 'RETURNED'
                GROUP BY OrderedAt;", fromDate, toDate);
        }

        internal static string GetAvgOrderOverviewQuery()
        {
            return _query = string.Format(@"
                DECLARE @totalPreviousMonth FLOAT
                SET @totalPreviousMonth =
                  (SELECT 
                        AVG(TotalPrice)
                   FROM OrderList
                   WHERE MONTH(OrderedAt) = MONTH(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
                     AND YEAR(OrderedAt) = YEAR(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
	                 AND OrderStatus != 'RETURNED')
                SELECT 
                    ROUND(AVG(od.TotalPrice),0) AS AvgOrderThisMonth,
                    ROUND(100*(AVG(od.TotalPrice) - @totalPreviousMonth)/ @totalPreviousMonth, 0) AS Percentage
                FROM OrderList od
                WHERE MONTH(GETDATE()) = MONTH(od.OrderedAt)
                  AND od.OrderStatus != 'RETURNED';");
        }

        internal static string GetAvgOrderOverviewChartQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
	                AVG(TotalPrice) AS AvgOrder,
                    OrderedAt
                FROM OrderList
                WHERE OrderedAt >= CAST('{0}' AS DATETIME)
                  AND OrderedAt < DATEADD(DAY, 1, '{1}')
                  AND OrderStatus != 'RETURNED'
                GROUP BY OrderedAt;", fromDate, toDate);
        }

        internal static string GetTotalReturnOverviewQuery()
        {
            return _query = string.Format(@"
                DECLARE @totalPreviousMonth FLOAT
                SET @totalPreviousMonth =
                  (SELECT SUM(TotalPrice)
                   FROM OrderList
                   WHERE MONTH(OrderedAt) = MONTH(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
                     AND YEAR(OrderedAt) = YEAR(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
	                 AND OrderStatus != 'RETURNED')
                SELECT SUM(od.TotalPrice) AS TotalThisMonth,
                       ROUND(100*(SUM(od.TotalPrice) - @totalPreviousMonth)/ @totalPreviousMonth, 0) AS 'Percentage'
                FROM OrderList od
                WHERE MONTH(GETDATE()) = MONTH(od.OrderedAt)
                  AND od.OrderStatus = 'RETURNED';");
        }

        internal static string GetTotalReturnOverviewChartQuery(string fromDate, string toDate)
        {
            return _query = string.Format(@"
                SELECT 
	                SUM(TotalPrice) AS TotalReturn,
                    OrderedAt
                FROM OrderList
                WHERE OrderedAt >= CAST('{0}' AS DATETIME)
                  AND OrderedAt < DATEADD(DAY, 1, '{1}')
                  AND OrderStatus = 'RETURNED'
                GROUP BY OrderedAt;", fromDate, toDate);
        }

        internal static string GetSaleOnChannelChartQuery()
        {
            return _query = string.Format(@"
                ;WITH tmpChannelMonth AS
                  (SELECT Monthvalue,
                          c.*
                   FROM
                     (SELECT (1) AS Monthvalue
                      UNION SELECT 2
                      UNION SELECT 3
                      UNION SELECT 4
                      UNION SELECT 5
                      UNION SELECT 6
                      UNION SELECT 7
                      UNION SELECT 8
                      UNION SELECT 9
                      UNION SELECT 10
                      UNION SELECT 11
                      UNION SELECT 12) t
                   CROSS JOIN channel c)
                SELECT chn.Monthvalue,
                       chn.ChannelName,
                       SUM(odr.totalprice) totals
                FROM tmpchannelmonth chn
                LEFT JOIN OrderList odr ON odr.Channelid = chn.id
                AND chn.Monthvalue = MONTH(odr.OrderedAt)
                GROUP BY chn.Monthvalue,
                         chn.ChannelName;");
        }

        internal static string GetSaleByLocationOverviewQuery()
        {
            return _query = string.Format(@"
                DECLARE @totalPreviousMonth FLOAT
                SET @totalPreviousMonth =
                  (SELECT AVG(TotalPrice)
                   FROM OrderList
                   WHERE MONTH(OrderedAt) = MONTH(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
                     AND YEAR(OrderedAt) = YEAR(DATEADD(MONTH, -1, CURRENT_TIMESTAMP))
	                 AND Region = 'Singapore'
	                 AND OrderStatus != 'RETURNED')
                SELECT 
	                DATENAME(MONTH, GETDATE()) AS ThisMonth,
	                DATENAME(MONTH, DATEADD(MONTH, -1, CURRENT_TIMESTAMP)) AS PreviousMonth,
	                AVG(TotalPrice) AS AvgOrderThisMonth,
                    ROUND(100*(AVG(TotalPrice) - @totalPreviousMonth)/ @totalPreviousMonth, 0) AS 'Percentage'
                FROM OrderList
                WHERE MONTH(GETDATE()) = MONTH(OrderedAt)
                  AND OrderStatus != 'RETURNED'
                  AND Region = 'Singapore'
                GROUP BY Region;");
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
    }
}
