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
        public Employee Employee { get; private set; } = null!;
        public int LeaveTypeId { get; private set; }
        public LeaveType LeaveType { get; private set; } = null!;
        public int FiscalYearId { get; private set; }
        public FiscalYear FiscalYear { get; private set; } = null!;
        public decimal AllocatedHrs { get; private set; }
        public decimal TakenHrs { get;private set; } 

        public void Update(decimal allocatedHrs, decimal takenHrs)
        {
            AllocatedHrs = allocatedHrs;
            TakenHrs=takenHrs;
        }

        private Entitlement()
        {

        }
        public Entitlement( 
            int employeeId,  
            int leaveTypeId,  
            int fiscalYearId, 
            decimal allocatedHrs 
            )
        {
            EmployeeId = employeeId;
            LeaveTypeId = leaveTypeId;
            FiscalYearId = fiscalYearId;
            AllocatedHrs = allocatedHrs;
            TakenHrs = 0;
        }
    }
}
