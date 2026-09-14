using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Application.LeaveRequests.DTOs
{
    public class ApproveLeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int ApprovedBy { get;  set; }
        public string? ManagerComment { get; set; } 

    }
}
