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
    public class ProductViewConfiguration : BaseEntityConfiguration<ProductView>
    {
        public override void Configure(EntityTypeBuilder<ProductView> builder)
        {
            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.ShopId);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
