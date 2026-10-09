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
        Task<IEnumerable<EmployeeDto>> GetAllAsync(int fiscalYearId, int leaveTypeId);
        Task<EmployeeDto?> GetByIdAsync(int id, int fiscalYearId, int leaveTypeId);
        Task<EmployeeLeaveBalanceDto?> GetLeaveBalanceAsync(int employeeId,int leaveTypeId,int fiscalYearId);
        Task<EmployeeDto?> CreateAsync(CreateEmployeeRequest request);
        Task<EmployeeDto?> UpdateAsync(int id, int fiscalYearId, int leaveTypeId,UpdateEmployeeRequest request);
        Task<bool> DeleteAsync(int id, int fiscalYearId, int leaveTypeId);
    }
}
