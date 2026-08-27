using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class EntitlementHistory
    {
        public int EntitlementHistoryId { get; private set; }
        public int EntitlementId { get; private set; }
        public Entitlement Entitlement { get; private set; } = null!;
        public decimal OldAllocationHrs { get; private set; }
        public decimal OldTakenHrs { get; private set; }
        public decimal NewAllocationHrs { get; private set; }
        public decimal NewTakenHrs { get; private set; }
        public string? Reason { get; private set; }
        public int ModifiedBy { get; private set; }
        public Employee ModifiedByEmployee { get; private set; } = null!;
        public DateTime ModifiedOn { get; private set; }
    }
}
