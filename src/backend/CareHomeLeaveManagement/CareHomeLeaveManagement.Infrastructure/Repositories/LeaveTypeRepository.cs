using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public LeaveTypeRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LeaveType>> GetAllAsync()
        {
            return await _context.LeaveTypes
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<List<LeaveType>> GetEntitlementLeaveTypesAsync()
        {
            return await _context.LeaveTypes
                 .Where(l => l.RequiresEntitlement)
                 .ToListAsync();
        }
    }
}
