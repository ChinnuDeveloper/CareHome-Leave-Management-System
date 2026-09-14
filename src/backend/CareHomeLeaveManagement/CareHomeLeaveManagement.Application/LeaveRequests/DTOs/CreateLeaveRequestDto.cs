using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.LeaveRequests.DTOs
{
    public class CreateLeaveRequestDto
    {
        public int employeeId { get; set; }
        public int leaveTypeId { get; set; }
        public DateTime startDate { get;set; }
        public DateTime endDate { get;set; }
        public string? reason { get; set; }
    }
}
