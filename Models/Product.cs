namespace ECommerceAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }  // Primary key for Product
        public string? Name { get; set; }  // Name of the product
        public string? Description { get; set; }  // Description of the product
        public decimal Price { get; set; }  // Price of the product
        public string? Category { get; set; }  // Product category
        public int Stock { get; set; }  // Available stock quantity
    }

}
