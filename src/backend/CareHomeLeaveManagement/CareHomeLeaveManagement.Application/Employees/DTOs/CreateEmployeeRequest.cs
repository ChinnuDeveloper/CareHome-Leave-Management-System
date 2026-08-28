using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Application.Employees.DTOs
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get;  set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public decimal WeeklyHours { get; set; }
        public EmployeeRole Role { get; set; } 
        public int? ManagerId { get; set; }
        public string EmailId { get;  set; } = string.Empty;
        public string Password { get;  set; } = string.Empty;

    }
}
