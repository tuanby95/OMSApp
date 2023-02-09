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
        private static readonly string _connectionString = "Data Source=.;Initial Catalog=OMSDb;Integrated Security=True";
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

        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(_connectionString, sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }
    }
}
