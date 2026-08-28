using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class Login
    {
        public int LoginId { get; private set; }
        public int EmployeeId { get; private set; }

        public Employee Employee { get; private set; } = null!;
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        private Login()
        {

        }
        public Login(
        Employee employee,
        string userName,
        string passwordHash)
        {
            Employee = employee;
            UserName = userName;
            PasswordHash = passwordHash;
        }

        public void ChangeUserName(string userName)
        {
            UserName = userName;
        }

        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
