using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    internal class OrderDetail : Order
    {
        public int TotalPerProductPrice { get; set; }
        public int Quantity { get; internal set; }
    }
}
