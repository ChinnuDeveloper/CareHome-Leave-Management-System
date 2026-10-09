using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; private set; }
        public int EmployeeId { get; private set; }

        public Employee Employee { get; private set; } = null!;
        public int LeaveTypeId { get; private set; }
        public LeaveType LeaveType { get; private set; } = null!;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string? Reason { get; private set; }
        public string? ManagerComment { get; private set; }
        public LeaveRequestStatus Status { get; private set; }
        public int? ApprovedBy { get; private set; }
        public Employee? Approver { get; private set; }
        public DateTime? ApprovedOn { get; private set; }
        public decimal HoursTaken { get; private set; } = 0;

        public ICollection<LeaveHistory> LeaveHistory { get; private set; } =new List<LeaveHistory>();

        private LeaveRequest()
        {

        }
        public LeaveRequest(
        int employeeId,
        int leaveTypeId,
        DateTime startDate,
        DateTime endDate,
        string? reason,
        decimal hoursTaken)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid employee.");

            if (leaveTypeId <= 0)
                throw new ArgumentException("Invalid leave type.");

            if (startDate.Date > endDate.Date)
                throw new ArgumentException("Start date cannot be after end date.");

            if (hoursTaken < 0)
                throw new ArgumentException("Hours taken cannot be negative.");

            EmployeeId = employeeId;
            LeaveTypeId=leaveTypeId;
            StartDate=startDate;
            EndDate=endDate;
            Reason=reason;
            HoursTaken = hoursTaken;

            Status = LeaveRequestStatus.Pending;
        }
        
        public void Approve(int managerId, string? managerComment)
        {
            if (Status != LeaveRequestStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending leave requests can be approved.");

            Status = LeaveRequestStatus.Approved;
            ApprovedBy = managerId;
            ApprovedOn = DateTime.UtcNow;
            ManagerComment = managerComment;
        }
        public void Reject(int managerId, string? managerComment)
        {
            if (Status != LeaveRequestStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending leave requests can be rejected.");

            Status = LeaveRequestStatus.Rejected;
            ApprovedBy = managerId;
            ApprovedOn = DateTime.UtcNow;
            ManagerComment= managerComment;
        }

        public void Cancel()
        {
            if (Status != LeaveRequestStatus.Pending)
                throw new InvalidOperationException("Only pending leave requests can be cancelled.");
            Status = LeaveRequestStatus.Cancelled;
        }
    }
}
