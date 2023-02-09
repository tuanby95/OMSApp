using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSTest;

namespace UnitTestProject1
{
    public static class CustomerService
    {
        internal static List<DashboardChartItem> GetDateToDateRatingByChannel(DateTime fromDate, DateTime toDate, int channelId)
        {
           var sql = SqlQueryHelper.GetDateToDateRatingByChannelQuery(fromDate, toDate, channelId);

            var reader = SqlHelper.ExecuteReader(sql, CommandType.Text);
            var result = new List<DashboardChartItem>();
            while(reader.Read()) {
                result.Add(new DashboardChartItem()
                {
                    DisplayText = Convert.ToString(reader[0]),
                    Value = Convert.ToInt64(reader[1]),
                    ExtraValue = Convert.ToInt64(reader[2]),
                });
            }
            return result;
        }
    }
}
