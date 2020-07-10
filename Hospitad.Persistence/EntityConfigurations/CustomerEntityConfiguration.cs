using Hospitad.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.EntityConfigurations
{
    internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).UseIdentityAlwaysColumn();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("Now()").ValueGeneratedOnAdd();
            builder.Property(q => q.UpdatedAt).HasDefaultValueSql("Now()").ValueGeneratedOnAddOrUpdate();
        }
    }
}
