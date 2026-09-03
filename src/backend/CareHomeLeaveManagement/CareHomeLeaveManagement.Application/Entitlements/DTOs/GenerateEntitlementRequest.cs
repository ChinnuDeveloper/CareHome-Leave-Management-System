using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Application.Entitlements.DTOs
{
    public class GenerateEntitlementRequest
    {
        public int FiscalYearId { get; set; }
        public int GeneratedBy { get; set; }
        public DateTime GeneratedOn { get; set; }
        public decimal CalculationRule { get; set; }
    }
}
