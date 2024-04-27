using System.ComponentModel.DataAnnotations;

namespace TechShopServer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string UserName { get; set; } = string.Empty;
        [StringLength(1024)]
        public string PasswordHash { get; set; } = string.Empty;

    }
}
