using Core.Common.Constants;
using Core.Entities.Read;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Read
{
    public class AddressViewConfiguration : BaseEntityConfiguration<AddressView>
    {
        public override void Configure(EntityTypeBuilder<AddressView> builder)
        {
            builder.Property(a => a.UserFullName)
                .HasMaxLength(MaxLengths.User.Name);

            builder.Property(a => a.AdressLine1)
                .HasMaxLength(MaxLengths.Address.AdressLine);
        }
    }
}
