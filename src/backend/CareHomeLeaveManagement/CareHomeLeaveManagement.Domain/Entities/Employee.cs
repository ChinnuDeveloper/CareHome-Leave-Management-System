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
        public DateTime DateOfBirth { get; private set; }
        public int DepartmentId { get; private set; }
        public decimal WeeklyHours { get; private set; }
        public EmployeeRole Role { get; private set; }
        public bool Active { get; private set; }
        public int? ManagerId { get; private set; }

        public void Deactivate()
        {
            Active = false;
        }
        public void UpdateWeeklyHours(decimal weeklyHours)
        {
            WeeklyHours = weeklyHours;
        }
        public Employee? Manager { get; private set; }
        public ICollection<Employee> TeamMembers { get; private set; } = new List<Employee>();
        public Department Department { get; private set; } = null!;

        public ICollection<LeaveRequest> LeaveRequests { get; private set; }=new List<LeaveRequest>();
        
        public ICollection<LeaveRequest> ApprovedLeaveRequests { get;private set; }=new List<LeaveRequest>();

        public Login? Login { get; private set; }
        private Employee()
        {

        }
        public Employee(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        int departmentId,
        decimal weeklyHours,
        EmployeeRole role,
        int? managerId)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DepartmentId = departmentId;
            WeeklyHours = weeklyHours;
            Role = role;
            ManagerId = managerId;
            Active = true;
        }

        public void Update(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        int departmentId,
        decimal weeklyHours,
        EmployeeRole role,
        int? managerId,
        bool active)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DepartmentId = departmentId;
            WeeklyHours = weeklyHours;
            Role = role;
            ManagerId = managerId;
            Active = active;
        }
    }
}
