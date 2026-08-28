using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Employees.Interfaces
{
    public interface ILoginRepository
    {
        Task AddAsync(Login login);
        Task<Login?> GetByUserNameAsync(string userName);

        Task<Login?> GetByEmployeeIdAsync(int employeeId);
    }
}
