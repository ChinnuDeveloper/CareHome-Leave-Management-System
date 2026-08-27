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
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.HasKey(l => l.LeaveRequestId);

            builder.Property(l => l.StartDate)
                .IsRequired();

            builder.Property(l => l.EndDate)
                .IsRequired();

            builder.Property(l => l.Reason)
                .HasMaxLength(250);

            builder.Property(l => l.ManagerComment)
                .HasMaxLength(250);

            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(l => l.EmployeeId)
                .IsRequired();

            builder.HasOne(l=>l.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l=>l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(l => l.LeaveTypeId)
                .IsRequired();

            builder.HasOne(l=>l.LeaveType)
                .WithMany(lt=>lt.LeaveRequests)
                .HasForeignKey(l=>l.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l=>l.Approver)
                .WithMany(e=>e.ApprovedLeaveRequests)
                .HasForeignKey(l=>l.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
