namespace ECommerceAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Reference to the order
        public int ProductId { get; set; } // Reference to the product
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price of the product at the time of order
    }

}
