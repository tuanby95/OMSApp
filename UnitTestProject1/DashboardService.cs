using OMSTest;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
        /// Get total sales by date
        /// </summary>
        /// <param name="fromDate">Date begin</param>
        /// <param name="toDate">Date end</param>
        /// <returns>Return the total sales</returns>
        internal static List<DashboardTotalSalesItem> GetTotalSalesByDate(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesByDateQuery(fromDate, toDate);
            return GetTotalSalesListByConditionInternal(sql);
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
            using(IDbConnection db = new SqlConnection(SqlHelper._connectionString))
            {
                var result = db.Query<DashboardProductsItem>(sql);
                return result;
            }
        }

        internal static List<DashboardChannelItem> GetTotalChannelsByStatus()
        {
            string sql = SqlQueryHelper.GetTotalChannelsByStatusQuery();
            return GetTotalChannelsByStatusInternal(sql);
        }

        private static List<DashboardChannelItem> GetTotalChannelsByStatusInternal(string sql)
        {
            using(IDbConnection db = new SqlConnection(SqlHelper._connectionString))
            {
                var result = db.Query<DashboardChannelItem>(sql).ToList();
                return result;
            }
        }

        internal static List<DashboardSingleItem> GetTotalInactiveProductOnChannel(string checkingChannel)
        {
            string sql = SqlQueryHelper.GetTotalInactiveProductOnChannel(checkingChannel);
            return GetTotalInactiveProductOnChannelInternal(sql);
        }

        private static List<DashboardSingleItem> GetTotalInactiveProductOnChannelInternal(string sql)
        {
            using(IDbConnection db = new SqlConnection(SqlHelper._connectionString))
            {
                var result = db.Query<DashboardSingleItem>(sql).ToList();
                return result;
            }
        }
    }
}
