using Microsoft.EntityFrameworkCore;
using PizzaStoreAndManagement.DomainLayer.Models;
namespace PizzaStoreAndManagement.Persistance.Context
{
    public class PizzaStoreDBContext : DbContext
    {
        public DbSet<User> Users {  get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=192.168.0.10;user=Phone;password=MySQLW0rkB3nch!;database=pizzadb;",
                    ServerVersion.AutoDetect("server=192.168.0.10;user=Phone;password=MySQLW0rkB3nch!;database=pizzadb;"));
        }
        
    }
}
