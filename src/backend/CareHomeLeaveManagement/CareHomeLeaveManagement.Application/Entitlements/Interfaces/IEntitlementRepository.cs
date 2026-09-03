using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Domain.Entities;

namespace CareHomeLeaveManagement.Application.Entitlements.Interfaces
{
    public interface IEntitlementRepository
    {
        Task<IEnumerable<FiscalYear>> GetFiscalYearAsync();
        Task<FiscalYear?> GetFiscalYearByIdAsync(int fiscalYearId);
        Task AddEntitlementAsync(IEnumerable<Entitlement> entitlements);
        Task DeleteEntitlementsByFiscalYearAsync(int fiscalYearId);
        Task<bool> CheckExistsByFiscalYearAsync(int fiscalYearId);
        Task<IEnumerable<Entitlement>> GetAllEntitlementsAsync(int fiscalYearId);
        Task<Entitlement?> GetEntitlementByIdAsync(int entitlementId);
        Task AddEntitlementHistoryAsync(EntitlementHistory history);
        Task<IEnumerable<EntitlementHistory>> GetEntitlementHistoryAsync(int employeeId, int leaveTypeId, int fiscalYearId);
    }
}
