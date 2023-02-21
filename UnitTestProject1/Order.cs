using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UnitTestProject1
{
    [Table("OrderInfo")]
    internal class Order : Product
    {
        public int OrderId { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public int ChannelId { get; internal set; }
        public string ChannelName { get; internal set; }
        public int UserId { get; internal set; }
        public int ProductUnit { get; internal set; }
        public int TotalPrice { get; internal set; }
        //public int VoucherId{ get; internal set; }
        public string Note { get; internal set; }
        public string OrderNumber { get; internal set; }
        public string TaxCode { get; internal set; }
        public int BuyerId { get; internal set; }
        public string BuyerName { get; internal set; }
        public string ShippingAddress { get; internal set; }
        public string RecipientName { get; internal set; }
        public string RecipientPhoneNumber { get; internal set; }
        public string Country { get; internal set; }
        public string City { get; internal set; }
        public string District { get; internal set; }
        public string Ward { get; internal set; }
        public string ZipCode { get; internal set; }
        public string ShippingServiceCost { get; internal set; }
        public string ShippingFee { get; internal set; }
        public string ShippingProvider { get; internal set; }
        public string CustomerPaymentMethod { get; internal set; }
        public string OrderStatus { get; internal set; }
    }
}
