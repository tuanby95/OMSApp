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
       

        internal static long GetTotalNotSellingProductsByDate(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetTotalNotSellingProductsByDateQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }
        internal static List<DashboardTotalOrderItem> GetMonthlyCancelledDeliveriedReturn(DateTime fromDate, DateTime toDate)
        {
            string sql = SqlQueryHelper.GetDateToDateCancelledDeliveriedReturnQuery(fromDate, toDate);
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
            var respond = SqlHelper.ExecuteScalar(   sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }
        internal static List<DashboardItem> GetTotalProductByCatalog(DateTime fromDate, DateTime toDate, string productName)
        {
            var sql = SqlQueryHelper.GetTotalProductByCatalogQuery(fromDate, toDate, productName);
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
                    DisplayText = reader[0] + "",
                    Value = long.Parse(reader[1] + ""),
                };

                result.Add(item);
            }
            return result;
        }
        internal static List<DashboardSaleByLocationItem> GetDateToDateSalesAnalyticsByCountry(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetDateToDateSalesAnalyticsByCountryQuery(fromDate, toDate);
            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardSaleByLocationItem>();

            while (reader.Read())
            {
                result.Add(new DashboardSaleByLocationItem
                {
                    Year = Convert.ToInt32(reader[0]),
                    Month = Convert.ToInt32(reader[1]),
                    Location = Convert.ToString(reader[2]),
                    Total = Convert.ToInt64(reader[3])
                });
            }
            return result;


        }
    }
}
