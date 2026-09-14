using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.LeaveRequests.DTOs
{
    public class LeaveRequestDto
    {
        public int LeaveRequestId { get;  set; }
        public int EmployeeId { get;  set; }
        public string Employee { get;  set; } = null!;
        public int LeaveTypeId { get;  set; }
        public string LeaveType { get;  set; } = null!;
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public string? Reason { get;  set; }
        public string? ManagerComment { get;  set; }
        public string Status { get; set; } = null!;
        public DateTime? ApprovedOn { get;  set; }
    }
}
