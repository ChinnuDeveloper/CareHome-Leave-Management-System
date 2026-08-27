using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareHomeLeaveManagement.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.WeeklyHours)
                .HasPrecision(18, 2);

            builder.Property(e => e.Role)
                .IsRequired();

            builder.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(e=>e.DepartmentId) 
                .IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Manager)
                .WithMany(m => m.TeamMembers)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
