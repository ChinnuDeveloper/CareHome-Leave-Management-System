using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Entitlements.DTOs
{
    public class EntitlementDto
    {
        public int EntitlementId { get; set; }
        public int EmployeeId { get;  set; }
        public string EmployeeName { get; set; } = null!;
        public int LeaveTypeId { get; set; }
        public string LeaveType { get; set; } = null!;
        public int FiscalYearId { get; set; }
        public string FiscalYear { get; set; } = null!;
        public decimal WeeklyHours { get; set; }
        public decimal AllocatedHrs { get; set; }
        public decimal TakenHrs { get; set; }
    }
}
