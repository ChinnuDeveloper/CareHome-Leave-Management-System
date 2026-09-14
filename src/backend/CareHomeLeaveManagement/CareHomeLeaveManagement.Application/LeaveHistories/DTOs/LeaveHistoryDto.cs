using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Application.LeaveHistories.DTOs
{
    public class LeaveHistoryDto
    {
        public int LeaveHistoryId { get; set; }
        public int LeaveRequestId { get;  set; }
        public DateTime OldStartDate { get;  set; }
        public DateTime OldEndDate { get;  set; }
        public DateTime NewStartDate { get;  set; }
        public DateTime NewEndDate { get;  set; }
        public string? Reason { get;  set; } 
        public LeaveRequestStatus Status { get;  set; }
        public int CreatedBy { get;  set; }
        public string CreatedByName { get;  set; } = string.Empty;
        public DateTime CreatedOn { get;  set; }

    }
}
