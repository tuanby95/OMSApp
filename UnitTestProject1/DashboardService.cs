using OMSTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class DashboardService
    {
        public static long GetTotalSales(String fromDate, String toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesQuery(fromDate, toDate);

            return GetTotalByConditionInternal(sql);
        }

        internal static List<DashboardItem> GetSaleList(String fromDate, String toDate)
        {

            var sql = SqlQueryHelper.GetSaleListQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDate(sql);
        }

        public static object GetTotalSalesReturn(String fromDate, String toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesReturnQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static object GetTotalSalesOverview()
        {
            var sql = SqlQueryHelper.GetTotalSalesOverviewQuery();
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetTotalSalesOverviewChart(string fromDate, string toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetAvgOrderOverview()
        {
            var sql = SqlQueryHelper.GetAvgOrderOverviewQuery();
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetAvgOrderOverviewChart(String fromDate, String toDate)
        {
            var sql = SqlQueryHelper.GetAvgOrderOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetTotalReturnOverview()
        {
            var sql = SqlQueryHelper.GetTotalReturnOverviewQuery();
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetTotalReturnOverviewChart(string fromDate, string toDate)
        {
            var sql = SqlQueryHelper.GetTotalReturnOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDate(sql);
        }

        internal static object GetSaleOnChannelChart()
        {
            var sql = SqlQueryHelper.GetSaleOnChannelChartQuery();
            //return GetDashboardItemExtraByConditionsDate(sql);
            return 0;
        }

        internal static object GetSaleByLocationOverview()
        {
            var sql = SqlQueryHelper.GetSaleByLocationOverviewQuery();
            return GetDashboardItemByConditionsDate(sql);
        }


        internal static long GetTotalNotSellingProductsByDate(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetTotalNotSellingProductsByDateQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(SqlHelper._connectionString, sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }

        private static List<DashboardItem> GetDashboardItemByConditionsDate(string sql)
        {
            var reader = SqlHelper.ExecuteReader(SqlHelper._connectionString, sql, CommandType.Text);

            var result = new List<DashboardItem>();

            while (reader.Read())
            {
                var item = new DashboardItem
                {
                    Value = reader[0].ToString() == String.Empty ? 0 + "" : reader[0] + "",
                    DisplayText = reader[1] + ""
                };

                result.Add(item);
            }
            return result;
        }
    }
}
