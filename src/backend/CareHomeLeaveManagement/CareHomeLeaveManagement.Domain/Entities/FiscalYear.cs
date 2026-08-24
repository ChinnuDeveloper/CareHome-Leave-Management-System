using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeLeaveManagement.Domain.Entities
{
    public class FiscalYear
    {
        public int FiscalYearId { get; private set; }
        public string Year { get; private set; } = string.Empty;
        public int GeneratedBy { get; private set; }
        public DateTime GeneratedOn { get; private set; }
    }
}
