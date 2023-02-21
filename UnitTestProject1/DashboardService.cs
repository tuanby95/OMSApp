using Dapper;
using OMSTest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public static class DashboardService
    {
        internal static List<DashboardTotalOrderItem> GetCancelledDeliveriedReturn(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetCancelledDeliveriedReturnQuery(fromDate, toDate);
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardTotalOrderItem>();
            while (reader.Read())
            {
                result.Add(new DashboardTotalOrderItem
                {
                    WeekNumber = Convert.ToInt32(reader[0]),
                    TotalDelivery = Convert.ToInt32(reader[1]),
                    TotalCancel = Convert.ToInt32(reader[2]),
                    TotalReturn = Convert.ToInt32(reader[3])
                });
            }
            return result;
        }
        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(SqlHelper._connectionString, sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }
        internal static List<DashboardItem> GetTotalProduct(string filterFormat, string productName)
        {
            var sql = SqlQueryHelper.GetTotalProductQuery(filterFormat, productName);
            return GetDashboardItemByConditions(sql);
        }
        private static List<DashboardItem> GetDashboardItemByConditions(string sql)
        {
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);

            var result = new List<DashboardItem>();

            while (reader.Read())
            {
                var item = new DashboardItem
                {
                    Hour= Convert.ToInt32(reader[0]),
                    Day = Convert.ToInt32(reader[1]),
                    Month = Convert.ToInt32(reader[2]),
                    Year = Convert.ToInt32(reader[3]),
                    Value= Convert.ToInt32(reader[4])
                };

                result.Add(item);
            }
            return result;
        }
        internal static List<DashboardSaleByLocationItem> GetSalesAnalytics(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetSalesAnalyticsQuery(fromDate, toDate);
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardSaleByLocationItem>();

            while (reader.Read())
            {
                result.Add(new DashboardSaleByLocationItem
                {
                    Year = Convert.ToInt32(reader[0]),
                    Month = Convert.ToInt32(reader[1]),
                    Location = Convert.ToString(reader[2]),
                    Value = Convert.ToInt64(reader[3])
                });
            }
            return result;


        }

        internal static List<DashboardItem> GetTotalProductDapper(string filterFormat, string productName)
        {
            var sql = SqlQueryHelper.GetTotalProductQuery(filterFormat, productName);
           return SqlHelper.Query<DashboardItem>(sql);
        }

        internal static List<DashboardTotalOrderItem> GetCancelledDeliveriedReturnDapper(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetCancelledDeliveriedReturnQuery(fromDate, toDate);
            return SqlHelper.Query<DashboardTotalOrderItem>(sql);

        }

        internal static List<DashboardSaleByLocationItem> GetSalesAnalyticsDapper(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetSalesAnalyticsQuery(fromDate, toDate);   
            
            return SqlHelper.Query<DashboardSaleByLocationItem>(sql);
        }
    }
}
