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
        public int LeaveTypeId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string? Reason { get; private set; }
        public string? ManagerComment { get; private set; }
        public LeaveRequestStatus Status { get; private set; }
        public int? ApprovedBy { get; private set; }
        public DateTime? ApprovedOn { get; private set; }
    }
}
