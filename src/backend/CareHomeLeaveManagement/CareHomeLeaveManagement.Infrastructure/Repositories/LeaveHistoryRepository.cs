using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveHistories.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class LeaveHistoryRepository : ILeaveHistoryRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public LeaveHistoryRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LeaveHistory leaveHistory)
        {
            await _context.LeaveHistories.AddAsync(leaveHistory);
        }

        public async Task<IEnumerable<LeaveHistory>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveHistories
                .Include(x => x.CreatedByEmployee)
                .Include(x => x.LeaveRequest)
                .Where(x => x.LeaveRequest.EmployeeId == employeeId)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<LeaveHistory>> GetByLeaveRequestIdAsync(int leaveRequestId)
        {
            return await _context.LeaveHistories
                .Include(x=>x.CreatedByEmployee)
                .Where(x => x.LeaveRequestId == leaveRequestId)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();
        }
    }
}
