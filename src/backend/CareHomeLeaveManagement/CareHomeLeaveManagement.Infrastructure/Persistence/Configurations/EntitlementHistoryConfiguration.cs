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
    public class EntitlementHistoryConfiguration : IEntityTypeConfiguration<EntitlementHistory>
    {
        public void Configure(EntityTypeBuilder<EntitlementHistory> builder)
        {
            builder.HasKey(t => t.EntitlementHistoryId);

            builder.Property(t => t.OldAllocationHrs)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.OldTakenHrs)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.NewAllocationHrs)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t=> t.NewTakenHrs)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.ModifiedOn)
                .IsRequired();

            builder.HasOne(t=>t.Entitlement)
                .WithMany()
                .HasForeignKey(t=>t.EntitlementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.ModifiedByEmployee)
                .WithMany()
                .HasForeignKey(t => t.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Reason)
                .HasMaxLength(500)
                .IsRequired(false);

        }
    }
}
