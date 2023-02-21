using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UnitTestProject1
{
    [Table("OrderDetail")]
    internal class OrderDetail : Order
    {
      
        public int PerProductPrice { get; set; }
        public int TotalPerProductPrice { get; set; }
    }
}
