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
    public class FiscalYearConfiguration : IEntityTypeConfiguration<FiscalYear>
    {
        public void Configure(EntityTypeBuilder<FiscalYear> builder)
        {
            builder.HasKey(f => f.FiscalYearId);

            builder.Property(f => f.Year)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(f => f.GeneratedByEmployee)
                .WithMany()
                .HasForeignKey(f => f.GeneratedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(f => f.GeneratedOn)
                .IsRequired(false);

            builder.HasData(
                new
                {
                    FiscalYearId = 1,
                    Year = "2026-2027"
                },
                new
                {
                    FiscalYearId = 2,
                    Year = "2027-2028"
                },
                new
                {
                    FiscalYearId = 3,
                    Year = "2028-2029"
                },
                new
                {
                    FiscalYearId = 4,
                    Year = "2029-2030"
                },
                new
                {
                    FiscalYearId = 5,
                    Year = "2030-2031"
                }

                );
            }
        }
    }

