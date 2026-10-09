using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Common.Interfaces;
using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using CareHomeLeaveManagement.Application.Employees.Services;
using CareHomeLeaveManagement.Application.Entitlements.DTOs;
using CareHomeLeaveManagement.Application.Entitlements.Interfaces;
using CareHomeLeaveManagement.Application.LeaveTypes.DTOs;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;
using CareHomeLeaveManagement.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CareHomeLeaveManagement.Application.Entitlements.Services
{
    public class EntitlementService : IEntitlementService
    {
        private readonly IEntitlementRepository _entitlementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EntitlementService> _logger;

        public EntitlementService(IEntitlementRepository entitlementRepository, 
            IEmployeeRepository employeeRepository,
            ILeaveTypeRepository leaveTypeRepository, 
            IUnitOfWork unitOfWork,
             ILogger<EntitlementService> logger)
        {
            _entitlementRepository = entitlementRepository;
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<FiscalYearDto>> GetFiscalYearAsync()
        {
            var fiscalYears= await _entitlementRepository.GetFiscalYearAsync();

            return fiscalYears.Select(f => new FiscalYearDto
            { 
                FiscalYearId=f.FiscalYearId, 
                Year = f.Year,                
                GeneratedBy = f.GeneratedBy,
                GeneratedOn =f.GeneratedOn,
                GeneratedByEmployee = f.GeneratedBy != null? $"{f.GeneratedByEmployee.FirstName} {f.GeneratedByEmployee.LastName}":null 
            });
        } 
        public async Task GenerateEntitlementsAsync(GenerateEntitlementRequest request)
        {
            try
            {
                var fiscalYear = await _entitlementRepository
                                .GetFiscalYearByIdAsync(request.FiscalYearId);

                if (fiscalYear == null)
                {
                    throw new KeyNotFoundException($"Fiscal year {request.FiscalYearId} was not found.");
                }

                //await _entitlementRepository.DeleteEntitlementsByFiscalYearAsync(request.FiscalYearId);

                //Get active employees
                var employees = await _employeeRepository.GetActiveEmployeeAsync();

                //Get leave types that require entitlement
                var leaveTypes = await _leaveTypeRepository.GetEntitlementLeaveTypesAsync();

                var entitlements = new List<Entitlement>();

                foreach (var employee in employees)
                {
                    foreach (var leaveType in leaveTypes)
                    {
                        var allocatedHours = employee.WeeklyHours * request.CalculationRule;

                        var entitlement = new Entitlement(
                            employee.EmployeeId,
                            leaveType.LeaveTypeId,
                            request.FiscalYearId,
                            allocatedHours);

                        entitlements.Add(entitlement);

                    }
                }

                await _entitlementRepository.AddEntitlementAsync(entitlements);

                fiscalYear.MarkEntitlementsGenerated(request.GeneratedBy, request.GeneratedOn);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating entitlement {FiscalYearId}",
                    request.FiscalYearId
                );

                throw;
            }

        }

        public async Task<bool> CheckExistsByFiscalYearAsync(int fiscalYearId)
        {
            return await _entitlementRepository.CheckExistsByFiscalYearAsync(fiscalYearId);
        }

        public async Task<IEnumerable<EntitlementDto>> GetAllEntitlementsAsync(int fiscalYearId)
        {
            var entitlements=await _entitlementRepository.GetAllEntitlementsAsync(fiscalYearId);


            return entitlements.Select(e => new EntitlementDto
            {
                 EntitlementId = e.EntitlementId,
                 EmployeeId = e.EmployeeId,
                 EmployeeName = $"{e.Employee.FirstName} {e.Employee.LastName}",
                 LeaveTypeId = e.LeaveTypeId,
                 LeaveType = e.LeaveType.Name,
                 FiscalYearId = e.FiscalYearId,
                 FiscalYear = e.FiscalYear.Year,
                 AllocatedHrs = e.AllocatedHrs,
                 TakenHrs = e.TakenHrs,
                 WeeklyHours=e.Employee.WeeklyHours,
                 DepartmentName=e.Employee.Department.DepartmentName,
                 Active=e.Employee.Active

            });
        }
        public async Task UpdateEntitlementAsync(int entitlementId, UpdateEntitlementRequest request)
        {
           var entitlement= await _entitlementRepository.GetEntitlementByIdAsync(entitlementId);
            if(entitlement==null)
            {
                throw new KeyNotFoundException($"Entitlement {entitlementId} was not found.");
            }

            var oldAllocationHrs = entitlement.AllocatedHrs;
            var oldTakenHrs = entitlement.TakenHrs;

            entitlement.Update(request.AllocatedHrs, request.TakenHrs);

            if(entitlement.Employee.WeeklyHours != request.WeeklyHours)
            {
                entitlement.Employee.UpdateWeeklyHours(request.WeeklyHours);
            } 
            
            var history= new EntitlementHistory(
                  entitlementId,
                  oldAllocationHrs,
                  oldTakenHrs,
                  request.AllocatedHrs,
                  request.TakenHrs,
                  request.Reason,
                  request.ModifiedBy,
                  DateTime.UtcNow);
             
            await _entitlementRepository.AddEntitlementHistoryAsync(history);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<EntitlementDto?> GetEntitlementByIdAsync(int entitlementId)
        {
            var entitlement = await _entitlementRepository.GetEntitlementByIdAsync(entitlementId);

            if (entitlement == null)
            {
                return null;
            }

            return new EntitlementDto
            {
                EntitlementId = entitlement.EntitlementId,
                EmployeeId = entitlement.EmployeeId,
                EmployeeName = $"{entitlement.Employee.FirstName} {entitlement.Employee.LastName}",
                LeaveTypeId = entitlement.LeaveTypeId,
                LeaveType = entitlement.LeaveType.Name,
                FiscalYearId = entitlement.FiscalYearId,
                FiscalYear = entitlement.FiscalYear.Year,
                AllocatedHrs = entitlement.AllocatedHrs,
                TakenHrs = entitlement.TakenHrs,
                WeeklyHours = entitlement.Employee.WeeklyHours
            };
        }

        public async Task<IEnumerable<EntitlementHistoryDto>> GetEntitlementHistoryAsync(int employeeId, int leaveTypeId, int fiscalYearId)
        {
            var history= await _entitlementRepository.GetEntitlementHistoryAsync(
                employeeId,
                leaveTypeId,
                fiscalYearId);

            return history.Select(h => new EntitlementHistoryDto
            {
                 EntitlementHistoryId=h.EntitlementHistoryId,
                 OldAllocationHrs=h.OldAllocationHrs,
                 OldTakenHrs=h.OldTakenHrs,
                 NewAllocationHrs=h.NewAllocationHrs,
                 NewTakenHrs=h.NewTakenHrs,
                 Reason= h.Reason,
                 ModifiedBy= h.ModifiedBy,
                 ModifiedOn= h.ModifiedOn
            });
        }
    }
}
