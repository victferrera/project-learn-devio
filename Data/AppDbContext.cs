using AppMercadoBasico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMercadoBasico.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){}

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CustomerProduct> CustomersProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerProduct>()
                .HasKey(x => new { x.ProductId, x.CustomerId });

            modelBuilder.Entity<CustomerProduct>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.CustomerProduct)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<CustomerProduct>()
                .HasOne(x => x.Product)
                .WithMany(x => x.CustomerProduct)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
