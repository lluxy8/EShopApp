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
    public class AdminViewConfiguration : BaseEntityConfiguration<AdminView>
    {
        public override void Configure(EntityTypeBuilder<AdminView> builder)
        {
            builder.Property(a => a.Username)
                .HasMaxLength(MaxLengths.User.Name);

            builder.Property(a => a.Role)
                .HasMaxLength(50);

            builder.HasIndex(a => a.Username).IsUnique();
        }
    }
}
