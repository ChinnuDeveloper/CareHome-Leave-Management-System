using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Infrastructure.Persistence
{
    public class CareHomeLeaveManagementDbContext : DbContext
    {
        public CareHomeLeaveManagementDbContext(
            DbContextOptions<CareHomeLeaveManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Login> Logins => Set<Login>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<LeaveType> LeaveTypes => Set<LeaveType>();
        public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
        public DbSet<LeaveHistory> LeaveHistories => Set<LeaveHistory>();
        public DbSet<FiscalYear> FiscalYears=>Set<FiscalYear>();
        public DbSet<Entitlement> Entitlements => Set<Entitlement>();
        public DbSet<EntitlementHistory> EntitlementHistories => Set<EntitlementHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CareHomeLeaveManagementDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


    }
}
