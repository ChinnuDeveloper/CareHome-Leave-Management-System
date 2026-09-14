using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveRequests.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public LeaveRequestRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
        } 
        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(l => l.LeaveRequestId == id);
        }
        public async Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveRequests
                .Include(l => l.Employee)
                .Include(x => x.LeaveType)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(x => x.StartDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
        }

        public async Task<IEnumerable<LeaveRequest>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return await _context.LeaveRequests
                 .Include(lr => lr.Employee)
                 .Include(lr => lr.LeaveType)
                 .Where(lr => lr.StartDate <= toDate && lr.EndDate >= fromDate)
                 .ToListAsync();
        }
    }
}
