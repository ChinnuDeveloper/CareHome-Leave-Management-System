using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.Employees.DTOs
{
    public class EmployeeLeaveBalanceDto
    {
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int FiscalYearId { get; set; }

        public decimal WeeklyHours { get; set; }
        public decimal EntitlementHours { get; set; }
        public decimal UsedHours { get; set; }
        public decimal RemainingHours { get; set; }
    }
}
