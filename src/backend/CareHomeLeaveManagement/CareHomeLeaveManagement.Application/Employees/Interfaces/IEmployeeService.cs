using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Employees.DTOs;

namespace CareHomeLeaveManagement.Application.Employees.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<EmployeeDto?> CreateAsync(CreateEmployeeRequest request);
        Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
