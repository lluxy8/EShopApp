using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class AdminConfiguration : BaseEntityConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Name);

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Password);

            builder.Property(a => a.Role)
                .IsRequired();
        }
    }
}
