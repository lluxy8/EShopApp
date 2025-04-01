using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(MaxLengths.Category.Name);

            builder.HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
