using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class LeaveType
    {
        public int LeaveTypeId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public bool RequiresEntitlement { get; private set; } 
    }
}
