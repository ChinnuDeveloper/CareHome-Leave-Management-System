using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypeService _leaveService;

        public LeaveTypesController(ILeaveTypeService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypes = await _leaveService.GetAllAsync();

            return Ok(leaveTypes);
        }

    }
}
