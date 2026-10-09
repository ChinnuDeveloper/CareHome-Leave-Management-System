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
    public class EntitlementConfiguration : IEntityTypeConfiguration<Entitlement>
    {
        public void Configure(EntityTypeBuilder<Entitlement> builder)
        {
            builder.HasKey(e => e.EntitlementId);

            builder.Property(e => e.AllocatedHrs)
                .IsRequired()
                .HasPrecision(18,2);

            builder.Property(e => e.TakenHrs)
                .IsRequired()
                .HasPrecision(18,2);

            builder.HasOne(e => e.Employee)
                .WithMany(e=>e.Entitlements)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.LeaveType)
                .WithMany()
                .HasForeignKey(e => e.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.FiscalYear)
                .WithMany()
                .HasForeignKey(e => e.FiscalYearId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
