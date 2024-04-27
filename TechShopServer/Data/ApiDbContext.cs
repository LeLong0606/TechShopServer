using Microsoft.EntityFrameworkCore;
using TechShopServer.Models;

namespace TechShopServer.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProfileUser> ProfileUsers { get; set; }
    }
}
