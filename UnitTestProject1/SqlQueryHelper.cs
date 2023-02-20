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

        internal static string GetSellingProductTopThreeQuery(DateTime fromDate, DateTime toDate)
        {
            _query = String.Format(@"
                                    SELECT p.Id,
                                     ProductName,
                                            SUM(ord.Quantity * ord.DiscountedPrice) AS ValueSale
                                    FROM OrderDetail ord
                                    JOIN Product p ON ord.ProductId = p.Id
                                    GROUP BY p.Id, ProductName
                                    ORDER BY ValueSale DESC");
            return _query;
        }

        internal static string GetTotalChannelsByStatusQuery()
        {
            _query = String.Format(@"
                                    SELECT
                                         SUM(CASE
                                             WHEN cn.ChannelStatus = 'ACTIVE' THEN 1
                                         ELSE 0 END) AS TotalActiveChannel,
                                         SUM(CASE
                                             WHEN cn.ChannelStatus = 'INACTIVE' THEN 1
                                             ELSE 0 END) AS TotalInactiveChannel
                                    FROM
                                         Channel AS cn");
            return _query;
        }

        internal static string GetTotalInactiveProductOnChannel(string checkingChannel)
        {
            _query = String.Format($@"
                                    SELECT
                                         COUNT(pc.ProductVariantId) AS TotalProducts
                                    FROM
                                         ProductChannel AS pc
                                    WHERE
                                         pc.ProductChannelStatus = 'INACTIVE'
                                     AND pc.ChannelId = {checkingChannel}
                                    ");
            return _query;
        }

        internal static string GetTotalIssueOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            string str1, str2;
            ConvertDateTimeToString(fromDate, toDate, out str1, out str2);
            _query = String.Format(@"
                                    SELECT
	                                    SUM(
		                                    CASE 
	                                        WHEN odl.OrderStatus IN('FAILED', 'RETURN', 'CANCELLED') THEN 1
		                                    WHEN odl.OrderStatus NOT IN('FAILED', 'RETURN', 'CANCELLED') THEN 0
		                                    END) AS TotalStatusWithFailed
                                    FROM
	                                    OrderList AS odl
                                    WHERE 
	                                    odl.OrderedAt BETWEEN '{0}'
	                                    AND DATEADD(SECOND, 86399, '{1}')", str1, str2);
            return _query;
        }
        


        #region DashboardService Query
        internal static string GetTotalNearlyOutOfStockProductsQuery()
        {
            _query = String.Format(@"
                                    SELECT 
                                      COUNT(1) as NearlyOutOfStockProducts 
                                    FROM 
                                      ProductChannel 
                                    WHERE 
                                      ProductChannel.AvaiableStock <= 10");
            return _query;
        }

        internal static string GetTotalNotSellingProductsByDateQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");
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
                                          ord.OrderedAt BETWEEN '{0}' 
                                          AND DATEADD(SECOND, -1, '{1}')
                                      )", str1, str2);
            return _query;
        }

        internal static string GetTotalOrdersOnChannelByDateQuery(DateTime fromDate, DateTime toDate)
        {
            string str1, str2;
            ConvertDateTimeToString(fromDate, toDate, out str1, out str2);
            _query = String.Format(@"
                                    DECLARE @PageNumber AS INT;
                                    DECLARE @RowsOfPage AS INT;
                                    DECLARE @MaxTablePage AS FLOAT;
                                    SET @PageNumber = 1;
                                    SET @RowsOfPage = 5;

                                    SELECT @MaxTablePage = COUNT (1) FROM Channel as chnl

                                    SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
                                    WHILE @MaxTablePage >= @PageNumber
                                    BEGIN
	                                    SELECT 
	                                      chnl.ChannelImg,
	                                      chnl.ChannelName, 
	                                      chnl.ChannelStatus, 
	                                      COUNT(ord.Id) as TotalOrders, 
	                                      SUM(ord.TotalPrice) as TotalSales 
	                                    FROM 
	                                      OrderList ord 
	                                      JOIN Channel chnl ON ord.ChannelId = chnl.Id 
	                                    WHERE 
	                                      ord.OrderedAt BETWEEN '{0}' 
	                                      AND DATEADD(SECOND, 86399, '{1}' )
	                                    GROUP BY 
	                                      chnl.ChannelImg,
	                                      chnl.ChannelName,
	                                      chnl.ChannelStatus
	                                    ORDER BY
	                                      chnl.ChannelName
	                                    OFFSET (@PageNumber-1)*@RowsOfPage ROWS
	                                    FETCH NEXT @RowsOfPage ROWS ONLY
                                        SET @PageNumber = @PageNumber + 1
                                    END ", str1, str2);
            return _query;
        }

        internal static string GetTotalOutOfStocProductsQuery()
        {
            _query = String.Format(@"
                                    SELECT 
                                      COUNT(1) as OutOfStockProducts 
                                    FROM 
                                      ProductChannel 
                                    WHERE 
                                      ProductChannel.AvaiableStock = 0");
            return _query;
        }

        internal static string GetTotalProductsByAmountQuery(string checkingChannel)
        {
            _query = String.Format(@"
                                    SELECT
	                                    SUM(CASE
        	                                    WHEN
            	                                    pc.Quantity > 0 AND (pc.Quantity <= pc.MinimumThreshold
            	                                    OR pc.SupplierLeadTimes/(ROUND((pc.Quantity/CAST(pc.LastSevenDaySalesAvg AS FLOAT)), 2)) >= 1) THEN 1
        	                                    ELSE 0 END) AS TotalNearlyOutOfStockProduct,
	                                    SUM(CASE
        	                                    WHEN
            	                                    pc.Quantity > 0
            	                                    AND pc.SupplierLeadTimes/(ROUND((pc.Quantity/CAST(pc.LastSevenDaySalesAvg AS FLOAT)), 2)) < 1 THEN 1
        	                                    ELSE 0 END) AS TotalNormalProduct,
	                                    SUM(CASE
        	                                    WHEN pc.Quantity = 0 THEN 1
        	                                    ELSE 0 END) AS TotalOutOfStockProduct
                                    FROM
                                        ProductChannel pc
                                    WHERE
                                        pc.ChannelId = {0}", checkingChannel);
            return _query;
        }

        internal static string GetTotalSalesByDateQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");

            _query = string.Format(@"
                                    DECLARE @PageNumber AS INT;
                                    DECLARE @RowsOfPage AS INT;
                                    DECLARE @MaxTablePage AS FLOAT;
                                    SET @PageNumber = 1;
                                    SET @RowsOfPage = 5;

                                    SELECT @MaxTablePage = COUNT (*) FROM OrderList odr
                                    WHERE
	                                    odr.OrderedAt BETWEEN '{0}'
	                                    AND DATEADD(SECOND, 86399, '{1}')

                                    SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage)
                                    WHILE @MaxTablePage >= @PageNumber
                                    BEGIN
	                                    SELECT
	                                    CONVERT(DATE, odr.OrderedAt) AS OrderedDate,
	                                    ROUND(AVG(odr.TotalPrice), 2) AS AverageTotalPrice,
	                                    SUM(odr.TotalPrice) AS TotalPrice,
	                                    SUM(
		                                    CASE WHEN odr.OrderStatus IN ('RETURN') THEN 1
		                                    ELSE 0 END) AS NumberOfReturns,
	                                    SUM(
		                                    CASE WHEN odr.OrderStatus IN ('COMPLETED') THEN odr.TotalPrice
		                                    ELSE 0 END) AS TotalSales
                                    FROM
	                                    OrderList odr
                                    WHERE
	                                    odr.OrderedAt BETWEEN '{0}'
	                                    AND DATEADD(SECOND, 86399, '{1}')
                                    GROUP BY
	                                    CONVERT(DATE, odr.OrderedAt)
	                                    ORDER BY CONVERT(DATE, odr.OrderedAt)
	                                    OFFSET (@PageNumber-1)*@RowsOfPage ROWS
	                                    FETCH NEXT @RowsOfPage ROWS ONLY
                                        SET @PageNumber = @PageNumber + 1
                                    END", str1, str2);
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

        private static void ConvertDateTimeToString(DateTime fromDate, DateTime toDate, out string str1, out string str2)
        {
            str1 = fromDate.ToString("yyyy-MM-dd");
            str2 = toDate.ToString("yyyy-MM-dd");
        }
    }
}
