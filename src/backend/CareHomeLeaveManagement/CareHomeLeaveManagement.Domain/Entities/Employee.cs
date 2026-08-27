using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Enums;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; private set; }
        public decimal WeeklyHours { get; private set; }
        public EmployeeRole Role { get; private set; }
        public bool Active { get; private set; }
        public int? ManagerId { get; private set; }

        public Employee? Manager { get; private set; }
        public ICollection<Employee> TeamMembers { get; private set; } = new List<Employee>();
        public Department Department { get; private set; } = null;

        public ICollection<LeaveRequest> LeaveRequests { get; private set; }=new List<LeaveRequest>();
        
        public ICollection<LeaveRequest> ApprovedLeaveRequests { get;private set; }=new List<LeaveRequest>();
    }
}
