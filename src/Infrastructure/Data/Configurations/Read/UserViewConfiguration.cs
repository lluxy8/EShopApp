using Core.Common.Constants;
using Core.Entities.Read;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read
{
    public class UserViewConfiguration : BaseEntityConfiguration<UserView>
    {
        public override void Configure(EntityTypeBuilder<UserView> builder)
        {

            builder.Property(u => u.Email)
                .HasMaxLength(MaxLengths.User.Email);

            builder.Property(u => u.Name)
                .HasMaxLength(MaxLengths.User.Name);

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
