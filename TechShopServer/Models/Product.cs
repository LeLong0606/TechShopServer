using System.ComponentModel.DataAnnotations;

namespace TechShopServer.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string ProductCode { get; set; } = string.Empty;
        [StringLength(255)]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(500)]
        public string ProductDescription { get; set; } = string.Empty;
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
