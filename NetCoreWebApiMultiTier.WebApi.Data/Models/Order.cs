using System;
using System.Collections.Generic;

namespace NetCoreWebApiMultiTier.WebApi.Data.Models
{
    /// <summary>
    /// Model representing orders table.
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryExpected { get; set; }
        public bool ContainsGift { get; set; }
        public string ShippingMode { get; set; }
        public string OrderSource { get; set; }
        public ICollection<OrderItem> OrderItems { get; set;}
    }
}
