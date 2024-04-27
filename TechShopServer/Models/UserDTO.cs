using System.ComponentModel.DataAnnotations;

namespace TechShopServer.Models
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public required string UserName { get; set; } = string.Empty;
        [StringLength(100)]
        public required string Password { get; set; } = string.Empty;
    }
}
