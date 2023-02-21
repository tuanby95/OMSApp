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

            SELECT
	            DATEPART(HOUR, ord.OrderedAt) AS HourNumber,
	            DATEPART(DAY, ord.OrderedAt) AS DayNumber,
	            DATEPART(MONTH, ord.OrderedAt) AS MonthNumber,
	            DATEPART(YEAR, ord.OrderedAt) AS YearNumber,
	            SUM(ord.TotalPrice) AS TotalSales
            FROM OrderInfo AS ord
            JOIN OrderDetail AS odt ON ord.Id = odt.OrderId
            JOIN ProductVariant pdv ON odt.ProductVariantId = pdv.Id
            JOIN Product AS prd ON pdv.ProductId = prd.Id 
					            AND prd.ProductName LIKE '%{0}%'
            WHERE ord.OrderedAt BETWEEN @FromDate AND @ToDate
            GROUP BY DATEPART(HOUR, ord.OrderedAt),
	            DATEPART(DAY, ord.OrderedAt),
	            DATEPART(MONTH, ord.OrderedAt),
	            DATEPART(YEAR, ord.OrderedAt)
            ORDER BY DATEPART(MONTH, ord.OrderedAt), DATEPART(YEAR, ord.OrderedAt)	
                ", productName, filterFormat);
            return _query;
        }
        internal static string GetCancelledDeliveriedReturnQuery(DateTime fromDate, DateTime toDate)
        {
            _query = string.Format(@"
                SELECT datepart(WEEK, od.OrderedAt) AS WeekNumber,
                       SUM(CASE
                               WHEN od.OrderStatus = 'DELIVERY' THEN 1
                               ELSE 0
                               END) AS TotalDelivery,
                       SUM(CASE
                               WHEN od.OrderStatus = 'FAILED' THEN 1
                               ELSE 0
                               END) AS TotalCancel,
                       SUM(CASE
                               WHEN re.ReturnStatus = 'APPROVED' THEN 1
                               ELSE 0
                               END) AS TotalReturn
                FROM OrderInfo AS od
                LEFT JOIN ReturnInfo AS re ON od.Id = re.OrderId
                WHERE od.orderedAt  BETWEEN '{0}' AND DATEADD(SECOND, -1, DATEADD(DAY, 1, '{1}'))
                GROUP BY datepart(WEEK, od.OrderedAt)
                ", fromDate, toDate);
            return _query;
        }
        internal static string GetSalesAnalyticsQuery(DateTime fromDate, DateTime toDate)
        {
            _query = string.Format(@"
                   SELECT month(ord.OrderedAt) AS MONTH,
                          year(ord.OrderedAt) AS YEAR,
                          ctr.CountryName,
                          SUM(ord.TotalPrice) AS TotalPrice
                    FROM Country AS ctr
                    JOIN Channel AS chn ON ctr.Id = chn.CountryId
                    JOIN OrderInfo AS ord ON chn.Id = ord.ChannelId
                    WHERE ord.OrderedAt
                          BETWEEN '{0}'
                          AND DATEADD(SECOND, -1, DATEADD(DAY, 1, '{1}'))
                    GROUP BY month(ord.OrderedAt),
                             year(ord.OrderedAt),
                             ctr.CountryName
                    ORDER BY year(ord.OrderedAt),
                             month(ord.OrderedAt)", fromDate, toDate);
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
    }
}
