using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CareHomeLeaveManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public DepartmentRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                 .AsNoTracking()
                 .ToListAsync();
        }
    }
}
