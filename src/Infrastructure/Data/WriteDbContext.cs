using Core.Entities.Write;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly,
                WriteDbConfigurations);
        }

        private static bool WriteDbConfigurations(Type type) =>
            type.FullName?.Contains("Write") ?? false;
    }
}
