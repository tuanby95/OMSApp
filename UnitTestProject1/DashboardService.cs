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
