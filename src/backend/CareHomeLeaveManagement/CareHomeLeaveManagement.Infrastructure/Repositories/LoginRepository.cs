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
    public class LoginRepository : ILoginRepository
    {
        private readonly CareHomeLeaveManagementDbContext _context;

        public LoginRepository(CareHomeLeaveManagementDbContext context)
        {
            _context = context;

        }
        public async Task AddAsync(Login login)
        {
           await _context.Logins.AddAsync(login);
        }

        public async Task<Login?> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.Logins
                 .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Login?> GetByUserNameAsync(string userName)
        {
            return await _context.Logins
                .FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
