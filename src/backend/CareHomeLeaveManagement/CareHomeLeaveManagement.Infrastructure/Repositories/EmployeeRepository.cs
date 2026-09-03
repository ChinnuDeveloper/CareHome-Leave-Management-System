using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await _context.Entitlements .AddRangeAsync(entitlements);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e=>e.Department)
                .Include(e=>e.Manager) 
                .Include(e=>e.Login)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .Include(e => e.Login)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
