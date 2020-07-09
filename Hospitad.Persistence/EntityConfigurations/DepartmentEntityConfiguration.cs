using Hospitad.Domain.Organizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Persistence.EntityConfigurations
{
    internal class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Id).UseIdentityAlwaysColumn();

            builder
                .HasOne(q => q.Organization)
                .WithMany(p => p.Departments)
                .HasForeignKey(q => q.OrganizationId);

            builder
                .HasOne(q => q.ParentDepartment)
                .WithMany(p => p.SubDepartments)
                .HasForeignKey(q => q.ParentDepartmentId);


        }
    }
}
