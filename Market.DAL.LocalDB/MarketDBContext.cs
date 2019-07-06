using System;
using Microsoft.EntityFrameworkCore;
using Market.Entities;
using Market.Abstract;

namespace Market.DAL.LocalDB
{
    public class MarketDBContext : DbContext, IDALContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Unit> Units { get; set; }
        
        public MarketDBContext(DbContextOptions<MarketDBContext> options) : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<User>()
                    .Property(s => s.Login).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<User>()
                    .Property(s => s.Password).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Product>()
                    .Property(s => s.ShortTitle).HasMaxLength(300);
            modelBuilder.Entity<Product>()
                    .Property(s => s.Title).HasMaxLength(1000);
        }
    }
}
