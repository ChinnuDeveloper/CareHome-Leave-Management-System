using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.LeaveRequests.Interfaces
{
    public interface ILeaveRequestRepository
    {
        Task AddAsync(LeaveRequest leaveRequest);
        Task<LeaveRequest?> GetByIdAsync(int id);
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);
        Task UpdateAsync(LeaveRequest leaveRequest);
        Task<IEnumerable<LeaveRequest>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
