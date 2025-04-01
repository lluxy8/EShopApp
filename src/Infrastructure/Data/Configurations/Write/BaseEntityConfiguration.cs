﻿using Core.Common.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Write
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedDate)
                   .IsRequired();
        }
    }
}
