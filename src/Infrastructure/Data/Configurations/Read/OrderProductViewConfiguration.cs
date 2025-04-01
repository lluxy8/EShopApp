using Core.Common.Constants;
using Core.Entities.Read;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations.Read
{
    public class OrderProductViewConfiguration : BaseEntityConfiguration<OrderProductView>
    {
        public override void Configure(EntityTypeBuilder<OrderProductView> builder)
        {
            builder.Property(op => op.ProductName)
                .HasMaxLength(MaxLengths.Product.Name);

            builder.Property(op => op.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(op => op.OrderId);
        }
    }

}
