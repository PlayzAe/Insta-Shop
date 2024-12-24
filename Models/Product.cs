﻿namespace ECommerceAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string? ImageUrl { get; set; }  // URL for product image
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}