using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareHomeLeaveManagement.Application.Departments.DTOs;
using CareHomeLeaveManagement.Application.Departments.Interfaces;
using CareHomeLeaveManagement.Application.LeaveTypes.DTOs;
using CareHomeLeaveManagement.Application.LeaveTypes.Interfaces;

namespace CareHomeLeaveManagement.Application.LeaveTypes.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _repository;

        public LeaveTypeService(ILeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LeaveTypeDto>> GetAllAsync()
        {
            var leaveTypes = await _repository.GetAllAsync();

            return leaveTypes.Select(l => new LeaveTypeDto
            {
                 LeaveTypeId=l.LeaveTypeId,
                 Name=l.Name,
                 RequiresEntitlement=l.RequiresEntitlement
            });
        }
    }
}
