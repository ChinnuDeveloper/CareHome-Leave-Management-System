using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Entitlements.DTOs;

namespace CareHomeLeaveManagement.Application.Entitlements.Interfaces
{
    public interface IEntitlementService
    {
        Task<IEnumerable<FiscalYearDto>> GetFiscalYearAsync();
        Task GenerateEntitlementsAsync(GenerateEntitlementRequest request);
        Task<bool> CheckExistsByFiscalYearAsync(int fiscalYearId);
        Task<IEnumerable<EntitlementDto>> GetAllEntitlementsAsync(int fiscalYearId);
        Task UpdateEntitlementAsync(int entitlementId, UpdateEntitlementRequest request);
        Task<EntitlementDto?> GetEntitlementByIdAsync(int entitlementId);
        Task<IEnumerable<EntitlementHistoryDto>> GetEntitlementHistoryAsync(int employeeId, int leaveTypeId, int fiscalYearId);
    }
}
