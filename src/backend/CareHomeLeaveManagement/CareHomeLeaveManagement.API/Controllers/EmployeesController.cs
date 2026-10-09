using CareHomeLeaveManagement.Application.Employees.DTOs;
using CareHomeLeaveManagement.Application.Employees.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{fiscalYearId:int}/{leaveTypeId:int}")]
        public async Task<IActionResult> GetAll(int fiscalYearId, int leaveTypeId)
        {
            var employees = await _employeeService.GetAllAsync(fiscalYearId,leaveTypeId);

            return Ok(employees);
        }

        [HttpGet("{id:int}/{fiscalYearId:int}/{leaveTypeId:int}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id, int fiscalYearId, int leaveTypeId)
        {
            var employee= await _employeeService.GetByIdAsync(id, fiscalYearId, leaveTypeId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet("{employeeId}/leave-balance")]
        public async Task<IActionResult> GetLeaveBalance(int employeeId, [FromQuery] int leaveTypeId, [FromQuery] int fiscalYearId)
        {
            var result = await _employeeService.GetLeaveBalanceAsync(employeeId, leaveTypeId, fiscalYearId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(
            CreateEmployeeRequest request)
        {
            var employee = await _employeeService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new {id= employee?.EmployeeId},
                employee);
        }

        [HttpDelete("{id:int}/{fiscalYearId:int}/{leaveTypeId:int}")]
        public async Task<IActionResult> Delete(int id, int fiscalYearId, int leaveTypeId)
        {
            var deleted = await _employeeService.DeleteAsync(id, fiscalYearId, leaveTypeId);

            if(!deleted)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpPut("{id:int}/{fiscalYearId:int}/{leaveTypeId:int}")]
        public async Task<ActionResult<EmployeeDto>> Update(
            int id,
            int fiscalYearId, 
            int leaveTypeId,
            [FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _employeeService.UpdateAsync(id, fiscalYearId, leaveTypeId, request);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
