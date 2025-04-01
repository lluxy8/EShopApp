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
    public class CategoryViewConfiguration : BaseEntityConfiguration<CategoryView>
    {
        public override void Configure(EntityTypeBuilder<CategoryView> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(MaxLengths.Category.Name)
                .IsRequired();

            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
