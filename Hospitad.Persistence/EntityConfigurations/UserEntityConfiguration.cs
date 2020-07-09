using Hospitad.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.EntityConfigurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).UseIdentityAlwaysColumn();
            builder.HasOne(q => q.Customer).WithMany(u => u.Users).HasForeignKey(q => q.CustomerId);
            
            builder.Property(q => q.Username).IsRequired();
            builder.Property(q => q.Password).IsRequired();
            builder.Property(q => q.Email).IsRequired();
            builder.Property(q => q.UserRole).IsRequired();
        }
    }
}
