using System;
using System.Collections.Generic;
using System.Data;
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
        internal static string GetTotalProductByCatalogQuery(DateTime fromDate, DateTime toDate, string productName)
        {
            _query = string.Format(@"
                   SELECT CAST(ord.OrderedAt AS date),
                          SUM(ord.TotalPrice) AS Total
                   FROM OrderList AS ord
                   JOIN OrderDetail AS odt ON ord.Id = odt.OrderId
                   JOIN Product AS prd ON odt.ProductId = prd.Id
                   AND prd.ProductName LIKE '%{0}%'
                   WHERE ord.OrderedAt BETWEEN CAST('{1}' AS DATETIME) AND CAST('{2}' AS DATETIME)
                   GROUP BY ord.OrderedAt
                ", productName, fromDate, toDate);
            return _query;
        }
        internal static string GetDateToDateCancelledDeliveriedReturnQuery(DateTime fromDate, DateTime toDate)
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
                FROM OrderList AS od
                LEFT JOIN ReturnList AS re ON od.Id = re.OrderId
                WHERE CAST(od.orderedAt AS DATE) BETWEEN '{0}' AND '{1}'
                GROUP BY datepart(WEEK, od.OrderedAt)
                ", fromDate, toDate);
            return _query;
        }
        internal static string GetDateToDateSalesAnalyticsByCountryQuery(DateTime fromDate, DateTime toDate)
        {
            _query = string.Format(@"
                   SELECT month(ord.OrderedAt) AS MONTH,
                          year(ord.OrderedAt) AS YEAR,
                          ctr.CountryName,
                          SUM(ord.TotalPrice) AS TotalPrice
                    FROM Country AS ctr
                    JOIN Channel AS chn ON ctr.Id = chn.CountryId
                    JOIN OrderList AS ord ON chn.Id = ord.ChannelId
                    WHERE ord.OrderedAt
                          BETWEEN '{0}'
                          AND '{1}'
                    GROUP BY month(ord.OrderedAt),
                             year(ord.OrderedAt),
                             ctr.CountryName
                    ORDER BY year(ord.OrderedAt),
                             month(ord.OrderedAt)", fromDate, toDate);
            return _query;
        }

        #endregion

        #region ProductService Query
        internal static string GetProductsByChannelQuery(int channelId, string byProductStatus)
        {
            _query = string.Format(@"
                SELECT prd.Id,
                       prd.ProductMainImg,
                       prd.SKU,
                       prd.ProductName,
                       prd.CreatedAt,
                       prc.UpdatedAt,
                       prc.AvaiableStock,
                       prd.Price,
                       prc.ProductChannelStatus
                FROM ProductChannel prc
                JOIN Product prd ON prc.ProductId = prd.Id
                JOIN Channel chn ON chn.Id = prc.ChannelId
                WHERE chn.Id = {0} 
                 {1}", channelId, byProductStatus);
            return _query;
        }

        internal static string UpdateProductStockByChannelQuery(int channelId, int productId, int updateStockQuantity)
        {
            _query = string.Format(@"
                    UPDATE ProductChannel
                    SET AvaiableStock = {2}
                    WHERE ChannelId = {0} AND ProductId = {1};"
                     , channelId, productId, updateStockQuantity);
            return _query;
        }
        internal static string InsertNewProductQuery(Product product)
        {
            _query = string.Format(@"
            INSERT INTO Product
                       ([ProductMainImg]
                       ,[ProductGallery]
                       ,[ProductName]
                       ,[SKU]
                       ,[Barcode]
                       ,[Price]
                       ,[Cost]
                       ,[Height]
                       ,[Width]
                       ,[Length]
                       ,[Weight]
                       ,[ProductDescription]
                       ,[CreatedAt]
                       ,[ProviderId])
                   VALUES
                       ('{0}'
                       ,'{1}'
                       ,'{2}'
                       ,'{3}'
                       ,'{4}'
                       ,{5}
                       ,{6}
                       ,{7}
                       ,{8}
                       ,{9}
                       ,{10}
                       ,'{11}'
                       ,'{12}'
                       ,{13});",
            product.ProductMainImg, product.ProductGallery, product.ProductName, product.SKU, product.Barcode,
            product.Price, product.Cost, product.Height, product.Width, product.Length, product.Weight, product.ProductDescription, product.CreatedAt, product.ProviderId);
            return _query;
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
        internal static string GetUserInformationByIdQuery(int id)
        {
            _query = string.Format(@"
                SELECT *
                FROM UserList
                WHERE Id = {0};
                ", id);
            return _query;
        }
        internal static string UpdatePasswordQuery(User user, string oldPassword, string newPassword)
        {
            _query = string.Format(@"
                UPDATE UserList
                SET [UserPassword] = CASE
                     WHEN '{0}' = [UserPassword] THEN '{1}'
                     ELSE [UserPassword]
                 END
                 WHERE [Id] = {2};", oldPassword, newPassword, user.Id);
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

        internal static string GetOrderDetailByIdQuery(int? id)
        {
            _query = string.Format(@"
                SELECT prd.ProductMainImg,
                       prd.ProductName,
                       prd.SKU,
                       prd.Barcode,
                       odt.Quantity,
                       prd.Price AS PerProductPrice,
                       (odt.Quantity * prd.Price) AS TotalPerProductPrice,
                       odl.RecipientName,
                       odl.RecipientPhoneNumber,
                       odl.ShippingAddress
                FROM OrderList AS odl
                JOIN OrderDetail AS odt ON odl.Id = odt.OrderId
                JOIN Product AS prd ON odt.ProductId = prd.Id
                WHERE odl.Id = {0}    
                ", id);
            return _query;
        }

        internal static string UpdateReturnOrderQuery(int returnId, User user, string updatedStatus)
        {
            _query = string.Format(@"
                    UPDATE ReturnList
                    SET ReturnStatus= '{0}',
                        UpdatedAt = GETDATE(),
                        UpdatedByUserId = {1}
                    WHERE ReturnStatus = 'PENDING'
                      AND Id = {2};", updatedStatus, user.Id, returnId);
            return _query;
        }

        internal static string GetDateToDateRatingByChannelQuery(DateTime fromDate, DateTime toDate, int channelId)
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
