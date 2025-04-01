using Core.Entities.Read;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options) { }

        public DbSet<AddressView> Addresses { get; set; }
        public DbSet<AdminView> Admins { get; set; }
        public DbSet<CategoryView> Categories { get; set; }
        public DbSet<OrderView> Orders { get; set; }
        public DbSet<ProductView> Products { get; set; }
        public DbSet<ShopView> Shops { get; set; }
        public DbSet<UserView> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadDbContext).Assembly,
                WriteDbConfigurations);
        }

        private static bool WriteDbConfigurations(Type type) =>
            type.FullName?.Contains("Read") ?? false;
    }
}
