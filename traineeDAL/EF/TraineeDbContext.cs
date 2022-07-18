using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using traineeDAL.Entities;

namespace traineeDAL.EF
{
    public class TraineeDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public TraineeDbContext(DbContextOptions<TraineeDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(p => p.Orders)
                .WithOne(b => b.Customer!)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.Customer)
                .WithMany(b => b.Orders)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
