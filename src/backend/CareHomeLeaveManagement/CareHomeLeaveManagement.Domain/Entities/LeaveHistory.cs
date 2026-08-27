using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class LeaveHistory
    {
        public int LeaveHistoryId { get; private set; }
        public int LeaveRequestId { get; private set; }

        public LeaveRequest LeaveRequest { get; private set; } = null;
        public DateTime OldStartDate { get; private set; }
        public DateTime OldEndDate { get; private set; }
        public DateTime NewStartDate { get; private set; }
        public DateTime NewEndDate { get; private set; }
        public string? Reason { get; private set; } = string.Empty;
        public LeaveRequestStatus Status { get; private set; }
        public int CreatedBy { get; private set; }
        public Employee CreatedByEmployee { get; private set; } = null;
        public DateTime CreatedOn { get; private set; }
    }
}
