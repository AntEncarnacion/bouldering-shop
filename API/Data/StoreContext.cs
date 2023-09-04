using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class StoreContext : DbContext
{
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Product>().ToTable("Product");
        }
}
