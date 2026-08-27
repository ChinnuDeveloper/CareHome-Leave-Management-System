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
    public class LeaveHistoryConfiguration : IEntityTypeConfiguration<LeaveHistory>
    {
        public void Configure(EntityTypeBuilder<LeaveHistory> builder)
        {
            builder.HasKey(h=>h.LeaveHistoryId);

            builder.Property(h => h.OldStartDate)
                .IsRequired();

            builder.Property(h => h.OldEndDate)
                .IsRequired();

            builder.Property(h => h.NewStartDate)
                .IsRequired();

            builder.Property(h => h.NewEndDate)
                .IsRequired();

            builder.Property(h => h.CreatedOn)
                .IsRequired();

            builder.HasOne(h => h.LeaveRequest)
                .WithMany(l => l.LeaveHistory)
                .HasForeignKey(h => h.LeaveRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(h=>h.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(h=>h.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
