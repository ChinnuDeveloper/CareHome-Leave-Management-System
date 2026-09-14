using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveRequests.DTOs;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.LeaveRequests.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<int> CreateLeaveRequestAsync(CreateLeaveRequestDto dto);
        Task<LeaveRequestDto?> GetByIdAsync(int id);
        Task<IEnumerable<LeaveRequestDto>> GetByEmployeeIdAsync(int employeeId);
        Task CancelLeaveRequestsAsync(int leaveRequestId, int employeeId);
        Task<IEnumerable<LeaveRequestDto>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task ApproveLeaveRequestAsync(ApproveLeaveRequestDto leaveRequest);
        Task RejectLeaveRequestAsync(RejectLeaveRequestDto leaveRequest);
    }
}
