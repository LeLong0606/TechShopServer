using System.ComponentModel.DataAnnotations;

namespace TechShopServer.Models
{
    public class ProfileUser
    {
        [Key]
        public int Id { get; set; }
        [StringLength(75)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(75)]
        public string LastName {  get; set; } = string.Empty;
        [StringLength(75)]
        public string DisplayName {  get; set; } = string.Empty;
        [StringLength(20)]
        public string PhoneNo {  get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        [StringLength(255)]
        public string Address {  get; set; } = string.Empty;
    }
}
