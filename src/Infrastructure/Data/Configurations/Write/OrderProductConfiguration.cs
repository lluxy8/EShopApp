using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class OrderProductConfiguration : BaseEntityConfiguration<OrderProduct>
    {

        public override void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            base.Configure(builder);

            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.Property(op => op.Quantity)
                .IsRequired()
                .HasMaxLength(MaxLengths.OrderProduct.Quantity);

            builder.HasOne(op => op.Product)
                .WithMany()
                .HasForeignKey(op => op.ProductId);
        }
    }
}
