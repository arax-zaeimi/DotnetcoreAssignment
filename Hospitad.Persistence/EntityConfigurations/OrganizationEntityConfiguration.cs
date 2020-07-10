using Hospitad.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.EntityConfigurations
{
    internal class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).UseIdentityAlwaysColumn();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("Now()").ValueGeneratedOnAdd();
            builder.Property(q => q.UpdatedAt).HasDefaultValueSql("Now()").ValueGeneratedOnAddOrUpdate();

            builder
                .HasOne(q => q.Customer)
                .WithMany(p => p.Organizations)
                .HasForeignKey(q => q.CustomerId);
        }
    }
}
