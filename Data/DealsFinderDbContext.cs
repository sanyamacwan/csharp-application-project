using lab_5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab_5.Data
{
    public class DealsFinderDbContext : DbContext
    {
        public DealsFinderDbContext(DbContextOptions<DealsFinderDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<FoodDeliveryService> FoodDeliveryServices => Set<FoodDeliveryService>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // singular table names
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<FoodDeliveryService>().ToTable("FoodDeliveryService");
            modelBuilder.Entity<Subscription>().ToTable("Subscription");

            // composite key for Subscription
            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.CustomerId, s.FoodDeliveryServiceId });

            // relationships
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Subscriptions)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.FoodDeliveryService)
                .WithMany(f => f.Subscriptions)
                .HasForeignKey(s => s.FoodDeliveryServiceId);
        }
    }
}
