using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    internal class ProductChannel: ProductVariant
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string CSKU { get; set; }
        public string ProductChannelStatus { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
