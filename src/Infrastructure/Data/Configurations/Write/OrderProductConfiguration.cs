using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.Property(op => op.Quantity)
                .IsRequired()
                .HasAnnotation("Range", new { Min = 1, MaxLengths.OrderProduct.Quantity });

            builder.HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);
        }
    }
}
