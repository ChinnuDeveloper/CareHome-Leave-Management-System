using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Employees.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int fiscalYearId, int leaveTypeId);
        Task<Employee?> GetByIdAsync(int id, int fiscalYearId, int leaveTypeId);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Employee employee);
        Task<List<Employee>> GetActiveEmployeeAsync();
        Task<EmployeeLeaveBalanceDto?> GetLeaveBalanceAsync(int employeeId,int leaveTypeId,int fiscalYearId);
    }
}
