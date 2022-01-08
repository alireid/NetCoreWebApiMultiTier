using System.Collections.Generic;

namespace NetCoreWebApiMultiTier.WebApi.Data.Models
{
    /// <summary>
    /// Model representing products table.
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal PackHeight { get; set; }
        public decimal PackWidth { get; set; }
        public decimal PackWeight { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
