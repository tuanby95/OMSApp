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
    public static class ChannelService
    {
        internal static List<OrderChannelItem> GetTotalOrdersOnChannel(DateTime fromDate, DateTime toDate)
        {
            var sql = SqlQueryHelper.GetTotalOrdersOnChannelByDateQuery(fromDate, toDate);
            return GetTotalOrdersInternal(sql);
        }

        private static List<OrderChannelItem> GetTotalOrdersInternal(string sql)
        {
            using (IDbConnection db = new SqlConnection(SqlHelper._connectionString))
            {
                var result = db.Query<OrderChannelItem>(sql).ToList();
                return result;
            }
        }
    }
}
