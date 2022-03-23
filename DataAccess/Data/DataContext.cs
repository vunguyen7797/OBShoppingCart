using Microsoft.EntityFrameworkCore;
using OBShoppingCart.Models;

namespace OBShoppingCart.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
