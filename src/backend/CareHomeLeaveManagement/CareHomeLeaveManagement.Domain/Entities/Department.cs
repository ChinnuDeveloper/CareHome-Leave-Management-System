using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; private set; }
        public string DepartmentName { get; private set; } = string.Empty;
        public int MaxConcurrentLeave { get; private set; }

        public ICollection<Employee> Employees { get; private set; }=new List<Employee>();
    }
}
