using Dapper;
using OMSTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class DashboardService
    {
        public static long GetTotalSales(String fromDate, String toDate, int backNumber)
        {
            var sql = SqlQueryHelper.GetTotalSalesQuery(fromDate, toDate, backNumber);
            long result = SqlHelper.DapperExecuteScalar(sql);
            return result;
        }

        internal static object GetSaleList(String fromDate, String toDate)
        {

            var sql = SqlQueryHelper.GetSaleListQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        public static long GetTotalSalesReturn(String fromDate, String toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesReturnQuery(fromDate, toDate);
            long result = SqlHelper.DapperExecuteScalar(sql);
            return result;
        }

        internal static object GetTotalSalesOverview()
        {
            var sql = SqlQueryHelper.GetTotalSalesOverviewQuery();
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        internal static object GetTotalSalesOverviewChart(string fromDate, string toDate)
        {
            var sql = SqlQueryHelper.GetTotalSalesOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        internal static object GetAvgOrderOverview()
        {
            var sql = SqlQueryHelper.GetAvgOrderOverviewQuery();
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        internal static object GetAvgOrderOverviewChart(String fromDate, String toDate)
        {
            var sql = SqlQueryHelper.GetAvgOrderOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        internal static object GetTotalReturnOverview()
        {
            var sql = SqlQueryHelper.GetTotalReturnOverviewQuery();
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }

        internal static object GetTotalReturnOverviewChart(string fromDate, string toDate)
        {
            var sql = SqlQueryHelper.GetTotalReturnOverviewChartQuery(fromDate, toDate);
            return GetDashboardItemByConditionsDateWithDapper(sql);
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
            return GetDashboardItemByConditionsDateWithDapper(sql);
        }


        internal static long GetTotalNotSellingProductsByDate(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetTotalNotSellingProductsByDateQuery(fromDate, toDate);
            long result = SqlHelper.DapperExecuteScalar(sql);
            return result;
        }
        private static IEnumerable<DashboardItem> GetDashboardItemByConditionsDateWithDapper(string sql)
        {
            using(var conn = new SqlConnection(SqlHelper._connectionString))
            {
                var result = conn.Query<DashboardItem>(sql);
                return result;
            }
        }
    }
}
