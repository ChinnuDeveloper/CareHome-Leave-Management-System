using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using CareHomeLeaveManagement.Application.Entitlements.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class EntitlementRepository : IEntitlementRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public EntitlementRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }

        public async Task AddEntitlementAsync(IEnumerable<Entitlement> entitlements)
        {
            await _context.Entitlements.AddRangeAsync(entitlements);
        }

        public async Task<FiscalYear?> GetFiscalYearByIdAsync(int fiscalYearId)
        {
            return await _context.FiscalYears
                .FirstOrDefaultAsync(f => f.FiscalYearId == fiscalYearId);
        }
        public async Task DeleteEntitlementsByFiscalYearAsync(int fiscalYearId)
        {
            var entitlements = await _context.Entitlements
                 .Where(e => e.FiscalYearId == fiscalYearId)
                 .ToListAsync();

            _context.Entitlements.RemoveRange(entitlements);
        }

        public async Task<IEnumerable<FiscalYear>> GetFiscalYearAsync()
        { 
            return await _context.FiscalYears
                .Include(f=>f.GeneratedByEmployee)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CheckExistsByFiscalYearAsync(int fiscalYearId)
        {
            return await _context.Entitlements
                .AnyAsync(e=>e.FiscalYearId==fiscalYearId);
        }

        public async Task<IEnumerable<Entitlement>> GetAllEntitlementsAsync(int fiscalYearId)
        {
            return await _context.Entitlements
                 .Where(e=>e.FiscalYearId ==fiscalYearId)
                 .Include(e => e.Employee)
                 .Include(e => e.LeaveType)
                 .Include(e => e.FiscalYear)
                 .OrderBy(e => e.Employee.FirstName)
                 .ToListAsync();
        }

        public async Task<Entitlement?> GetEntitlementByIdAsync(int entitlementId)
        {
            return await _context.Entitlements
                .Include(e => e.Employee)
                .Include(e => e.LeaveType)
                .Include(e => e.FiscalYear)
                .FirstOrDefaultAsync(e => e.EntitlementId == entitlementId); 
        }

        public async Task AddEntitlementHistoryAsync(EntitlementHistory history)
        {
            await _context.EntitlementHistories.AddAsync(history);
        }

        public async Task<IEnumerable<EntitlementHistory>> GetEntitlementHistoryAsync(int employeeId, int leaveTypeId, int fiscalYearId)
        {
            return await _context.EntitlementHistories
                .Include(h => h.ModifiedByEmployee)
                .Include(h => h.Entitlement)
                .Where(h =>
                    h.Entitlement.EmployeeId == employeeId &&
                    h.Entitlement.LeaveTypeId == leaveTypeId &&
                    h.Entitlement.FiscalYearId == fiscalYearId)
                .OrderByDescending(h => h.ModifiedOn)
                .ToListAsync();
               
        }
    }
}
