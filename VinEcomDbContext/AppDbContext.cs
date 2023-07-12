using Microsoft.EntityFrameworkCore;
using VinEcomDomain.Model;

namespace VinEcomDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Building> Building { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Shipper> Shipper { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreStaff> StoreStaff { get; set; }
        public DbSet<User> User { get; set; }
    }
}