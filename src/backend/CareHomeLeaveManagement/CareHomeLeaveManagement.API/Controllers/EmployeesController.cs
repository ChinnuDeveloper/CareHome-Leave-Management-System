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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee= await _employeeService.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _employeeService.DeleteAsync(id);

            if(!deleted)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> Update(
            int id,
            [FromBody] UpdateEmployeeRequest request)
        {
            var employee = await _employeeService.UpdateAsync(id, request);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
