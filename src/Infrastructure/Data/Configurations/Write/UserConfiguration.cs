using Core.Common.Constants;
using Core.Entities.Write;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations.Write
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Name);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Name); 

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Password);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Email);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.PhoneNumber); 

            builder.Property(u => u.Country)
                .IsRequired()
                .HasMaxLength(50); 

            builder.HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            builder.HasOne(u => u.Shop)
                .WithOne(s => s.User)
                .HasForeignKey<Shop>(s => s.UserId);
        }
    }
}
