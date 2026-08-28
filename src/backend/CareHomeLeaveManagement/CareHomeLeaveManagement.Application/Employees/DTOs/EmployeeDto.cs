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
    }
}
