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
    public class ShopViewConfiguration : BaseEntityConfiguration<ShopView>
    {
        public override void Configure(EntityTypeBuilder<ShopView> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(MaxLengths.Shop.Name);

            builder.Property(s => s.Description)
                .HasMaxLength(MaxLengths.Shop.Description);

            builder.Property(s => s.Name).HasColumnName("ShopName");
            builder.Property(s => s.UserId).HasColumnName("OwnerId");

            builder.HasIndex(s => s.UserId);
            builder.HasIndex(s => s.Name);
        }
    }
}
