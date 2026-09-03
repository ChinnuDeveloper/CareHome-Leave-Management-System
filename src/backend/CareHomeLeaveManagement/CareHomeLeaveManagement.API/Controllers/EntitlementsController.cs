using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Entitlements.DTOs;
using CareHomeLeaveManagement.Application.Entitlements.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitlementsController : ControllerBase
    {
        private readonly IEntitlementService _entitlementService;

        public EntitlementsController(IEntitlementService entitlementService)
        {
            _entitlementService = entitlementService;
        }

        [HttpGet("fiscal-years")]
        public async Task<IActionResult> GetFiscalYears()
        {
            var fiscalYears = await _entitlementService.GetFiscalYearAsync();

            return Ok(fiscalYears);
        }

        [HttpGet("exists/{fiscalYearId:int}")]
        public async Task<IActionResult> CheckEntitlementExists(int fiscalYearId)
        {
            var exists = await _entitlementService.CheckExistsByFiscalYearAsync(fiscalYearId);
            return Ok(new { exists });
        }
        [HttpGet("{fiscalYearId:int}")]
        public async Task<IActionResult> GetAllEntitlements(int fiscalYearId)
        {
            var entitlements= await _entitlementService.GetAllEntitlementsAsync(fiscalYearId);
            return Ok(entitlements);
        }

        [HttpGet("entitlement/{entitlementId:int}")]
        public async Task<ActionResult<EmployeeDto>> GetEntitlementById(int entitlementId)
        {
            var entitlement = await _entitlementService.GetEntitlementByIdAsync(entitlementId);

            if (entitlement == null)
            {
                return NotFound();
            }

            return Ok(entitlement);
        }
        [HttpGet("history")]
        public async Task<IActionResult> GetEntitlementHistory(
            [FromQuery] int employeeId,
            [FromQuery] int leaveTypeId,
            [FromQuery] int fiscalYearId)
        {
            var history = await _entitlementService.GetEntitlementHistoryAsync(
                employeeId,
                leaveTypeId,
                fiscalYearId);

            return Ok(history);
        }

        [HttpPut("{entitlementId:int}")]
        public async Task<IActionResult> UpdateEntitlement(int entitlementId, [FromBody] UpdateEntitlementRequest request)
        {
            await _entitlementService.UpdateEntitlementAsync(entitlementId, request);

            return Ok(new
            {
                message = "Entitlement updated successfully."
            });
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] GenerateEntitlementRequest request)
        {
            try
            {
                await _entitlementService.GenerateEntitlementsAsync(request);

                return Ok(new
                {
                    message = "Entitlements generated successfully."
                });
            }
            catch (Exception ex) {
                return StatusCode(500, new
                {
                    message = ex.Message 
                });
            }
        }

    }
}
