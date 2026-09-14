using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.LeaveRequests.DTOs
{
    public class RejectLeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int ApprovedBy { get; set; }
        [Required]
        public string ManagerComment { get; set; }
    }
}
