using System;
using Dapper.Contrib.Extensions;

namespace UnitTestProject1
{
    [Table("Product")]
    internal class Product : ProductChannel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[] ProductImage { get; set; }
        public string Barcode { get; set; }
        public int BasePrice { get; set; }
        public int Cost { get; set; }
        public int ProductHeight { get; set; }
        public int ProductWidth { get; set; }
        public int ProductLength { get; set; }
        public int ProductWeight { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        public int VoucherId { get; set; }
        public int ProviderId { get; set; }

    }
}