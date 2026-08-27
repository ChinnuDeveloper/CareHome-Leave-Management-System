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
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.HasKey(l=> l.LoginId);

            builder.Property(l => l.UserName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(l => l.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);


            builder.HasIndex(l => l.UserName)
                .IsUnique();

            builder.HasOne<Employee>()
                .WithOne()
                .HasForeignKey<Login>(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
