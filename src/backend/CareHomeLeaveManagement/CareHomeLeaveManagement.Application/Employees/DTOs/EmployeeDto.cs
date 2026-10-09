using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Application.Employees.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public decimal WeeklyHours { get; set; }
        public EmployeeRole Role { get; set; }
        public bool Active { get; set; }
        public int? ManagerId { get; set; } 
        public string? ManagerName { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int? EntitlementId { get; set; } 
        public string EmployeeName { get; set; } = null!;
        public int? LeaveTypeId { get; set; }
        public string LeaveType { get; set; } = null!;
        public int? FiscalYearId { get; set; }
        public string FiscalYear { get; set; } = null!; 
        public decimal? AllocatedHrs { get; set; }
        public decimal? TakenHrs { get; set; } 
    }
}
