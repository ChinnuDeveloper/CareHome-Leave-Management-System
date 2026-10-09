using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public EmployeeRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            employee.Deactivate();
        }

        public async Task<List<Employee>> GetActiveEmployeeAsync()
        {
            return await _context.Employees
                .Where(e => e.Active)
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Entitlement> entitlements)
        {
            await _context.Entitlements.AddRangeAsync(entitlements);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(int fiscalYearId,int leaveTypeId)
        {
            return await _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.Manager)
                    .Include(e => e.Login)

                    .Include(e => e.Entitlements
                        .Where(ent =>
                            ent.FiscalYearId == fiscalYearId &&
                            ent.LeaveTypeId == leaveTypeId))
                        .ThenInclude(ent => ent.LeaveType)

                    .Include(e => e.Entitlements
                        .Where(ent =>
                            ent.FiscalYearId == fiscalYearId &&
                            ent.LeaveTypeId == leaveTypeId))
                        .ThenInclude(ent => ent.FiscalYear)

                    .OrderByDescending(e => e.EmployeeId)
                    .ToListAsync();
        } 
        public async Task<Employee?> GetByIdAsync(int id,int fiscalYearId,int leaveTypeId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .Include(e => e.Login)

                .Include(e => e.Entitlements
                    .Where(ent =>
                        ent.FiscalYearId == fiscalYearId &&
                        ent.LeaveTypeId == leaveTypeId))
                    .ThenInclude(ent => ent.LeaveType)

                .Include(e => e.Entitlements
                    .Where(ent =>
                        ent.FiscalYearId == fiscalYearId &&
                        ent.LeaveTypeId == leaveTypeId))
                    .ThenInclude(ent => ent.FiscalYear)

                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeLeaveBalanceDto?> GetLeaveBalanceAsync(int employeeId, int leaveTypeId, int fiscalYearId)
        {

            var employee = await _context.Employees
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

            if (employee == null)
                return null;

            var entitlement = await _context.Entitlements
                                .AsNoTracking()
                                .FirstOrDefaultAsync(e =>
                                e.EmployeeId == employeeId &&
                                e.LeaveTypeId == leaveTypeId &&
                                e.FiscalYearId == fiscalYearId);

            return new EmployeeLeaveBalanceDto
            {
                EmployeeId = employeeId,
                WeeklyHours = employee.WeeklyHours,
                EntitlementHours = entitlement.AllocatedHrs,
                UsedHours = entitlement.TakenHrs,
                RemainingHours = entitlement.AllocatedHrs - entitlement.TakenHrs
            };
        }

        
    }
}

