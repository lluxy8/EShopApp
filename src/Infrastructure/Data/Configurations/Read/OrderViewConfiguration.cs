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
    public class OrderViewConfiguration : BaseEntityConfiguration<OrderView>
    {
        public override void Configure(EntityTypeBuilder<OrderView> builder)
        {

            builder.HasIndex(o => o.UserId); 

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(o => o.Status)
                .IncludeProperties(o => new { o.ShopName, o.UserFullName });
        }
    }

}
