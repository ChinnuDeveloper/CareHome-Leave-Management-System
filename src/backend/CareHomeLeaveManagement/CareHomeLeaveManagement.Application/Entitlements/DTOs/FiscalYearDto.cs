using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Entitlements.DTOs
{
    public class FiscalYearDto
    {
        public int FiscalYearId { get; set; }
        public string Year { get; set; } = string.Empty;
        public int? GeneratedBy { get;  set; }
        public string GeneratedByEmployee { get;  set; } = string.Empty;
        public DateTime? GeneratedOn { get;  set; }
    }
}
