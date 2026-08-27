using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.LeaveTypes.DTOs
{
    public class LeaveTypeDto
    {
        public int LeaveTypeId { get;  set; }
        public string Name { get;  set; } = string.Empty;
        public bool RequiresEntitlement { get;  set; }

    }
}
