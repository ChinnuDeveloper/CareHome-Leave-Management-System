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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepartmentId);

            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(100);           

            builder.Property(d => d.MaxConcurrentLeave)
                .IsRequired();

            builder.HasMany(d=>d.Employees)
                .WithOne(e=>e.Department)
                .HasForeignKey(e=>e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new 
               {
                   DepartmentId = 1,
                   DepartmentName = "Carer",
                   MaxConcurrentLeave=2
               },
               new
               {
                   DepartmentId = 2,
                   DepartmentName= "Domestic Worker",
                   MaxConcurrentLeave=1
               },
               new
               {
                   DepartmentId = 3,
                   DepartmentName= "Nurse",
                   MaxConcurrentLeave=1
               },
               new
               { 
                   DepartmentId = 4,
                   DepartmentName= "Kitchen Staff",
                   MaxConcurrentLeave=1

               }
               );
        }
    }
}
