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
        internal static string GetTotalProductQuery(string filterFormat, string productName)
        {
            _query = string.Format(@"
             DECLARE @FromDate AS DATETIME = DATEADD({1}, (DATEDIFF({1}, 0, CURRENT_TIMESTAMP) + 0), 0);
             DECLARE @ToDate AS DATETIME = DATEADD({1}, (DATEDIFF({1}, 0, CURRENT_TIMESTAMP) + 1), 0);

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
        internal static string GetProductsQuery(int channelId, string byProductStatus)
        {
            _query = string.Format(@"
               SELECT 
	                pd.Id,
	                prv.ProductVariantImage,
	                pd.ProductDescription,
	                pd.ProductName,
	                prv.ProductVariantValue,
	                prc.LastUpdatedAt,
	                prc.Quantity,
	                prc.CSKU,
	                prc.Price,
	                prc.ProductChannelStatus
                FROM ProductChannel prc
                JOIN ProductVariant prv ON prc.ProductVariantId = prv.Id
                JOIN Product pd ON prv.ProductId = pd.Id
                WHERE prc.ChannelId = {0} {1}
                ORDER BY prc.Quantity DESC", channelId, byProductStatus);
            return _query;
        }

        internal static string UpdateProductStockQuery(int channelId, int productVariantId, int warehouseId, int updateStockQuantity)
        {
            _query = string.Format(@"
                    UPDATE [dbo].[ProductWarehouse]
                       SET [Quantity] = {3}
                     WHERE ProductVariantId = {1} AND ChannelId = {0} AND WarehouseId = {2}"
                     , channelId, productVariantId, warehouseId, updateStockQuantity);
            return _query;
        }
        internal static string InsertNewProductQuery(Product product)
        {
            //_query = string.Format(@"
            //INSERT INTO Product
            //           ([ProductMainImg]
            //           ,[ProductGallery]
            //           ,[ProductName]
            //           ,[SKU]
            //           ,[Barcode]
            //           ,[Price]
            //           ,[Cost]
            //           ,[Height]
            //           ,[Width]
            //           ,[Length]
            //           ,[Weight]
            //           ,[ProductDescription]
            //           ,[CreatedAt]
            //           ,[ProviderId])
            //       VALUES
            //           ('{0}'
            //           ,'{1}'
            //           ,'{2}'
            //           ,'{3}'
            //           ,'{4}'
            //           ,{5}
            //           ,{6}
            //           ,{7}
            //           ,{8}
            //           ,{9}
            //           ,{10}
            //           ,'{11}'
            //           ,'{12}'
            //           ,{13});",
            //product.ProductMainImg, product.ProductGallery, product.ProductName, product.SKU, product.Barcode,
            //product.Price, product.Cost, product.Height, product.Width, product.Length, product.Weight, product.ProductDescription, product.CreatedAt, product.ProviderId);
            //return _query;
            return "";
        }

        internal static string InserProductChannelStockQuery(int productId, int channelId, int updateStock, string status)
        {
            _query = string.Format(@"
                    INSERT INTO ProductChannel
                               ([ProductId]
                               ,[ChannelId]
                               ,[AvaiableStock]
                               ,[ProductChannelStatus]
                               ,[UpdatedAt])
                         VALUES
                               ({0}
                               ,{1}
                               ,{2}
                               ,'{3}'
                               ,GETDATE())", productId, channelId, updateStock, status);
            return _query;
        }
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
        internal static string GetUserInformationQuery(int id)
        {
            _query = string.Format(@"
               SELECT [Id]
                      ,[FullName]
                      ,[PhoneNumber]
                      ,[DOB]
                      ,[GENDER]
                      ,[Email]
                      ,[UserRole]
                      ,[FullAddress]
                      ,[UserStatus]
                      ,[Facebook]
                      ,[Instagram]
                      ,[UserName]
                      ,[UserPassword]
                      ,[Avatar]
               FROM [dbo].[UserInfo]
	           WHERE Id = {0}
                ", id);
            return _query;
        }
        internal static string UpdatePasswordQuery(User user, string oldPassword, string newPassword)
        {
            _query = string.Format(@"
                UPDATE UserInfo
                SET [UserPassword] = CASE
                     WHEN '{0}' = [UserPassword] THEN '{1}'
                     ELSE [UserPassword]
                 END
                 WHERE [Id] = {2};", oldPassword, newPassword, user.Id);
            return _query;
        }
        #endregion

        #region OrderService Query
        internal static string GetAllOrderQuery(string fromDate, string toDate, int? channelId, string orderStatus = "")
        {
            string byChannel = channelId > 0 ? string.Format("AND ChannelId = {0}", channelId) : string.Empty;

            string byOrderStatus = orderStatus?.Length > 0 ? $"AND OrderStatus = '{orderStatus}'" : string.Empty;


            _query = string.Format(@"
            SELECT odl.Id,
                   CONVERT(DATE, odl.OrderedAt) AS Date,
                   chn.ChannelName,
                   COUNT(DISTINCT odt.ProductVariantId) AS ProductUnit,
                   odl.TotalPrice,
                   odl.ShipmentProvider,
                   odl.OrderStatus
            FROM OrderInfo AS odl
            JOIN Channel AS chn ON odl.ChannelId = chn.Id
            JOIN OrderDetail AS odt ON odt.OrderId = odl.Id
            JOIN ProductVariant prv ON odt.ProductVariantId = prv.Id
            JOIN Product pd ON prv.ProductId = pd.Id
            WHERE odl.OrderedAt BETWEEN '{0}' AND DATEADD(SECOND, -1, DATEADD(DAY, 1, '{1}'))
              {2} {3} 
            GROUP BY odl.Id,
                     odl.OrderedAt,
                     ChannelName,
                     TotalPrice,
                     ShipmentProvider,
                     OrderStatus
            ORDER BY odl.OrderedAt
                        ", fromDate, toDate, byChannel, byOrderStatus);
            return _query;
        }

        internal static string GetOrderDetailQuery(int? id)
        {
            _query = string.Format(@"
                SELECT
	                DISTINCT prd.Id,
	                 prd.ProductName,
	                pdv.ProductVariantImage,
	                pdv.ProductVariantValue,
	                prd.ProductDescription,
	                pdc.CSKU,
	                odt.Quantity,
	                pdc.Price AS PerProductPrice,
	                (odt.Quantity * pdc.Price) as TotalPerProductPrice,
	                odl.RecipientName,
	                odl.RecipientPhoneNumber,
	                odl.ShippingAddress
                FROM OrderInfo odl
                JOIN OrderDetail odt ON odl.Id = odt.OrderId
                JOIN ProductVariant pdv ON odt.ProductVariantId = pdv.Id
                JOIN Product prd ON prd.Id=pdv.ProductId  
                JOIN ProductChannel pdc ON pdc.ProductVariantId = pdv.Id AND odl.ChannelId = pdc.ChannelId
                WHERE odt.OrderId = {0}   
                ", id);
            return _query;
        }

        internal static string UpdateReturnOrderQuery(int returnId, User user, string updatedStatus)
        {
            _query = string.Format(@"
                    UPDATE [dbo].[ReturnInfo]
                      SET [UpdatedByUserId] = {1}
                          ,[LastUpdatedAt] = GETDATE()
                          ,[ReturnStatus] = '{0}'
                    WHERE ReturnStatus = 'PENDING' 
                      AND Id= {2}", updatedStatus, user.Id, returnId);
            return _query;
        }

        internal static string GetRatingQuery(DateTime fromDate, DateTime toDate, int channelId)
        {
            _query = string.Format(@"
            SELECT
                rating,
                COUNT(fb.rating) AS Total,
                CEILING(Count(1) * 100 / Sum(Count(*)) over()) AS Percentage
            FROM Channel as chn
            JOIN Feedback AS fb ON fb.ChannelId = chn.Id
            WHERE ChannelId = {2} AND fb.CreatedAt BETWEEN CAST('{0}' AS DATE) AND CAST('{1}' AS DATE)
            GROUP BY rating
            ", fromDate, toDate, channelId);
                return _query;
        }

        #endregion
    }
}
