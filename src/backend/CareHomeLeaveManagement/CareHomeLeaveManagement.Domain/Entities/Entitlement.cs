using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class Entitlement
    {
        public int EntitlementId { get; private set; }
        public int EmployeeId { get; private set; }
        public int LeaveTypeId { get; private set; }
        public int FiscalYearId { get; private set; }
        public decimal AllocatedHrs { get; private set; }
        public decimal TakenHrs { get;private set; } 
    }
}
