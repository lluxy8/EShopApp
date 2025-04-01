using Core.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities.Write;

namespace Infrastructure.Data.Configurations.Write
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.UserId);

            builder.Property(a => a.AddressType)
                .IsRequired();

            builder.Property(a => a.AdressLine1)
                .HasMaxLength(MaxLengths.Address.AdressLine);

            builder.Property(a => a.AdressLine2)
                .HasMaxLength(MaxLengths.Address.AdressLine);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(MaxLengths.Address.City);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(MaxLengths.Address.Street);

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(MaxLengths.Address.ZipCode);

            builder.HasOne(a => a.User)
                .WithMany(x => x.Addresses)
                .HasForeignKey(a => a.UserId);
        }
    }
}
