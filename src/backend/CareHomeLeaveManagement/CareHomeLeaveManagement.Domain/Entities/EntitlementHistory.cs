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
        public decimal OldAllocationHrs { get; private set; }
        public decimal OldTakenHrs { get; private set; }
        public decimal NewAllocationHrs { get; private set; }
        public decimal NewTakenHrs { get; private set; }
        public string? Reason { get; private set; }
        public int ModifiedBy { get; private set; }
        public DateTime ModifiedOn { get; private set; }
    }
}
