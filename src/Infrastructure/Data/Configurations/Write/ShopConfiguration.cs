using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class ShopConfiguration : BaseEntityConfiguration<Shop>
    {
        public override void Configure(EntityTypeBuilder<Shop> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(MaxLengths.Shop.Name);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(MaxLengths.Shop.Description);

            builder.HasOne(s => s.User)
                .WithOne(u => u.Shop)
                .HasForeignKey<Shop>(s => s.UserId);

            builder.HasMany(s => s.Products)
                .WithOne(p => p.Shop)
                .HasForeignKey(p => p.ShopId);

            builder.HasMany(s => s.Orders)
                .WithOne(o => o.Shop)
                .HasForeignKey(o => o.ShopId);
        }
    }
}
