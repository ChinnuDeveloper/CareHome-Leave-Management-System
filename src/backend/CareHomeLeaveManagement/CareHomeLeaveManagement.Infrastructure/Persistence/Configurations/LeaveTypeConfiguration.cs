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
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasKey(l=>l.LeaveTypeId);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.RequiresEntitlement)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasData(
                new
                {
                    LeaveTypeId=1,
                    Name= "Annual Leave",
                    RequiresEntitlement=true
                },
                new
                {
                    LeaveTypeId=2,
                    Name= "Birthday Leave",
                    RequiresEntitlement=false
                });
        }
    }
}
