using CareHomeLeaveManagement.Application.LeaveHistories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveHistoryController : ControllerBase
    {
        private readonly ILeaveHistoryService _leaveHistoryService;
        public LeaveHistoryController(ILeaveHistoryService leaveHistoryService)
        {
            _leaveHistoryService = leaveHistoryService;
        }

        [HttpGet("leave-request/{leaveRequestId}")]
        public async Task<IActionResult> GetByLeaveRequestId(int leaveRequestId)
        {
            var history=await _leaveHistoryService.GetByLeaveRequestIdAsync(leaveRequestId);

            return Ok(history);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var history = await _leaveHistoryService.GetByEmployeeIdAsync(employeeId);

            return Ok(history);
        }

    }
}
