using CareHomeLeaveManagement.Application.LeaveRequests.DTOs;
using CareHomeLeaveManagement.Application.LeaveRequests.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareHomeLeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leaveRequest = await _leaveRequestService.GetByIdAsync(id);

            if(leaveRequest == null) 
                return NotFound();

            return Ok(leaveRequest);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployeeId(int employeeId)
        {
            var leaveRequests = await _leaveRequestService.GetByEmployeeIdAsync(employeeId);

            return Ok(leaveRequests);
        }

        [HttpGet("employee/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetByDateRange(
            DateTime fromDate, DateTime toDate)
        {
            var result = await _leaveRequestService.GetByDateRangeAsync(fromDate, toDate);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveRequestDto dto)
        {
            var leaveRequestId = await _leaveRequestService.CreateLeaveRequestAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new {id=leaveRequestId},
                new
                {
                    LeaveRequestId = leaveRequestId
                }
                );
        }

        [HttpPut("/approve")]
        public async Task<IActionResult> Approve(ApproveLeaveRequestDto dto)
        {
            try
            {
                await _leaveRequestService.ApproveLeaveRequestAsync(dto);

                return Ok(new
                {
                    Message = "Leave request approved successfully"
                });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("/reject")]
        public async Task<IActionResult> Reject(RejectLeaveRequestDto dto)
        {
            try
            {
                await _leaveRequestService.RejectLeaveRequestAsync(dto);

                return Ok(new
                {
                    Message = "Leave request rejected successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            
        }
        [HttpPut("{id}/{employeeId}/cancel")]
        public async Task<IActionResult> Cancel(int id,int employeeId)
        {
            try
            {
                await _leaveRequestService.CancelLeaveRequestsAsync(id, employeeId);

                return Ok(new
                {
                    Message = "Leave request cancelled successfully."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
