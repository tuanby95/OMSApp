using OMSTest;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace UnitTestProject1
{
    public static class DashboardService
    {
        /// <summary>
        /// Get total products that not selling from a range of time
        /// </summary>
        /// <param name="fromDate">The date begining checking</param>
        /// <param name="toDate">The last date checking</param>
        /// <returns>Return total products</returns>
        internal static long GetTotalNotSellingProductsByDate(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetTotalNotSellingProductsByDateQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        /// <summary>
        /// Get total product that out of stock at the moment
        /// </summary>
        /// <returns>Return total amount of out of stock products</returns>
        internal static long GetTotalOutOfStockProducts()
        {
            string sql = SqlQueryHelper.GetTotalOutOfStocProductsQuery();
            return GetTotalByConditionInternal(sql);
        }

        /// <summary>
        /// Get total nearly out of stock product
        /// </summary>
        /// <returns>Return total product that have under 10 amount in stock</returns>
        internal static long GetTotalNearlyOutOfStockProdudcts()
        {
            string sql = SqlQueryHelper.GetTotalNearlyOutOfStockProductsQuery();
            return GetTotalByConditionInternal(sql);
        }


        private static List<DashboardTotalSalesItem> GetTotalSalesListByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardTotalSalesItem>();
            while (reader.Read())
            {
                var item = new DashboardTotalSalesItem
                {
                    DisplayText = reader[0] + "",
                    Value = long.Parse(reader[1] + ""),
                    AverageTotalPrice = reader[2] + "",
                    TotalReturnPrice = ConvertToLong(reader[3]),
                    ReturnOrderAmount = reader[4] + ""
                };
                result.Add(item);
            }
            return result;
        }

        private static long? ConvertToLong(object v)
        {
            if (v == null || v.ToString() == String.Empty)
            {
                return 0;
            }
            return long.Parse(v.ToString());
        }

        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }

        internal static List<DashboardSaleChannelItem> GetTotalOrdersOnChannelByDate(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetTotalOrdersOnChannelByDateQuery(fromDate, toDate);
            return GetTotalListOrderByConditionInternal(sql);
        }

        private static List<DashboardSaleChannelItem> GetTotalListOrderByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardSaleChannelItem>();
            while (reader.Read())
            {
                var item = new DashboardSaleChannelItem
                {
                    ChannelImg = reader[0] + "",
                    ChannelName = reader[1] + "",
                    DisplayText = reader[2] + "",
                    Value = long.Parse(reader[3] + ""),
                    TotalSale = long.Parse(reader[4] + "")
                };
                result.Add(item);
            }
            return result;
        }

        internal static long GetTotalIssueOrdersByDate(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetTotalIssueOrdersByDate(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static object GetSellingProductTopThree(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetSellingProductTopThreeQuery(fromDate, toDate);
            return GetTotalListByConditionInternal(sql);
        }

        private static List<DashboardItem> GetTotalListByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardItem>();
            while (reader.Read())
            {
                var item = new DashboardItem()
                {
                    DisplayText = reader[0] + "",
                    Value = long.Parse(reader[1] + "")
                };
                result.Add(item);
            }
            return result;
        }

        internal static IEnumerable<DashboardProductsItem> GetTotalProductByAmount(string checkingChannel)
        {
            string sql = SqlQueryHelper.GetTotalProductsByAmountQuery(checkingChannel);
            return GetTotalProductByAmountInternal(sql);
        }

        private static IEnumerable<DashboardProductsItem> GetTotalProductByAmountInternal(string sql)
        {
            return SqlHelper.Query<DashboardProductsItem>(sql);

        }

        internal static IEnumerable<DashboardChannelItem> GetTotalChannelsByStatus()
        {
            string sql = SqlQueryHelper.GetTotalChannelsByStatusQuery();
            return GetTotalChannelsByStatusInternal(sql);
        }

        private static IEnumerable<DashboardChannelItem> GetTotalChannelsByStatusInternal(string sql)
        {
            return SqlHelper.Query<DashboardChannelItem>(sql);
        }

        internal static IEnumerable<DashboardSingleItem> GetTotalInactiveProductOnChannel(string checkingChannel)
        {
            string sql = SqlQueryHelper.GetTotalInactiveProductOnChannel(checkingChannel);
            return GetTotalInactiveProductOnChannelInternal(sql);
        }

        private static IEnumerable<DashboardSingleItem> GetTotalInactiveProductOnChannelInternal(string sql)
        {
            return SqlHelper.Query<DashboardSingleItem>(sql);
        }

        internal static IEnumerable<DashboardOrderItem> GetTotalOrderByDate(string fromDate, string toDate)
        {
            string sql = SqlQueryHelper.GetTotalOrdersByDateQuery(fromDate, toDate);
            return GetTotalOrderByDate(sql);
        }

        private static IEnumerable<DashboardOrderItem> GetTotalOrderByDate(string sql)
        {
            return SqlHelper.Query<DashboardOrderItem>(sql);
        }

        internal static IEnumerable<DashboardProductItem> GetTotalNotSellingProducts(DateTime toDate, string checkingChannel)
        {
            string sql = SqlQueryHelper.GetTotalNotSellingProductsQuery(toDate, checkingChannel);
            return GetTotalNotSellingProductsInternal(sql);
        }

        private static IEnumerable<DashboardProductItem> GetTotalNotSellingProductsInternal(string sql)
        {
            return SqlHelper.Query<DashboardProductItem>(sql);
        }
    }
}
