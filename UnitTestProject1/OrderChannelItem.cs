using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    internal class OrderChannelItem
    {
        public Byte[] ChannelImage { get; set; }
        public string ChannelName { get; set; }
        public string ChannelStatus { get; set; }
        public int TotalOrders { get; set; }
        public float TotalSales { get; set; }
    }
}
