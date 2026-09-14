using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.LeaveHistories.Interfaces
{
    public interface ILeaveHistoryRepository
    {
        Task AddAsync(LeaveHistory leaveHistory);
        Task<IEnumerable<LeaveHistory>> GetByLeaveRequestIdAsync(int leaveRequestId);
        Task<IEnumerable<LeaveHistory>> GetByEmployeeIdAsync(int employeeId);
    }
}
