using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Employees.DTOs
{
    public class EntitlementHistoryDto
    { 
        public int EntitlementHistoryId { get; set; }
        public decimal OldAllocationHrs { get; set; }
        public decimal OldTakenHrs { get; set; }
        public decimal NewAllocationHrs { get; set; }
        public decimal NewTakenHrs { get; set; }
        public string? Reason { get; set; }
        public int ModifiedBy { get; set; } 
        public DateTime ModifiedOn { get; set; }
    }
}
