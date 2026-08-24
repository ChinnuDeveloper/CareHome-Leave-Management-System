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
        public string UserName { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; }= string.Empty;
    }
}
