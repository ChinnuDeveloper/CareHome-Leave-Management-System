using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.Entitlements.DTOs
{
    public class UpdateEntitlementRequest
    {
        public decimal AllocatedHrs { get; set; }
        public decimal TakenHrs { get; set; }
        public decimal WeeklyHours { get; set; }
        public string? Reason { get; set; }
        public int ModifiedBy { get; set; }

    }
}
