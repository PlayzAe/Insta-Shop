namespace ECommerceAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public List<OrderItem>? OrderItems { get; set; } // List of products in the order
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } // example, "Pending", "Shipped", "Delivered", "Cancelled"
        public DateTime OrderDate { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PaymentStatus { get; set; } // example, "Paid", "Pending"
    }


}
