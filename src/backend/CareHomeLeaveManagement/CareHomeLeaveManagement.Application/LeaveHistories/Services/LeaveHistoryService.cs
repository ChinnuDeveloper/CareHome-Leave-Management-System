using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.LeaveHistories.DTOs;
using CareHomeLeaveManagement.Application.LeaveHistories.Interfaces;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;

namespace CareHomeLeaveManagement.Application.LeaveHistories.Services
{
    public class LeaveHistoryService : ILeaveHistoryService
    {
        private readonly ILeaveHistoryRepository _repository;
        public LeaveHistoryService(ILeaveHistoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<LeaveHistoryDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var histories= await _repository.GetByEmployeeIdAsync(employeeId);

            return histories.Select(x => new LeaveHistoryDto
            {
                LeaveHistoryId = x.LeaveHistoryId,
                LeaveRequestId = x.LeaveRequestId,
                OldStartDate = x.OldStartDate,
                OldEndDate = x.OldEndDate,
                NewStartDate = x.NewStartDate,
                NewEndDate = x.NewEndDate,
                Reason = x.Reason,
                Status = x.Status,
                CreatedBy = x.CreatedBy,
                CreatedByName = x.CreatedByEmployee.FirstName + " " + x.CreatedByEmployee.LastName,
                CreatedOn = x.CreatedOn
            });
        }

        public async Task<IEnumerable<LeaveHistoryDto>> GetByLeaveRequestIdAsync(int leaveRequestId)
        {
            var histories = await _repository.GetByLeaveRequestIdAsync(leaveRequestId);

            return histories.Select(x => new LeaveHistoryDto
            {
                 LeaveHistoryId=x.LeaveHistoryId,
                 LeaveRequestId=x.LeaveRequestId,
                 OldStartDate=x.OldStartDate,
                 OldEndDate=x.OldEndDate,
                 NewStartDate=x.NewStartDate,
                 NewEndDate=x.NewEndDate,
                 Reason=x.Reason,
                 Status=x.Status,
                 CreatedBy=x.CreatedBy,
                 CreatedByName=x.CreatedByEmployee.FirstName + " "+ x.CreatedByEmployee.LastName,
                 CreatedOn=x.CreatedOn
            });
        } 
    }
}
