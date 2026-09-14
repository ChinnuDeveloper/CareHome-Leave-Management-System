using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Common.Interfaces;
using CareHomeLeaveManagement.Application.LeaveHistories.Interfaces;
using CareHomeLeaveManagement.Application.LeaveRequests.DTOs;
using CareHomeLeaveManagement.Application.LeaveRequests.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.LeaveRequests.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveHistoryRepository _leaveHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, ILeaveHistoryRepository leaveHistoryRepository, IUnitOfWork unitOfWork)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveHistoryRepository = leaveHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ApproveLeaveRequestAsync(ApproveLeaveRequestDto leaveRequest)
        {
            var leaveRequests = await _leaveRequestRepository.GetByIdAsync(leaveRequest.LeaveRequestId);

            if (leaveRequests == null)
                throw new KeyNotFoundException("Leave request not found.");

            leaveRequests.Approve(leaveRequest.ApprovedBy, leaveRequest.ManagerComment); 

            await _leaveRequestRepository.UpdateAsync(leaveRequests);

            await AddHistoryAsync(leaveRequests, leaveRequest.ApprovedBy, leaveRequests.StartDate, leaveRequests.EndDate); 

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task CancelLeaveRequestsAsync(int leaveRequestId, int employeeId)
        {
           var leaveRequest=await _leaveRequestRepository.GetByIdAsync(leaveRequestId);

            if (leaveRequest == null)
                throw new KeyNotFoundException("Leave request not found.");

            if (leaveRequest.EmployeeId != employeeId)
                throw new UnauthorizedAccessException("You can only cancel your own leave request.");

            leaveRequest.Cancel();

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            await AddHistoryAsync(leaveRequest, employeeId, leaveRequest.StartDate, leaveRequest.EndDate);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> CreateLeaveRequestAsync(CreateLeaveRequestDto dto)
        {
            var leaveRequest =new LeaveRequest(
                dto.employeeId,
                dto.leaveTypeId,
                dto.startDate,
                dto.endDate,
                dto.reason);

            await _leaveRequestRepository.AddAsync(leaveRequest);

            await _unitOfWork.SaveChangesAsync();

            return leaveRequest.LeaveRequestId;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var leaveRequests = await _leaveRequestRepository.GetByDateRangeAsync(fromDate, toDate);

            return leaveRequests.Select(lr => new LeaveRequestDto
            {
                LeaveRequestId = lr.LeaveRequestId,
                EmployeeId = lr.EmployeeId,
                Employee = $"{lr.Employee.FirstName} {lr.Employee.LastName}",
                LeaveTypeId = lr.LeaveTypeId,
                LeaveType = lr.LeaveType.Name,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Reason = lr.Reason,
                ManagerComment = lr.ManagerComment,
                Status = lr.Status.ToString(),
                ApprovedOn = lr.ApprovedOn
            });

        }

        public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeIdAsync(int employeeId)
        {
            var leaveRequests = await _leaveRequestRepository.GetByEmployeeIdAsync(employeeId);

            return leaveRequests.Select(lr => new LeaveRequestDto
            {
                LeaveRequestId = lr.LeaveRequestId,
                EmployeeId = lr.EmployeeId,
                Employee = $"{lr.Employee.FirstName} {lr.Employee.LastName}",
                LeaveTypeId = lr.LeaveTypeId,
                LeaveType = lr.LeaveType.Name,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                Reason = lr.Reason,
                ManagerComment = lr.ManagerComment,
                Status = lr.Status.ToString(),
                ApprovedOn = lr.ApprovedOn
            });
        }
        public async Task<LeaveRequestDto?> GetByIdAsync(int id)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);

            if (leaveRequest == null)
                return null;

            return new LeaveRequestDto
            {
               LeaveRequestId = leaveRequest.LeaveRequestId,
               EmployeeId = leaveRequest.EmployeeId,
               Employee = $"{leaveRequest.Employee.FirstName} {leaveRequest.Employee.LastName}",
               LeaveTypeId = leaveRequest.LeaveTypeId,
               LeaveType = leaveRequest.LeaveType.Name,
               StartDate = leaveRequest.StartDate,
               EndDate = leaveRequest.EndDate,
               Reason = leaveRequest.Reason,
               ManagerComment = leaveRequest.ManagerComment,
               Status = leaveRequest.Status.ToString(),
               ApprovedOn= leaveRequest.ApprovedOn
            };
        }

        public async Task RejectLeaveRequestAsync(RejectLeaveRequestDto rejectRequest)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(rejectRequest.LeaveRequestId);

            if (leaveRequest == null)
                throw new KeyNotFoundException(
                    "Leave request not found.");

            leaveRequest.Reject(rejectRequest.ApprovedBy, rejectRequest.ManagerComment);
             
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            await AddHistoryAsync(leaveRequest, rejectRequest.ApprovedBy, leaveRequest.StartDate, leaveRequest.EndDate);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task AddHistoryAsync(LeaveRequest leaveRequest,
            int createdBy,DateTime newStartdate,DateTime newEndDate)
        {
            var history = new LeaveHistory(
                   leaveRequest.LeaveRequestId,
                   leaveRequest.StartDate,
                   leaveRequest.EndDate,
                   newStartdate,
                   newEndDate,
                   leaveRequest.ManagerComment,
                   leaveRequest.Status,
                   createdBy
                   );

            await _leaveHistoryRepository.AddAsync(history);

        }
    }
}
