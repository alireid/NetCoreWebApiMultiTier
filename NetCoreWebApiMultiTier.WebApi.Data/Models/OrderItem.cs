namespace NetCoreWebApiMultiTier.WebApi.Data.Models
{
    /// <summary>
    /// Model representing orderitems table.
    /// </summary>
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? Returnable { get; set; }
    }
}
