using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveHistories.DTOs;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.LeaveHistories.Interfaces
{
    public interface ILeaveHistoryService
    {
        Task<IEnumerable<LeaveHistoryDto>> GetByLeaveRequestIdAsync(int leaveRequestId);
        Task<IEnumerable<LeaveHistoryDto>> GetByEmployeeIdAsync(int employeeId);

    }
}
