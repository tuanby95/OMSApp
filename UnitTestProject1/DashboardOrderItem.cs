using System;
using System.Globalization;

namespace UnitTestProject1
{
    internal class DashboardOrderItem
    {
        public string OrderedDate { get; set; }
        public int NumberOfOrders { get; set; }
        public float AverageOrderSales { get; set; }
        public int NumberOfReturns { get; set; }
        public float TotalSales { get; set; }
        
    }
}